using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Worker
{
    public class GetWorkerListResponse : Response
    {
        public List<Worker> Data { get; set; }
    }
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string IsActiveEnum { get; set; }
        public string CNIC { get; set; }
        public string CNICFrontImageUrl { get; set; }
        public string CNICBackImageUrl { get; set; }
        public int ControlCenterId { get; set; }
        public string ControllerName { get; set; }
        public string ControlCenterName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime Date { get; set; }

    }
    public class GetWorkerListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetWorkerListRequest req)
        {
            var response = new GetWorkerListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<Worker>();
            try
            {
                var Workers = _dbContext.UserProfile.AsNoTracking().Where(x=>x.UserType == (int)UserType.Worker).ToList();
                foreach (var worker in Workers)
                {
                    var Worker = new Worker();
                    Worker.Id = worker.Id;
                    Worker.Name = worker.FirstName + " " + worker.LastName;
                    Worker.CNIC = worker.CNIC;
                    Worker.Address = worker.Address;
                    Worker.IsActive = worker.IsActive;
                    if (worker.IsActive)
                    {
                        Worker.IsActiveEnum = "Active";
                    }
                    else
                    {
                        Worker.IsActiveEnum = "In Active";
                    }
                    Worker.ControlCenterId = worker.ControllerCenterId??0;
                    Worker.ProfileImageUrl = worker.ProfileImageUrl;
                    Worker.ControlCenterName = _dbContext.ControlCenter.Where(x=>x.Id == worker.ControllerCenterId).SingleOrDefault().Name;
                    Worker.ControllerName = _dbContext.UserProfile.Where(x => x.ControllerCenterId == worker.ControllerCenterId).FirstOrDefault().FullName;
                    response.Data.Add(Worker);
                }
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.ValidationErrors.Add(e.Message.ToString());
            }
            return response;
        }
    }
}

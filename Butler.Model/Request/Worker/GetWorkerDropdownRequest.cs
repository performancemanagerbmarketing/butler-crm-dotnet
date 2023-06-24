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
    public class GetWorkerDropdownResponse : Response
    {
        public List<WorkerDropdown> Data { get; set; }
    }
    public class WorkerDropdown
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string Contact { get; set; }
    }
    public class GetWorkerDropdownRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public int ControllCenterId { get; set; }
        public object RunRequest(GetWorkerDropdownRequest req)
        {
            var response = new GetWorkerDropdownResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<WorkerDropdown>();
            try
            {
                var Workers = from p in _dbContext.UserProfile.Where(x => x.UserType == (int)UserType.Worker && x.IsActive == true)
                              select new { p.Id, p.FirstName, p.LastName, p.CNIC, p.Contact };
                response.Data = Workers.Select(x => new WorkerDropdown { Id = x.Id, Name = x.FirstName + " " + x.LastName, CNIC = x.CNIC, Contact = x.Contact}).ToList();
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

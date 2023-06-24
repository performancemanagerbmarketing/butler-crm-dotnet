using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Worker
{
    public class AddWorkerResponse : Response { }
    public class AddWorkerRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }
        public string CNIC { get; set; }
        public string CNICFrontImageUrl { get; set; }
        public string CNICBackImageUrl { get; set; }
        public int ControlCenterId { get; set; }
        public string ControllerName { get; set; }
        public string ControlCenterName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public bool IsAdded { get; set; }
        public string ProfileImageUrl { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddWorkerRequest req)
        {
            var response = new AddWorkerResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Worker = new Butler.Model.EntityModel.Worker();
                Worker.Name = req.Name;
                Worker.CNIC = req.CNIC;
                Worker.Contact = req.Contact;
                Worker.ProfileImageUrl = req.ProfileImageUrl;
                Worker.Description = req.Description;
                Worker.CNICBackImageUrl = req.CNICBackImageUrl;
                Worker.CNICFrontImageUrl = req.CNICFrontImageUrl;
                Worker.ControlCenterId = req.ControlCenterId;
                Worker.ControlCenterName = req.ControlCenterName;
                if(Worker.ControlCenterId != 0)
                {
                    var ControllerName = _dbContext.UserProfile.Where(x => x.ControllerCenterId == Worker.ControlCenterId).FirstOrDefault();
                    if(ControllerName != null)
                    {
                        Worker.ControllerName = ControllerName.FullName;
                    }
                }
               
                Worker.CreatedAt = DateTime.Now;
                Worker.Date = DateTime.Today;
                Worker.IsAdded = true;
                _dbContext.Worker.Add(Worker);
                _dbContext.SaveChanges();
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

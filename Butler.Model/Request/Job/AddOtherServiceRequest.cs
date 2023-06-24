using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Job
{
    public class AddOtherServiceResponse : Response { }
    public class AddOtherServiceRequest
    {
        public List<EntityModel.Others> Others { get; set; }
        public string UserId { get; set; }
        public int JobId { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddOtherServiceRequest req)
        {
            var response = new AddOtherServiceResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Agent = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).FirstOrDefault();
                var Job = _dbContext.Job.Where(x=>x.Id == req.JobId).FirstOrDefault();
                if(Job.Others != null)
                {
                    foreach(var other in Job.Others)
                    {
                        Job.TotalAmount = Job.TotalAmount - other.Cost;
                        _dbContext.Others.Remove(other);
                        _dbContext.SaveChanges();
                    }
                }
                var Others = new EntityModel.Others();
                foreach (var other in req.Others)
                {
                    other.CreatedBy = Agent.FullName;
                    other.CreatedAt = DateTime.Now;
                    other.Date = DateTime.Today;
                    other.JobId = req.JobId;
                    _dbContext.Others.Add(other);
                    Job.TotalAmount = Job.TotalAmount + other.Cost;
                    _dbContext.SaveChanges();
                }
                response.Success = true;
            }
            catch(Exception e)
            {
                response.ValidationErrors.Add(e.Message.ToString());
                response.Success = false;
            }
            return response;
        }
    }
}

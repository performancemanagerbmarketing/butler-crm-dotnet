using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Job
{
    public class AddJobWorkerResponse : Response { }
    public class JobWorker
    {
        public int JobId { get; set; }
        public int WorkerId { get; set; }
        public string WorkerName { get; set; }
        public string Contact { get; set; }
        public string CNIC { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public bool IsAdded { get; set; }
    }
    public class AddJobWorkerRequest
    {
        public string UserId { get; set; }
        public int JobId { get; set; }
        public List<JobWorker> JobWorker { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddJobWorkerRequest req)
        {
            var response = new AddJobWorkerResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var User = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).FirstOrDefault();
                var items = _dbContext.JobWorker.Where(x => x.JobId == req.JobId).ToList();
                foreach (var item in items)
                {
                    _dbContext.JobWorker.Remove(item);
                    _dbContext.SaveChanges();
                }
               
                foreach (var JW in req.JobWorker)
                {
                    var JobWorker = new Butler.Model.EntityModel.JobWorker();
                    JobWorker.JobId = JW.JobId;
                    JobWorker.WorkerId = JW.WorkerId;
                    JobWorker.WorkerName = JW.WorkerName;
                    JobWorker.CreatedAt = DateTime.Now;
                    JobWorker.Date = DateTime.Today;
                    JobWorker.CreatedBy = User.FullName;
                    _dbContext.JobWorker.Add(JobWorker);
                    _dbContext.SaveChanges();
                }
                var Notification = new Butler.Model.EntityModel.Notification();
                Notification.AdminId = User.Id;
                Notification.CustomerId = _dbContext.Job.Where(x => x.Id == req.JobId).FirstOrDefault().CustomerId;
                Notification.IsRead = false;
                Notification.Content = "Your Order has been Assign to Worker Name " + String.Join(", " , req.JobWorker.Select(s=>s.WorkerName).ToArray());
                Notification.Title = "Job Assign to Worker";
                Notification.CreatedBy = User.FullName;
                Notification.CreatedAt = DateTime.Now;
                Notification.Date = DateTime.Today;
                _dbContext.Notification.Add(Notification);
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

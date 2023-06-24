using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.WorkerAppRequest.JobAssign
{
    public class PreviousJobResponse : Response
    {
        public List<JobClass> Jobs { get; set; }
    }
    public class PreviousJobRequest
    {
        public int WorkerId { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(PreviousJobRequest req)
        {
            var response = new PreviousJobResponse();
            response.ValidationErrors = new List<string>();
            response.Jobs = new List<JobClass>();
            try
            {
                var WorkerJob = _dbContext.JobWorker.Where(x => x.WorkerId == req.WorkerId).ToList();
                var Jobs = WorkerJob.Select(x => x.Job).ToList();
                foreach (var Job in Jobs.Where(x => x.Status == (int)JobStatus.Complete || x.Status == (int)JobStatus.Cancelled))
                {
                    var job = new JobClass();
                    job.Id = Job.Id;
                    job.BookingDate = Job.BookingDate;
                    job.CustomerName = Job.CustomerName;
                    job.Title = Job.Title;
                    job.Description = Job.Description;
                    job.Status = Job.Status;
                    job.StatusEnum = ((JobStatus)Job.Status.Value).ToString();
                    job.ServiceImage = Job.Category.ProfileImageUrl;
                    job.ServiceName = String.Join(", ", Job.JobDetail.Select(x => x.SubCategoryName).ToArray());
                    if (Job.StartTime.HasValue && Job.EndTime.HasValue)
                    {
                        job.StartTime = Job.StartTime;
                        job.EndTime = Job.EndTime;
                        job.Duration = Job.Duration;
                    }
                    response.Jobs.Add(job);
                }
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.ValidationErrors.Add(e.Message.ToString());
            }
            return response; ;
        }
    }
}

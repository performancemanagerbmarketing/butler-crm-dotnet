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
    public class UpdateJobStatusResponse : Response { }
    public class UpdateJobStatusRequest
    {
        public int JobId { get; set; }
        public int Status { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(UpdateJobStatusRequest req)
        {
            var response = new UpdateJobStatusResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Job = _dbContext.Job.Where(x => x.Id == req.JobId).SingleOrDefault();
                if(Job != null)
                {
                    if(req.Status == (int)JobStatus.In_Progress)
                    {
                        Job.StartTime = DateTime.Now;
                        Job.Status = (int)JobStatus.In_Progress;
                    }
                    if(req.Status == (int)JobStatus.Complete)
                    {
                        Job.EndTime = DateTime.Now;
                        Job.Status = (int)JobStatus.Complete;
                        if (Job.StartTime != null)
                        {
                            TimeSpan Difference = Job.EndTime.Value - Job.StartTime.Value;
                            Job.Duration = Difference.Hours.ToString();
                        }
                    }
                    _dbContext.SaveChanges();
                }
                response.Success = true;
            }
            catch(Exception e)
            {
                response.Success = false;
                response.ValidationErrors.Add(e.Message.ToString());
            }
            return response;
        }
    }
}

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
    public class EndJobResponse : Response
    {
        public bool EndJobStatus { get; set; }
    }
    public class EndJobRequest
    {
        public int JobId { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EndJobRequest req)
        {
            var response = new EndJobResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Job = _dbContext.Job.Where(x => x.Id == req.JobId).SingleOrDefault();
                if (Job != null)
                {
                    Job.EndTime = DateTime.Now;
                    Job.Status = (int)JobStatus.Processing;
                    if (Job.StartTime != null)
                    {
                        TimeSpan Difference = Job.EndTime.Value - Job.StartTime.Value;
                        Job.Duration = Difference.Hours.ToString();
                    }
                    _dbContext.SaveChanges();
                    response.EndJobStatus = true;
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

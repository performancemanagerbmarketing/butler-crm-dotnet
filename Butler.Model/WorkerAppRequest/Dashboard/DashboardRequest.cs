using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.WorkerAppRequest.Dashboard
{
    public class DashboardResponse : Response
    {
        public int TotalJob { get; set; }
        public int TotalInProgress { get; set; }
        public int TotalPending { get; set; }
        public int TotalCompleted { get; set; }
        public int TotalCancelled { get; set; }
    }
    public class DashboardRequest
    {
        public int WorkerId { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(DashboardRequest req)
        {
            var response = new DashboardResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Worker = _dbContext.JobWorker.Where(x => x.WorkerId == req.WorkerId).ToList();
                response.TotalJob = Worker.Count();
                response.TotalPending = Worker.Where(x => x.Job.Status == (int)JobStatus.Pending).ToList().Count();
                response.TotalInProgress = Worker.Where(x => x.Job.Status == (int)JobStatus.In_Progress || x.Job.Status == (int)JobStatus.Processing).ToList().Count();
                response.TotalCompleted = Worker.Where(x => x.Job.Status == (int)JobStatus.Complete).ToList().Count();
                response.TotalCancelled = Worker.Where(x => x.Job.Status == (int)JobStatus.Cancelled).ToList().Count();
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

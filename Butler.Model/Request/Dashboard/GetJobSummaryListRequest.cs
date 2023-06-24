using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Dashboard
{
    public class GetJobSummaryListResponse : Response
    {
        public List<JobSummaryData> PendingJobList { get; set; }
        public List<JobSummaryData> StartedJobList { get; set; }
        public List<JobSummaryData> InProcessJobList { get; set; }
        public List<JobSummaryData> CompletedJobList { get; set; }
        public List<JobSummaryData> CancelledJobList { get; set; }
    }
   public class JobSummaryData
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Services { get; set; }
        public string Workers { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? MaterialCost { get; set; }
    }
    public class GetJobSummaryListRequest
    {
        public string UserId { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DateFrom { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetJobSummaryListRequest req)
        {
            var response = new GetJobSummaryListResponse();
            response.PendingJobList = new List<JobSummaryData>();
            response.StartedJobList = new List<JobSummaryData>();
            response.InProcessJobList = new List<JobSummaryData>();
            response.CompletedJobList = new List<JobSummaryData>();
            response.CancelledJobList = new List<JobSummaryData>();
            response.ValidationErrors = new List<string>();
            response.Success = true;
            try
            {
                var Agent = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).FirstOrDefault();
                var Jobs = _dbContext.Job.AsNoTracking().Take(1000).OrderByDescending(x => x.Id).ToList();
                if (req.DateFrom.HasValue)
                {
                    Jobs = Jobs.Where(x => x.BookingDate >= req.DateFrom).ToList();
                }
                if (req.DateTo.HasValue)
                {
                    Jobs = Jobs.Where(x => x.BookingDate <= req.DateTo).ToList();
                }
                
                var PendingJobs = Jobs.Where(X => X.Status == (int)JobStatus.Pending);
                foreach(var pj in PendingJobs)
                {
                    var row = new JobSummaryData();
                    row.Address = pj.CustomerAddress;
                    row.Contact = pj.CustomerContact;
                    row.CreatedAt = pj.CreatedAt;
                    row.Customer = pj.CustomerName;
                    row.Description = pj.Description;
                    row.Id = pj.Id;
                    row.Services = string.Join(",", pj.JobDetail.Select(x => x.SubCategoryName));
                    row.Status = ((JobStatus)pj.Status).ToString();
                    row.UpdatedAt = pj.UpdatedAt;
                    response.PendingJobList.Add(row);
                }
                var StartedJobList = Jobs.Where(X => X.Status == (int)JobStatus.In_Progress);
                foreach(var pj in StartedJobList)
                {
                    var row = new JobSummaryData();
                    row.Address = pj.CustomerAddress;
                    row.Contact = pj.CustomerContact;
                    row.CreatedAt = pj.CreatedAt;
                    row.Customer = pj.CustomerName;
                    row.Description = pj.Description;
                    row.Id = pj.Id;
                    row.Services = string.Join(",", pj.JobDetail.Select(x => x.SubCategoryName));
                    row.Status = ((JobStatus)pj.Status).ToString();
                    row.UpdatedAt = pj.UpdatedAt;
                    response.PendingJobList.Add(row);
                }
                var InProcessJobList = Jobs.Where(X => X.Status == (int)JobStatus.Processing);
                foreach(var pj in InProcessJobList)
                {
                    var row = new JobSummaryData();
                    row.Address = pj.CustomerAddress;
                    row.Contact = pj.CustomerContact;
                    row.CreatedAt = pj.CreatedAt;
                    row.Customer = pj.CustomerName;
                    row.Description = pj.Description;
                    row.Id = pj.Id;
                    row.Services = string.Join(",", pj.JobDetail.Select(x => x.SubCategoryName));
                    row.Status = ((JobStatus)pj.Status).ToString();
                    row.UpdatedAt = pj.UpdatedAt;
                    response.InProcessJobList.Add(row);
                } 
                var CompletedJobList = Jobs.Where(X => X.Status == (int)JobStatus.Complete);
                foreach(var pj in CompletedJobList)
                {
                    var row = new JobSummaryData();
                    row.Address = pj.CustomerAddress;
                    row.Contact = pj.CustomerContact;
                    row.CreatedAt = pj.CreatedAt;
                    row.Customer = pj.CustomerName;
                    row.Description = pj.Description;
                    row.Id = pj.Id;
                    row.Services = string.Join(",", pj.JobDetail.Select(x => x.SubCategoryName));
                    row.Workers = string.Join(",", pj.JobWorker.Select(x => x.WorkerName));
                    row.Status = ((JobStatus)pj.Status).ToString();
                    row.UpdatedAt = pj.UpdatedAt;
                    row.MaterialCost = pj.MaterialCost.Sum(x=>x.Cost);
                    row.TotalAmount = pj.TotalAmount;
                    response.CompletedJobList.Add(row);
                } 
                var CancelledJobList = Jobs.Where(X => X.Status == (int)JobStatus.Cancelled);
                foreach(var pj in CancelledJobList)
                {
                    var row = new JobSummaryData();
                    row.Address = pj.CustomerAddress;
                    row.Contact = pj.CustomerContact;
                    row.CreatedAt = pj.CreatedAt;
                    row.Customer = pj.CustomerName;
                    row.Description = pj.Description;
                    row.Id = pj.Id;
                    row.Services = string.Join(",", pj.JobDetail.Select(x => x.SubCategoryName));
                    row.Workers = string.Join(",", pj.JobWorker.Select(x => x.WorkerName));
                    row.Status = ((JobStatus)pj.Status).ToString();
                    row.UpdatedAt = pj.UpdatedAt;
                    row.MaterialCost = pj.MaterialCost.Sum(x=>x.Cost);
                    row.TotalAmount = pj.TotalAmount;
                    response.CancelledJobList.Add(row);
                }
            }
            catch (Exception e)
            {
                var ex = e;
                response.Success = false;
                response.ValidationErrors.Add(e.InnerException.ToString());
            }
            return response;
        }

    }
}

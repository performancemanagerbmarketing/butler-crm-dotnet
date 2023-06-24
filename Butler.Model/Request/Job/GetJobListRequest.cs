using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Job
{
    public class GetJobListResponse : Response
    {
        public List<Job> Data { get; set; }
    }
    public class Job
    {
        public string DateString { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public decimal Latitude { get; set; }
        public string BookingDate { get; set; }
        public decimal Longitutde { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImageUrl { get; set; }
        public string JobAddress { get; set; }
        public int PaymentStatus { get; set; }
        public int ControlCenterId { get; set; }
        public string ControlCenterName { get; set; }
        public string AudioUrl { get; set; }
        public List<string> ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public string CustomerContact { get; set; }
        public bool IsAdded { get; set; }
        public string StatusEnum { get; set; }
        public List<Category> Category { get; set; }
        public List<ControlCenter> ControlCenter { get; set; }
        public List<JobDetail> JobDetail { get; set; }
        public List<JobWorker> JobWorker { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Amount { get; set; }
        public string SubAmount { get; set; }
        public string AdditionalKey { get; set; }
        public string MaterialCost { get; set; }
        public string PaymentStatusEnum { get; set; }
        public int TotalWorker { get; set; }
        public string WorkerNames { get; set; }
    }
    public class GetJobListRequest
    {
        public bool IsController { get; set; }
        public string UserId { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetJobListRequest req)
        {
            var response = new GetJobListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<Job>();
            try
            {
                var CurrentUser = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).First();
                var Jobs = new List<Model.EntityModel.Job>();//_dbContext.Job.ToList();
                Jobs = _dbContext.Job.OrderByDescending(o => o.Id).ToList();
                //if (req.IsController)
                //{
                //    if (CurrentUser != null)
                //        Jobs = _dbContext.Job.Where(x => x.ControlCenterId == CurrentUser.ControllerCenterId).OrderByDescending(o => o.Id).ToList();
                //}
                //else
                //{
                //    Jobs = _dbContext.Job.OrderByDescending(o=>o.Id).ToList();
                //}
                foreach (var job in Jobs)
                {
                    var Job = new Job();
                    Job.Id = job.Id;
                    if (job.CustomerId.HasValue)
                    {
                        Job.CustomerId = job.CustomerId.Value;
                    }
                    Job.CustomerName = job.CustomerName;
                    Job.CustomerContact = job.CustomerContact;
                    Job.CustomerEmail = job.CustomerEmail;
                    Job.CustomerAddress = job.CustomerAddress;
                    Job.ImageUrl = new List<string>();
                    Job.ImageUrl.Add(job.ImageUrl);
                    Job.ImageUrl.Add(job.ImageUrl2);
                    Job.ImageUrl.Add(job.ImageUrl3);
                    if(job.JobDetail != null)
                    {
                        var SubCategory = job.JobDetail.Select(x => x.SubCategoryId);
                        var Sub = _dbContext.SubCategory.Where(x => x.Id == SubCategory.FirstOrDefault()).FirstOrDefault();
                        if(Sub.AdditionalInfoKey != null)
                        {
                            Job.AdditionalKey = Sub.AdditionalInfoKey;
                        }
                    }
                    Job.CustomerContact = job.CustomerContact;
                    Job.Status = job.Status??0;
                    Job.JobAddress = job.JobAddress;
                    if (job.BookingDate.HasValue)
                    {
                        Job.BookingDate = job.BookingDate.Value.ToString("dd-MMM-yyyy hh:mm tt");
                    }
                    
                    Job.PaymentStatus = job.PaymentStatus;
                    Job.StatusEnum = ((JobStatus)job.Status.Value).ToString();
                    Job.Title = job.Title;
                    Job.Description = job.Description;
                    Job.CategoryName = job.CategoryName;
                    var JobDetail = job.JobDetail;
                    if (JobDetail != null)
                    {
                        var SubAmount = JobDetail.Sum(s => s.Amount);
                        Job.SubAmount = SubAmount.Value.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                    }
                    var MaterialCost = job.MaterialCost;
                    if (MaterialCost != null)
                    {
                        var MaterialAmount = MaterialCost.Sum(s => s.Cost);
                        Job.MaterialCost = MaterialAmount.Value.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                    }
                    if (job.TotalAmount.HasValue)
                    {
                        Job.Amount = job.TotalAmount.Value.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                    }
                    else
                    {
                        decimal TotalAmount = 0;
                        if(Job.SubAmount != null)
                        {
                            TotalAmount = job.JobDetail.Sum(s => s.Amount).Value;
                        }
                        
                        if(Job.MaterialCost != null)
                        {
                            var MaterialCosts = job.MaterialCost.Sum(s => s.Cost);
                            TotalAmount = TotalAmount + MaterialCosts.Value;
                        }
                        Job.Amount = TotalAmount.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                    }
                    Job.PaymentStatusEnum = ((PaymentStatus)job.PaymentStatus).ToString();
                    if (job.CategoryName == null)
                    {
                        if(job.CategoryId != null)
                        {
                            Job.CategoryName = _dbContext.Category.Where(x => x.Id == job.CategoryId).FirstOrDefault().Name;
                        }
                        
                    }
                    if(job.JobDetail != null)
                    {
                        Job.JobDetail = new List<JobDetail>();
                        foreach (var JD in job.JobDetail)
                        {
                            var JobDetails = new JobDetail();
                            JobDetails.JobId = JD.JobId;
                            JobDetails.SubCategoryId = JD.SubCategoryId;
                            JobDetails.SubCategoryName = JD.SubCategoryName;
                            JobDetails.Amount = JD.Amount.Value;
                            if(JD.Discount != null)
                            {
                                JobDetails.Discount = JD.Discount.Value;
                            }
                            Job.JobDetail.Add(JobDetails);
                        }
                        //Job.TotalAmount = job.JobDetail.Sum(s => s.Amount) ?? 0;
                       
                    }
                    if (job.JobWorker != null && job.JobWorker.Count() != 0)
                    {
                        Job.JobWorker = new List<JobWorker>();
                        foreach (var JW in job.JobWorker)
                        {
                            var JobWorker = new JobWorker();
                            JobWorker.JobId = JW.JobId;
                            JobWorker.WorkerId = JW.WorkerId;
                            JobWorker.WorkerName = JW.WorkerName;
                            var Worker = _dbContext.Worker.Where(x => x.Id == JW.WorkerId).FirstOrDefault();
                            if(Worker != null)
                            {
                                JobWorker.CNIC = Worker.CNIC;
                                JobWorker.Contact = Worker.Contact;
                                Job.JobWorker.Add(JobWorker);
                            }
                        }
                        Job.TotalWorker = job.JobWorker.Count();
                    }
                    Job.WorkerNames = String.Join(", ", job.JobWorker.Select(s => s.WorkerName).ToArray());
                    Job.ControlCenterName = job.ControlCenterName;
                    response.Data.Add(Job);
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

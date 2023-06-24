using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Job
{
    public class ActiveBookingResponse : Response
    {
        public List<Job> Data { get; set; }
       
    }
    public class ActiveBookingRequest
    {
        public int CustomerId { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(ActiveBookingRequest req)
        {
            var response = new ActiveBookingResponse();
            response.ValidationErrors = new List<string>();
            
            response.Data = new List<Job>();
            try
            {
                var Jobs = _dbContext.Job.Where(x=> x.CustomerId == req.CustomerId &&  (x.Status != (int)JobStatus.Complete && x.Status != (int)JobStatus.Cancelled)).ToList();
                foreach (var job in Jobs)
                {
                    var Job = new Job();
                    Job.JobDetail = new List<JobDetail>();
                    Job.JobWorker = new List<JobWorker>();
                    Job.Id = job.Id;
                    if (job.BookingDate.HasValue)
                    {
                        Job.BookingDate = job.BookingDate.Value.ToString("dd-MMM-yyyy hh:mm tt");
                    }
                    Job.CustomerName = job.CustomerName;
                    Job.CustomerEmail = job.CustomerEmail;
                    Job.CustomerAddress = job.CustomerAddress;
                    Job.ImageUrl = new List<string>();
                    Job.ImageUrl.Add(job.ImageUrl);
                    Job.ImageUrl.Add(job.ImageUrl2);
                    Job.ImageUrl.Add(job.ImageUrl3);
                    Job.CustomerContact = job.CustomerContact;
                    Job.Status = job.Status ?? 0;
                    Job.StatusEnum = ((JobStatus)job.Status.Value).ToString();
                    Job.Title = job.Title;
                    Job.Description = job.Description;
                    Job.CategoryName = job.CategoryName;
                    Job.ControlCenterName = job.ControlCenterName;
                    Job.CategoryId = job.CategoryId;
                    if (job.CategoryName == null)
                    {
                        var Cat = _dbContext.Category.Where(x => x.Id == job.CategoryId).FirstOrDefault();
                        if (Cat != null)
                        {
                            Job.CategoryName = Cat.Name;
                        }
                    }
                    if(Job.CategoryImageUrl == null)
                    {
                        var Cat = _dbContext.Category.Where(x => x.Id == job.CategoryId).FirstOrDefault();
                        if (Cat != null)
                        {
                            Job.CategoryImageUrl = Cat.ProfileImageUrl;
                        }
                    }
                    if (job.JobDetail != null && job.JobDetail.Count() != 0)
                    {
                        foreach (var JD in job.JobDetail)
                        {
                            var JobDetail = new JobDetail();
                            JobDetail.JobId = JD.JobId;
                            JobDetail.SubCategoryId = JD.SubCategoryId;
                            JobDetail.SubCategoryName = JD.SubCategoryName;
                            JobDetail.Amount = JD.Amount.Value;
                            if (JD.Discount != null)
                            {
                                JobDetail.Discount = JD.Discount.Value;
                            }

                            Job.JobDetail.Add(JobDetail);
                        }
                        Job.TotalAmount = job.JobDetail.Sum(s => s.Amount)??0;
                    }
                    if (job.JobWorker != null && job.JobWorker.Count() != 0)
                    {
                        foreach (var JW in job.JobWorker)
                        {
                            var JobWorker = new JobWorker();
                            JobWorker.JobId = JW.JobId;
                            JobWorker.WorkerId = JW.WorkerId;
                            JobWorker.WorkerName = JW.WorkerName;
                            var Worker = _dbContext.Worker.Where(x => x.Id == JW.WorkerId).FirstOrDefault();
                            JobWorker.CNIC = Worker.CNIC;
                            JobWorker.Contact = Worker.Contact;
                            Job.JobWorker.Add(JobWorker);
                        }
                        Job.TotalWorker = job.JobWorker.Count();
                    }
                    
                    var category = _dbContext.Category.Where(x => x.Id == Job.CategoryId).FirstOrDefault();
                    Job.DateString = job.Date.Value.ToString("MM/dd/yyyy");
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

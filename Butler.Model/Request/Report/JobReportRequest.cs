using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Report
{
    public class JobReportResponse : Response
    {
        public List<Report> Data { get; set; }
    }
    public class Report
    {
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public int ServiceRating { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int TotalJobDone { get; set; }
        public int ReferenceByCustomer { get; set; }
        public string JobLocation { get; set; }
        public int JobNumber { get; set; }
        public string Nature { get; set; }
        public string JobAddress { get; set; }
        public string WorkerName { get; set; }
        public string WorkerId { get; set; }
        public string ControllerName { get; set; }
        public int ControlCenterId { get; set; }
        public int ControllerId { get; set; }
        public string BookingDate { get; set; }
        public string ExecutionDate { get; set; }
        public string ControlCenterName { get; set; }
        public string CompletionDate { get; set; }
        public decimal JobStart { get; set; }
        public decimal JobEnd { get; set; }
        public TimeSpan JobDuration { get; set; }
        public decimal JobValue { get; set; }
        public decimal DiscountOffer { get; set; }
        public decimal ValueAgreed { get; set; }
        public string ServiceName { get; set; }
        public string ServiceCost { get; set; }
        public string MaterialName { get; set; }
        public string MaterialCost { get; set; }
        public string PaymentStatus { get; set; }
        public string StatusEnum { get; set; }
        public decimal RatePerHour { get; set; }
        public int Id { get; set; }
        public string TotalAmount { get; set; }
    }
    public class JobReportRequest
    {
        public string UserId { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(JobReportRequest req)
        {
            var response = new JobReportResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<Report>();
            try
            {
                var Agent = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).FirstOrDefault();
                var Jobs = _dbContext.Job.ToList();
                if (Agent.UserType == (int)UserType.Controller)
                {
                    Jobs = Jobs.Where(x => x.ControlCenterId == Agent.ControllerCenterId).ToList();
                }
                foreach(var Job in Jobs)
                {
                    var row = new Report();
                    row.Id = Job.Id;
                    row.Title = Job.Title;
                    row.Description = Job.Description;
                    if (Job.CustomerId.HasValue)
                    {
                        row.CustomerId = Job.CustomerId.Value;
                        row.CustomerName = Job.CustomerName;
                        row.CustomerNumber = Job.CustomerContact;
                        row.Email = Job.CustomerEmail;
                        row.JobAddress = Job.JobAddress;
                    }
                    if(Job.ServiceRating.Count > 0)
                    {
                        var Rating = Job.ServiceRating.FirstOrDefault();
                        if(Rating.NoOfRating != null && Rating.NoOfRating != 0)
                        {
                            row.ServiceRating = Rating.NoOfRating.Value;
                        }
                    }
                    if(Job.JobWorker.Count > 0)
                    {
                        row.WorkerName = String.Join(", ", Job.JobWorker.Select(s => s.WorkerName).ToArray());
                        row.WorkerId = String.Join("# ", Job.JobWorker.Select(s => s.WorkerId).ToArray());
                    }
                    if(Job.ControlCenterId.HasValue)
                    {
                        row.ControlCenterId = Job.ControlCenterId.Value;
                        row.ControlCenterName = Job.ControlCenterName;
                        var CtrlName = _dbContext.UserProfile.Where(x => x.ControllerCenterId == Job.ControlCenterId).FirstOrDefault();
                        row.ControllerName = CtrlName.FirstName + " " + CtrlName.LastName;
                    }
                    if (Job.BookingDate.HasValue)
                    {
                        row.BookingDate = Job.BookingDate.Value.ToString("dd-MMM-yyyy hh:mm tt");
                    }
                    if (Job.StartTime.HasValue)
                    {
                        row.ExecutionDate = Job.StartTime.Value.ToString("dd-MMM-yyyy hh:mm tt");
                        row.JobStart = Job.StartTime.Value.Hour;
                    }
                    if (Job.EndTime.HasValue)
                    {
                        row.CompletionDate = Job.CompleteDateTime.Value.ToString("dd-MMM-yyyy hh:mm tt");
                        row.JobEnd = Job.EndTime.Value.Hour;
                        var Duration = Job.EndTime.Value - Job.StartTime.Value;
                        row.JobDuration = Duration;
                    }
                    if (Job.CompleteDateTime.HasValue)
                    {
                        row.CompletionDate  = Job.CompleteDateTime.Value.ToString("dd-MMM-yyyy hh:mm tt");
                    }
                    if(Job.JobDetail.Count() > 0)
                    {
                        row.ServiceName = String.Join(", ", Job.JobDetail.Select(s => s.SubCategoryName).ToArray());
                        row.ServiceCost = Job.JobDetail.Sum(s => s.Amount).Value.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                    }
                    if(Job.JobWorker.Count() > 0)
                    {
                        row.MaterialName = String.Join(", ", Job.MaterialCost.Select(s => s.MaterialName).ToArray());
                        row.MaterialCost = Job.MaterialCost.Sum(s => s.Cost).Value.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                    }
                    if (Job.TotalAmount.HasValue)
                    {
                        row.TotalAmount = Job.TotalAmount.Value.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                    }
                    else
                    {
                        decimal TotalAmount = 0;
                        if (row.ServiceCost != null)
                        {
                            TotalAmount = Job.JobDetail.Sum(s => s.Amount).Value;
                        }

                        if (row.MaterialCost != null)
                        {
                            var MaterialCost = Job.MaterialCost.Sum(s => s.Cost);
                            TotalAmount = TotalAmount + MaterialCost.Value;
                        }
                        row.TotalAmount = TotalAmount.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                    }
                    row.PaymentStatus = ((PaymentStatus)Job.PaymentStatus).ToString();
                    row.StatusEnum = ((JobStatus)Job.Status.Value).ToString();
                    response.Data.Add(row);
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

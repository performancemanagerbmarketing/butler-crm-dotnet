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
    public class GetJobResponse : Response
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string JobAddress { get; set; }
        public int PaymentStatus { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitutde { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ControlCenterId { get; set; }
        public string ControlCenterName { get; set; }
        public string AudioUrl { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public string CustomerContact { get; set; }
        public bool IsAdded { get; set; }
        public string BookingDate { get; set; }
        public Customer Customer { get; set; }
        public Category Category { get; set; }
        public ControlCenter ControlCenter { get; set; }
        public List<JobDetail> JobDetail { get; set; }
        public List<Material> Material { get; set; }
        public List<JobWorker> JobWorker { get; set; }
        public List<Others> Others { get; set; }
        public List<string> Images { get; set; }
        public decimal Discount { get; set; }
        public int DiscountPercentage { get; set; }
        public int TotalWorker { get; set; }
        public string StatusEnum { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? MaterialAmount { get; set; }
        public string PaymentStatusEnum { get; set; }
        public string ProfileImageUrl { get; set; }
    }
    public class Material
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string MaterialName { get; set; }
        public decimal? Cost { get; set; }
        
    }

    public class Others
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string OthersName { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    }
    public class GetJobRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetJobRequest req)
        {
            var response = new GetJobResponse();
            response.ValidationErrors = new List<string>();
            response.JobDetail = new List<JobDetail>();
            response.Images = new List<string>();
            response.JobWorker = new List<JobWorker>();
            response.Material = new List<Material>();
            response.Others = new List<Others>(); 
            try
            {
                var Job = _dbContext.Job.Where(x => x.Id == req.Id).FirstOrDefault();
                response.Id = Job.Id;
                response.CustomerId = Job.CustomerId??0;
                if(Job.Category !=null)
                    response.CategoryName = Job.Category.Name;
                if(Job.Category != null)
                {
                    response.Type = Job.Category.Type;
                }
                if (Job.Discount.HasValue)
                {
                    response.Discount = Job.Discount.Value;
                }
                if(Job.Others != null)
                {
                    foreach (var other in Job.Others)
                    {
                        var Others = new Others();
                        Others.Id = other.Id;
                        Others.OthersName = other.OthersName;
                        Others.Cost = other.Cost;
                        response.Others.Add(Others);
                    }
                }
                response.CustomerEmail = Job.CustomerEmail;
                response.CustomerAddress = Job.CustomerAddress;
                response.CustomerContact = Job.CustomerContact;
                response.Title = Job.Title;
                response.Description = Job.Description;
                if (Job.BookingDate.HasValue)
                {
                    response.BookingDate = Job.BookingDate.Value.ToString("dd-MMM-yyyy hh:mm tt");
                }
                response.PaymentStatusEnum = ((PaymentStatus)Job.PaymentStatus).ToString();
                var customer = _dbContext.UserProfile.Where(x => x.Id == Job.CustomerId).FirstOrDefault();
                if(customer != null)
                {
                    var Customer = new Customer();
                    Customer.Id = customer.Id;
                    Customer.FullName = customer.FullName;
                    response.CustomerName = customer.FullName;
                    response.ProfileImageUrl = customer.ProfileImageUrl;
                    response.Customer = Customer;

                }
                response.JobAddress = Job.JobAddress;
                response.PaymentStatus = Job.PaymentStatus;
                if(Job.JobDetail != null)
                {
                    foreach(var JD in Job.JobDetail)
                    {
                        var JobDetail = new JobDetail();
                        JobDetail.JobId = JD.JobId;
                        JobDetail.SubCategoryId = JD.SubCategoryId;
                        JobDetail.SubCategoryName = JD.SubCategoryName;
                        JobDetail.Amount = JD.Amount??0;
                        JobDetail.Discount = JD.Discount ?? 0;
                        response.JobDetail.Add(JobDetail);
                    }
                }
                if (Job.MaterialCost != null)
                {
                    foreach (var JD in Job.MaterialCost)
                    {
                        var Material = new Material();
                        Material.Id = JD.Id;
                        Material.JobId = JD.JobId;
                        Material.MaterialName = JD.MaterialName;
                        Material.Cost = JD.Cost.Value;
                        response.Material.Add(Material);
                    }
                    response.MaterialAmount = _dbContext.MaterialCost.Where(x=>x.JobId == Job.Id).Sum(s => s.Cost);
                }
                response.TotalAmount = Job.JobDetail.Sum(s => s.Amount);
                if (Job.JobWorker != null)
                {
                    foreach (var JW in Job.JobWorker)
                    {
                        var JobWorker = new JobWorker();
                        JobWorker.JobId = JW.JobId;
                        JobWorker.WorkerId = JW.WorkerId;
                        JobWorker.WorkerName = JW.WorkerName;
                        var Worker = _dbContext.UserProfile.Where(x => x.Id == JW.WorkerId).FirstOrDefault();
                        JobWorker.CNIC = Worker.CNIC;
                        JobWorker.Contact = Worker.Contact;
                        response.JobWorker.Add(JobWorker);
                    }
                }
                response.TotalWorker = Job.JobWorker.Count();
                var category = _dbContext.Category.Where(x => x.Id == Job.CategoryId).FirstOrDefault();
                if(category != null)
                {
                    var Category = new Category();
                    Category.Id = category.Id;
                    Category.Name = category.Name;
                    response.Category = Category;
                }
                
                response.StatusEnum = ((JobStatus)Job.Status.Value).ToString();
                var ControlCenter = new ControlCenter();
                ControlCenter.Id = Job.ControlCenterId??0;
                ControlCenter.Name = Job.ControlCenterName;
                response.ControlCenter = ControlCenter ;
                response.JobAddress = Job.JobAddress;
                response.Status = Job.Status??0;
                response.ImageUrl = Job.ImageUrl;
                if(Job.ImageUrl != null)
                {
                    response.Images.Add(Job.ImageUrl);
                }
                if (Job.ImageUrl2 != null)
                {
                    response.Images.Add(Job.ImageUrl2);
                }
                if (Job.ImageUrl3 != null)
                {
                    response.Images.Add(Job.ImageUrl3);
                }
                response.AudioUrl = Job.AudioUrl;
                response.VideoUrl = Job.VideoUrl;
                response.IsAdded = Job.IsAdded;
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

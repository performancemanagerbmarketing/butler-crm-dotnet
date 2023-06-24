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
    public class ApplicationJobResponse : Response { }
    public class ApplicationJobRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CustomerContact { get; set; }
        public string JobAddress { get; set; }
        public string CustomerName { get; set; }
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
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
        public string VideoUrl { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public bool IsAdded { get; set; }
        public Category Category { get; set; }
        public ControlCenter ControlCenter { get; set; }
        public Customer Customer { get; set; }
        public string BookingDate { get; set; }
        public List<JobDetail> SubCategories { get; set; }
        public decimal TotalAmount { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(ApplicationJobRequest req)
        {
            var response = new ApplicationJobResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Job = new Butler.Model.EntityModel.Job();
                Job.Title = req.Title;
                Job.Description = req.Description;
                if (req.Customer != null)
                {
                    var Customer = _dbContext.Customer.Where(x => x.Id == req.Customer.Id).FirstOrDefault();
                    if (Customer != null)
                    {
                        Job.CustomerId = Customer.Id;
                        Job.CustomerName = Customer.FullName;
                        Job.CustomerEmail = Customer.Email;
                        Job.CustomerContact = Customer.Contact;
                        Job.CustomerAddress = Customer.Address;
                    }

                }
                else if (req.CustomerContact != null)
                {
                    var Customer = _dbContext.UserProfile.Where(x => x.Contact == req.CustomerContact && x.UserType == (int)UserType.Customer).FirstOrDefault();
                    if (Customer != null)
                    {
                        Job.CustomerId = Customer.Id;
                        Job.CustomerName = Customer.FullName;
                        Job.CustomerEmail = Customer.Email;
                        Job.CustomerContact = Customer.Contact;
                        Job.CustomerAddress = Customer.Address;
                    }
                }
                Job.Status = (int)JobStatus.Pending;
                if (req.Category != null)
                {
                    Job.CategoryId = req.Category.Id;
                    Job.CategoryName = req.Category.Name;
                }
                if (req.Category == null)
                {
                    Job.CategoryId = null;
                    //Job.CategoryName = req.Category.Name;
                }
                else
                {
                    Job.CategoryId = req.CategoryId;
                    Job.CategoryName = req.CategoryName;
                }

                if (req.ControlCenter != null)
                {
                    Job.ControlCenterId = req.ControlCenter.Id;
                    Job.ControlCenterName = req.ControlCenter.Name;
                }
                if (req.SubCategories != null)
                {
                    foreach (var subCategory in req.SubCategories)
                    {
                        var JobDetail = new Butler.Model.EntityModel.JobDetail();
                        JobDetail.SubCategoryId = subCategory.Id;
                        JobDetail.SubCategoryName = subCategory.Name;
                        JobDetail.Amount = subCategory.Amount;
                        Job.JobDetail.Add(JobDetail);
                    }
                }
                if(req.BookingDate == null)
                {
                    Job.BookingDate = DateTime.Now;
                }
                else
                {
                    var BookingDate = DateTime.Parse(req.BookingDate);
                    Job.BookingDate = BookingDate;
                }
                Job.JobAddress = req.JobAddress;
                Job.ImageUrl = req.ImageUrl;
                Job.ImageUrl2 = req.ImageUrl2;
                Job.ImageUrl3 = req.ImageUrl3;
                Job.AudioUrl = req.AudioUrl;
                Job.VideoUrl = req.VideoUrl;
                Job.TotalAmount = req.TotalAmount;
                Job.CreatedAt = DateTime.Now;
                Job.Date = DateTime.Today;
                Job.IsAdded = true;
                _dbContext.Job.Add(Job);
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

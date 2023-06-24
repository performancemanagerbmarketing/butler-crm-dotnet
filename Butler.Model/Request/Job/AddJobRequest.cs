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
    public class AddJobResponse : Response { 
    public int Id { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ControlCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Contact { get; set; }

    }
    public class AddJobRequest
    {
        public string UserId { get; set; }
        public bool IsController { get; set; }
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
        public int SubCategoryId { get; set; }
       
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddJobRequest req)
        {
            var response = new AddJobResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Agent = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).FirstOrDefault();
                var Job = new Butler.Model.EntityModel.Job();
                Job.Title = req.Title;
                Job.Description = req.Description;
                if(req.Customer != null)
                {
                    var Customer = _dbContext.UserProfile.Where(x => x.Id == req.Customer.Id).FirstOrDefault();
                    if(Customer != null)
                    {
                        Job.CustomerId = Customer.Id;
                        Job.CustomerName = Customer.FullName;
                        Job.CustomerEmail = Customer.Email;
                        Job.CustomerContact = Customer.Contact;
                        Job.CustomerAddress = Customer.Address;
                    }
                    
                }
                else if ( req.CustomerContact != null)
                {
                    var Customer = _dbContext.UserProfile.Where(x => x.Contact == req.CustomerContact && x.UserType == (int)UserType.Customer ).FirstOrDefault();
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
                if(req.Category != null)
                {
                    Job.CategoryId = req.Category.Id;
                    Job.CategoryName = req.Category.Name;
                }
                else
                {
                    Job.CategoryId = req.CategoryId;
                    Job.CategoryName = req.CategoryName;
                }

                if (req.ControlCenter != null && !req.IsController)
                {
                    Job.ControlCenterId = req.ControlCenter.Id;
                    Job.ControlCenterName = req.ControlCenter.Name;
                }
                var ControllerProfile = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).FirstOrDefault();
                if (req.IsController && ControllerProfile != null)
                {
                    Job.ControlCenterId = ControllerProfile.ControllerCenterId;
                    var controllcenter = _dbContext.ControlCenter.Where(x => x.Id == ControllerProfile.ControllerCenterId).FirstOrDefault();
                    if (controllcenter != null)
                        Job.ControlCenterName = controllcenter.Name;
                }
                if (req.SubCategoryId != 0 && req.SubCategoryId != null)
                {
                    var SubCat = _dbContext.SubCategory.Where(x => x.Id == req.SubCategoryId).FirstOrDefault();
                    var JobDetail = new Butler.Model.EntityModel.JobDetail();
                    JobDetail.SubCategoryId = SubCat.Id;
                    JobDetail.SubCategoryName = SubCat.Name;
                    JobDetail.Amount = SubCat.Cost;
                    Job.JobDetail.Add(JobDetail);
                }
                var BookingDate = DateTime.Parse(req.BookingDate);
                Job.BookingDate = BookingDate;
                Job.JobAddress = req.JobAddress;
                Job.ImageUrl = req.ImageUrl;
                Job.AudioUrl = req.AudioUrl;
                Job.VideoUrl = req.VideoUrl;
                Job.CreatedAt = DateTime.Now;
                Job.CreatedBy = Agent.FullName;
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

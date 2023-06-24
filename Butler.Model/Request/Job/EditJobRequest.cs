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
    public class EditJobResponse : Response { }
   
    public class EditJobRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
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
        public DateTime BookingDate { get; set; }
        public string UserId { get; set; }
        public int PaymentStatus { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EditJobRequest req)
        {
            var response = new EditJobResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Agent = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).FirstOrDefault();
                var Job = _dbContext.Job.Where(x=> x.Id == req.Id).FirstOrDefault();
                Job.Title = req.Title;
                Job.PaymentStatus = req.PaymentStatus;
                Job.Description = req.Description;
                if (req.Customer != null)
                {
                    var Customer = _dbContext.Customer.Where(x => x.Id == req.Customer.Id).FirstOrDefault();
                    if(Customer != null)
                    {
                        Job.CustomerId = Customer.Id;
                        Job.CustomerName = Customer.FullName;
                        Job.CustomerEmail = Customer.Email;
                        Job.CustomerContact = Customer.Contact;
                        Job.CustomerAddress = Customer.Address;
                    }
                    else
                    {
                        var customer = _dbContext.UserProfile.Where(x => x.Id == req.Customer.Id).FirstOrDefault();
                        Job.CustomerId = customer.Id;
                        Job.CustomerName = customer.FullName;
                        Job.CustomerEmail = customer.Email;
                        Job.CustomerContact = customer.Contact;
                        Job.CustomerAddress = customer.Address;
                    }
                    
                }
                else if (req.CustomerId != 0 && req.CustomerId != null)
                {
                    var Customer = _dbContext.UserProfile.Where(x => x.Id == req.CustomerId && x.UserType == (int)UserType.Customer).FirstOrDefault();
                    if (Customer != null)
                    {
                        Job.CustomerId = req.CustomerId;
                        Job.CustomerName = Customer.FullName;
                        Job.CustomerEmail = Customer.Email;
                        Job.CustomerContact = Customer.Contact;
                        Job.CustomerAddress = Customer.Address;
                    }
                }
                Job.Status = req.Status;
                if (req.Category != null)
                {
                    Job.CategoryId = req.Category.Id;
                    Job.CategoryName = req.Category.Name;
                }
                else
                {
                    Job.CategoryId = req.CategoryId;
                    Job.CategoryName = req.CategoryName;
                }

                if (req.ControlCenter != null && req.ControlCenter.Id != 0)
                {
                    Job.ControlCenterId = req.ControlCenter.Id;
                    Job.ControlCenterName = req.ControlCenter.Name;
                }
                //Job.BookingDate = req.BookingDate;
                Job.ImageUrl = req.ImageUrl;
                Job.AudioUrl = req.AudioUrl;
                Job.VideoUrl = req.VideoUrl;
                Job.UpdatedAt = DateTime.Now;
                Job.UpdateBy = Agent.FullName;
                Job.IsAdded = true;
                _dbContext.SaveChanges();
                response.Success = true;
                if (Job.ControlCenterId != req.ControlCenterId)
                {
                    var ControllerAdmin = _dbContext.UserProfile.Where(x => x.ControllerCenterId == Job.ControlCenterId && x.UserType == (int)UserType.Controller).ToList();
                    foreach (var admin in ControllerAdmin)
                    {
                        var AdminNotification = new Butler.Model.EntityModel.Notification();
                        AdminNotification.AdminId = admin.Id;
                        AdminNotification.IsRead = false;
                        AdminNotification.Content = "New Job Has been Posted #" + req.Id;
                        AdminNotification.Title = "Job Assigned";
                        AdminNotification.CreatedBy = "System";
                        AdminNotification.Link = "/Job/Details?Id=" + req.Id;
                        AdminNotification.CreatedAt = DateTime.Now;
                        AdminNotification.Date = DateTime.Today;
                        _dbContext.Notification.Add(AdminNotification);
                        _dbContext.SaveChanges();
                    }
                }
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

using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.CustomerAdmin
{
    public class AddCustomerResponse : Response { }
    public class AddCustomerRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CNIC { get; set; }
        public string Contact { get; set; }
        public string ProfileImageUrl { get; set; }
        public string VerificationImageUrl { get; set; }
        public string CNICFromtImageUrl { get; set; }
        public string CNICBackImageUrl { get; set; }
        public bool VerificationStatus { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
       
        public DateTime Date { get; set; }
        public bool IsAdded { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddCustomerRequest req)
        {
            var response = new AddCustomerResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Customer = new Butler.Model.EntityModel.Customer();
                Customer.FullName = req.FullName;
                Customer.Email = req.Email;
                Customer.CNIC = req.CNIC;
                Customer.CNICBackImageUrl = req.CNICBackImageUrl;
                Customer.CNICFromtImageUrl = req.CNICFromtImageUrl;
                Customer.Address = req.Address;
                Customer.IsAdded = req.IsAdded;
                Customer.Contact = req.Contact;
                Customer.VerificationImageUrl = req.VerificationImageUrl;
                Customer.ProfileImageUrl = req.ProfileImageUrl;
                Customer.VerificationStatus = req.VerificationStatus;
                Customer.Status = req.Status;
                Customer.CreatedAt = DateTime.Now;
                Customer.Date = DateTime.Today;
                Customer.IsAdded = true;
                _dbContext.Customer.Add(Customer);
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

using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.CustomerAdmin
{
    public class EditCustomerResponse : Response { }
    public class EditCustomerRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string OfficeAddress { get; set; }
        public string OtherAddress { get; set; }
        public string CNIC { get; set; }
        public string Contact { get; set; }
        public string ProfileImageUrl { get; set; }
        public string VerificationImageUrl { get; set; }
        public string CNICFrontImageUrl { get; set; }
        public string CNICBackImageUrl { get; set; }
        public bool VerificationStatus { get; set; }
        public bool Status { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime Date { get; set; }
        public bool IsAdded { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EditCustomerRequest req)
        {
            var response = new EditCustomerResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Customer = _dbContext.UserProfile.Where(x => x.Id == req.Id && x.UserType == (int)UserType.Customer).FirstOrDefault();
                Customer.FullName = req.FullName;
                Customer.Email = req.Email;
                Customer.CNIC = req.CNIC;
                Customer.CNICBackImageUrl = req.CNICBackImageUrl;
                Customer.CNICFrontImageUrl = req.CNICFrontImageUrl;
                Customer.Address = req.Address;
                Customer.OfficeAddress = req.OfficeAddress;
                Customer.OtherAddress = req.OtherAddress;
                Customer.Address = req.Address;
                Customer.IsAdded = req.IsAdded;
                Customer.Contact = req.Contact;
                Customer.VerficationImageUrl = req.VerificationImageUrl;
                Customer.ProfileImageUrl = req.ProfileImageUrl;
                Customer.VerificationStatus = req.VerificationStatus;
                Customer.Date = DateTime.Today;
                //Customer.IsAdded = true;
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

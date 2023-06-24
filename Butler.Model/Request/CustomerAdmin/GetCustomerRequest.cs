using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.CustomerAdmin
{
    public class GetCustomerResponse : Response
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
        public string CNICFromtImageUrl { get; set; }
        public string CNICBackImageUrl { get; set; }
        public bool VerificationStatus { get; set; }
        public bool Status { get; set; }
    }
    public class GetCustomerRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetCustomerRequest req)
        {
            var response = new GetCustomerResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Customer = _dbContext.UserProfile.Where(x => x.Id == req.Id).FirstOrDefault();
                response.Id = Customer.Id;
                response.FullName = Customer.FullName;
                response.Address = Customer.Address;
                response.OfficeAddress = Customer.OfficeAddress;
                response.OtherAddress = Customer.OtherAddress;
                response.Contact = Customer.Contact;
                response.CNIC = Customer.CNIC;
                response.CNICBackImageUrl = Customer.CNICBackImageUrl;
                response.CNICFromtImageUrl = Customer.CNICFrontImageUrl;
                response.ProfileImageUrl = Customer.ProfileImageUrl;
                response.Email = Customer.Email;
                response.VerificationImageUrl = Customer.VerficationImageUrl;
                response.VerificationStatus = Customer.VerificationStatus;
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

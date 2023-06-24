using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Customer
{
    public class GetCustomerResponse : Response
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public int UserType { get; set; }
        public bool ApprovalStatus { get; set; }
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
                response.Contact = Customer.Contact;
                response.UserName = Customer.UserName;
                response.ProfileImageUrl = Customer.ProfileImageUrl;
                response.ApprovalStatus = Customer.ApprovalStatus;
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

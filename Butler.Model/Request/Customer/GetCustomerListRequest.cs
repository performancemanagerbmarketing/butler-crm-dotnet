using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Customer
{
    public class GetCustomerListResponse : Response
    {
        public List<Customer> Data { get; set; }
    }
    public class Customer
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
    public class GetCustomerListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetCustomerListRequest req)
        {
            var response = new GetCustomerListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<Customer>();
            try
            {
                var Customers = _dbContext.UserProfile.Where(x => x.UserType == (int)UserType.Customer).ToList();
                foreach (var customer in Customers)
                {
                    var Customer = new Customer();
                    Customer.Id = customer.Id;
                    Customer.FullName = customer.FullName;
                    Customer.ProfileImageUrl = customer.ProfileImageUrl;
                    Customer.Address = customer.Address;
                    Customer.ApprovalStatus = customer.ApprovalStatus;
                    Customer.Contact = customer.Contact;
                    Customer.UserName = customer.UserName;
                    response.Data.Add(Customer);
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

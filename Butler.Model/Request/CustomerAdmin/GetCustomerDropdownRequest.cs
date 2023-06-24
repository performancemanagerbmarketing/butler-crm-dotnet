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
    public class GetCustomerDropdownResponse : Response
    {
        public List<CustomerDropdown> Data { get; set; }
    }
    public class CustomerDropdown
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Contact { get; set; }
    }
    public class GetCustomerDropdownRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetCustomerDropdownRequest req)
        {
            var response = new GetCustomerDropdownResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<CustomerDropdown>();
            try
            {
                var Customers = _dbContext.UserProfile.Where(x=>x.UserType == (int)UserType.Customer).OrderBy(o => o.FullName).ToList();
                foreach (var Customer in Customers)
                {
                    var row = new CustomerDropdown();
                    row.Id = Customer.Id;
                    row.FullName = Customer.FullName;
                    row.Contact = Customer.Contact;
                    response.Data.Add(row);
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

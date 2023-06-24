using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Customer
{
    public class EditCustomerResponse : Response { }
    public class EditCustomerRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public int UserType { get; set; }
        public bool ApprovalStatus { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EditCustomerRequest req)
        {
            var response = new EditCustomerResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Customer = _dbContext.UserProfile.Where(x => x.Id == req.Id).FirstOrDefault();
                Customer.FullName = req.FullName;
                Customer.UserName = req.UserName;
                Customer.Address = req.Address;
                Customer.ApprovalStatus = req.ApprovalStatus;
                Customer.ProfileImageUrl = req.ProfileImageUrl;
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

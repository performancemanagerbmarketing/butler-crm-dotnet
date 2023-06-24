using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Admin
{
    public class GetAdminResponse : Response
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
        public string Email { get; set; }
    }
    public class GetAdminRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetAdminRequest req)
        {
            var response = new GetAdminResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Admin = _dbContext.UserProfile.Where(x => x.Id == req.Id).FirstOrDefault();
                response.Id = Admin.Id;
                response.FirstName = Admin.FirstName;
                response.LastName = Admin.LastName;
                response.Address = Admin.Address;
                response.Contact = Admin.Contact;
                response.UserName = Admin.UserName;
                response.ProfileImageUrl = Admin.ProfileImageUrl;
                response.ApprovalStatus = Admin.ApprovalStatus;
                response.Email = Admin.Email;
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

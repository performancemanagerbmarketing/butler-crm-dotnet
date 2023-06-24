using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Admin
{
    public class GetAdminListResponse : Response
    {
        public List<Admin> Data { get; set; }
    }
    public class Admin
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
    public class GetAdminListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetAdminListRequest req)
        {
            var response = new GetAdminListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<Admin>();
            try
            {
                var Admins = _dbContext.UserProfile.Where(x=>x.UserType == (int)UserType.Admin).ToList();
                foreach (var admin in Admins)
                {
                    var Admin = new Admin();
                    Admin.Id = admin.Id;
                    Admin.FullName = admin.FirstName + " " + admin.LastName;
                    Admin.ProfileImageUrl = admin.ProfileImageUrl;
                    Admin.Address = admin.Address;
                    Admin.ApprovalStatus = admin.ApprovalStatus;
                    Admin.Contact = admin.Contact;
                    Admin.UserName = admin.UserName;
                    Admin.Email = admin.Email;
                    response.Data.Add(Admin);
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

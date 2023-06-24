using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Admin
{
    public class EditAdminResponse : Response { }
    public class EditAdminRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public int UserType { get; set; }
        public bool ApprovalStatus { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EditAdminRequest req)
        {
            var response = new EditAdminResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Admin = _dbContext.UserProfile.Where(x => x.Id == req.Id).FirstOrDefault();
                Admin.FirstName = req.FirstName;
                Admin.LastName = req.LastName;
                Admin.UserName = req.UserName;
                Admin.Contact = req.Contact;
                Admin.Address = req.Address;
                Admin.ApprovalStatus = req.ApprovalStatus;
                Admin.ProfileImageUrl = req.ProfileImageUrl;
                Admin.Date = DateTime.Today;
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


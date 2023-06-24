using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Admin;
using ButlerDotCom.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ButlerDotCom.Controllers.Api
{
    public class AdminApiController : ApiController
    {
        private ApplicationUserManager _userManager;
        public AdminApiController()
        {
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        }

        private ButlerEntities _dbContext = new ButlerEntities();
        [System.Web.Http.Authorize(Roles = "SuperAdmin")]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<object> RegisterAdmin(RegisterAdminViewModel model)
        {
            var response = new RegisterUserResponse();
            var RolesToBeAdded = new List<string>();
            if (ModelState.IsValid && model.ConfirmPassword == model.Password)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email }; //We can put username field instead of email
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var CurrentUserName = User.Identity.Name;
                    var Admin = new Butler.Model.EntityModel.UserProfile();
                    Admin.FirstName = model.FirstName;
                    Admin.LastName = model.LastName;
                    Admin.UserId = user.Id;
                    Admin.FullName = Admin.FirstName + " " + Admin.LastName;
                    Admin.ProfileImageUrl = model.ProfileImageUrl;
                    Admin.Email = model.Email;
                    Admin.Contact = model.Contact;
                    Admin.Address = model.Address;
                    Admin.ApprovalStatus = model.ApprovalStatus;
                    Admin.Date = DateTime.Today;
                    Admin.UserName = model.UserName;
                    Admin.UserType = (int)UserType.Admin;
                    var AdminResult = _dbContext.UserProfile.Add(Admin);
                    var RoleResult = await _userManager.AddToRoleAsync(user.Id, Roles.Admin);
                    if (RoleResult.Succeeded)
                    {
                        response.IsRoleAdded = true;
                    }
                    else
                    {
                        response.IsRoleAdded = false;
                    }
                    _dbContext.SaveChanges();
                    response.Success = true;
                    return response;
                }
                if (model.ConfirmPassword != model.Password)
                {
                    response.ValidationErrors.ToList().Add("Password and confirm password does not match");
                }
                response.Success = false;
                response.ValidationErrors = (result.Errors);
            }

            // If we got this far, something failed, redisplay form
            return response;
        }

        [System.Web.Http.HttpGet]
        public object GetListing([FromUri] GetAdminListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [System.Web.Http.Authorize(Roles = "SuperAdmin")]
        [System.Web.Http.HttpPost]
        public object EditAdmin([FromBody] EditAdminRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [System.Web.Http.HttpGet]
        public object GetAdmin([FromUri] GetAdminRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}
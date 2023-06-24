using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Controller;
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
    public class ControllerApiController : ApiController
    {
        private ApplicationUserManager _userManager;
        public ControllerApiController()
        {
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        }

        private ButlerEntities _dbContext = new ButlerEntities();
        [System.Web.Http.Authorize(Roles = "Admin, SuperAdmin")]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<object> RegisterController(RegisterControllerViewModel model)
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
                    var Controller = new Butler.Model.EntityModel.UserProfile();
                    Controller.FirstName = model.FirstName;
                    Controller.LastName = model.LastName;
                    Controller.CNIC = model.CNIC;
                    Controller.UserId = user.Id;
                    Controller.FullName = Controller.FirstName + " " + Controller.LastName;
                    Controller.ProfileImageUrl = model.ProfileImageUrl;
                    Controller.Email = model.Email;
                    Controller.Contact = model.Contact;
                    Controller.Address = model.Address;
                    Controller.ApprovalStatus = model.ApprovalStatus;
                    Controller.Date = DateTime.Today;
                    Controller.UserName = model.UserName;
                    Controller.ReferenceId = model.ReferenceId;
                    Controller.IsActive = model.IsActive;
                    Controller.CNICBackImageUrl = model.CNICBackImageUrl;
                    Controller.CNICFrontImageUrl = model.CNICFrontImageUrl;
                    Controller.UserType = (int)UserType.Controller;
                    Controller.ControllerCenterId = model.ControlCenterId;
                    var ControllerResult = _dbContext.UserProfile.Add(Controller);
                    var RoleResult = await _userManager.AddToRoleAsync(user.Id, Roles.Controller);
                    if (RoleResult.Succeeded)
                    {
                        response.IsRoleAdded = true;
                    }
                    else
                    {
                        response.IsRoleAdded = false;
                    }
                    _dbContext.SaveChanges();
                    if (model.Category != null)
                    {
                        foreach (var Category in model.Category)
                        {
                            var ControllerCategory = new Butler.Model.EntityModel.ControllerCategory();
                            ControllerCategory.ControllerId = Controller.Id;
                            ControllerCategory.ControllerName = Controller.FullName;
                            ControllerCategory.CategoryId = Category.Id;
                            ControllerCategory.CategoryName = Category.Name;
                            _dbContext.ControllerCategory.Add(ControllerCategory);
                            _dbContext.SaveChanges();
                        }
                    }
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
        public object GetListing([FromUri] GetControllerListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [System.Web.Http.Authorize(Roles = "Admin,SuperAdmin")]
        [System.Web.Http.HttpPost]
        public object EditController([FromBody] EditControllerRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [System.Web.Http.HttpGet]
        public object GetController([FromUri] GetControllerRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

    }
}
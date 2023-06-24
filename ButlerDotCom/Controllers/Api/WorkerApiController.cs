using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Worker;
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
    public class WorkerApiController : ApiController
    {
        [System.Web.Http.HttpGet]
        public object GetListing([FromUri] GetWorkerListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        private ApplicationUserManager _userManager;
        public WorkerApiController()
        {
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        }

        private ButlerEntities _dbContext = new ButlerEntities();
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Authorize(Roles = "SuperAdmin,Admin, Controller")]
        public async Task<object> RegisterWorker(RegisterWorkerViewModel model)
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
                    var Worker = new Butler.Model.EntityModel.UserProfile();
                    Worker.FirstName = model.FirstName;
                    Worker.LastName = model.LastName;
                    Worker.CNIC = model.CNIC;
                    Worker.UserId = user.Id;
                    Worker.FullName = Worker.FirstName + " " + Worker.LastName;
                    Worker.ProfileImageUrl = model.ProfileImageUrl;
                    Worker.Email = model.Email;
                    Worker.Contact = model.Contact;
                    Worker.Address = model.Address;
                    if (model.IsActive == true)
                    {
                        Worker.IsActive = model.IsActive;
                        Worker.ApprovalStatus = true;
                    }
                    Worker.Date = DateTime.Today;
                    Worker.UserName = model.UserName;
                    Worker.CNICBackImageUrl = model.CNICBackImageUrl;
                    Worker.CNICFrontImageUrl = model.CNICFrontImageUrl;
                    Worker.UserType = (int)UserType.Worker;
                    Worker.ControllerCenterId = model.ControlCenterId;
                    var Workers = _dbContext.UserProfile.Add(Worker);
                    var RoleResult = await _userManager.AddToRoleAsync(user.Id, Roles.Worker);
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
                response.ValidationErrors = (result.Errors);
                response.Success = false;
            }
            return response;
        }

        [System.Web.Http.Authorize(Roles = "SuperAdmin,Admin, Controller")]
        [System.Web.Http.HttpPost]
        public object EditWorker([FromBody] EditWorkerRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [System.Web.Http.HttpGet]
        public object GetWorker([FromUri] GetWorkerRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [System.Web.Http.HttpGet]
        public object GetWorkerDropdown([FromUri] GetWorkerDropdownRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

    }
}
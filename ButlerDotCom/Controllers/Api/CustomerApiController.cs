using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.CustomerAdmin;
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
    public class CustomerApiController : ApiController
    {
        private ApplicationUserManager _userManager;
        public CustomerApiController()
        {
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        }

        private ButlerEntities _dbContext = new ButlerEntities();
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<object> RegisterCustomer(RegisterCustomerViewModel model)
        {
            var response = new RegisterUserResponse();
            var RolesToBeAdded = new List<string>();
            var email = string.Empty;
            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(1000);
            if (model.Email == null)
            {
                email = model.FullName + randomInt + "@butlers.com";
            }
            else
            {
                email = model.Email;
            }
            if (ModelState.IsValid && model.ConfirmPassword == model.Password)
            {
                var user = new ApplicationUser { UserName = model.Contact, Email = email }; //We can put username field instead of email
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var CurrentUserName = User.Identity.Name;
                    var Customer = new Butler.Model.EntityModel.UserProfile();
                    Customer.UserId = user.Id;
                    Customer.FullName = model.FullName;
                    Customer.ProfileImageUrl = model.ProfileImageUrl;
                    Customer.Contact = model.Contact;
                    Customer.Address = model.Address;
                    Customer.ApprovalStatus = true;
                    Customer.Date = DateTime.Today;
                    Customer.UserName = model.UserName;
                    Customer.UserType = (int)UserType.Customer;
                    var CustomerResult = _dbContext.UserProfile.Add(Customer);
                    var RoleResult = await _userManager.AddToRoleAsync(user.Id, Roles.Customer);
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

        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<object> AddNewCustomer(AddNewCustomerViewModel model)
        {
            var response = new RegisterUserResponse();
            var RolesToBeAdded = new List<string>();
            model.Password = "Pass123";
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int Num = random.Next();
                var user = new ApplicationUser { UserName = model.Contact, Email = model.FullName+Num+"@butler.pk" }; //We can put username field instead of email
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var CurrentUserName = User.Identity.Name;
                    var Customer = new Butler.Model.EntityModel.UserProfile();
                    Customer.UserId = user.Id;
                    Customer.FullName = model.FullName;
                    Customer.Contact = model.Contact;
                    Customer.UserName = model.UserName;
                    Customer.UserType = (int)UserType.Customer;
                    Customer.ApprovalStatus = true;
                    Customer.IsActive = true;
                    var CustomerResult = _dbContext.UserProfile.Add(Customer);
                    var RoleResult = await _userManager.AddToRoleAsync(user.Id, Roles.Customer);
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
                    response.Id = Customer.Id;
                    response.FullName = Customer.FullName;
                    return response;
                }
                response.Success = false;
                response.ValidationErrors = (result.Errors);
            }

            // If we got this far, something failed, redisplay form
            return response;
        }
        [System.Web.Http.HttpGet]
        public object GetListing([FromUri] GetCustomerListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [System.Web.Http.HttpPost]
        public object EditCustomer([FromBody] EditCustomerRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [System.Web.Http.HttpPost]
        public object AddCustomer([FromBody] AddCustomerRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [System.Web.Http.HttpGet]
        public object GetCustomer([FromUri] GetCustomerRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [System.Web.Http.HttpGet]
        public object CustomerDropdown([FromUri] GetCustomerDropdownRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}
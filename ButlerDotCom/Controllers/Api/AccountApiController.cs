using Butler.Model.EntityModel;
using Butler.Model.Enum;
using ButlerDotCom.Models;
using Microsoft.AspNet.Identity;
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
    public class AccountApiController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ButlerEntities _dbContext = new ButlerEntities();

        public AccountApiController()
        {
            _userManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            _signInManager = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]

        public async Task<Object> LoginWorker([FromBody] WorkerLoginViewModel model)
        {
            var response = new LoginResponse();
            response.ValidationErrors = new List<string>();
            if (!ModelState.IsValid)
            {
                foreach (var modelStateVal in ModelState.Values)
                {
                    foreach (var error in modelStateVal.Errors)
                        response.ValidationErrors.Add(error.ErrorMessage);
                }
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {

                case SignInStatus.Success:
                    {
                        var Worker = _dbContext.UserProfile.Where(x => x.Email == model.Email).FirstOrDefault();
                        response.Id = Worker.Id;
                        response.FullName = Worker.FullName;
                        response.Contact = Worker.Contact;
                        response.ProfileImageUrl = Worker.ProfileImageUrl;
                        response.Email = Worker.Email;
                        response.UserId = Worker.UserId;
                        response.Address = Worker.Address;
                        response.Success = true;
                        return response;
                    }
                default:
                    {
                        response.ValidationErrors.Add("Either the username or password is incorrect!");
                        return response;
                    }
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
      
        public async Task<Object> Login([FromBody] CustomerLoginViewModel model)
        {
            var response = new LoginResponse();
            response.ValidationErrors = new List<string>();
            if (!ModelState.IsValid)
            {
                foreach (var modelStateVal in ModelState.Values)
                {
                    foreach (var error in modelStateVal.Errors)
                        response.ValidationErrors.Add(error.ErrorMessage);
                }
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {

                case SignInStatus.Success:
                {
                        var Customer = _dbContext.UserProfile.Where(x => x.Contact == model.UserName).FirstOrDefault();
                        response.Id = Customer.Id;
                        response.FullName = Customer.FullName;
                        response.Contact = Customer.Contact;
                        response.ProfileImageUrl = Customer.ProfileImageUrl;
                        response.Email = Customer.Email;
                        response.Address = Customer.Address;
                        response.UserId = Customer.UserId;
                        response.OfficeAddress = Customer.OfficeAddress;
                        response.Success = true;
                        return response;
                }
                default:
                {
                        response.ValidationErrors.Add("Either the username or password is incorrect!");
                        return response;
                }
            }
        }

        [System.Web.Http.HttpPost]
        public async Task<object> RegisterCustomer(RegisterCustomerViewModel model)
        {
            var response = new RegisterUserResponse();
            response.ValidationErrors = new List<string>();
            var RolesToBeAdded = new List<string>();
            if (ModelState.IsValid)
            {
                Random randomGenerator = new Random();
                int randomInt = randomGenerator.Next(1000);
                var email = string.Empty;
                if(model.Email == null)
                {
                    email = model.FullName + randomInt + "@butlers.com";
                }
                else
                {
                    email = model.Email;
                }
                var user = new ApplicationUser { PhoneNumber = model.Contact, PhoneNumberConfirmed = true, UserName = model.Contact, Email = email }; //We can put username field instead of email
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
                    Customer.IsActive = true;
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
                    if (response.Success == true)
                    {
                        response.Id = Customer.Id;
                        response.FullName = Customer.FullName;
                        response.Contact = Customer.Contact;
                        response.Address = Customer.Address;
                        response.ProfileImageUrl = Customer.ProfileImageUrl;
                        response.UserId = Customer.UserId;
                        response.OfficeAddress = Customer.OfficeAddress;
                    }

                    return response;
                }
                response.Success = false;
                response.ValidationErrors = (result.Errors);
            }
           
            return response;
        }

        [System.Web.Http.HttpPost]
        public async Task<Object> ChangePassword([FromBody] ChangesPasswordViewModel model)
        {
            var response = new LoginResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var User = _dbContext.AspNetUsers.Where(x => x.UserName == model.Number).FirstOrDefault();
                var RemovePassword = _userManager.RemovePassword(User.Id);
                var NewPassword = await _userManager.AddPasswordAsync(User.Id, model.Password);
                response.Success = true;
                if (ModelState.IsValid)
                {
                    var Customer = _dbContext.UserProfile.Where(x => x.Contact == model.Number).FirstOrDefault();
                    response.Id = Customer.Id;
                    response.FullName = Customer.FullName;
                    response.Contact = Customer.Contact;
                    response.ProfileImageUrl = Customer.ProfileImageUrl;
                    response.Email = Customer.Email;
                    response.Address = Customer.Address;
                    response.UserId = Customer.UserId;
                    response.OfficeAddress = Customer.OfficeAddress;
                }
            }
            catch(Exception e)
            {
                response.Success = false;
                response.ValidationErrors.Add(e.Message.ToString());
            }
            return response;
        }
    }
}
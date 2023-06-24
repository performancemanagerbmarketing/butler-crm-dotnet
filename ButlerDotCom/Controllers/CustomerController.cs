using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Customer;
using ButlerDotCom.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Threading.Tasks;

namespace ButlerDotCom.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ButlerEntities _dbContext = new ButlerEntities();
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterCustomer(RegisterCustomerViewModel model)
        {
            var response = new RegisterUserResponse();
            var RolesToBeAdded = new List<string>();
            if (ModelState.IsValid && model.ConfirmPassword == model.Password)
            {
                Random randomGenerator = new Random();
                int randomInt = randomGenerator.Next(1000);
                var user = new ApplicationUser { PhoneNumber = model.Contact, PhoneNumberConfirmed = true, UserName = model.Contact, Email = model.FullName+randomInt+"@butlers.com" }; //We can put username field instead of email
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                    var CurrentUserName = User.Identity.Name;
                    var Customer = new Butler.Model.EntityModel.UserProfile();
                    Customer.UserId = user.Id;
                    Customer.FullName = model.FullName;
                    Customer.ProfileImageUrl = model.ProfileImageUrl;
                    Customer.UserName = model.FullName + randomInt;
                    Customer.Email = user.Email;
                    Customer.Contact = model.Contact;
                    Customer.Address = model.Address;
                    Customer.ApprovalStatus = model.ApprovalStatus;
                    Customer.Date = DateTime.Today;
                    Customer.UserName = model.UserName;
                    Customer.UserType = (int)UserType.Customer;
                    var CustomerResult = _dbContext.UserProfile.Add(Customer);
                    var RoleResult = await UserManager.AddToRoleAsync(user.Id, Roles.Customer);
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
                    return RedirectToAction("Index", "Home");
                }
                if (model.ConfirmPassword != model.Password)
                {
                    response.ValidationErrors.ToList().Add("Password and confirm password does not match");
                }
                response.Success = false;
                response.ValidationErrors = (result.Errors);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Home");
        }
    }
}
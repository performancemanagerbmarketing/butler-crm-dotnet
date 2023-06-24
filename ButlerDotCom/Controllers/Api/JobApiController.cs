using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Job;
using ButlerDotCom.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class JobApiController : ApiController
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        [HttpGet]
        public object GetListing([FromUri] GetJobListRequest req)
        {
            //if (User.IsInRole(Roles.Controller.ToString()))
            //{
            //    req.IsController = true;
            //}
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddJob([FromBody] AddJobRequest req)
        {
            if (User.IsInRole(Roles.Controller))
            {
                req.IsController = true;
            }
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddAppJob([FromBody] AddAppJobRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public async Task<object> ApplicationJob([FromBody] ApplicationJobRequest req)
        {
            if(req.Customer == null)
            {
                var Customer = _dbContext.UserProfile.Where(x => x.Contact == req.CustomerContact).SingleOrDefault();
                if(Customer == null)
                {
                    var RegisterCustomer = new CustomerApiController();
                    var RCVM = new RegisterCustomerViewModel();
                    RCVM.Contact = req.CustomerContact;
                    RCVM.FullName = req.CustomerName;
                    RCVM.Email = req.CustomerEmail;
                    RCVM.UserName = req.CustomerContact;
                    RCVM.Password = "Password123$";
                    RCVM.ConfirmPassword = "Password123$";
                    await RegisterCustomer.RegisterCustomer(RCVM);
                }
                
            }
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditJob([FromBody] EditJobRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetJob([FromUri] GetJobRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetInvoice([FromUri] GetInvoiceDetailRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetActiveBooking([FromUri] ActiveBookingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetPreviousBooking([FromUri] PreviousBookingRequest req)
        {
            var result = req.RunRequest(req);
            return result;

        }
        [HttpGet]
        public object GetBookingDetail([FromUri] GetBookingDetailRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddJobDetail([FromBody] AddJobDetailRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddOtherService([FromBody] AddOtherServiceRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object ApplyDiscount([FromBody] ApplyDiscountRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddJobWorker([FromBody] AddJobWorkerRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object UpdateStatus([FromBody] UpdateStatusRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AmountPaid([FromBody] UpdatePaymentStatusRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
    }
}
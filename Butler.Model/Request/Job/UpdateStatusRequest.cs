using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Job
{
    public class UpdateStatusResponse : Response 
    {
        public string Contact { get; set; }
        public string Title { get; set; }
    }
    public class UpdateStatusRequest
    {
        public string UserId { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(UpdateStatusRequest req)
        {
            var response = new UpdateStatusResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var User = _dbContext.UserProfile.Where(x => x.UserId == req.UserId && x.UserType == (int)UserType.Controller).FirstOrDefault();
                if(User != null)
                {
                    var Job = _dbContext.Job.Where(x => x.Id == req.JobId).FirstOrDefault();
                    Job.Status = req.Status;
                    if(Job.Status == (int)JobStatus.Complete)
                    {
                        Job.CompleteDateTime = DateTime.Now;
                        response.Title = "Thankyou for trusting us. Please give your valuable feedback on our page www.facebook.com/butlersbench  For further assistance, call 0340 130 77 77";
                    }
                    else
                    {
                        response.Title = "Hi," + Job.CustomerName + "! Thankyou for booking our service. We have confirmed your order. Our representative will get in touch with you shortly! For further assistance, call 0340 130 77 77";
                    }
                    _dbContext.SaveChanges();
                    response.Success = true;
                    if(Job.CustomerContact == null)
                    {
                        response.Contact = _dbContext.UserProfile.Where(x => x.Id == Job.CustomerId).FirstOrDefault().Contact;
                    }
                    response.Contact = Job.CustomerContact;
                    
                }
                else
                {
                    response.Success = false;
                }
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

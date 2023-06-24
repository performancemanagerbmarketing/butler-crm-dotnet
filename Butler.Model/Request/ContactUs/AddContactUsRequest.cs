using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.ContactUs
{
    public class AddContactUsResponse : Response { }
    public class AddContactUsRequest
    {
        public EntityModel.ContactUs ContactUs { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddContactUsRequest req)
        {
            var response = new AddContactUsResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var ContactUs = new EntityModel.ContactUs();
                ContactUs = req.ContactUs;
                ContactUs.Date = DateTime.Today;
                _dbContext.ContactUs.Add(ContactUs);
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

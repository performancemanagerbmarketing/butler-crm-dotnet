using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.ContactUs
{
    public class EditContactUsResponse : Response { }
    public class EditContactUsRequest
    {
        public EntityModel.ContactUs ContactUs { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EditContactUsRequest req)
        {
            var response = new EditContactUsResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                _dbContext.Entry(req.ContactUs).State = System.Data.Entity.EntityState.Modified;
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

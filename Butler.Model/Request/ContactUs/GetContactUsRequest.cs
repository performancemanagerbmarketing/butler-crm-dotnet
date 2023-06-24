using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.ContactUs
{
    public class GetContactUsResponse : Response
    {
        public EntityModel.ContactUs ContactUs { get; set; }
    }
    public class GetContactUsRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetContactUsRequest req)
        {
            var response = new GetContactUsResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                response.ContactUs = _dbContext.ContactUs.Find(req.Id); ;
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

using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.ContactUs
{
    public class GetContactUsListResponse : Response
    {
        public IEnumerable<EntityModel.ContactUs> Data { get; set; }
    }
    public class GetContactUsListRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetContactUsListRequest req)
        {
            var response = new GetContactUsListResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                response.Data = _dbContext.ContactUs.ToList();
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

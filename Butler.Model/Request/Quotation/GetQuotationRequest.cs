using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Quotation
{
    public class GetQuotationResponse : Response 
    {
        public EntityModel.Quotations Quotation { get; set; }
    }
    public class GetQuotationRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetQuotationRequest req)
        {
            var response = new GetQuotationResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                response.Quotation = _dbContext.Quotations.Find(req.Id); ;
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

using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Quotation
{
    public class GetQuotationListResponse : Response 
    {
        public IEnumerable<EntityModel.Quotations> Data { get; set; }
    }
    public class GetQuotationListRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetQuotationListRequest req)
        {
            var response = new GetQuotationListResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                response.Data = _dbContext.Quotations.ToList();
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

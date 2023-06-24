using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Quotation
{
    public class EditQuotationResponse : Response { }
    public class EditQuotationRequest
    {
        public EntityModel.Quotations Quotation { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EditQuotationRequest req)
        {
            var response = new EditQuotationResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Quotation = req.Quotation;
                _dbContext.Entry(Quotation).State = System.Data.Entity.EntityState.Modified;
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

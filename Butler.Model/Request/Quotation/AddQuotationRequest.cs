using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Quotation
{
    public class AddQuotationResponse : Response { }
    public class AddQuotationRequest
    {
        public EntityModel.Quotations Quotation { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddQuotationRequest req)
        {
            var response = new AddQuotationResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Quotation = new Quotations();
                Quotation = req.Quotation;
                Quotation.CreatedAt = DateTime.Now;
                Quotation.Date = DateTime.Today;
                _dbContext.Quotations.Add(Quotation);
                _dbContext.SaveChanges();
                response.Success = true;
            }
            catch(Exception e)
            {
                response.Success = false;
                response.ValidationErrors.Add(e.Message.ToString());
            }
            return response;
        }
    }
}

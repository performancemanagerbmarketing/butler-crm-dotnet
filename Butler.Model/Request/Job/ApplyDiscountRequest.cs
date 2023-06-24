using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Job
{
    public class ApplyDiscountResponse : Response { }
    public class ApplyDiscountRequest
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(ApplyDiscountRequest req)
        {
            var response = new ApplyDiscountResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Job = _dbContext.Job.Where(x => x.Id == req.Id).FirstOrDefault();
                if (Job != null)
                {
                    Job.Discount = req.Discount;
                    decimal percent = (Job.TotalAmount - Job.Discount) / Job.TotalAmount ??0;
                    Job.DiscountPercentage = (int)percent * 100;
                    Job.TotalAmount = Job.TotalAmount - Job.Discount;
                    _dbContext.SaveChanges();
                    response.Success = true;
                }
            }
            catch(Exception e)
            {
                response.ValidationErrors.Add(e.Message.ToString());
                response.Success = false;
            }
            return response;
        }
    }
}

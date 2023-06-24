using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.WorkerAppRequest.JobAssign
{
    public class PaymentCollectionResponse : Response 
    {
        public bool PaidStatus { get; set; }
    }
    public class PaymentCollectionRequest
    {
        public int JobId { get; set; }
        public decimal PaidAmount { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(PaymentCollectionRequest req)
        {
            var response = new PaymentCollectionResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Job = _dbContext.Job.Where(x => x.Id == req.JobId).SingleOrDefault();
                if(Job != null)
                {
                    if(req.PaidAmount == Job.TotalAmount)
                    {
                        Job.PaymentStatus = (int)PaymentStatus.Done;
                        response.PaidStatus = true;
                    }
                    else
                    {
                        Job.PaymentStatus = (int)PaymentStatus.Decline;
                    }
                    _dbContext.SaveChanges();

                }
                response.Success = true;
            }
            catch(Exception e)
            {
                response.Success = true;
                response.ValidationErrors.Add(e.Message.ToString());
            }
            return response;
        }
    }
}

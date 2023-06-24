using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Worker
{
    public class EditWorkerResponse : Response { }
    public class EditWorkerRequest
    {
        public EntityModel.UserProfile Worker { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EditWorkerRequest req)
        {
            var response = new EditWorkerResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var WorkerData = _dbContext.UserProfile.Find(Worker.Id);
                _dbContext.Entry(WorkerData).CurrentValues.SetValues(req.Worker);
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

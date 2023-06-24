using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.WorkerAppRequest.WorkerAttendance
{
    public class EndDayResponse : Response
    {
        public bool EndStatus { get; set; }
    }
    public class EndDayRequest
    {
        public int Id { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EndDayRequest req)
        {
            var response = new EndDayResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var WorkerAttendance = _dbContext.WorkerAttendance.Where(x => x.Id == req.Id).FirstOrDefault();
                WorkerAttendance.EndDate = DateTime.Today;
                WorkerAttendance.EndDateTime = DateTime.Now;
                WorkerAttendance.EndStatus = true;
                WorkerAttendance.UpdatedAt = DateTime.Now;
                _dbContext.SaveChanges();
                response.Success = true;
                response.EndStatus = true;
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

using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.WorkerAppRequest.WorkerAttendance
{
    public class StartDayResponse : Response
    {
        public int Id { get; set; }
        public bool StartDayStatus { get; set; }
    }
    public class StartDayRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(StartDayRequest req)
        {
            var response = new StartDayResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Attendance = _dbContext.WorkerAttendance.Where(x => x.WorkerId == req.Id && x.Date == DateTime.Now).FirstOrDefault();
                if (Attendance != null)
                {
                    response.Success = true;
                    return response;
                }
                var WorkerAttendance = new EntityModel.WorkerAttendance();
                TimeSpan LateTime = new TimeSpan(11, 45, 0);
                TimeSpan now = DateTime.Now.TimeOfDay;
                WorkerAttendance.WorkerId = req.Id;
                WorkerAttendance.StartDate = DateTime.Today;
                WorkerAttendance.StartDateTime = DateTime.Now;
                WorkerAttendance.StartStatus = true;
                WorkerAttendance.CreatedAt = DateTime.Now;
                WorkerAttendance.Date = DateTime.Today;
                if (LateTime < now)
                {
                    WorkerAttendance.IsLate = true;
                }
                else
                {
                    WorkerAttendance.IsPresent = true;
                    WorkerAttendance.IsLate = false;
                }
                _dbContext.WorkerAttendance.Add(WorkerAttendance);
                _dbContext.SaveChanges();
                response.StartDayStatus = true;
                response.Success = true;
                response.Id = WorkerAttendance.Id;
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

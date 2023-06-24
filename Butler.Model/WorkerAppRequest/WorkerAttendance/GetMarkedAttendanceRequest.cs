using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.WorkerAppRequest.WorkerAttendance
{
    public class GetMarkedAttendanceResponse : Response 
    {
        public int Id { get; set; }
        public string Attendance  { get; set; }
    }
    public class GetMarkedAttendanceRequest
    {
        public int WorkerId { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetMarkedAttendanceRequest req)
        {
            var response = new GetMarkedAttendanceResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Attendance = _dbContext.WorkerAttendance.Where(x => x.Date == DateTime.Today && x.WorkerId == req.WorkerId).FirstOrDefault();
                response.Attendance = "";
                if(Attendance != null)
                {
                    if (Attendance.StartStatus == true)
                    {
                        response.Id = Attendance.Id;
                        response.Attendance = "Marked";
                    }
                    if (Attendance.EndStatus == true)
                    {
                        response.Id = Attendance.Id;
                        response.Attendance = "End";
                    }
                }
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

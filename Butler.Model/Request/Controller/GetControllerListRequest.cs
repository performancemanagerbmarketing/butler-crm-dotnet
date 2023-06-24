using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Controller
{
    public class GetControllerListResponse : Response
    {
        public List<Controller> Data { get; set; }
    }
    public class Controller
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public int UserType { get; set; }
        public bool ApprovalStatus { get; set; }
        public string Email { get; set; }
        public int ReferenceId { get; set; }
        public string ReferenceName { get; set; }
        public bool IsActive { get; set; }
        public string CNIC { get; set; }
        public string ControlCenterName { get; set; }
    }
    public class GetControllerListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetControllerListRequest req)
        {
            var response = new GetControllerListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<Controller>();
            try
            {
                var Controllers = _dbContext.UserProfile.Where(x => x.UserType == (int)UserType.Controller).ToList();
                foreach (var controller in Controllers)
                {
                    var Controller = new Controller();
                    Controller.Id = controller.Id;
                    Controller.FullName = controller.FirstName + " " + controller.LastName;
                    Controller.ProfileImageUrl = controller.ProfileImageUrl;
                    Controller.Address = controller.Address;
                    Controller.ApprovalStatus = controller.ApprovalStatus;
                    Controller.Contact = controller.Contact;
                    Controller.UserName = controller.UserName;
                    Controller.Email = controller.Email;
                    Controller.ReferenceId = controller.ReferenceId??0;
                    Controller.CNIC = controller.CNIC;
                    Controller.IsActive = controller.IsActive;
                    if(controller.ControllerCenterId != 0 && controller.ControllerCenterId != null)
                    {
                        Controller.ControlCenterName = _dbContext.ControlCenter.Where(x => x.Id == controller.ControllerCenterId).FirstOrDefault().Name;
                    }
                    if (controller.ReferenceId.HasValue)
                    {
                        Controller.ReferenceId = controller.ReferenceId.Value;
                        var Reference = _dbContext.Reference.Where(x => x.Id == controller.ReferenceId).FirstOrDefault(); 
                        if(Reference != null)
                        {
                            Controller.ReferenceName = Reference.FullName;
                        }
                    }
                    response.Data.Add(Controller);
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

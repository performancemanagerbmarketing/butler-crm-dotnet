using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Controller
{
    public class GetControllerResponse : Response
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
        public int? ReferenceId { get; set; }
        public bool IsActive { get; set; }
        public string CNIC { get; set; }
        public string CNICFrontImageUrl { get; set; }
        public string CNICBackImageUrl { get; set; }
        public List<Category> Category { get; set; }
        public Reference Reference { get; set; }
        public ControlCenter ControlCenter { get; set; }
    }
    public class Reference
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ControlCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetControllerRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetControllerRequest req)
        {
            var response = new GetControllerResponse();
            response.ValidationErrors = new List<string>();
            response.Category = new List<Category>();
            try
            {
                var Controller = _dbContext.UserProfile.Where(x => x.Id == req.Id).FirstOrDefault();
                response.Id = Controller.Id;
                response.FirstName = Controller.FirstName;
                response.LastName = Controller.LastName;
                response.Address = Controller.Address;
                response.Contact = Controller.Contact;
                response.UserName = Controller.UserName;
                response.ProfileImageUrl = Controller.ProfileImageUrl;
                response.ApprovalStatus = Controller.ApprovalStatus;
                response.Email = Controller.Email;
                response.ReferenceId = Controller.ReferenceId;
                response.CNIC = Controller.CNIC;
                response.CNICBackImageUrl = Controller.CNICBackImageUrl;
                response.CNICFrontImageUrl = Controller.CNICFrontImageUrl;
                response.IsActive = response.IsActive;
                response.Success = true;
                if(Controller.ReferenceId != 0 && Controller.ReferenceId != null)
                {
                    var Reference = _dbContext.Reference.Where(x => x.Id == Controller.ReferenceId).FirstOrDefault();
                    var reference = new Reference();
                    reference.Id = Reference.Id;
                    reference.Name = Reference.FullName;
                    response.Reference = reference;
                }
                if (Controller.ControllerCenterId != 0 && Controller.ControllerCenterId != null)
                {
                    var CC = _dbContext.ControlCenter.Where(x => x.Id == Controller.ControllerCenterId).FirstOrDefault();
                    var ControlCenter = new ControlCenter();
                    ControlCenter.Id = CC.Id;
                    ControlCenter.Name = CC.Name;
                    response.ControlCenter = ControlCenter;
                }
                if (Controller.ControllerCategory != null)
                {
                    foreach(var Category in Controller.ControllerCategory)
                    {
                        var row = new Category();
                        row.Id = Category.CategoryId;
                        row.Name = Category.CategoryName;
                        response.Category.Add(row);
                    }
                }
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

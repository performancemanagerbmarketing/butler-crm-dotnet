using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Controller
{
    public class EditControllerResponse : Response { }
    public class EditControllerRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public int UserType { get; set; }
        public bool ApprovalStatus { get; set; }
        public int ReferenceId { get; set; }
        public bool IsActive { get; set; }
        public string CNIC { get; set; }
        public int ControlCenterId { get; set; }
        public string CNICBackImageUrl { get; set; }
        public string CNICFrontImageUrl { get; set; }
        public  List<Category> Category { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EditControllerRequest req)
        {
            var response = new EditControllerResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Controller = _dbContext.UserProfile.Where(x => x.Id == req.Id).FirstOrDefault();
                Controller.FirstName = req.FirstName;
                Controller.LastName = req.LastName;
                Controller.UserName = req.UserName;
                Controller.FullName = req.FirstName + " " + req.LastName;
                Controller.Contact = req.Contact;
                Controller.Address = req.Address;
                Controller.ApprovalStatus = req.ApprovalStatus;
                Controller.ProfileImageUrl = req.ProfileImageUrl;
                Controller.Date = DateTime.Today;
                Controller.CNIC = req.CNIC;
                Controller.CNICBackImageUrl = req.CNICBackImageUrl;
                Controller.CNICFrontImageUrl = req.CNICFrontImageUrl;
                Controller.ReferenceId = req.ReferenceId;
                Controller.ControllerCenterId = req.ControlCenterId;
                Controller.IsActive = req.IsActive;
                if(req.Category != null)
                {
                    var items = Controller.ControllerCategory.ToList();
                    if(items != null)
                    {
                        foreach (var item in items)
                        {
                            _dbContext.ControllerCategory.Remove(item);
                            _dbContext.SaveChanges();
                        }
                    }
                    foreach (var Category in req.Category)
                    {
                        var ControllerCategory = new Butler.Model.EntityModel.ControllerCategory();
                        ControllerCategory.ControllerId = Controller.Id;
                        ControllerCategory.ControllerName = Controller.FullName;
                        ControllerCategory.CategoryId = Category.Id;
                        ControllerCategory.CategoryName = Category.Name;
                        _dbContext.ControllerCategory.Add(ControllerCategory);
                        _dbContext.SaveChanges();
                    }
                }
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
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

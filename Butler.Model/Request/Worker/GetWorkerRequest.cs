using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Worker
{
    public class GetWorkerResponse : Response
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public Nullable<int> UserType { get; set; }
        public bool IsActive { get; set; }
        public string CNIC { get; set; }
        public Nullable<System.DateTime> CNICExpiryDate { get; set; }
        public string CNICFrontImageUrl { get; set; }
        public string CNICBackImageUrl { get; set; }
        public string VerficationImageUrl { get; set; }
        public bool VerificationStatus { get; set; }
        public string OfficeAddress { get; set; }
        public string OtherAddress { get; set; }
        public bool ApprovalStatus { get; set; }
        public Nullable<int> ReferenceId { get; set; }
        public Nullable<int> ControllerCenterId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public bool IsAdded { get; set; }
        public ControlCenter ControlCenter { get; set; }
    }
    public class ControlCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetWorkerRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetWorkerRequest req)
        {
            var response = new GetWorkerResponse();

            response.ValidationErrors = new List<string>();
            try
            {
                var Worker = _dbContext.UserProfile.Where(x => x.Id == req.Id && x.UserType == (int)UserType.Worker).FirstOrDefault();
                response.Id = Worker.Id;
                response.FirstName = Worker.FirstName;
                response.LastName = Worker.LastName;
                response.CNIC = Worker.CNIC;
                response.Address = Worker.Address;
                response.Contact = Worker.Contact;
                response.Email = Worker.Email;
                response.IsActive = Worker.IsActive;
                response.ApprovalStatus = Worker.ApprovalStatus;
                response.UserName = Worker.UserName;
                response.ControllerCenterId = Worker.ControllerCenterId;
                response.CNICBackImageUrl = Worker.CNICBackImageUrl;
                response.CNICFrontImageUrl = Worker.CNICFrontImageUrl;
                response.ProfileImageUrl = Worker.ProfileImageUrl;
                response.UserId = Worker.UserId;
                response.UserType = Worker.UserType;
                response.ReferenceId = Worker.ReferenceId;
                var ControlCenter = _dbContext.ControlCenter.Where(x => x.Id == Worker.ControllerCenterId).FirstOrDefault();
                if (ControlCenter != null)
                {
                    var CC = new ControlCenter();
                    CC.Id = ControlCenter.Id;
                    CC.Name = ControlCenter.Name;
                    response.ControlCenter = CC;
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

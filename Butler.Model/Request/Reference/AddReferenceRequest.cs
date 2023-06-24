using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Reference
{
    public class AddReferenceResponse : Response { }
    public class AddReferenceRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CNIC { get; set; }
        public string CNICFrontImageUrl { get; set; }
        public string CNICBackImageUrl { get; set; }
        public string Notes { get; set; }
        public string Address { get; set; }
        public bool IsAMember { get; set; }
        public string UserId { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public bool IsAdded { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddReferenceRequest req)
        {
            var response = new AddReferenceResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Reference = new Butler.Model.EntityModel.Reference();
                Reference.FullName = req.FullName;
                Reference.CNIC = req.CNIC;
                Reference.CNICFrontImageUrl = req.CNICFrontImageUrl;
                Reference.CNICBackImageUrl = req.CNICBackImageUrl;
                Reference.Notes = req.Notes;
                Reference.Address = req.Address;
                Reference.IsAMember = req.IsAMember;
                Reference.ProfileImageUrl = req.ProfileImageUrl;
                Reference.UserId = req.UserId;
                Reference.CreatedAt = DateTime.Now;
                Reference.Date = DateTime.Today;
                Reference.IsAdded = true;
                _dbContext.Reference.Add(Reference);
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

using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Reference
{
    public class GetReferenceResponse : Response
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
        public bool IsAdded { get; set; }
        
    }
    public class GetReferenceRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetReferenceRequest req)
        {
            var response = new GetReferenceResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Reference = _dbContext.Reference.Where(x => x.Id == req.Id).FirstOrDefault();
                response.Id = Reference.Id;
                response.FullName = Reference.FullName;
                response.CNIC = Reference.CNIC;
                response.CNICBackImageUrl = Reference.CNICBackImageUrl;
                response.CNICFrontImageUrl = Reference.CNICFrontImageUrl;
                response.ProfileImageUrl = Reference.ProfileImageUrl;
                response.Notes = Reference.Notes;
                response.Address = Reference.Address;
                response.IsAMember = Reference.IsAMember;
                response.IsAdded = Reference.IsAdded;
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

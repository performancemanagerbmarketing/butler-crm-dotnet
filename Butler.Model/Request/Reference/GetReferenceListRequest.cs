using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Reference
{
    public class GetReferenceListResponse : Response
    {
        public List<Reference> Data { get; set; }
    }
    public class Reference
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
    public class GetReferenceListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetReferenceListRequest req)
        {
            var response = new GetReferenceListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<Reference>();
            try
            {
                var References = _dbContext.Reference.ToList();
                foreach (var reference in References)
                {
                    var Reference = new Reference();
                    Reference.Id = reference.Id;
                    Reference.FullName = reference.FullName;
                    Reference.CNIC = reference.CNIC;
                    Reference.CNICFrontImageUrl = reference.CNICFrontImageUrl;
                    Reference.CNICBackImageUrl = reference.CNICBackImageUrl;
                    Reference.Address = reference.Address;
                    Reference.Notes = reference.Notes;
                    Reference.IsAMember = reference.IsAMember;
                    Reference.ProfileImageUrl = reference.ProfileImageUrl;
                    Reference.UserId = reference.UserId;
                    Reference.IsAdded = reference.IsAdded;
                    response.Data.Add(Reference);
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

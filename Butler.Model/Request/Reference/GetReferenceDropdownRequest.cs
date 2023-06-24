using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Reference
{
    public class GetReferenceDropdownResponse : Response
    {
        public List<ReferenceDropdown> Data { get; set; }
    }
    public class ReferenceDropdown
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
    public class GetReferenceDropdownRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetReferenceDropdownRequest req)
        {
            var response = new GetReferenceDropdownResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<ReferenceDropdown>();
            try
            {
                var References = _dbContext.Reference.OrderBy(o => o.FullName).ToList();
                foreach (var Reference in References)
                {
                    var row = new ReferenceDropdown();
                    row.Id = Reference.Id;
                    row.FullName = Reference.FullName;
                    response.Data.Add(row);
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

using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.ControlCenter
{
    public class GetControlCenterDropdownResponse : Response
    {
        public List<ControlCenterDropdown> Data { get; set; }
    }
    public class ControlCenterDropdown
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetControlCenterDropdownRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetControlCenterDropdownRequest req)
        {
            var response = new GetControlCenterDropdownResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<ControlCenterDropdown>();
            try
            {
                var ControlCenters = _dbContext.ControlCenter.OrderBy(o => o.Name).ToList();
                foreach(var ControlCenter in ControlCenters)
                {
                    var row = new ControlCenterDropdown();
                    row.Id = ControlCenter.Id;
                    row.Name = ControlCenter.Name;
                    response.Data.Add(row);
                }
                response.Success = true;
            }
            catch(Exception e)
            {
                response.Success = false;
                response.ValidationErrors.Add(e.Message.ToString());
            }
            return response;
        }
    }
}

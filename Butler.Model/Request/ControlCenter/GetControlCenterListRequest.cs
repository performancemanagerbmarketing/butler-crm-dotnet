using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.ControlCenter
{
    public class GetControlCenterListResponse : Response
    {
        public List<ControlCenter> Data { get; set; }
    }
    public class ControlCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public bool IsAdded { get; set; }
    }
    public class GetControlCenterListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetControlCenterListRequest req)
        {
            var response = new GetControlCenterListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<ControlCenter>();
            try
            {
                var ControlCenters = _dbContext.ControlCenter.ToList();
                foreach (var controlCenter in ControlCenters)
                {
                    var ControlCenter = new ControlCenter();
                    ControlCenter.Id = controlCenter.Id;
                    ControlCenter.Name = controlCenter.Name;
                    ControlCenter.Latitude = controlCenter.Latitude;
                    ControlCenter.Longitude = controlCenter.Longitude;
                    response.Data.Add(ControlCenter);
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

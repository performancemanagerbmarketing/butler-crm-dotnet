using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.ControlCenter
{
    public class GetControlCenterResponse : Response
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public bool IsAdded { get; set; }
    }
    public class GetControlCenterRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetControlCenterRequest req)
        {
            var response = new GetControlCenterResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var ControlCenter = _dbContext.ControlCenter.Where(x => x.Id == req.Id).FirstOrDefault();
                response.Id = ControlCenter.Id;
                response.Name = ControlCenter.Name;
                response.Longitude = ControlCenter.Longitude;
                response.Latitude = ControlCenter.Latitude;
                response.IsAdded = ControlCenter.IsAdded;
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

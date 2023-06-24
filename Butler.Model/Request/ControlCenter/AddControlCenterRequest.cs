using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.ControlCenter
{
    public class AddControlCenterResponse : Response { }
    public class AddControlCenterRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public bool IsAdded { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddControlCenterRequest req)
        {
            var response = new AddControlCenterResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var ControlCenter = new Butler.Model.EntityModel.ControlCenter();
                ControlCenter.Name = req.Name;
                ControlCenter.Longitude = req.Longitude;
                ControlCenter.Latitude = req.Latitude;
                ControlCenter.LocationName = req.LocationName;
                ControlCenter.IsAdded = true;
                _dbContext.ControlCenter.Add(ControlCenter);
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

using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.ControlCenter
{
    public class EditControlCenterResponse : Response { }
    public class EditControlCenterRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public bool IsAdded { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EditControlCenterRequest req)
        {
            var response = new EditControlCenterResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var ControlCenter = _dbContext.ControlCenter.Where(x => x.Id == req.Id).FirstOrDefault();
                ControlCenter.Name = req.Name;
                ControlCenter.Latitude = req.Latitude;
                ControlCenter.Longitude = req.Longitude;
                ControlCenter.LocationName = req.LocationName;
                ControlCenter.IsAdded = req.IsAdded;
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

using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Rating
{
    public class AddServiceRatingResponse : Response { }
    public class AddServiceRatingRequest
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int CustomerId { get; set; }
        public string JobTitle { get; set; }
        public string CustomerName { get; set; }
        public int NoOfRating { get; set; }
        public string Remarks { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddServiceRatingRequest req)
        {
            var response = new AddServiceRatingResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Rating = new Butler.Model.EntityModel.ServiceRating();
                Rating.CustomerId = req.CustomerId;
                Rating.CustomerName = req.CustomerName;
                Rating.JobId = req.JobId;
                Rating.JobTitle = req.JobTitle;
                Rating.NoOfRating = req.NoOfRating;
                Rating.Remarks = req.Remarks;
                Rating.CreatedAt = DateTime.Now;
                Rating.CreatedBy = req.CustomerName;
                Rating.Date = DateTime.Today;
                _dbContext.ServiceRating.Add(Rating);
                _dbContext.SaveChanges();
                response.Success = true;
            }
            catch(Exception e)
            {
                response.ValidationErrors.Add(e.Message.ToString());
                response.Success = false;
            }
            return response;
        }
    }
}

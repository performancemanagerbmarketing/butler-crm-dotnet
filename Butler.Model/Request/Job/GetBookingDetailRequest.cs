using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Job
{
    public class GetBookingDetailResponse : Response
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitutde { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ControlCenterId { get; set; }
        public string ControlCenterName { get; set; }
        public string AudioUrl { get; set; }
        public List<string> ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public string CustomerContact { get; set; }
        public bool IsAdded { get; set; }
        public DateTime? BookingDate { get; set; }
        public Category Category { get; set; }
        public ControlCenter ControlCenter { get; set; }
    }
    public class GetBookingDetailRequest
    {
        public int Id { get; set; }
        public int Status { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetBookingDetailRequest req)
        {
            var response = new GetBookingDetailResponse();
            response.ValidationErrors = new List<string>();
            response.ImageUrl = new List<string>();
            try
            {
                var Job = _dbContext.Job.Where(x => x.Id == req.Id && x.Status == req.Status).FirstOrDefault();
                response.Id = Job.Id;
                response.CustomerId = Job.CustomerId ?? 0;
                response.CategoryName = Job.CustomerName;
                response.CustomerEmail = Job.CustomerEmail;
                response.CustomerAddress = Job.CustomerAddress;
                response.CustomerContact = Job.CustomerContact;
                response.Title = Job.Title;
                response.Description = Job.Description;
                response.BookingDate = Job.BookingDate;
                response.ImageUrl.Add(Job.ImageUrl);
                response.ImageUrl.Add(Job.ImageUrl2);
                response.ImageUrl.Add(Job.ImageUrl3);
                var Category = new Category();
                Category.Id = Job.CategoryId ?? 0;
                Category.Name = Job.CategoryName;
                response.Category = Category;
                var ControlCenter = new ControlCenter();
                ControlCenter.Id = Job.ControlCenterId ?? 0;
                ControlCenter.Name = Job.ControlCenterName;
                if(ControlCenter != null)
                {
                    response.ControlCenter = ControlCenter;
                }
                response.Status = Job.Status ?? 0;
                response.AudioUrl = Job.AudioUrl;
                response.VideoUrl = Job.VideoUrl;
                response.IsAdded = Job.IsAdded;
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

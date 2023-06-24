using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Job
{
    public class AddJobDetailResponse : Response { }
    public class JobDetail
    {
        public int JobId { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public decimal Amount { get; set; }
        public int Id { get; set; }
        public string MaterialName { get; set; }
        public decimal Cost { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public bool IsAdded { get; set; }
    }
    public class AddJobDetailRequest
    {
        public List<JobDetail> JobDetail { get; set; }
        public List<JobDetail> Material { get; set; }
        public string UserId { get; set; }
        public int JobId { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddJobDetailRequest req)
        {
            var response = new AddJobDetailResponse();
            response.ValidationErrors = new List<string>();

            try
            {
                var User = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).FirstOrDefault();
                var Job = _dbContext.Job.Where(x => x.Id == req.JobId).FirstOrDefault();
                if (req.JobDetail != null)
                {
                    var items = _dbContext.JobDetail.Where(x => x.JobId == req.JobId).ToList();
                    foreach (var item in items)
                    {
                        Job.TotalAmount = Job.TotalAmount - item.Amount;
                        _dbContext.JobDetail.Remove(item);
                        _dbContext.SaveChanges();
                    }
                    foreach (var JD in req.JobDetail)
                    {
                        var JobDetail = new Butler.Model.EntityModel.JobDetail();
                        if (JD.SubCategoryId != 0)
                        {
                            JobDetail.JobId = JD.JobId;
                            JobDetail.Description = JD.Description;
                            JobDetail.SubCategoryId = JD.SubCategoryId;
                            JobDetail.SubCategoryName = JD.SubCategoryName;
                            JobDetail.Amount = JD.Amount;
                            JobDetail.Discount = JD.Discount;
                            JobDetail.CreatedAt = DateTime.Now;
                            JobDetail.Date = DateTime.Today;
                            JobDetail.CreatedBy = User.FullName;
                        }
                        _dbContext.JobDetail.Add(JobDetail);
                        Job.TotalAmount = Job.TotalAmount + JobDetail.Amount;
                        _dbContext.SaveChanges();
                    }
                }
                if(req.Material != null)
                {
                    var items = _dbContext.MaterialCost.Where(x => x.JobId == req.JobId).ToList();
                    foreach (var item in items)
                    {
                        Job.TotalAmount = Job.TotalAmount - item.Cost;
                        _dbContext.MaterialCost.Remove(item);
                        _dbContext.SaveChanges();
                    }
                    foreach (var JD in req.Material)
                    {
                        var MaterialCost = new Butler.Model.EntityModel.MaterialCost();
                        MaterialCost.JobId = req.JobId;
                        MaterialCost.MaterialName = JD.MaterialName;
                        MaterialCost.Cost = JD.Cost;
                        MaterialCost.CreatedAt = DateTime.Now;
                        MaterialCost.Date = DateTime.Today;
                        MaterialCost.CreatedBy = User.FullName;
                        _dbContext.MaterialCost.Add(MaterialCost);
                        Job.TotalAmount = Job.TotalAmount + MaterialCost.Cost;
                        _dbContext.SaveChanges();
                    }
                }
                var Notification = new Butler.Model.EntityModel.Notification();
                Notification.AdminId = User.Id;
                Notification.CustomerId = _dbContext.Job.Where(x => x.Id == req.JobId).FirstOrDefault().CustomerId;
                Notification.IsRead = false;
                Notification.Content = "Controller has update your order status";
                Notification.Title = "Service Amount";
                Notification.CreatedBy = User.FullName;
                Notification.CreatedAt = DateTime.Now;
                Notification.Date = DateTime.Today;
                _dbContext.Notification.Add(Notification);
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

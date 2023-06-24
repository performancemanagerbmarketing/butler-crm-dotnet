using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.SubCategory
{
    public class EditSubCategoryResponse : Response { }
    public class EditSubCategoryRequest
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string ConditionalKey { get; set; }
        public string AdditionalInfoHeading { get; set; }
        public string AdditionalInfoKey { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EditSubCategoryRequest req)
        {
            var response = new EditSubCategoryResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var SubCategory = _dbContext.SubCategory.Where(x => x.Id == req.Id).FirstOrDefault();
                SubCategory.Name = req.Name;
                SubCategory.CategoryId = req.CategoryId;
                SubCategory.Cost = req.Cost;
                SubCategory.CreatedAt = DateTime.Now;
                SubCategory.Date = DateTime.Today;
                SubCategory.ConditionalKey = req.ConditionalKey;
                SubCategory.AdditionalInfoHeading = req.AdditionalInfoHeading;
                SubCategory.AdditionalInfoKey = req.AdditionalInfoKey;
                SubCategory.ImageUrl = req.ImageUrl;
                SubCategory.IsActive = req.IsActive;
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

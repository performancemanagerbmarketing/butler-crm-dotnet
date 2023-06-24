using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.SubCategory
{
    public class GetSubCategoryResponse : Response
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
        public Category Category { get; set; }
        public bool IsActive { get; set; }
        public string ConditionalKey { get; set; }
        public string AdditionalInfoHeading { get; set; }
        public string AdditionalInfoKey { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetSubCategoryRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetSubCategoryRequest req)
        {
            var response = new GetSubCategoryResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var SubCategory = _dbContext.SubCategory.Where(x => x.Id == req.Id).FirstOrDefault();
                response.Id = SubCategory.Id;
                response.Name = SubCategory.Name;
                response.ImageUrl = SubCategory.ImageUrl;
                response.Cost = SubCategory.Cost??0;
                response.IsActive = SubCategory.IsActive;
                response.ConditionalKey = SubCategory.ConditionalKey;
                response.AdditionalInfoKey = SubCategory.AdditionalInfoKey;
                response.AdditionalInfoHeading = SubCategory.AdditionalInfoHeading;
                var Category = new Category();
                Category.Id = SubCategory.Category.Id;
                Category.Name = SubCategory.Category.Name;

                response.Category = Category;
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

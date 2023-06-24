using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.SubCategory
{
    public class GetSubCategoryListResponse : Response
    {
        public List<SubCategory> Data { get; set; }
    }
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public string Cost { get; set; }
        public bool IsActive { get; set; }
        public bool IsCheck { get; set; }
        public decimal Amount { get; set; }
        public string ConditionalKey { get; set; }
        public string AdditionalInfoHeading { get; set; }
        public string AdditionalInfoKey { get; set; }
    }
    public class GetSubCategoryListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public int? CategoryId { get; set; }
        public object RunRequest(GetSubCategoryListRequest req)
        {
            var response = new GetSubCategoryListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<SubCategory>();
            try
            {
                var SubCategories = new List<Model.EntityModel.SubCategory>();//_dbContext.SubCategory.Where(x=>x.Type == req.Type).ToList();
                if (req.CategoryId.HasValue)
                {
                    SubCategories = _dbContext.SubCategory.Where(x => x.CategoryId == req.CategoryId).ToList();
                }
                else
                {
                    SubCategories = _dbContext.SubCategory.ToList();
                }
                foreach (var subCategory in SubCategories)
                {
                    var SubCategory = new SubCategory();
                    SubCategory.Id = subCategory.Id;
                    SubCategory.Name = subCategory.Name;
                    var val = subCategory.Cost.Value;
                    SubCategory.Cost = val.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                    SubCategory.CategoryName = _dbContext.Category.Where(x => x.Id == subCategory.CategoryId).FirstOrDefault().Name;
                    SubCategory.IsActive = subCategory.IsActive;
                    response.Data.Add(SubCategory);
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

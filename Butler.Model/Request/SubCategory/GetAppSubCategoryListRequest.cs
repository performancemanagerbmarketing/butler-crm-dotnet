using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.SubCategory
{
    public class GetAppSubCategoryListResponse : Response
    {
        public List<SubCategory> Data { get; set; }
    }
    
    public class GetAppSubCategoryListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public int? CategoryId { get; set; }
        public object RunRequest(GetAppSubCategoryListRequest req)
        {
            var response = new GetAppSubCategoryListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<SubCategory>();
            try
            {
                var SubCategories = new List<Model.EntityModel.SubCategory>();
                if (req.CategoryId.HasValue)
                {
                    SubCategories = _dbContext.SubCategory.Where(x => x.CategoryId == req.CategoryId && x.IsActive == true).ToList();
                }
                else
                {
                    SubCategories = _dbContext.SubCategory.ToList();
                }
                foreach (var subCategory in SubCategories)
                {
                    var SubCategory = new SubCategory();
                    SubCategory.ImageUrl = subCategory.ImageUrl;
                    SubCategory.Amount = subCategory.Cost.Value;
                    SubCategory.Id = subCategory.Id;
                    SubCategory.Name = subCategory.Name;
                    var val = subCategory.Cost.Value;
                    SubCategory.Cost = val.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                    SubCategory.CategoryName = _dbContext.Category.Where(x => x.Id == subCategory.CategoryId).FirstOrDefault().Name;
                    SubCategory.IsActive = subCategory.IsActive;
                    SubCategory.ConditionalKey = subCategory.ConditionalKey;
                    SubCategory.AdditionalInfoHeading = subCategory.AdditionalInfoHeading;
                    SubCategory.AdditionalInfoKey = subCategory.AdditionalInfoKey;
                    SubCategory.IsCheck = false;
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

using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Category
{
    public class GetAppCategoryListResponse : Response
    {
        public List<Category> Data { get; set; }
    }

    public class GetAppCategoryListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public int? Type { get; set; }
        public object RunRequest(GetAppCategoryListRequest req)
        {
            var response = new GetAppCategoryListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<Category>();
            try
            {
                var Categories = new List<Model.EntityModel.Category>();
                if (req.Type.HasValue)
                {
                    Categories = _dbContext.Category.Where(x => x.Type == req.Type.Value && x.IsAdded == true).ToList();
                }
                else
                {
                    Categories = _dbContext.Category.Where(x => x.IsAdded == true).ToList();
                }
                foreach (var category in Categories)
                {
                    var Category = new Category();
                    Category.Id = category.Id;
                    Category.Name = category.Name;
                    Category.Description = category.Description;
                    Category.ProfileImageUrl = category.ProfileImageUrl;
                    Category.WebImageUrl = category.WebImageUrl;
                    Category.Type = category.Type;
                    Category.TypeEnum = ((CategoryType)category.Type).ToString();
                    response.Data.Add(Category);
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

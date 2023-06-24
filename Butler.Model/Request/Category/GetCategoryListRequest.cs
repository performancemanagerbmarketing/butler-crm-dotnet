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
    public class GetCategoryListResponse : Response
    {
        public List<Category> Data { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeEnum { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public string ProfileImageUrl { get; set; }
        public string WebImageUrl { get; set; }
        public int ServiceCount { get; set; }

    }
    public class GetCategoryListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public int? Type { get; set; }
        public object RunRequest(GetCategoryListRequest req)
        {
            var response = new GetCategoryListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<Category>();
            try
            {
                var Categories = new List<Model.EntityModel.Category>();//_dbContext.Category.Where(x=>x.Type == req.Type).ToList();
                if (req.Type.HasValue)
                {
                    Categories = _dbContext.Category.Where(x=>x.Type == req.Type.Value).ToList();
                }
                else
                {
                    Categories = _dbContext.Category.ToList();
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
            catch(Exception e)
            {
                response.Success = false;
                response.ValidationErrors.Add(e.Message.ToString());
            }
            return response;
        }
    }
}

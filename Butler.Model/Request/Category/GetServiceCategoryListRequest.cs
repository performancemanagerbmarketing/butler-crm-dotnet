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
    public class GetServiceCategoryListResponse : Response
    {
        public List<ServiceCategory> Data { get; set; }
    }
    public class ServiceCategory
    {
        public int Type { get; set; }
        public string TypeEnum { get; set; }
        public List<Category> Category { get; set; }
    }
    public class GetServiceCategoryListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetServiceCategoryListRequest req)
        {
            var response = new GetServiceCategoryListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<ServiceCategory>();
            try
            {
                var Categories = _dbContext.Category.Where(x=>x.IsAdded == true).GroupBy(g=>g.Type).Select(s=> new { Type = s.Key, Data = s.ToList()  }).ToList();
                foreach(var Category in Categories)
                {
                    var row = new ServiceCategory();
                    row.Type = Category.Type;
                    row.TypeEnum = ((CategoryType)Category.Type).ToString();
                    row.Category = new List<Category>();
                    foreach (var category in Category.Data)
                    {
                        var data = new Category();
                        data.Id = category.Id;
                        data.Name = category.Name;
                        data.Description = category.Description;
                        data.ProfileImageUrl = category.ProfileImageUrl;
                        data.WebImageUrl = category.WebImageUrl;
                        data.Type = category.Type;
                        data.TypeEnum = ((CategoryType)category.Type).ToString();
                        data.ServiceCount = category.SubCategory.Where(x=>x.IsActive == true).Count();
                        row.Category.Add(data);
                    }
                    response.Data.Add(row);
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

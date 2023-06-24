using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Category
{
    public class GetCategoryDropdownResponse : Response
    {
        public List<CategoryDropdown> Data { get; set; }
    }
    public class CategoryDropdown
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetCategoryDropdownRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetCategoryDropdownRequest req)
        {
            var response = new GetCategoryDropdownResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<CategoryDropdown>();
            try
            {
                var Categorys = _dbContext.Category.OrderBy(o => o.Name).ToList();
                foreach (var Category in Categorys)
                {
                    var row = new CategoryDropdown();
                    row.Id = Category.Id;
                    row.Name = Category.Name;
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

using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.SubCategory
{
    public class GetSubCategoryDropdownResponse : Response
    {
        public List<SubCategoryDropdown> Data { get; set; }
    }
    public class SubCategoryDropdown
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
    public class GetSubCategoryDropdownRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetSubCategoryDropdownRequest req)
        {
            var response = new GetSubCategoryDropdownResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<SubCategoryDropdown>();
            try
            {
                var SubCategorys = _dbContext.SubCategory.OrderBy(o => o.Name).ToList();
                foreach (var SubCategory in SubCategorys)
                {
                    var row = new SubCategoryDropdown();
                    row.Id = SubCategory.Id;
                    row.Name = SubCategory.Name;
                    row.Amount = SubCategory.Cost??0;
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

using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.SubCategory
{
    public class DeleteSubCategoryResponse : Response { }
    public class DeleteSubCategoryRequest
    {
        public int Id { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(DeleteSubCategoryRequest req)
        {
            var response = new DeleteSubCategoryResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var SubCategory = _dbContext.SubCategory.Where(x => x.Id == req.Id).FirstOrDefault();
                _dbContext.SubCategory.Remove(SubCategory);
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

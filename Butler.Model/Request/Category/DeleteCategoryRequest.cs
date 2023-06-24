using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Category
{
    public class DeleteCategoryResponse : Response { }
    public class DeleteCategoryRequest
    {
        public int Id { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(DeleteCategoryRequest req)
        {
            var response = new DeleteCategoryResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Category = _dbContext.Category.Where(x => x.Id == req.Id).FirstOrDefault();
                _dbContext.Category.Remove(Category);
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

using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Category
{
    public class AddCategoryResponse : Response { }
    public class AddCategoryRequest
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public string ProfileImageUrl { get; set; }
        public string WebImageUrl { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddCategoryRequest req)
        {
            var response = new AddCategoryResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Category = new Butler.Model.EntityModel.Category();
                Category.Name = req.Name;
                Category.Type = req.Type;
                Category.Description = req.Description;
                Category.ProfileImageUrl = req.ProfileImageUrl;
                Category.WebImageUrl = req.WebImageUrl;
                Category.CreatedAt = DateTime.Now;
                Category.Date = DateTime.Today;
                Category.IsAdded = true;
                _dbContext.Category.Add(Category);
                _dbContext.SaveChanges();
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

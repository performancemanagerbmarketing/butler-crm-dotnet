using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Category
{
    public class EditCategoryResponse : Response { }
    public class EditCategoryRequest
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProfileImageUrl { get; set; }
        public string WebImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public bool IsAdded { get; set; }

        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(EditCategoryRequest req)
        {
            var response = new EditCategoryResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Category = _dbContext.Category.Where(x => x.Id == req.Id).FirstOrDefault();
                Category.Name = req.Name;
                Category.Type = req.Type;
                Category.Description = req.Description;
                Category.ProfileImageUrl = req.ProfileImageUrl;
                Category.WebImageUrl = req.WebImageUrl;
                Category.CreatedAt = DateTime.Now;
                Category.Date = DateTime.Today;
                Category.IsAdded = req.IsAdded;
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

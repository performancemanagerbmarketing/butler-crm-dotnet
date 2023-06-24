using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.FileUpload
{
    public class AddFileResponse : Response {}
    public class AddFileRequest
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public string FileUploadUrl { get; set; }
        public string Type { get; set; }
        public int FilesType { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(AddFileRequest req)
        { 
            var response = new AddFileResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var User = _dbContext.UserProfile.Where(x => x.UserId == req.Id).FirstOrDefault();
                
                var FileUpload = new Butler.Model.EntityModel.UserFileUpload();
                if (req.UserId != 0 && req.UserId != null)
                {
                    FileUpload.UserId = req.UserId;
                }
                else
                {
                    FileUpload.UserId = User.Id;
                }
                FileUpload.FileUploadUrl = req.FileUploadUrl;
                if (req.Type.Contains("image"))
                {
                    FileUpload.FileType = (int)FileType.Image;
                }
                else if(req.Type.Contains("application"))
                {
                    FileUpload.FileType = (int)FileType.Document;
                }
                FileUpload.Date = DateTime.Today;
                _dbContext.UserFileUpload.Add(FileUpload);
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
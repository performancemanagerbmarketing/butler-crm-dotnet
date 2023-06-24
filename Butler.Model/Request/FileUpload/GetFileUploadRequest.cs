using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.FileUpload
{
    public class GetFileUploadResponse : Response
    {
        public List<FileUpload> FileUpload { get; set; }
    }
    public class FileUpload
    {
        public int Id { get; set; }
        public string FileUploadUrl { get; set; }
        public int FileType { get; set; }
    }
    public class GetFileUploadRequest
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetFileUploadRequest req)
        {
            var response = new GetFileUploadResponse();
            response.ValidationErrors = new List<string>();
            response.FileUpload = new List<FileUpload>();
            try
            {
                var User = _dbContext.UserProfile.Where(x => x.UserId == req.Id).FirstOrDefault();
                if (req.UserId != null && req.UserId != 0)
                {
                    var FileUploads = _dbContext.UserFileUpload.Where(x => x.UserId == req.UserId).ToList();
                    foreach(var fileUpload in FileUploads)
                    {
                        var row = new FileUpload();
                        row.FileUploadUrl = fileUpload.FileUploadUrl;
                        row.FileType = fileUpload.FileType??0;
                        response.FileUpload.Add(row);
                    }
                    response.Success = true;
                }
                else
                {
                    var FileUploads = _dbContext.UserFileUpload.Where(x => x.UserId == User.Id).ToList();
                    foreach (var fileUpload in FileUploads)
                    {
                        var row = new FileUpload();
                        row.FileUploadUrl = fileUpload.FileUploadUrl;
                        row.FileType = fileUpload.FileType ?? 0;
                        response.FileUpload.Add(row);
                    }
                    response.Success = true;
                }
                
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

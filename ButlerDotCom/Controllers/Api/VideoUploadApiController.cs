using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class VideoUploadApiController : ApiController
    {
        public class VideoUpload
        {
            public string base64Video { get; set; }
        }
        [HttpPost]
        public object uploadVideo(VideoUpload req)
        {
            var response = new ImageResponse();
            response.ValidationError = new List<string>();
            try
            {
                Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
                var base64File = regex.Replace(req.base64Video, string.Empty);
                byte[] bytes = Convert.FromBase64String(base64File);
                string path = HttpContext.Current.Server.MapPath("~/Uploads"); //Path
                string ImageName = Guid.NewGuid() + ".mp4";
                string CompletePath = path + "/" + ImageName;

                //Check if directory exist
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                }

                using (var output = File.Create(CompletePath))
                {

                    output.Close();
                    File.WriteAllBytes(CompletePath, bytes);
                    var FileUri = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/Uploads/" + ImageName;
                    response.VideoUrl = FileUri;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ValidationError.Add(ex.Message.ToString());
            }
            return response;

        }

        public class ImageResponse
        {
            public bool Success { get; set; }
            public string VideoUrl { get; set; }
            public List<string> ValidationError { get; set; }
        }
    }
}
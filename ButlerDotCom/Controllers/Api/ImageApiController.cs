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
    public class ImageApiController : ApiController
    {
        public class ImageUpload
        {
            public string base64Image { get; set; }
        }
        [HttpPost]
        public object uploadImage(ImageUpload req)
        {
            var response = new ImageResponse();
            response.ValidationError = new List<string>();
            try
            {
                Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
                var base64File = regex.Replace(req.base64Image, string.Empty);
                byte[] bytes = Convert.FromBase64String(base64File);

                Image image;
                string path = HttpContext.Current.Server.MapPath("~/Uploads"); //Path
                string ImageName = Guid.NewGuid() + ".jpg";
                string CompletePath = path + "/" + ImageName;

                //Check if directory exist
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                }

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                    image.Save(CompletePath);
                }
                var ImageUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/Uploads/" + ImageName;
                response.ImageUrl = ImageUrl;
                response.Success = true;


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
            public string ImageUrl { get; set; }
            public List<string> ValidationError { get; set; }
        }
    }
}
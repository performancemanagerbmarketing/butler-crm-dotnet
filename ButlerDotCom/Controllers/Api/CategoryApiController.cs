using Butler.Model.Request.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class CategoryApiController : ApiController
    {
        [HttpGet]
        public object GetListing([FromUri] GetCategoryListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object AppCategory([FromUri] GetAppCategoryListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object ServiceCategory([FromUri] GetServiceCategoryListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddCategory([FromBody] AddCategoryRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditCategory([FromBody] EditCategoryRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object DeleteCategory([FromBody] DeleteCategoryRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetCategory([FromUri] GetCategoryRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetCategoryDropDown([FromUri] GetCategoryDropdownRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}
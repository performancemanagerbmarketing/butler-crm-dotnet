using Butler.Model.Request.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class SubCategoryApiController : ApiController
    {
        [HttpGet]
        public object GetListing([FromUri] GetSubCategoryListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object AppSubCategoryList([FromUri] GetAppSubCategoryListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddSubCategory([FromBody] AddSubCategoryRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditSubCategory([FromBody] EditSubCategoryRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object DeleteSubCategory([FromBody] DeleteSubCategoryRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetSubCategory([FromUri] GetSubCategoryRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetSubCategoryDropdown([FromUri] GetSubCategoryDropdownRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}
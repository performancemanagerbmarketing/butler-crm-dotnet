using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ButlerDotCom.Controllers
{
    public class SubCategoryController : Controller
    {
        // GET: SubCategory
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [Authorize]
        public ActionResult Edit() 
        {
            return View();
        }
        [Authorize]
        public ActionResult Detail() 
        {
            return View();
        }

    }
}
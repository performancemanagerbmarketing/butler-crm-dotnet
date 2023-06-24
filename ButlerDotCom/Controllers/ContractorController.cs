using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ButlerDotCom.Controllers
{
    public class ContractorController : Controller
    {
        // GET: Contractor
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
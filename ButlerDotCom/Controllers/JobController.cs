using Butler.Model.Request.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ButlerDotCom.Controllers
{
    public class JobController : Controller
    {
        // GET: Job
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
        public ActionResult Details()
        {
            return View();
        }
        [Authorize]
        public ActionResult Print(int Id,string Type)
        {
            var req = new GetInvoiceDetailRequest { Id = Id };
            var resp = req.RunRequest(req);
            return View(resp);
        }
    }
}
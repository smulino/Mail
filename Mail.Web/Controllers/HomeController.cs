using Mail.Data;
using Mail.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mail.Web.Controllers
{
    public class HomeController : Controller
    {
		MailDbContext dbContext = new MailDbContext();

		// GET: Home
		public ActionResult Index()
        {
			var messages = dbContext.Set<Message>().AsQueryable().ToList();

            return View();
        }
    }
}
using Mail.Data;
using Mail.Data.Entities;
using System.Linq;
using System.Web.Mvc;

namespace Mail.Web.Controllers
{
    public class HomeController : Controller
    {
		MailDbContext dbContext = new MailDbContext();

		// GET: Home
		public ActionResult Index()
        {
			//var messages = dbContext.Set<Message>().AsQueryable().ToList();

			//var users = dbContext.Set<User>().AsQueryable().ToList()[1].Roles;

			var user = dbContext.GetUserById(1); 

			return View();
        }
    }
}
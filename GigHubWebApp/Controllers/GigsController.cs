using System.Linq;
using System.Web.Mvc;
using GigHubWebApp.Models;
using GigHubWebApp.ViewModels;

namespace GigHubWebApp.Controllers {
    public class GigsController : Controller {
        private readonly ApplicationDbContext _dbContext;

        public GigsController() {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Gigs
        public ActionResult Create() {
            var gigFormViewModel = new GigFormViewModel() {
                Genres = _dbContext.Genres.ToList()
            };

            return View(gigFormViewModel);
        }
    }
}
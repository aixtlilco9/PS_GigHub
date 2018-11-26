using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;
        public HomeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_context = new ApplicationDbContext();
        }
        public ActionResult Index(string query = null)
        {

            var upcomingGigs = _unitOfWork.Gigs.GetUpcomingGigs(query);

            

            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "UpComing Gigs",
                SearchTerm = query,
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };
            return View("Gigs",viewModel);
        }

        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
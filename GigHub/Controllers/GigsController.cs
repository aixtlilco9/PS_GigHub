using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistence;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ViewResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetUpComingGigsByArtist(userId);
                //.ToList();//the .tolist is to immidiately exit the query according to mosh

            return View(gigs);
        }

       

        [HttpPost] // so it can only be executed in a form post
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchTerm});
        }

        [Authorize]
        //redirects users to login pag before being able to creating a gig
        //limits acces to action to authenticated users
        //user.identity.getuserid() gets id of currently logged in user.
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Gig"
            };
            return View("GigForm",viewModel);
        }


        [Authorize]
       //this method was duplicated of off create and renamed to edit with a parameter added
        public ActionResult Edit(int id)
        {
            //added var gig below to retrieve id from database
            //and added userId so not anyone can edit a gig only the person who created it.
            var userId = User.Identity.GetUserId();
            //var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            var gig = _unitOfWork.Gigs.GetGig(id);
            if (gig == null)
            {
                return HttpNotFound();
            }

            if (gig.ArtistId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            var viewModel = new GigFormViewModel()
            {
                //added Date,time,genre to intialize view model so the form can be 
                //auto-populated with values stored in the database
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = new ApplicationDbContext().Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue


            };
            return View("GigForm", viewModel);
        }

        

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            //cntr+shft+r to extract n minimize queries

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }

       

       

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            //used cntr shift R to refactor and inline variable
            //therfore combining it where it is used below
            //var artistId = User.Identity.GetUserId();
            //var genreId = viewModel.Genre;

            //below are no longer used changed were made to model and migration made to
            //simplify this replacing Artist and Genre
            //var artist = _context.Users.Single(u => u.Id == artistId);
            //var genre = _context.Genres.Single(g => g.Id == genreId);

            //if below is not valid returns create view and used viewmodel passed to this method so all existing values are present along with validation methods.
            if (!ModelState.IsValid)
            {

                viewModel.Genres = _unitOfWork.Genres.GetGenres();//this was done to prevent returning null in view when rending dropdown list GENRES IS NOt POPULATED
                return View("GigForm", viewModel);
            }


            var gig = new Gig
            {
                //Artist = artist,
                ArtistId = User.Identity.GetUserId(),
                //GetDateTime = GetDateTime.Parse(string.Format("{0} {1}", viewModel.Date, viewModel.Time)),
                DateTime = viewModel.GetDateTime(),
                //Genre = genre,
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();
           // _context.Gigs.Add(gig);
            //_context.SaveChanges();

            //instead of redirecting to home we will redirct to myy upcoming gigs
           // return RedirectToAction("Index", "Home");
            return RedirectToAction("Mine", "Gigs");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            //this was dupplicated from create and named update
            if (!ModelState.IsValid)
            {

                viewModel.Genres = _unitOfWork.Genres.GetGenres();//this was done to prevent returning null in view when rending dropdown list GENRES IS NOt POPULATED
                return View("GigForm", viewModel);
            }


            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
            {
                return HttpNotFound();
            }

            if (gig.ArtistId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);
            //gig.Venue = viewModel.Venue;
            //gig.DateTime = viewModel.GetDateTime();//<--comebine date and time fields and returns them as a datetime object
            //gig.GenreId = viewModel.Genre;

            _unitOfWork.Complete();

            //instead of redirecting to home we will redirct to myy upcoming gigs
           // return RedirectToAction("Index", "Home");
            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
            {
                return HttpNotFound();
            }

            var viewModel = new GigDetailsViewModel {Gig = gig};

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.isAttending = 
                    _unitOfWork.Attendances.GetAttendance(gig.Id, userId) != null; //!=null;

                viewModel.isFollowing = 
                    _unitOfWork.Followings.GetFollowing(userId, gig.ArtistId) != null;

            }

            return View("Details", viewModel);
        }

        
    }
}
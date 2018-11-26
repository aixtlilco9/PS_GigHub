using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.IntegrationTests.Extensions;
using GigHub.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _gigsController;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _gigsController = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            //Arrange
            var user = _context.Users.First();
            _gigsController.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig{Artist = user, DateTime = DateTime.Now.AddDays(1),Genre =genre, Venue = "-"};
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _gigsController.Mine();

            //Assert
            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);

        }

        //[Test, Isolated]
        //public void Update_WhenCalled_ShouldUppdateTheGivenGig()
        //{

        //    //Arrange
        //    var user = _context.Users.First();
        //    _gigsController.MockCurrentUser(user.Id, user.UserName);
        //    var genre = _context.Genres.Single(g => g.Id == 1);
        //    var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
        //    _context.Gigs.Add(gig);
        //    _context.SaveChanges();

        //    //Act
        //    var result = _gigsController.Update(new GigFormViewModel
        //    {
        //        Id = gig.Id,
        //        Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
        //        Time = "20:00",
        //        Venue = "Venue",
        //        Genre = 2
        //    });

        //    //Assert
        //    _context.Entry(gig).Reload();
        //    gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
        //    gig.Venue.Should().Be("Venue");
        //    gig.Genre.Should().Be(2);
        //}
    }
}

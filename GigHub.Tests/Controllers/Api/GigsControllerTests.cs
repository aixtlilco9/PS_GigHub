using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.IRepository;
using GigHub.Core.Models;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _gigsController;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRepository>();
           
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _gigsController = new GigsController(mockUoW.Object);
            _userId = "1";
            _gigsController.MockCurrentUser(_userId,"user1@domain.com");
        }
        [TestMethod]
        //3 nameconvention by roy osherove author of the art of unit testing
        public void Cancel_NoGigWithGivenIdExist_ShouldReturnNotFound()
        {
            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();


        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig { ArtistId = _userId + "2"};

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();

        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig { ArtistId = _userId  };

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}

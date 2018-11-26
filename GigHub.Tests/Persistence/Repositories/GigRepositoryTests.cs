using System;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using FluentAssertions;
using GigHub.Tests.Extensions;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _gigRepository;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);

            _gigRepository = new GigRepository(mockContext.Object);
            ;
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() {DateTime = DateTime.Now.AddDays(-1),ArtistId = "1"};
            _mockGigs.SetSource(new[] { gig });
            var gigs = _gigRepository.GetUpComingGigsByArtist("1");

            gigs.Should().BeEmpty();


        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCancled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();
            _mockGigs.SetSource(new[] { gig });
            var gigs = _gigRepository.GetUpComingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            _mockGigs.SetSource(new[] { gig });
            var gigs = _gigRepository.GetUpComingGigsByArtist(gig.ArtistId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsBArtist_GigIsForGivenArtistAndIsInFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            _mockGigs.SetSource(new[] { gig });
            var gigs = _gigRepository.GetUpComingGigsByArtist(gig.ArtistId );

            gigs.Should().Contain(gig);
        }
    }
}

using FluentAssertions;
using GigHubWebApp.Core.Models;
using GigHubWebApp.Persistence;
using GigHubWebApp.Persistence.Repositories;
using GigHubWebApp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace GigHubWebApp.Tests.Persistence.Repositories {
    [TestClass]
    public class GigRepositoryTests {
        private GigsRepositories _gigRepo;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitialize() {
            _mockGigs = new Mock<DbSet<Gig>>();

            var mockDbContext = new Mock<IApplicationDbContext>();
            mockDbContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);

            _gigRepo = new GigsRepositories(mockDbContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_PastGigs_ShouldNotBeReturned() {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var result = _gigRepo.GetUpcomingGigsByArtistId("1");

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned() {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });

            var result = _gigRepo.GetUpcomingGigsByArtistId("1");

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigForDifferentArtist_ShouldNotBeReturned() {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var result = _gigRepo.GetUpcomingGigsByArtistId(gig.ArtistId + "-");

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigForGivenArtistWhichInTheFuture_ShouldReturn() {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var result = _gigRepo.GetUpcomingGigsByArtistId(gig.ArtistId);

            result.Should().Contain(gig);
        }
    }
}

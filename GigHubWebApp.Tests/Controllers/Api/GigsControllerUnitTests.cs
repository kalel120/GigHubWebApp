using FluentAssertions;
using GigHubWebApp.Controllers.Api;
using GigHubWebApp.Core;
using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.Repositories;
using GigHubWebApp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;


namespace GigHubWebApp.Tests.Controllers.Api {
    [TestClass]
    public class GigsControllerUnitTests {
        private GigsController _gigsController;
        private Mock<IGigsRepositories> _mockRepository;
        private string _userid;

        public GigsControllerUnitTests() {
            _mockRepository = new Mock<IGigsRepositories>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.GigsRepo).Returns(_mockRepository.Object);

            _gigsController = new GigsController(mockUoW.Object);
            _userid = "1";
            _gigsController.MockThisUser(_userid, "user1@domain.com");

        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound() {
            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound() {
            var newGig = new Gig();
            newGig.Cancel();

            // This mock repository does setup gig repo with mockUser "1" and return our newly creatd gig object.
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(newGig);

            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnothersGig_ShouldReturnUnauthorized() {
            var testGig = new Gig { ArtistId = _userid + "-" };

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(testGig);

            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk() {
            var testGig = new Gig { ArtistId = _userid };

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(testGig);

            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}
using FluentAssertions;
using GigHubWebApp.Controllers.Api;
using GigHubWebApp.Core;
using GigHubWebApp.Core.Repositories;
using GigHubWebApp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;


namespace GigHubWebApp.Tests.Controllers.Api {
    [TestClass]
    public class GigsControllerUnitTests {
        private GigsController _gigsController;

        public GigsControllerUnitTests() {
            var mockRepository = new Mock<IGigsRepositories>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.GigsRepo).Returns(mockRepository.Object);

            _gigsController = new GigsController(mockUoW.Object);
            _gigsController.MockThisUser("1", "user1@domain.com");

        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound() {
            var result = _gigsController.Cancel(1);
            //Fluent assertion
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}

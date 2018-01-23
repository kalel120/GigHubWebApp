using GigHubWebApp.Controllers.Api;
using GigHubWebApp.Core;
using GigHubWebApp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace GigHubWebApp.Tests.Controllers.Api {
    [TestClass]
    public class GigsControllerUnitTests {
        private GigsController _gigsController;

        public GigsControllerUnitTests() {
            var mockUoW = new Mock<IUnitOfWork>();
            _gigsController = new GigsController(mockUoW.Object);
            _gigsController.MockThisUser("1", "user1@domain.com");

        }

        [TestMethod]
        public void TestMethod1() {
        }
    }
}

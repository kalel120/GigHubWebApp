using GigHubWebApp.Controllers.Api;
using GigHubWebApp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Claims;
using System.Security.Principal;


namespace GigHubWebApp.Tests.Controllers.Api {
    [TestClass]
    public class GigsControllerUnitTests {
        private GigsController _gigsController;

        public GigsControllerUnitTests() {
            var identity = new GenericIdentity("user1@domain.com");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "user1@domain.com"));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));

            var principal = new GenericPrincipal(identity, null);

            var mockUoW = new Mock<IUnitOfWork>();
            _gigsController = new GigsController(mockUoW.Object);
            _gigsController.User = principal;

        }

        [TestMethod]
        public void TestMethod1() {
        }
    }
}

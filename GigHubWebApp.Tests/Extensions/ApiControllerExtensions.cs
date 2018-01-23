using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace GigHubWebApp.Tests.Extensions {
    public static class ApiControllerExtensions {
        public static void MockThisUser(this ApiController controller, string userid, string username) {
            var identity = new GenericIdentity(username);
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userid));

            var principal = new GenericPrincipal(identity, null);
            controller.User = principal;
        }
    }
}

using System;
using System.Security.Claims;
using System.Security.Principal;

namespace CrudWithJQueryDatatable
{
    public static class customClaims
    {
        public static Claim FindClaim(this IPrincipal user, string claimType)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(claimType)) throw new ArgumentNullException(nameof(claimType));

            var claimsPrincipal = user as ClaimsPrincipal;
            return claimsPrincipal?.FindFirst(claimType);
        }

        public static string ProfilePic(IPrincipal user)
        {
            return FindClaim(user, "ProfilePic")?.Value;
        }

        public static string username(IPrincipal user)
        {
            return FindClaim(user, "username")?.Value;
        }
        public static string email(IPrincipal user)
        {
            return FindClaim(user, "email")?.Value;
        }
    }
}
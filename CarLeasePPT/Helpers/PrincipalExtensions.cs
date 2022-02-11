using System.Linq;
using System.Security.Principal;

namespace CarLeasePPT.Helpers
{
    public static class PrincipalExtensions
    {
        #region Public Methods and Operators

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Extensions is based on the post here:
        /// https://stackoverflow.com/questions/32369229/usage-of-user-isinrole-in-a-view
        /// Original author:
        /// https://stackoverflow.com/users/1071632/sam-farajpour-ghamari
        /// Updated/Modified with method group
        /// <summary>
        /// </summary>
        /// <param name="principal">The "User"</param>
        /// <param name="roles">"A comma delimited list of roles for which to return True.</param>
        /// <returns></returns>
        public static bool IsInAnyRole(this IPrincipal principal, params string[] roles)
        {
            return roles.Any(principal.IsInRole);
        }

        #endregion
    }
}
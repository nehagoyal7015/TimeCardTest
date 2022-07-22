using System;
using System.Security.Claims;
using System.Security.Principal;

namespace TimeCard.CustomAuthentication
{
    /// <summary>
    /// 
    /// </summary>
    public static class IdentityExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetUserId(this IIdentity identity)
        {
            var ident = identity as ClaimsIdentity;
            if (ident != null)
            {
                var claim = ident.FindFirst(ClaimTypes.NameIdentifier);
                if (claim != null)
                    return claim.Value;
            }
            
            return null;
        }
     

    }
}

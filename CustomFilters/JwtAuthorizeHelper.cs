using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WorksJwtClient.Builders.Concrete;
using WorksJwtClient.Models;

namespace WorksJwtClient.CustomFilters
{
    public class JwtAuthorizeHelper
    {
        public static void CheckUserRole(AppUser activeUser, string roles, ActionExecutingContext context)
        {
            Status status=null;
            if (!string.IsNullOrWhiteSpace(roles))
            {
                
                if (roles.Contains(","))
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new MultiRoleStatusBuilder());
                    status = director.GenerateStatus(activeUser, roles);

                }
                else
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new SingleRoleStatusBuilder());
                    status = director.GenerateStatus(activeUser, roles);
                }

            }
            CheckStatus(status,context);
        }

        private static void CheckStatus(Status status, ActionExecutingContext context)
        {
            if (!status.AccessStatus)
            {
                context.Result = new RedirectToActionResult("AccesDenied", "Account", null);
            }

        }
    }
}
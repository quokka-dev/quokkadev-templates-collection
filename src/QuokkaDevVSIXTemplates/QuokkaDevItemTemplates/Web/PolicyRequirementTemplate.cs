using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace $rootnamespace$
{
    /// <summary>
    /// $safeitemname$ policy requirement
    /// </summary>
    public class $safeitemname$ : IAuthorizationRequirement
    {  
        public string MyValue { get; set; }
    }

    /// <summary>
    /// Validate the $safeitemname$ policy requirement
    /// </summary>
    public class $safeitemname$Handler: AuthorizationHandler<$safeitemname$>
    {
        /// <summary>
        /// Validate the $safeitemname$ policy requirement
        /// Remember to register this handler in startup, for example with:
        /// services.AddScoped<$safeitemname$Handler,$safeitemname$Handler>();
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, $safeitemname$ requirement)
        {
            Claim claim = context?.User?.FindFirst("http://schemas.microsoft.com/identity/claims/sub");
            if ( claim == null || claim.Value !== requirement.MyValue)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
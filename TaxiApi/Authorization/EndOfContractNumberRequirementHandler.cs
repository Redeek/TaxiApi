using Microsoft.AspNetCore.Authorization;

namespace TaxiApi.Authorization
{
    public class EndOfContractNumberRequirementHandler : AuthorizationHandler<EndOfContractNumberRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EndOfContractNumberRequirement requirement)
        {
           var endOfContractNumber = DateTime.Parse(context.User.FindFirst(c => c.Type == "EndOfContractNumber").Value);

            if(endOfContractNumber < DateTime.Now)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

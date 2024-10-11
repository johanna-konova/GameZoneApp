using Microsoft.AspNetCore.Identity;

namespace Microsoft.Models.DependencyInjection
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }
    }
}

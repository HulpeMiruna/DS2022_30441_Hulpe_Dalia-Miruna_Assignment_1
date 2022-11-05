using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EnergyPlatform.Repository.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EnergyPlatformProgram.BusinessLogic.Implementations
{
    public class UserClaimsFactory : UserClaimsPrincipalFactory<UserEntity>
    {
        private readonly UserManager<UserEntity> _userManager;
        public UserClaimsFactory(
            UserManager<UserEntity> userManager,
            IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserEntity user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var roleValue = await _userManager.GetRolesAsync(user);
            identity.AddClaim(new Claim(ClaimTypes.Role, roleValue.FirstOrDefault()));

            return identity;

        }
    }
}

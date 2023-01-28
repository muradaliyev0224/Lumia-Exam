using Lumia.Models;
using Microsoft.AspNetCore.Identity;

namespace Lumia.Services
{
    public class AdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AppUser> GetCurrentAsnyc()
        {
            AppUser user = null;

            if(_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            }

            return user;
        }

    }
}

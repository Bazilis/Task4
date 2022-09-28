using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.Data;

namespace WebApp.Filters
{
    public class BlockUserFilterAsync : IAsyncResourceFilter
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlockUserFilterAsync(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var currentUser = await _userManager.GetUserAsync(context.HttpContext.User);

            if (currentUser != null && currentUser.IsBlocked)
            {
                context.ModelState.AddModelError(string.Empty, $"Unfortunately {currentUser.UserName} is blocked!");
                await _signInManager.SignOutAsync();
                context.Result = new RedirectResult("~/Home/Index");
            }

            else
                await next();
        }
    }
}

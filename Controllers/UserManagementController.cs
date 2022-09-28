using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagementController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> BlockUsers(string[] usersNames)
        {
            foreach (var userName in usersNames)
            {
                var user = await _userManager.FindByNameAsync(userName);
                user.IsBlocked = true;
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UnblockUsers(string[] usersNames)
        {
            foreach (var userName in usersNames)
            {
                var user = await _userManager.FindByNameAsync(userName);
                user.IsBlocked = false;
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUsers(string[] usersNames)
        {

            foreach (var userName in usersNames)
            {
                if(userName == User.Identity?.Name)
                    await _signInManager.SignOutAsync();
                var user = await _userManager.FindByNameAsync(userName);
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

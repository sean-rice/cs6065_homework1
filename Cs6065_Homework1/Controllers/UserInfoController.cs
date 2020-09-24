using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Cs6065_Homework1.Models;
using Cs6065_Homework1.Services;

namespace Cs6065_Homework1.Controllers
{
    [Authorize]
    public class UserInfoController : Controller
    {
        private readonly ILogger<UserInfoController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserInfoService _userInfoService;

        public UserInfoController(
            ILogger<UserInfoController> logger,
            UserManager<ApplicationUser> userManager,
            UserInfoService userInfoService)
        {
            _logger = logger;
            _userManager = userManager;
            _userInfoService = userInfoService;
        }

        public async Task<IActionResult> Index()
        {
            #nullable enable
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            UserInfo? userInfo = await _userInfoService.GetUserInfoAsync(currentUser);
            if (userInfo == null)
            {
                userInfo = new UserInfo { FirstName = string.Empty, LastName = string.Empty };
            }
            return View(userInfo);
            #nullable disable
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyUserInfo(UserInfo userInfo)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            bool success = await _userInfoService.SetUserInfoAsync(currentUser, userInfo);

            if (!success)
            {
                return BadRequest("failed to get user info!");
            }
            return RedirectToAction("Index");
        }
    }
}

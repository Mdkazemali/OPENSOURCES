// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace EMS.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {      

        private readonly IMemoryCache _cache;
        private readonly IHubContext<OnlineStatusHub> _hubContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, IMemoryCache cache, IHubContext<OnlineStatusHub> hubContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            _hubContext = hubContext;
            _cache = cache;
        }


        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out.");

            var user = await _signInManager.UserManager.GetUserAsync(User);
            if (user != null)
            {
                // ক্যাশ থেকে ইউজার লিস্ট নিন
                var loggedInUsers = _cache.GetOrCreate("LoggedInUsers", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return new List<string>();
                });

                // ইউজার রিমুভ করুন
                if (loggedInUsers.Contains(user.Email))
                {
                    loggedInUsers.Remove(user.Email);
                    _cache.Set("LoggedInUsers", loggedInUsers);
                }

                // ইউজারের অনলাইন স্ট্যাটাস ক্যাশ থেকে রিমুভ করুন
                _cache.Remove(user.Email);  // Remove user status from cache

                // SignalR দিয়ে ক্লায়েন্টকে আপডেট পাঠান
                await _hubContext.Clients.All.SendAsync("UserStatusChanged", user.Email, false);  // User status is now false (logged out)
            }

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }

    }
}

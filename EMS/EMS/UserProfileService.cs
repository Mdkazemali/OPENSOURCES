using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using EMS.Models; // আপনার ইউজার মডেলের নেমস্পেস অনুযায়ী আপডেট করুন
using Microsoft.EntityFrameworkCore;
using EMS.Data;

public interface IUserProfileService
{
    Task<string> GetUserPhotoUrlAsync();
    Task<string> GetUserNameAsync();
}

public class UserProfileService : IUserProfileService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;

    public UserProfileService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<string> GetUserPhotoUrlAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return "/noimage.png"; // ডিফল্ট ইমেজ যদি ইউজার আইডি পাওয়া না যায়
        }

        var user = await _context.UserInformation
            .Where(u => u.LoginId == userId)
            .Select(u => u.PhotoUrl)
            .FirstOrDefaultAsync();

        return user ?? "/noimage.png"; // যদি ইউজার ফটো না থাকে, ডিফল্ট ইমেজ দেখাবে
    }

    public async Task<string> GetUserNameAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

        var user = await _context.UserInformation
            .Where(u => u.LoginId == userId)
            .Select(u => u.UserFullName)
            .FirstOrDefaultAsync();

        return user ?? "User Name"; // যদি ইউজার ফটো না থাকে, ডিফল্ট ইমেজ দেখাবে
    }
}

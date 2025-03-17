using EMS.Data;
using EMS.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace EMS
{
    public class OnlineStatusHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private static readonly Dictionary<string, string> OnlineUsers = new();

        public OnlineStatusHub(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(userId))
            {
                lock (OnlineUsers)
                {
                    OnlineUsers[userId] = Context.ConnectionId;
                }

                // ক্যাশে ও ডাটাবেজ আপডেট
                _cache.Set(userId, true);
                await UpdateUserStatus(userId, true);

                // সকল ক্লায়েন্টকে জানানো যে ইউজার অনলাইন হয়েছে
                await Clients.All.SendAsync("UserStatusChanged", userId, true);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(userId))
            {
                lock (OnlineUsers)
                {
                    OnlineUsers.Remove(userId);
                }

                // ক্যাশে ও ডাটাবেজ আপডেট
                _cache.Remove(userId);
                await UpdateUserStatus(userId, false);

                // সকল ক্লায়েন্টকে জানানো যে ইউজার অফলাইন হয়েছে
                await Clients.All.SendAsync("UserStatusChanged", userId, false);
            }

            await base.OnDisconnectedAsync(exception);
        }

        private async Task UpdateUserStatus(string userId, bool isOnline)
        {
            var userStatus = _context.UserInformation.FirstOrDefault(u => u.LoginId == userId);
            if (userStatus == null)
            {
                userStatus = new UserInformation { LoginId = userId, IsOnline = isOnline, LastUpdated = DateTime.Now };
                _context.UserInformation.Add(userStatus);
            }
            else
            {
                userStatus.IsOnline = isOnline;
                userStatus.LastUpdated = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }

        public static List<string> GetOnlineUsers()
        {
            return OnlineUsers.Keys.ToList();
        }



        //// ✅ 3. SendSignal: WebRTC সিগন্যাল পাঠানো
        //public async Task SendSignal(object signal, string targetUser)
        //{
        //    if (OnlineUsers.TryGetValue(targetUser, out var connectionId))
        //    {
        //        await Clients.Client(connectionId).SendAsync("ReceiveSignal", signal, Context.User.Identity.Name);
        //        Console.WriteLine($"Signal sent from {Context.User.Identity.Name} to {targetUser}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Failed to send signal: {targetUser} is not connected.");
        //    }
        //}

        //// ✅ 4. EndCall: কল শেষ করার জন্য সিগন্যাল পাঠানো
        //public async Task EndCall(string targetUserId)
        //{
        //    if (OnlineUsers.TryGetValue(targetUserId, out var connectionId))
        //    {
        //        await Clients.Client(connectionId).SendAsync("CallEnded");
        //        Console.WriteLine($"Call ended notification sent to {targetUserId}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Failed to end call: {targetUserId} is not connected.");
        //    }
        //}

        //// ✅ 5. SendMessageToUser: এক ব্যবহারকারী থেকে অন্য ব্যবহারকারীকে মেসেজ পাঠানো
        //public async Task SendMessageToUser(string targetUserName, string user, string message)
        //{
        //    if (OnlineUsers.TryGetValue(targetUserName, out var targetConnectionId) &&
        //        OnlineUsers.TryGetValue(user, out var senderConnectionId))
        //    {
        //        // টার্গেট ইউজারকে মেসেজ পাঠানো
        //        await Clients.Client(targetConnectionId).SendAsync("ReceiveMessage", targetUserName, message);
        //        // প্রেরককে কনফার্মেশন মেসেজ পাঠানো
        //        //await Clients.Client(senderConnectionId).SendAsync("ReceiveMessage", targetUserName, message);
        //        Console.WriteLine($"Message sent from {user} to {targetUserName}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Failed to send message: Either {user} or {targetUserName} is not connected.");
        //    }
        //}


    }
}





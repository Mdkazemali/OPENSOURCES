using EMS.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace EMS
{
    public class UserStatusService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public UserStatusService(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        //for second pages
        //public List<object> GetAllUsersStatus(string category)
        //{
        //    var users = from A in _context.UserInformat
        //                where B.ExapairDate >= DateTime.Now && C.Ocupations == category && B.Confirmation=="Active"
        //                select new
        //                {
        //                    A.LoginId,
        //                    A.UserFullName,
        //                    A.LastUpdated,
        //                    C.Ocupations,
        //                    A.PhotoUrl,
        //                };

        //         return users.AsEnumerable()
        //                .Select(u => new
        //                {
        //                    UserId = u.LoginId,
        //                    Name = u.UserFullName,
        //                    Dateon = u.LastUpdated,
        //                    Ocupations = u.Ocupations,
        //                    PhotoUrl = u.PhotoUrl,
        //                    IsOnline = _cache.TryGetValue(u.LoginId, out _)
        //                })
        //                .ToList<object>();
        //}


    }
}

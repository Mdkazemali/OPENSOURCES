using EMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserInformation> UserInformation { get; set; }

        public virtual DbSet<FacilityRegistry> FacilityRegistry { get; set; }
        public object UserInformations { get; internal set; }
        public virtual DbSet<Trainingvideo> Trainingvideo { get; set; }
    }
}

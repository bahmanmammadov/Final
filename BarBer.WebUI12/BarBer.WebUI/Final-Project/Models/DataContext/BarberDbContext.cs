using Final_Project.Models.Entity;
using Final_Project.Models.Entity.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Final_Project.Models.DataContext
{
    public class BarberDbContext : IdentityDbContext<BarberUser, BarberRole, int, BarberUserClaim, BarberUserRole, BarberUserLogin, BarberRoleClaim, BarberUserToken>
    {
        public BarberDbContext()
            : base()
        {

        }
        public BarberDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Gallery> Gallerys { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Subscribe> subscribes { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }





        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BarberUser>(e =>
            {
                e.ToTable("User", "Membership");
            });
            builder.Entity<BarberRole>(e =>
            {
                e.ToTable("Role", "Membership");
            });
            builder.Entity<BarberRoleClaim>(e =>
            {
                e.ToTable("RoleClaim", "Membership");
            });
            builder.Entity<BarberUserClaim>(e =>
            {
                e.ToTable("UserClaim", "Membership");
            });
            builder.Entity<BarberUserLogin>(e =>
            {
                e.HasKey(p => new { p.ProviderKey, p.LoginProvider });

                e.ToTable("UserLogin", "Membership");
            });
            builder.Entity<BarberUserToken>(e =>
            {
                e.ToTable("UserToken", "Membership");
            });
            builder.Entity<BarberUserRole>(e =>
            {
                e.ToTable("UserRole", "Membership");
            });
        }
        internal object UserClaims(Func<object, object> p)
        {
            throw new NotImplementedException();
        }


    }
}

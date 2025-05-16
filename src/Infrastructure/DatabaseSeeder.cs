using Domain.Entities.Core;
using Domain.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DatabaseSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IHttpContextAccessor httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            DbContextOptions dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<Database>>();

            using Database dbContext = new(dbContextOptions, configuration, httpContextAccessor);

            PopulateDb(dbContext);
        }

        public static void PopulateDb(this ModelBuilder builder)
        {
            //builder.Entity<Address>().HasData(_ADDRESS);
            builder.Entity<Claim>().HasData(_CLAIMS);
            builder.Entity<Role>().HasData(_ROLES);
            builder.Entity<ApplicationUser>().HasData(_APP_USERS);
            builder.Entity<UserClaim>().HasData(_USER_CLAIMS);
            builder.Entity<UserRole>().HasData(_USER_ROLES);
        }

        public static void PopulateDb(Database database)
        {
            //database.Addresses.AddRange(_ADDRESS);
            database.Roles.AddRange(_ROLES);
            database.Claims.AddRange(_CLAIMS);
            database.ApplicationUsers.AddRange(_APP_USERS);
            database.UserClaims.AddRange(_USER_CLAIMS);
            database.UserRoles.AddRange(_USER_ROLES);

            database.SaveChanges();
        }

        public static readonly List<Claim> _CLAIMS =
        [
            new Claim(ClaimType.FullAccess){ Id = 1},
            new Claim(ClaimType.ManageUsers){ Id = 2}
        ];

        public static readonly List<Role> _ROLES =
        [
            new Role(RoleType.Administrator){ Id = 1},
            new Role(RoleType.Business){ Id = 2},
            new Role(RoleType.Client){ Id = 3}
        ];

        public static readonly List<ApplicationUser> _APP_USERS =
        [
            new ApplicationUser(
                "admin@email.hr",
                BCrypt.Net.BCrypt.HashPassword("admin"),
                "Admin User",
                CultureType.Croatian) { Id = 1 },
            new ApplicationUser(
                "client@email.hr",
                BCrypt.Net.BCrypt.HashPassword("client"),
                "Client User",
                CultureType.English) { Id = 2 },
            new ApplicationUser(
                "bussiness@email.hr",
                BCrypt.Net.BCrypt.HashPassword("bussiness"),
                "Business User",
                CultureType.English) { Id = 3 },
        ];

        public static readonly List<UserClaim> _USER_CLAIMS =
        [
            new UserClaim
            {
                UserId = _APP_USERS[1].Id,
                ClaimId = _CLAIMS[1].Id
            }
        ];

        public static readonly List<UserRole> _USER_ROLES =
        [
            new UserRole()
            {
                UserId = _APP_USERS[0].Id,
                RoleId = _ROLES[0].Id
            },
             new UserRole()
            {
                UserId = _APP_USERS[0].Id,
                RoleId = _ROLES[1].Id
            },
             new UserRole()
            {
                UserId = _APP_USERS[0].Id,
                RoleId = _ROLES[2].Id
            },
            new UserRole()
            {
                UserId = _APP_USERS[2].Id,
                RoleId = _ROLES[1].Id
            },
            new UserRole()
            {
                UserId = _APP_USERS[1].Id,
                RoleId = _ROLES[2].Id
            }
        ];

    }
}


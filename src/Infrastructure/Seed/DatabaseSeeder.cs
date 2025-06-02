using Domain.Entities.Core;
using Domain.Entities.Security;
using Domain.Enumerations;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.Seed
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

        private static readonly DateTime _createdOn = DateTime.UtcNow;
        private const string _createdBy = "Migration";

        public static void PopulateDb(this ModelBuilder builder)
        {
            TranslationSeeder.SeedTranslations(builder);

            builder.Entity<CultureType>().HasData(
                new { Id = 1, Value = "hr-HR", Type = CultureTypeEnum.Croatian, CreatedBy = _createdBy, CreatedOn = _createdOn },
                new { Id = 2, Value = "en-US", Type = CultureTypeEnum.English, CreatedBy = _createdBy, CreatedOn = _createdOn }
            );

            builder.Entity<Claim>().HasData(
               new
               {
                   Id = 1,
                   Type = ClaimTypeEnum.FullAccess,
                   Name = "FullAccess",
                   CreatedBy = _createdBy,
                   CreatedOn = _createdOn
               },
               new
               {
                   Id = 2,
                   Type = ClaimTypeEnum.ManageUsers,
                   Name = "ManageUsers",
                   CreatedBy = _createdBy,
                   CreatedOn = _createdOn
               }
            );

            builder.Entity<Role>().HasData(
                new
                {
                    Id = 1,
                    Type = RoleTypeEnum.Administrator,
                    Name = "Administrator",
                    CreatedBy = _createdBy,
                    CreatedOn = _createdOn
                },
                new
                {
                    Id = 2,
                    Type = RoleTypeEnum.Business,
                    Name = "Business",
                    CreatedBy = _createdBy,
                    CreatedOn = _createdOn
                },
                new
                {
                    Id = 3,
                    Type = RoleTypeEnum.Client,
                    Name = "Client",
                    CreatedBy = _createdBy,
                    CreatedOn = _createdOn
                }
            );

            builder.Entity<User>(user =>
            {
                user.HasData(new
                {
                    Id = 1,
                    Email = "admin@email.hr",
                    Telephone = "+385955535353",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                    Name = "Admin User",
                    FullName = "Administrator User",
                    CultureTypeId = 1,
                    CreatedBy = _createdBy,
                    CreatedOn = _createdOn
                },
                new
                {
                    Id = 2,
                    Email = "client@email.hr",
                    Telephone = "+385955585353",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("client"),
                    Name = "Client User",
                    FullName = "Client User",
                    CultureTypeId = 2,
                    CreatedBy = _createdBy,
                    CreatedOn = _createdOn
                },
                new
                {
                    Id = 3,
                    Email = "bussiness@email.hr",
                    Telephone = "+385956685353",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("bussiness"),
                    Name = "Business User",
                    FullName = "Business user",
                    CultureTypeId = 2,
                    CreatedBy = _createdBy,
                    CreatedOn = _createdOn
                });

                user.OwnsOne(c => c.Address).HasData(
                   new
                   {
                       UserId = 1,
                       Street = "Ilica 10",
                       Country = "Croatia",
                       City = "Zagreb",
                       PostalCode = "10000"
                   },
                   new
                   {
                       UserId = 2,
                       Street = "Ilica 255",
                       Country = "Croatia",
                       City = "Zagreb",
                       PostalCode = "10000"
                   },
                   new
                   {
                       UserId = 3,
                       Street = "Taborska 15",
                       Country = "Croatia",
                       City = "Zagreb",
                       PostalCode = "10000"
                   }
               );
            });

            builder.Entity<UserClaim>().HasData(
               new { UserId = 2, ClaimId = 2, CreatedBy = _createdBy, CreatedOn = _createdOn }
             );

            builder.Entity<UserRole>().HasData(
                new { UserId = 1, RoleId = 1, CreatedBy = _createdBy, CreatedOn = _createdOn },
                new { UserId = 1, RoleId = 2, CreatedBy = _createdBy, CreatedOn = _createdOn },
                new { UserId = 1, RoleId = 3, CreatedBy = _createdBy, CreatedOn = _createdOn },
                new { UserId = 3, RoleId = 2, CreatedBy = _createdBy, CreatedOn = _createdOn },
                new { UserId = 2, RoleId = 3, CreatedBy = _createdBy, CreatedOn = _createdOn }
            );
        }

        public static void PopulateDb(Database database)
        {
           
        }  
    }
}


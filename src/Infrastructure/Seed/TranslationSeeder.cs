using Domain.Entities.Translation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed
{
    internal static class TranslationSeeder
    {
        private static readonly DateTime _createdOn = DateTime.UtcNow;
        private const string _createdBy = "Migration";

        public static void SeedTranslations(this ModelBuilder builder)
        {
            builder.Entity<CultureTypeTranslation>().ToTable(nameof(CultureTypeTranslation), Schemas.TRANSLATION);
            builder.Entity<RoleTranslation>().ToTable(nameof(RoleTranslation), Schemas.TRANSLATION);
            builder.Entity<ClaimTranslation>().ToTable(nameof(ClaimTranslation), Schemas.TRANSLATION);

            builder.Entity<CultureTypeTranslation>().HasData(
                new
                {
                    Id = Guid.NewGuid(),
                    CultureTypeId = 1,
                    CultureName = "hr-HR",
                    Content = "Hrvatski",
                    CreatedBy = _createdBy,
                    CreatedOn = _createdOn
                },
                new
                {
                    Id = Guid.NewGuid(),
                    CultureTypeId = 2,
                    CultureName = "en-US",
                    Content = "English",
                    CreatedBy = _createdBy,
                    CreatedOn = _createdOn
                }
            );

            builder.Entity<RoleTranslation>().HasData(
               new
               {
                   Id = Guid.NewGuid(),
                   RoleId = 1,
                   CultureName = "hr-HR",
                   Content = "Administrator",
                   CreatedBy = _createdBy,
                   CreatedOn = _createdOn
               },
               new
               {
                   Id = Guid.NewGuid(),
                   RoleId = 1,
                   CultureName = "en-US",
                   Content = "Administrator",
                   CreatedBy = _createdBy,
                   CreatedOn = _createdOn
               },
               new
               {
                   Id = Guid.NewGuid(),
                   RoleId = 1,
                   CultureName = "hr-HR",
                   Content = "Bussiness",
                   CreatedBy = _createdBy,
                   CreatedOn = _createdOn
               },
               new
               {
                   Id = Guid.NewGuid(),
                   RoleId = 1,
                   CultureName = "en-US",
                   Content = "Poslovni",
                   CreatedBy = _createdBy,
                   CreatedOn = _createdOn
               },
               new
               {
                   Id = Guid.NewGuid(),
                   RoleId = 3,
                   CultureName = "hr-HR",
                   Content = "Klijent",
                   CreatedBy = _createdBy,
                   CreatedOn = _createdOn
               },
               new
               {
                   Id = Guid.NewGuid(),
                   RoleId = 3,
                   CultureName = "en-US",
                   Content = "Client",
                   CreatedBy = _createdBy,
                   CreatedOn = _createdOn
               }
           );

            builder.Entity<ClaimTranslation>().HasData(
               new
               {
                   Id = Guid.NewGuid(),
                   ClaimId = 1,
                   CultureName = "hr-HR",
                   Content = "Puno pravo",
                   CreatedBy = _createdBy,
                   CreatedOn = _createdOn
               },
               new
               {
                   Id = Guid.NewGuid(),
                   ClaimId = 1,
                   CultureName = "en-US",
                   Content = "Full access",
                   CreatedBy = _createdBy,
                   CreatedOn = _createdOn
               },
               new
               {
                    Id = Guid.NewGuid(),
                    ClaimId = 2,
                    CultureName = "hr-HR",
                    Content = "Upravljanje korisnicima",
                    CreatedBy = _createdBy,
                    CreatedOn = _createdOn
               },
               new
               {
                   Id = Guid.NewGuid(),
                   ClaimId = 2,
                   CultureName = "en-US",
                   Content = "Managing users",
                   CreatedBy = _createdBy,
                   CreatedOn = _createdOn
               }
           );
        }
    }
}


using Domain;
using Domain.Abstraction;
using Domain.Entities.Core;
using Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class Database(DbContextOptions options, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : DbContext(options)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<Claim> Claims { get; set; } = null!;
        public DbSet<UserClaim> UserClaims { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string? connectionString = _configuration?.GetConnectionString("DatabaseKey");
                optionsBuilder.EnableSensitiveDataLogging().UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Database).Assembly);
            modelBuilder.PopulateDb();
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            string userIdentifier = AppConstants.SYSTEM;
            bool isAuthenticated = _httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

            if (isAuthenticated)
            {
                IUserCredentials user = PrincipalHelper.ToUserCredentials(_httpContextAccessor?.HttpContext?.User!);
                userIdentifier = user.Id.ToString();
            }

            var entities = ChangeTracker.Entries()
                .Where(e => e.Entity is AudiableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entities)
            {
                var entity = (AudiableEntity)entityEntry.Entity;
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entity.CreatedOn = DateTime.UtcNow;
                        entity.CreatedBy = userIdentifier;
                        break;

                    case EntityState.Modified:
                        entity.ModifiedOn = DateTime.UtcNow;
                        entity.ModifiedBy = userIdentifier;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

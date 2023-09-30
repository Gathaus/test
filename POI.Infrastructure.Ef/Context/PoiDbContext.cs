using System.Reflection;
using Microsoft.EntityFrameworkCore;
using POI.Domain.Entities;

namespace POI.Infrastructure.Ef.Context
{
    public class PoiDbContext : DbContext
    {
        #region Constructors

        public PoiDbContext(DbContextOptions<PoiDbContext> options)
            : base(options)
        {

        }

        #endregion

        #region DbSets
        public DbSet<PoiDistance> PoiDistances { get; set; }
        public DbSet<PoiCatalog> PoiCatalogs { get; set; }
        public DbSet<Demography> Demographies { get; set; }
        public DbSet<PoiSalesPointMatchHistory> PoiSalesPointMatchHistories { get; set; }
        #endregion

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseLazyLoadingProxies(false);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // RegisterDbSets(builder);
            ApplyConfigurationsFromAssembly(builder);
        }
        
        
        private void ApplyConfigurationsFromAssembly(ModelBuilder builder)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            var configurations = currentAssembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(i => i.IsGenericType
                                                       && i.GetGenericTypeDefinition() ==
                                                       typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var configuration in configurations)
            {
                dynamic configurationInstance = Activator.CreateInstance(configuration)!;
                builder.ApplyConfiguration(configurationInstance);
            }
        }
        
        private void RegisterDbSets(ModelBuilder modelBuilder)
        {
            var dbSetProperties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            foreach (var prop in dbSetProperties)
            {
                var entityType = prop.PropertyType.GetGenericArguments().First();
                modelBuilder.Entity(entityType);
            }
        }


        #endregion
    }
}

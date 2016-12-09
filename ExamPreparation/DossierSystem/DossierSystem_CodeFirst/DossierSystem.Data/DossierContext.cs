namespace DossierSystem.Data
{
    using DossierSystem.Data.Migrations;
    using DossierSystem.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DossierContext : DbContext
    {        
        public DossierContext()
            : base("name=DossierContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DossierContext, Configuration>());
        }

        public virtual IDbSet<Activity> Activities { get; set; }

        public virtual IDbSet<Individual> Individuals { get; set; }

        public virtual IDbSet<Location> Locations { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<ActivityType> ActivityTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Individual>()
                .HasMany(i => i.RelatedIndividuals)
                .WithMany()
                .Map(map =>
                {
                    map.MapLeftKey("IndividualId");
                    map.MapRightKey("RelatedId");
                    map.ToTable("RelatedIndividuals");
                });
        }
    }
}
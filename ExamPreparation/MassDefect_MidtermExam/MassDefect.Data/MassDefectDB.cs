namespace MassDefect.Data
{
    using Models;
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Linq;

    public class MassDefectDB : DbContext
    {        
        public MassDefectDB()
            : base("name=MassDefectDB")
        {
            Database.SetInitializer<MassDefectDB>(new CreateDatabaseIfNotExists<MassDefectDB>());
        }      
        
        public virtual IDbSet<Anomaly>  Anomalies { get; set; }
        public virtual IDbSet<Person> Persons { get; set; }
        public virtual IDbSet<Planet> Planets { get; set; }
        public virtual IDbSet<SolarSystem> SolarSystems { get; set; }
        public virtual IDbSet<Star> Stars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomaly>()
                .HasMany(anomaly => anomaly.Victims)
                .WithMany(person => person.Anomalies)
                .Map(entity =>
                {
                    entity.ToTable("AnomalyVictims");
                    entity.MapLeftKey("AnomalyId");
                    entity.MapRightKey("PersonId");
                });
        }
    }
}
namespace _6.PhonebookCodeFirst
{
    using Migrations;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhonebookContext : DbContext
    {        
        public PhonebookContext()
            : base("name=PhonebookContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhonebookContext, Configuration>());
        }

        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<Channel> Channels { get; set; }
        public virtual IDbSet<ChannelMessage> ChannelMessages { get; set; }
        public virtual IDbSet<UserMessage> UserMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMessage>()
                .HasRequired(x => x.SenderUser)
                .WithMany(x => x.SentUserMessages)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserMessage>()
                .HasRequired(x => x.RecipientUser)
                .WithMany(x => x.RecievedUserMessages)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
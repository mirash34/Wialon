namespace Wialion_Forms
{
    using System.Data.Entity;
    public partial class DbOption : DbContext
    {
        public DbOption()
            : base("name=ContextFromDB")
        {
        }

        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<UserOptions> UserOptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Groups>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Units>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<UserOptions>()
                .Property(e => e.Login)
                .IsFixedLength();

            modelBuilder.Entity<UserOptions>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<UserOptions>()
                .Property(e => e.UserName)
                .IsFixedLength();

            modelBuilder.Entity<UserOptions>()
                .Property(e => e.Proxy)
                .IsFixedLength();

            modelBuilder.Entity<UserOptions>()
                .Property(e => e.Directory)
                .IsFixedLength();
        }
    }
}

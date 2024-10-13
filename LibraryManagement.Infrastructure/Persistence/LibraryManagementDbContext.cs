using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence
{
    public class LibraryManagementDbContext : DbContext
    {

        public LibraryManagementDbContext() { }

        public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options)
            : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Book>(e => {
                e.HasKey(b => b.Id);
            });

            builder.Entity<User>(e => {
                e.HasKey(u => u.Id);

                e.HasMany(u => u.Loans)
                    .WithOne(u => u.User)
                    .HasForeignKey(u => u.IdUser)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Loan>(e => { 
                e.HasKey(l => l.Id);

                e.HasOne(l => l.User)
                    .WithMany(u => u.Loans)
                    .HasForeignKey(l => l.IdUser)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(l => l.Book)
                    .WithMany(b => b.Loans)
                    .HasForeignKey(l => l.IdBook)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(builder);
        }

    }
}

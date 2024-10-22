using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Persistence.EntityConfig
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loans");
            builder.HasKey(l => l.Id);

            builder.Property(x => x.DateOfLoan).HasColumnType("date");
            builder.Property(x => x.EndDateLoan).HasColumnType("date");
            builder.Property(x => x.ReturnDate).HasColumnType("date");
            builder.Property(x => x.CreatedAt).HasColumnType("date");
            builder.Property(x => x.DaysOfDelay).HasColumnType("int");

            builder.HasOne(l => l.User)
                   .WithMany(u => u.Loans)
                   .HasForeignKey(l => l.IdUser)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Book)
                   .WithMany(b => b.Loans)
                   .HasForeignKey(l => l.IdBook)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}

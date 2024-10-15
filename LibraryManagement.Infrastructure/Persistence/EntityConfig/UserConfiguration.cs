using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Persistence.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);

            builder.HasMany(u => u.Loans)
                   .WithOne(u => u.User)
                   .HasForeignKey(u => u.IdUser)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new {Id = 1, Name = "Cliente1", Email = "cliente1@teste.com.br", CreatedAt = DateTime.Now, IsDeleted = false },
                new {Id = 2, Name = "cliente2", Email = "cliente2@teste.com.br", CreatedAt = DateTime.Now, IsDeleted = false }
            );

        }
    }
}

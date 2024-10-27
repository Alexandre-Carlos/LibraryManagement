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
            builder.HasIndex(u => u.Email);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Salt).HasMaxLength(1000).IsRequired();

            builder.HasMany(u => u.Loans)
                   .WithOne(u => u.User)
                   .HasForeignKey(u => u.IdUser)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new {Id = 1, Name = "Cliente_1", Email = "cliente1@teste.com.br", Password = "$HASH|V1$10000$hkJQltolSmXA86IJW76J46fST28x1inv1NDGHrmlMKlBVN2c", Salt = "hkJQltolSmXA86IJW76J4w==", CreatedAt = DateTime.Now, IsDeleted = false },
                new {Id = 2, Name = "cliente_2", Email = "cliente2@teste.com.br", Password = "$HASH|V1$10000$GDDUHWb1no2cn7BGXZGQ22J4IqpTX5Ng8bH+fV12BhnFl0CV", Salt = "GDDUHWb1no2cn7BGXZGQ2w==", CreatedAt = DateTime.Now, IsDeleted = false }
            );

        }
    }
}

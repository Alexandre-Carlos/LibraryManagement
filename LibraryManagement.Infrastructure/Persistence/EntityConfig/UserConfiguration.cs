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
                new User("Cliente1","cliente1@teste.com.br" ),
                new User("cliente2","cliente2@teste.com.br" )
            );

        }
    }
}

using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Persistence.EntityConfig
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(b => b.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Author).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Isbn).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Quantity).HasColumnType("int");
            builder.Property(x => x.CreatedAt).HasColumnType("date");

            builder.HasMany(u => u.Loans)
               .WithOne(x => x.Book)
               .HasForeignKey(x => x.IdBook)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new { Id = 1, Title = "Código Limpo: Habilidades Práticas do Agile Software", Author = "Robert C. Martin", Isbn = "978-8576082675", YearPublished = 2009, Quantity = 5, IsDeleted = false, CreatedAt = DateTime.Now},
                new { Id = 2, Title = "Arquitetura Limpa: o Guia do Artesão Para Estrutura e Design de Software", Author = "Robert C. Martin", Isbn = "978-8550804606", YearPublished = 2019, Quantity = 3, IsDeleted = false, CreatedAt = DateTime.Now},
                new { Id = 3, Title = "Entendendo Algoritmos: Um Guia Ilustrado Para Programadores e Outros Curiosos", Author = " Aditya Y. Bhargava", Isbn = "978-8575225639", YearPublished = 2019, Quantity = 8, IsDeleted = false, CreatedAt = DateTime.Now}
            );
        }
    }
}

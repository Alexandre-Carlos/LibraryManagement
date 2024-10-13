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
            builder.Property(x => x.CreatedAt).HasColumnType("date");


            builder.HasData(
                new Book("Código Limpo: Habilidades Práticas do Agile Software", "Robert C. Martin", "978-8576082675", 2009, 5),
                new Book("Arquitetura Limpa: o Guia do Artesão Para Estrutura e Design de Software", "Robert C. Martin", "978-8550804606", 2019, 3),
                new Book("Entendendo Algoritmos: Um Guia Ilustrado Para Programadores e Outros Curiosos", " Aditya Y. Bhargava", "978-8575225639", 2019, 8)
            );
        }
    }
}

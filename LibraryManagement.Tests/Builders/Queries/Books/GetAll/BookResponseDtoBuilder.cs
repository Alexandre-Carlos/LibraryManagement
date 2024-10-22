using Bogus;
using LibraryManagement.Application.Dtos.Books;

namespace LibraryManagement.Tests.Builders.Queries.Books.GetAll
{
    public class BookResponseDtoBuilder
    {
        public readonly Faker<BookResponseDto> instance;

        public BookResponseDtoBuilder()
        {
            instance = new AutoFaker<BookResponseDto>();
        }



        public BookResponseDto Build() => instance.Generate();
    }
}

using LibraryManagement.Api.Entities;

namespace LibraryManagement.Api.Dtos.Users
{
    public class UserResponseDto
    {
        public UserResponseDto(string name, string email)
        {
            Name = name;
            Email = email;   
        }

        public UserResponseDto(string name, string email, List<Book> books)
        {
            Name = name;
            Email = email;
            Books = books;
        }

        public string Name { get; set; }
        public string Email { get; set; }

        public List<Book> Books { get; set; }

        public static UserResponseDto FromEntity(User user) => new UserResponseDto(user.Name, user.Email, user.Books);

    }
}

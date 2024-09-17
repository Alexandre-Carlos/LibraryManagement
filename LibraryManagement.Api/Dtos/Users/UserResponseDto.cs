using LibraryManagement.Api.Entities;

namespace LibraryManagement.Api.Dtos.Users
{
    public class UserResponseDto
    {
        public UserResponseDto(string name, string email, List<Loan> loans)
        {
            Name = name;
            Email = email;
            Loans = loans;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public List<Loan> Loans { get; set; }


        public static UserResponseDto FromEntity(User user) => new UserResponseDto(user.Name, user.Email, user.Loans);

    }
}

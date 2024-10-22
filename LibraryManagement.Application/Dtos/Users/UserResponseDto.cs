using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.Dtos.Users
{
    public class UserResponseDto
    {
        public UserResponseDto()
        {
            
        }
        public UserResponseDto(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        public static UserResponseDto FromEntity(User user) => new UserResponseDto(user.Id, user.Name, user.Email);

    }
}

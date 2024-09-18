using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.Dtos.Users
{
    public class UserRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }


        public User ToEntity() => new(Name, Email);
    }
}

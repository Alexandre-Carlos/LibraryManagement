namespace LibraryManagement.Api.Entities
{
    public class User : BaseEntity
    {


        public User(string name, string email, string password) : base()
        {
            Name = name;
            Email = email;
            Password = password;
            Loans = [];
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public List<Loan> Loans { get; private set; }
    }
}

namespace LibraryManagement.Core.Entities
{
    public class AuthBase : BaseEntity
    {
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}

﻿namespace LibraryManagement.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string email) : base()
        {
            Name = name;
            Email = email;
            Loans = [];
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public List<Loan> Loans { get; private set; }


        public void SetName(string name) => Name = name;

        public void SetEmail(string email) => Email = email;  

        public void Update(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}

using Bogus;

namespace LibraryManagement.Tests
{
    public class AutoFaker<T> : Faker<T> where T : class
    {
        public AutoFaker() : base("pt_BR")
        {
            RuleForType(typeof(Guid), faker => faker.Random.Guid())
            .RuleForType(typeof(string), faker => faker.Random.Word())
            .RuleForType(typeof(DateTime), faker => faker.Date.Past());
        }
    }
}

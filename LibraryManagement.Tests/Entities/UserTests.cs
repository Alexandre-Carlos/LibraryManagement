using FluentAssertions;
using LibraryManagement.Core.Entities;
using LibraryManagement.Tests.Builders.Entities;

namespace LibraryManagement.Tests.Entities
{
    public class UserTests
    {
        [Fact]
        public void Update_UserDataIsOk_Success()
        {
            var name = "Alexandre C";
            var email = "emailcorreto@email.com.br";

            var user = new UserBuilder().Build();

            user.Update(name, email);

            user.Name.Should().BeEquivalentTo(name);
            user.Email.Should().BeEquivalentTo(email);
        }

        [Fact]
        public void Change_UserEmail_Success()
        {
            var email = "emailcorreto@email.com.br";

            var user = new UserBuilder().Build();

            user.SetEmail(email);

            user.Email.Should().BeEquivalentTo(email);

        }

        [Fact]
        public void Change_UserName_Success()
        {
            var name = "Alexandre C";

            var user = new UserBuilder().Build();

            user.SetName(name);

            user.Name.Should().BeEquivalentTo(name);

        }

        [Fact]
        public void Check_UserIsDeletecOk_Success()
        {
            var user = new UserBuilder().Build();

            user.SetAsDeleted();

            user.IsDeleted.Should().BeTrue();

        }
    }
}

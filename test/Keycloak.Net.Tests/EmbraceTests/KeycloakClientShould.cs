using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Keycloak.Net.Tests
{
    public partial class KeycloakClientShould
    {
        [Theory]
        [Trait("Category", "EmbraceTests")]
        [InlineData("master")]
        public async Task CreateUserAsync(string realm)
        {

            var user = new Models.Users.User()
            {
                UserName = "testun1",
                Enabled = true,
                EmailVerified = true,
                FirstName = "testfirstname1",
                LastName = "testlastname1",
                Email = "testemail@e.ee"
            };

            var result = await _client.CreateUserAsync(realm, user);
            Assert.NotNull(result);
        }

    }
}

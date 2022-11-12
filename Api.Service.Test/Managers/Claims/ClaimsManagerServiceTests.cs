using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Api.Domain.DTOs;
using Api.Domain.Enums;
using Api.Service.Managers;

namespace Api.Service.Test.Managers.Claims
{
    public class ClaimsManagerServiceTests : BaseTest
    {
        [Theory(DisplayName = "Should extract information from claims")]
        [InlineData((int)EnumUserType.ADMIN, true, false, false, false)]
        [InlineData((int)EnumUserType.COMMERCE, false, true, false, false)]
        [InlineData((int)EnumUserType.TEACHER, false, false, true, false)]
        [InlineData((int)EnumUserType.STUDENT, false, false, false, true)]
        public void ShouldExtractInformationFromClaims(
            int userType,
            bool expectedIsAdmin,
            bool expectedIsCommerce,
            bool expectedIsTeacher,
            bool expectedIsStudent
        )
        {
            // Given
            var studentId = new Random().Next(1000, 9999).ToString();
            var userId = Guid.NewGuid();
            var claims = new ClaimsIdentity(
                new GenericIdentity(studentId),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, studentId),
                    new Claim("Name", "A mocked name"),
                    new Claim("UserType", userType.ToString()),
                    new Claim("id", userId.ToString()),
                }
            );
            var payload = new ClaimsPrincipal(claims);

            var manager = new ClaimsManager();

            // When
            UserSessionDTO result = manager.GetUserSession(payload);

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedIsAdmin, result.IsAdmin);
            Assert.Equal(expectedIsCommerce, result.IsCommerce);
            Assert.Equal(expectedIsTeacher, result.IsTeacher);
            Assert.Equal(expectedIsStudent, result.IsStudent);
            Assert.Equal(userId, result.UserId);
            Assert.Equal((EnumUserType)userType, result.UserType);
        }
    }
}
using Api.Domain.DTO;
using Api.Domain.DTOs;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Transfer
{
    public class TransferIntegrationTests : BaseTest
    {
        private Guid FromId;
        private Guid ToId;

        [Fact(DisplayName = "")]
        public async void ShouldTestTransferFlow()
        {
            var from = await InsertAnUser();
            var to = await InsertAnUser();

            await SignInWithUser(from);

            FromId = from.Id;
            ToId = to.Id;

            await ShouldCallTransferToUser();
        }

        async Task ShouldCallTransferToUser()
        {
            var payload = new TransferToUserRequestDTO()
            {
                ToId = ToId,
                Total = 15m,
            };

            var result = await PostJsonAsync(payload, "/api/transfers", Client);
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<BaseResultDTO>(resultContent);

            Assert.NotNull(resultObj);
            Assert.True(resultObj.IsValid);
            Assert.Empty(resultObj.Message);
        }
    }
}
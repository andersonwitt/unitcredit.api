using Api.Domain.DTOs;
using Api.Domain.Stubs;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Transaction
{
    public class TransactionIntegrationTests : BaseTest
    {

        [Fact(DisplayName = "Should test transactions flow")]
        public async void ShouldTestTransactionsFlow()
        {
            var transaction = await ShouldCallPostToInsertATransaction();

            await ShouldCallGetAllTransaction();

            await ShouldCallGetTransactionById(transaction.Id);

            await ShouldCallUpdateTransaction(transaction);

            await ShouldCallDeleteTransaction(transaction.Id);
        }

        public async Task ShouldCallGetAllTransaction()
        {
            var result = await Client.GetAsync($"/api/transactions");
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<List<TransactionDTO>>(resultContent);

            Assert.NotEmpty(resultObj);
        }

        public async Task ShouldCallGetTransactionById(Guid transactionId)
        {
            var result = await Client.GetAsync($"/api/transactions/{transactionId}");
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<TransactionDTO>(resultContent);

            Assert.NotNull(resultObj);
        }


        public async Task<TransactionDTO> ShouldCallPostToInsertATransaction()
        {
            var from = await InsertAnUser();
            var to = await InsertAnUser();

            var payload = TransactionStubs.GetTransactionDTO();
            payload.FromId = from.Id;
            payload.ToId = to.Id;

            var result = await PostJsonAsync(payload, "/api/transactions", Client);
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<TransactionDTO>(resultContent);

            Assert.NotNull(resultObj);

            return resultObj;
        }

        public async Task ShouldCallDeleteTransaction(Guid transactionId)
        {
            var result = await Client.DeleteAsync($"/api/transactions/{transactionId}");
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<bool>(resultContent);

            Assert.True(resultObj);
        }

        public async Task ShouldCallUpdateTransaction(TransactionDTO transaction)
        {
            var payload = TransactionStubs.GetTransactionDTO();
            payload.Id = transaction.Id;
            payload.FromId = transaction.FromId;
            payload.ToId = transaction.ToId;
            payload.Description = "an Updated Name";
            payload.Total = 235m;

            var result = await PutJsonAsync(payload, "/api/transactions", Client);
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<TransactionDTO>(resultContent);

            Assert.NotNull(resultObj);
            Assert.NotEqual(resultObj.Description, transaction.Description);
            Assert.NotEqual(resultObj.Total, transaction.Total);
            Assert.Equal(payload.Description, resultObj.Description);
            Assert.Equal(payload.Total, resultObj.Total);
        }
    }
}
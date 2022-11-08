using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test.Transaction
{
    public class TransactionDataUnitTests : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;
        private TransactionEntity _entity;
        private TransactionImplementation _repository;

        public TransactionDataUnitTests(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
            _entity = new TransactionEntity()
            {
                CreateAt = DateTime.UtcNow,
                Description = Faker.Company.CatchPhrase(),
                Total = 135m,
                Type = Domain.Enums.EnumTransactionType.Credit,
            };
        }

        [Fact(DisplayName = "Transaction Crud")]
        [Trait("CRUD", "TransactionEntity")]
        public async void Should_Do_A_CRUD_Operation()
        {
            using (var context = _serviceProvider.GetService<UnitContext>())
            {
                _repository = new TransactionImplementation(context);
                var users = await context.Users.ToListAsync();

                _entity.FromId = users.First().Id;
                _entity.ToId = users.Skip(1).First().Id;

                await InsertTest();

                await UpdateTest();

                await ExistsTest();

                await SelectTest();

                await SelectByUserIdTest();

                await SelectAllTest();

                await RemoveTest();
            }
        }

        private async Task InsertTest()
        {
            var _dataCreated = await _repository.InsertAsync(_entity);

            ObjectAsserts<TransactionEntity, TransactionEntity>(_entity, _dataCreated, new List<string>());

            Assert.False(_dataCreated.Id == Guid.Empty);
        }

        private async Task UpdateTest()
        {
            _entity.Total = 435m;
            _entity.Description = Faker.Company.CatchPhrase();
            _entity.Type = Domain.Enums.EnumTransactionType.SaleOrPayment;

            var _dataUpdated = await _repository.UpdateAsync(_entity);

            ObjectAsserts<TransactionEntity, TransactionEntity>(_entity, _dataUpdated, new List<string>());

            _entity = _dataUpdated;
        }

        private async Task ExistsTest()
        {
            var _dataExists = await _repository.ExistAsync(_entity.Id);
            Assert.True(_dataExists);
        }

        private async Task SelectTest()
        {
            var _dataSelected = await _repository.SelectAsync(_entity.Id);

            ObjectAsserts<TransactionEntity, TransactionEntity>(_entity, _dataSelected, new List<string>());
        }

        private async Task SelectByUserIdTest()
        {
            var _dataSelected = await _repository.SelectByUserId(_entity.FromId);

            Assert.NotEmpty(_dataSelected);
            Assert.False(_dataSelected.Count() > 1);
        }

        private async Task SelectAllTest()
        {
            var _allData = await _repository.SelectAsync();
            Assert.NotEmpty(_allData);
            Assert.False(_allData.Count() > 1);
        }

        private async Task RemoveTest()
        {
            var _removed = await _repository.DeleteAsync(_entity.Id);
            Assert.True(_removed);
        }
    }
}
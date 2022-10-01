using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.DTOs;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test.User
{
    public class UserDataUnitTests : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvide;
        private UserEntity _entity;
        private UserImplementation _repository;
        public UserDataUnitTests(DbTest dbTest)
        {
            _serviceProvide = dbTest.ServiceProvider;
            _entity = new UserEntity()
            {
                CreateAt = DateTime.UtcNow,
                Email = Faker.Internet.Email(),
                Name = Faker.Name.First(),
                Password = new Random().Next(1000, 9999).ToString(),
                StudentId = new Random().Next(1000, 9999).ToString(),
            };
        }

        [Fact(DisplayName = "User Crud")]
        [Trait("CRUD", "UserEntity")]
        public async Task Should_Do_A_CRUD_Operation()
        {
            using (var context = _serviceProvide.GetService<UnitContext>())
            {
                _repository = new UserImplementation(context);

                await InsertTest();

                await UpdateTest();

                await ExistsTest();

                await SelectTest();

                await SelectAllTest();

                await SelectByLoginTest();

                await RemoveTest();
            }
        }


        private async Task InsertTest()
        {
            var _dataCreated = await _repository.InsertAsync(_entity);

            ObjectAsserts<UserEntity, UserEntity>(_entity, _dataCreated, new List<string>());

            Assert.False(_dataCreated.Id == Guid.Empty);
        }

        private async Task UpdateTest()
        {
            _entity.Name = Faker.Name.First();
            _entity.Password = new Random().Next(1000, 9999).ToString();
            _entity.Email = Faker.Internet.Email();

            var _dataUpdated = await _repository.UpdateAsync(_entity);

            ObjectAsserts<UserEntity, UserEntity>(_entity, _dataUpdated, new List<string>());

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

            ObjectAsserts<UserEntity, UserEntity>(_entity, _dataSelected, new List<string>());
        }

        private async Task SelectByLoginTest()
        {
            var payload = new LoginPayloadDTO()
            {
                Password = _entity.Password,
                StudentId = _entity.StudentId,
            };

            var _selectedByLogin = await _repository.SelectByLogin(payload);

            Assert.NotNull(_selectedByLogin);
            ObjectAsserts<UserEntity, UserEntity>(_entity, _selectedByLogin, new List<string>());
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
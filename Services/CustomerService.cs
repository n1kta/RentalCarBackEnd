using System.Collections.Generic;
using System.Threading.Tasks;
using RentalCar.Contracts;
using RentalCar.Dtos;
using RentalCar.Entity;
using RentalCar.Repository;

namespace RentalCar.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IBaseRepositoryAsync _baseRepositoryAsync;

        public CustomerService(IBaseRepositoryAsync baseRepositoryAsync)
        {
            _baseRepositoryAsync = baseRepositoryAsync;
        }

        public async Task<CustomerDto> GetById(int id)
        {
            var model = await _baseRepositoryAsync.GetById<Customer>(id);
            var result = new CustomerDto
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Phone = model.Phone
            };

            return result;
        }

        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var models = await _baseRepositoryAsync.GetAll<Customer>();
            var result = new List<CustomerDto>();

            foreach (var model in models)
            {
                result.Add(new CustomerDto
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Phone = model.Phone
                });
            }

            return result;
        }

        public Task Create(CustomerDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(int id, CustomerDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
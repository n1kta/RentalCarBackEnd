using System.Collections.Generic;
using System.Threading.Tasks;
using RentalCar.Contracts;
using RentalCar.Dtos;
using RentalCar.Entity;
using RentalCar.Repository;

namespace RentalCar.Services
{
    public class ReturnService : IReturnService
    {
        private readonly IBaseRepositoryAsync _baseRepositoryAsync;

        public ReturnService(IBaseRepositoryAsync baseRepositoryAsync)
        {
            _baseRepositoryAsync = baseRepositoryAsync;
        }

        public Task<ReturnDto> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ReturnDto>> GetAll()
        {
            var models = await _baseRepositoryAsync.GetAll<Return>();
            var result = new List<ReturnDto>();

            foreach (var model in models)
            {
                result.Add(new ReturnDto
                {
                    RentalId = model.RentalId,
                    InTime = model.InTime,
                    ReturnDate = model.ReturnDate
                });
            }

            return result;
        }

        public Task Create(ReturnDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(int id, ReturnDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
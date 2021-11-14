using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentalCar.Contracts;
using RentalCar.Dtos;
using RentalCar.Entity;
using RentalCar.Repository;

namespace RentalCar.Services
{
    public class RentalService : IRentalService
    {
        private readonly IBaseRepositoryAsync _baseRepositoryAsync;

        public RentalService(IBaseRepositoryAsync baseRepositoryAsync)
        {
            _baseRepositoryAsync = baseRepositoryAsync;
        }

        public Task<RentalDto> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<RentalDto>> GetAll()
        {
            var models = await _baseRepositoryAsync.GetWithIncludeAsync<Rental>(x => true, m => m.Car, m => m.Customer);
            var result = new List<RentalDto>();

            foreach (var model in models)
            {
                result.Add(new RentalDto
                {
                    Car = $"{model.Car.Brand} ({model.Car.Model})",
                    Customer = $"{model.Customer.FirstName} {model.Customer.LastName}",
                    PickUpDate = model.PickUpDate,
                    DropOffDate = model.DropOffDate,
                    Fee = model.Fee
                });
            }

            return result;
        }

        public async Task Create(RentalCreateDto dto)
        {
            var model = new Rental
            {
                CarId = dto.CarId,
                CustomerId = dto.CustomerId,
                DropOffDate = dto.DropOffDate,
                PickUpDate = dto.PickUpDate,
                PickUpLoc = dto.PickUpLoc,
                DropOffLoc = dto.DropOffLoc,
                Fee = dto.Fee,
                CreatedDate = DateTime.Now,
                Return = null
            };

            await _baseRepositoryAsync.Create(model);
        }

        public Task Update(int id, RentalDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<RentalByCustomerDto>> GetByCustomerId(int customerId)
        {
            var models = await _baseRepositoryAsync.GetWithIncludeAsync<Rental>(x => x.CustomerId == customerId, m => m.Car, m => m.Return);
            var result = new List<RentalByCustomerDto>();

            foreach (var model in models)
            {
                result.Add(new RentalByCustomerDto
                {
                    RentalId = model.Id,
                    Car = $"{model.Car.Brand} ({model.Car.Model})",
                    Photo = model.Car.ImageUrl,
                    DropOffDate = model.DropOffDate,
                    PickUpDate = model.PickUpDate,
                    DropOffLoc = model.DropOffLoc,
                    PickUpLoc = model.PickUpLoc,
                    IsReturned = model.Return != null
                });
            }

            return result;
        }

        public async Task ReturnRental(int rentalId)
        {
            var model = await _baseRepositoryAsync.GetById<Rental>(rentalId);
            if (model != null)
            {
                var currentDateTime = DateTime.Now;
                var returnModel = new Return
                {
                    RentalId = model.Id,
                    ReturnDate = currentDateTime,
                    CreatedDate = currentDateTime,
                    InTime = model.PickUpDate.Date <= currentDateTime && model.DropOffDate >= currentDateTime
                };

                await _baseRepositoryAsync.Create<Return>(returnModel);
            }
            else
            {
                throw new Exception("Error");
            }
        }
    }
}
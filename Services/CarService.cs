using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using RentalCar.Contracts;
using RentalCar.Dtos;
using RentalCar.Entity;
using RentalCar.Repository;

namespace RentalCar.Services
{
    public class CarService : ICarService
    {
        private readonly IBaseRepositoryAsync _baseRepositoryAsync;
        private readonly IFileUploader _fileUploader;

        public CarService(IBaseRepositoryAsync baseRepositoryAsync,
            IFileUploader fileUploader)
        {
            _baseRepositoryAsync = baseRepositoryAsync;
            _fileUploader = fileUploader;
        }

        public async Task<CarDto> GetById(int id)
        {
            var model = await _baseRepositoryAsync.GetById<Car>(id);
            var result = new CarDto
            {
                Id = model.Id,
                Brand = model.Brand,
                Model = model.Model,
                PricePerHour = model.PricePerHour,
                Available = model.Available,
                Fuel = model.Fuel,
                Year = model.Year,
                Photo = model.ImageUrl
            };

            return result;
        }

        public async Task<IEnumerable<CarDto>> GetAll()
        {
            var models = await _baseRepositoryAsync.GetAll<Car>();
            var result = new List<CarDto>();

            foreach (var model in models)
            {
                result.Add(new CarDto
                {
                    Id = model.Id,
                    Brand = model.Brand,
                    Model = model.Model,
                    PricePerHour = model.PricePerHour,
                    Available = model.Available,
                    Fuel = model.Fuel,
                    Year = model.Year,
                    Photo = model.ImageUrl
                });
            }

            return result;
        }

        public async Task<IEnumerable<CarDto>> GetAvailable(FilterDto dto)
        {
            var rental = new List<Rental>();

            if (dto.DropOffDate != null && dto.PickUpDate != null)
            {
                rental = (await _baseRepositoryAsync.Fetch<Rental>(x =>
                    dto.PickUpDate >= x.PickUpDate && dto.PickUpDate < x.DropOffDate)).ToList();
            }
            
            var cars = default(IEnumerable<Car>);

            if (rental.Any())
            {
                var notFilteredCars = await _baseRepositoryAsync.GetAll<Car>();
                cars = notFilteredCars.Where(x => rental.All(r => r.CarId != x.Id));
            }
            else
            {
                cars = await _baseRepositoryAsync.GetAll<Car>();
            }
            
            var result = new List<CarDto>();
            
            foreach (var car in cars)
            {
                result.Add(new CarDto
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    PricePerHour = car.PricePerHour,
                    Available = car.Available,
                    Fuel = car.Fuel,
                    Year = car.Year,
                    Photo = car.ImageUrl
                });
            }
            
            return result;
        }

        public async Task Create(CarDto dto)
        {
            var result = new Car
            {
                Brand = dto.Brand,
                Model = dto.Model,
                PricePerHour = dto.PricePerHour,
                Available = dto.Available,
                Fuel = dto.Fuel,
                Year = dto.Year,
                Rentals = null,
                ImageUrl = _fileUploader.Upload(dto.Photo),
                CreatedDate = DateTime.Now
            };

            await _baseRepositoryAsync.Create(result);
        }

        public async Task Update(int id, CarDto dto)
        {
            var model = await _baseRepositoryAsync.GetById<Car>(id);
            model.Brand = dto.Brand;
            model.Model = dto.Model;
            model.PricePerHour = dto.PricePerHour;
            model.Available = dto.Available;
            model.Fuel = dto.Fuel;
            model.Year = dto.Year;
            if (!string.IsNullOrWhiteSpace(dto.Photo) && !dto.Photo.Contains("https://localhost:44374"))
            {
                model.ImageUrl = _fileUploader.Upload(dto.Photo);
            }

            _baseRepositoryAsync.Update(model);
        }

        public async Task Delete(int id)
        {
            await _baseRepositoryAsync.Delete<Car>(id);
        }

        
    }
}

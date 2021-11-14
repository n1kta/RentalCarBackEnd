using System.Collections.Generic;
using System.Threading.Tasks;
using RentalCar.Dtos;

namespace RentalCar.Contracts
{
    public interface ICarService
    {
        Task<CarDto> GetById(int id);
        Task<IEnumerable<CarDto>>GetAll();
        Task<IEnumerable<CarDto>> GetAvailable(FilterDto dto);
        Task Create(CarDto dto);
        Task Update(int id, CarDto dto);
        Task Delete(int id);
    }
}

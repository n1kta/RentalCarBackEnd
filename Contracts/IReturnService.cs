using System.Collections.Generic;
using System.Threading.Tasks;
using RentalCar.Dtos;

namespace RentalCar.Contracts
{
    public interface IReturnService
    {
        Task<ReturnDto> GetById(int id);
        Task<IEnumerable<ReturnDto>> GetAll();
        Task Create(ReturnDto dto);
        Task Update(int id, ReturnDto dto);
        Task Delete(int id);
    }
}
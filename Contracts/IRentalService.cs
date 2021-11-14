using System.Collections.Generic;
using System.Threading.Tasks;
using RentalCar.Dtos;

namespace RentalCar.Contracts
{
    public interface IRentalService
    {
        Task<RentalDto> GetById(int id);
        Task<IEnumerable<RentalDto>> GetAll();
        Task Create(RentalCreateDto dto);
        Task Update(int id, RentalDto dto);
        Task Delete(int id);
        Task<IEnumerable<RentalByCustomerDto>> GetByCustomerId(int customerId);
        Task ReturnRental(int rentalId);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using RentalCar.Dtos;

namespace RentalCar.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetById(int id);
        Task<IEnumerable<CustomerDto>> GetAll();
        Task Create(CustomerDto dto);
        Task Update(int id, CustomerDto dto);
        Task Delete(int id);
    }
}
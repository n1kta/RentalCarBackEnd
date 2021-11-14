using System.Threading.Tasks;
using RentalCar.Dtos;
using RentalCar.Models;

namespace RentalCar.Contracts
{
    public interface IUserService
    {
        Task<ResponseModel> RegisterAsync(RegisterDto dto);
        Task<AuthenticationDto> GetTokenAsync(TokenDto model);
        Task<CustomerDto> GetCurrentCustomer(string token);
        Task<bool> IsAdmin(string token);
        Task UpdateCustomer(int customerId, CustomerUpdateDto dto);
    }
}

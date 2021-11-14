using System.Collections.Generic;

namespace RentalCar.Dtos
{
    public class AuthenticationDto
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string Token { get; set; }
    }
}

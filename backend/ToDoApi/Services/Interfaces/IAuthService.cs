using ToDoApi.Models.DTOs;

namespace ToDoApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> ValidateRegistrationAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
    }
}

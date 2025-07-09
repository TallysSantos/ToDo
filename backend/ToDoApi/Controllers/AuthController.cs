using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Entities;
using ToDoApi.Models.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using ToDoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using ToDoApi.Exceptions;

namespace ToDoApi.Controllers;

[Authorize]
[ApiController]
[Route("Api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto dto)
    {
        try
        {
            string returnMessage = await _authService.ValidateRegistrationAsync(dto);
            return StatusCode(201, new { Message = returnMessage });
        }
        catch (AuthenticationException ex)
        {
            return StatusCode(400, new { Message = new { Error = ex.Message } });
        }
        catch (Exception ex)
        {
            return StatusCode(400, new { Message = new { Error = ex.Message } });
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        try
        {
            string token = await _authService.LoginAsync(dto);
            return StatusCode(200, new { Message = new { Token = token } });
        }
        catch (AuthenticationException ex)
        {
            return StatusCode(401, new { Message = new { Error = ex.Message } });
        }
        catch (Exception ex)
        {
            return StatusCode(400, new { Message = new { Error = ex.Message } });
        }
    }
}

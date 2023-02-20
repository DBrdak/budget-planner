using API.Auth;
using API.Auth.DTOs;
using Application.Interfaces;
using Domain;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;

        public UserController(DataContext context, UserManager<User> userManager, TokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.NormalizedEmail == dto.Email.ToUpper());

            if (user == null)
                return BadRequest();

            var success = await _userManager
                .CheckPasswordAsync(user, dto.Password);

            if (success)
                return CreateUserObject(user);

            return BadRequest();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var usernameIsUnique = await _context.Users.AnyAsync(u => u.UserName == dto.Username);
            var emailIsUnique = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            var budgetIsUnique = await _context.Budgets.AnyAsync(b => b.Name == dto.Username);

            if (usernameIsUnique)
            {
                ModelState.AddModelError("username", "Username taken");
                return ValidationProblem();
            }

            if (emailIsUnique)
            {
                ModelState.AddModelError("email", "Email taken");
                return ValidationProblem();
            }

            if (budgetIsUnique)
            {
                ModelState.AddModelError("budgetName", "Budget name taken");
                return ValidationProblem();
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Passwords must be equal");
                return ValidationProblem();
            }

            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email,
                DisplayName = dto.DisplayName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var budget = new Budget
            {
                Name = dto.BudgetName,
                User = user
            };

            await _context.AddAsync(budget);
            var fail = await _context.SaveChangesAsync() < 0;

            if (fail)
                return BadRequest("Problem while creating budget");

            return CreateUserObject(user);
        }

        [Authorize] // Autoryzacja jedynie właściciela konta
        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email == User.FindFirstValue(ClaimTypes.Email));

            return CreateUserObject(user);
        }

        private UserDto CreateUserObject(User user)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            };
        }
    }
}
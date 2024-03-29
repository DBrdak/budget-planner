﻿using System.ComponentModel;
using System.Security.Claims;
using API.Auth;
using API.Auth.DTOs;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

/// <summary>
/// Set of HTTP requests for user authorization
/// </summary>

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    private readonly TokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public UserController(DataContext context, UserManager<User> userManager, TokenService tokenService)
    {
        _context = context;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [Description("Logging in an user")]
    public async Task<ActionResult<UserDto>> Login(LoginDto dto)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.NormalizedEmail == dto.Email.ToUpper()).ConfigureAwait(false);

        if (user == null)
            return BadRequest();

        var success = await _userManager
            .CheckPasswordAsync(user, dto.Password).ConfigureAwait(false);

        if (success)
            return CreateUserObject(user);

        return BadRequest();
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [Description("Signing up an user")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
    {
        var usernameIsUnique = await _context.Users.AnyAsync(u => u.UserName == dto.Username).ConfigureAwait(false);
        var emailIsUnique = await _context.Users.AnyAsync(u => u.Email == dto.Email).ConfigureAwait(false);
        var budgetIsUnique = await _context.Budgets.AnyAsync(b => b.Name.ToUpper() == dto.BudgetName.ToUpper())
            .ConfigureAwait(false);

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

        var result = await _userManager.CreateAsync(user, dto.Password).ConfigureAwait(false);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var budget = new Budget
        {
            Name = dto.BudgetName,
            User = user
        };

        await _context.AddAsync(budget).ConfigureAwait(false);
        var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

        if (fail)
            return BadRequest("Problem while creating budget");

        return CreateUserObject(user);
    }

    [Authorize]
    [HttpGet("user")]
    [Description("Gets current instance of user")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == User.FindFirstValue(ClaimTypes.Email))
            .ConfigureAwait(false);

        return CreateUserObject(user);
    }

    private UserDto CreateUserObject(User user)
    {
        return new UserDto
        {
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user),
            Username = user.UserName,
            BudgetName = _context.Budgets
                .FirstOrDefault(u => u.UserId == user.Id).Name
        };
    }
}
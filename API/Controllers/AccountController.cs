using API.DTOs;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController(SignInManager<User> signInManager): BaseApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
   public async Task<ActionResult<string>> Register(RegisterUserDto registerUserDto)
    {
        var user = new User
        {
            UserName = registerUserDto.Email,
            Email = registerUserDto.Email,
            DisplayName = registerUserDto.DisplayName

        };
      var result = await signInManager.UserManager.CreateAsync(user, registerUserDto.Password);
        if (result.Succeeded)
        {
            return Ok("User created successfully");
        };
        foreach (var error in result.Errors) { 
            ModelState.AddModelError(error.Code, error.Description);

        };

        return ValidationProblem(ModelState);

    }
    [AllowAnonymous]
    [HttpGet("user-info")]
    public async Task<ActionResult> GetCurrentUser()
    {
        if(User.Identity.IsAuthenticated == false) return NoContent();

        var user= await signInManager.UserManager.GetUserAsync(User);
        if (user == null) return Unauthorized();
        return Ok(new
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            ImageUrl = user.ImageUrl
        });
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok("User logged out successfully");
    }

}

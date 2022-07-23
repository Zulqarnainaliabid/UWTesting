using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Upwork.Testing.Common;
using Upwork.Testing.Data.Abstractions;
using Upwork.Testing.Data.DTOs.Auth;
using Upwork.Testing.Data.Models.Auth;

namespace Upwork.Testing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailService _emailService;
        private readonly JwtSettings _jwtSettings;
        private readonly EmailConfiguration _configuration;
        private readonly IDataProtector _dataProtector;

        public AuthController(
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IEmailService emailService,
            IOptionsSnapshot<JwtSettings> jwtSettings,
            EmailConfiguration configuration,
            IDataProtectionProvider dataProtectionProvider)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
            _configuration = configuration;
            _dataProtector = dataProtectionProvider.CreateProtector(new DataProtectionTokenProviderOptions().Name);
            //_userManager.UserValidator = new UserValidator<User>(UserManager<User>) { AllowOnlyAlphanumericUserNames = false }
        }

        private string GetUserIdFromResetToken(string token)
        {
            var resetTokenArray = Convert.FromBase64String(token);
            var unprotectedResetTokenArray = _dataProtector.Unprotect(resetTokenArray);
            string result = null;
            using (var ms = new MemoryStream(unprotectedResetTokenArray))
            {
                using (var reader = new BinaryReader(ms))
                {
                    // Read off the creation UTC timestamp
                    reader.ReadInt64();

                    // Then you can read the userId!
                    result = reader.ReadString();

                }
            }
            return result;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(UserSignUpDto userSignUp)
        {
            try
            {
                var user = _mapper.Map<UserSignUpDto, User>(userSignUp);
                user.UserName = user.Email;
                var userCreateResult = await _userManager.CreateAsync(user, userSignUp.Password);

                if (userCreateResult.Succeeded)
                {
                    return Created("User Successfuly created", string.Empty);
                }

                return Problem(userCreateResult.Errors.First().Description, null, 500);
            }
            catch (Exception exp)
            {
                return Problem(exp.Message, null, 500);
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserLoginDto userLoginResource)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginResource.Email);
            if (user is null)
            {
                return NotFound("User not found");
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);

            if (userSigninResult)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(new { User = user, Token = GenerateJwt(user, roles) });
            }

            return BadRequest("Email or password incorrect.");
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordDto userForgotPasswordResource)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userForgotPasswordResource.Email);
                if (user is null)
                {
                    return NotFound("User not found");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                ///email sending code to send the token to the user email
                var body = $"{_configuration.ResetPasswordPagePath}?token={HttpUtility.UrlEncode(token)}";

                await _emailService.SendEmailAsync(_configuration.FromEmail, userForgotPasswordResource.Email, "Reset Password link", body);
                return Problem("Error on forgot password", null, 500);
            }
            catch (Exception exp)
            {
                return Problem(exp.Message, null, 500);
            }
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordResource resetPasswordResource)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordResource.Email);
                if (user is null)
                {
                    return NotFound("User not found");
                }
                var result = await _userManager.ResetPasswordAsync(user, resetPasswordResource.Token, resetPasswordResource.Password);
                if (result.Succeeded)
                    return Ok("Password changed successfuly");
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception exp)
            {
                return BadRequest(exp);
            }

        }

        [HttpPost("Roles")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name should be provided.");
            }

            var newRole = new Role
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return Ok();
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }

        [HttpPost("User/{userEmail}/Role")]
        public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userEmail);

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Problem(result.Errors.First().Description, null, 500);
        }

        private string GenerateJwt(User user, IList<string> roles)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
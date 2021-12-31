using Authentication.Configuration;
using Authentication.Models.DTO.Incoming;
using Authentication.Models.DTO.Outgoing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.IConfiguration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;

namespace WebApplication.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AccountsController(
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionMonitor)
            : base(unitOfWork)
        {
            _userManager = userManager;
            _jwtConfig = optionMonitor.CurrentValue;
        }

        //Register action
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDTO registrationDTO)
        {
            // check the model or obj we are receiving is valid
            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByEmailAsync(registrationDTO.Email);

                if (userExists != null) // email already in the table
                {
                    return BadRequest(new UserRegistrationResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> //doublecheck this
                        {
                            "Email already in use"
                        }
                    });
                }

                // add the user
                var newUser = new IdentityUser()
                {
                    Email = registrationDTO.Email,
                    UserName = registrationDTO.Email,
                    EmailConfirmed = true // todo build email functionality to send to the user to confirm email
                };

                // adding the user to the db
                var isCreated = await _userManager.CreateAsync(newUser, registrationDTO.Password);
                if (!isCreated.Succeeded) // registration failed
                {
                    return BadRequest(new UserRegistrationResponseDTO()
                    {
                        Success = isCreated.Succeeded,
                        Errors = isCreated.Errors.Select(x => x.Description).ToList()
                    });
                }

                // create jwt
                var token = GenerateJWT(newUser);

                // return jwt to the user
                return Ok(new UserRegistrationResponseDTO()
                {
                    Success = true,
                    Token = token
                });
            }
            else // invalid object
            {
                return BadRequest(new UserRegistrationResponseDTO
                {
                    Success = false,
                    Errors = new List<string> //doublecheck this
                    {
                        "Invalid payload"
                    }
                });
            }
        }

        private string GenerateJWT(IdentityUser user)
        {
            // the handler is responsible for creating the token
            var jwtHandler = new JwtSecurityTokenHandler();

            // get the security key
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            // define the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email), //unique id
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // used by the refresh token
                }),
                Expires = DateTime.UtcNow.AddHours(3), // todo update expiration time to minutes
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // review the algorithm
            };

            // create the security obj token
            var token = jwtHandler.CreateToken(tokenDescriptor);

            // convert the security obj token into string
            var jwtToken = jwtHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
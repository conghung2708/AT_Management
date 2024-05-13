using AT_Management.Models.Domain;
using AT_Management.Models.DTO;
using AT_Management.Repositories;
using AT_Management.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AT_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        //Regiser
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerRequestDTO.Username,
                    Email = registerRequestDTO.Username,
                    PhoneNumber = registerRequestDTO.PhoneNumber,
                    FullName = registerRequestDTO.FullName,
                    PosId = registerRequestDTO.PosId
                };

                var identityResult = await _userManager.CreateAsync(user, registerRequestDTO.Password);

                if (identityResult.Succeeded)
                {
                    //Add roles to this User
                    if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                    {
                        identityResult = await _userManager.AddToRolesAsync(user, registerRequestDTO.Roles);

                        if (identityResult.Succeeded)
                        {
                            return Ok("User was registered ! Please login");
                        }
                    }
                }
            }
           
            return BadRequest("Something was wrong");
        }

        //Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {

            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);

            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if (checkPasswordResult)
                {
                    //Get roles for this user
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //CREATE TOKEN
                        var jwtToken = _unitOfWork.TokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}


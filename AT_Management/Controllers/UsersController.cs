using AT_Management.CustomActionFilters;
using AT_Management.Models.Domain;
using AT_Management.Models.DTO;
using AT_Management.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AT_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        //GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var userDomainModels = await _unitOfWork.ApplicationUserRepository.GetAllAsync(includeProperties: "Position");

            //Map Domain Model to DTO
            var userDTOs = _mapper.Map<List<UserDTO>>(userDomainModels);

            return Ok(userDTOs);
        }

        //Get user by id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] string id)
        {
            var userDomainModel = await _unitOfWork.ApplicationUserRepository.GetAsync(u => u.Id == id, includeProperties: "Position");

            if(userDomainModel == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserDTO>(userDomainModel);

            return Ok(userDTO);
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] string id, UpdateUserRequestDTO updateUserRequestDTO)
        {
            var userDomainModel = _mapper.Map<ApplicationUser>(updateUserRequestDTO);

            if (userDomainModel == null)
            {
                return NotFound();
            }

            userDomainModel = await _unitOfWork.ApplicationUserRepository.UpdateUserAsync(id,userDomainModel);

            var userDTO = _mapper.Map<UserDTO>(userDomainModel);

            return Ok(userDTO);

        }



        [HttpGet("userinfo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserInfo()
        {
            // Retrieve the user ID from the claims in the JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            // Check if the user ID claim is present
            if (userIdClaim == null)
            {
                return NotFound("User ID claim not found in token.");
            }

            // Retrieve the user ID from the claim
            var userId = userIdClaim.Value;

            return await GetUserByIdAsync(userId);
        }
    }
}

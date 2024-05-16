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
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace AT_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private APIResponse _response;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }

        //GET ALL
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUserAsync([FromQuery] string? search, int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                IEnumerable<ApplicationUser> userDomainModel = await _unitOfWork.ApplicationUserRepository.GetAllAsync(
                    filter: null,
                    includeProperties: "Position",
                    pageSize: pageSize,
                    pageNumber: pageNumber
                );

                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    userDomainModel = userDomainModel.Where(u => u.FullName.ToLower().Contains(search));
                }

                PaginationDTO pagination = new()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItems = userDomainModel.Count()
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<UserDTO>>(userDomainModel);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(500, _response);
            }
        }

        //Get user by id
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin,Employee")]
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

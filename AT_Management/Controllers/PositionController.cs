using AT_Management.CustomActionFilters;
using AT_Management.Models.Domain;
using AT_Management.Models.DTO;
using AT_Management.Repositories.IRepositories;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace AT_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PositionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected APIResponse _response;


        public PositionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPositionAsync([FromQuery] string? search, int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Position> positionDomainModel = await _unitOfWork.PositionRepository.GetAllAsync(
                    filter: null,
                    includeProperties: null,
                    pageSize: pageSize,
                    pageNumber: pageNumber
                );

                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    positionDomainModel = positionDomainModel.Where(u => u.Name.ToLower().Contains(search));
                }

                PaginationDTO pagination = new()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItems = positionDomainModel.Count()
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<PositionDTO>>(positionDomainModel);
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


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPositionByIdAsync([FromRoute] Guid id)
        {
            var positionDomainModal = await _unitOfWork.PositionRepository.GetAsync(u => u.Id == id);

            if (positionDomainModal == null)
            {
                return NotFound();
            }
            var positionDTO = _mapper.Map<PositionDTO>(positionDomainModal);
            return Ok(positionDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreatePositionAsync([FromBody] AddPositionRequestDTO addPositionRequestDTO)
        {
            //Map DTO to Domain
            var positionDomainModel = _mapper.Map<Position>(addPositionRequestDTO);

            //Create Position
            await _unitOfWork.PositionRepository.AddAsync(positionDomainModel);

            //Save
            await _unitOfWork.SaveAsync();
            //Map Domain Model to DTO
            var positionDTO = _mapper.Map<PositionDTO>(positionDomainModel);
            return Ok(positionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePositionAsync([FromRoute] Guid id, [FromBody] UpdatePositionRequestDTO updatePositionRequestDTO)
        {
            var positionDomainModel = _mapper.Map<Position>(updatePositionRequestDTO);
            await _unitOfWork.PositionRepository.UpdateAsync(id, positionDomainModel);
            var positionDTO = _mapper.Map<PositionDTO>(positionDomainModel);

            return Ok(positionDTO);

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePositionAsync([FromRoute] Guid id)
        {
            var positionDomainModel = await _unitOfWork.PositionRepository.GetAsync(u => u.Id == id);

            await _unitOfWork.PositionRepository.RemoveAsync(positionDomainModel);


            if (positionDomainModel == null)
            {
                return NotFound();
            }
            var positionDTO = _mapper.Map<PositionDTO>(positionDomainModel);
            await _unitOfWork.SaveAsync();
            return Ok(positionDTO);
        }


    }
}

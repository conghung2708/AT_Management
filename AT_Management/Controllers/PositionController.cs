using AT_Management.CustomActionFilters;
using AT_Management.Models.Domain;
using AT_Management.Models.DTO;
using AT_Management.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AT_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PositionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPositionAsync()
        {
            var postitionDomainModels = await _unitOfWork.PositionRepository.GetAllAsync();

            //Map Domain Model to DTO
            var positionDTO = _mapper.Map<List<PositionDTO>>(postitionDomainModels);

            return Ok(positionDTO);
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

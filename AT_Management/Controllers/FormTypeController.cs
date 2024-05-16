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
    public class FormTypeController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public FormTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllFormTypeAsync([FromQuery] string? search, int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                IEnumerable<FormType> formTypeDomainModels = await _unitOfWork.FormTypeRepository.GetAllAsync(
                    filter: null,
                    includeProperties: null,
                    pageSize: pageSize,
                    pageNumber: pageNumber
                );

                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    formTypeDomainModels = formTypeDomainModels.Where(u => u.Name.ToLower().Contains(search));
                }

                PaginationDTO pagination = new()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItems = formTypeDomainModels.Count()
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<FormTypeDTO>>(formTypeDomainModels);
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
        public async Task<IActionResult> GetFormTypeByIdAsync([FromRoute] Guid id)
        {
            var formTypeDomainModal = await _unitOfWork.FormTypeRepository.GetAsync(u => u.Id == id);

            if (formTypeDomainModal == null)
            {
                return NotFound();
            }
            var formTypeDTO = _mapper.Map<FormTypeDTO>(formTypeDomainModal);
            return Ok(formTypeDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateFormTypeAsync([FromBody] AddFormTypeRequestDTO addFormTypeRequestDTO)
        {
            //Map DTO to Domain
            var formTypeDomainModel = _mapper.Map<FormType>(addFormTypeRequestDTO);

            //Create FormType
            await _unitOfWork.FormTypeRepository.AddAsync(formTypeDomainModel);

            //Save
            await _unitOfWork.SaveAsync();
            //Map Domain Model to DTO
            var formTypeDTO = _mapper.Map<FormTypeDTO>(formTypeDomainModel);
            return Ok(formTypeDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateFormTypeAsync([FromRoute] Guid id, [FromBody] UpdateFormTypeRequestDTO updateFormTypeRequestDTO)
        {
            var formTypeDomainModel = _mapper.Map<FormType>(updateFormTypeRequestDTO);
            await _unitOfWork.FormTypeRepository.Update(id, formTypeDomainModel);
            var formTypeDTO = _mapper.Map<FormTypeDTO>(formTypeDomainModel);

            return Ok(formTypeDTO);

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteFormTypeAsync([FromRoute] Guid id)
        {
            var formTypeDomainModel = await _unitOfWork.FormTypeRepository.GetAsync(u => u.Id == id);

            await _unitOfWork.FormTypeRepository.RemoveAsync(formTypeDomainModel);


            if (formTypeDomainModel == null)
            {
                return NotFound();
            }
            var formTypeDTO = _mapper.Map<FormTypeDTO>(formTypeDomainModel);
            await _unitOfWork.SaveAsync();
            return Ok(formTypeDTO);
        }


    }
}

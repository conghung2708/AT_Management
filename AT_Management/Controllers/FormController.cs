using AT_Management.CustomActionFilters;
using AT_Management.Models.Domain;
using AT_Management.Models.DTO;
using AT_Management.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Security.Claims;

namespace AT_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {

        private readonly IFormRepository _formRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FormController(IFormRepository formRepository, IHttpContextAccessor httpContextAccessor)
        {
            _formRepository = formRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        [ValidateModel]
        public async Task<IActionResult> CreateForm([FromForm] AddFormRequestDTO addFormRequestDTO)
        {
            // Get the user ID of the currently logged-in user
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Create the form
            var form = new Form
            {
                FormId = Guid.NewGuid(), // Assign a new GUID for the FormId
                UserId = userId,
                FormTypeId = addFormRequestDTO.FormTypeId, 
                Description = addFormRequestDTO.Description,
                ImageId = Guid.NewGuid() 
            };

            // Pass the form DTO and user ID to the repository for creation
            var createdForm = await _formRepository.CreateFormAsync(form, addFormRequestDTO.Image);

            return Ok(new { Message = "Form created successfully!", FormId = createdForm.FormId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllForms()
        {
            // Retrieve all forms
            var forms = await _formRepository.GetAllFormsAsync();

            return Ok(forms);
        }

        [HttpDelete("{formId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteForm(Guid formId)
        {
            // Check if the form exists
            var form = await _formRepository.GetFormByIdAsync(formId);
            if (form == null)
            {
                return NotFound();
            }

            // Delete the form
            await _formRepository.DeleteFormAsync(formId);

            return Ok(new { Message = "Form deleted successfully!" });
        }

        [HttpGet("{formId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFormById(Guid formId)
        {
            // Retrieve the form by its ID
            var form = await _formRepository.GetFormByIdAsync(formId);

            if (form == null)
            {
                return NotFound();
            }

            return Ok(form);
        }

        [HttpGet("user")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetMyForms()
        {
            // Get the user ID of the currently logged-in user
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Retrieve forms by user ID
            var forms = await _formRepository.GetMyFormsAsync(userId);

            return Ok(forms);
        }


    }
}

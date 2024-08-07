using Application.DTO;
using Application.Interfaces;
using Domain.Operation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Web.Controllers
{
    public class ProgrammingLanguageController(IProgrammingLanguageService programmingLanguageService) : AbstractController
    {
        /// <summary>
        /// Get a programming language by ID.
        /// </summary>
        /// <param name="id">The ID of the programming language.</param>
        /// <returns>The programming language details.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a programming language by ID")]
        [SwaggerResponse(200, "Programming language found", typeof(OperationResult<ProgrammingLanguageDto>))]
        [SwaggerResponse(404, "Programming language not found")]
        public async Task<IActionResult> GetProgrammingLanguageById(int id)
        {
            var result = await programmingLanguageService.GetByIdAsync(id);
            if (result.Status == OperationResultStatus.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Get a list of all programming languages.
        /// </summary>
        /// <returns>List of programming languages.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get a list of all programming languages")]
        [SwaggerResponse(200, "List of programming languages", typeof(OperationResult<IEnumerable<ProgrammingLanguageDto>>))]
        public async Task<IActionResult> ListProgrammingLanguages()
        {
            var result = await programmingLanguageService.GetListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Create a new programming language.
        /// </summary>
        /// <param name="programmingLanguageDto">The programming language details.</param>
        /// <returns>Result of the creation operation.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new programming language")]
        [SwaggerResponse(201, "Programming language created", typeof(OperationResult))]
        [SwaggerResponse(400, "Invalid input")]
        public async Task<IActionResult> CreateProgrammingLanguage([FromBody] ProgrammingLanguageDto programmingLanguageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await programmingLanguageService.CreateAsync(programmingLanguageDto);
            return CreatedAtAction(nameof(GetProgrammingLanguageById), new { id = programmingLanguageDto.Id }, result);
        }

        /// <summary>
        /// Update an existing programming language.
        /// </summary>
        /// <param name="id">The ID of the programming language.</param>
        /// <param name="programmingLanguageDto">The updated programming language details.</param>
        /// <returns>Result of the update operation.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing programming language")]
        [SwaggerResponse(200, "Programming language updated", typeof(OperationResult))]
        [SwaggerResponse(400, "Invalid input")]
        [SwaggerResponse(404, "Programming language not found")]
        public async Task<IActionResult> UpdateProgrammingLanguage(int id, [FromBody] ProgrammingLanguageDto programmingLanguageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != programmingLanguageDto.Id)
            {
                return BadRequest(OperationResult.FailCustom("Incorrect id"));
            }

            var result = await programmingLanguageService.UpdateAsync(programmingLanguageDto);
            if (result.Status == OperationResultStatus.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Delete a programming language by ID.
        /// </summary>
        /// <param name="id">The ID of the programming language.</param>
        /// <returns>Result of the deletion operation.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a programming language by ID")]
        [SwaggerResponse(200, "Programming language deleted", typeof(OperationResult))]
        [SwaggerResponse(404, "Programming language not found")]
        public async Task<IActionResult> DeleteProgrammingLanguage(int id)
        {
            var result = await programmingLanguageService.DeleteAsync(id);
            if (result.Status == OperationResultStatus.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}

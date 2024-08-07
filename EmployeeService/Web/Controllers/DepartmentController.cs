using Application.DTO;
using Application.Interfaces;
using Domain.Operation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Web.Controllers
{
    public class DepartmentController(IDepartmentService departmentService) : AbstractController
    {
        /// <summary>
        /// Get a department by ID.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        /// <returns>The department details.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a department by ID")]
        [SwaggerResponse(200, "Department found", typeof(OperationResult<DepartmentDto>))]
        [SwaggerResponse(404, "Department not found")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var result = await departmentService.GetByIdAsync(id);
            if (result.Status == OperationResultStatus.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Get a list of all departments.
        /// </summary>
        /// <returns>List of departments.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get a list of all departments")]
        [SwaggerResponse(200, "List of departments", typeof(OperationResult<IEnumerable<DepartmentDto>>))]
        public async Task<IActionResult> ListDepartments()
        {
            var result = await departmentService.GetListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Create a new department.
        /// </summary>
        /// <param name="departmentDto">The department details.</param>
        /// <returns>Result of the creation operation.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new department")]
        [SwaggerResponse(201, "Department created", typeof(OperationResult))]
        [SwaggerResponse(400, "Invalid input")]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await departmentService.CreateAsync(departmentDto);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = departmentDto.Id }, result);
        }

        /// <summary>
        /// Update an existing department.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        /// <param name="departmentDto">The updated department details.</param>
        /// <returns>Result of the update operation.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing department")]
        [SwaggerResponse(200, "Department updated", typeof(OperationResult))]
        [SwaggerResponse(400, "Invalid input")]
        [SwaggerResponse(404, "Department not found")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departmentDto.Id)
            {
                return BadRequest(OperationResult.FailCustom("Incorrect id"));
            }

            var result = await departmentService.UpdateAsync(departmentDto);
            if (result.Status == OperationResultStatus.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Delete a department by ID.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        /// <returns>Result of the deletion operation.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a department by ID")]
        [SwaggerResponse(200, "Department deleted", typeof(OperationResult))]
        [SwaggerResponse(404, "Department not found")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await departmentService.DeleteAsync(id);
            if (result.Status == OperationResultStatus.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}

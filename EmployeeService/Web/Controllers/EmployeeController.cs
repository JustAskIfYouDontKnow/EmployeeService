using Application.DTO;
using Application.Interfaces;
using Domain.Operation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Web.Controllers
{
    public class EmployeeController(IEmployeeService employeeService) : AbstractController
    {
        /// <summary>
        /// Get an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        /// <returns>The employee details.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get an employee by ID")]
        [SwaggerResponse(200, "Employee found", typeof(OperationResult<EmployeeDto>))]
        [SwaggerResponse(404, "Employee not found")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await employeeService.GetByIdAsync(id);
            if (result.Status == OperationResultStatus.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Get a list of all employees.
        /// </summary>
        /// <returns>List of employees.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get a list of all employees")]
        [SwaggerResponse(200, "List of employees", typeof(OperationResult<IEnumerable<EmployeeDto>>))]
        public async Task<IActionResult> ListEmployees()
        {
            var result = await employeeService.GetListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Create a new employee.
        /// </summary>
        /// <param name="employeeDto">The employee details.</param>
        /// <returns>Result of the creation operation.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new employee")]
        [SwaggerResponse(201, "Employee created", typeof(OperationResult))]
        [SwaggerResponse(400, "Invalid input")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await employeeService.CreateAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeDto.Id }, result);
        }

        /// <summary>
        /// Update an existing employee.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        /// <param name="employeeDto">The updated employee details.</param>
        /// <returns>Result of the update operation.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing employee")]
        [SwaggerResponse(200, "Employee updated", typeof(OperationResult))]
        [SwaggerResponse(400, "Invalid input")]
        [SwaggerResponse(404, "Employee not found")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeDto.Id)
            {
                return BadRequest(OperationResult.FailCustom("Incorrect id"));
            }

            var result = await employeeService.UpdateAsync(employeeDto);
            if (result.Status == OperationResultStatus.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Delete an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        /// <returns>Result of the deletion operation.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an employee by ID")]
        [SwaggerResponse(200, "Employee deleted", typeof(OperationResult))]
        [SwaggerResponse(404, "Employee not found")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await employeeService.DeleteAsync(id);
            if (result.Status == OperationResultStatus.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}

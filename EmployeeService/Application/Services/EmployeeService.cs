using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.StoredProcedureBase;
using Domain.Operation;
using Domain.StoredProcedure;

namespace Application.Services;

public class EmployeeService(
    IEmployeeRepository employeeRepository,
    IEmployeeStoredProcedureRepository employeeStoredProcedureRepository,
    IMapper mapper) : IEmployeeService
{
    public async Task<OperationResult<EmployeeDto>> GetByIdAsync(int id)
        => await Operation.RunAsync(async () =>
        {
            var employee = await employeeStoredProcedureRepository.GetAsync(id);

            if (employee == null)
            {
                throw new Exception($"Not found by id {id}");
            }

            return mapper.Map<EmployeeDto>(employee);
        });

    public async Task<OperationResult<IEnumerable<EmployeeDto>>> GetListAsync()
        => await Operation.RunAsync(async () =>
        {
            var employees = await employeeStoredProcedureRepository.ListAsync();
            return mapper.Map<IEnumerable<EmployeeDto>>(employees);
        });

    public async Task<OperationResult> UpdateAsync(EmployeeDto employeeDto)
        => await Operation.RunAsync(async () =>
        {
            var employee = mapper.Map<EmployeeSpResult>(employeeDto);
            var result = await employeeStoredProcedureRepository.UpdateAsync(employee);
            if (!result.IsSuccess)
            {
                throw new Exception("Something went wrong during the update");
            }

            return result;
        });

    public async Task<OperationResult> CreateAsync(EmployeeDto employeeDto)
        => await Operation.RunAsync(async () =>
        {
            var employee = mapper.Map<EmployeeSpResult>(employeeDto);
            var result = await employeeStoredProcedureRepository.CreateAsync(employee);
            if (!result.IsSuccess)
            {
                throw new Exception("Something went wrong during the create");
            }

            return result;
        });

    public async Task<OperationResult> DeleteAsync(int id)
        => await Operation.RunAsync(async () =>
        {
            var result = await employeeStoredProcedureRepository.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                throw new Exception("Something went wrong during the delete");
            }
            
            return result;
        });
}
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces.StoredProcedureBase;
using Domain.Operation;
using Domain.StoredProcedure;

namespace Application.Services;

public class DepartmentService(
    IDepartmentStoredProcedureRepository departmentStoredProcedureRepository,
    IMapper mapper) : IDepartmentService
{
    public async Task<OperationResult<DepartmentDto>> GetByIdAsync(int id)
        => await Operation.RunAsync(async () =>
        {
            var employee = await departmentStoredProcedureRepository.GetAsync(id);

            if (employee == null)
            {
                throw new Exception($"Not found by id {id}");
            }

            return mapper.Map<DepartmentDto>(employee);
        });

    public async Task<OperationResult<IEnumerable<DepartmentDto>>> GetListAsync()
        => await Operation.RunAsync(async () =>
        {
            var employees = await departmentStoredProcedureRepository.ListAsync();
            return mapper.Map<IEnumerable<DepartmentDto>>(employees);
        });

    public async Task<OperationResult> UpdateAsync(DepartmentDto employeeDto)
        => await Operation.RunAsync(async () =>
        {
            var employee = mapper.Map<DepartmentSpResult>(employeeDto);
            var result = await departmentStoredProcedureRepository.UpdateAsync(employee);
            if (!result.IsSuccess)
            {
                throw new Exception("Something went wrong during the update");
            }

            return result;
        });

    public async Task<OperationResult> CreateAsync(DepartmentDto employeeDto)
        => await Operation.RunAsync(async () =>
        {
            var employee = mapper.Map<DepartmentSpResult>(employeeDto);
            var result = await departmentStoredProcedureRepository.CreateAsync(employee);
            if (!result.IsSuccess)
            {
                throw new Exception("Something went wrong during the create");
            }

            return result;
        });

    public async Task<OperationResult> DeleteAsync(int id)
        => await Operation.RunAsync(async () =>
        {
            var result = await departmentStoredProcedureRepository.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                throw new Exception("Something went wrong during the delete");
            }
            
            return result;
        });
}
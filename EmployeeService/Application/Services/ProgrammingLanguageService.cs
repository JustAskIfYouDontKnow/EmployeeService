using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces.StoredProcedureBase;
using Domain.Operation;
using Domain.StoredProcedure;

namespace Application.Services;

public class ProgrammingLanguageService(
    IProgrammingLanguageStoredProcedureRepository programmingLanguageRepository,
    IMapper mapper) : IProgrammingLanguageService
{
    public async Task<OperationResult<ProgrammingLanguageDto>> GetByIdAsync(int id)
        => await Operation.RunAsync(async () =>
        {
            var employee = await programmingLanguageRepository.GetAsync(id);

            if (employee == null)
            {
                throw new Exception($"Not found by id {id}");
            }

            return mapper.Map<ProgrammingLanguageDto>(employee);
        });

    public async Task<OperationResult<IEnumerable<ProgrammingLanguageDto>>> GetListAsync()
        => await Operation.RunAsync(async () =>
        {
            var employees = await programmingLanguageRepository.ListAsync();
            return mapper.Map<IEnumerable<ProgrammingLanguageDto>>(employees);
        });

    public async Task<OperationResult> UpdateAsync(ProgrammingLanguageDto employeeDto)
        => await Operation.RunAsync(async () =>
        {
            var employee = mapper.Map<ProgrammingLanguageSpResult>(employeeDto);
            var result = await programmingLanguageRepository.UpdateAsync(employee);
            if (!result.IsSuccess)
            {
                throw new Exception("Something went wrong during the update");
            }

            return result;
        });

    public async Task<OperationResult> CreateAsync(ProgrammingLanguageDto employeeDto)
        => await Operation.RunAsync(async () =>
        {
            var employee = mapper.Map<ProgrammingLanguageSpResult>(employeeDto);
            var result = await programmingLanguageRepository.CreateAsync(employee);
            if (!result.IsSuccess)
            {
                throw new Exception("Something went wrong during the create");
            }

            return result;
        });

    public async Task<OperationResult> DeleteAsync(int id)
        => await Operation.RunAsync(async () =>
        {
            var result = await programmingLanguageRepository.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                throw new Exception("Something went wrong during the delete");
            }
            
            return result;
        });
}
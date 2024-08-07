using Domain.Operation;
using Application.DTO;

namespace Application.Interfaces
{
    public interface IService<T> where T : AbstractDto
    {
        Task<OperationResult<T>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<T>>> GetListAsync();
        Task<OperationResult> UpdateAsync(T dto);
        Task<OperationResult> CreateAsync(T dto);
        Task<OperationResult> DeleteAsync(int id);
    }
}
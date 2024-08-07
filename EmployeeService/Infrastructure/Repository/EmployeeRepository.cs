using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class EmployeeRepository(AppDbContext dbContext) :
    RepositoryBase<Employee>(dbContext), IEmployeeRepository { }
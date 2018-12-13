using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCApi.Domain;

namespace NCApi.Repositories
{
    public interface IABTestingRepository
    {        
        Task<bool> AddAsync(EmpABTesting entity);
        Task<IEnumerable<EmpABTesting>> GetAllAsync();
        Task<EmpABTesting> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(EmpABTesting entity);
    }
}

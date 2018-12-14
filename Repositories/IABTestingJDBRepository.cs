using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCApi.Domain;

namespace NCApi.Repositories
{
    public interface IABTestingJDBRepository
    {        
        Task<bool> AddAsync(ABTestingJDB entity);
        Task<IEnumerable<ABTestingJDB>> GetAllAsync();
        Task<ABTestingJDB> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(ABTestingJDB entity);
    }
}

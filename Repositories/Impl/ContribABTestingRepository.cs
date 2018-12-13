using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using NCApi.Common.Domain;
using NCApi.Common.Services;
using NCApi.Domain;

namespace NCApi.Repositories.Impl
{
    public class ContribABTestingRepository : ABTestingDBContext, IABTestingRepository
    {
        public ContribABTestingRepository(IOptions<ConnectionSettingList> options) : base(options) {}

        public async Task<bool> AddAsync(EmpABTesting entity)
        {
            using (Connection)
            {
                return await Connection.InsertAsync(entity) > 0;
            }
        }

        public async Task<IEnumerable<EmpABTesting>> GetAllAsync()
        {
            using (Connection)
            {
                return await Connection.GetAllAsync<EmpABTesting>();
            }
        }

        public async Task<EmpABTesting> GetByIdAsync(int id)
        {
            using (Connection)
            {
                return await Connection.GetAsync<EmpABTesting>(id);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (Connection)
            {
                var entity = await Connection.GetAsync<EmpABTesting>(id);
                return await Connection.DeleteAsync(entity);
            }
        }

        public async Task<bool> UpdateAsync(EmpABTesting entity)
        {
            using (Connection)
            {
                return await Connection.UpdateAsync(entity);
            }
        }
    }
}

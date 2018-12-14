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
    public class ContribABTestingJDBRepository : AWSDBContext, IABTestingJDBRepository
    {
        public ContribABTestingJDBRepository(IOptions<ConnectionSettingList> options) : base(options) {}

        public async Task<bool> AddAsync(ABTestingJDB entity)
        {
            using (Connection)
            {
                return await Connection.InsertAsync(entity) > 0;
            }
        }

        public async Task<IEnumerable<ABTestingJDB>> GetAllAsync()
        {
            using (Connection)
            {
                return await Connection.GetAllAsync<ABTestingJDB>();
            }
        }

        public async Task<ABTestingJDB> GetByIdAsync(int id)
        {
            using (Connection)
            {
                return await Connection.GetAsync<ABTestingJDB>(id);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (Connection)
            {
                var entity = await Connection.GetAsync<ABTestingJDB>(id);
                return await Connection.DeleteAsync(entity);
            }
        }

        public async Task<bool> UpdateAsync(ABTestingJDB entity)
        {
            using (Connection)
            {
                return await Connection.UpdateAsync(entity);
            }
        }
    }
}

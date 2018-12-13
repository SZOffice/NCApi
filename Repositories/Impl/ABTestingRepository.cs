using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using NCApi.Common.Domain;
using NCApi.Common.Services;
using NCApi.Domain;

namespace NCApi.Repositories.Impl
{
    public class ABTestingRepository : ABTestingDBContext, IABTestingRepository
    {
        public ABTestingRepository(IOptions<ConnectionSettingList> options) : base(options) {}

        public async Task<bool> AddAsync(EmpABTesting entity)
        {
            using (Connection)
            {
                string sql = @"INSERT INTO Product 
                                (Name
                                ,Quantity
                                ,Price
                                ,CategoryId)
                            VALUES
                                (@Name
                                ,@Quantity
                                ,@Price
                                ,@CategoryId)";
                return await Connection.ExecuteAsync(sql, entity) > 0;
            }
        }

        public async Task<IEnumerable<EmpABTesting>> GetAllAsync()
        {
            using (Connection)
            {
                return await Connection.QueryAsync<EmpABTesting>(@"SELECT * FROM EmpABTesting");
            }
        }

        public async Task<EmpABTesting> GetByIdAsync(int id)
        {
            using (Connection)
            {
                string sql = @"SELECT Id
                                    ,Name
                                    ,Quantity
                                    ,Price 
                                    ,CategoryId
                                FROM Product
                                WHERE Id = @Id";
                return await Connection.QueryFirstOrDefaultAsync<EmpABTesting>(sql, new { Id = id });
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (Connection)
            {
                string sql = @"DELETE FROM Product
                                WHERE Id = @Id";
                return await Connection.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }

        public async Task<bool> UpdateAsync(EmpABTesting entity)
        {
            using (Connection)
            {
                string sql = @"UPDATE EmpABTesting SET 
                                    Name = @Name
                                    ,Quantity = @Quantity
                                    ,Price= @Price
                                    ,CategoryId= @CategoryId
                               WHERE Id = @Id";
                return await Connection.ExecuteAsync(sql, entity) > 0;
            }
        }
    }
}

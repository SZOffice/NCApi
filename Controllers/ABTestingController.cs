using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NCApi.Domain;
using NCApi.Extensions;
using NCApi.Repositories;

namespace NCApi.Controllers
{
    [UserAuthorize]
    public class ABTestingController : BaseApiController
    {
        private readonly ILogger<ABTestingController> _logger;
        private readonly IABTestingRepository _aBTestingRepository;
        private readonly IABTestingJDBRepository _aBTestingJDBRepository;
        private readonly ISqlSugarABTestingRepository _sqlSugarABTestingRepository;

        public ABTestingController(ILogger<ABTestingController> logger, IABTestingRepository aBTestingRepository, IABTestingJDBRepository aBTestingJDBRepository, ISqlSugarABTestingRepository sqlSugarABTestingRepository)
        {
            _logger = logger;
            _aBTestingRepository = aBTestingRepository;
            _aBTestingJDBRepository = aBTestingJDBRepository;
            _sqlSugarABTestingRepository = sqlSugarABTestingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _aBTestingRepository.GetAllAsync();
            var dataJDB = await _aBTestingJDBRepository.GetAllAsync();
            var sugarData = _sqlSugarABTestingRepository.GetAll();
            var aBTestingPageRequest = new ABTestingPageRequest() {
                Id = 0
            };
            var pageData = _sqlSugarABTestingRepository.GetPageList(aBTestingPageRequest);
            return Ok(pageData);
        }
        
        [HttpGet]
        [CustomRoute(ApiVersions.v1, "info")]
        public IActionResult InfoV1()
        {
            var userPayload = ServiceLocator.Resolve<IApiTokenService>().GetUserPayloadByToken();
            return Ok(new { status = 1, data = userPayload });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _aBTestingRepository.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmpABTesting entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = await _aBTestingRepository.GetByIdAsync(id);
            model.ABValue = entity.ABValue;
            model.utcLastModifiedTime = DateTime.Now;
            await _aBTestingRepository.UpdateAsync(model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _aBTestingRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

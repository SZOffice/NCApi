using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NCApi.Domain;
using NCApi.Repositories;

namespace NCApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ABTestingController : ControllerBase
    {
        private readonly ILogger<ABTestingController> _logger;
        private readonly IABTestingRepository _aBTestingRepository;

        public ABTestingController(ILogger<ABTestingController> logger, IABTestingRepository aBTestingRepository)
        {
            _logger = logger;
            _aBTestingRepository = aBTestingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _aBTestingRepository.GetAllAsync();
            return Ok(data);
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

using FENGSHUIKOI.Common;
using FENGSHUIKOI.Data.Dto;
using FENGSHUIKOI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FENGSHUIKOI.APIService.Controllers
{
    [ApiController]
    [Route("SuitableObject")]
    public class SuitableObjectController : ControllerBase
    {
        private readonly ISuitableObjectService _suitableObjectService;

        public SuitableObjectController(ISuitableObjectService suitableObjectService)
        {
            _suitableObjectService = suitableObjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _suitableObjectService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _suitableObjectService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SuitableObjectDTO suitableObjectDto)
        {
            var result = await _suitableObjectService.Save(suitableObjectDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SuitableObjectDTO suitableObjectDto)
        {
            var result = await _suitableObjectService.Update(id, suitableObjectDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _suitableObjectService.DeleteById(id);
            return Ok(result);
        }
    }
}

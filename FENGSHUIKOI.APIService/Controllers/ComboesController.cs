using FENGSHUIKOI.Common;
using FENGSHUIKOI.Data.Dto;
using FENGSHUIKOI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FENGSHUIKOI.APIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComboesController : ControllerBase
    {
        private readonly IComboService _comboService;
        
        public ComboesController(IComboService comboService)
        {
            _comboService = comboService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _comboService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _comboService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ComboRequestDTO combo)
        {
            var result = await _comboService.Save(combo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ComboRequestDTO combo)
        {
            var result = await _comboService.Update(id, combo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _comboService.DeleteById(id);
            return Ok(result);
        }
    }
}

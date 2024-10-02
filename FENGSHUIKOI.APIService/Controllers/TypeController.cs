using FENGSHUIKOI.Data.Dto;
using FENGSHUIKOI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FENGSHUIKOI.APIService.Controllers
{
    [Route("api/type")]
    [ApiController]
    public class TypeController : Controller
    {
        private readonly ITypeService _typeService;

        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _typeService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _typeService.GetById(id);
            return Ok(result);
        }
    }
}

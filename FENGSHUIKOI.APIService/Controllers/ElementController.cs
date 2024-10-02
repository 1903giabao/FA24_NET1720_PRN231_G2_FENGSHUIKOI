using FENGSHUIKOI.Common;
using FENGSHUIKOI.Data.Dto;
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FENGSHUIKOI.APIService.Controllers
{
    [ApiController]
    [Route("elements")]
    public class ElementController : ControllerBase
    {
        private  IElementService _productService;

        public ElementController(IElementService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            if (result.Status == Const.SUCCESS_READ)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ElementDTO elementDTO)
        {
            var element = new Element()
            {
                Id = elementDTO.Id,
                CreatedAt = elementDTO.CreatedAt,
                CreatedBy = elementDTO.CreatedBy,
                Description = elementDTO.Description,
                ImageUrl = elementDTO.ImageUrl,
                LuckyNumbers = elementDTO.LuckyNumbers,
                Name = elementDTO.Name,
                Status = elementDTO.Status,
                TabooTime = elementDTO.TabooTime,
                UpdateBy = elementDTO.UpdateBy,
                UpdatedAt = elementDTO.UpdatedAt,
            };
            var result = await _productService.Save(element);
            return Ok(result);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ElementDTO elementDTO)
        {
            var element = new Element()
            {
                Id=elementDTO.Id,
                CreatedAt=elementDTO.CreatedAt,
                CreatedBy=elementDTO.CreatedBy,
                Description = elementDTO.Description,
                ImageUrl = elementDTO.ImageUrl,
                LuckyNumbers = elementDTO.LuckyNumbers,
                Name = elementDTO.Name,
                Status = elementDTO.Status,
                TabooTime = elementDTO.TabooTime,
                UpdateBy = elementDTO.UpdateBy,
                UpdatedAt = elementDTO.UpdatedAt,
            };
            var result = await _productService.Update(element);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _productService.DeleteById(id);
            return Ok(result);
        }
    }
}

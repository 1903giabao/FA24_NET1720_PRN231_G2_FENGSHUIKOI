using FENGSHUIKOI.Common;
using FENGSHUIKOI.Data.Dto;
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FENGSHUIKOI.APIService.Controllers
{
    [ApiController]
    [Route("ProductDetail")]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productDetailService.GetAll();
            if (result.Status == Const.SUCCESS_READ)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productDetailService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ProductDetailDTO productDetail)
        {
            var result = await _productDetailService.Save(productDetail);
            return Ok(result);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDetailDTO productDetail)
        {
          
            var result = await _productDetailService.Update(id,productDetail);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _productDetailService.DeleteById(id);
            return Ok(result);
        }
    }
}

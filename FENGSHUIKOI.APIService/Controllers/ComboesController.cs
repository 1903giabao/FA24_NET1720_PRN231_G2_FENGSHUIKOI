using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Service.Services;
using FENGSHUIKOI.Data.Dto;
using FENGSHUIKOI.Service.Base;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.AccessControl;

namespace FENGSHUIKOI.APIService.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ComboesController : ControllerBase
    {
        private readonly IComboService _comboService;

        public ComboesController(IComboService comboService)
        {
            _comboService ??= comboService;
        }

        [HttpGet("getall")]
        public async Task<IBusinessResult> GetAll()
        {
            return await _comboService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetById(int id)
        {
            return await _comboService.GetById(id);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteById(int id)
        {
            return await _comboService.DeleteById(id);
        }

        [HttpPost("create")]
        public async Task<IBusinessResult> Create([FromBody] ComboRequestDTO dto)
        {
            var obj = new Combo
            {
                Id = dto.Id,
                MemberId = dto.MemberId,
                ElementId = dto.ElementId,
                ProductDetailId = dto.ProductDetailId,
                ComboName = dto.ComboName,
                ComboPrice = dto.ComboPrice,
                Discount = dto.Discount,
                Status = dto.Status,
                CreatedBy = dto.CreatedBy,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };
            return await _comboService.Save(obj);
        }

        [HttpPut]
        public async Task<IBusinessResult> UpdateCombo(ComboRequestDTO comboDTO)
        {
            var existedCombo = await _comboService.GetById(comboDTO.Id);
            var existedData = existedCombo.Data as Combo;

            existedData.MemberId = comboDTO.MemberId;
            existedData.ElementId = comboDTO.ElementId;
            existedData.ProductDetailId = comboDTO.ProductDetailId;
            existedData.ComboName = comboDTO.ComboName;
            existedData.ComboPrice = comboDTO.ComboPrice;
            existedData.Discount = comboDTO.Discount;
            existedData.Status = comboDTO.Status;
            existedData.CreatedBy = comboDTO.CreatedBy;
            existedData.CreatedAt = comboDTO.CreatedAt;
            existedData.UpdatedAt = comboDTO.UpdatedAt;

            return await _comboService.Update(existedData);
        }
    }
}

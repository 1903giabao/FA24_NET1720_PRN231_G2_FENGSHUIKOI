using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Service.Services;
using FENGSHUIKOI.Service.Base;
using FENGSHUIKOI.Data.Dto;
using FENGSHUIKOI.Common;

namespace FENGSHUIKOI.APIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackagesController : Controller
    {
        private readonly IPackageService _packageService;

        public PackagesController(IPackageService packageService)
        {
            _packageService ??= packageService;
        }


        [HttpGet("package/search")]
        public async Task<IBusinessResult> SearchPackage(int? id, string? name, double? price, string? featureType, bool? highlight)
        {
            return await _packageService.SearchPackage(id, name, price, featureType, highlight);
        }
        [HttpGet]
        public async Task<IBusinessResult> GetAll()
        {
            return await _packageService.GetAll();
        }
        [HttpGet("package/{id}")]
        public async Task<IBusinessResult> GetById(int id)
        {
            return await _packageService.GetById(id);
        }
        [HttpPost("package")]
        public async Task<IBusinessResult> CreatePackage(PackageDto packageDto)
        {
            var newPackage = new Package
            {
                Name = packageDto.Name,
                Description = packageDto.Description,
                Price = packageDto.Price,
                StartDate = packageDto.StartDate,
                FeatureType = packageDto.FeatureType,
                EndDate = packageDto.EndDate,
                Status = packageDto.Status,
                Discount = packageDto.Discount,
                Highlight = packageDto.Highlight,
                CreatedBy = packageDto.CreateBy,
                CreatedAt = DateTime.Now,
                UpdatedBy = null,
                UpdatedAt = null
            }; 
            return await _packageService.Save(newPackage);
        }
        [HttpPut("package")]
        public async Task<IBusinessResult> UpdatePackage(PackageDto packageDto)
        {
            var existingPackage = await _packageService.GetById(packageDto.Id);
            
            var updatedPackage =(Package)existingPackage.Data;
            updatedPackage.Name = packageDto.Name;
            updatedPackage.Description = packageDto.Description;
            updatedPackage.Price = packageDto.Price;
            updatedPackage.StartDate = packageDto.StartDate;
            updatedPackage.EndDate = packageDto.EndDate;
            updatedPackage.FeatureType = packageDto.FeatureType;
            updatedPackage.Status = packageDto.Status;
            updatedPackage.Discount = packageDto.Discount;
            updatedPackage.Highlight = packageDto.Highlight;
            updatedPackage.UpdatedBy = packageDto.UpdatedBy;
            updatedPackage.UpdatedAt = DateTime.Now;
            return await _packageService.Update(updatedPackage);
        }

        [HttpDelete("package/{id}")]
        public async Task<IBusinessResult> DeleteById(int id)
        {
            return await _packageService.DeleteById(id);
        }

    }
}
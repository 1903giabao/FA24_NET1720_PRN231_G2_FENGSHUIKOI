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

namespace FENGSHUIKOI.APIService.Controllers
{
    [Route("member")]
    public class MembersController : Controller
    {
        private readonly IMemberService _context;

        public MembersController(IMemberService context)
        {
            _context ??= context;
        }
        [HttpGet]
        public async Task<IBusinessResult> GetAll()
        {
            return await _context.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetById(int id)
        {
            return await _context.GetById(id);
        }
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteById(int id)
        {
            return await _context.DeleteById(id);
        }
        [HttpPost]
        public async Task<IBusinessResult> Create([FromBody]MemberDTO dto)
        {
            var obj = new Member { Name = dto.Name, Address = dto.Address, Avatar = dto.Avatar, Description = dto.Description, Password = dto.Password,Phone=dto.Phone,X=dto.X,Y=dto.Y,StoreName=dto.StoreName};
            return await _context.Save(obj);
        }
    }

}

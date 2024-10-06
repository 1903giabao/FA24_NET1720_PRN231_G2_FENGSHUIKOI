using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Service.Services;
using FENGSHUIKOI.Common;
using FENGSHUIKOI.Data.Dto;

namespace FENGSHUIKOI.MVCWebApp.Controllers
{
    public class ComboesV2Controller : Controller
    {
        private readonly ComboService _comboService;

        private readonly ElementService _elementService;
        private readonly MemberService _memberService;
        private readonly ProductDetailService _productDetailService;

        public List<Element> Elements { get; set; }
        public List<Member> Members { get; set; }
        public List<ProductDetail> Products { get; set; }

        public ComboesV2Controller(ElementService elementService, MemberService memberService, ProductDetailService productDetailService)
        {
            _elementService = elementService;
            _memberService = memberService;
            _productDetailService = productDetailService;
        }

        // GET: ComboesV2
        public async Task<IActionResult> Index()
        {
            var elements = _elementService.GetAll();
            var members = _memberService.GetAll();
            var productDetails = _productDetailService.GetAll();

            var elementList = (List<Element>)elements.Result.Data;
            var membersList = (List<Member>)members.Result.Data;
            var productDetailsList = (List<ProductDetail>)productDetails.Result.Data;

            ViewBag.Elements = new SelectList(elementList, nameof(Element.Id), nameof(Element.Name));
            ViewBag.Members = new SelectList(membersList, nameof(Member.Id), nameof(Member.Name));
            ViewBag.ProductDetails = new SelectList(productDetailsList, nameof(ProductDetail.Id), nameof(ProductDetail.Name));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComboRequestDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _comboService.Save(model);

                if (result != null && result.Message == Const.SUCCESS_CREATE_MSG)
                {
                    return RedirectToAction("Index");
                }

                throw new Exception("Create error !");
            }
            return View(model);
        }
    }
}

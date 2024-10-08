using FENGSHUIKOI.Common;
using FENGSHUIKOI.Data.Dto;
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FENGSHUIKOI.MVCWebApp.Controllers
{
    public class SuitableObjectsV2Controller : Controller
    {
        private readonly ElementService _elementService;
        private readonly TypeService _typeService;
        private readonly SuitableObjectService _suitableObjectService;

        public List<Element> Elements { get; set; }
        public List<Data.Models.Type> Types { get; set; }

        public SuitableObjectsV2Controller(ElementService elementService, TypeService typeService, SuitableObjectService suitableObjectService)
        {
            _elementService = elementService;
            _typeService = typeService;
            _suitableObjectService = suitableObjectService;
        }

        public IActionResult Index()
        {
            var elements = _elementService.GetAll();
            var types = _typeService.GetAll();

            var elementList = (List<Element>) elements.Result.Data;
            var typeList = (List<Data.Models.Type>) types.Result.Data;

            ViewBag.Elements = new SelectList(elementList, nameof(Element.Id), nameof(Element.Name));
            ViewBag.Types = new SelectList(typeList, nameof(Data.Models.Type.Id), nameof(Data.Models.Type.Name));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SuitableObjectDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _suitableObjectService.Save(model);

                if (result != null && result.Message == Const.SUCCESS_CREATE_MSG)
                {
                    return RedirectToAction("Index");
                }

                throw new Exception("create error");
            }
            return View(model);
        }
    }
}

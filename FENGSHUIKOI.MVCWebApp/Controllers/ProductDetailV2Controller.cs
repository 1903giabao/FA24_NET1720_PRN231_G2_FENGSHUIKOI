using FENGSHUIKOI.Common;
using FENGSHUIKOI.Data.Dto;
using FENGSHUIKOI.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace FENGSHUIKOI.MVCWebApp.Controllers
{
    public class ProductDetailV2Controller : Controller
    {

        public IActionResult Index()
        {
            return View(); 
        }

    }
}

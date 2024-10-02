using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Common;
using Newtonsoft.Json;

using System.Reflection.Metadata.Ecma335;
using Azure;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FENGSHUIKOI.Service.Base;

namespace FENGSHUIKOI.MVCWebApp.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly NET1720_231_2_FENGSHUIKOIContext _context;

        public ProductDetailsController(NET1720_231_2_FENGSHUIKOIContext context)
        {
            _context = context;
        }

        // GET: ProductDetails
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ProductDetail"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            // Directly deserialize into a List<ProductDetail>
                            var data = JsonConvert.DeserializeObject<List<ProductDetail>>(content);

                            if (data != null)
                            {
                                return View(data);
                            }
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
            return View(new List<ProductDetail>());
        }


        public async Task<IActionResult> Create()
        {
            ViewData["TypeId"] = new SelectList(await this.GetType(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,Description,ComboId,TypeId,Color,Name,Quantity,Type,Status,CreateDate,Size,Origin")] ProductDetail productDetail)
        public async Task<IActionResult> Create(ProductDetail productDetail)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpCilent = new HttpClient())
                {
                    using (var response = await httpCilent.PostAsJsonAsync(Const.APIEndPoint + "ProductDetail/", productDetail))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["TypeId"] = new SelectList(await this.GetType(), "Id", "Name", productDetail.Id);
                return View(productDetail);
            }
        }
        /*
                public async Task<IActionResult> Edit(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var productDetail = await _context.ProductDetails.FindAsync(id);
                    if (productDetail == null)
                    {
                        return NotFound();
                    }
                    ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", productDetail.TypeId);
                    return View(productDetail);
                }*/


        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("Id,Description,ComboId,TypeId,Color,Name,Quantity,Type,Status,CreateDate,Size,Origin")] ProductDetail productDetail)
         {
             if (id != productDetail.Id)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(productDetail);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!ProductDetailExists(productDetail.Id))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Index));
             }
             ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", productDetail.TypeId);
             return View(productDetail);
         }

 */
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ProductDetail/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ProductDetail>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }

            }
            return View(new ProblemDetails());
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "Combo"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_DELETE)
                            {
                                deleteStatus = true;
                            }
                            else
                            {
                                deleteStatus = false;
                            }
                        }
                    }
                }
            }

            if (deleteStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }


        /*  [HttpPost, ActionName("Delete")]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> DeleteConfirmed(string id)
          {
              var productDetail = await _context.ProductDetails.FindAsync(id);
              if (productDetail != null)
              {
                  _context.ProductDetails.Remove(productDetail);
              }

              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(Index));
          }

          private bool ProductDetailExists(int id)
          {
              return _context.ProductDetails.Any(e => e.Id == id);
          }*/

        public async Task<List<FENGSHUIKOI.Data.Models.Type>> GetType()
        {
            var type = new List<FENGSHUIKOI.Data.Models.Type>();
            using (var httpClient = new HttpClient()) {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "type"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var rs = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (rs != null && rs.Data != null)
                        {
                            type = JsonConvert.DeserializeObject<List<FENGSHUIKOI.Data.Models.Type>>(rs.Data.ToString());
                        }
                       
                    }
                }
            }
            return type;

        }
    }
}

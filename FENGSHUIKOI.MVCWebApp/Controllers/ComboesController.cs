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
using FENGSHUIKOI.Service.Base;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FENGSHUIKOI.MVCWebApp.Controllers
{
    public class ComboesController : Controller
    {
        public ComboesController()
        {
        }

        // GET: Comboes
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Comboes"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Data != null)
                            {
                                var data = JsonConvert.DeserializeObject<List<Combo>>(result.Data.ToString());
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
            return View(new List<Combo>());
        }

        // GET: Comboes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Comboes/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Combo>(result.Data.ToString());
                            ViewData["MemberId"] = new SelectList(await this.GetMembers(), "Id", "Name", data.Id);
                            ViewData["ElementId"] = new SelectList(await this.GetElements(), "Id", "Name", data.Id);
                            ViewData["ProductDetailId"] = new SelectList(await this.GetProductDetails(), "Id", "Name", data.Id);
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<Combo>());
        }

        // GET: Comboes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["MemberId"] = new SelectList(await this.GetMembers(), "Id", "Name");
            ViewData["ElementId"] = new SelectList(await this.GetElements(), "Id", "Name");
            ViewData["ProductDetailId"] = new SelectList(await this.GetProductDetails(), "Id", "Name");
            return View();
        }

        // POST: Comboes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberId,ElementId,ProductDetailId,ComboName,ComboPrice,Discount,Status,CreatedBy,CreatedAt")] Combo combo)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpCilent = new HttpClient())
                {
                    using (var response = await httpCilent.PostAsJsonAsync(Const.APIEndPoint + "Comboes/", combo))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Message == Const.SUCCESS_CREATE_MSG)
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
                ViewData["MemberId"] = new SelectList(await this.GetMembers(), "Id", "Name", combo.Id);
                ViewData["ElementId"] = new SelectList(await this.GetElements(), "Id", "Name", combo.Id);
                ViewData["ProductDetailId"] = new SelectList(await this.GetProductDetails(), "Id", "Name", combo.Id);
                return View(combo);
            }
        }

        // GET: Comboes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            using (var httpCilent = new HttpClient())
            {
                using (var response = await httpCilent.GetAsync(Const.APIEndPoint + "Comboes/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result == null || result.Status == 404)
                        {
                            return NotFound();
                        }
                        if (result != null)
                        {
                            var data = JsonConvert.DeserializeObject<Combo>(result.Data.ToString());
                            ViewData["MemberId"] = new SelectList(await this.GetMembers(), "Id", "Name", data.Id);
                            ViewData["ElementId"] = new SelectList(await this.GetElements(), "Id", "Name", data.Id);
                            ViewData["ProductDetailId"] = new SelectList(await this.GetProductDetails(), "Id", "Name", data.Id);
                            return View(data);
                        }
                    }
                }
            }
            return View(new Combo());
        }

        // POST: Comboes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,ElementId,ProductDetailId,ComboName,ComboPrice,Discount,Status,CreatedBy,CreatedAt")] Combo combo)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpCilent = new HttpClient())
                {
                    using (var response = await httpCilent.PostAsJsonAsync(Const.APIEndPoint + "Comboes/", combo))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Message == Const.SUCCESS_UDATE_MSG)
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
                ViewData["MemberId"] = new SelectList(await this.GetMembers(), "Id", "Name", combo.Id);
                ViewData["ElementId"] = new SelectList(await this.GetElements(), "Id", "Name", combo.Id);
                ViewData["ProductDetailId"] = new SelectList(await this.GetProductDetails(), "Id", "Name", combo.Id);
                return View(combo);
            }
        }

        // GET: Comboes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Comboes/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Combo>(result.Data.ToString());
                            ViewData["MemberId"] = new SelectList(await this.GetMembers(), "Id", "Name", data.Id);
                            ViewData["ElementId"] = new SelectList(await this.GetElements(), "Id", "Name", data.Id);
                            ViewData["ProductDetailId"] = new SelectList(await this.GetProductDetails(), "Id", "Name", data.Id);
                            return View(data);
                        }
                    }
                }

            }
            return View(new Combo());
        }

        // POST: Comboes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "Comboes/" + id))
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

        public async Task<List<Member>> GetMembers()
        {
            var member = new List<Member>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7194/" + "member"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var rs = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (rs != null && rs.Data != null)
                        {
                            member = JsonConvert.DeserializeObject<List<Member>>(rs.Data.ToString());
                        }

                    }
                }
            }
            return member;
        }
        
        public async Task<List<Element>> GetElements()
        {
            var elements = new List<Element>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7194/" + "elements"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var rs = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (rs != null && rs.Data != null)
                        {
                            elements = JsonConvert.DeserializeObject<List<Element>>(rs.Data.ToString());
                        }

                    }
                }
            }
            return elements;
        }
        
        public async Task<List<ProductDetail>> GetProductDetails()
        {
            var productDetails = new List<ProductDetail>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ProductDetail"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var rs = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (rs != null && rs.Data != null)
                        {
                            productDetails = JsonConvert.DeserializeObject<List<ProductDetail>>(rs.Data.ToString());
                        }

                    }
                }
            }
            return productDetails;
        }

    }
}

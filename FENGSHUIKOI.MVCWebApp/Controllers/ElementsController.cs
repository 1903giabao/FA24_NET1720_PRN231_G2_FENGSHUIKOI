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
using FENGSHUIKOI.Data.Dto;

namespace FENGSHUIKOI.MVCWebApp.Controllers
{
    public class ElementsController : Controller
    {
        private readonly NET1720_231_2_FENGSHUIKOIContext _context;
        private string ApiUrl = "https://localhost:7194/";
        public ElementsController(NET1720_231_2_FENGSHUIKOIContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(ApiUrl + "elements"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Data != null)
                            {
                                var data = JsonConvert.DeserializeObject<List<Element>>(result.Data.ToString());
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
            return View(new List<Element>());
        }

        // GET: Elements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(ApiUrl + "elements/"+id))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                      
                            if (result == null || result.Status==404)
                            {
                                return NotFound();
                            }
                            if (result != null)
                            {
                                var data = JsonConvert.DeserializeObject<Element>(result.Data.ToString());
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
            

            return View(new Element());
        }

        // GET: Elements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Elements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,TabooTime,ImageUrl,Status,LuckyNumbers,UpdatedAt,CreatedAt,CreatedBy,UpdateBy")] Element element)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.PostAsJsonAsync(ApiUrl + "elements", new ElementDTO()
                    {
                        Id = element.Id,
                        Status = element.Status,
                        CreatedAt = DateTime.Now,
                        CreatedBy = element.CreatedBy,
                        Description = element.Description,
                        ImageUrl = element.ImageUrl,
                        LuckyNumbers = element.LuckyNumbers,
                        Name = element.Name,
                        TabooTime = element.TabooTime,
                        UpdateBy=element.UpdateBy,
                        UpdatedAt=DateTime.Now,
                    }))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                      
                            if (result == null || (result.Status!=200 && result.Status!=201))
                            {
                                return View();
                            }

                                return RedirectToAction(nameof(Index));
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
                
            }
            return View(element);
        }

        // GET: Elements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(ApiUrl + "elements/" + id))
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
                                var data = JsonConvert.DeserializeObject<Element>(result.Data.ToString());
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


            return View(new Element());
        }

        // POST: Elements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,TabooTime,ImageUrl,Status,LuckyNumbers,UpdatedAt,CreatedAt,CreatedBy,UpdateBy")] Element element)
        {
            if (id != element.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        try
                        {
                            using (var response = await httpClient.PutAsJsonAsync(ApiUrl + "elements/" +id, new ElementDTO()
                            {
                                Id = element.Id,
                                Status = element.Status,
                                CreatedAt = DateTime.Now,
                                CreatedBy = element.CreatedBy,
                                Description = element.Description,
                                ImageUrl = element.ImageUrl,
                                LuckyNumbers = element.LuckyNumbers,
                                Name = element.Name,
                                TabooTime = element.TabooTime,
                                UpdateBy = element.UpdateBy,
                                UpdatedAt = DateTime.Now,
                            }))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    var content = await response.Content.ReadAsStringAsync();
                                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);


                                    if (result == null || (result.Status != 200 && result.Status != 201))
                                    {
                                        return View();
                                    }

                                    return RedirectToAction(nameof(Index));
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElementExists(element.Id))
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
            return View(element);
        }

        // GET: Elements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(ApiUrl + "elements/" + id))
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
                                var data = JsonConvert.DeserializeObject<Element>(result.Data.ToString());
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

            return View(new Element());
        }

        // POST: Elements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(ApiUrl + "elements/" + id))
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
                               using(var deletedObj= await httpClient.DeleteAsync(ApiUrl + "elements/" + id))
                                {
                                    var content2 = await response.Content.ReadAsStringAsync();
                                    var result2 = JsonConvert.DeserializeObject<BusinessResult>(content2);
                                    if(result2.Status==1) return RedirectToAction(nameof(Index));
                                    return View(result.Message);
                                }
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
                return View();
            }
            
        }

        private bool ElementExists(int id)
        {
            return _context.Elements.Any(e => e.Id == id);
        }
    }
}

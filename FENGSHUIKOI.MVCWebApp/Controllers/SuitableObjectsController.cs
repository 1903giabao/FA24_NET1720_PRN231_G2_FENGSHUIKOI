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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using FENGSHUIKOI.Service.Base;

namespace FENGSHUIKOI.MVCWebApp.Controllers
{
    public class SuitableObjectsController : Controller
    {
        public SuitableObjectsController()
        {
        }

        // GET: SuitableObjects
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "suitableObject"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Data != null)
                            {
                                var data = JsonConvert.DeserializeObject<List<SuitableObject>>(result.Data.ToString());
                                return View(data);
                            }
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Log the exception (consider using a logging framework)
                    Console.WriteLine($"Request error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Catch any other exceptions
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
            return View(new List<SuitableObject>());
        }

        // GET: SuitableObjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"suitableObject/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<SuitableObject>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<Member>());
        }

        // GET: SuitableObjects/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ElementId"] = new SelectList(await this.GetElements(), "Id", "Name");
            ViewData["TypeId"] = new SelectList(await this.GetTypes(), "Id", "Name");
            return View();
        }

        // POST: SuitableObjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ElementId,TypeId,Color,Size,Direction,Position,Shape,Volume,WaterQuality,WaterTemperature,InformationDirection,Flag")] SuitableObject suitableObject)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "suitableObject/", suitableObject))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var context = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(context);

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
                ViewData["ElementId"] = new SelectList(await this.GetElements(), "Id", "Name");
                ViewData["TypeId"] = new SelectList(await this.GetTypes(), "Id", "Name");
                return View(suitableObject);
            }
        }

        // GET: SuitableObjects/Edit
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
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "suitableObject/" + id))
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

        // POST: SuitableObjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ElementId,TypeId,Color,Size,Direction,Position,Shape,Volume,WaterQuality,WaterTemperature,InformationDirection,Flag")] SuitableObject suitableObject)
        {
            bool updateStatus = false;
            
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "suitableObject/", suitableObject))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var context = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(context);

                            if (result != null && result.Message == Const.SUCCESS_UDATE_MSG)
                            {
                                updateStatus = true;
                            }
                            else
                            {
                                updateStatus = false;
                            }
                        }
                    }
                }
            }

            if (updateStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["ElementId"] = new SelectList(await this.GetElements(), "Id", "Name");
                ViewData["TypeId"] = new SelectList(await this.GetTypes(), "Id", "Name");
                return View(suitableObject);
            }
        }

        // GET: SuitableObjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "suitableObject/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<SuitableObject>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new SuitableObject());
        }

        // POST: SuitableObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "suitableObject/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var context = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(context);
                            if (result != null && result.Message == Const.SUCCESS_DELETE_MSG)
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
                return RedirectToAction(nameof(Delete));
            }
        }

/*        private bool SuitableObjectExists(int id)
        {
            return _context.SuitableObjects.Any(e => e.Id == id);
        }*/

        public async Task<List<FENGSHUIKOI.Data.Models.Type>> GetTypes()
        {
            var types = new List<FENGSHUIKOI.Data.Models.Type>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "type"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var rs = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (rs != null && rs.Data != null)
                        {
                            types = JsonConvert.DeserializeObject<List<FENGSHUIKOI.Data.Models.Type>>(rs.Data.ToString());
                        }

                    }
                }
            }
            return types;
        }        
        
        public async Task<List<FENGSHUIKOI.Data.Models.Element>> GetElements()
        {
            var elements = new List<FENGSHUIKOI.Data.Models.Element>();
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
                            elements = JsonConvert.DeserializeObject<List<FENGSHUIKOI.Data.Models.Element>>(rs.Data.ToString());
                        }

                    }
                }
            }
            return elements;

        }
    }
}

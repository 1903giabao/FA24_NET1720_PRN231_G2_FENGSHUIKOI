using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FENGSHUIKOI.Data.Models;
using FENGSHUIKOI.Common;
using FENGSHUIKOI.Service.Base;
using Newtonsoft.Json;

namespace FENGSHUIKOI.MVCWebApp.Controllers
{
    public class SuitableObjectsController : Controller
    {
        private readonly NET1720_231_2_FENGSHUIKOIContext _context;

        public SuitableObjectsController(NET1720_231_2_FENGSHUIKOIContext context)
        {
            _context = context;
        }

        // GET: SuitableObjects
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "SuitableObject"))
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

        /*        // GET: SuitableObjects/Details/5
                public async Task<IActionResult> Details(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var suitableObject = await _context.SuitableObjects
                        .Include(s => s.Element)
                        .Include(s => s.Type)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (suitableObject == null)
                    {
                        return NotFound();
                    }

                    return View(suitableObject);
                }

                // GET: SuitableObjects/Create
                public IActionResult Create()
                {
                    ViewData["ElementId"] = new SelectList(_context.Elements, "Id", "Name");
                    ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name");
                    return View();
                }

                // POST: SuitableObjects/Create
                // To protect from overposting attacks, enable the specific properties you want to bind to.
                // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create([Bind("Id,ElementId,TypeId,Color,Size,Direction,Position,Shape,Volume,WaterQuality,WaterTemperature,InformationDirection,Flag")] SuitableObject suitableObject)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(suitableObject);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["ElementId"] = new SelectList(_context.Elements, "Id", "Name", suitableObject.ElementId);
                    ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", suitableObject.TypeId);
                    return View(suitableObject);
                }

                // GET: SuitableObjects/Edit/5
                public async Task<IActionResult> Edit(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var suitableObject = await _context.SuitableObjects.FindAsync(id);
                    if (suitableObject == null)
                    {
                        return NotFound();
                    }
                    ViewData["ElementId"] = new SelectList(_context.Elements, "Id", "Name", suitableObject.ElementId);
                    ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", suitableObject.TypeId);
                    return View(suitableObject);
                }

                // POST: SuitableObjects/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to.
                // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(int id, [Bind("Id,ElementId,TypeId,Color,Size,Direction,Position,Shape,Volume,WaterQuality,WaterTemperature,InformationDirection,Flag")] SuitableObject suitableObject)
                {
                    if (id != suitableObject.Id)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(suitableObject);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!SuitableObjectExists(suitableObject.Id))
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
                    ViewData["ElementId"] = new SelectList(_context.Elements, "Id", "Name", suitableObject.ElementId);
                    ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", suitableObject.TypeId);
                    return View(suitableObject);
                }

                // GET: SuitableObjects/Delete/5
                public async Task<IActionResult> Delete(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var suitableObject = await _context.SuitableObjects
                        .Include(s => s.Element)
                        .Include(s => s.Type)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (suitableObject == null)
                    {
                        return NotFound();
                    }

                    return View(suitableObject);
                }

                // POST: SuitableObjects/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {
                    var suitableObject = await _context.SuitableObjects.FindAsync(id);
                    if (suitableObject != null)
                    {
                        _context.SuitableObjects.Remove(suitableObject);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                private bool SuitableObjectExists(int id)
                {
                    return _context.SuitableObjects.Any(e => e.Id == id);
                }*/
            }
    }

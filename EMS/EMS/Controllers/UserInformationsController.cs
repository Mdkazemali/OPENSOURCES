using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Data;
using EMS.Models;

namespace EMS.Controllers
{
    public class UserInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserInformationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserInformations
        public async Task<IActionResult> Index()
        {
              return View(await _context.UserInformation.ToListAsync());
        }

        // GET: UserInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserInformation == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformation
                .FirstOrDefaultAsync(m => m.UserinfoId == id);
            if (userInformation == null)
            {
                return NotFound();
            }

            return View(userInformation);
        }

        // GET: UserInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserinfoId,UserFullName,Gender,PhoneNumber,NID,Address,CreateDate,Status,ExpireDate,LoginId,TranjectionId,Designation,Degree,DVMRegiNo,KhamarType,PhotoUrl")] UserInformation userInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userInformation);
        }

        // GET: UserInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserInformation == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformation.FindAsync(id);
            if (userInformation == null)
            {
                return NotFound();
            }
            return View(userInformation);
        }

        // POST: UserInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserinfoId,UserFullName,Gender,PhoneNumber,NID,Address,CreateDate,Status,ExpireDate,LoginId,TranjectionId,Designation,Degree,DVMRegiNo,KhamarType,PhotoUrl")] UserInformation userInformation)
        {
            if (id != userInformation.UserinfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInformationExists(userInformation.UserinfoId))
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
            return View(userInformation);
        }

        // GET: UserInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserInformation == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformation
                .FirstOrDefaultAsync(m => m.UserinfoId == id);
            if (userInformation == null)
            {
                return NotFound();
            }

            return View(userInformation);
        }

        // POST: UserInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserInformation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserInformation'  is null.");
            }
            var userInformation = await _context.UserInformation.FindAsync(id);
            if (userInformation != null)
            {
                _context.UserInformation.Remove(userInformation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInformationExists(int id)
        {
          return _context.UserInformation.Any(e => e.UserinfoId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Data;
using EMS.Models;

namespace EMS.Controllers
{
    public class FacilityRegistriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private object _webHostEnvironment;

        public FacilityRegistriesController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index(string header,string org ,string upojela,string district, string union, string phone, int pp, int page = 1, int pageSize = 50)
        {          
            var custquery = from x in _context.FacilityRegistry select x;

            ViewData["header"] = header;
            if (!string.IsNullOrEmpty(header))
            {
                custquery = custquery.Where(x => x.FacilityHeadInfomations.Contains(header));
            }

            ViewData["org"] = org;
            if (!string.IsNullOrEmpty(org))
            {
                custquery = custquery.Where(x => x.OrganizationName.Contains(org));
            }

            ViewData["phone"] = phone;
            if (!string.IsNullOrEmpty(phone))
            {
                custquery = custquery.Where(x => x.FacilityMobile.Contains(phone));
            }
            
            ViewData["district"] = district;
            if (!string.IsNullOrEmpty(district))
            {
                custquery = custquery.Where(x => x.DistricName.Contains(district));
            }

            ViewData["upojela"] = upojela;
            if (!string.IsNullOrEmpty(upojela))
            {
                custquery = custquery.Where(x => x.UpozillaName.Contains(upojela));
            } 
            
            ViewData["union"] = union;
            if (!string.IsNullOrEmpty(union))
            {
                custquery = custquery.Where(x => x.UnionName.Contains(union));
            }
            

            // for page setups

            int p;
            if (pp == 0)
            {
                p = 6;

            }
            else
            {
                p = pp;
            }

            ViewData["pp"] = p;
            pageSize = p;



            // Count the total number of records
            var totalRecords = await custquery.CountAsync();

            // Calculate the number of pages
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            // Validate the current page value
            page = Math.Max(1, Math.Min(totalPages, page));

            // Calculate the number of records to skip
            var skip = (page - 1) * pageSize;

            // Apply pagination and ordering
            var pagedQuery = custquery.OrderByDescending(x => x.FacilityId).Skip(skip).Take(pageSize).AsNoTracking();

            // Pass the pagination information to the view
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = totalPages;
            ViewData["TotalRecords"] = totalRecords;

            return View(await pagedQuery.ToListAsync());
        }









        // GET: FacilityRegistries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FacilityRegistry == null)
            {
                return NotFound();
            }

            var facilityRegistry = await _context.FacilityRegistry
                .FirstOrDefaultAsync(m => m.FacilityId == id);
            if (facilityRegistry == null)
            {
                return NotFound();
            }

            return View(facilityRegistry);
        }

        // GET: FacilityRegistries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FacilityRegistries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FacilityRegistry facilityRegistry)
        {
            if (ModelState.IsValid)
            {
                string luniqueFileName = LGetProfilePhotoFileName(facilityRegistry);
                facilityRegistry.FalPhotoUrl = luniqueFileName;

                string runiqueFileName = RGetProfilePhotoFileName(facilityRegistry);
                facilityRegistry.FarPhotoUrl = runiqueFileName;

                facilityRegistry.FaUserName=User.Identity.Name.Split('@')[0];
                facilityRegistry.LastUpdateDate= DateTime.Now; 

                _context.Add(facilityRegistry);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facilityRegistry);
        }


        // GET: FacilityRegistries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FacilityRegistry == null)
            {
                return NotFound();
            }

            var facilityRegistry = await _context.FacilityRegistry.FindAsync(id);
            if (facilityRegistry == null)
            {
                return NotFound();
            }
            return View(facilityRegistry);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, FacilityRegistry facilityRegistry)
        {
            if (id != facilityRegistry.FacilityId)
            {
                return NotFound();
            }
            if (facilityRegistry.FalProfilePhoto != null)
            {
                string luniqueFileName = LGetProfilePhotoFileName(facilityRegistry);
                facilityRegistry.FalPhotoUrl = luniqueFileName;
            }
            
            if (facilityRegistry.FarProfilePhoto != null)
            {
                string runiqueFileName = RGetProfilePhotoFileName(facilityRegistry);
                facilityRegistry.FarPhotoUrl = runiqueFileName;
            }



            if (ModelState.IsValid)
            {
                
                facilityRegistry.FaUserName = User.Identity.Name.Split('@')[0];
                facilityRegistry.LastUpdateDate = DateTime.Now;


                _context.Attach(facilityRegistry);
                _context.Entry(facilityRegistry).State = EntityState.Modified;
                _context.SaveChanges();
             
                
                return RedirectToAction(nameof(Index));
            }
            return View(facilityRegistry);
        }

        // GET: FacilityRegistries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FacilityRegistry == null)
            {
                return NotFound();
            }

            var facilityRegistry = await _context.FacilityRegistry
                .FirstOrDefaultAsync(m => m.FacilityId == id);
            if (facilityRegistry == null)
            {
                return NotFound();
            }

            return View(facilityRegistry);
        }

        // POST: FacilityRegistries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FacilityRegistry == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FacilityRegistry'  is null.");
            }
            var facilityRegistry = await _context.FacilityRegistry.FindAsync(id);
            if (facilityRegistry != null)
            {
                _context.FacilityRegistry.Remove(facilityRegistry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityRegistryExists(int id)
        {
          return _context.FacilityRegistry.Any(e => e.FacilityId == id);
        }

        // For Facility file name and upload Left logo

        private string LGetProfilePhotoFileName(FacilityRegistry facilityRegistry)
        {
            string luniqueFileName = null;

            if (facilityRegistry.FalProfilePhoto != null)
            {
                // Get the file name without the path
                string loriginalFileName = Path.GetFileName(facilityRegistry.FalProfilePhoto.FileName);

                // Generate a unique file name
                luniqueFileName = Guid.NewGuid().ToString() + "_" + loriginalFileName;

                // Define the path where the file will be saved
                string luploadsFolder = Path.Combine(_webHost.WebRootPath, "FacilitiImages");
                string lfilePath = Path.Combine(luploadsFolder, luniqueFileName);

                // Ensure the directory exists
                Directory.CreateDirectory(luploadsFolder);

                // Save the file
                using (var fileStream = new FileStream(lfilePath, FileMode.Create))
                {
                    facilityRegistry.FalProfilePhoto.CopyTo(fileStream);
                }
            }

            return luniqueFileName;
        }



        // For Facility file name and upload right logo

        private string RGetProfilePhotoFileName(FacilityRegistry facilityRegistry)
        {
            string runiqueFileName = null;

            if (facilityRegistry.FarProfilePhoto != null)
            {
                // Get the file name without the path
                string roriginalFileName = Path.GetFileName(facilityRegistry.FarProfilePhoto.FileName);

                // Generate a unique file name
                runiqueFileName = Guid.NewGuid().ToString() + "_" + roriginalFileName;

                // Define the path where the file will be saved
                string ruploadsFolder = Path.Combine(_webHost.WebRootPath, "FacilitiImages");
                string rfilePath = Path.Combine(ruploadsFolder, runiqueFileName);

                // Ensure the directory exists
                Directory.CreateDirectory(ruploadsFolder);

                // Save the file
                using (var fileStream = new FileStream(rfilePath, FileMode.Create))
                {
                    facilityRegistry.FarProfilePhoto.CopyTo(fileStream);
                }
            }

            return runiqueFileName;
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS.Data;

namespace EMS.Controllers
{
    //[Authorize(Roles ="Admin")]
 
 
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _manager;
        private readonly ApplicationDbContext _context;
        public RoleController(RoleManager<IdentityRole> manager, ApplicationDbContext context)
        {
            _manager = manager;
            _context = context;
        }
        public IActionResult Index()
        {
            var roles = _manager.Roles.Where(x => x.Name!="User" && x.Name!="System").OrderByDescending(x=>x.Name);
            return View(roles);
        }

        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            if (!_manager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
            
            {
                _manager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
              
            }

            return RedirectToAction("Index");
        }


        // for Edit controller code

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var role = _manager.Roles.FirstOrDefault(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost]
        public IActionResult Edit(string id, IdentityRole role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingRole = _manager.Roles.FirstOrDefault(r => r.Id == id);
                    existingRole.Name = role.Name;

                    _manager.UpdateAsync(existingRole).GetAwaiter().GetResult();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // for delete controller code

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var role = _manager.Roles.FirstOrDefault(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            var role = _manager.Roles.FirstOrDefault(r => r.Id == id);
            if (_context.UserRoles.Any(x=>x.RoleId==id))
            {
                // Return empty result with styled message if fromDate or toDate are in the default state
                string errorMessage = "<div style=\"display: flex; flex-direction: column; justify-content: center; " +
                    "align-items: center; height: 100vh;\"><span style='font-size:100px;'>&#128554;</span><p style=\"font-size: 44px; text-align: center; color: red;\">" +
                    "You cannot delete it because someone has Assigns this Roles</p></div>";
                return Content(errorMessage, "text/html");
            }
            if (role != null)
            {
                _manager.DeleteAsync(role).GetAwaiter().GetResult();
            }

            return RedirectToAction(nameof(Index));
        }




    }
}

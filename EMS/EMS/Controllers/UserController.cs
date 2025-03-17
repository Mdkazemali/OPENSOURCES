using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Data;
using EMS.Models;
using EMS.ResetModelView;
using EMS.UserInfoModelView;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EMS.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _manager;
    
        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> manager, ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _userManager = userManager;
            _manager = manager;
            _context = context;
            _webHost = webHost;

        }



        public ActionResult Index()
        {
            List<UserRoleViewModel> groupedUserData = GetGroupedVideoData();
            return View("Index", groupedUserData);
        }

        private List<UserRoleViewModel> GetGroupedVideoData()
        {
            var groupedUsersWithRoles = from A in _userManager.Users
                                        join B in _context.UserRoles on A.Id equals B.UserId
                                        join C in _manager.Roles on B.RoleId equals C.Id
                                        join D in _context.UserInformation on A.Email equals D.LoginId
                                        where A.Email != "system@gmail.com"
                                        group C.Name by new { A.Id, A.UserName, D.UserFullName,D.PhotoUrl,D.PhoneNumber,D.Address } into groupedData
                                        select new UserRoleViewModel
                                        {
                                            UserId = groupedData.Key.Id,

                                            UserFullName=groupedData.Key.UserFullName,
                                            PhotoUrl= groupedData.Key.PhotoUrl,
                                            UserName = groupedData.Key.UserName,
                                            RoleCount = groupedData.Count(),
                                            PhoneNumber=groupedData.Key.PhoneNumber,
                                            Address=groupedData.Key.Address,

                                        };

            // Convert the result to a List<UserRoleGroupedViewModel>
            List<UserRoleViewModel> groupedUserList = groupedUsersWithRoles.ToList();

            return groupedUserList;
        }



        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var uu = _context.UserInformation.Where(x=>x.LoginId == user.Email).FirstOrDefault();  // for UserInformation Remove

            _context.UserInformation.Remove(uu); // for UserInformation Remove

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }


        // for assign role

        [HttpGet]
        public IActionResult AssignRoles(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var roles = _userManager.GetRolesAsync(user).Result;
           


            var viewModel = new UserRoleViewModel
            {
                UserId = id,
                Roles = _manager.Roles.Where(x => x.Name != "System").ToList(),          //For not show System Role              
                UserRoles = roles.ToList()
            };

            return View(viewModel);
        }




        [HttpPost]
        public async Task<IActionResult> AssignRoles(UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            // Remove existing roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Add selected roles
            await _userManager.AddToRolesAsync(user, model.SelectedRoles);

            return RedirectToAction("Index");
        }

        //=====================================================   For Password Reset ==========================

        public IActionResult ResetPassword(string Email)
        {
            var user = _userManager.FindByEmailAsync(Email).Result;
            if (user == null)
            {
                return NotFound();
            }

            var model = new PasswordResetViewModel
            {
                UserName = user.UserName
            };

            return View(model);
        }

        // POST: Admin/ResetPassword
        [HttpPost]
        public async Task<IActionResult> ResetPassword(PasswordResetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.UserName);
            if (user == null)
            {
                return NotFound();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                // Redirect or show success message
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }



    }
}


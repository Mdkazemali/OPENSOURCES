using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EMS.Data;
using EMS.Models;
using EMS.UserInfoModelView;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace EMS.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly SmtpSettings _smtpSettings;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _manager;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,
            IWebHostEnvironment webHost, IOptions<SmtpSettings> smtpSettings,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> manager)
        {
            _webHost = webHost;
            _logger = logger; // ILogger সঠিকভাবে সেট করা
            _context = context; // ডাটাবেজ কন্টেক্সট সঠিকভাবে সেট করা
            _smtpSettings = smtpSettings.Value; // SMTP সেটিংস সঠিকভাবে সেট করা
            _userManager = userManager;
            _manager = manager;
        }

        public ActionResult Index()
        {

            return View();
        }



        public IActionResult Call()
        {
            return View();
        }

        // for user informatio update


        [HttpGet]
        public IActionResult UserInformationsUpdated(string loginId)
        {
            var facilityRegistryList = _context.FacilityRegistry
                                      .Select(x => new { x.FacilityId, x.OrganizationName })
                                      .ToList();
            ViewBag.FacilityRegistryId = new SelectList(facilityRegistryList, "FacilityId", "OrganizationName");

            var info = _context.UserInformation.FirstOrDefault(x => x.LoginId == loginId);

            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }


        [HttpPost]
        public IActionResult UserInformationsUpdated(string loginId, UserInformation userInformation)
        {
            var facilityRegistryList = _context.FacilityRegistry
                                     .Select(x => new { x.FacilityId, x.OrganizationName })
                                     .ToList();
            ViewBag.FacilityRegistryId = new SelectList(facilityRegistryList, "FacilityId", "OrganizationName");




            if (userInformation.ProfilePhoto != null)
            {
                string uniqueFileName = GetProfilePhotoFileName(userInformation);
                userInformation.PhotoUrl = uniqueFileName;
            }

            if (userInformation.ProfilePhotosig != null)
            {
                string uniqueFileNamesig = GetProfilePhotoFileNamesig(userInformation);
                userInformation.PhotoUrlsig = uniqueFileNamesig;
            }

            if (ModelState.IsValid)
            {

                _context.Attach(userInformation);
                _context.Entry(userInformation).State = EntityState.Modified;

                _context.SaveChanges();

                return RedirectToAction("Index");

            }


            return View(userInformation);
        }





        private string GetProfilePhotoFileName(UserInformation customer)
        {
            string uniqueFileName = null;

            if (customer.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + customer.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    customer.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private string GetProfilePhotoFileNamesig(UserInformation customer)
        {
            string uniqueFileNamesig = null;

            if (customer.ProfilePhotosig != null)
            {
                string uploadsFoldersig = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileNamesig = Guid.NewGuid().ToString() + "_" + customer.ProfilePhotosig.FileName;
                string filePathsig = Path.Combine(uploadsFoldersig, uniqueFileNamesig);
                using (var fileStream = new FileStream(filePathsig, FileMode.Create))
                {
                    customer.ProfilePhotosig.CopyTo(fileStream);
                }
            }
            return uniqueFileNamesig;
        }



        [HttpGet]
        public IActionResult PhotoUrl()
        {
            var loginId = User.Identity.Name;
            var info = _context.UserInformation.FirstOrDefault(x => x.LoginId == loginId);

            var viewModel = new UserInfoViewModel
            {
                Name = User.Identity.Name,
                PhotoUrl = (info != null) ? info.PhotoUrl : null
            };

            return View(viewModel);
        }



        //==============   forgoten password =====================

        public IActionResult SendEmail()
        {
            return View();
        }


        private void SendEmail(string toEmail, string resetLink)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
                {
                    Credentials = new NetworkCredential(_smtpSettings.Email, _smtpSettings.AppPassword),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.Email),
                    Subject = "🔐 Password Reset Request",
                    Body = @"
                    <div style='font-family: Arial, sans-serif; max-width: 500px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #f9f9f9; text-align: center;'>
                        <h2 style='color: #333;'>Password Reset Request</h2>
                        <p style='color: #555;'>You have requested to reset your password. Click the button below to proceed.</p>
                        <a href='" + resetLink + @"' 
                           style='display: inline-block; padding: 12px 20px; margin: 10px 0; color: white; background-color: #007bff; text-decoration: none; font-size: 16px; border-radius: 5px;'>
                            Reset Password
                        </a>
                        <p style='color: #777; font-size: 14px;'>This link will expire in <b>30 minutes</b>.</p>
                        <hr style='border: none; height: 1px; background: #ddd; margin: 20px 0;'>
                        <p style='font-size: 12px; color: #888;'>If you didn’t request this, you can safely ignore this email.</p>
                    </div>",
                    IsBodyHtml = true
                };


                mailMessage.To.Add(toEmail);
                smtpClient.Send(mailMessage);
                _logger.LogInformation("Password reset email sent successfully to {Email}", toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {Email}", toEmail);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SendResetLink(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Message = "Email is required!";
                return View("SendEmail");
            }

            // ব্যবহারকারী যাচাই করুন
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ViewBag.Message = "No user found with this email.";
                return View("SendEmail");
            }

            // রিসেট টোকেন তৈরি করুন
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // লিংক তৈরি করুন
            var resetLink = Url.Action("ResetPassword", "Home",
                new { token, email = user.Email }, protocol: HttpContext.Request.Scheme);

            // ইমেইল পাঠান
            SendEmail(user.Email, resetLink);

            ViewBag.Message = "Password reset link has been sent to your email.";
            return View("SendEmail");
        }




        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid token or email.");
            }

            return View(new ResetPasswordModel { Token = token, Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ResetPassword", model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Invalid user.");
            }

            // পাসওয়ার্ড রিসেট করুন
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                ViewBag.Message = "Password has been reset successfully!";
                return View("ResetPasswordSuccess");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("ResetPassword", model);
        }



        //for chat and call


        public ActionResult Users()
        {
            List<UserRoleViewModel> groupedUserData = GetGroupedVideoData();
            return View("Users", groupedUserData);
        }

        private List<UserRoleViewModel> GetGroupedVideoData()
        {
            var groupedUsersWithRoles = from A in _userManager.Users
                                        join B in _context.UserRoles on A.Id equals B.UserId
                                        join C in _manager.Roles on B.RoleId equals C.Id
                                        join D in _context.UserInformation on A.Email equals D.LoginId
                                        where A.Email != User.Identity.Name
                                        group C.Name by new { A.Id, A.UserName, D.UserFullName, D.PhotoUrl, D.PhoneNumber, D.Address } into groupedData
                                        select new UserRoleViewModel
                                        {
                                            UserId = groupedData.Key.Id,
                                            UserName = groupedData.Key.UserName,
                                            UserFullName = string.IsNullOrEmpty(groupedData.Key.UserFullName) ? "Anonymous" : groupedData.Key.UserFullName,
                                            PhotoUrl = groupedData.Key.PhotoUrl,
                                        };

            // Convert the result to a List<UserRoleGroupedViewModel>
            List<UserRoleViewModel> groupedUserList = groupedUsersWithRoles.ToList();

            return groupedUserList;
        }

        public IActionResult VideoCall(string usrname, string userfullname, string photourl)
        {
            string photoUrl = string.IsNullOrEmpty(photourl) ? "noimage.png" : photourl;

            ViewData["Usrnm"] = usrname;
            ViewData["UserFullName"] = userfullname;
            ViewData["Userphoto"] = photoUrl;

            return View();
        }

        public IActionResult Messages(string usrname)
        {
            ViewData["Usrnm"] = usrname;

            return View();
        }

        public IActionResult IndivisualMessages(string usrname, string userfullname, string photourl)
        {
            ViewData["Usrnm"] = usrname;
            ViewData["UserFullName"] = userfullname;
            ViewData["Userphoto"] = photourl;

            return View();
        }



    }
}

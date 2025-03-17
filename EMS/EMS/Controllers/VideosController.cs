using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EMS.Data;
using EMS.Models;
using EMS.Controllers;

namespace EMS.Controllers
{
    public class VideosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public VideosController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        [Authorize]

        public IActionResult videos()
        {
            List<Trainingvideo> trainingvideodata = GetVideoData();

            var model = new UserRoleViewModel
            {
                //VideoDataList = trainingvideodata
            };

            // Calculate and set the time difference
            //model.TimeCalculations();

            return View(model);
        }

       public List<Trainingvideo> GetVideoData()
        {
            List<Trainingvideo> ls = _context.Trainingvideo.ToList();

            return ls;
                
        }


    }
}

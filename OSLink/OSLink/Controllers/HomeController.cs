using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OSLink.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UAParser;
namespace OSLink.Controllers
{
    public class HomeController : Controller
    {
        public string os_version { get; set; }
        public string os_name { get; set; }
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("Store/{UID}")]
        [HttpGet]
        public IActionResult Show(string UID)
        {

            var Link = _context.Links.Where(x => x.UID == UID).FirstOrDefault();
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            UAParser.ClientInfo c = uaParser.Parse(userAgent);
            if (c.OS.Family == "Windows")
            {
                ViewBag.message = "windows os";

                return Redirect(Link.Microsoft_store);
            }
            if (c.Device.Family == "Android")
            {
                ViewBag.message = "OS And";

                
                return Redirect(Link.Android_url);
            }

            return RedirectToAction("Show");
        }
        [HttpPost]
        public IActionResult Index(Link model)
        {
            if (!_context.Links.Any(x => x.UID == model.UID))
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        var link = new Link();
                        link.UID = model.UID;
                        link.Application_Name = model.Application_Name;
                        link.AppStore_url = model.AppStore_url;
                        link.Android_url = model.Android_url;
                        link.BlackBerry_url = model.BlackBerry_url;
                        link.Huawei_url = model.Huawei_url;
                        link.Kindel_url = model.Kindel_url;
                        link.Ipad = model.Ipad;
                        link.Anyother_url = model.Anyother_url;
                        link.Microsoft_store = model.Microsoft_store;
                        _context.Links.Add(link);
                        _context.SaveChanges();
                        return View("Index");
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "APP name is already existed");
            }
            return View("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

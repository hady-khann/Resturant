using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant.WebAPI.Guest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly Response _response;

        public HomeController(Response response, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _response = response;
        }


        [Authorize(Roles = "Guest,Resturant,Admin")] //Guest
        [Route("Guest/Test")]
        [HttpPost]
        public Global_Response_DTO<UserDTO> test()
        {
            try
            {
                var token = Request.HttpContext.Session.GetString("Token") ?? Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var UserFromcontext = _httpContext.HttpContext.Items["UserInfo"] as UserDTO;
                return _response.Global_Controller_Result<UserDTO>(UserFromcontext,"token   : " + token + "  email : " + UserFromcontext.Email + "   role: " + UserFromcontext.Role+ "   uid  : " + UserFromcontext.UserID+ "  un : " + UserFromcontext.UserName, true);
            }
            catch (Exception ex)
            {
                return _response.Global_Controller_Result<UserDTO>(null, "Error : " + ex, false);
            }
        }


        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

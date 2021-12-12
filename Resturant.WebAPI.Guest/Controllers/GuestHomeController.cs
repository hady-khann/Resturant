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
    public class GuestHomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly Response _response;

        public GuestHomeController(Response response, IHttpContextAccessor httpContext)
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
                var UserFromcontext = _httpContext.HttpContext.Items["UserInfoDTO"] as UserDTO;
                return _response.Global_Result<UserDTO>(UserFromcontext,"token   : " + token + "  email : " + UserFromcontext.Email + "   role: " + UserFromcontext.Role+ "   uid  : " + UserFromcontext.Id + "  un : " + UserFromcontext.UserName, true);
            }
            catch (Exception ex)
            {
                return _response.Global_Result<UserDTO>(null, "Error : " + ex, false);
            }
        }


        // GET: GuestHomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GuestHomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GuestHomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GuestHomeController/Create
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

        // GET: GuestHomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GuestHomeController/Edit/5
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

        // GET: GuestHomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GuestHomeController/Delete/5
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

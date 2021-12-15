using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Resturant.WebAPI.Resturant.Controllers
{
    public class ResturantHomeController : Controller
    {

        private readonly IHttpContextAccessor _httpContext;
        private readonly Response _response;

        public ResturantHomeController(Response response, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _response = response;
        }



        [Authorize(Roles = "Resturant,Admin")] //Resturant
        [Route("Resturant/Test")]
        [HttpPost]
        public Global_Response_DTO<UserDTO> test()
        {
            try
            {
                //var token = Request.HttpContext.Session.GetString("Token") ?? Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var UserFromcontext = _httpContext.HttpContext.Items["ViwUserInfoDTO"] as UserDTO;
                return _response.Global_Result<UserDTO>(UserFromcontext, User.Identity.Name + "  :  " + User.Claims);
            }
            catch (Exception)
            {
                return _response.Global_Result<UserDTO>(null);
            }
        }



        // GET: ResturantHomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ResturantHomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResturantHomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResturantHomeController/Create
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

        // GET: ResturantHomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResturantHomeController/Edit/5
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

        // GET: ResturantHomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResturantHomeController/Delete/5
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sdb.Models;
using sdb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdb.Controllers
{
    public class UserRegisterController : Controller
    {
        private readonly ISDBRepository _sdbRepository;

        public UserRegisterController(ISDBRepository sdbRepository)
        {
            _sdbRepository = sdbRepository;
        }

        // GET: UserRegisterController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserRegisterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserRegisterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRegisterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SdbSystemUsers sdbSystemUsers)
        {
            try
            {
                sdbSystemUsers = _sdbRepository.Add(sdbSystemUsers);
                return RedirectToAction(nameof(Index));  // HomeController index action method should be invoked
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRegisterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserRegisterController/Edit/5
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

        // GET: UserRegisterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserRegisterController/Delete/5
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

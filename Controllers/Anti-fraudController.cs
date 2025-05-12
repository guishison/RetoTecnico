using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RetoTecnico.Controllers
{
    public class Anti_fraudController : Controller
    {
        // GET: Anti_fraudController
        public ActionResult Index()
        {
            return View();
        }

        // GET: Anti_fraudController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Anti_fraudController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anti_fraudController/Create
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

        // GET: Anti_fraudController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Anti_fraudController/Edit/5
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

        // GET: Anti_fraudController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Anti_fraudController/Delete/5
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

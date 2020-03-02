using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Universidad.Models;

namespace Universidad.Controllers
{
    public class MaestriaController : Controller
    {
        public IActionResult Lista()
        {
            return View(UniversidadManager.Instance.Maestrias);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Maestria m)
        {
            if (ModelState.IsValid)
            {
                UniversidadManager.Instance.AgregarMaestria(m);
                return RedirectToAction("List");
            }
            return View(m);
        }

        public IActionResult Eliminar(int? id)
        {
            var maestria = UniversidadManager.Instance.ObtenerMaestria((int)id);
            return View(maestria);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            UniversidadManager.Instance.EliminarMaestria(id);
            return RedirectToAction("List");
        }

        public IActionResult Actualizar(int? id)
        {
            var m = UniversidadManager.Instance.ObtenerMaestria((int)id);

            return View(m);
        }

        [HttpPost]
        public IActionResult Actualizar(Maestria n, int id)
        {

            UniversidadManager.Instance.ActualizarMaestria(id, n);
            return RedirectToAction("List");
        }

        public IActionResult Docentes(int? id)
        {
            var m = UniversidadManager.Instance.ObtenerMaestria((int)id);

            return View(m);
        }

        public IActionResult AgregarDocente(int? id)
        {

            var maestria = UniversidadManager.Instance.ObtenerMaestria((int)id);
            return View(maestria);
        }

        [HttpPost]
        public IActionResult AgregarDocente(int idMaestria, int idDocente)
        {
            var m = UniversidadManager.Instance.ObtenerMaestria(idMaestria);
            var d = UniversidadManager.Instance.ObtenerDocente(idDocente);
            if (d != null)
            {
                if (!m.Docentes.Contains(d))
                {
                    m.Docentes.Add(d);
                    d.Maestrias.Add(m);
                    return RedirectToAction("Docentes", new { id = idMaestria });
                }
                else
                {
                    ViewBag.hasError = true;
                    ViewBag.error = $"El docente {d.Nombre} ya imparte la maestría {m.Nombre}";
                }
            }
            return View(m);
        }

        [HttpPost]
        public IActionResult EliminarDocente(int idMaestria, int idDocente)
        {
            var m = UniversidadManager.Instance.ObtenerMaestria(idMaestria);
            var d = UniversidadManager.Instance.ObtenerDocente(idDocente);

            m.Docentes.Remove(d);
            d.Maestrias.Remove(m);

            return RedirectToAction("Docentes", new { id = idMaestria });
        }
    }
}
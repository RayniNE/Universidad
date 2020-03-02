using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Universidad.Models;

namespace Universidad.Controllers
{
    public class UniversidadController : Controller
    {
        // GET: /<controller>/
        public IActionResult Lista()
        {
            return View(UniversidadManager.Instance.Universidades);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Models.Universidad u)
        {
            if (ModelState.IsValid)
            {
                UniversidadManager.Instance.AgregarUniversidad(u);
                return RedirectToAction("List");
            }
            return View(u);
        }

        public IActionResult Eliminar(int? id)
        {
            var u = UniversidadManager.Instance.ObtenerUniversidad((int)id);

            return View(u);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            UniversidadManager.Instance.EliminarUniversidad(id);
            return RedirectToAction("List");
        }

        public IActionResult Actualizar(int? id)
        {
            var u = UniversidadManager.Instance.ObtenerUniversidad((int)id);

            return View(u);
        }

        [HttpPost]
        public IActionResult Actualizar(Models.Universidad n, int id)
        {

            UniversidadManager.Instance.ActualizarUniversidad(id, n);
            return RedirectToAction("List");
        }

        public IActionResult Docentes(int? id)
        {
 
            var u = UniversidadManager.Instance.ObtenerUniversidad((int)id);

            return View(u);
        }

        public IActionResult AgregarDocente(int? id)
        {
 
            var universidad = UniversidadManager.Instance.ObtenerUniversidad((int)id);


            return View(universidad);
        }

        [HttpPost]
        public IActionResult AgregarDocente(int idUniversidad, int idDocente)
        {
            var u = UniversidadManager.Instance.ObtenerUniversidad(idUniversidad);
            var d = UniversidadManager.Instance.ObtenerDocente(idDocente);
            if (d != null)
            {
                if (!u.Docentes.Contains(d))
                {
                    u.Docentes.Add(d);
                    d.Universidades.Add(u);
                    return RedirectToAction("Docentes", new { id = idUniversidad });
                }
                else
                {
                    ViewBag.error = $"El docente{d.Nombre} ya trabaja en la universidad {u.Nombre}";
                }
            }
            return View(u);
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
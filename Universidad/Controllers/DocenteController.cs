using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Universidad.Models;

namespace Universidad.Controllers
{
    public class DocenteController : Controller
    {
        public IActionResult Lista()
        {
            return View(UniversidadManager.Instance.Docentes);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Docente d)
        {
            UniversidadManager.Instance.AgregarDocente(d);
            return View(d);
        }

        public IActionResult Eliminar(int? id)
        {
            var d = UniversidadManager.Instance.ObtenerDocente((int)id); //Se necesita castear el id a tipo INT.
            return View(d);
        }
        [HttpPost]

        public IActionResult Eliminar(int id)
        {
            UniversidadManager.Instance.EliminarDocente(id);
            return RedirectToAction("Lista");
        }
        public IActionResult Actualizar(int? id)
        {
            var d = UniversidadManager.Instance.ObtenerDocente((int)id);
            return View(d);
        }

        [HttpPost]

        public IActionResult Actualizar(int id, Docente d)
        {
            UniversidadManager.Instance.ActualizarDocente(id, d);
            return RedirectToAction("Lista");
        }

        public IActionResult Maestrias(int? id)
        {
            var d = UniversidadManager.Instance.ObtenerDocente((int)id);
            return View(d);
        }


        public IActionResult AgregarMaestrias(int? id)
        {
            var d = UniversidadManager.Instance.ObtenerDocente((int)id);

            return View(d);
        }
        [HttpPost]
        public IActionResult AgregarMaestrias(int idDocente, int idMaestria)
        {
            var d = UniversidadManager.Instance.ObtenerDocente(idDocente);
            var m = UniversidadManager.Instance.ObtenerMaestria(idMaestria);

            if (d != null)
            {
                if (!d.Maestrias.Contains(m))
                {
                    m.Docentes.Add(d);
                    d.Maestrias.Add(m);
                    return RedirectToAction("Maestrias", new { id = idDocente });
                }
                else
                {
                    ViewBag.error = $"La maestria {m.Nombre} con el ID {m.IDmaestria} ya esta siendo impartida por {d.Nombre}";
                }
            }
            return View(m);
        }

        public IActionResult Universidades(int? id)
        {
            var d = UniversidadManager.Instance.ObtenerDocente((int) id);
            return View(d);
        }
        [HttpPost]

        public IActionResult AgregarUniversidad(int idUniversidad, int idDocente)
        {
            var d = UniversidadManager.Instance.ObtenerDocente(idDocente);
            var u = UniversidadManager.Instance.ObtenerUniversidad(idUniversidad);

            if (!d.Universidades.Contains(u))
            {
                u.Docentes.Add(d);
                d.Universidades.Add(u);
                return RedirectToAction("Universidades", new { id = idDocente });
            }
            else
            {
                ViewBag.error = $"El {d.Nombre} ya trabaja en la universidad {u.Nombre}";
            }
            return View(d);
        }

        [HttpPost]
        public IActionResult EliminarUniversidad(int idDocente, int idUniversidad)
        {
            var d = UniversidadManager.Instance.ObtenerDocente(idDocente);
            var u = UniversidadManager.Instance.ObtenerUniversidad(idUniversidad);

            d.Universidades.Remove(u);
            u.Docentes.Remove(d);

            return RedirectToAction("Universidades", new { id = idDocente });
        }

    }
}
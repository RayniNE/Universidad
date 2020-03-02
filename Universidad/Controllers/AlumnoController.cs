using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Universidad.Models;

namespace Universidad.Controllers
{
    public class AlumnoController : Controller
    {
        public IActionResult Lista()
        {
            return View(UniversidadManager.Instance.Alumnos);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Alumno a)
        {
            UniversidadManager.Instance.AgregarAlumno(a);
            List<Alumno> alumn = UniversidadManager.Instance.ObtenerEstudiantes();
            return View("Lista",alumn);
        }

        public IActionResult Eliminar(int? id)
        {
            var a = UniversidadManager.Instance.ObtenerEstudiante((int)id); //Se necesita castear el id a tipo INT.
            return View(a);
        }
        [HttpPost]

        public IActionResult Eliminar(int id)
        {
            UniversidadManager.Instance.EliminarEstudiante(id);
            return RedirectToAction("Lista");
        }
        public IActionResult Actualizar(int? id)
        {
            var a = UniversidadManager.Instance.ObtenerEstudiante((int)id);
            return View(a);
        }

        [HttpPost]

        public IActionResult Actualizar(int id, Alumno a)
        {
            UniversidadManager.Instance.ActualizarEstudiante(id, a);
            return RedirectToAction("Lista");
        }

        public IActionResult Maestrias(int? id)
        {
            var a = UniversidadManager.Instance.ObtenerEstudiante((int)id);
            return View(a);
        }


        public IActionResult AgregarMaestrias(int? id)
        {
            var a = UniversidadManager.Instance.ObtenerEstudiante((int)id);

            return View(a);
        }
        [HttpPost]
        public IActionResult AgregarMaestrias(int idAlumno, int idMaestria)
        {
            var a = UniversidadManager.Instance.ObtenerEstudiante(idAlumno);
            var m = UniversidadManager.Instance.ObtenerMaestria(idMaestria);

            if(m != null)
            {
                if (!a.Maestrias.Contains(m))
                {
                    a.Maestrias.Add(m);
                    return RedirectToAction("Maestrias", new { id = idAlumno });
                }
                else
                {
                    ViewBag.error = $"El estudiante{a.Nombre} con el CURP {a.CURP} ya se encuentra cursando la maestria {a.Maestrias}";
                }
            }
            return View(a);
        }

        [HttpPost]
        public IActionResult EliminarMaestria(int idAlumno, int idMaestria)
        {
            var a = UniversidadManager.Instance.ObtenerEstudiante(idAlumno);
            var m = UniversidadManager.Instance.ObtenerMaestria(idMaestria);

            a.Maestrias.Remove(m);

            return RedirectToAction("Maestrias", new { id = idAlumno });
        }

    }
}
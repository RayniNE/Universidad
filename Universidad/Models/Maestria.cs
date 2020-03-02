using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class Maestria
    {
        public int IDmaestria { get; set; }
        public string Nombre { get; set; }
        public int duracion { get; set; }

        private List<Docente> docentes = new List<Docente>();

        public List<Docente> Docentes
        {
            get
            {
                if(docentes.Count != 0)
                {
                    for(int i = 0; i < docentes.Count; i++)
                    {
                        foreach(Docente docente in UniversidadManager.Instance.Docentes)
                        {
                            if(docentes[i].IDempleado == docente.IDempleado)
                            {
                                docentes[i] = docente;
                            }
                        }
                    }
                }
                return docentes;
            }
        }
    }
}

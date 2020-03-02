using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class Alumno
    {
        public int Boleta { get; set; } //Clave primaria del estudiante, o Key.
        public string Nombre { get; set; }
        public int CURP { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha_nac { get; set; }

        private List<Maestria> maestrias = new List<Maestria>();

        public List<Maestria> Maestrias
        {
            get
            {
                if(maestrias.Count > 0)
                {
                    for(int i = 0; i < maestrias.Count; i++)
                    {
                        foreach(Maestria maester in UniversidadManager.Instance.Maestrias)
                        {
                            if(maestrias[i].IDmaestria == maester.IDmaestria)
                            {
                                maestrias[i] = maester;
                            }
                        }
                    }

                }
                return maestrias;
            }
        }
    }
}

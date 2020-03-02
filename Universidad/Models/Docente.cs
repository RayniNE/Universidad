using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class Docente
    {
        public int IDempleado { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        private List<Universidad> universidades = new List<Universidad>();

        public List<Universidad> Universidades
        {
            get
            {
                if(universidades.Count != 0)
                {
                    for(int i = 0; i < universidades.Count; i++)
                    {
                        foreach(Universidad universidad in UniversidadManager.Instance.Universidades)
                        {
                            if(universidades[i].IDuniversidad == universidad.IDuniversidad)
                            {
                                universidades[i] = universidad;
                            }
                            

                        }
                    }
                }
                return universidades;
            }
        }

        private List<Maestria> maestrias = new List<Maestria>();

        public List<Maestria> Maestrias
        {
            get
            {
                if (maestrias.Count != 0)
                {
                    for (int i = 0; i < maestrias.Count; i++)
                    {
                        foreach (Maestria maester in UniversidadManager.Instance.Maestrias)
                        {
                            if (maestrias[i].IDmaestria == maester.IDmaestria)
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

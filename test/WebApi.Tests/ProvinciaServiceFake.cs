using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Tests
{
    public class ProvinciaServiceFake : IProvinciaService
    {
        private readonly List<Provincia> _provincias;

        public ProvinciaServiceFake()
        {
            _provincias = new List<Provincia>()
            {
                new Provincia()
                {
                    centroide = new Centroide()
                    {
                        lat = -34.6073275,
                        lon = -58.3853642
                    },
                    id = "1",
                    nombre = "Buenos Aires"

                },
                new Provincia()
                {
                    centroide = new Centroide()
                    {
                        lat = -31.4127369,
                        lon = -64.1999124
                    },
                    id = "2",
                    nombre = "Cordoba"
                }
            };
        }

        public IEnumerable<Provincia> GetAll()
        {
            return _provincias;
        }

        public Provincia GetByNombre(string nombre)
        {
            return _provincias.Where(a => a.nombre == nombre).FirstOrDefault();
        }


    }
}

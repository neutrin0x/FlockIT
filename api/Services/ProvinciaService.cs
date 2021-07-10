using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{

    public interface IProvinciaService
    {
        IEnumerable<Provincia> GetAll();
        Provincia GetByNombre(string nombre);
    }


    public class ProvinciaService : IProvinciaService
    {
        string endpoint = $"https://apis.datos.gob.ar/georef/api/provincias";

        public IEnumerable<Provincia> GetAll()
        {
            
            List<Provincia> provincias = new List<Provincia>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = client.GetAsync(endpoint).Result;
                    var json = response.Content.ReadAsStringAsync().Result;
                    var provinciasResponse = JsonSerializer.Deserialize<ProvinciasResponse>(json);

                    foreach (var provincia in provinciasResponse.provincias)
                    {
                        provincias.Add(provincia);
                    }
                }

                Logger.GetInstance().LogInfo($"Web.Api.Services.ProvinciaService: {"Se obtuvo el listado de provincias"}");

            }
            catch (Exception ex)
            {
                //nombre del servicio y metodo que arroja el error "hardcoded" para simplificar
                Logger.GetInstance().LogError($"Web.Api.Services.ProvinciaService - GetAll: {ex.Message}");
            }
            return provincias;
        }

        public Provincia GetByNombre(string nombre)
        {
            Provincia provincia = new Provincia();

            using (HttpClient client = new HttpClient())
            {
                endpoint += "?nombre=" + nombre;

                var response = client.GetAsync(endpoint).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                var provinciasResponse = JsonSerializer.Deserialize<ProvinciasResponse>(json);

                foreach (var prov in provinciasResponse.provincias)
                {
                    provincia.centroide = prov.centroide;
                    provincia.id = prov.id;
                    provincia.nombre = prov.nombre;

                }

                return provincia;

            }

            
        }
    }
}
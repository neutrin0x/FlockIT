using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProvinciasController : ControllerBase
    {
        private IProvinciaService _provinciasService;

        public ProvinciasController(IProvinciaService provinciaService)
        {
            _provinciasService = provinciaService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var provincias = _provinciasService.GetAll();
            return Ok(provincias);
        }

        [Authorize]
        [HttpGet("{nombre}")]
        public IActionResult GetByNombre(string nombre)
        {

            var provincia = _provinciasService.GetByNombre(nombre);

            if (provincia == null)
                return NotFound();

            return Ok(provincia);
        }

        


    }
}

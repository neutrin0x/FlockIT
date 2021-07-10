using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Entities;
using WebApi.Services;
using Xunit;

namespace WebApi.Tests
{
    public class ProvinciasControllerTest
    {
        ProvinciasController _controller;
        IProvinciaService _service;

        public ProvinciasControllerTest()
        {
            _service = new ProvinciaServiceFake();
            _controller = new ProvinciasController(_service);
        }

        [Fact]
        public void GetAll_Resultado_Ok()
        {
            // Act
            var okResult = _controller.GetAll() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Provincia>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void GetByNombre_Inexistente()
        {
            // Act
            var failResult = _controller.GetByNombre("Valle Oscuro") as OkObjectResult;
            
            // Assert
            Assert.Null(failResult);
        }
    }
}

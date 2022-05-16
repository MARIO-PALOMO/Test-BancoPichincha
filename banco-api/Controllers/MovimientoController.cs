using banco_dao.ModelosContratos;
using banco_dto.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;

namespace banco_api.Controllers
{
    [ApiController]
    [Route("movimientos")]
    public class MovimientoController : ControllerBase
    {

        private readonly IMovimientoModelo movimientoModelo;
        public MovimientoController(IMovimientoModelo movimientoModelo)
        {
            this.movimientoModelo = movimientoModelo;
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult crear(Movimiento movimiento)
        {
            var resultado = movimientoModelo.crear(movimiento);
            return Ok(resultado);
        }

        [HttpGet]
        [Route("reportes")]
        public IActionResult buscar(DateTime fechaInicio, DateTime fechaFin)
        {
            var resultado = movimientoModelo.buscar(fechaInicio, fechaFin);
            return Ok(resultado);
        }
    }
}

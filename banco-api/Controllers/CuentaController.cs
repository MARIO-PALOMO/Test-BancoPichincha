using banco_dao.ModelosContratos;
using banco_dto.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace banco_api.Controllers
{
    [ApiController]
    [Route("cuentas")]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaModelo cuentaModelo;
        public CuentaController(ICuentaModelo cuentaModelo)
        {
            this.cuentaModelo = cuentaModelo;
        }

        [HttpGet]
        [Route("buscar")]
        public IActionResult buscar()
        {
            var resultado = cuentaModelo.buscar();
            return Ok(resultado);
        }

        [HttpGet]
        [Route("buscarCuentasCliente")]
        public IActionResult buscarCuentasCliente(string identificacion)
        {
            var resultado = cuentaModelo.buscarCuentasCliente(identificacion);
            return Ok(resultado);
        }

        [HttpGet]
        [Route("buscarUno")]
        public IActionResult buscarUno(string numero)
        {
            var resultado = cuentaModelo.buscarUno(numero);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult crear(Cuenta cuenta)
        {
            var resultado = cuentaModelo.crear(cuenta);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("editar")]
        public IActionResult editar(Cuenta cuenta)
        {
            var resultado = cuentaModelo.editar(cuenta);
            return Ok(resultado);
        }

        [HttpDelete]
        [Route("eliminar")]
        public IActionResult eliminar(string numero)
        {
            var resultado = cuentaModelo.eliminar(numero);
            return Ok(resultado);
        }
    }
}

using banco_dao.ModelosContratos;
using banco_dto.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace banco_api.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteModelo clienteModelo;
        public ClienteController(IClienteModelo clienteModelo)
        {
            this.clienteModelo = clienteModelo;
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult crear(Cliente cliente)
        {
            var resultado = clienteModelo.crear(cliente);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("editar")]
        public IActionResult editar(Cliente cliente)
        {
            var resultado = clienteModelo.editar(cliente);
            return Ok(resultado);
        }

        [HttpDelete]
        [Route("eliminar")]
        public IActionResult eliminar(string identificacion)
        {
            var resultado = clienteModelo.eliminar(identificacion);
            return Ok(resultado);
        }

        [HttpGet]
        [Route("buscar")]
        public IActionResult buscar()
        {
            var resultado = clienteModelo.buscar();
            return Ok(resultado);
        }

        [HttpGet]
        [Route("buscarUno")]
        public IActionResult buscarUno(string identificacion)
        {
            var resultado = clienteModelo.buscarUno(identificacion);
            return Ok(resultado);
        }
    }
}

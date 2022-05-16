using banco_dto.Entidades;
using banco_dto.Respuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banco_dao.ModelosContratos
{
    public interface IClienteModelo
    {
        ResponseIntegration<Cliente> buscarUno(string identificacion);
        ResponseIntegration<List<Cliente>> buscar();
        ResponseIntegration<Cliente> crear(Cliente cliente);
        ResponseIntegration<Cliente> editar(Cliente cliente);
        ResponseIntegration<bool> eliminar(string identificacion);
    }
}

using banco_dto.Entidades;
using banco_dto.Respuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banco_dao.ModelosContratos
{
    public interface ICuentaModelo
    {
        ResponseIntegration<List<Cuenta>> buscarCuentasCliente(string identificacion);
        ResponseIntegration<List<Cuenta>> buscar();
        ResponseIntegration<Cuenta> buscarUno(string numero);
        ResponseIntegration<Cuenta> crear(Cuenta cuenta);
        ResponseIntegration<Cuenta> editar(Cuenta cuenta);
        ResponseIntegration<bool> eliminar(string numero);
    }
}

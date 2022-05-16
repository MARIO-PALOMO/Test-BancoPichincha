using banco_dto.Entidades;
using banco_dto.Respuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banco_dao.ModelosContratos
{
    public interface IMovimientoModelo
    {
        ResponseIntegration<Movimiento> crear(Movimiento movimiento);
        ResponseIntegration<List<Movimiento>> buscar(DateTime fechaInicio, DateTime fechaFin);
    }
}

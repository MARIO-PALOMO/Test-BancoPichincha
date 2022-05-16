using banco_dao.Contexto;
using banco_dao.ModelosContratos;
using banco_dto.Entidades;
using banco_dto.Respuestas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banco_dao.Modelos
{
    public class MovimientoModelo: IMovimientoModelo
    {
        private readonly BancoContext dbContext;
        private readonly decimal LimiteDiarioRetiro;
        public IConfiguration configuration;
        private readonly ICuentaModelo cuentaModelo;

        public MovimientoModelo(BancoContext dbContext, IConfiguration configuration, ICuentaModelo cuentaModelo)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.cuentaModelo = cuentaModelo;
            this.LimiteDiarioRetiro = Convert.ToDecimal(configuration.GetSection("LimiteDiarioRetiro").Value);
        }

        public bool consultarCantidadDiarioRetiro(string numeroCuenta)
        {

            DateTime fecha = DateTime.Now;
            string fechaActual = String.Format("{0:yyyy-MM-dd}", fecha);

            var query = from m in dbContext.Movimiento
                        join c in dbContext.Cuenta on m.CuentaId equals c.Id
                        where c.Numero.Equals(numeroCuenta) && m.Tipo.Equals("Retiro") && m.Fecha == fecha
                        select m;

            var consulta = query.ToList();
            var suma = consulta.Sum(m => m.Valor);

            if (suma > LimiteDiarioRetiro)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public decimal consultarSaldoActual(string numeroCuenta)
        {

            DateTimeOffset fecha = DateTimeOffset.Now;
            string fechaActual = String.Format("{0:yyyy-MM-dd}", fecha);

            var query = from m in dbContext.Movimiento
                        join c in dbContext.Cuenta on m.CuentaId equals c.Id
                        where c.Numero.Equals(numeroCuenta)
                        select m;

            var consulta = query.OrderByDescending(m => m.Fecha).FirstOrDefault();

            if (consulta != null)
            {
                var saldoActual = consulta.Saldo;
                return saldoActual;
            }
            else
            {
                var buscarCuenta = cuentaModelo.buscarUno(numeroCuenta);
                var cuenta = buscarCuenta.Datos;
                return cuenta.SaldoInicial;
            }
           
        }

        public ResponseIntegration<Movimiento> crear(Movimiento movimiento)
        {
            ResponseIntegration<Movimiento> resultado = new ResponseIntegration<Movimiento>();

            try
            {
                var saldoActual = consultarSaldoActual(movimiento.Cuenta.Numero);
                if (movimiento.Tipo.Equals("Retiro"))
                {
                    movimiento.Saldo = saldoActual - movimiento.Valor;
                }
                else if (movimiento.Tipo.Equals("Deposito"))
                {
                    movimiento.Saldo = saldoActual + movimiento.Valor;
                }

                var buscarCuenta = cuentaModelo.buscarUno(movimiento.Cuenta.Numero);
                var cuenta = buscarCuenta.Datos;
                if (movimiento.Tipo.Equals("Retiro") && movimiento.Saldo == 0)
                {
                    resultado.Codigo = 200;
                    resultado.Datos = null;
                    resultado.Mensaje = "Saldo no disponible";
                }else if (movimiento.Tipo.Equals("Retiro") && consultarCantidadDiarioRetiro(movimiento.Cuenta.Numero).Equals(false))
                {
                    resultado.Codigo = 200;
                    resultado.Datos = null;
                    resultado.Mensaje = "Cupo diario Excedido";
                }
                else
                {
                    
                    movimiento.Fecha = DateTime.Now;
                    movimiento.CuentaId = cuenta.Id;
                    movimiento.Cuenta.Cliente = cuenta.Cliente;

                    var pDatos = dbContext.Set<Movimiento>();
                    pDatos.Add(movimiento);
                    dbContext.SaveChanges();


                    resultado.Codigo = 200;
                    resultado.Datos = movimiento;
                    resultado.Mensaje = "Exito";
                }
                

            }
            catch (Exception ex)
            {
                resultado.Codigo = 500;
                resultado.Datos = null;
                resultado.Mensaje = ex.Message.ToString();
            }

            return resultado;
        }

        public ResponseIntegration<List<Movimiento>> buscar(DateTime fechaInicio, DateTime fechaFin)
        {
            ResponseIntegration<List<Movimiento>> resultado = new ResponseIntegration<List<Movimiento>>();
            try
            {
                var fechaInicioOffset = new DateTimeOffset(fechaInicio);
                var fechaFinOffset = new DateTimeOffset(fechaFin);

                var query = from m in dbContext.Movimiento
                            join c in dbContext.Cuenta on m.CuentaId equals c.Id
                            where c.Numero.Equals(m.Fecha >= fechaInicioOffset && m.Fecha <= fechaFinOffset)
                            select m;

                var consulta = query.ToList();

                resultado.Codigo = 200;
                resultado.Datos = consulta;
                resultado.Mensaje = "Exito";

            }
            catch (Exception ex)
            {
                resultado.Codigo = 500;
                resultado.Datos = null;
                resultado.Mensaje = ex.Message.ToString();
            }

            return resultado;
        }


    }
}

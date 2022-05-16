using banco_dao.Contexto;
using banco_dao.ModelosContratos;
using banco_dto.Entidades;
using banco_dto.Respuestas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banco_dao.Modelos
{
    public class CuentaModelo : ICuentaModelo
    {
        private readonly BancoContext dbContext;
        public CuentaModelo(BancoContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ResponseIntegration<List<Cuenta>> buscarCuentasCliente(string identificacion)
        {
            ResponseIntegration<List<Cuenta>> resultado = new ResponseIntegration<List<Cuenta>>();

            try
            {
                var query = from c in dbContext.Cuenta
                            join cl in dbContext.Cliente on c.ClienteId equals cl.Id
                            where cl.Persona.Identificacion == identificacion
                            select c;
                var consulta = query.Include(res => res.Cliente).Include(res => res.Cliente.Persona).ToList();

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

        public ResponseIntegration<Cuenta> buscarUno(string numero)
        {
            ResponseIntegration<Cuenta> resultado = new ResponseIntegration<Cuenta>();

            try
            {
                var query = from c in dbContext.Cuenta
                            join cl in dbContext.Cliente on c.ClienteId equals cl.Id
                            where c.Numero == numero
                            select c;
                var consulta = query.Include(res => res.Cliente).Include(res => res.Cliente.Persona).FirstOrDefault();

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

        public ResponseIntegration<List<Cuenta>> buscar()
        {
            ResponseIntegration<List<Cuenta>> resultado = new ResponseIntegration<List<Cuenta>>();
            try
            {
                var query = from c in dbContext.Cuenta
                            join cl in dbContext.Cliente on c.ClienteId equals cl.Id
                            select c;
                var consulta = query.Include(res => res.Cliente).Include(res => res.Cliente.Persona).ToList();

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

        public ResponseIntegration<Cuenta> crear(Cuenta cuenta)
        {
            ResponseIntegration<Cuenta> resultado = new ResponseIntegration<Cuenta>();

            try
            {
                var pDatos = dbContext.Set<Cuenta>();
                pDatos.Add(cuenta);
                dbContext.SaveChanges();


                resultado.Codigo = 200;
                resultado.Datos = cuenta;
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

        public ResponseIntegration<Cuenta> editar(Cuenta cuenta)
        {
            ResponseIntegration<Cuenta> resultado = new ResponseIntegration<Cuenta>();

            try
            {
                var buscarCuenta = buscarUno(cuenta.Numero);
                var consulta = buscarCuenta.Datos;
                if (consulta != null)
                {
                    consulta.Tipo = cuenta.Tipo;
                    consulta.SaldoInicial = cuenta.SaldoInicial;
                    consulta.Estado = cuenta.Estado;

                    dbContext.SaveChanges();

                    resultado.Codigo = 200;
                    resultado.Datos = cuenta;
                    resultado.Mensaje = "Exito";
                }
                else
                {
                    resultado.Codigo = 300;
                    resultado.Datos = null;
                    resultado.Mensaje = "No existe la cuenta";
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

        public ResponseIntegration<bool> eliminar(string numero)
        {
            ResponseIntegration<bool> resultado = new ResponseIntegration<bool>();

            try
            {
                var buscarCliente = buscarUno(numero);
                var consulta = buscarCliente.Datos;

                dbContext.Cuenta.Attach(consulta);
                dbContext.Cuenta.RemoveRange(consulta);
                dbContext.SaveChanges();

                resultado.Codigo = 200;
                resultado.Datos = true;
                resultado.Mensaje = "Exito";

            }
            catch (Exception ex)
            {
                resultado.Codigo = 500;
                resultado.Datos = false;
                resultado.Mensaje = ex.Message.ToString();
            }



            return resultado;
        }


    }
}

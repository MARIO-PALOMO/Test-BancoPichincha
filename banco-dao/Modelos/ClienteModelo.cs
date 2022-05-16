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
    public class ClienteModelo : IClienteModelo
    {
        private readonly BancoContext dbContext;
        public ClienteModelo(BancoContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ResponseIntegration<Cliente> buscarUno(string identificacion)
        {
            ResponseIntegration<Cliente> resultado = new ResponseIntegration<Cliente>();

            try
            {
                var query = from c in dbContext.Cliente
                            join p in dbContext.Persona on c.PersonaId equals p.Id
                            where p.Identificacion == identificacion
                            select c;
                var consulta = query.Include(res => res.Persona).FirstOrDefault();

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

        public ResponseIntegration<List<Cliente>> buscar()
        {
            ResponseIntegration<List<Cliente>> resultado = new ResponseIntegration<List<Cliente>>();
            try
            {
                var query = from c in dbContext.Cliente
                            join p in dbContext.Persona on c.PersonaId equals p.Id
                            select c;
                var consulta = query.Include(res => res.Persona).ToList();

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

        public ResponseIntegration<Cliente> crear(Cliente cliente)
        {
            ResponseIntegration<Cliente> resultado = new ResponseIntegration<Cliente>();

            try
            {
                var pDatos = dbContext.Set<Persona>();
                pDatos.Add(cliente.Persona);
                dbContext.SaveChanges();

                var cDatos = dbContext.Set<Cliente>();
                cDatos.Add(cliente);
                dbContext.SaveChanges();

                resultado.Codigo = 200;
                resultado.Datos = cliente;
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

        public ResponseIntegration<Cliente> editar(Cliente cliente)
        {
            ResponseIntegration<Cliente> resultado = new ResponseIntegration<Cliente>();

            try
            {
                var buscarCliente = buscarUno(cliente.Persona.Identificacion);
                var consulta = buscarCliente.Datos;
                if (consulta != null)
                {
                    consulta.Estado = cliente.Estado;
                    consulta.Contraseña = cliente.Contraseña;
                    consulta.Persona.Identificacion = cliente.Persona.Identificacion;
                    consulta.Persona.Nombre = cliente.Persona.Nombre;
                    consulta.Persona.Genero = cliente.Persona.Genero;
                    consulta.Persona.Edad = cliente.Persona.Edad;
                    consulta.Persona.Direccion = cliente.Persona.Direccion;
                    consulta.Persona.Telefono = cliente.Persona.Telefono;

                    dbContext.SaveChanges();

                    resultado.Codigo = 200;
                    resultado.Datos = cliente;
                    resultado.Mensaje = "Exito";
                }
                else
                {
                    resultado.Codigo = 300;
                    resultado.Datos = null;
                    resultado.Mensaje = "No existe el cliente";
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

        public ResponseIntegration<bool> eliminar(string identificacion)
        {
            ResponseIntegration<bool> resultado = new ResponseIntegration<bool>();

            try
            {
                var buscarCliente = buscarUno(identificacion);
                var consulta = buscarCliente.Datos;

                dbContext.Cliente.Attach(consulta);
                dbContext.Cliente.RemoveRange(consulta);
                dbContext.SaveChanges();

                dbContext.Persona.Attach(consulta.Persona);
                dbContext.Persona.RemoveRange(consulta.Persona);
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

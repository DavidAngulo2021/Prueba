using api_gestion_caso.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api_gestion_caso.Controllers
{
    public class RolController : ApiController
    {
        private string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        ///Registrar un tickets

        [HttpPost]
        public IHttpActionResult Add([FromBody] Models.Rol Tikcket)
        {
            string query = @" insert into persona(id_ticket,usuario,f_creacion,f_actualizacion) values
                                                (@id_ticket,@usuario,@f_creacion,@f_actualizacion)";
            string sqlDataSource = conexion;
            NpgsqlDataReader myReader;
            try
            {
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@id_ticket", Tikcket.id_ticket);
                        myCommand.Parameters.AddWithValue("@usuario", Tikcket.usuario);
                        myCommand.Parameters.AddWithValue("@f_creacion", DateTime.Now);
                        myCommand.Parameters.AddWithValue("@f_actualizacion", DateTime.Now);
                        myReader = myCommand.ExecuteReader();

                        myReader.Close();
                        myCon.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                return Ok("Error: " + ex);
            }

            return Ok("Agregado");
        }
        //Este Metodo es
        ///Consultar un tickets pero tambien se borra ya que esta configurado para mostrar si esta en estado 2 de lo contrario
        ///no mostrara datos
        [HttpGet]
        public IHttpActionResult Get()
        {
            string query = @" select * from persona where estado=2";

            string sqlDataSource = conexion;
            DataTable table = new DataTable();
            NpgsqlDataReader myReader;

            try
            {
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                return Ok("Error: " + ex);
            }

            return Json(table);
        }

        //Editar un tickets
        [HttpPut]
        public IHttpActionResult Put(Models.Rol tik)
        {
            string query = @"update persona set estado=@estado, usuario = @usuario, f_creacion = @f_creacion, f_actualizacion= @f_actualizacion
                where id_ticket=@id_ticket";

            string sqlDataSource = conexion;
            NpgsqlDataReader myReader;
            try
            {
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@id_ticket", tik.id_ticket);
                        myCommand.Parameters.AddWithValue("@usuario", tik.usuario);
                        myCommand.Parameters.AddWithValue("@estado", tik.estado);
                        myCommand.Parameters.AddWithValue("@f_creacion", DateTime.Now);
                        myCommand.Parameters.AddWithValue("@f_actualizacion", DateTime.Now);

                        myReader = myCommand.ExecuteReader();

                        myReader.Close();
                        myCon.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                return Ok("Error: " + ex);
            }

            return Ok("Actualizado");
        }
        // aca se borran los tickets pero tambien se recupera donde  (1 no es activo) y (2 Activo) 


        [HttpDelete]
        public IHttpActionResult Delete(Models.Rol usuario)
        {
         
            string query = @"
                update persona
                set estado = 1
                where id_ticket=@id_ticket  
            ";

            string sqlDataSource = conexion;
            NpgsqlDataReader myReader;
            try
            {
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@id_ticket", usuario.id_ticket);
                        myReader = myCommand.ExecuteReader();

                        myReader.Close();
                        myCon.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                return Ok("Error: " + ex);
            }

            return Ok("Borrado");
        }
    }
    }
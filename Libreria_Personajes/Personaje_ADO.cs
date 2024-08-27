using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Collections.Generic;

namespace Libreria_Personajes
{
    public static class Personaje_ADO
    {
        private static string connectionsString;
        private static MySqlConnection connection;
        private static MySqlCommand command;

        static Personaje_ADO()
        {
            connectionsString = "Server =localhost; Database = superheroes; Uid = root; Pwd =;";
            connection = new MySqlConnection(connectionsString);
            command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = connection;
        }
        public static bool Guardar(Personaje personaje)
        {
            bool exito = true;
            try
            {
                string planMalvado = string.Empty;
                string alianza = string.Empty;

                if (personaje.GetType().Name == typeof(Heroe).Name)
                {
                    alianza = ((Heroe)personaje).Alianza;
                }
                else
                {
                    planMalvado = ((Villano)personaje).PlanMalvado;
                }

                string query = "INSERT INTO personajes(NombrePersonaje,NombreReal,Habilidades,LugarDeOrigen, PlanMalvado,Alianza) VALUES (@NombrePersonaje,@NombreReal,@Habilidades,@LugarDeOrigen,@PlanMalvado,@Alianza)";
                command.Connection.Open();
                command.CommandText = query;
                command.Parameters.Clear();

                //command.Parameters.AddWithValue("@ID",personaje.ID);
                command.Parameters.AddWithValue("@NombrePersonaje", personaje.NombrePersonaje);
                command.Parameters.AddWithValue("@NombreReal", personaje.NombreReal);
                command.Parameters.AddWithValue("@Habilidades", personaje.Habilidades);
                command.Parameters.AddWithValue("@LugarDeOrigen", personaje.LugarDeOrigen);
                command.Parameters.AddWithValue("@planMalvado", planMalvado);
                command.Parameters.AddWithValue("@alianza", alianza);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                if (command.Connection != null && command.Connection.State == System.Data.ConnectionState.Open)
                {
                    command.Connection.Close();
                }
            }
            return exito;
        }


        public static List<Personaje> LeerTodos()
        {
            List<Personaje> list = new List<Personaje>();
            string query = string.Empty;

            try
            {
                query = "SELECT * FROM personajes";
                command.Connection.Open();
                command.CommandText = query;

                //using se encarga de abrir y cerrar archivos
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0); // esa y reader.GetInt32["id"] son formas distintas de hacer lo mismo. 
                        string NombrePersonaje = reader["NombrePersonaje"].ToString();
                        string NombreReal = reader.GetString(2); // otra forma de hacer lo mismo.
                        string Habilidades = string.Empty;
                        string Alianza = string.Empty;
                        string PlanMalvado = string.Empty;

                        if (!reader.IsDBNull(reader.GetOrdinal("Habilidades")))
                        {
                            Habilidades = reader.GetString(3);
                        }
                        string LugarDeOrigen = reader.GetString(4);

                        if (!reader.IsDBNull(reader.GetOrdinal("PlanMalvado")))
                        {
                            PlanMalvado = reader.GetString(5);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Alianza")))
                        {
                            Alianza = reader.GetString(6);
                        }

                        if (string.IsNullOrEmpty(Alianza))
                        {
                            Villano v = new Villano(NombreReal, NombrePersonaje, LugarDeOrigen, PlanMalvado);
                            v.ID = id;
                            v.CargarHabilidadesDesdeString(Habilidades);
                            list.Add(v);
                        }
                        else
                        {
                            Heroe h = new Heroe(NombreReal, NombrePersonaje, LugarDeOrigen, Alianza);
                            h.ID = id;
                            h.CargarHabilidadesDesdeString(Habilidades);
                            list.Add(h);
                        }
                    }
                }
            }
            catch (Exception)
            {
                list = null;
            }
            finally
            {
                if (command.Connection != null && command.Connection.State == System.Data.ConnectionState.Open)
                {
                    command.Connection.Close();
                }
            }


            return list;
        }

        public static Personaje LeerUno(Personaje p)
        {
            Personaje personaje = null;
            try
            {
                string query = "SELECT * FROM personajes WHERE ID = @personajeBuscado";
                command.Connection.Open();
                command.CommandText = query;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@personajeBuscado", p.ID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    int id = reader.GetInt32(0); // esa y reader.GetInt32["id"] son formas distintas de hacer lo mismo. 
                    string NombrePersonaje = reader["NombrePersonaje"].ToString();
                    string NombreReal = reader.GetString(2); // otra forma de hacer lo mismo.
                    string Habilidades = string.Empty;
                    string Alianza = string.Empty;
                    string PlanMalvado = string.Empty;

                    if (!reader.IsDBNull(reader.GetOrdinal("Habilidades")))
                    {
                        Habilidades = reader.GetString(3);
                    }
                    string LugarDeOrigen = reader.GetString(4);

                    if (!reader.IsDBNull(reader.GetOrdinal("PlanMalvado")))
                    {
                        PlanMalvado = reader.GetString(5);
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Alianza")))
                    {
                        Alianza = reader.GetString(6);
                    }

                    if (string.IsNullOrEmpty(Alianza))
                    {
                        Villano v = new Villano(NombreReal, NombrePersonaje, LugarDeOrigen, PlanMalvado);
                        v.ID = id;
                        v.CargarHabilidadesDesdeString(Habilidades);
                        personaje = v;

                    }
                    else
                    {
                        Heroe h = new Heroe( NombreReal, NombrePersonaje, LugarDeOrigen, Alianza);
                        h.ID = id;
                        h.CargarHabilidadesDesdeString(Habilidades);
                        personaje = h;

                    }
                }
            }
            catch (Exception)
            {
                personaje = null;
            }
            finally
            {
                if (command.Connection != null && command.Connection.State == System.Data.ConnectionState.Open)
                {
                    command.Connection.Close();
                }
            }
            return personaje;
        }

        public static bool Eliminar(int idEliminar)
        {
            bool exito = true;

            try
            {
                string query = "DELETE FROM personajes WHERE ID = @personajeBuscado";
                command.Connection.Open();
                command.CommandText = query;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@personajeBuscado",idEliminar);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                exito = false;
            }
            finally 
            {
                if (command.Connection != null && command.Connection.State == System.Data.ConnectionState.Open)
                {
                    command.Connection.Close();
                }
            }
            return exito;
        }

        public static bool Modificar(Personaje p)
        {
            bool exito = true;
            try
            {
                string planMalvado = string.Empty;
                string alianza = string.Empty;

                if (p.GetType().Name == typeof(Heroe).Name)
                {
                    alianza = ((Heroe)p).Alianza;
                }
                else
                {
                    planMalvado = ((Villano)p).PlanMalvado;
                }
                string query = "UPDATE personajes SET NombrePersonaje = @NombrePersonaje,NombreReal = @NombreReal,Habilidades = @Habilidades,LugarDeOrigen = @LugarDeOrigen,PlanMalvado = @PlanMalvado,Alianza = @Alianza WHERE ID = @personajeBuscado";
                command.Connection.Open();
                command.CommandText = query;
                command.Parameters.Clear();

                command.Parameters.AddWithValue("@personajeBuscado", p.ID);

                command.Parameters.AddWithValue("@NombrePersonaje", p.NombrePersonaje);
                command.Parameters.AddWithValue("@NombreReal", p.NombreReal);
                command.Parameters.AddWithValue("@Habilidades", p.Habilidades);
                command.Parameters.AddWithValue("@LugarDeOrigen", p.LugarDeOrigen);
                command.Parameters.AddWithValue("@planMalvado", planMalvado);
                command.Parameters.AddWithValue("@alianza", alianza);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                if (command.Connection != null && command.Connection.State == System.Data.ConnectionState.Open)
                {
                    command.Connection.Close();
                }
            }
            return exito;
        }
        public static bool ModificarPorLugarDeOrigenSegunAlianza(string nuevoValor, string alianza)
        {
            bool exito = true;
            try
            {
                string query = "UPDATE personajes SET LugarDeOrigen = @nuevoValor WHERE Alianza = @alianza ";
                command.Connection.Open();
                command.CommandText = query;
                command.Parameters.Clear();

                command.Parameters.AddWithValue("@nuevoValor",nuevoValor);
                command.Parameters.AddWithValue("@alianza", alianza);


                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                if (command.Connection != null && command.Connection.State == System.Data.ConnectionState.Open)
                {
                    command.Connection.Close();
                }
            }
            return exito;
        }
        public static bool ModificarAlianzaPorRangoDeID(string alianza,int min,int max) //modificar por rango
        {
            bool exito = true;
            try
            {
                string query = "UPDATE personajes SET Alianza = @nuevoValor WHERE id BETWEEN @min and @max "; //creo que seria asi
                command.Connection.Open();
                command.CommandText = query;
                command.Parameters.Clear();

                command.Parameters.AddWithValue("@nuevoValor", alianza);
                command.Parameters.AddWithValue("@min", min);
                command.Parameters.AddWithValue("@max", max);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                if (command.Connection != null && command.Connection.State == System.Data.ConnectionState.Open)
                {
                    command.Connection.Close();
                }
            }
            return exito;
        }

    }
}

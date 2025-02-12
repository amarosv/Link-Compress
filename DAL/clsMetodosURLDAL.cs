using System;
using System.Collections.Generic;
using System.Data;
using link_compress_api.ENT;
using MySql.Data.MySqlClient;  // Usamos el proveedor de MySQL

namespace link_compress_api.DAL
{
    public class clsMetodosURLDAL
    {
        /// <summary>
        /// Función que busca los datos de un enlace acortado en la base de datos por su id
        /// </summary>
        /// <param name="id">ID de la url a buscar</param>
        /// <returns>Datos del enlace acortado</returns>
        public static clsURL findURLByIdDAL(int id)
        {
            clsURL url = null;

            using (MySqlConnection conexion = clsConexionDB.getConexion())
            {
                MySqlCommand miComando = new MySqlCommand();
                MySqlDataReader miLector;

                try
                {
                    // Verificamos si la conexión está abierta
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        miComando.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                        miComando.CommandText = "SELECT * FROM URLS WHERE ID = @id";
                        miComando.Connection = conexion;
                        miLector = miComando.ExecuteReader();

                        if (miLector.HasRows)
                        {
                            while (miLector.Read())
                            {
                                int idBD = (int)miLector["ID"];
                                string urlBD = (string)miLector["URL"];
                                int clicksBD = (int)miLector["CLICKS"];
                                DateTime creationDateBD = (DateTime)miLector["CREATION_DATE"];
                                string aliasBD = (string)miLector["ALIAS"];

                                // Conversion a bool para el campo PRIVATE
                                bool privateBD = miLector["PRIVATE"] != DBNull.Value && Convert.ToBoolean(miLector["PRIVATE"]);

                                url = new clsURL(idBD, urlBD, clicksBD, creationDateBD, aliasBD, privateBD);
                            }
                        }
                        miLector.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    // Puedes hacer un manejo de excepciones adecuado aquí
                    Console.WriteLine($"Error en la base de datos: {ex.Message}");
                    throw;
                }
            }

            return url;
        }

        /// <summary>
        /// Función que busca los datos de un enlace acortado en la base de datos por su alias
        /// </summary>
        /// <param name="alias">Alias del enlace acortado</param>
        /// <returns>Datos del enlace acortado</returns>
        public static clsURL findURLByAliasDAL(String alias)
        {
            clsURL url = null;

            using (MySqlConnection conexion = clsConexionDB.getConexion())
            {
                MySqlCommand miComando = new MySqlCommand();
                MySqlDataReader miLector;

                try
                {
                    // Verificamos si la conexión está abierta
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        miComando.Parameters.Add("@alias", MySqlDbType.String).Value = alias.ToLower();
                        miComando.CommandText = "SELECT * FROM URLS WHERE ALIAS = @alias";
                        miComando.Connection = conexion;
                        miLector = miComando.ExecuteReader();

                        if (miLector.HasRows)
                        {
                            while (miLector.Read())
                            {
                                int idBD = (int)miLector["ID"];
                                string urlBD = (string)miLector["URL"];
                                int clicksBD = (int)miLector["CLICKS"];
                                DateTime creationDateBD = (DateTime)miLector["CREATION_DATE"];
                                string aliasBD = (string)miLector["ALIAS"];

                                // Conversion a bool para el campo PRIVATE
                                bool privateBD = miLector["PRIVATE"] != DBNull.Value && Convert.ToBoolean(miLector["PRIVATE"]);

                                url = new clsURL(idBD, urlBD, clicksBD, creationDateBD, aliasBD, privateBD);
                            }
                        }
                        miLector.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    // Puedes hacer un manejo de excepciones adecuado aquí
                    Console.WriteLine($"Error en la base de datos: {ex.Message}");
                    throw;
                }
            }

            return url;
        }

        /// <summary>
        /// Función que obtiene el url completo de un link corto mediante su alias
        /// </summary>
        /// <param name="alias">Alias del link corto</param>
        /// <param name="ip">Ip del usuario</param>
        /// <returns>URL completa</returns>
        public static String getLongUrlByAliasDAL(String alias, String ip)
        {
            String url = "";
            int clicks = 0;
            int id = 0;

            using (MySqlConnection conexion = clsConexionDB.getConexion())
            {
                MySqlCommand miComando = new MySqlCommand();
                MySqlDataReader miLector;

                try
                {
                    // Verificamos si la conexión está abierta
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        // Preparar el comando para la consulta SELECT
                        miComando.Parameters.Clear();  // Limpiar parámetros anteriores
                        miComando.Parameters.Add("@alias", MySqlDbType.String).Value = alias.ToLower();
                        miComando.CommandText = "SELECT URL, CLICKS, ID FROM URLS WHERE ALIAS = @alias";
                        miComando.Connection = conexion;
                        miLector = miComando.ExecuteReader();

                        if (miLector.Read())
                        {
                            url = (String)miLector["URL"];
                            clicks = (int)miLector["CLICKS"];
                            id = (int)miLector["ID"];
                        }
                        miLector.Close();

                        // Actualizamos las stats
                        updateClicks(alias, clicks);
                        clsMetodosStatsDAL.createStatsDAL(id, ip);
                    }
                }
                catch (MySqlException ex)
                {
                    // Manejo adecuado de excepciones
                    Console.WriteLine($"Error en la base de datos: {ex.Message}");
                    throw;
                }
            }

            return url;
        }

        /// <summary>
        /// Función que actualiza los clicks de un enlace
        /// </summary>
        /// <param name="alias">Alias del enlace</param>
        /// <param name="clicks">Clicks actuales</param>
        private static void updateClicks(String alias, int clicks)
        {
            // Sumamos un click
            clicks = clicks + 1;
            MySqlConnection conexion = new MySqlConnection();
            MySqlCommand miComando = new MySqlCommand();
            MySqlDataReader miLector;

            try
            {
                conexion = clsConexionDB.getConexion();

                miComando.Parameters.Add("@alias", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = alias.ToLower();
                miComando.Parameters.Add("@clicks", MySql.Data.MySqlClient.MySqlDbType.Int64).Value = clicks;

                miComando.CommandText = "UPDATE URLS SET CLICKS = @clicks WHERE ALIAS = @alias";

                miComando.Connection = conexion;

                miComando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        /// <summary>
        /// Función que crea un link acortado
        /// </summary>
        /// <param name="url">URL completa</param>
        /// <returns>Alias</returns>
        public static String createLinkCompressDAL(String url)
        {
            String alias = "";

            MySqlConnection conexion = new MySqlConnection();
            MySqlCommand miComando = new MySqlCommand();
            MySqlDataReader miLector;

            try
            {
                conexion = clsConexionDB.getConexion();

                miComando.Parameters.Add("@url", MySql.Data.MySqlClient.MySqlDbType.Text).Value = url;

                // Comprobamos que el alias no exista
                bool existsAlias;
                
                do
                {
                    alias = generateAlias();
                    existsAlias = checksAlias(alias);
                } while (existsAlias);

                miComando.Parameters.Add("@alias", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = alias;

                miComando.CommandText = "INSERT INTO URLS (URL, ALIAS) VALUES (@url, @alias)";

                miComando.Connection = conexion;

                miComando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }

            return alias;
        }

        /// <summary>
        /// Función que crea un link acortado con un alias personalizado
        /// </summary>
        /// <param name="url">URL completa</param>
        /// <param name="alias">Alias personalizado</param>
        /// <returns>Número de filas afectadas</returns>
        public static int createPersonalizatedLinkCompressDAL(String url, String alias)
        {
            int numeroFilasAfectadas = 0;

            // Comprobamos que el alias no exista
            if (!checksAlias(alias))
            {
                MySqlConnection conexion = new MySqlConnection();
                MySqlCommand miComando = new MySqlCommand();
                MySqlDataReader miLector;

                try
                {
                    conexion = clsConexionDB.getConexion();

                    miComando.Parameters.Add("@url", MySql.Data.MySqlClient.MySqlDbType.Text).Value = url;

                    miComando.Parameters.Add("@alias", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = alias.ToLower();

                    miComando.CommandText = "INSERT INTO URLS (URL, ALIAS) VALUES (@url, @alias)";

                    miComando.Connection = conexion;

                    numeroFilasAfectadas = miComando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conexion.Close();
                }
            }

            return numeroFilasAfectadas;
        }

        /// <summary>
        /// Función que genera un alias aleatorio de 6 caracteres
        /// </summary>
        /// <returns>Alias aleatorio</returns>
        private static String generateAlias()
        {
            // Variable donde se almacenará el alias
            String alias = "";

            // Array de letras
            String[] letters = {
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
                "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
            };

            // Creamos el objeto random
            Random random = new Random();

            // Creamos cuatro partes aleatorias compuestas por letra + número
            String part1 = letters[random.Next(1, letters.Length)] + random.Next(0, 10);
            String part2 = letters[random.Next(1, letters.Length)] + random.Next(0, 10);
            String part3 = letters[random.Next(1, letters.Length)] + random.Next(0, 10);

            // Los añadimos a un array
            String[] parts = { part1, part2, part3 };

            // Shuffle parts y añadirlo al alias
            parts = parts.OrderBy(x => random.Next()).ToArray();

            alias = string.Join("", parts);

            // Devolvemos el alias
            return alias;
        }
        
        /// <summary>
        /// Función que comprueba si el alias ya existe
        /// </summary>
        /// <param name="alias">Alias</param>
        /// <returns>Alias ya existe o no</returns>
        private static bool checksAlias(String alias)
        {
            bool exists = false;

            using (MySqlConnection conexion = clsConexionDB.getConexion())
            {
                try
                {
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        string query = "SELECT 1 FROM URLS WHERE ALIAS = @alias LIMIT 1";
                        using (MySqlCommand miComando = new MySqlCommand(query, conexion))
                        {
                            miComando.Parameters.Add("@alias", MySqlDbType.VarChar).Value = alias;

                            using (MySqlDataReader miLector = miComando.ExecuteReader())
                            {
                                exists = miLector.HasRows; // Si hay filas, existe el alias.
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Error en la base de datos: {ex.Message}");
                    throw;
                }
            }


            return exists;
        }
    }
}

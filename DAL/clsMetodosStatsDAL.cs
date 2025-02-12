using System;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using link_compress_api.ENT;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;

namespace link_compress_api.DAL
{
    public class clsMetodosStatsDAL
    {
        /// <summary>
        /// Función que obtiene unas stats por su ID
        /// </summary>
        /// <param name="id">ID de las stats a buscar</param>
        /// <returns>Stats</returns>

        public static clsStats getStatsByIdDAL(int id)
        {
            clsStats stats = null;

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
                        miComando.CommandText = "SELECT * FROM STATS WHERE ID = @id";
                        miComando.Connection = conexion;
                        miLector = miComando.ExecuteReader();

                        if (miLector.HasRows)
                        {
                            while (miLector.Read())
                            {
                                int idBD = (int)miLector["ID"];
                                int urlIdBD = (int)miLector["URL_ID"];
                                DateTime clickedDateBD = (DateTime)miLector["CLICKED_DATE"];
                                string countryBD = (string)miLector["COUNTRY"];
                                string cityBD = (string)miLector["CITY"];

                                stats = new clsStats(idBD, urlIdBD, clickedDateBD, countryBD, cityBD);
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

            return stats;
        }

        /// <summary>
        /// Función que obtiene unas stats por la ID del link compress
        /// </summary>
        /// <param name="urlId">ID del link compress</param>
        /// <returns>Stats</returns>
        public static clsStats getStatsByURLIdDAL(int urlId)
        {
            clsStats stats = null;

            using (MySqlConnection conexion = clsConexionDB.getConexion())
            {
                MySqlCommand miComando = new MySqlCommand();
                MySqlDataReader miLector;

                try
                {
                    // Verificamos si la conexión está abierta
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        miComando.Parameters.Add("@urlid", MySqlDbType.Int32).Value = urlId;
                        miComando.CommandText = "SELECT * FROM STATS WHERE URL_ID = @urlid";
                        miComando.Connection = conexion;
                        miLector = miComando.ExecuteReader();

                        if (miLector.HasRows)
                        {
                            while (miLector.Read())
                            {
                                int idBD = (int)miLector["ID"];
                                int urlIdBD = (int)miLector["URL_ID"];
                                DateTime clickedDateBD = (DateTime)miLector["CLICKED_DATE"];
                                string countryBD = (string)miLector["COUNTRY"];
                                string cityBD = (string)miLector["CITY"];

                                stats = new clsStats(idBD, urlIdBD, clickedDateBD, countryBD, cityBD);
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

            return stats;
        }

        /// <summary>
        /// Función que crea los stats de un click para el link compress dado su id
        /// </summary>
        /// <param name="urlId">ID del link compress</param>
        /// <returns>Número de filas afectadas</returns>
        public static async Task<int> createStatsDAL(int urlId)
        {
            int numeroFilasAfectadas = 0;

            MySqlConnection conexion = new MySqlConnection();
            MySqlCommand miComando = new MySqlCommand();
            MySqlDataReader miLector;

            try
            {
                conexion = clsConexionDB.getConexion();

                miComando.Parameters.Add("@urlid", MySql.Data.MySqlClient.MySqlDbType.Int64).Value = urlId;

                String[] ubicacion = await getLocation();

                String country = ubicacion[0];
                String city = ubicacion[1];

                miComando.Parameters.Add("@country", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = country;
                miComando.Parameters.Add("@city", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = city;


                miComando.CommandText = "INSERT INTO STATS (URL_ID, COUNTRY, CITY) VALUES (@urlid, @country, @city)";

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

            return numeroFilasAfectadas;
        }


        private static async Task<string[]> getLocation()
        {
            string[] data = new string[2];

            using HttpClient client = new();
            try
            {
                // Obtener la IP pública
                string ipResponse = await client.GetStringAsync("https://api.ipify.org");
                string ip = ipResponse.Trim();

                // Obtener la ubicación basada en la IP
                string url = $"http://ip-api.com/json/{ip}?lang=en";
                string respuesta = await client.GetStringAsync(url);

                using JsonDocument doc = JsonDocument.Parse(respuesta);
                var root = doc.RootElement;

                data[0] = root.GetProperty("country").GetString() ?? "Desconocido";
                data[1] = root.GetProperty("city").GetString() ?? "Desconocido";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo ubicación: {ex.Message}");
            }

            return data;
        }

    }
}

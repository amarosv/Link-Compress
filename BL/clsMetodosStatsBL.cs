using link_compress_api.DAL;
using link_compress_api.ENT;

namespace link_compress_api.BL
{
    public class clsMetodosStatsBL
    {
        /// <summary>
        /// Función que obtiene unas stats por su ID
        /// </summary>
        /// <param name="id">ID de las stats a buscar</param>
        /// <returns>Stats</returns>

        public static clsStats getStatsByIdBL(int id)
        {
            return clsMetodosStatsDAL.getStatsByIdDAL(id);
        }

        /// <summary>
        /// Función que obtiene todas las stats de un link compress dado su alias
        /// </summary>
        /// <param name="alias">Alias de un link compress</param>
        /// <returns>Lista de stats</returns>
        public static List<clsStats> getAllStatsByAliasBL(String alias)
        { 
            return clsMetodosStatsDAL.getAllStatsByAliasDAL(alias);
        }

        /// <summary>
        /// Función que obtiene unas stats por la ID del link compress
        /// </summary>
        /// <param name="urlId">ID del link compress</param>
        /// <returns>Stats</returns>
        public static clsStats getStatsByURLIdBL(int urlId)
        {
            return clsMetodosStatsDAL.getStatsByURLIdDAL(urlId);
        }

        /// <summary>
        /// Función que crea los stats de un click para el link compress dado su id
        /// </summary>
        /// <param name="urlId">ID del link compress</param>
        /// <param name="ip">Ip del usuario</param>
        /// <returns>Número de filas afectadas</returns>
        public static async Task<int> createStatsBL(int urlId, String ip)
        {
            return await clsMetodosStatsDAL.createStatsDAL(urlId, ip);
        }
    }
}

using link_compress_api.DAL;
using link_compress_api.ENT;

namespace link_compress_api.BL
{
    public class clsMetodosURLBL
    {
        /// <summary>
        /// Función que busca los datos de un enlace acortado en la base de datos por su id
        /// </summary>
        /// <param name="id">ID de la url a buscar</param>
        /// <returns>Datos del enlace acortado</returns>
        public static clsURL findURLByIdBL(int id)
        {
            return clsMetodosURLDAL.findURLByIdDAL(id);
        }

        /// <summary>
        /// Función que busca los datos de un enlace acortado en la base de datos por su alias
        /// </summary>
        /// <param name="alias">Alias del enlace acortado</param>
        /// <returns>Datos del enlace acortado</returns>
        public static clsURL findURLByAliasBL(String alias)
        {
            return clsMetodosURLDAL.findURLByAliasDAL(alias);
        }

        /// <summary>
        /// Función que obtiene el url completo de un link corto mediante su alias
        /// </summary>
        /// <param name="alias">Alias del link corto</param>
        /// <param name="ip">Ip del usuario</param>
        /// <returns>URL completa</returns>
        public static String getLongUrlByAliasBL(String alias, String ip)
        {
            return clsMetodosURLDAL.getLongUrlByAliasDAL(alias, ip);
        }

        /// <summary>
        /// Función que crea un link acortado
        /// </summary>
        /// <param name="url">URL completa</param>
        /// <returns>Alias</returns>
        public static String createLinkCompressBL(String url)
        {
            return clsMetodosURLDAL.createLinkCompressDAL(url);
        }

        /// <summary>
        /// Función que crea un link acortado con un alias personalizado
        /// </summary>
        /// <param name="url">URL completa</param>
        /// <param name="alias">Alias personalizado</param>
        /// <returns>Número de filas afectadas</returns>
        public static int createPersonalizatedLinkCompressBL(String url, String alias)
        {
            return clsMetodosURLDAL.createPersonalizatedLinkCompressDAL(url, alias);
        }
    }
}

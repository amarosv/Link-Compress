namespace link_compress_api.ENT
{
    public class clsURL
    {
        #region Atributos
        private int id;
        private String url;
        private int clicks;
        private DateTime creationDate;
        private String alias;
        private bool privado;
        #endregion

        #region Propiedades
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Url
        {
            get { return url; }
            set { url = value; }
        }

        public int Clicks
        {
            get { return clicks; }
            set { clicks = value; }
        }

        public DateTime CreationDate {
            get { return creationDate; } 
            set { creationDate = value; }
        }

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }

        public bool Privado
        {
            get { return privado; }
            set { privado = value; }
        }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor con tres parámetros: url, alias y privado<br>
        /// Principalmente para generar el url corto</br>
        /// </summary>
        /// <param name="url">URL completa</param>
        /// <param name="alias">Alias personalizado del url corto</param>
        /// <param name="privado">Boolean que determina si el enlace puede estar en la sección pública</param>
        public clsURL(String url, String alias, bool privado)
        {
            if (!string.IsNullOrEmpty(url)) { 
                this.url = url;
            }

            if (!string.IsNullOrEmpty(alias))
            {
                this.alias = alias;
            }

            this.privado = privado;
        }

        /// <summary>
        /// Constructor con todos los parámetros<br>
        /// Principalmente para mostrar los datos del enlace</br>
        /// </summary>
        /// <param name="id">ID del enlace</param>
        /// <param name="url">URL completo</param>
        /// <param name="clicks">Número de clicks</param>
        /// <param name="creationDate">Fecha de creación </param>
        /// <param name="alias">Alias del link corto</param>
        /// <param name="privado">Boolean que determina si puede estar en la sección pública</param>
        public clsURL(int id, String url, int clicks, DateTime creationDate, String alias, bool privado) {
            if (id > 0)
            {
                this.id = id;
            }

            if (!string.IsNullOrEmpty(url))
            {
                this.url = url;
            }

            if (clicks >= 0)
            {
                this.clicks = clicks;
            }

            this.creationDate = creationDate;
            
            if (!string.IsNullOrEmpty(alias))
            {
                this.alias = alias;
            }

            this.privado = privado;
        }
        
        /// <summary>
        /// Constructor sin parámetros
        /// </summary>
        public clsURL() { }
        #endregion
    }
}

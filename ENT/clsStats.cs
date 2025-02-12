namespace link_compress_api.ENT
{
    public class clsStats
    {
        #region Atributos
        private int id;
        private int urlId;
        private DateTime clickedDate;
        private String country;
        private String city;
        #endregion

        #region Propiedades
        public int Id
        {
            get { return id; }
            set {
                if (value > 0) { 
                    id = value;
                }
            }
        }

        public int UrlId
        {
            get { return urlId; }
            set {
                if (value > 0)
                {
                    urlId = value;
                }
            }
        }

        public DateTime ClickedDate
        {
            get { return clickedDate; }
            set { clickedDate = value; }
        }

        public String Country { 
            get { return country; }
            set {
                if (!string.IsNullOrEmpty(value))
                {
                    country = value;
                }
            }
        }

        public String City
        {
            get { return city; }
            set {
                if (!string.IsNullOrEmpty(value))
                {
                    city = value;
                }
            }
        }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor con los parámetros urlID, country y city<br>
        /// Principalmente para crear una stat</br>
        /// </summary>
        /// <param name="urlId">ID del link compress</param>
        /// <param name="country">País del usuario que ha clickado</param>
        /// <param name="city">Ciudad del usuario que ha clickado</param>
        public clsStats(int urlId, String country, String city)
        {
            if (urlId > 0)
            {
                this.urlId = urlId;
            }

            if (!string.IsNullOrEmpty(country))
            {
                this.country = country;
            }

            if (!string.IsNullOrEmpty(city))
            {
                this.city = city;
            }
        }

        /// <summary>
        /// Constructor con todos los parámetros<br>
        /// Principalmente para mostrar las estadísticas</br>
        /// </summary>
        /// <param name="id">ID de la stat</param>
        /// <param name="urlId">ID del link compress</param>
        /// <param name="clickedDate">Fecha en la que se ha clickado</param>
        /// <param name="country">País del usuario que ha clickado</param>
        /// <param name="city">Ciudad del usuario que ha clickado</param>
        public clsStats(int id, int urlId, DateTime clickedDate, String country, String city)
        {
            if (id > 0) { 
                this.id = id;
            }

            if (urlId > 0)
            {
                this.urlId = urlId;
            }

            this.clickedDate = clickedDate;

            if (!string.IsNullOrEmpty(country))
            {
                this.country = country;
            }

            if (!string.IsNullOrEmpty(city))
            {
                this.city = city;
            }
        }

        /// <summary>
        /// Constructor sin parámetros
        /// </summary>
        public clsStats() { }
        #endregion
    }
}

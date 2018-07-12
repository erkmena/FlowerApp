namespace FlowerApp.Data.Infrastructure
{
    public static  class ConnectionString
    {
        /// The client channel connection string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FlowerAppConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["FlowerAppConnectionString"].ToString();
        }
    }
}

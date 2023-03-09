namespace AddressBookMulti.DAL
{
    public class DALHelper
    {
        #region DataTable Connection String

        public static string myConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Json").Build().GetConnectionString("myConnectionStrings");
        
        #endregion
    }
}

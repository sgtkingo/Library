
namespace DBControl.SQLConnector
{
    public class ConnectionStringBuilder
    {
        public static string BuildConnectionString_Std(string server, string database, string user_id, string password){
            return $"Server={server};Database={database};User Id={user_id};Password={password};";
        }

        public static string BuildConnectionString_Std_WithInstance(string server, string instance, string database, string user_id, string password)
        {
            return $"Server={server}\\{instance};Database={database};User Id={user_id};Password={password};";
        }

        public static string BuildConnectionString_Trusted(string server, string database)
        {
            return $"Server={server};Database={database};Trusted_Connection = True;";
        }
    }
}

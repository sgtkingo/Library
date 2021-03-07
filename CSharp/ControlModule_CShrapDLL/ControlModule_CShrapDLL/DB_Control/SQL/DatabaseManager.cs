using System.Threading.Tasks;

namespace DBControl.SQLConnector
{
    //TODO dodelat metody pro práci s SQL Serverem
    public class DatabaseManager
    {
        private const string XML_ConnectionString = ""; //TODO
        
        public SQLConnector SQLConnector { get; private set; }

        public DatabaseManager(string connectionString){
            //Use XML_ConnectionString
            NewConnect(connectionString);
        }

        //TODO
        public async Task NewConnect(string connectionString)
        {
            if( SQLConnector != null)
            {
                SQLConnector.Disconnect();
            }
            SQLConnector = SQLConnector.BuildConnector(connectionString);
            await Task.Run( () => SQLConnector.Connect() );
        }

        public void Disconnect(){
            SQLConnector.Disconnect();
        }

        //TODO
        public bool SendData(){
            return false;
        }

        //TODO
        public bool GetData()
        {
            return false;
        }

        public string GetServerInfo(){
            return SQLConnector.GetServerFriendlyName();
        }
    }
}

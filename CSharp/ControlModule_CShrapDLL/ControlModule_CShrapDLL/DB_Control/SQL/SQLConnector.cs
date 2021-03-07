using System;
using System.Data.SqlClient;
using System.Threading;

namespace DBControl.SQLConnector
{
    public class SQLConnector
    {
        private const string messageOK = "Connected!";
        private const string messageConnecting = "Connecting...";
        private const string messageFAIL = "Connection FAIL!";
        private const string messageDisconnected = "Disconnected...";

        public string ServerAddress { get; private set; }
        public string DatabaseName { get; private set; }
        public string Username { get; private set; }
        private string Password { get; set; }

        //Connection Test
        public bool LastTestResult { get; private set; }
        public DateTime? LastTestTime { get; private set; }
        public string LastTestMessage {
            get {
                if ( LastTestResult){
                    return $"{messageOK} [{LastTestTime}]";
                }
                else
                {
                    return $"{messageFAIL} [{LastTestTime}]";
                }
            }
        }

        public bool ConnectionStatus { get; private set; }
        public string ConnectionMessage { get; private set; }

        private string ConnectionString { get; set; }
        private SqlConnection Connection { get; set; }

        private SQLConnector(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.FireInfoMessageEventOnUserErrors = true;
            Connection.InfoMessage += ErrorMessageEventHandler;

            ConnectionString = connectionString;
            ConnectionStatus = false;
            ConnectionMessage = messageDisconnected;

            LastTestResult = false;
            LastTestTime = null;

            ParseConnectionString(connectionString);
        }

        //Použít jen label pro XML?
        public static SQLConnector BuildConnector(string connectionString){
            return new SQLConnector(connectionString);
        }

        public void Connect(){
            Console.WriteLine("Try connect to database..");
            ConnectionMessage = messageConnecting;
            //Delete later
            Thread.Sleep(1000);
            using ( Connection )
            {
                try
                {
                    Connection.Open();
                    ConnectionMessage = messageOK;
                    ConnectionStatus = true;
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"{e.Message}|{e.Source}");
                    ConnectionMessage = $"{messageFAIL}";
                    ConnectionStatus = false;
                }
            }
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnecting database..");
            //delete
            Thread.Sleep(1000);
            try
            {
                Connection.Close();
                ConnectionMessage = messageDisconnected;
                ConnectionStatus = false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}|{e.Source}");
            }
        }

        public bool TestConnection(){
            Console.WriteLine("Testing connection..");

            Connect();
            //Timestamp
            DoTestTimestamp(ConnectionStatus);
            Disconnect();

            return LastTestResult;
        }

        private void DoTestTimestamp(bool status){
            LastTestResult = ConnectionStatus;
            LastTestTime = DateTime.Now;
        }

        private void ParseConnectionString(string connectionString){
            //todo
            this.ServerAddress = "";
            this.DatabaseName = "";
            this.Username = "";
            this.Password = "";
        }

        public string GetServerFriendlyName(){
            return $"{ServerAddress} -> {DatabaseName} ({Username})";
        }

        private void ErrorMessageEventHandler(object sender, SqlInfoMessageEventArgs e)
        {
            string state = Connection.State.ToString();
            string msg = e.Message;
            Console.WriteLine($"Some databese error occours, state: {state}, message: {msg}"); 
        }
    }
}

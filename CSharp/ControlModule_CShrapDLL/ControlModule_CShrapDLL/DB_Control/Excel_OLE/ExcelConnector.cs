using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace UniCtrlMod.DBControl.ExcelConnectorOLE
{
    public class ExcelConnector
    {
        public DataTable Data { get; private set; }

        public string ExcelListName { get; private set; }

        public ExcelConnectionString ConnectionString { get; private set; }

        private OleDbConnection oleDbConnection;
        private OleDbCommand oleDbCommand;
        private OleDbDataAdapter oleDbDataAdapter;

        public ExcelConnector(){ 
            this.Data = new DataTable();
            this.ConnectionString = new ExcelConnectionString();

            Console.WriteLine($"{this.ToString()} ready!");
        }

        public ExcelConnector(string excelFileToConnect) : this(){ 
            ConnectFile(excelFileToConnect);
        }

        public bool ExtractExcelFile(string ListName){ 
            ExcelListName = ListName;
            Console.WriteLine($"Starting  extract {ConnectionString.Data_Source}|{ExcelListName}");

            using (oleDbConnection = new OleDbConnection(ConnectionString.ToString())){ 
                oleDbCommand = new OleDbCommand("Select * From [" + ExcelListName + "$]", oleDbConnection);
                //Try open connection
                try { oleDbConnection.Open(); }
                catch (OleDbException e){ Console.WriteLine(e.ToString()); return false; }
                //Using adapter to send dB command
                oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
                try { 
                    oleDbDataAdapter.Fill(this.Data);
                }
                catch (InvalidOperationException e){ Console.WriteLine(e.ToString()); return false; }
            }
            Console.WriteLine($"Extraction {ConnectionString.Data_Source}|{ExcelListName} success!");
            return true;
        }

        public void ConnectFile(string filePath){ 
            if( File.Exists(filePath) ){ 
                ConnectionString.Data_Source = filePath;
                Console.WriteLine($"File {filePath} connected!");
            }else  Console.WriteLine($"File {filePath} does NOT exist!");
        }
    }
}

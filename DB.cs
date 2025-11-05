using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Net.Security;
using Renci.SshNet;

namespace WTFpa
{
    class DB
    {
        
        public NpgsqlConnection Connection = new NpgsqlConnection();

        public void openConnection()
        {
            
            var client = new SshClient("217.25.228.218", "pavel", "00000000");
            
            
            client.Connect()


                ForwardedPortLocal port = new ForwardedPortLocal( "127.0.0.1", "127.0.0.1", 5432);
                client.AddForwardedPort(port);
                port.Start();

                string connString = $"Server={port.BoundHost}; Database=life test; Port={port.BoundPort};" + "User Id=postgres; Password=12345678;";

                Connection = new NpgsqlConnection(connString);


                Connection.Open();

                 
        }

        public void closeConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
                Connection.Close();
        }

        public NpgsqlConnection GetConnection()
        {
            return Connection;

        }
    }
}

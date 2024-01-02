using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI_System
{
    internal class config
    {
        private string server = Properties.Settings.Default.server;
        private string port = Properties.Settings.Default.port;
        private string uname = Properties.Settings.Default.uname;
        private string db = Properties.Settings.Default.dbase;

        public MySqlConnection connection = new MySqlConnection();

        public void connect ()
        {
            string constring = $"Server={server}; Port={port}; Username={uname}; Password=''; Database={db}; charset=utf8";
            connection = new MySqlConnection(constring);
            connection.Open ();
        }

        public void disconnect ()
        {
            connection.Close ();
            connection.Dispose ();
        }
    }
}

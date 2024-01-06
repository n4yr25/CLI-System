﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLI_System
{
    public partial class Dashboard : Form
    {
       LogRecord logRecord = new LogRecord();
        ManageClass manageClass = new ManageClass();

        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            load();
        }

        public void load()
        {
            config config = new config();
            config.connect();

            String select = "SELECT laboratory.lab_room AS 'LABORATORY ROOM', class.class_section AS 'BLOCK', timein AS TIMEIN, timeout AS TIMEOUT FROM log_record JOIN instructor ON log_record.ins_id=instructor.ins_id JOIN laboratory ON log_record.lab_id=laboratory.lab_id JOIN class on log_record.class_id=class.class_id\r\n";
            MySqlCommand cmd = new MySqlCommand(select, config.connection);
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.Fill(dt);

            this.history_gridview.DataSource = dt;

            config.disconnect();
        }

        private void history_gridview_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            logRecord.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            manageClass.Show();
        }
    }
}

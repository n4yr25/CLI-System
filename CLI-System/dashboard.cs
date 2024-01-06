using MySql.Data.MySqlClient;
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
     
        public string ins_id { get; set; }

        public Dashboard(string ins_id)
        {
            InitializeComponent();
            this.ins_id = ins_id;

            config config = new config();
            config.connect();

            string select = $"SELECT * FROM instructor where ins_id='{ins_id}'";
            MySqlCommand cmd = new MySqlCommand(select, config.connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                this.user_label.Text = dr.GetValue(1).ToString();

            }

            config.disconnect();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            load();
        }

        public void load()
        {
            config config = new config();
            config.connect();

            string select = "SELECT laboratory.lab_room AS 'LABORATORY ROOM', class.class_section AS 'BLOCK', timein AS TIMEIN, timeout AS TIMEOUT FROM log_record JOIN instructor ON log_record.ins_id=instructor.ins_id JOIN laboratory ON log_record.lab_id=laboratory.lab_id JOIN class on log_record.class_id=class.class_id\r\n";
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
          
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManageClass manageClass = new ManageClass(ins_id);

            if (manageClass.IsDisposed == true)
            {
                manageClass = new ManageClass(ins_id);
            }

            manageClass.Show();
        }
    }
}

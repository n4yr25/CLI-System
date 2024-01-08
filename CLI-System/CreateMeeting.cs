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
    public partial class CreateMeeting : Form
    {
        public string ins_id {  get; set; }
       
        public CreateMeeting(string insid)
        {
            InitializeComponent();
            this.ins_id = insid;
        }

        private void CreateMeeting_Load(object sender, EventArgs e)
        {
            populatelab();
            populatesection();
        }

        public void populatelab()
        {
            config config = new config();
            config.connect();

            string query = "SELECT lab_room FROM laboratory";
            MySqlCommand cmd = new MySqlCommand(query, config.connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string values = dr["lab_room"].ToString();
                comboBox1.Items.Add(values);
            }
        }

        public void populatesection()
        {
            config config = new config();
            config.connect();

            string query = $"SELECT class_section FROM class WHERE ins_id='{ins_id}'";
            MySqlCommand cmd = new MySqlCommand(query, config.connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string values = dr["class_section"].ToString();
                comboBox2.Items.Add(values);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void create_button_Click(object sender, EventArgs e)
        {
            config config = new config();
            config.connect();

            string class_section = this.comboBox2.SelectedItem.ToString();
            string lab_room = this.comboBox1.SelectedItem.ToString();

            string add = $"INSERT INTO logrecord(ins_id, class_section, lab_room, timein,timeout) VALUES('{ins_id}', '{class_section}', '{lab_room}', NOW(), '')";
            MySqlCommand cmd = new MySqlCommand(add, config.connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr != null && !dr.IsClosed)
            {
                dr.Close();
            }

            cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", config.connection);
            string logid = cmd.ExecuteScalar().ToString(); 

            ComputerLog computerLog = new ComputerLog(logid);
            if (computerLog.IsDisposed == true)
            {
                computerLog = new ComputerLog(logid);
            }
            computerLog.Show();
            this.Dispose();

            config.disconnect();
        }
    }
}

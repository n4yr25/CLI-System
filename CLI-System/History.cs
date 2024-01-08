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
    public partial class History : Form
    {   
        public string log_id {  get; set; }
        public History(string logid)
        {
            InitializeComponent();
            this.log_id = logid;
        }

        private void History_Load(object sender, EventArgs e)
        {
            load();
        
        }

        public void load()
        {
            config config = new config();
            config.connect();

            string select = $"SELECT comp_log.comp_id AS 'COMPUTER NUMBER', student.stud_number AS 'STUDENT NUMBER', student.stud_lastname AS LASTNAME, student.stud_firstname AS FIRSTNAME FROM comp_log JOIN logrecord ON comp_log.log_id=logrecord.log_id JOIN student ON comp_log.stud_id=student.stud_id WHERE logrecord.log_id='{log_id}'";
            MySqlCommand cmd = new MySqlCommand(select, config.connection);
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.Fill(dt);

            this.dataGridView1.DataSource = dt;

            config.disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            config config = new config();
            config.connect();

            string query = $"UPDATE logrecord SET timeout=NOW() WHERE log_id='{log_id}'";

            MySqlCommand cmd = new MySqlCommand(query, config.connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            config.disconnect();

            this.Dispose();
        }
    }
}

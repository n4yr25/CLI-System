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
    public partial class ManageClass : Form
    {
  
        public ManageClass()
        {
            InitializeComponent();
        }

        private void ManageClass_Load(object sender, EventArgs e)
        {
            load();
        }

        public void load()
        {
            config config = new config();
            config.connect();

            String select = "SELECT class_section AS SECTION, class_subject AS SUBJECT, class_year AS YEAR FROM class";
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
       
        }

        
    }
}

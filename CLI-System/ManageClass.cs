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
        public string ins_id { get; set; }

        public ManageClass(string insid)
        {
            InitializeComponent();
            ins_id = insid;
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
            AddClass addclass = new AddClass(ins_id);
            if (addclass.IsDisposed == true)
            {
                addclass = new AddClass(ins_id);
            }
            addclass.Show();
        }

        private void ManageClass_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void showstud_button_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
        }
    }
}

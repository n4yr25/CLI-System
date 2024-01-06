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
    public partial class AddClass : Form
    {
        public string ins_id {  get; set; }

        public AddClass(string ins_id)
        {
            InitializeComponent();
            this.ins_id = ins_id;
        }

        private void AddClass_Load(object sender, EventArgs e)
        {
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            config config = new config();
            config.connect();

            string add = $"INSERT INTO class(ins_id, class_section, class_subject, class_year) VALUES ('{ins_id}', 'Block {this.block_textfield.Text}', '{this.subject_textfield.Text}', '{this.year_textfield.Text}')";
            MySqlCommand cmd = new MySqlCommand(add, config.connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            config.disconnect();
            this.Hide();
        }

        private void AddClass_FormClosing(object sender, FormClosingEventArgs e)
        {
          
            if (MessageBox.Show("Are you sure you want to close this form?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
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
    public partial class ManageStudent : Form
    {
        public string classid {  get; set; }
        string studid;
        public ManageStudent(string cid)
        {
            InitializeComponent();
            this.classid = cid;
        }

        private void ManageStudent_Load(object sender, EventArgs e)
        {
            load();
        }

        public void cleartext()
        {
            this.studnum_textfield.Text = "";
            this.lname_textfield.Text = "";
            this.gname_textfield.Text = "";
            this.dataGridView1.CurrentRow.Selected = false;
        }

        public void load()
        {
            config config = new config();
            config.connect();

            string select = $"SELECT stud_id AS 'No.',stud_number AS 'STUDENT NUMBER', stud_lastname AS LASTNAME, stud_firstname AS FIRSTNAME FROM student INNER JOIN class ON student.class_id=class.class_id WHERE class.class_id='{classid}'";
            MySqlCommand cmd = new MySqlCommand(select, config.connection);
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.Fill(dt);

            this.dataGridView1.DataSource = dt;

            config.disconnect();
        }

        private void addstud_button_Click(object sender, EventArgs e)
        {
            config config = new config();
            config.connect();

            string add = $"INSERT INTO student(class_id, stud_number, stud_lastname, stud_firstname) VALUES('{classid}','{this.studnum_textfield.Text}','{this.lname_textfield.Text}','{this.gname_textfield.Text}')";
            MySqlCommand cmd = new MySqlCommand(add, config.connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            config.disconnect();
            load();
            cleartext();
        }

        private void editstud_button_Click(object sender, EventArgs e)
        {
            config config = new config();
            config.connect();

            string add = $"UPDATE student SET stud_number='{this.studnum_textfield.Text}', stud_lastname='{this.lname_textfield.Text}', stud_firstname='{this.gname_textfield.Text}' WHERE stud_id='{studid}'";
            MySqlCommand cmd = new MySqlCommand(add, config.connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            config.disconnect();
            load();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            addstud_button.Enabled = false;
            this.dataGridView1.CurrentRow.Selected = true;
            studid = this.dataGridView1.Rows[e.RowIndex].Cells["No."].Value.ToString();
            this.studnum_textfield.Text = this.dataGridView1.Rows[e.RowIndex].Cells["STUDENT NUMBER"].Value.ToString();
            this.lname_textfield.Text = this.dataGridView1.Rows[e.RowIndex].Cells["LASTNAME"].Value.ToString();
            this.gname_textfield.Text = this.dataGridView1.Rows[e.RowIndex].Cells["FIRSTNAME"].Value.ToString();
        }

        private void deletestud_button_Click(object sender, EventArgs e)
        {
            DialogResult alert = MessageBox.Show("Do you want to delete this data?", "Confirmation", MessageBoxButtons.YesNo);
            if (alert == DialogResult.Yes)
            {
                config config = new config();
                config.connect();

                string delete = $"DELETE FROM student WHERE stud_number='{this.studnum_textfield.Text}' AND stud_lastname='{this.lname_textfield.Text}' AND stud_firstname='{this.gname_textfield.Text}'";
                MySqlCommand cmd = new MySqlCommand(delete, config.connection);
                MySqlDataReader dr = cmd.ExecuteReader();
                config.disconnect();
                load();
                cleartext();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            load();
            cleartext();
        }
    }
}

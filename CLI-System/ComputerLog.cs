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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CLI_System
{
    public partial class ComputerLog : Form
    {
        public string log_id {  get; set; }
        string studid;
        public ComputerLog(string logid)
        {
            InitializeComponent();
            this.log_id = logid;
        }

        private void ComputerLog_Load(object sender, EventArgs e)
        {
            load();
            populatecomputer();
        }
        public void populatecomputer()
        {
            config config = new config();
            config.connect();

            string query = $"SELECT * FROM computer WHERE comp_id NOT IN (SELECT comp_id FROM comp_log)";
            MySqlCommand cmd = new MySqlCommand(query, config.connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string values = dr["comp_id"].ToString();
                comboBox1.Items.Add(values);
            }
        }

        public void load()
        {
            config config = new config();
            config.connect();

            string select = $"SELECT stud_id AS 'No.',stud_number AS 'STUDENT NUMBER', stud_lastname AS LASTNAME, stud_firstname AS FIRSTNAME FROM student INNER JOIN class ON student.class_id=class.class_id ";
            MySqlCommand cmd = new MySqlCommand(select, config.connection);
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.Fill(dt);

            this.dataGridView1.DataSource = dt;

            config.disconnect();
        }

        public void loadstud()
        {
            config config = new config();
            config.connect();

            string select = $"SELECT comp_id AS 'COMPUTER NUMBER', student.stud_lastname AS LASTNAME, student.stud_firstname FROM comp_log INNER JOIN student ON comp_log.stud_id=student.stud_id WHERE log_id='{log_id}'";
            MySqlCommand cmd = new MySqlCommand(select, config.connection);
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.Fill(dt);

            this.dataGridView2.DataSource = dt;

            config.disconnect();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dataGridView1.CurrentRow.Selected = true;
            studid = this.dataGridView1.Rows[e.RowIndex].Cells["No."].Value.ToString();
            this.studnum_textfield.Text = this.dataGridView1.Rows[e.RowIndex].Cells["STUDENT NUMBER"].Value.ToString();
            this.lname_textfield.Text = this.dataGridView1.Rows[e.RowIndex].Cells["LASTNAME"].Value.ToString();
            this.gname_textfield.Text = this.dataGridView1.Rows[e.RowIndex].Cells["FIRSTNAME"].Value.ToString();
        }

        private void addstud_button_Click(object sender, EventArgs e)
        {
            config config = new config();
            config.connect();

            string compid = this.comboBox1.SelectedItem.ToString();

            string add = $"INSERT INTO comp_log(comp_id, log_id, stud_id, remarks) VALUES ('{compid}','{log_id}','{studid}','')";
            MySqlCommand cmd = new MySqlCommand(add, config.connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            this.comboBox1.Items.Clear();
            populatecomputer();
            this.comboBox1.Items.Remove(compid);

            config.disconnect();
            loadstud();
        }
    }
}

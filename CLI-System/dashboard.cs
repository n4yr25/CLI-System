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
                this.user_label.Text = dr.GetValue(4).ToString();

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

            string select = $"SELECT log_id, class_section, lab_room, timein, timeout FROM logrecord JOIN instructor ON logrecord.ins_id=instructor.ins_id WHERE instructor.ins_id='{ins_id}' ORDER BY log_id DESC";
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

        private void button2_Click(object sender, EventArgs e)
        {
            CreateMeeting createmeeting = new CreateMeeting(ins_id);
            if(createmeeting.IsDisposed == true)
            {
                createmeeting = new CreateMeeting(ins_id);
            }
            createmeeting.Show();
        }

        private void history_gridview_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string logid = this.history_gridview.Rows[e.RowIndex].Cells["log_id"].Value.ToString();

            History history = new History(logid);
            if(history.IsDisposed == true)
            {
                history = new History(logid);
            }
            history.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            load();
        }
    }
}

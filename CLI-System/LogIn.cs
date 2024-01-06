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
    public partial class LogIn : Form
    {

        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            load();
            this.pword_textfield.UseSystemPasswordChar = true;
        }

        public void load()
        {
            config config = new config();
            config.connect();

            String select = "SELECT * FROM instructor";
            MySqlCommand cmd = new MySqlCommand(select, config.connection);
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.Fill(dt);

            config.disconnect();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            config config = new config();
            config.connect();

            string email = this.email_textfield.Text;
            string password = this.pword_textfield.Text;

            string sql = $"SELECT * FROM instructor where ins_email='{email}' || ins_password='{password}'";

            MySqlCommand cmd = new MySqlCommand(sql, config.connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                string dbemail = dr.GetValue(1).ToString();
                string dbpassword = dr.GetValue(2).ToString();
                string instructor = dr.GetValue(0).ToString();

                if (email == dbemail && password == dbpassword)
                {
                    MessageBox.Show($"Log In Success {instructor}");
                    Dashboard dashboard = new Dashboard(instructor);

                    dashboard.Show();
                    this.Close();
                }
                else if(email != dbemail)
                {
                    MessageBox.Show("You Entered An Invalid Account!");
                }
                else if(password != dbpassword)
                {
                    MessageBox.Show("Invalid Password");
                }
            }
            else
            {
                MessageBox.Show("You Entered An Invalid Account!");
            }
        }
    }
}

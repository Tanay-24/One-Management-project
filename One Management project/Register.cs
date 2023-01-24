using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace One_Management_project
{
    public partial class Register : Form
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; port=3306;username=root;password=");
        public Register()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
            cbGender.Items.Add("Other");
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (!this.txtEmail.Text.Contains('@') || !this.txtEmail.Text.Contains('.'))
            {
                MessageBox.Show("Please Enter A Valid Email", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtPassword.Text != txtCPassword.Text)
            {
                MessageBox.Show("Password doesn't match!", "Error");
                return;
            }

            if (string.IsNullOrEmpty(txtName.Text)  || string.IsNullOrEmpty(cbGender.Text) || string.IsNullOrEmpty(txtUName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtCPassword.Text))
            {
                MessageBox.Show("Please fill out all information!", "Error");
                return;
            }

            else
            {
                conn.Open();

                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM loginform.userinfo WHERE Username = @UserName", conn),
                cmd2 = new MySqlCommand("SELECT * FROM loginform.userinfo WHERE Email = @UserEmail", conn);


                cmd1.Parameters.AddWithValue("@UserName", txtUName.Text);
                cmd2.Parameters.AddWithValue("@UserEmail", txtEmail.Text);

                bool userExists = false, mailExists = false;

                using (var dr1 = cmd1.ExecuteReader())
                    if (userExists = dr1.HasRows) MessageBox.Show("Username not available!");

                using (var dr2 = cmd2.ExecuteReader())
                    if (mailExists = dr2.HasRows) MessageBox.Show("Email not available!");


                if (!(userExists || mailExists))
                {

                    string iquery = "INSERT INTO loginform.userinfo(`ID`,`Name`,`Gender`,`Username`,`Email`, `Password`) VALUES (NULL, '" + txtName.Text + "', '" + cbGender.Text + "', '" + txtUName.Text + "', '" + txtEmail.Text + "', '" + txtPassword.Text + "')";
                    MySqlCommand commandDatabase = new MySqlCommand(iquery, conn);
                    commandDatabase.CommandTimeout = 60;

                    try
                    {
                        MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        // Show any error message.
                        MessageBox.Show(ex.Message);
                    }

                    MessageBox.Show("Account Successfully Created!");

                }

                conn.Close();
                this.Hide();
                Services services = new Services();
                services.ShowDialog();
            }
        }

        private void btnGobacktologin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        private void cbShow_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShow.Checked == true)
            {
                txtCPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtCPassword.UseSystemPasswordChar = true;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}

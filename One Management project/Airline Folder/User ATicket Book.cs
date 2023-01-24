using MySql.Data.MySqlClient;
using One_Management_project.Bus_Folder;
using One_Management_project.Railway_Folder;
using One_Management_project.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace One_Management_project.Airline_Folder
{
    public partial class User_ATicket_Book : Form
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        public User_ATicket_Book()
        {
            InitializeComponent();
        }

        private void User_ATicket_Book_Load(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPassengername.Text == "" || txtNoofseats.Text == "" || cbSource.SelectedItem.ToString() == "" || cbDestination.SelectedItem.ToString() == "" || txtTotalcost.Text == "" || cbPassenegerage.SelectedItem.ToString() == "" || cbGender.SelectedItem.ToString() == "" || dateTimePicker1.Value.Date.ToString() == "" || txtUsermobile.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                conn.Open();
                string iquery = "INSERT INTO ptickets.patickets(`ID`,`PassengerName`,`PassengerAge`,`Gender`,`NoOfSeats`,`Source`,`Destination`,`JourneyDate`,`TicketCost`,`UserMobile`) VALUES (NULL, '" + txtPassengername.Text + "', '" + cbPassenegerage.SelectedItem.ToString() + "', '" + cbGender.SelectedItem.ToString() + "', '" + txtNoofseats.Text + "','" + cbSource.SelectedItem.ToString() + "','" + cbDestination.SelectedItem.ToString() + "', '" + dateTimePicker1.Value.Date.ToString() + "','" + txtTotalcost.Text + "','" + txtUsermobile.Text + "')";
                MySqlCommand commandDatabase = new MySqlCommand(iquery, conn);
                commandDatabase.CommandTimeout = 180;
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                MessageBox.Show("Ticket Added Successfully!");
                conn.Close();
                this.Hide();
                User_Flight user_Flight = new User_Flight();
                user_Flight.ShowDialog();

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassengername.Text = "";
            cbSource.Text = "";
            cbDestination.Text = "";
            txtNoofseats.Text = "";
            txtTotalcost.Text = "";
            dateTimePicker1.Text = "";
            cbGender.Text = "";
            cbPassenegerage.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Flight user_Flight = new User_Flight();
            user_Flight.ShowDialog();
        }
    }
}

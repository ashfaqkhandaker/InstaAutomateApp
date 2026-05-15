using InstaAutomateApp.Database;
using InstaAutomateApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InstaAutomateApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            SignupForm form = new SignupForm();

            form.ShowDialog();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DbHelper db = new DbHelper();

                using (var conn = db.GetConnection())
                {
                    conn.Open();

                    var cmd = new SqlCommand(
                        "SELECT * FROM Users WHERE Email=@e AND Password=@p",
                        conn);

                    cmd.Parameters.AddWithValue("@e", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@p", txtPassword.Text);

                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // SAVE SESSION DATA
                        UserSession.UserId =
                            Convert.ToInt32(reader["Id"]);

                        UserSession.UserName =
                            reader["FullName"].ToString();

                        UserSession.UserRole =
                            reader["Role"].ToString();

                        MessageBox.Show("Login Successful!");

                        // ROLE-BASED DASHBOARD ROUTING

                        // ADMIN
                        if (UserSession.UserRole == "Admin")
                        {
                            Form1 adminDashboard =
                                new Form1();

                            adminDashboard.Show();
                        }

                        // PROVIDER
                        else if (UserSession.UserRole == "Provider")
                        {
                            ProviderDashboard providerDashboard =
                                new ProviderDashboard();

                            providerDashboard.Show();
                        }

                        // CUSTOMER
                        else if (UserSession.UserRole == "Customer")
                        {
                            CustomerDashboard customerDashboard =
                                new CustomerDashboard();

                            customerDashboard.Show();
                        }

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Email or Password!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
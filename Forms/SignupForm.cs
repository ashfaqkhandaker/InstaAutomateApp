using InstaAutomateApp.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InstaAutomateApp
{
    public partial class SignupForm : Form
    {
        public SignupForm()
        {
            InitializeComponent();

            cmbRole.Items.Add("Customer");
            cmbRole.Items.Add("Provider");

            cmbRole.SelectedIndex = 0;
        }

        private void SignupForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCreatAccount_Click(object sender, EventArgs e)
            {
                try
                {
                    DbHelper db = new DbHelper();

                    using (var conn = db.GetConnection())
                    {
                        conn.Open();

                    var cmd = new SqlCommand(
"INSERT INTO Users (FullName, Email, Password, Role) " +
"VALUES (@n, @e, @p, @r)",
conn);

                    cmd.Parameters.AddWithValue("@n", txtName.Text);
                    cmd.Parameters.AddWithValue("@e", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@p", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@r", cmbRole.Text);

                    cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Account Created Successfully!");

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
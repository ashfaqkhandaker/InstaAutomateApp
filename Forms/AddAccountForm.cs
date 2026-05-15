using InstaAutomateApp.Database;
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
    public partial class AddAccountForm : Form
    {
        public AddAccountForm()
        {
            InitializeComponent();
        }



        private void btnSaveAccount_Click(object sender, EventArgs e)
        {
            try
            {
                DbHelper db = new DbHelper();

                using (var conn = db.GetConnection())
                {
                    conn.Open();

                    var cmd = new SqlCommand(
                        "INSERT INTO InstagramAccounts (Username, AccessToken) VALUES (@u, @t)",
                        conn);

                    cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@t", txtToken.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Account Saved!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}

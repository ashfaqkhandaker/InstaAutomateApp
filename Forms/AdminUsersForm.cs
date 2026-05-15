using InstaAutomateApp.Database;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace InstaAutomateApp
{
    public partial class AdminUsersForm : Form
    {
        public AdminUsersForm()
        {
            InitializeComponent();
            LoadUsers();
        }

        public void LoadUsers()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT Id, FullName, Email, Role FROM Users ORDER BY Role, FullName", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridUsers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridUsers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select a user to delete.");
                    return;
                }

                int userId = Convert.ToInt32(gridUsers.SelectedRows[0].Cells["Id"].Value);
                string role = gridUsers.SelectedRows[0].Cells["Role"].Value.ToString();

                if (role == "Admin")
                {
                    MessageBox.Show("Cannot delete Admin accounts.");
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to delete this user?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Users WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("User deleted.");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefreshUsers_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}

using InstaAutomateApp.Database;
using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace InstaAutomateApp
{
    public partial class AddServiceForm : Form
    {
        public AddServiceForm()
        {
            InitializeComponent();
        }

        private void btnSaveService_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtServiceName.Text.Trim() == "")
                {
                    MessageBox.Show("Enter service name.");
                    return;
                }

                if (txtPrice.Text.Trim() == "")
                {
                    MessageBox.Show("Enter service price.");
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price))
                {
                    MessageBox.Show("Enter a valid price.");
                    return;
                }

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    // If the logged-in user is Admin, ProviderId is NULL (platform/direct service).
                    // If the logged-in user is Provider, ProviderId is their own Id.
                    object providerId;
                    if (UserSession.UserRole == "Admin")
                        providerId = DBNull.Value;
                    else
                        providerId = UserSession.UserId;

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Services (ServiceName, Description, Price, ProviderId) " +
                        "VALUES (@name, @desc, @price, @provider)",
                        conn);

                    cmd.Parameters.AddWithValue("@name", txtServiceName.Text.Trim());
                    cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@provider", providerId);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Service added successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

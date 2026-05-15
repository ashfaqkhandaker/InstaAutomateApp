using InstaAutomateApp.Database;
using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace InstaAutomateApp
{
    public partial class EditServiceForm : Form
    {
        private int _serviceId;

        public EditServiceForm(int serviceId)
        {
            InitializeComponent();
            _serviceId = serviceId;
            LoadServiceData();
        }

        // =====================
        // LOAD EXISTING DATA
        // =====================
        private void LoadServiceData()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT ServiceName, Description, Price FROM Services WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", _serviceId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtServiceName.Text = reader["ServiceName"].ToString();
                        txtDescription.Text = reader["Description"].ToString();
                        txtPrice.Text = reader["Price"].ToString();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // SAVE CHANGES
        // =====================
        private void btnUpdateService_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtServiceName.Text == "")
                {
                    MessageBox.Show("Enter service name.");
                    return;
                }
                if (txtPrice.Text == "")
                {
                    MessageBox.Show("Enter service price.");
                    return;
                }

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Services SET " +
                        "ServiceName=@name, Description=@desc, Price=@price " +
                        "WHERE Id=@id AND ProviderId=@pid", conn);
                    cmd.Parameters.AddWithValue("@name", txtServiceName.Text);
                    cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@id", _serviceId);
                    cmd.Parameters.AddWithValue("@pid", UserSession.UserId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Service updated successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

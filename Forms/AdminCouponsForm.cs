using InstaAutomateApp.Database;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace InstaAutomateApp
{
    public partial class AdminCouponsForm : Form
    {
        public AdminCouponsForm()
        {
            InitializeComponent();
            LoadCoupons();
        }

        // =====================
        // LOAD COUPONS
        // =====================
        public void LoadCoupons()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT Id, Code, Discount, ExpiryDate, IsActive FROM Coupons ORDER BY Id DESC",
                        conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridCoupons.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // ADD COUPON
        // =====================
        private void btnAddCoupon_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCouponCode.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a coupon code.");
                    return;
                }
                if (txtDiscount.Text.Trim() == "")
                {
                    MessageBox.Show("Enter discount percentage.");
                    return;
                }

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Coupons (Code, Discount, ExpiryDate, IsActive) " +
                        "VALUES (@code, @disc, @expiry, 1)", conn);
                    cmd.Parameters.AddWithValue("@code", txtCouponCode.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@disc", Convert.ToDecimal(txtDiscount.Text));
                    cmd.Parameters.AddWithValue("@expiry", dtpExpiry.Value.Date);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Coupon added successfully!");
                txtCouponCode.Text = "";
                txtDiscount.Text = "";
                LoadCoupons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // TOGGLE ACTIVE
        // =====================
        private void btnToggleCoupon_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridCoupons.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select a coupon.");
                    return;
                }

                int id = Convert.ToInt32(gridCoupons.SelectedRows[0].Cells["Id"].Value);
                bool isActive = Convert.ToBoolean(gridCoupons.SelectedRows[0].Cells["IsActive"].Value);

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Coupons SET IsActive=@active WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@active", !isActive);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Coupon status updated.");
                LoadCoupons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // DELETE COUPON
        // =====================
        private void btnDeleteCoupon_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridCoupons.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select a coupon to delete.");
                    return;
                }

                int id = Convert.ToInt32(gridCoupons.SelectedRows[0].Cells["Id"].Value);

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Coupons WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Coupon deleted.");
                LoadCoupons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

using InstaAutomateApp.Database;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace InstaAutomateApp
{
    public partial class CheckoutForm : Form
    {
        private decimal _cartTotal = 0;
        private decimal _discountAmount = 0;
        private string _appliedCouponCode = "";

        public CheckoutForm()
        {
            InitializeComponent();
            LoadCartSummary();
        }

        private void LoadCartSummary()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT s.ServiceName, c.Quantity, s.Price, " +
                        "(c.Quantity * s.Price) AS Total " +
                        "FROM Cart c " +
                        "INNER JOIN Services s ON c.ServiceId = s.Id " +
                        "WHERE c.UserId = @uid",
                        conn);
                    da.SelectCommand.Parameters.AddWithValue("@uid", UserSession.UserId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridCheckoutCart.DataSource = dt;

                    SqlCommand totalCmd = new SqlCommand(
                        "SELECT ISNULL(SUM(c.Quantity * s.Price), 0) " +
                        "FROM Cart c INNER JOIN Services s ON c.ServiceId = s.Id " +
                        "WHERE c.UserId = @uid", conn);
                    totalCmd.Parameters.AddWithValue("@uid", UserSession.UserId);
                    _cartTotal = (decimal)totalCmd.ExecuteScalar();

                    UpdateTotalsDisplay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateTotalsDisplay()
        {
            decimal finalTotal = _cartTotal - _discountAmount;
            lblSubtotal.Text = "Subtotal: $" + _cartTotal.ToString("F2");
            lblDiscount.Text = "Discount: -$" + _discountAmount.ToString("F2");
            lblFinalTotal.Text = "Final Total: $" + finalTotal.ToString("F2");
        }

        private void btnApplyCouponCheckout_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtCouponCheckout.Text.Trim();
                if (code == "")
                {
                    MessageBox.Show("Enter a coupon code.");
                    return;
                }

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT Discount, ExpiryDate, IsActive FROM Coupons WHERE Code=@code", conn);
                    cmd.Parameters.AddWithValue("@code", code);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        bool isActive = Convert.ToBoolean(reader["IsActive"]);
                        DateTime expiry = Convert.ToDateTime(reader["ExpiryDate"]);
                        decimal discount = Convert.ToDecimal(reader["Discount"]);
                        reader.Close();

                        if (!isActive) { MessageBox.Show("Coupon is not active."); return; }
                        if (expiry < DateTime.Now) { MessageBox.Show("Coupon has expired."); return; }

                        _discountAmount = _cartTotal * (discount / 100);
                        _appliedCouponCode = code;
                        UpdateTotalsDisplay();
                        MessageBox.Show("Coupon applied! " + discount + "% discount.");
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Invalid coupon code.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPaymentMethod.SelectedItem == null)
                {
                    MessageBox.Show("Please select a payment method.");
                    return;
                }

                decimal finalTotal = _cartTotal - _discountAmount;
                string paymentMethod = cmbPaymentMethod.SelectedItem.ToString();

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    // Fetch cart items — ProviderId may be NULL for platform services
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT c.ServiceId, c.Quantity, s.Price, s.ProviderId " +
                        "FROM Cart c INNER JOIN Services s ON c.ServiceId = s.Id " +
                        "WHERE c.UserId = @uid", conn);
                    da.SelectCommand.Parameters.AddWithValue("@uid", UserSession.UserId);
                    DataTable cartItems = new DataTable();
                    da.Fill(cartItems);

                    if (cartItems.Rows.Count == 0)
                    {
                        MessageBox.Show("Cart is empty!");
                        return;
                    }

                    foreach (DataRow row in cartItems.Rows)
                    {
                        int serviceId = Convert.ToInt32(row["ServiceId"]);
                        int qty = Convert.ToInt32(row["Quantity"]);
                        decimal price = Convert.ToDecimal(row["Price"]);
                        decimal itemTotal = qty * price;

                        // ProviderId is nullable — platform services have NULL provider
                        object providerId = row["ProviderId"] == DBNull.Value
                            ? (object)DBNull.Value
                            : Convert.ToInt32(row["ProviderId"]);

                        // Insert Order
                        SqlCommand orderCmd = new SqlCommand(
                            "INSERT INTO Orders " +
                            "(CustomerId, ServiceId, ProviderId, Quantity, TotalAmount, Status, CreatedAt) " +
                            "VALUES (@cid, @sid, @pid, @qty, @total, 'Pending', @now); " +
                            "SELECT SCOPE_IDENTITY();", conn);
                        orderCmd.Parameters.AddWithValue("@cid", UserSession.UserId);
                        orderCmd.Parameters.AddWithValue("@sid", serviceId);
                        orderCmd.Parameters.AddWithValue("@pid", providerId);
                        orderCmd.Parameters.AddWithValue("@qty", qty);
                        orderCmd.Parameters.AddWithValue("@total", itemTotal);
                        orderCmd.Parameters.AddWithValue("@now", DateTime.Now);
                        int orderId = Convert.ToInt32(orderCmd.ExecuteScalar());

                        // Insert Payment
                        SqlCommand payCmd = new SqlCommand(
                            "INSERT INTO Payments " +
                            "(OrderId, UserId, Amount, PaymentMethod, PaymentStatus, PaymentDate) " +
                            "VALUES (@oid, @uid, @amt, @method, 'Completed', @now)", conn);
                        payCmd.Parameters.AddWithValue("@oid", orderId);
                        payCmd.Parameters.AddWithValue("@uid", UserSession.UserId);
                        payCmd.Parameters.AddWithValue("@amt", itemTotal);
                        payCmd.Parameters.AddWithValue("@method", paymentMethod);
                        payCmd.Parameters.AddWithValue("@now", DateTime.Now);
                        payCmd.ExecuteNonQuery();

                        // Insert DeliveryTracking
                        SqlCommand delivCmd = new SqlCommand(
                            "INSERT INTO DeliveryTracking " +
                            "(OrderId, DeliveryStatus, UpdatedAt) " +
                            "VALUES (@oid, 'Processing', @now)", conn);
                        delivCmd.Parameters.AddWithValue("@oid", orderId);
                        delivCmd.Parameters.AddWithValue("@now", DateTime.Now);
                        delivCmd.ExecuteNonQuery();
                    }

                    // Clear cart
                    SqlCommand clearCart = new SqlCommand(
                        "DELETE FROM Cart WHERE UserId=@uid", conn);
                    clearCart.Parameters.AddWithValue("@uid", UserSession.UserId);
                    clearCart.ExecuteNonQuery();
                }

                MessageBox.Show(
                    "Order placed successfully!\n" +
                    "Payment Method: " + paymentMethod + "\n" +
                    "Total Paid: $" + finalTotal.ToString("F2"),
                    "Order Confirmed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelCheckout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using InstaAutomateApp.Database;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace InstaAutomateApp
{
    public partial class CustomerDashboard : Form
    {
        public CustomerDashboard()
        {
            InitializeComponent();
            lblCustomer.Text = "Welcome " + UserSession.UserName + " (Customer)";
            LoadServices();
            LoadCart();
            LoadOrders();
            LoadStats();
            LoadAccounts();
            LoadFlows();
        }

        // =====================
        // LOAD SERVICES
        // =====================
        public void LoadServices()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    // Load all services (both admin-listed and provider-listed)
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT s.Id, s.ServiceName, s.Description, s.Price, " +
                        "ISNULL(u.FullName, 'Platform') AS Provider " +
                        "FROM Services s " +
                        "LEFT JOIN Users u ON s.ProviderId = u.Id", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridServices.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // LOAD CART
        // =====================
        public void LoadCart()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT c.Id, s.ServiceName, c.Quantity, s.Price, " +
                        "(c.Quantity * s.Price) AS Total " +
                        "FROM Cart c " +
                        "INNER JOIN Services s ON c.ServiceId = s.Id " +
                        "WHERE c.UserId = @uid",
                        conn);
                    da.SelectCommand.Parameters.AddWithValue("@uid", UserSession.UserId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridCart.DataSource = dt;
                }
                UpdateCartTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // LOAD ORDERS
        // =====================
        public void LoadOrders()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT o.Id, s.ServiceName, o.Quantity, o.TotalAmount, " +
                        "o.Status, o.CreatedAt, " +
                        "ISNULL(dt.DeliveryStatus, 'N/A') AS DeliveryStatus " +
                        "FROM Orders o " +
                        "INNER JOIN Services s ON o.ServiceId = s.Id " +
                        "LEFT JOIN DeliveryTracking dt ON dt.OrderId = o.Id " +
                        "WHERE o.CustomerId = @uid " +
                        "ORDER BY o.CreatedAt DESC",
                        conn);
                    da.SelectCommand.Parameters.AddWithValue("@uid", UserSession.UserId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridOrders.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // LOAD INSTAGRAM ACCOUNTS
        // =====================
        public void LoadAccounts()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT * FROM InstagramAccounts WHERE UserId = @uid", conn);
                    da.SelectCommand.Parameters.AddWithValue("@uid", UserSession.UserId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridAccounts.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // LOAD AUTOMATION FLOWS
        // =====================
        public void LoadFlows()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT * FROM AutomationFlows WHERE UserId = @uid", conn);
                    da.SelectCommand.Parameters.AddWithValue("@uid", UserSession.UserId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridFlows.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // LOAD STATS
        // =====================
        public void LoadStats()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmdCart = new SqlCommand(
                        "SELECT COUNT(*) FROM Cart WHERE UserId=@uid", conn);
                    cmdCart.Parameters.AddWithValue("@uid", UserSession.UserId);
                    int cartCount = (int)cmdCart.ExecuteScalar();

                    SqlCommand cmdOrders = new SqlCommand(
                        "SELECT COUNT(*) FROM Orders WHERE CustomerId=@uid", conn);
                    cmdOrders.Parameters.AddWithValue("@uid", UserSession.UserId);
                    int orderCount = (int)cmdOrders.ExecuteScalar();

                    SqlCommand cmdSpent = new SqlCommand(
                        "SELECT ISNULL(SUM(TotalAmount),0) FROM Orders WHERE CustomerId=@uid AND Status='Completed'", conn);
                    cmdSpent.Parameters.AddWithValue("@uid", UserSession.UserId);
                    decimal totalSpent = (decimal)cmdSpent.ExecuteScalar();

                    lblCartItems.Text = "Cart Items: " + cartCount;
                    lblTotalOrders.Text = "Total Orders: " + orderCount;
                    lblTotalSpent.Text = "Total Spent: $" + totalSpent.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // UPDATE CART TOTAL
        // =====================
        private void UpdateCartTotal()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT ISNULL(SUM(c.Quantity * s.Price), 0) " +
                        "FROM Cart c INNER JOIN Services s ON c.ServiceId = s.Id " +
                        "WHERE c.UserId = @uid", conn);
                    cmd.Parameters.AddWithValue("@uid", UserSession.UserId);
                    decimal total = (decimal)cmd.ExecuteScalar();
                    lblCartTotal.Text = "Cart Total: $" + total.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // ADD TO CART
        // =====================
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridServices.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a service to add to cart.");
                    return;
                }

                int serviceId = Convert.ToInt32(
                    gridServices.SelectedRows[0].Cells["Id"].Value);

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    SqlCommand checkCmd = new SqlCommand(
                        "SELECT Id FROM Cart WHERE UserId=@uid AND ServiceId=@sid", conn);
                    checkCmd.Parameters.AddWithValue("@uid", UserSession.UserId);
                    checkCmd.Parameters.AddWithValue("@sid", serviceId);
                    object existing = checkCmd.ExecuteScalar();

                    if (existing != null)
                    {
                        SqlCommand updateCmd = new SqlCommand(
                            "UPDATE Cart SET Quantity = Quantity + 1 " +
                            "WHERE UserId=@uid AND ServiceId=@sid", conn);
                        updateCmd.Parameters.AddWithValue("@uid", UserSession.UserId);
                        updateCmd.Parameters.AddWithValue("@sid", serviceId);
                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand insertCmd = new SqlCommand(
                            "INSERT INTO Cart (UserId, ServiceId, Quantity) " +
                            "VALUES (@uid, @sid, 1)", conn);
                        insertCmd.Parameters.AddWithValue("@uid", UserSession.UserId);
                        insertCmd.Parameters.AddWithValue("@sid", serviceId);
                        insertCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Service added to cart!");
                LoadCart();
                LoadStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // REMOVE FROM CART
        // =====================
        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridCart.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a cart item to remove.");
                    return;
                }

                int cartId = Convert.ToInt32(
                    gridCart.SelectedRows[0].Cells["Id"].Value);

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Cart WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", cartId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Item removed from cart.");
                LoadCart();
                LoadStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // CHECKOUT
        // =====================
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM Cart WHERE UserId=@uid", conn);
                    checkCmd.Parameters.AddWithValue("@uid", UserSession.UserId);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Your cart is empty!");
                        return;
                    }
                }

                CheckoutForm form = new CheckoutForm();
                form.ShowDialog();

                LoadCart();
                LoadOrders();
                LoadStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // APPLY COUPON
        // =====================
        private void btnApplyCoupon_Click(object sender, EventArgs e)
        {
            try
            {
                string couponCode = txtCoupon.Text.Trim();

                if (couponCode == "")
                {
                    MessageBox.Show("Enter a coupon code.");
                    return;
                }

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "SELECT Discount, ExpiryDate, IsActive FROM Coupons " +
                        "WHERE Code=@code", conn);
                    cmd.Parameters.AddWithValue("@code", couponCode);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        bool isActive = Convert.ToBoolean(reader["IsActive"]);
                        DateTime expiry = Convert.ToDateTime(reader["ExpiryDate"]);
                        decimal discount = Convert.ToDecimal(reader["Discount"]);

                        reader.Close();

                        if (!isActive)
                        {
                            MessageBox.Show("This coupon is not active.");
                            return;
                        }

                        if (expiry < DateTime.Now)
                        {
                            MessageBox.Show("This coupon has expired.");
                            return;
                        }

                        SqlCommand totalCmd = new SqlCommand(
                            "SELECT ISNULL(SUM(c.Quantity * s.Price), 0) " +
                            "FROM Cart c INNER JOIN Services s ON c.ServiceId = s.Id " +
                            "WHERE c.UserId = @uid", conn);
                        totalCmd.Parameters.AddWithValue("@uid", UserSession.UserId);
                        decimal cartTotal = (decimal)totalCmd.ExecuteScalar();

                        decimal discountAmount = cartTotal * (discount / 100);
                        decimal newTotal = cartTotal - discountAmount;

                        lblCartTotal.Text = "Cart Total: $" + newTotal.ToString("F2") +
                            " (Coupon: " + discount + "% off)";

                        MessageBox.Show("Coupon applied! You saved $" +
                            discountAmount.ToString("F2"));
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

        // =====================
        // ADD INSTAGRAM ACCOUNT
        // =====================
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            AddAccountForm form = new AddAccountForm();
            form.ShowDialog();
            LoadAccounts();
        }

        // =====================
        // ADD AUTOMATION FLOW
        // =====================
        private void btnAddFlow_Click(object sender, EventArgs e)
        {
            AddFlowForm form = new AddFlowForm();
            form.ShowDialog();
            LoadFlows();
        }

        // =====================
        // EDIT AUTOMATION FLOW
        // =====================
        private void btnEditFlow_Click(object sender, EventArgs e)
        {
            if (gridFlows.CurrentRow == null)
            {
                MessageBox.Show("Select a flow first!");
                return;
            }

            int id = Convert.ToInt32(gridFlows.CurrentRow.Cells["Id"].Value);

            AddFlowForm form = new AddFlowForm();
            form.LoadFlowData(id);
            form.ShowDialog();

            LoadFlows();
        }

        // =====================
        // DELETE AUTOMATION FLOW
        // =====================
        private void btnDeleteFlow_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridFlows.CurrentRow == null)
                {
                    MessageBox.Show("Select a flow first!");
                    return;
                }

                int id = Convert.ToInt32(gridFlows.CurrentRow.Cells["Id"].Value);

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM AutomationFlows WHERE Id=@id AND UserId=@uid", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@uid", UserSession.UserId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Flow deleted!");
                LoadFlows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // SIMULATE AUTOMATION
        // =====================
        private void btnSimulate_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridFlows.CurrentRow == null)
                {
                    MessageBox.Show("Select a flow to simulate first!");
                    return;
                }

                string postUrl = gridFlows.CurrentRow.Cells["PostUrl"].Value?.ToString() ?? "";
                string comment = gridFlows.CurrentRow.Cells["ReplyMessage"].Value?.ToString() ?? "";

                InstaAutomateApp.Services.AutomationEngine engine = new InstaAutomateApp.Services.AutomationEngine();
                engine.ProcessComment(postUrl, comment);

                MessageBox.Show("Automation Simulated Successfully!", "Simulation Result",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // SEARCH FLOWS
        // =====================
        private void btnSearchFlow_Click(object sender, EventArgs e)
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT * FROM AutomationFlows WHERE UserId=@uid AND Keywords LIKE @k", conn);
                    da.SelectCommand.Parameters.AddWithValue("@uid", UserSession.UserId);
                    da.SelectCommand.Parameters.AddWithValue("@k", "%" + txtSearchFlow.Text + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridFlows.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // REFRESH ALL
        // =====================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadServices();
            LoadCart();
            LoadOrders();
            LoadStats();
            LoadAccounts();
            LoadFlows();
        }
    }
}

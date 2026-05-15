using InstaAutomateApp.Database;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace InstaAutomateApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Admin dashboard – show admin identity
            lblCurrentUser.Text =
                "Admin Dashboard — Logged in as: " +
                UserSession.UserName;

            LoadServices();
            LoadUsers();
            LoadDashboardStats();

            refreshTimer.Start();

            MessageBox.Show(
                "Welcome to the Admin Dashboard!",
                "Admin Panel",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // =====================
        // LOAD SERVICES (Admin manages the catalog)
        // =====================
        public void LoadServices()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT s.Id, s.ServiceName, s.Description, s.Price, " +
                        "ISNULL(u.FullName, 'Platform') AS Provider " +
                        "FROM Services s " +
                        "LEFT JOIN Users u ON s.ProviderId = u.Id " +
                        "ORDER BY s.Id DESC", conn);
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
        // ADD PLATFORM SERVICE (Admin adds services with no provider — direct purchase)
        // =====================
        private void btnAddService_Click(object sender, EventArgs e)
        {
            AddServiceForm form = new AddServiceForm();
            form.ShowDialog();
            LoadServices();
            LoadDashboardStats();
        }

        // =====================
        // DELETE SERVICE
        // =====================
        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridServices.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select a service first.");
                    return;
                }

                int id = Convert.ToInt32(gridServices.SelectedRows[0].Cells["Id"].Value);

                if (MessageBox.Show("Delete this service?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Services WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Service deleted.");
                LoadServices();
                LoadDashboardStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // LOAD USERS
        // =====================
        public void LoadUsers()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT Id, FullName, Email, Role FROM Users ORDER BY Role", conn);
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

        // =====================
        // DASHBOARD STATS
        // =====================
        public void LoadDashboardStats()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmdServices = new SqlCommand(
                        "SELECT COUNT(*) FROM Services", conn);
                    int serviceCount = (int)cmdServices.ExecuteScalar();

                    SqlCommand cmdOrders = new SqlCommand(
                        "SELECT COUNT(*) FROM Orders", conn);
                    int orderCount = (int)cmdOrders.ExecuteScalar();

                    SqlCommand cmdCustomers = new SqlCommand(
                        "SELECT COUNT(*) FROM Users WHERE Role='Customer'", conn);
                    int customerCount = (int)cmdCustomers.ExecuteScalar();

                    SqlCommand cmdRevenue = new SqlCommand(
                        "SELECT ISNULL(SUM(TotalAmount),0) FROM Orders WHERE Status='Completed'", conn);
                    decimal revenue = (decimal)cmdRevenue.ExecuteScalar();

                    lblServices.Text = "Services: " + serviceCount;
                    lblOrders.Text = "Orders: " + orderCount;
                    lblCustomers.Text = "Customers: " + customerCount;
                    lblRevenue.Text = "Revenue: $" + revenue.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            LoadDashboardStats();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadServices();
            LoadUsers();
            LoadDashboardStats();
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            AdminUsersForm form = new AdminUsersForm();
            form.ShowDialog();
            LoadUsers();
        }

        private void btnViewAllOrders_Click(object sender, EventArgs e)
        {
            AdminOrdersForm form = new AdminOrdersForm();
            form.ShowDialog();
        }

        private void btnPaymentOverview_Click(object sender, EventArgs e)
        {
            AdminPaymentsForm form = new AdminPaymentsForm();
            form.ShowDialog();
        }

        private void btnManageCoupons_Click(object sender, EventArgs e)
        {
            AdminCouponsForm form = new AdminCouponsForm();
            form.ShowDialog();
        }

        private void btnDeliveryMonitor_Click(object sender, EventArgs e)
        {
            AdminDeliveryForm form = new AdminDeliveryForm();
            form.ShowDialog();
        }

        private void btnRevenueStats_Click(object sender, EventArgs e)
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmdRevenue = new SqlCommand(
                        "SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders WHERE Status='Completed'", conn);
                    decimal totalRevenue = (decimal)cmdRevenue.ExecuteScalar();

                    SqlCommand cmdOrders = new SqlCommand(
                        "SELECT COUNT(*) FROM Orders", conn);
                    int totalOrders = (int)cmdOrders.ExecuteScalar();

                    SqlCommand cmdUsers = new SqlCommand(
                        "SELECT COUNT(*) FROM Users WHERE Role='Customer'", conn);
                    int totalCustomers = (int)cmdUsers.ExecuteScalar();

                    SqlCommand cmdProviders = new SqlCommand(
                        "SELECT COUNT(*) FROM Users WHERE Role='Provider'", conn);
                    int totalProviders = (int)cmdProviders.ExecuteScalar();

                    SqlCommand cmdServices = new SqlCommand(
                        "SELECT COUNT(*) FROM Services", conn);
                    int totalServices = (int)cmdServices.ExecuteScalar();

                    string stats =
                        "====== Revenue Analytics ======\n" +
                        "Total Revenue:   $" + totalRevenue.ToString("F2") + "\n" +
                        "Total Orders:    " + totalOrders + "\n" +
                        "Total Customers: " + totalCustomers + "\n" +
                        "Total Providers: " + totalProviders + "\n" +
                        "Total Services:  " + totalServices;

                    MessageBox.Show(stats, "Revenue Analytics",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

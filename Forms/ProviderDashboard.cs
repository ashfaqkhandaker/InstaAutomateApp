using InstaAutomateApp.Database;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace InstaAutomateApp
{
    public partial class ProviderDashboard : Form
    {
        public ProviderDashboard()
        {
            InitializeComponent();
            LoadServices();
            LoadOrders();
            LoadStats();

            lblProvider.Text = "Welcome " + UserSession.UserName + " (Provider)";
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void gridServices_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

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
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT * FROM Services WHERE ProviderId=@id", conn);
                    da.SelectCommand.Parameters.AddWithValue("@id", UserSession.UserId);
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
                        "SELECT o.Id, u.FullName AS Customer, s.ServiceName, " +
                        "o.Quantity, o.TotalAmount, o.Status, o.CreatedAt, " +
                        "ISNULL(dt.DeliveryStatus, 'N/A') AS DeliveryStatus " +
                        "FROM Orders o " +
                        "INNER JOIN Users u ON o.CustomerId = u.Id " +
                        "INNER JOIN Services s ON o.ServiceId = s.Id " +
                        "LEFT JOIN DeliveryTracking dt ON dt.OrderId = o.Id " +
                        "WHERE o.ProviderId = @pid " +
                        "ORDER BY o.CreatedAt DESC", conn);
                    da.SelectCommand.Parameters.AddWithValue("@pid", UserSession.UserId);
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

                    SqlCommand cmdSvc = new SqlCommand(
                        "SELECT COUNT(*) FROM Services WHERE ProviderId=@id", conn);
                    cmdSvc.Parameters.AddWithValue("@id", UserSession.UserId);
                    int svcCount = (int)cmdSvc.ExecuteScalar();

                    SqlCommand cmdOrders = new SqlCommand(
                        "SELECT COUNT(*) FROM Orders WHERE ProviderId=@id", conn);
                    cmdOrders.Parameters.AddWithValue("@id", UserSession.UserId);
                    int orderCount = (int)cmdOrders.ExecuteScalar();

                    SqlCommand cmdPending = new SqlCommand(
                        "SELECT COUNT(*) FROM Orders WHERE ProviderId=@id AND Status='Pending'", conn);
                    cmdPending.Parameters.AddWithValue("@id", UserSession.UserId);
                    int pendingCount = (int)cmdPending.ExecuteScalar();

                    SqlCommand cmdEarnings = new SqlCommand(
                        "SELECT ISNULL(SUM(TotalAmount),0) FROM Orders WHERE ProviderId=@id AND Status='Completed'", conn);
                    cmdEarnings.Parameters.AddWithValue("@id", UserSession.UserId);
                    decimal earnings = (decimal)cmdEarnings.ExecuteScalar();

                    lblTotalServices.Text = "Services: " + svcCount;
                    lblTotalOrders.Text = "Orders: " + orderCount;
                    lblPendingOrders.Text = "Pending: " + pendingCount;
                    lblTotalEarnings.Text = "Earnings: $" + earnings.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // ADD SERVICE
        // =====================
        private void btnAddService_Click(object sender, EventArgs e)
        {
            AddServiceForm form = new AddServiceForm();
            form.ShowDialog();
            LoadServices();
            LoadStats();
        }

        // =====================
        // EDIT SERVICE
        // =====================
        private void btnEditService_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridServices.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select a service to edit.");
                    return;
                }

                int id = Convert.ToInt32(
                    gridServices.SelectedRows[0].Cells["Id"].Value);

                EditServiceForm form = new EditServiceForm(id);
                form.ShowDialog();
                LoadServices();
                LoadStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

                int id = Convert.ToInt32(
                    gridServices.SelectedRows[0].Cells["Id"].Value);

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Services WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Service Deleted!");
                LoadServices();
                LoadStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // UPDATE ORDER DELIVERY STATUS
        // =====================
        private void btnUpdateDelivery_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridOrders.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select an order to update delivery status.");
                    return;
                }

                int orderId = Convert.ToInt32(
                    gridOrders.SelectedRows[0].Cells["Id"].Value);

                string[] statuses = { "Processing", "Shipped", "Delivered", "Cancelled" };
                string selected = "";

                // Simple input dialog using combo
                Form dlg = new Form();
                dlg.Text = "Update Delivery Status";
                dlg.Size = new System.Drawing.Size(350, 180);
                dlg.StartPosition = FormStartPosition.CenterParent;

                Label lbl = new Label();
                lbl.Text = "Select new delivery status:";
                lbl.Location = new System.Drawing.Point(20, 20);
                lbl.AutoSize = true;

                ComboBox cmb = new ComboBox();
                cmb.Items.AddRange(statuses);
                cmb.Location = new System.Drawing.Point(20, 50);
                cmb.Size = new System.Drawing.Size(280, 30);
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.SelectedIndex = 0;

                Button btnOk = new Button();
                btnOk.Text = "Update";
                btnOk.Location = new System.Drawing.Point(20, 100);
                btnOk.Click += (s, ev) => { selected = cmb.SelectedItem.ToString(); dlg.Close(); };

                dlg.Controls.Add(lbl);
                dlg.Controls.Add(cmb);
                dlg.Controls.Add(btnOk);
                dlg.ShowDialog();

                if (selected == "") return;

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    // Check if DeliveryTracking row exists
                    SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM DeliveryTracking WHERE OrderId=@oid", conn);
                    checkCmd.Parameters.AddWithValue("@oid", orderId);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists > 0)
                    {
                        SqlCommand updCmd = new SqlCommand(
                            "UPDATE DeliveryTracking SET DeliveryStatus=@status, UpdatedAt=@now " +
                            "WHERE OrderId=@oid", conn);
                        updCmd.Parameters.AddWithValue("@status", selected);
                        updCmd.Parameters.AddWithValue("@now", DateTime.Now);
                        updCmd.Parameters.AddWithValue("@oid", orderId);
                        updCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand insCmd = new SqlCommand(
                            "INSERT INTO DeliveryTracking (OrderId, DeliveryStatus, UpdatedAt) " +
                            "VALUES (@oid, @status, @now)", conn);
                        insCmd.Parameters.AddWithValue("@oid", orderId);
                        insCmd.Parameters.AddWithValue("@status", selected);
                        insCmd.Parameters.AddWithValue("@now", DateTime.Now);
                        insCmd.ExecuteNonQuery();
                    }

                    // If delivered, update order status
                    if (selected == "Delivered")
                    {
                        SqlCommand orderCmd = new SqlCommand(
                            "UPDATE Orders SET Status='Completed' WHERE Id=@oid", conn);
                        orderCmd.Parameters.AddWithValue("@oid", orderId);
                        orderCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Delivery status updated to: " + selected);
                LoadOrders();
                LoadStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

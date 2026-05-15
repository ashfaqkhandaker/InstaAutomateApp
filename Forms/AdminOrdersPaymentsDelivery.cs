using InstaAutomateApp.Database;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace InstaAutomateApp
{
    // =====================
    // ADMIN ORDERS FORM
    // =====================
    public partial class AdminOrdersForm : Form
    {
        public AdminOrdersForm()
        {
            InitializeComponent();
            LoadAllOrders();
        }

        public void LoadAllOrders()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT o.Id, c.FullName AS Customer, ISNULL(p.FullName, 'Platform') AS Provider, " +
                        "s.ServiceName, o.Quantity, o.TotalAmount, o.Status, o.CreatedAt " +
                        "FROM Orders o " +
                        "INNER JOIN Users c ON o.CustomerId = c.Id " +
                        "LEFT JOIN Users p ON o.ProviderId = p.Id " +
                        "INNER JOIN Services s ON o.ServiceId = s.Id " +
                        "ORDER BY o.CreatedAt DESC", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridAllOrders.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefreshOrders_Click(object sender, EventArgs e)
        {
            LoadAllOrders();
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            gridAllOrders = new DataGridView();
            btnRefreshOrders = new Button();

            ((System.ComponentModel.ISupportInitialize)gridAllOrders).BeginInit();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(310, 15);
            lblTitle.Text = "All Orders";

            gridAllOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridAllOrders.Location = new Point(12, 55);
            gridAllOrders.ReadOnly = true;
            gridAllOrders.Size = new Size(960, 380);

            btnRefreshOrders.Location = new Point(12, 450);
            btnRefreshOrders.Size = new Size(120, 34);
            btnRefreshOrders.Text = "Refresh";
            btnRefreshOrders.Click += btnRefreshOrders_Click;

            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 510);
            Text = "Admin - All Orders";
            Controls.Add(lblTitle);
            Controls.Add(gridAllOrders);
            Controls.Add(btnRefreshOrders);

            ((System.ComponentModel.ISupportInitialize)gridAllOrders).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView gridAllOrders;
        private Button btnRefreshOrders;
    }

    // =====================
    // ADMIN PAYMENTS FORM
    // =====================
    public partial class AdminPaymentsForm : Form
    {
        public AdminPaymentsForm()
        {
            InitializeComponent();
            LoadPayments();
        }

        public void LoadPayments()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT p.Id, u.FullName AS Customer, p.Amount, " +
                        "p.PaymentMethod, p.PaymentStatus, p.PaymentDate, p.OrderId " +
                        "FROM Payments p " +
                        "INNER JOIN Users u ON p.UserId = u.Id " +
                        "ORDER BY p.PaymentDate DESC", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridPayments.DataSource = dt;

                    SqlCommand totalCmd = new SqlCommand(
                        "SELECT ISNULL(SUM(Amount), 0) FROM Payments WHERE PaymentStatus='Completed'", conn);
                    decimal total = (decimal)totalCmd.ExecuteScalar();
                    lblTotalPayments.Text = "Total Payments Received: $" + total.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefreshPayments_Click(object sender, EventArgs e) { LoadPayments(); }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblTotalPayments = new Label();
            gridPayments = new DataGridView();
            btnRefreshPayments = new Button();

            ((System.ComponentModel.ISupportInitialize)gridPayments).BeginInit();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(310, 15);
            lblTitle.Text = "Payment Overview";

            lblTotalPayments.AutoSize = true;
            lblTotalPayments.Location = new Point(12, 50);
            lblTotalPayments.Text = "Total Payments Received: $0.00";

            gridPayments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridPayments.Location = new Point(12, 80);
            gridPayments.ReadOnly = true;
            gridPayments.Size = new Size(860, 330);

            btnRefreshPayments.Location = new Point(12, 425);
            btnRefreshPayments.Size = new Size(120, 34);
            btnRefreshPayments.Text = "Refresh";
            btnRefreshPayments.Click += btnRefreshPayments_Click;

            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 480);
            Text = "Admin - Payment Overview";
            Controls.Add(lblTitle);
            Controls.Add(lblTotalPayments);
            Controls.Add(gridPayments);
            Controls.Add(btnRefreshPayments);

            ((System.ComponentModel.ISupportInitialize)gridPayments).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblTotalPayments;
        private DataGridView gridPayments;
        private Button btnRefreshPayments;
    }

    // =====================
    // ADMIN DELIVERY MONITOR FORM
    // =====================
    public partial class AdminDeliveryForm : Form
    {
        public AdminDeliveryForm()
        {
            InitializeComponent();
            LoadDelivery();
        }

        public void LoadDelivery()
        {
            try
            {
                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT dt.Id, dt.OrderId, c.FullName AS Customer, " +
                        "s.ServiceName, dt.DeliveryStatus, dt.UpdatedAt " +
                        "FROM DeliveryTracking dt " +
                        "INNER JOIN Orders o ON dt.OrderId = o.Id " +
                        "INNER JOIN Users c ON o.CustomerId = c.Id " +
                        "INNER JOIN Services s ON o.ServiceId = s.Id " +
                        "ORDER BY dt.UpdatedAt DESC", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridDelivery.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefreshDelivery_Click(object sender, EventArgs e) { LoadDelivery(); }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            gridDelivery = new DataGridView();
            btnRefreshDelivery = new Button();

            ((System.ComponentModel.ISupportInitialize)gridDelivery).BeginInit();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(300, 15);
            lblTitle.Text = "Delivery Monitor";

            gridDelivery.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridDelivery.Location = new Point(12, 55);
            gridDelivery.ReadOnly = true;
            gridDelivery.Size = new Size(860, 350);

            btnRefreshDelivery.Location = new Point(12, 420);
            btnRefreshDelivery.Size = new Size(120, 34);
            btnRefreshDelivery.Text = "Refresh";
            btnRefreshDelivery.Click += btnRefreshDelivery_Click;

            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 480);
            Text = "Admin - Delivery Monitor";
            Controls.Add(lblTitle);
            Controls.Add(gridDelivery);
            Controls.Add(btnRefreshDelivery);

            ((System.ComponentModel.ISupportInitialize)gridDelivery).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView gridDelivery;
        private Button btnRefreshDelivery;
    }
}

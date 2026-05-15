namespace InstaAutomateApp
{
    partial class CustomerDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // ── Top bar
            lblCustomer = new Label();
            lblCartItems = new Label();
            lblTotalOrders = new Label();
            lblTotalSpent = new Label();
            lblCartTotal = new Label();

            // ── Services section
            lblServicesHeader = new Label();
            gridServices = new DataGridView();
            btnAddToCart = new Button();

            // ── Cart section
            lblCartHeader = new Label();
            gridCart = new DataGridView();
            btnRemoveFromCart = new Button();
            txtCoupon = new TextBox();
            btnApplyCoupon = new Button();
            btnCheckout = new Button();

            // ── Orders section
            lblOrdersHeader = new Label();
            gridOrders = new DataGridView();

            // ── Instagram Accounts section
            lblAccountsHeader = new Label();
            gridAccounts = new DataGridView();
            btnAddAccount = new Button();

            // ── Automation Flows section
            lblFlowsHeader = new Label();
            gridFlows = new DataGridView();
            txtSearchFlow = new TextBox();
            btnSearchFlow = new Button();
            btnAddFlow = new Button();
            btnEditFlow = new Button();
            btnDeleteFlow = new Button();
            btnSimulate = new Button();

            // ── Refresh
            btnRefresh = new Button();

            ((System.ComponentModel.ISupportInitialize)gridServices).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridCart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridOrders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridAccounts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridFlows).BeginInit();
            SuspendLayout();

            // ─────────────── TOP BAR ───────────────
            lblCustomer.AutoSize = true;
            lblCustomer.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCustomer.Location = new Point(12, 10);
            lblCustomer.Text = "Welcome Customer";

            lblCartItems.AutoSize = true;
            lblCartItems.Location = new Point(12, 45);
            lblCartItems.Text = "Cart Items: 0";

            lblTotalOrders.AutoSize = true;
            lblTotalOrders.Location = new Point(180, 45);
            lblTotalOrders.Text = "Total Orders: 0";

            lblTotalSpent.AutoSize = true;
            lblTotalSpent.Location = new Point(360, 45);
            lblTotalSpent.Text = "Total Spent: $0";

            lblCartTotal.AutoSize = true;
            lblCartTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCartTotal.Location = new Point(560, 45);
            lblCartTotal.Text = "Cart Total: $0.00";

            // ─────────────── SERVICES ───────────────
            lblServicesHeader.AutoSize = true;
            lblServicesHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblServicesHeader.Location = new Point(12, 78);
            lblServicesHeader.Text = "Available Services (Browse & Add to Cart)";

            gridServices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridServices.Location = new Point(12, 100);
            gridServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridServices.MultiSelect = false;
            gridServices.ReadOnly = true;
            gridServices.RowHeadersWidth = 40;
            gridServices.Size = new Size(960, 130);

            btnAddToCart.BackColor = Color.SteelBlue;
            btnAddToCart.FlatStyle = FlatStyle.Flat;
            btnAddToCart.ForeColor = Color.White;
            btnAddToCart.Location = new Point(12, 238);
            btnAddToCart.Size = new Size(140, 34);
            btnAddToCart.Text = "Add To Cart";
            btnAddToCart.Click += btnAddToCart_Click;

            // ─────────────── CART ───────────────
            lblCartHeader.AutoSize = true;
            lblCartHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCartHeader.Location = new Point(12, 285);
            lblCartHeader.Text = "My Cart";

            gridCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridCart.Location = new Point(12, 308);
            gridCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCart.MultiSelect = false;
            gridCart.ReadOnly = true;
            gridCart.RowHeadersWidth = 40;
            gridCart.Size = new Size(960, 120);

            btnRemoveFromCart.Location = new Point(12, 436);
            btnRemoveFromCart.Size = new Size(160, 34);
            btnRemoveFromCart.Text = "Remove From Cart";
            btnRemoveFromCart.Click += btnRemoveFromCart_Click;

            txtCoupon.Location = new Point(200, 436);
            txtCoupon.PlaceholderText = "Enter Coupon Code";
            txtCoupon.Size = new Size(180, 31);

            btnApplyCoupon.Location = new Point(390, 436);
            btnApplyCoupon.Size = new Size(140, 34);
            btnApplyCoupon.Text = "Apply Coupon";
            btnApplyCoupon.Click += btnApplyCoupon_Click;

            btnCheckout.BackColor = Color.SeaGreen;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(560, 436);
            btnCheckout.Size = new Size(110, 34);
            btnCheckout.Text = "Checkout";
            btnCheckout.Click += btnCheckout_Click;

            // ─────────────── ORDERS ───────────────
            lblOrdersHeader.AutoSize = true;
            lblOrdersHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOrdersHeader.Location = new Point(12, 485);
            lblOrdersHeader.Text = "My Orders & Tracking";

            gridOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridOrders.Location = new Point(12, 508);
            gridOrders.ReadOnly = true;
            gridOrders.RowHeadersWidth = 40;
            gridOrders.Size = new Size(960, 130);

            // ─────────────── INSTAGRAM ACCOUNTS ───────────────
            lblAccountsHeader.AutoSize = true;
            lblAccountsHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAccountsHeader.Location = new Point(12, 655);
            lblAccountsHeader.Text = "My Instagram Accounts";

            gridAccounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridAccounts.Location = new Point(12, 678);
            gridAccounts.ReadOnly = true;
            gridAccounts.RowHeadersWidth = 40;
            gridAccounts.Size = new Size(960, 130);

            btnAddAccount.BackColor = Color.SteelBlue;
            btnAddAccount.FlatStyle = FlatStyle.Flat;
            btnAddAccount.ForeColor = Color.White;
            btnAddAccount.Location = new Point(12, 818);
            btnAddAccount.Size = new Size(150, 34);
            btnAddAccount.Text = "Add Account";
            btnAddAccount.Click += btnAddAccount_Click;

            // ─────────────── AUTOMATION FLOWS ───────────────
            lblFlowsHeader.AutoSize = true;
            lblFlowsHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFlowsHeader.Location = new Point(12, 870);
            lblFlowsHeader.Text = "My Automation Flows";

            txtSearchFlow.Location = new Point(12, 898);
            txtSearchFlow.PlaceholderText = "Search by keyword...";
            txtSearchFlow.Size = new Size(200, 31);

            btnSearchFlow.BackColor = Color.SteelBlue;
            btnSearchFlow.FlatStyle = FlatStyle.Flat;
            btnSearchFlow.ForeColor = Color.White;
            btnSearchFlow.Location = new Point(222, 897);
            btnSearchFlow.Size = new Size(100, 34);
            btnSearchFlow.Text = "Search";
            btnSearchFlow.Click += btnSearchFlow_Click;

            gridFlows.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridFlows.Location = new Point(12, 940);
            gridFlows.ReadOnly = true;
            gridFlows.RowHeadersWidth = 40;
            gridFlows.Size = new Size(960, 140);

            btnAddFlow.BackColor = Color.SteelBlue;
            btnAddFlow.FlatStyle = FlatStyle.Flat;
            btnAddFlow.ForeColor = Color.White;
            btnAddFlow.Location = new Point(12, 1090);
            btnAddFlow.Size = new Size(120, 34);
            btnAddFlow.Text = "Add Flow";
            btnAddFlow.Click += btnAddFlow_Click;

            btnEditFlow.BackColor = Color.SteelBlue;
            btnEditFlow.FlatStyle = FlatStyle.Flat;
            btnEditFlow.ForeColor = Color.White;
            btnEditFlow.Location = new Point(145, 1090);
            btnEditFlow.Size = new Size(120, 34);
            btnEditFlow.Text = "Edit Flow";
            btnEditFlow.Click += btnEditFlow_Click;

            btnDeleteFlow.BackColor = Color.Firebrick;
            btnDeleteFlow.FlatStyle = FlatStyle.Flat;
            btnDeleteFlow.ForeColor = Color.White;
            btnDeleteFlow.Location = new Point(278, 1090);
            btnDeleteFlow.Size = new Size(120, 34);
            btnDeleteFlow.Text = "Delete Flow";
            btnDeleteFlow.Click += btnDeleteFlow_Click;

            btnSimulate.BackColor = Color.DarkGoldenrod;
            btnSimulate.FlatStyle = FlatStyle.Flat;
            btnSimulate.ForeColor = Color.White;
            btnSimulate.Location = new Point(415, 1090);
            btnSimulate.Size = new Size(150, 34);
            btnSimulate.Text = "Simulate Flow";
            btnSimulate.Click += btnSimulate_Click;

            // ─────────────── REFRESH ───────────────
            btnRefresh.Location = new Point(12, 1145);
            btnRefresh.Size = new Size(110, 34);
            btnRefresh.Text = "Refresh All";
            btnRefresh.Click += btnRefresh_Click;

            // ─────────────── FORM ───────────────
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 1200);
            Text = "Customer Dashboard - InstaAutomate";
            AutoScroll = true;

            Controls.Add(lblCustomer);
            Controls.Add(lblCartItems);
            Controls.Add(lblTotalOrders);
            Controls.Add(lblTotalSpent);
            Controls.Add(lblCartTotal);
            Controls.Add(lblServicesHeader);
            Controls.Add(gridServices);
            Controls.Add(btnAddToCart);
            Controls.Add(lblCartHeader);
            Controls.Add(gridCart);
            Controls.Add(btnRemoveFromCart);
            Controls.Add(txtCoupon);
            Controls.Add(btnApplyCoupon);
            Controls.Add(btnCheckout);
            Controls.Add(lblOrdersHeader);
            Controls.Add(gridOrders);
            Controls.Add(lblAccountsHeader);
            Controls.Add(gridAccounts);
            Controls.Add(btnAddAccount);
            Controls.Add(lblFlowsHeader);
            Controls.Add(txtSearchFlow);
            Controls.Add(btnSearchFlow);
            Controls.Add(gridFlows);
            Controls.Add(btnAddFlow);
            Controls.Add(btnEditFlow);
            Controls.Add(btnDeleteFlow);
            Controls.Add(btnSimulate);
            Controls.Add(btnRefresh);

            ((System.ComponentModel.ISupportInitialize)gridServices).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridCart).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridOrders).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridAccounts).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridFlows).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCustomer;
        private Label lblCartItems;
        private Label lblTotalOrders;
        private Label lblTotalSpent;
        private Label lblCartTotal;
        private Label lblServicesHeader;
        private DataGridView gridServices;
        private Button btnAddToCart;
        private Label lblCartHeader;
        private DataGridView gridCart;
        private Button btnRemoveFromCart;
        private TextBox txtCoupon;
        private Button btnApplyCoupon;
        private Button btnCheckout;
        private Label lblOrdersHeader;
        private DataGridView gridOrders;
        private Label lblAccountsHeader;
        private DataGridView gridAccounts;
        private Button btnAddAccount;
        private Label lblFlowsHeader;
        private DataGridView gridFlows;
        private TextBox txtSearchFlow;
        private Button btnSearchFlow;
        private Button btnAddFlow;
        private Button btnEditFlow;
        private Button btnDeleteFlow;
        private Button btnSimulate;
        private Button btnRefresh;
    }
}

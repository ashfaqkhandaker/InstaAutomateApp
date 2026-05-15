namespace InstaAutomateApp
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            lblCurrentUser = new Label();
            lblServices = new Label();
            lblOrders = new Label();
            lblCustomers = new Label();
            lblRevenue = new Label();
            lblServicesHeader = new Label();
            gridServices = new DataGridView();
            btnAddService = new Button();
            btnDeleteService = new Button();
            lblUsersHeader = new Label();
            gridUsers = new DataGridView();
            btnManageUsers = new Button();
            btnViewAllOrders = new Button();
            btnPaymentOverview = new Button();
            btnManageCoupons = new Button();
            btnDeliveryMonitor = new Button();
            btnRevenueStats = new Button();
            btnRefresh = new Button();
            refreshTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)gridServices).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridUsers).BeginInit();
            SuspendLayout();
            // 
            // lblCurrentUser
            // 
            lblCurrentUser.AutoSize = true;
            lblCurrentUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCurrentUser.Location = new Point(12, 12);
            lblCurrentUser.Name = "lblCurrentUser";
            lblCurrentUser.Size = new Size(182, 28);
            lblCurrentUser.TabIndex = 0;
            lblCurrentUser.Text = "Admin Dashboard";
            // 
            // lblServices
            // 
            lblServices.AutoSize = true;
            lblServices.BorderStyle = BorderStyle.FixedSingle;
            lblServices.Font = new Font("Microsoft Sans Serif", 12F);
            lblServices.Location = new Point(12, 50);
            lblServices.Name = "lblServices";
            lblServices.Size = new Size(133, 31);
            lblServices.TabIndex = 1;
            lblServices.Text = "Services: 0";
            lblServices.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblOrders
            // 
            lblOrders.AutoSize = true;
            lblOrders.BorderStyle = BorderStyle.FixedSingle;
            lblOrders.Font = new Font("Microsoft Sans Serif", 12F);
            lblOrders.Location = new Point(200, 50);
            lblOrders.Name = "lblOrders";
            lblOrders.Size = new Size(115, 31);
            lblOrders.TabIndex = 2;
            lblOrders.Text = "Orders: 0";
            lblOrders.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCustomers
            // 
            lblCustomers.AutoSize = true;
            lblCustomers.BorderStyle = BorderStyle.FixedSingle;
            lblCustomers.Font = new Font("Microsoft Sans Serif", 12F);
            lblCustomers.Location = new Point(380, 50);
            lblCustomers.Name = "lblCustomers";
            lblCustomers.Size = new Size(156, 31);
            lblCustomers.TabIndex = 3;
            lblCustomers.Text = "Customers: 0";
            lblCustomers.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblRevenue
            // 
            lblRevenue.AutoSize = true;
            lblRevenue.BorderStyle = BorderStyle.FixedSingle;
            lblRevenue.Font = new Font("Microsoft Sans Serif", 12F);
            lblRevenue.Location = new Point(580, 50);
            lblRevenue.Name = "lblRevenue";
            lblRevenue.Size = new Size(181, 31);
            lblRevenue.TabIndex = 4;
            lblRevenue.Text = "Revenue: $0.00";
            lblRevenue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblServicesHeader
            // 
            lblServicesHeader.AutoSize = true;
            lblServicesHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblServicesHeader.Location = new Point(12, 100);
            lblServicesHeader.Name = "lblServicesHeader";
            lblServicesHeader.Size = new Size(545, 25);
            lblServicesHeader.TabIndex = 5;
            lblServicesHeader.Text = "Platform Services (Admin-Managed — visible to all customers)";
            // 
            // gridServices
            // 
            gridServices.AllowUserToAddRows = false;
            gridServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridServices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridServices.Location = new Point(12, 125);
            gridServices.MultiSelect = false;
            gridServices.Name = "gridServices";
            gridServices.ReadOnly = true;
            gridServices.RowHeadersWidth = 40;
            gridServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridServices.Size = new Size(950, 160);
            gridServices.TabIndex = 6;
            // 
            // btnAddService
            // 
            btnAddService.BackColor = Color.SteelBlue;
            btnAddService.FlatStyle = FlatStyle.Flat;
            btnAddService.ForeColor = Color.White;
            btnAddService.Location = new Point(12, 295);
            btnAddService.Name = "btnAddService";
            btnAddService.Size = new Size(140, 34);
            btnAddService.TabIndex = 7;
            btnAddService.Text = "Add Service";
            btnAddService.UseVisualStyleBackColor = false;
            btnAddService.Click += btnAddService_Click;
            // 
            // btnDeleteService
            // 
            btnDeleteService.BackColor = Color.Firebrick;
            btnDeleteService.FlatStyle = FlatStyle.Flat;
            btnDeleteService.ForeColor = Color.White;
            btnDeleteService.Location = new Point(165, 295);
            btnDeleteService.Name = "btnDeleteService";
            btnDeleteService.Size = new Size(140, 34);
            btnDeleteService.TabIndex = 8;
            btnDeleteService.Text = "Delete Service";
            btnDeleteService.UseVisualStyleBackColor = false;
            btnDeleteService.Click += btnDeleteService_Click;
            // 
            // lblUsersHeader
            // 
            lblUsersHeader.AutoSize = true;
            lblUsersHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsersHeader.Location = new Point(12, 345);
            lblUsersHeader.Name = "lblUsersHeader";
            lblUsersHeader.Size = new Size(86, 25);
            lblUsersHeader.TabIndex = 9;
            lblUsersHeader.Text = "All Users";
            // 
            // gridUsers
            // 
            gridUsers.AllowUserToAddRows = false;
            gridUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridUsers.Location = new Point(12, 368);
            gridUsers.Name = "gridUsers";
            gridUsers.ReadOnly = true;
            gridUsers.RowHeadersWidth = 40;
            gridUsers.Size = new Size(950, 160);
            gridUsers.TabIndex = 10;
            // 
            // btnManageUsers
            // 
            btnManageUsers.BackColor = Color.SteelBlue;
            btnManageUsers.FlatStyle = FlatStyle.Flat;
            btnManageUsers.ForeColor = Color.White;
            btnManageUsers.Location = new Point(12, 540);
            btnManageUsers.Name = "btnManageUsers";
            btnManageUsers.Size = new Size(150, 34);
            btnManageUsers.TabIndex = 11;
            btnManageUsers.Text = "Manage Users";
            btnManageUsers.UseVisualStyleBackColor = false;
            btnManageUsers.Click += btnManageUsers_Click;
            // 
            // btnViewAllOrders
            // 
            btnViewAllOrders.Location = new Point(12, 600);
            btnViewAllOrders.Name = "btnViewAllOrders";
            btnViewAllOrders.Size = new Size(160, 38);
            btnViewAllOrders.TabIndex = 12;
            btnViewAllOrders.Text = "All Orders";
            btnViewAllOrders.Click += btnViewAllOrders_Click;
            // 
            // btnPaymentOverview
            // 
            btnPaymentOverview.Location = new Point(185, 600);
            btnPaymentOverview.Name = "btnPaymentOverview";
            btnPaymentOverview.Size = new Size(180, 38);
            btnPaymentOverview.TabIndex = 13;
            btnPaymentOverview.Text = "Payment Overview";
            btnPaymentOverview.Click += btnPaymentOverview_Click;
            // 
            // btnManageCoupons
            // 
            btnManageCoupons.Location = new Point(378, 600);
            btnManageCoupons.Name = "btnManageCoupons";
            btnManageCoupons.Size = new Size(180, 38);
            btnManageCoupons.TabIndex = 14;
            btnManageCoupons.Text = "Manage Coupons";
            btnManageCoupons.Click += btnManageCoupons_Click;
            // 
            // btnDeliveryMonitor
            // 
            btnDeliveryMonitor.Location = new Point(571, 600);
            btnDeliveryMonitor.Name = "btnDeliveryMonitor";
            btnDeliveryMonitor.Size = new Size(170, 38);
            btnDeliveryMonitor.TabIndex = 15;
            btnDeliveryMonitor.Text = "Delivery Monitor";
            btnDeliveryMonitor.Click += btnDeliveryMonitor_Click;
            // 
            // btnRevenueStats
            // 
            btnRevenueStats.Location = new Point(754, 600);
            btnRevenueStats.Name = "btnRevenueStats";
            btnRevenueStats.Size = new Size(150, 38);
            btnRevenueStats.TabIndex = 16;
            btnRevenueStats.Text = "Revenue Stats";
            btnRevenueStats.Click += btnRevenueStats_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(12, 656);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 34);
            btnRefresh.TabIndex = 17;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // refreshTimer
            // 
            refreshTimer.Interval = 10000;
            refreshTimer.Tick += refreshTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(990, 710);
            Controls.Add(lblCurrentUser);
            Controls.Add(lblServices);
            Controls.Add(lblOrders);
            Controls.Add(lblCustomers);
            Controls.Add(lblRevenue);
            Controls.Add(lblServicesHeader);
            Controls.Add(gridServices);
            Controls.Add(btnAddService);
            Controls.Add(btnDeleteService);
            Controls.Add(lblUsersHeader);
            Controls.Add(gridUsers);
            Controls.Add(btnManageUsers);
            Controls.Add(btnViewAllOrders);
            Controls.Add(btnPaymentOverview);
            Controls.Add(btnManageCoupons);
            Controls.Add(btnDeliveryMonitor);
            Controls.Add(btnRevenueStats);
            Controls.Add(btnRefresh);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin Dashboard — InstaAutomate";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)gridServices).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCurrentUser;
        private Label lblServices;
        private Label lblOrders;
        private Label lblCustomers;
        private Label lblRevenue;
        private Label lblServicesHeader;
        private DataGridView gridServices;
        private Button btnAddService;
        private Button btnDeleteService;
        private Label lblUsersHeader;
        private DataGridView gridUsers;
        private Button btnManageUsers;
        private Button btnViewAllOrders;
        private Button btnPaymentOverview;
        private Button btnManageCoupons;
        private Button btnDeliveryMonitor;
        private Button btnRevenueStats;
        private Button btnRefresh;
        private System.Windows.Forms.Timer refreshTimer;
    }
}

namespace InstaAutomateApp
{
    partial class ProviderDashboard
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
            lblProvider = new Label();
            lblTotalServices = new Label();
            lblTotalOrders = new Label();
            lblTotalEarnings = new Label();
            lblPendingOrders = new Label();
            gridServices = new DataGridView();
            btnAddService = new Button();
            btnEditService = new Button();
            btnDeleteService = new Button();
            label1 = new Label();
            gridOrders = new DataGridView();
            btnUpdateDelivery = new Button();

            ((System.ComponentModel.ISupportInitialize)gridServices).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridOrders).BeginInit();
            SuspendLayout();

            // lblProvider
            lblProvider.AutoSize = true;
            lblProvider.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblProvider.Location = new Point(12, 9);
            lblProvider.Name = "lblProvider";
            lblProvider.Size = new Size(200, 25);
            lblProvider.Text = "Welcome Provider";

            // Stats Labels
            lblTotalServices.AutoSize = true;
            lblTotalServices.Location = new Point(12, 50);
            lblTotalServices.Name = "lblTotalServices";
            lblTotalServices.Text = "Services: 0";
            lblTotalServices.Click += label1_Click;

            lblTotalOrders.AutoSize = true;
            lblTotalOrders.Location = new Point(160, 50);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Text = "Orders: 0";

            lblTotalEarnings.AutoSize = true;
            lblTotalEarnings.Location = new Point(300, 50);
            lblTotalEarnings.Name = "lblTotalEarnings";
            lblTotalEarnings.Text = "Earnings: $0";

            lblPendingOrders.AutoSize = true;
            lblPendingOrders.Location = new Point(460, 50);
            lblPendingOrders.Name = "lblPendingOrders";
            lblPendingOrders.Text = "Pending: 0";

            // gridServices
            gridServices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridServices.Location = new Point(12, 90);
            gridServices.Name = "gridServices";
            gridServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridServices.MultiSelect = false;
            gridServices.ReadOnly = true;
            gridServices.RowHeadersWidth = 40;
            gridServices.Size = new Size(760, 130);
            gridServices.TabIndex = 5;
            gridServices.CellContentClick += gridServices_CellContentClick;

            // btnAddService
            btnAddService.Location = new Point(12, 230);
            btnAddService.Name = "btnAddService";
            btnAddService.Size = new Size(143, 34);
            btnAddService.Text = "Add Service";
            btnAddService.Click += btnAddService_Click;

            // btnEditService
            btnEditService.Location = new Point(170, 230);
            btnEditService.Name = "btnEditService";
            btnEditService.Size = new Size(143, 34);
            btnEditService.Text = "Edit Service";
            btnEditService.Click += btnEditService_Click;

            // btnDeleteService
            btnDeleteService.Location = new Point(328, 230);
            btnDeleteService.Name = "btnDeleteService";
            btnDeleteService.Size = new Size(143, 34);
            btnDeleteService.Text = "Delete Service";
            btnDeleteService.Click += btnDeleteService_Click;

            // Orders Label
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(12, 282);
            label1.Name = "label1";
            label1.Text = "Customer Orders";

            // gridOrders
            gridOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridOrders.Location = new Point(12, 308);
            gridOrders.Name = "gridOrders";
            gridOrders.ReadOnly = true;
            gridOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridOrders.MultiSelect = false;
            gridOrders.RowHeadersWidth = 40;
            gridOrders.Size = new Size(760, 150);
            gridOrders.TabIndex = 9;

            // btnUpdateDelivery
            btnUpdateDelivery.Location = new Point(12, 470);
            btnUpdateDelivery.Name = "btnUpdateDelivery";
            btnUpdateDelivery.Size = new Size(200, 34);
            btnUpdateDelivery.Text = "Update Delivery Status";
            btnUpdateDelivery.Click += btnUpdateDelivery_Click;

            // Form
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 525);
            Text = "Provider Dashboard - InstaAutomate";
            Controls.Add(lblProvider);
            Controls.Add(lblTotalServices);
            Controls.Add(lblTotalOrders);
            Controls.Add(lblTotalEarnings);
            Controls.Add(lblPendingOrders);
            Controls.Add(gridServices);
            Controls.Add(btnAddService);
            Controls.Add(btnEditService);
            Controls.Add(btnDeleteService);
            Controls.Add(label1);
            Controls.Add(gridOrders);
            Controls.Add(btnUpdateDelivery);

            ((System.ComponentModel.ISupportInitialize)gridServices).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridOrders).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblProvider;
        private Label lblTotalServices;
        private Label lblTotalOrders;
        private Label lblTotalEarnings;
        private Label lblPendingOrders;
        private DataGridView gridServices;
        private Button btnAddService;
        private Button btnEditService;
        private Button btnDeleteService;
        private Label label1;
        private DataGridView gridOrders;
        private Button btnUpdateDelivery;
    }
}

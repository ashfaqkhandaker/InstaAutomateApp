namespace InstaAutomateApp
{
    partial class AdminCouponsForm
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
            lblTitle = new Label();
            gridCoupons = new DataGridView();
            lblCode = new Label();
            txtCouponCode = new TextBox();
            lblDiscount = new Label();
            txtDiscount = new TextBox();
            lblExpiry = new Label();
            dtpExpiry = new DateTimePicker();
            btnAddCoupon = new Button();
            btnToggleCoupon = new Button();
            btnDeleteCoupon = new Button();

            ((System.ComponentModel.ISupportInitialize)gridCoupons).BeginInit();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(300, 15);
            lblTitle.Text = "Coupon Management";

            gridCoupons.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridCoupons.Location = new Point(12, 55);
            gridCoupons.ReadOnly = true;
            gridCoupons.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCoupons.MultiSelect = false;
            gridCoupons.Size = new Size(760, 220);

            lblCode.AutoSize = true;
            lblCode.Location = new Point(12, 292);
            lblCode.Text = "Coupon Code:";

            txtCouponCode.Location = new Point(130, 289);
            txtCouponCode.Size = new Size(150, 31);
            txtCouponCode.PlaceholderText = "e.g. SAVE20";

            lblDiscount.AutoSize = true;
            lblDiscount.Location = new Point(12, 333);
            lblDiscount.Text = "Discount (%):";

            txtDiscount.Location = new Point(130, 330);
            txtDiscount.Size = new Size(100, 31);
            txtDiscount.PlaceholderText = "e.g. 20";

            lblExpiry.AutoSize = true;
            lblExpiry.Location = new Point(12, 373);
            lblExpiry.Text = "Expiry Date:";

            dtpExpiry.Location = new Point(130, 370);
            dtpExpiry.Size = new Size(200, 31);
            dtpExpiry.Format = DateTimePickerFormat.Short;

            btnAddCoupon.Location = new Point(12, 415);
            btnAddCoupon.Size = new Size(140, 34);
            btnAddCoupon.Text = "Add Coupon";
            btnAddCoupon.Click += btnAddCoupon_Click;

            btnToggleCoupon.Location = new Point(170, 415);
            btnToggleCoupon.Size = new Size(160, 34);
            btnToggleCoupon.Text = "Toggle Active";
            btnToggleCoupon.Click += btnToggleCoupon_Click;

            btnDeleteCoupon.Location = new Point(345, 415);
            btnDeleteCoupon.Size = new Size(140, 34);
            btnDeleteCoupon.Text = "Delete Coupon";
            btnDeleteCoupon.Click += btnDeleteCoupon_Click;

            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 475);
            Text = "Admin - Coupon Management";
            Controls.Add(lblTitle);
            Controls.Add(gridCoupons);
            Controls.Add(lblCode);
            Controls.Add(txtCouponCode);
            Controls.Add(lblDiscount);
            Controls.Add(txtDiscount);
            Controls.Add(lblExpiry);
            Controls.Add(dtpExpiry);
            Controls.Add(btnAddCoupon);
            Controls.Add(btnToggleCoupon);
            Controls.Add(btnDeleteCoupon);

            ((System.ComponentModel.ISupportInitialize)gridCoupons).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView gridCoupons;
        private Label lblCode;
        private TextBox txtCouponCode;
        private Label lblDiscount;
        private TextBox txtDiscount;
        private Label lblExpiry;
        private DateTimePicker dtpExpiry;
        private Button btnAddCoupon;
        private Button btnToggleCoupon;
        private Button btnDeleteCoupon;
    }
}

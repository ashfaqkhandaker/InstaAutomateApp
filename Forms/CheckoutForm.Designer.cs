namespace InstaAutomateApp
{
    partial class CheckoutForm
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
            gridCheckoutCart = new DataGridView();
            lblSubtotal = new Label();
            lblDiscount = new Label();
            lblFinalTotal = new Label();
            txtCouponCheckout = new TextBox();
            btnApplyCouponCheckout = new Button();
            lblPaymentMethod = new Label();
            cmbPaymentMethod = new ComboBox();
            btnPlaceOrder = new Button();
            btnCancelCheckout = new Button();

            ((System.ComponentModel.ISupportInitialize)gridCheckoutCart).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(270, 15);
            lblTitle.Text = "Checkout";

            // gridCheckoutCart
            gridCheckoutCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridCheckoutCart.Location = new Point(30, 55);
            gridCheckoutCart.Name = "gridCheckoutCart";
            gridCheckoutCart.ReadOnly = true;
            gridCheckoutCart.Size = new Size(620, 160);

            // Totals
            lblSubtotal.AutoSize = true;
            lblSubtotal.Location = new Point(30, 230);
            lblSubtotal.Text = "Subtotal: $0.00";

            lblDiscount.AutoSize = true;
            lblDiscount.Location = new Point(30, 258);
            lblDiscount.ForeColor = System.Drawing.Color.Green;
            lblDiscount.Text = "Discount: -$0.00";

            lblFinalTotal.AutoSize = true;
            lblFinalTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFinalTotal.Location = new Point(30, 286);
            lblFinalTotal.Text = "Final Total: $0.00";

            // Coupon
            txtCouponCheckout.Location = new Point(30, 330);
            txtCouponCheckout.PlaceholderText = "Coupon Code (optional)";
            txtCouponCheckout.Size = new Size(200, 31);

            btnApplyCouponCheckout.Location = new Point(240, 330);
            btnApplyCouponCheckout.Size = new Size(140, 34);
            btnApplyCouponCheckout.Text = "Apply Coupon";
            btnApplyCouponCheckout.Click += btnApplyCouponCheckout_Click;

            // Payment Method
            lblPaymentMethod.AutoSize = true;
            lblPaymentMethod.Location = new Point(30, 380);
            lblPaymentMethod.Text = "Payment Method:";

            cmbPaymentMethod.Location = new Point(170, 377);
            cmbPaymentMethod.Size = new Size(200, 33);
            cmbPaymentMethod.Items.Add("Credit Card");
            cmbPaymentMethod.Items.Add("Debit Card");
            cmbPaymentMethod.Items.Add("PayPal");
            cmbPaymentMethod.Items.Add("Cash On Delivery");
            cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;

            // Buttons
            btnPlaceOrder.Location = new Point(30, 430);
            btnPlaceOrder.Size = new Size(160, 40);
            btnPlaceOrder.Text = "Place Order";
            btnPlaceOrder.Click += btnPlaceOrder_Click;

            btnCancelCheckout.Location = new Point(210, 430);
            btnCancelCheckout.Size = new Size(100, 40);
            btnCancelCheckout.Text = "Cancel";
            btnCancelCheckout.Click += btnCancelCheckout_Click;

            // Form
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 500);
            Text = "Checkout";
            Controls.Add(lblTitle);
            Controls.Add(gridCheckoutCart);
            Controls.Add(lblSubtotal);
            Controls.Add(lblDiscount);
            Controls.Add(lblFinalTotal);
            Controls.Add(txtCouponCheckout);
            Controls.Add(btnApplyCouponCheckout);
            Controls.Add(lblPaymentMethod);
            Controls.Add(cmbPaymentMethod);
            Controls.Add(btnPlaceOrder);
            Controls.Add(btnCancelCheckout);

            ((System.ComponentModel.ISupportInitialize)gridCheckoutCart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView gridCheckoutCart;
        private Label lblSubtotal;
        private Label lblDiscount;
        private Label lblFinalTotal;
        private TextBox txtCouponCheckout;
        private Button btnApplyCouponCheckout;
        private Label lblPaymentMethod;
        private ComboBox cmbPaymentMethod;
        private Button btnPlaceOrder;
        private Button btnCancelCheckout;
    }
}

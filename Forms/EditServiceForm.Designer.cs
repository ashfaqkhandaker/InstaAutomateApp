namespace InstaAutomateApp
{
    partial class EditServiceForm
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
            lblServiceName = new Label();
            txtServiceName = new TextBox();
            lblDescription = new Label();
            txtDescription = new RichTextBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            btnUpdateService = new Button();
            btnCancelEdit = new Button();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(300, 27);
            lblTitle.Text = "Edit Service";

            // lblServiceName
            lblServiceName.AutoSize = true;
            lblServiceName.Location = new Point(182, 76);
            lblServiceName.Text = "Service Name";

            // txtServiceName
            txtServiceName.Location = new Point(182, 104);
            txtServiceName.Size = new Size(357, 31);

            // lblDescription
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(182, 145);
            lblDescription.Text = "Description";

            // txtDescription
            txtDescription.Location = new Point(182, 170);
            txtDescription.Size = new Size(357, 60);

            // lblPrice
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(182, 240);
            lblPrice.Text = "Price";

            // txtPrice
            txtPrice.Location = new Point(182, 265);
            txtPrice.Size = new Size(357, 31);

            // btnUpdateService
            btnUpdateService.Location = new Point(220, 315);
            btnUpdateService.Size = new Size(150, 34);
            btnUpdateService.Text = "Update Service";
            btnUpdateService.Click += btnUpdateService_Click;

            // btnCancelEdit
            btnCancelEdit.Location = new Point(385, 315);
            btnCancelEdit.Size = new Size(100, 34);
            btnCancelEdit.Text = "Cancel";
            btnCancelEdit.Click += btnCancelEdit_Click;

            // Form
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 380);
            Text = "Edit Service";
            Controls.Add(lblTitle);
            Controls.Add(lblServiceName);
            Controls.Add(txtServiceName);
            Controls.Add(lblDescription);
            Controls.Add(txtDescription);
            Controls.Add(lblPrice);
            Controls.Add(txtPrice);
            Controls.Add(btnUpdateService);
            Controls.Add(btnCancelEdit);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblServiceName;
        private TextBox txtServiceName;
        private Label lblDescription;
        private RichTextBox txtDescription;
        private Label lblPrice;
        private TextBox txtPrice;
        private Button btnUpdateService;
        private Button btnCancelEdit;
    }
}

namespace InstaAutomateApp
{
    partial class AddServiceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblServiceName = new Label();
            txtServiceName = new TextBox();
            lblDescription = new Label();
            lblPrice = new Label();
            txtDescription = new RichTextBox();
            txtPrice = new TextBox();
            btnSaveService = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(312, 27);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(106, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add Service";
            // 
            // lblServiceName
            // 
            lblServiceName.AutoSize = true;
            lblServiceName.Location = new Point(182, 76);
            lblServiceName.Name = "lblServiceName";
            lblServiceName.Size = new Size(119, 25);
            lblServiceName.TabIndex = 1;
            lblServiceName.Text = "Service Name";
            // 
            // txtServiceName
            // 
            txtServiceName.Location = new Point(182, 104);
            txtServiceName.Name = "txtServiceName";
            txtServiceName.Size = new Size(357, 31);
            txtServiceName.TabIndex = 2;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(182, 138);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(102, 25);
            lblDescription.TabIndex = 3;
            lblDescription.Text = "Description";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(182, 221);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(49, 25);
            lblPrice.TabIndex = 4;
            lblPrice.Text = "Price";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(182, 166);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(357, 52);
            txtDescription.TabIndex = 5;
            txtDescription.Text = "";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(182, 249);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(357, 31);
            txtPrice.TabIndex = 6;
            // 
            // btnSaveService
            // 
            btnSaveService.Location = new Point(283, 298);
            btnSaveService.Name = "btnSaveService";
            btnSaveService.Size = new Size(146, 34);
            btnSaveService.TabIndex = 7;
            btnSaveService.Text = "Save Service";
            btnSaveService.UseVisualStyleBackColor = true;
            btnSaveService.Click += btnSaveService_Click;
            // 
            // AddServiceForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSaveService);
            Controls.Add(txtPrice);
            Controls.Add(txtDescription);
            Controls.Add(lblPrice);
            Controls.Add(lblDescription);
            Controls.Add(txtServiceName);
            Controls.Add(lblServiceName);
            Controls.Add(lblTitle);
            Name = "AddServiceForm";
            Text = "AddServiceForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblServiceName;
        private TextBox txtServiceName;
        private Label lblDescription;
        private Label lblPrice;
        private RichTextBox txtDescription;
        private TextBox txtPrice;
        private Button btnSaveService;
    }
}
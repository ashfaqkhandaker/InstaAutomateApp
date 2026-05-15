namespace InstaAutomateApp
{
    partial class AddAccountForm
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
            label1 = new Label();
            txtUsername = new TextBox();
            label2 = new Label();
            txtToken = new TextBox();
            btnSaveAccount = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(217, 63);
            label1.Name = "label1";
            label1.Size = new Size(91, 25);
            label1.TabIndex = 0;
            label1.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(217, 91);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(329, 31);
            txtUsername.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(217, 136);
            label2.Name = "label2";
            label2.Size = new Size(116, 25);
            label2.TabIndex = 0;
            label2.Text = "Access Token";
            // 
            // txtToken
            // 
            txtToken.Location = new Point(217, 164);
            txtToken.Name = "txtToken";
            txtToken.Size = new Size(329, 31);
            txtToken.TabIndex = 1;
            // 
            // btnSaveAccount
            // 
            btnSaveAccount.Location = new Point(321, 227);
            btnSaveAccount.Name = "btnSaveAccount";
            btnSaveAccount.Size = new Size(112, 34);
            btnSaveAccount.TabIndex = 2;
            btnSaveAccount.Text = "Save Account";
            btnSaveAccount.UseVisualStyleBackColor = true;
            btnSaveAccount.Click += btnSaveAccount_Click;
            // 
            // AddAccountForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSaveAccount);
            Controls.Add(txtToken);
            Controls.Add(txtUsername);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "AddAccountForm";
            Text = "AddAccountForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtUsername;
        private Label label2;
        private TextBox txtToken;
        private Button btnSaveAccount;
    }
}
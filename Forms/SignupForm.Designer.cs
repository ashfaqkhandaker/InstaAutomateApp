namespace InstaAutomateApp
{
    partial class SignupForm
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
            txtName = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnCreatAccount = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            cmbRole = new ComboBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(181, 90);
            txtName.Name = "txtName";
            txtName.Size = new Size(345, 31);
            txtName.TabIndex = 0;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(181, 164);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(345, 31);
            txtEmail.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(181, 238);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(345, 31);
            txtPassword.TabIndex = 2;
            // 
            // btnCreatAccount
            // 
            btnCreatAccount.Location = new Point(307, 363);
            btnCreatAccount.Name = "btnCreatAccount";
            btnCreatAccount.Size = new Size(112, 34);
            btnCreatAccount.TabIndex = 3;
            btnCreatAccount.Text = "SignUp";
            btnCreatAccount.UseVisualStyleBackColor = true;
            btnCreatAccount.Click += btnCreatAccount_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(181, 62);
            label1.Name = "label1";
            label1.Size = new Size(59, 25);
            label1.TabIndex = 4;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(181, 136);
            label2.Name = "label2";
            label2.Size = new Size(54, 25);
            label2.TabIndex = 5;
            label2.Text = "Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(181, 210);
            label3.Name = "label3";
            label3.Size = new Size(87, 25);
            label3.TabIndex = 6;
            label3.Text = "Password";
            // 
            // cmbRole
            // 
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(181, 310);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(345, 33);
            cmbRole.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(181, 282);
            label4.Name = "label4";
            label4.Size = new Size(97, 25);
            label4.TabIndex = 8;
            label4.Text = "Select Role";
            // 
            // SignupForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(cmbRole);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCreatAccount);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Controls.Add(txtName);
            Name = "SignupForm";
            Text = "SignupForm";
            Load += SignupForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtName;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnCreatAccount;
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox cmbRole;
        private Label label4;
    }
}
namespace InstaAutomateApp
{
    partial class AdminUsersForm
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
            gridUsers = new DataGridView();
            btnDeleteUser = new Button();
            btnRefreshUsers = new Button();

            ((System.ComponentModel.ISupportInitialize)gridUsers).BeginInit();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(300, 15);
            lblTitle.Text = "User Management";

            gridUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridUsers.Location = new Point(12, 55);
            gridUsers.Name = "gridUsers";
            gridUsers.ReadOnly = true;
            gridUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridUsers.MultiSelect = false;
            gridUsers.Size = new Size(760, 320);

            btnDeleteUser.Location = new Point(12, 390);
            btnDeleteUser.Size = new Size(140, 34);
            btnDeleteUser.Text = "Delete User";
            btnDeleteUser.Click += btnDeleteUser_Click;

            btnRefreshUsers.Location = new Point(170, 390);
            btnRefreshUsers.Size = new Size(120, 34);
            btnRefreshUsers.Text = "Refresh";
            btnRefreshUsers.Click += btnRefreshUsers_Click;

            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Text = "Admin - User Management";
            Controls.Add(lblTitle);
            Controls.Add(gridUsers);
            Controls.Add(btnDeleteUser);
            Controls.Add(btnRefreshUsers);

            ((System.ComponentModel.ISupportInitialize)gridUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView gridUsers;
        private Button btnDeleteUser;
        private Button btnRefreshUsers;
    }
}

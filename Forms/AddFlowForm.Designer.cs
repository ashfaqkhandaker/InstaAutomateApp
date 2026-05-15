namespace InstaAutomateApp
{
    partial class AddFlowForm
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
            txtPostUrl = new TextBox();
            txtKeywords = new TextBox();
            txtReply = new TextBox();
            txtDM = new TextBox();
            txtLink = new TextBox();
            btnSave = new Button();
            SuspendLayout();
            // 
            // txtPostUrl
            // 
            txtPostUrl.Location = new Point(210, 28);
            txtPostUrl.Name = "txtPostUrl";
            txtPostUrl.Size = new Size(346, 31);
            txtPostUrl.TabIndex = 0;
            txtPostUrl.Text = "Post URL";
            // 
            // txtKeywords
            // 
            txtKeywords.Location = new Point(210, 74);
            txtKeywords.Name = "txtKeywords";
            txtKeywords.Size = new Size(346, 31);
            txtKeywords.TabIndex = 0;
            txtKeywords.Text = "Keywords";
            // 
            // txtReply
            // 
            txtReply.Location = new Point(210, 120);
            txtReply.Name = "txtReply";
            txtReply.Size = new Size(346, 31);
            txtReply.TabIndex = 0;
            txtReply.Text = "Reply Message";
            // 
            // txtDM
            // 
            txtDM.Location = new Point(210, 168);
            txtDM.Name = "txtDM";
            txtDM.Size = new Size(346, 31);
            txtDM.TabIndex = 0;
            txtDM.Text = "DM Message";
            // 
            // txtLink
            // 
            txtLink.Location = new Point(210, 214);
            txtLink.Name = "txtLink";
            txtLink.Size = new Size(346, 31);
            txtLink.TabIndex = 0;
            txtLink.Text = "Special Link";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(321, 276);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 34);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // AddFlowForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(txtLink);
            Controls.Add(txtDM);
            Controls.Add(txtReply);
            Controls.Add(txtKeywords);
            Controls.Add(txtPostUrl);
            Name = "AddFlowForm";
            Text = "AddFlowForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPostUrl;
        private TextBox txtKeywords;
        private TextBox txtReply;
        private TextBox txtDM;
        private TextBox txtLink;
        private Button btnSave;
    }
}
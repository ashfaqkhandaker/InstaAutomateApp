using InstaAutomateApp.Database;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace InstaAutomateApp
{
    public partial class AddFlowForm : Form
    {
        public int FlowId = 0;

        public AddFlowForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPostUrl.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a Post URL.");
                    return;
                }

                DbHelper db = new DbHelper();
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    if (FlowId == 0)
                    {
                        // INSERT – store UserId so flows are per-user
                        SqlCommand cmd = new SqlCommand(
                            "INSERT INTO AutomationFlows " +
                            "(UserId, PostUrl, Keywords, ReplyMessage, DmMessage, SpecialLink) " +
                            "VALUES (@uid, @p, @k, @r, @d, @l)", conn);

                        cmd.Parameters.AddWithValue("@uid", UserSession.UserId);
                        cmd.Parameters.AddWithValue("@p", txtPostUrl.Text.Trim());
                        cmd.Parameters.AddWithValue("@k", txtKeywords.Text.Trim());
                        cmd.Parameters.AddWithValue("@r", txtReply.Text.Trim());
                        cmd.Parameters.AddWithValue("@d", txtDM.Text.Trim());
                        cmd.Parameters.AddWithValue("@l", txtLink.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // UPDATE – only allow editing own flows
                        SqlCommand cmd = new SqlCommand(
                            "UPDATE AutomationFlows SET " +
                            "PostUrl=@p, Keywords=@k, ReplyMessage=@r, DmMessage=@d, SpecialLink=@l " +
                            "WHERE Id=@id AND UserId=@uid", conn);

                        cmd.Parameters.AddWithValue("@id", FlowId);
                        cmd.Parameters.AddWithValue("@uid", UserSession.UserId);
                        cmd.Parameters.AddWithValue("@p", txtPostUrl.Text.Trim());
                        cmd.Parameters.AddWithValue("@k", txtKeywords.Text.Trim());
                        cmd.Parameters.AddWithValue("@r", txtReply.Text.Trim());
                        cmd.Parameters.AddWithValue("@d", txtDM.Text.Trim());
                        cmd.Parameters.AddWithValue("@l", txtLink.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Saved Successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadFlowData(int id)
        {
            FlowId = id;

            DbHelper db = new DbHelper();
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM AutomationFlows WHERE Id=@id AND UserId=@uid", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@uid", UserSession.UserId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtPostUrl.Text = reader["PostUrl"].ToString();
                    txtKeywords.Text = reader["Keywords"].ToString();
                    txtReply.Text = reader["ReplyMessage"].ToString();
                    txtDM.Text = reader["DmMessage"].ToString();
                    txtLink.Text = reader["SpecialLink"].ToString();
                }
                else
                {
                    MessageBox.Show("Flow not found or you don't have permission to edit it.");
                    this.Close();
                }
            }
        }
    }
}

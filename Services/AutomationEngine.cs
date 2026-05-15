using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using InstaAutomateApp.Database;

namespace InstaAutomateApp.Services
{
    public class AutomationEngine
    {
        DbHelper db = new DbHelper();

        public void ProcessComment(string postUrl, string commentText)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM AutomationFlows", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string dbPost = reader["PostUrl"].ToString();
                    string keywords = reader["Keywords"].ToString();
                    string reply = reader["ReplyMessage"].ToString();
                    string dm = reader["DmMessage"].ToString();
                    string link = reader["SpecialLink"].ToString();

                    var keywordList = keywords.Split(',');

                    if (dbPost == postUrl &&
                        keywordList.Any(k => commentText.ToLower().Contains(k.Trim().ToLower())))
                    {
                        reader.Close();

                        Log("Comment Reply Sent: " + reply);
                        Log("DM Sent: " + dm);

                        if (commentText.ToLower().Contains("done"))
                        {
                            Log("User Follow Verified");
                            Log("Link Sent: " + link);
                        }

                        return;
                    }
                }
            }
        }

        private void Log(string action)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO ActivityLogs (Action, Status) VALUES (@a, 'Success')", conn);
                cmd.Parameters.AddWithValue("@a", action);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
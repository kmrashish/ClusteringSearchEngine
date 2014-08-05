using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace SearchWebVersion
{
    
    public partial class index : System.Web.UI.Page
    {
        public static Boolean loggedIn=false;
        public static string keyword;
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTimeLabelTwo.Text = DateTime.Now.ToString();
            if (loggedIn == true)
            {
                loginLabel.Text = "Hi " + login.username+" !!";
                add_Link.Text += "<b><a href=\"scraper.aspx\" target=\"_new\"> Add data to our index </a></b>";
                logoutButton.Visible = true;

            }
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            loggedIn = false;
            Response.Redirect("index.aspx");
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=(localdb)\\ProjectsV12;Initial Catalog=siteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string time = DateTime.Now.ToString();
            int keyword_exists_at=0;

            keyword = KeywordInputBox.Text;

            string check_for_keyword_existence = "select id from dbo.history where (keyword like '" + keyword + "');";
            SqlCommand check_for_keyword_existence_cmd = new SqlCommand(check_for_keyword_existence, connection);
            //SqlDataReader reader = check_for_keyword_existence_cmd.ExecuteReader();

            try
            {
                keyword_exists_at = (int)check_for_keyword_existence_cmd.ExecuteScalar();
                
            }
            catch (Exception ex1) { }

            //testLabel.Text = "Found at: "+keyword_exists_at.ToString();

            if (keyword_exists_at == 0)
            {
                string add_keyword_to_history = "insert into dbo.history (keyword) values (@keyword)";
                SqlCommand add_keyword_to_history_cmd = new SqlCommand(add_keyword_to_history, connection);

                add_keyword_to_history_cmd.Parameters.AddWithValue("@keyword", keyword);
                add_keyword_to_history_cmd.ExecuteNonQuery();

            }
            else
            {
                string increment_keyword_score = @"update dbo.history set score=score+1 where (id=" + keyword_exists_at + ");";
                SqlCommand increment_keyword_score_command = new SqlCommand(increment_keyword_score, connection);

                increment_keyword_score_command.ExecuteNonQuery();
            }
            
            connection.Close();
            Response.Redirect("Results.aspx");
        }
    }
}
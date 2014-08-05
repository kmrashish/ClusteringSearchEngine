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
    public partial class login : System.Web.UI.Page
    {
        public static string username = "";
        string password = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           

            
        }
        protected void loginAction(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=(localdb)\\ProjectsV12;Initial Catalog=siteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            username = userloginTextBox.Text;
            password = pwdloginTextBox.Text;

            string query = "SELECT count(*)  FROM dbo.auth WHERE (username LIKE \'" + username + "') AND (password LIKE \'" + password + "')";
            SqlCommand cmd = new SqlCommand(query, connection);

            int r = (int)cmd.ExecuteScalar();

            if (r > 0) { index.loggedIn = true; Response.Redirect("index.aspx"); }
            else error_status.Text = "Invalid Credentials";

        }
    }
}
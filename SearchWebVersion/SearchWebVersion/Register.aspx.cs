using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SearchWebVersion
{
    public partial class Register : System.Web.UI.Page
    {
        string name = "";
        string email = "";
        string dob = "";
        string country = "";
        string username = "";
        string pwd = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void registerSubmitButton_Click(object sender, EventArgs e)
        {
            name += nameInputBox.Text;
            email += mailInputBox.Text;
            dob += dobInputBox.Text;
            country += countryInputBox.Text;
            username += UsernameInputBox.Text;
            pwd += pwdInputBox.Text;

            if (Regex.Equals(name, "") || Regex.Equals(email, "") || Regex.Equals(dob, "") || Regex.Equals(country, "")
                || Regex.Equals(username, "") || Regex.Equals(pwd, ""))
                testlabel.Text = "All fields are required, Make sure there is no empty field";
            else
            {

                string ConnectionString = "Data Source=(localdb)\\ProjectsV12;Initial Catalog=siteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                int exists_at = 0;

                SqlCommand check_for_existence = new SqlCommand(@"select Id from dbo.auth where (username like '" + username + "');", connection);
                if (check_for_existence.ExecuteScalar() == null)
                {
                    try
                    {
                        string add_new_user = "INSERT INTO dbo.auth (username, password, name, email, dob, country) VALUES (@username, @pwd, @name, @email, @dob, @country)";
                        SqlCommand add_query = new SqlCommand(add_new_user, connection);

                        add_query.Parameters.AddWithValue("@username", username);
                        add_query.Parameters.AddWithValue("@pwd", pwd);
                        add_query.Parameters.AddWithValue("@name", name);
                        add_query.Parameters.AddWithValue("@email", email);
                        add_query.Parameters.AddWithValue("@dob", dob);
                        add_query.Parameters.AddWithValue("@country", country);

                        add_query.ExecuteNonQuery();
                        testlabel.Text="User created successfully!! <br/> <a href=\"login.aspx\">login</a>";
                    }
                    catch (Exception exx)
                    {
                        connection.Close();
                        testlabel.Text = "transaction failure";
                    }
                }
                else
                {
                    exists_at = (int)check_for_existence.ExecuteScalar();
                    testlabel.Text = "Username exists!! Try another one";
                }
            }

        }
    }
}
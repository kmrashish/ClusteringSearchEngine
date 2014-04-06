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
    public partial class Results : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=(localdb)\\ProjectsV12;Initial Catalog=siteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            ConnectionStatusLabel.Text+="<h3>Connection Status:</h3>"+"Connection String: "+connection.ConnectionString+"<br/>"+"Timeout: "+connection.ConnectionTimeout+
                "<br/>"+"Database: "+connection.Database+"<br/>"+"PacketSize: "+connection.PacketSize+"<br/>"+"Server Version: "+connection.ServerVersion+"<br/>"+
                "Connection State: "+connection.State+"<br/>";

            string keywordCopy = index.keyword;

            ConnectionStatusLabel.Text+= "<br/> You searched for : "+ keywordCopy;

            DateTimeLabel.Text += @DateTime.Now;

            string query = "SELECT * FROM dbo.ind WHERE (url LIKE \'%" + keywordCopy+"%')"+"OR (meta LIKE \'%" +keywordCopy+"%')"
                +"OR (body LIKE \'%" +keywordCopy+"%')"+"OR (cluster LIKE \'%"+keywordCopy+"%')";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader r;
            r=cmd.ExecuteReader();

            ActualResults.Text += "<br/> <br/>";
            if (r.HasRows)
            {
                int i = 1;
                string[] idarray=new string[]{};
                string[] urlarray=new string[]{};
                string[] metaarray=new string[]{};
                string[] bodyarray=new string[]{};
                string[] clusterarray=new string[]{};
                while (r.Read())
                {
                    ActualResults.Text += i + " " + "<a href=\"http://www."+r[1]+"\" target=\"_new\">"+r[1]+"</a>"+"<br/> Cluster : "+r[4]+"<br/>";
                    //idarray[i] = r[0].ToString();
                    //urlarray[i] = r[1].ToString();
                    //metaarray[i] = r[2].ToString();
                    //bodyarray[i] = r[3].ToString();
                    //clusterarray[i] = r[4].ToString();                    
                    ActualResults.Text += "<br/>";
                    i++;
                }
                ActualResults.Text += "<br/>";
            }
            else ActualResults.Text += "No Matches found";          


        }
    }
}
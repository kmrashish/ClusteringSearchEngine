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
    public partial class Results : System.Web.UI.Page
    {
        int[] score=new int[15];
        string[] results = new string[] { };
        static int predicted_cluster_index;
        string predicted_cluster = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            string ConnectionString = "Data Source=(localdb)\\ProjectsV12;Initial Catalog=siteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string keyword = index.keyword;
            keywordHeaderLabel.Text = keyword;      

            DateTimeLabel.Text += @DateTime.Now;
            string cluster = "";

            SqlCommand cmd = new SqlCommand(@"select predicted_cluster from dbo.predicted_cluster_table 
            where id=(select max(id) from dbo.predicted_cluster_table);", connection);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cluster = reader[0].ToString();
            }
            reader.Close();




            //PREDICTED RESULTS FROM URL MATCHES
            cmd.CommandText = @"SELECT DISTINCT url, cluster FROM dbo.ind WHERE (url LIKE '%" + keyword + "%' and cluster like '"+cluster+"');";
            SqlDataReader  r = cmd.ExecuteReader();

            ActualResults.Text += "<br/> <br/>";
            if (r.HasRows)
            {
                ActualResults.Text += "Predicted URL matches: <br>";
                while (r.Read())
                {
                    ActualResults.Text += "<a href=\"" + r[0] + "\" target=\"_new\">" + r[0] + "</a>" + "<br/> Cluster : " + r[1] + "<br/>";
                    ActualResults.Text += "<br/>";
                    CalculateScore(r[1].ToString());
                }
                ActualResults.Text += "<br/>";
            }
            else ActualResults.Text += "No predicted Matches found in URLs";
            r.Close();

            
            //PREDICTED RESULTS FROM META MATCHES
            cmd.CommandText = "SELECT DISTINCT url, cluster FROM dbo.ind WHERE (meta LIKE \'%" + keyword + "%' and cluster like '" + cluster + "');";
            r = cmd.ExecuteReader();

            ActualResults.Text += "<br/> <br/>";
            if (r.HasRows)
            {
                ActualResults.Text += " Predicted matches in meta section: <br>";
                while (r.Read())
                {
                    ActualResults.Text += "<a href=\"" + r[0] + "\" target=\"_new\">" + r[0] + "</a>" + "<br/> Cluster : " + r[1] + "<br/>";
                    ActualResults.Text += "<br/>";
                    CalculateScore(r[1].ToString());
                }
                ActualResults.Text += "<br/>";
            }
            else ActualResults.Text += "No predicted Matches found in meta tags";
            r.Close();

            //PREDICTED RESULTS FROM BODY MATCHES
            cmd.CommandText = "SELECT DISTINCT url, cluster FROM dbo.ind WHERE (body LIKE \'%" + keyword + "%' and cluster like '" + cluster + "');";
            r = cmd.ExecuteReader();

            ActualResults.Text += "<br/> <br/>";
            if (r.HasRows)
            {
                ActualResults.Text += "Predicted matches in body section: <br>";
                while (r.Read())
                {
                    ActualResults.Text += "<a href=\"" + r[0] + "\" target=\"_new\">" + r[0] + "</a>" + "<br/> Cluster : " + r[1] + "<br/>";
                    ActualResults.Text += "<br/>";
                    CalculateScore(r[1].ToString());
                }
                ActualResults.Text += "<br/>";
            }
            else ActualResults.Text += "No predicted Matches found in body section";
            r.Close();

            //NORMAL RESULTS CONTINUE FROM HERE
            cmd.CommandText = "SELECT DISTINCT url, cluster FROM dbo.ind WHERE (url LIKE \'%" + keyword + "%'  or cluster like '%" + keyword + "%');";
            r = cmd.ExecuteReader();

            ActualResults.Text += "<br/> <br/>";
            if (r.HasRows)
            {
                ActualResults.Text += " Other matches in URL section: <br>";
                while (r.Read())
                {
                    ActualResults.Text += "<a href=\"" + r[0] + "\" target=\"_new\">" + r[0] + "</a>" + "<br/> Cluster : " + r[1] + "<br/>";
                    ActualResults.Text += "<br/>";
                    CalculateScore(r[1].ToString());
                }
                ActualResults.Text += "<br/>";
            }
            else ActualResults.Text += "No Other Matches found in url tags";
            r.Close();

            cmd.CommandText = "SELECT DISTINCT url, cluster FROM dbo.ind WHERE (meta LIKE \'%" + keyword + "%'  or cluster like '%" + keyword + "%');";
            r = cmd.ExecuteReader();

            ActualResults.Text += "<br/> <br/>";
            if (r.HasRows)
            {
                ActualResults.Text += " Other matches in meta section: <br>";
                while (r.Read())
                {
                    ActualResults.Text += "<a href=\"" + r[0] + "\" target=\"_new\">" + r[0] + "</a>" + "<br/> Cluster : " + r[1] + "<br/>";
                    ActualResults.Text += "<br/>";
                    CalculateScore(r[1].ToString());
                }
                ActualResults.Text += "<br/>";
            }
            else ActualResults.Text += "No other Matches found in meta tags";
            r.Close();


           

            cmd.CommandText = "SELECT DISTINCT url, cluster FROM dbo.ind WHERE (body LIKE \'%" + keyword + "%'  or cluster like '%" + keyword + "%');";
            r = cmd.ExecuteReader();

            ActualResults.Text += "<br/> <br/>";
            if (r.HasRows)
            {
                ActualResults.Text += " Other matches in body section: <br>";
                while (r.Read())
                {
                    ActualResults.Text += "<a href=\"" + r[0] + "\" target=\"_new\">" + r[0] + "</a>" + "<br/> Cluster : " + r[1] + "<br/>";
                    ActualResults.Text += "<br/>";
                    CalculateScore(r[1].ToString());
                }
                ActualResults.Text += "<br/>";
            }
            else ActualResults.Text += "No other Matches found in body tags";
            r.Close();
            

            int i = 0;
            while (i < 15)
            {
                ActualResults.Text +=  "<br/> Matches found in " +scraper.clusterNames[i]+": "+ score[i];
                i++;
            }

            predicted_cluster_index = Array.IndexOf(score, score.Max());
            predicted_cluster = scraper.clusterNames[predicted_cluster_index];
            //ActualResults.Text += "<br/> " + predicted_cluster;

            cmd.CommandText=@"insert into dbo.predicted_cluster_table (predicted_cluster, time) values (@predicted_cluster,@time);";
            cmd.Parameters.AddWithValue("@predicted_cluster", predicted_cluster);
            cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString());
            cmd.ExecuteNonQuery();


            connection.Close();
        }

        void CalculateScore(string cluster)
        {
            int i = 0;
            while (i < 15)
            {
                if (Regex.Equals(cluster, scraper.clusterNames[i])) score[i]++;
                i++;
            }
        }
    }
}
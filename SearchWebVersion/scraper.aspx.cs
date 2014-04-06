using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SearchWebVersion
{
    public partial class scraper : System.Web.UI.Page
    {
        static int id = 1;
        string links = " ";
        string meta = " ";
        string body = " ";
        string cluster = " ";
        int site_count=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=(localdb)\\ProjectsV12;Initial Catalog=siteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand count_sites=new SqlCommand("select count(*) from dbo.ind", connection);
            //count_sites.CommandText = "select count(*) from dbo.ind";
            site_count = (int)count_sites.ExecuteScalar();

            site_count_label.Text = "No. of URL's in the index right now: "+site_count.ToString();
            id = site_count + 1;
            connection.Close();
        }
        protected void AddUrlButton_Click(object sender, EventArgs e)
        {
                     
            var webGet = new HtmlWeb();
            var document = webGet.Load("http://"+InputTextBox.Text);
            string url = "http://www." + InputTextBox.Text;

            int counter = 1; 

            try
            {
                var aTags = document.DocumentNode.SelectNodes("//a");            
            if (aTags != null)
            {
                foreach (var aTag in aTags)
                {
                    counter++;
                    links+= aTag.Attributes["href"].Value + "\n";
                }
            }
            
            var metaTags = document.DocumentNode.SelectNodes("//meta");

            if (metaTags != null)
            {
                foreach (var tag in metaTags)
                {
                    if (tag.Attributes["name"] != null && tag.Attributes["content"] != null)
                    {
                        if (tag.Attributes["charset"] != null) Console.WriteLine(tag.Attributes["charset"].Value);
                        meta += tag.InnerText;
                    }
                }
            }
            
                var bodyContent = document.DocumentNode.SelectNodes("//body");

                if (bodyContent != null)
                {
                    foreach (var tag in bodyContent)
                    {
                        if (tag.InnerText != null)
                        {
                            body += tag.InnerText;

                        }
                    }
                }
            }
            catch (Exception ex) { }

            cluster += clusterTypeDDL.SelectedItem.ToString();

            meta = Regex.Replace(meta, "<.*?>", string.Empty);
            body = Regex.Replace(body, "<.*?>", string.Empty);
            meta = Regex.Replace(meta, "{.*?}", string.Empty);
            body = Regex.Replace(body, "{.*?}", string.Empty);
            meta = Regex.Replace(meta, "[.*?]", string.Empty);
            body = Regex.Replace(body, "[.*?]", string.Empty);
            meta = Regex.Replace(meta, "\".*?\"", string.Empty);
            body = Regex.Replace(body, "\".*?\"", string.Empty);
            //meta = Regex.Replace(meta, ";.*?;", string.Empty);
            //body = Regex.Replace(body, ";.*?;", string.Empty);
            //meta = Regex.Replace(meta, "(.*?)", string.Empty);
            //body = Regex.Replace(body, "(.*?)", string.Empty);
            

            string ConnectionString = "Data Source=(localdb)\\ProjectsV12;Initial Catalog=siteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();


            
            string add_new_url = "INSERT INTO dbo.ind (Id, url, meta, body, cluster, links) VALUES (@id, @url, @meta, @body, @cluster, @links)";
            SqlCommand add_query = new SqlCommand(add_new_url, connection);

            add_query.Parameters.AddWithValue("@id", id);
            add_query.Parameters.AddWithValue("@url", url);
            add_query.Parameters.AddWithValue("@meta", meta);
            add_query.Parameters.AddWithValue("@body", body);
            add_query.Parameters.AddWithValue("@cluster", cluster);
            add_query.Parameters.AddWithValue("@links", links);

            add_query.ExecuteNonQuery();


            OutputLabel.Text += "<br/><br/>URL: " + url + "<br/><br/>links: " + links + "<br/><br/> meta: " + meta + "<br/><br/> body: " + body + "<br/><br/>cluster: " + cluster;
            Response.Redirect("scraper.aspx");
        }
    }
}

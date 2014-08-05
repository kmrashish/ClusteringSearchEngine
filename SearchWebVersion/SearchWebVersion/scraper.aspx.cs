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
using System.Diagnostics;

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
        public static string[] clusterNames = new string[15]{"Academics", "Education", "Business", "Commercial", "Firm", "Government", "Information", 
            "Jobs", "Military", "Network Administration", "Non-profit Organization", "Professional", "Retail", "Internet Communication", 
            "Internet Site"};

        string[] key_for_cluster = new string[15]{".ac", ".edu", ".biz", ".com", ".firm", ".gov", ".info", ".jobs", ".mil",".net"
            , ".org", ".pro", ".store", ".edu", ".web"};
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (index.loggedIn == true)
            {
                string ConnectionString = "Data Source=(localdb)\\ProjectsV12;Initial Catalog=siteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand count_sites = new SqlCommand("select count(*) from dbo.ind", connection);
                site_count = (int)count_sites.ExecuteScalar();

                site_count_label.Text = "No. of URL's in the index right now: " + site_count.ToString();
                
                connection.Close();
            }
            else { site_count_label.Text = "Please login into the site to add data to our index!!"; AddUrlButton.Visible = false; }
        }
        protected void AddUrlButton_Click(object sender, EventArgs e)
        {

            string url = "http://www." + InputTextBox.Text;

            if (Regex.Equals(url, "http://www.")) OutputLabel.Text = "Please enter data and then press submit";
            else
            {
                
                    var webGet = new HtmlWeb();
                    var document = webGet.Load("http://" + InputTextBox.Text);
                      

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
            }
                catch(Exception edd)
            {}
            
           
            try {
            var metaTags = document.DocumentNode.SelectNodes("//meta");




            if (metaTags != null)
            {
                foreach (var tag in metaTags)
                {
                    if (String.Equals(tag.GetAttributeValue("name", "nothing"), "keywords"))
                    {
                        meta += tag.Attributes["content"].Value;
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

            meta = Regex.Replace(meta, "<.*?>", string.Empty);
            body = Regex.Replace(body, "<.*?>", string.Empty);
            meta = Regex.Replace(meta, "{.*?}", string.Empty);
            body = Regex.Replace(body, "{.*?}", string.Empty);
            meta = Regex.Replace(meta, "[.*?]", string.Empty);
            body = Regex.Replace(body, "[.*?]", string.Empty);
            meta = Regex.Replace(meta, "\".*?\"", string.Empty);
            body = Regex.Replace(body, "\".*?\"", string.Empty);
            meta = Regex.Replace(meta, ";.*?;", string.Empty);
            body = Regex.Replace(body, ";.*?;", string.Empty);
            meta = Regex.Replace(meta, "(.*?)", string.Empty);
            body = Regex.Replace(body, "(.*?)", string.Empty);

            string extracted_domain = "";
            if (Regex.IsMatch(url, ".ac")) extracted_domain=".ac";
            if (Regex.IsMatch(url, ".edu")) extracted_domain = ".edu";
            if (Regex.IsMatch(url, ".biz")) extracted_domain = ".biz";
            if (Regex.IsMatch(url, ".com")) extracted_domain = ".com";
            if (Regex.IsMatch(url, ".firm")) extracted_domain = ".firm";
            if (Regex.IsMatch(url, ".gov")) extracted_domain = ".gov";
            if (Regex.IsMatch(url, ".info")) extracted_domain = ".info";
            if (Regex.IsMatch(url, ".jobs")) extracted_domain = ".jobs";
            if (Regex.IsMatch(url, ".mil")) extracted_domain = ".mil";
            if (Regex.IsMatch(url, ".net")) extracted_domain = ".net";
            if (Regex.IsMatch(url, ".org")) extracted_domain = ".org";
            if (Regex.IsMatch(url, ".pro")) extracted_domain = ".pro";
            if (Regex.IsMatch(url, ".store")) extracted_domain = ".store";
            if (Regex.IsMatch(url, ".tel")) extracted_domain = ".tel";
            if (Regex.IsMatch(url, ".web")) extracted_domain = ".web";

            

            int[] levenshtein_distance = new int[15];
            int i = 0;
            while (i < 15)
            {
                string key_for_cluster_temp = key_for_cluster[i];
                levenshtein_distance[i] = LevenshteinDistance(extracted_domain, extracted_domain.Length, key_for_cluster_temp, key_for_cluster_temp.Length);
                i++;
            }


            int min_index = Array.IndexOf(levenshtein_distance, levenshtein_distance.Min());

            cluster = clusterNames[min_index];


            string ConnectionString = "Data Source=(localdb)\\ProjectsV12;Initial Catalog=siteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            

                string add_new_url = "INSERT INTO dbo.ind (url, meta, body, cluster, links) VALUES (@url, @meta, @body, @cluster, @links)";
                SqlCommand add_query = new SqlCommand(add_new_url, connection);

                add_query.Parameters.AddWithValue("@url", url);
                add_query.Parameters.AddWithValue("@meta", meta);
                add_query.Parameters.AddWithValue("@body", body);
                add_query.Parameters.AddWithValue("@cluster", cluster);
                add_query.Parameters.AddWithValue("@links", links);

                add_query.ExecuteNonQuery();
                connection.Close();
                Response.Redirect("scraper.aspx");
            }
           
        }

        int LevenshteinDistance(string s, int s_len, string t, int t_len)
        {
            //base case: Empty Strings
            if (s_len == 0) return t_len;
            if (t_len == 0) return s_len;
            int cost;
            //test if the last characters of the strings match
            if (s[s_len - 1] == t[t_len - 1]) cost = 0;
            else cost = 1;

            //return the minimum of delete char from s, delete char from t, and delete char from both
            return minimum(LevenshteinDistance(s, s_len - 1, t, t_len) + 1,
                         LevenshteinDistance(s, s_len, t, t_len - 1) + 1,
                         LevenshteinDistance(s, s_len - 1, t, t_len - 1) + cost);
        }

        int minimum(int x, int y, int z)
        {
            return Math.Min(x, Math.Min(y, z));
        }
    }
}

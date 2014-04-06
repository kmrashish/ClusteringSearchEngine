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
        public static string keyword;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            keyword = KeywordInputBox.Text;
            Response.Redirect("Results.aspx");
        }
    }
}
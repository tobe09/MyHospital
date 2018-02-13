using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManager
{
    public partial class Patients : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null)
            {
                Session["Info"] = "You do not have access to the requested page";
                Response.Redirect("LoggedinPage.aspx");
            }
        }

        protected void PatDisabLinkButton_Click(object sender, EventArgs e)
        {
            Session["Quote"] = "SuperUser patient disability click";
            Response.Redirect("~/PatientDisabilities.aspx");
        }
    }
}
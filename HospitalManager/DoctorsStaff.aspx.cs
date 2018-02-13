using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManager
{
    public partial class Staffs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null)
            {
                Session["Info"] = "You do not have access to the requested page";
                Response.Redirect("LoggedinPage.aspx");
            }
        }

        protected void DocEmpLinkButton_Click(object sender, EventArgs e)
        {
            Session["Quote"] = "Privileged Doctor Employment";
            Response.Redirect("~/DoctorStaffHistory.aspx");
        }

        protected void StaffEmpLinkButton_Click(object sender, EventArgs e)
        {
            Session["Quote"] = "Privileged Staff Employment";
            Response.Redirect("~/DoctorStaffHistory.aspx");
        }

        protected void DocActLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WorkSchedule.aspx");
        }
    }
}
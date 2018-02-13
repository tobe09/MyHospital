using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManager
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null && Session["User"] == null)
            {
                profilePageLink.Visible = false;
                LogOutLinkButton.Visible = false;
            }
            else
            {
                StatusLabel.Visible = true;
                if (Session["Info"] != null) { StatusLabel.Text = (string)Session["Info"]; Session["Info"] = null; }     //redirecting from login page
                string regStatus = "";
                if (Session["RegStatus"] != null) regStatus = (string)Session["RegStatus"];  //registration status of user
                if (regStatus.ToUpper() == "VALIDATED")
                {
                    StatusLabel.CssClass = "success";
                    StatusLabel.Text += "- Validated User".ToUpper();
                }
                else
                {
                    StatusLabel.CssClass = "error";
                    StatusLabel.Text = "Please Validate registration with the system administrator";
                }
            }
        }

        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {
            string updateCode = "",
                updaterId = "",
                userId = "";
            //set update code
            if (Session["User"].ToString().StartsWith("SUP")) updateCode = "SUP_LGO";
            else if (Session["User"].ToString().StartsWith("ADM")) updateCode = "ADM_LGO";
            else if (Session["User"].ToString().StartsWith("DC")) updateCode = "DOC_LGO";
            else if (Session["User"].ToString().StartsWith("ST")) updateCode = "STF_LGO";
            else updateCode = "PAT_LGO"; 
            //set updater id
            if (Session["SuperUser"] != null) updaterId = Session["SuperUser"].ToString();
            else if (Session["Admin"] != null) updaterId = Session["Admin"].ToString();
            else updaterId = Session["User"].ToString();
            //set user id
            userId = Session["User"].ToString();
            //dispose sessions
            Session["SuperUser"] = null;
            Session["Admin"] = null;
            Session["User"] = null;
            List<string> values = new List<string>();
            values.Add(HospitalClass.getTransactionId());
            values.Add(updateCode);
            values.Add(updaterId);
            values.Add(userId);
            int status = DataConsumer.executeProcedure("audit_trail_proc", values);
            Response.Redirect("~/Login.aspx");
        }

    }
}
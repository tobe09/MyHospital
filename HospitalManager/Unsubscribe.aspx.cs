using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManager
{
    public partial class Unsubscribe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null)
            {
                Session["Info"] = "You do not have access to the requested page";
                Response.Redirect("LoggedinPage.aspx");
            }
            if (Session["User"] != null) UserIdBox.Text = Session["User"].ToString();
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (UnsubscribeCheckBox.Checked && ReasonBox.Text.Trim().Length>3)
                {
                    string updateCode="",
                        updaterId="";
                    if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                    else updaterId = (string)Session["Admin"];
                    if (UserIdBox.Text.StartsWith("ST")) updateCode="UNSB_STF";
                    else updateCode = "UNSB_" + HospitalClass.getTableName(UserIdBox.Text.Substring(0, 2)).Substring(0, 3).ToUpper();
                    //delete picture from iis server
                    string picAddressQuery = DataProvider.Unsubscribe.getPicAddress(UserIdBox.Text);
                    System.Data.DataTable dt = HospitalClass.getDataTable(picAddressQuery);
                    if (dt.Rows.Count == 1) System.IO.File.Delete(Server.MapPath(dt.Rows[0][0].ToString()));
                    //delete from database and update necessary tables
                    List<string> values = new List<string>();
                    values.Add(ReasonBox.Text);
                    values.Add(HospitalClass.getTransactionId());
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add(UserIdBox.Text);
                    values.Add(HospitalClass.getTableName(UserIdBox.Text.Substring(0, 2)));
                    int status = DataConsumer.executeProcedure("unsb_proc", values);
                    //return user privileges
                    if (Session["SuperUser"] != null)
                    {
                        if (Session["SuperUser"].ToString() != Session["User"].ToString()) Session["User"] = Session["SuperUser"].ToString();
                        else
                        {
                            Session["SuperUser"] = null;
                            Session["Admin"] = null;
                            Session["User"] = null;
                            Response.Redirect("~/Login.aspx");
                        }
                    }
                    else if (Session["Admin"] != null)
                    {
                        if (Session["Admin"].ToString() != Session["User"].ToString()) Session["User"] = Session["Admin"].ToString();
                        else
                        {
                            Session["SuperUser"] = null;
                            Session["Admin"] = null;
                            Session["User"] = null;
                            Response.Redirect("~/Login.aspx");
                        }
                    }
                    SubmitButton.Visible = false;
                    StatusLabel.CssClass = "success";
                    StatusLabel.Text = "Successful Unsubscription.<br>User Id: " + UserIdBox.Text;
                }
                else
                {
                    StatusLabel.CssClass = "error";
                    if (!UnsubscribeCheckBox.Checked) StatusLabel.Text = "Unsubscription was not enforced";
                    else StatusLabel.Text = "Please enter a valid reason";
                }
            }
            catch (Exception ex)
            {
                StatusLabel.CssClass = "error";
                StatusLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

    }
}
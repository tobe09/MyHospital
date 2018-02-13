using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class Auditing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null)
            {
                Session["Info"] = "You do not have access to the requested page";
                Response.Redirect("LoggedinPage.aspx");
            }
        }

        protected void ExecuteButton_Click(object sender, EventArgs e)
        {
            try
            {
                TableDiv.Visible = false;  //visisbility depends on operation
                if (QueryBox.Text.Trim().Length > 6)
                {
                    StatusLabel.CssClass = "success";
                    string query = QueryBox.Text.Trim().Substring(0, 6).ToUpper();
                    string updatecode = query;
                    if (query == "SELECT")
                    {
                        DataTable dt = HospitalClass.getDataTable(QueryBox.Text.Trim());
                        TableLabel.Text = HospitalClass.drawDataTableInHtml(dt, 1036);  //draw the data table on a label control as html
                        TableDiv.Visible = true;
                        StatusLabel.CssClass = "success paraNormal";
                        StatusLabel.Text = "Successful.";
                    }
                    else
                    {
                        int status = DataConsumer.executeQuery(QueryBox.Text.Trim());
                        StatusLabel.Text = "Successful.<br/>Rows affected: " + status + "Rows.";
                        if (System.Text.RegularExpressions.Regex.IsMatch(query, "(^(DROP)|(TRUNC)|(DELETE))")) updatecode = "DELETE";
                        if (!(query == "CREATE" || query == "INSERT" || query == "UPDATE" || System.Text.RegularExpressions.Regex.IsMatch(query, "(^(DROP)|(TRUNC)|(DELETE))")))
                        { updatecode = "OTHER"; }
                    }
                    TransBox.Text = HospitalClass.getTransactionId();
                    if (UpdCodeOverrideBox.Text.Trim().Length == 0) UpdateCodeBox.Text = "A_" + updatecode;
                    else UpdateCodeBox.Text = UpdCodeOverrideBox.Text;
                    UpdaterBox.Text = Session["SuperUser"].ToString();
                    UserIdBox.Text = Session["User"].ToString();
                    DateUpdBox.Text = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
                    if (AuditCheckBox.Checked)
                    {
                        List<string> values = new List<string>();
                        values.Add(TransBox.Text);
                        values.Add(UpdateCodeBox.Text);
                        values.Add(UpdaterBox.Text);
                        values.Add(UserIdBox.Text);
                        int status = DataConsumer.executeProcedure("audit_trail_proc", values);
                        AuditLabel.CssClass = "success";
                        AuditLabel.Text = "ADDED";
                    }
                    else
                    {
                        AuditLabel.ForeColor = System.Drawing.Color.Brown;
                        AuditLabel.Text = "NOT ADDED";
                    }
                }
                else
                {
                    StatusLabel.CssClass = "error";
                    StatusLabel.Text = "Invalid query";
                }
            }
            catch (Exception ex)
            {
                StatusLabel.CssClass = "error paraNormal";
                StatusLabel.Text = "Error: " + ex.Message;
                if (ex.GetType().ToString() != "System.Data.OracleClient.OracleException") HospitalClass.Log(ex);
            }
        }

        protected void ShowTableInfoLinkButton_Click(object sender, EventArgs e)
        {
            if (TableInfoDiv.Visible)
            {
                TableInfoDiv.Visible = false;
                ShowTableInfoLinkButton.Text = "Show DB tables";
            }
            else
            {
                TableInfoDiv.Visible = true;
                ShowTableInfoLinkButton.Text = "Hide DB tables";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class InformationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null)
            {
                Session["Info"] = "You do not have access to the requested page";
                Response.Redirect("LoggedinPage.aspx");
            }
            if (!ExtUserCheckBox.Checked)
            {
                UserIdBox.Text = Session["User"].ToString();
                UserIdBox.ReadOnly = true;
            }
            else UserIdBox.ReadOnly = false;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string checkTopicExist = DataProvider.AddInformationPage.checkTopic(TopicBox.Text.Trim());
            DataTable dt = HospitalClass.getDataTable(checkTopicExist);
            if ((UserIdBox.Text.Length >= 3 && UserIdBox.Text.Length <= 32) && (TopicBox.Text.Trim().Length >= 2 && TopicBox.Text.Length <= 32) &&
                (InfoBox.Text.Length >= 4 && InfoBox.Text.Length <= 256) && RecipientList.SelectedIndex != 0 && dt.Rows.Count == 0)
            {
                List<string> values = new List<string>();
                values.Add(InfoBox.Text);
                values.Add(RecipientList.SelectedItem.Value);
                values.Add(HospitalClass.PascalCasing(TopicBox.Text.Trim()));
                values.Add(HospitalClass.getTransactionId());
                values.Add("INFO_ADD");
                values.Add(UpdaterId());
                values.Add(UserIdBox.Text);
                values.Add("");
                values.Add("insert");
                int status = DataConsumer.executeProcedure("info_proc", values);
                StatusLabel.CssClass = "success";
                StatusLabel.Text = "Successful.<br/>Updater: " + UpdaterId() + ".";
            }
            else
            {
                StatusLabel.CssClass = "error";
                if (dt.Rows.Count != 0) StatusLabel.Text = "This topic already exists. Choose another topic name";
                else if (RecipientList.SelectedIndex == 0) StatusLabel.Text = "Please select a recipient classification";
                else if (!(UserIdBox.Text.Length >= 3 && UserIdBox.Text.Length <= 32)) StatusLabel.Text = "Please enter a valid user Id.<br/>Between 6 and 32 characters";
                else if ((TopicBox.Text.Length >= 2 && TopicBox.Text.Length <= 32)) StatusLabel.Text = "Please enter a valid topic.<br/>Between 2 and 32 characters";
                else StatusLabel.Text = "Please enter valid information.<br/>Between 4 and 256 characters";
            }
        }

        public string UpdaterId()
        {
            string updaterId = "";
            if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
            else updaterId = (string)Session["Admin"];
            return updaterId;
        }
    }
}
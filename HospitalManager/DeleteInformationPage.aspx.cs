using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class DeleteInformationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null)
            {
                Session["Info"] = "You do not have access to the requested page";
                Response.Redirect("LoggedinPage.aspx");
            }
            SortButton_Click(new object(), new EventArgs());
            TopicDelLabel.Text = "";
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndex >= 0)
            {
                string topic = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("TopicLabel")).Text;
                List<string> values = new List<string>();
                values.Add(topic);
                values.Add(HospitalClass.getTransactionId());
                values.Add("INFO_DEL");
                values.Add(UpdaterId());
                int status = DataConsumer.executeProcedure("info_del_proc", values);
                TopicDelLabel.CssClass = "success";
                TopicDelLabel.Text = "Topic Deleted: " + topic;
                SortButton_Click(new object(), new EventArgs());
            }
            else
            {
                TopicDelLabel.CssClass = "error paraNormal";
                TopicDelLabel.Text = "No item selected for deletion";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            TopicDelLabel.Text = "";
            ListView1.SelectedIndex = -1;
        }

        public string UpdaterId()
        {
            string updaterId = "";
            if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
            else updaterId = (string)Session["Admin"];
            return updaterId;
        }

        protected void ModifyButton_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndex >= 0)
            {
                addDelDiv.Visible = true;
                DivIdBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("SenderLabel")).Text;
                DivTopicBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("TopicLabel")).Text;
                DivDescBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("InformationLabel")).Text;
                RecipientList.SelectedIndex = 0;
                TopicDelLabel.CssClass = "modifyColor paraNormal";
                TopicDelLabel.Text = "Edit the required fields and enforce modification";
            }
            else
            {
               TopicDelLabel.CssClass = "error paraNormal";
               TopicDelLabel.Text = "No item selected for modification";
                addDelDiv.Visible = false;
            }
        }

        protected void DivModifyButton_Click(object sender, EventArgs e)
        {
            addDelDiv.Visible = true;
            //check for changes
            bool check = DivTopicBox.Text.ToUpper() == ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("TopicLabel")).Text.ToUpper();
            check = check && DivIdBox.Text.ToUpper() == ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("SenderLabel")).Text.ToUpper();
            check = check && DivDescBox.Text == ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("InformationLabel")).Text;
            check = check && RecipientList.SelectedItem.Text == ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("RecipientLabel")).Text;
            //check for existence
            string checkTopic = DataProvider.DeleteInformationPage.checkTopic(HospitalClass.PascalCasing(DivTopicBox.Text.Trim()));
            DataTable dt = HospitalClass.getDataTable(checkTopic);
            //check for acceptance of change
            bool check2 = DivTopicBox.Text.ToUpper() == ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("TopicLabel")).Text.ToUpper();
            if (DivTopicBox.Text.Trim().Length >= 2 && DivDescBox.Text.Length >= 3 && DivIdBox.Text.Trim().Length >= 1 && RecipientList.SelectedIndex != 0 &&
                !check && ((!check2 && dt.Rows.Count == 0) || (check2 && dt.Rows.Count == 1)))
            {
                string updaterId = "",
                    updateCode = "INFO_UPD";
                if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                else updaterId = (string)Session["Admin"];
                List<string> values = new List<string>();
                values.Add(DivDescBox.Text);
                values.Add(RecipientList.SelectedItem.Value);
                values.Add(HospitalClass.PascalCasing(DivTopicBox.Text.Trim()));
                values.Add(HospitalClass.getTransactionId());
                values.Add(updateCode);
                values.Add(updaterId);
                values.Add(DivIdBox.Text);
                values.Add(((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("TopicLabel")).Text);
                values.Add("update");
                int status = DataConsumer.executeProcedure("info_proc", values);
                TopicDelLabel.CssClass = "success normal";
                TopicDelLabel.Text = "Successful information modification.<br/> Updater: " + updaterId;
                addDelDiv.Visible = false;
                SortButton_Click(new object(), new EventArgs());
                ListView1.SelectedIndex = -1;
            }
            else
            {
                TopicDelLabel.CssClass = "error paraNormal";
                TopicDelLabel.Text = "User input error below";
                DivStatusLabel.CssClass = "error paraNormal";
                if (DivIdBox.Text.Trim().Length < 1) DivStatusLabel.Text = "Enter a valid ID";
                else if (DivTopicBox.Text.Trim().Length < 2) DivStatusLabel.Text = "Enter a valid topic name";
                else if (DivDescBox.Text.Length < 3) DivStatusLabel.Text = "Enter valid information";
                else if (RecipientList.SelectedIndex == 0) DivStatusLabel.Text = "Please select a recipient";
                else if (check) DivStatusLabel.Text = "No change made";
                else DivStatusLabel.Text = "The topic already exists";
            }
        }

        protected void SortDivButton_Click(object sender, EventArgs e)
        {
            if (AscDescSpan.Visible) { AscDescSpan.Visible = false; SortDivButton.Text = "Sort:"; }
            else Sorting(new object(), new EventArgs());
        }

        protected void Sorting(object sender, EventArgs e)
        {
            AscDescSpan.Visible = true;
            SortDivButton.Text = "Enforce";
        }

        //to sort the table of information
        protected void SortButton_Click(object sender, EventArgs e)
        {
            string selectOrder = "";
            if (DateRadioButton.Checked)
            {
                if (DescRadioButton.Checked) selectOrder = "to_number(create_id) desc";
                else selectOrder = "to_number(create_id) asc";
            }
            else
            {
                if (DescRadioButton.Checked) selectOrder = "topic desc";
                else selectOrder = "topic asc";
            }
            if (!AlphabeticRadioButton.Checked && !DateRadioButton.Checked) BindListView();
            else BindListView(selectOrder);
            addDelDiv.Visible = false;
        }

        #region "Bind the listview"
        private void BindListView(string selectOrder="to_number(trans_id) desc")
        {
            SqlDataSource1.ConnectionString = HospitalClass.getConnectionString().Substring(0, 60);
            SqlDataSource1.ProviderName = "System.Data.OracleClient";
            SqlDataSource1.SelectCommand = DataProvider.DeleteInformationPage.getGeneralInfo(selectOrder);
            ListView1.DataSourceID = "SqlDataSource1";
            ListView1.DataBind();
        }
        #endregion

        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndex >= 0)
            {
                string topic = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("TopicLabel")).Text;
                TopicDelLabel.CssClass = "selectedColor";
                TopicDelLabel.Text = "Topic selected: " + topic;
                addDelDiv.Visible = false;
            }
        }

        //to handle change in paging during execution
        protected void ListView1_PagePropertiesChanged(object sender, EventArgs e)
        {
            addDelDiv.Visible = false;
            ListView1.SelectedIndex = -1;
        }

    }
}
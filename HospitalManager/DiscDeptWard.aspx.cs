using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class DeptWardDisc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null)
            {
                Session["Info"] = "You do not have access to the requested page";
                Response.Redirect("LoggedinPage.aspx");
            }
            if (!IsPostBack) populateDeptList();
            if (DeptNameLabel.Text == "") noDeptSelected();
            else deptSelected();
        }

        protected void deptSelected()
        {
            DeptList.Visible = false;
            DeptSelectButton.Visible = false;
            wardDiv.Visible = true;
            DeptTable.Visible = true;
            CancelButton.Visible = true;
            BindListView();
            DeptMessageLabel.Text = "You can select another department after this unrelation (or upon cancellation).";
        }

        protected void noDeptSelected()
        {
            DeptList.Visible = true;
            DeptSelectButton.Visible = true;
            wardDiv.Visible = false;
            DeptTable.Visible = false;
            CancelButton.Visible = false;
            DeptMessageLabel.Text = "Select department by name";
        }

        protected void populateDeptList()
        {
            string getDeptsQuery = DataProvider.DiscDeptWard.getDepartments();
            DataTable dt = HospitalClass.getDataTable(getDeptsQuery);
            DeptList.DataSource = dt;
            DeptList.DataTextField = "DEPT_NAME";
            DeptList.DataBind();
            DeptList.Items.Insert(0, new ListItem("Please select..."));
        }

        protected void BindListView()
        {
            SqlDataSource1.ConnectionString = HospitalClass.getConnectionString().Substring(0, 60);
            SqlDataSource1.ProviderName = "System.Data.OracleClient";
            SqlDataSource1.SelectCommand = DataProvider.DiscDeptWard.addWardsToListView(DeptNameLabel.Text);
            ListView1.DataSourceID = "SqlDataSource1";
            ListView1.DataBind();
        }

        protected void DeptSelectButton_Click(object sender, EventArgs e)
        {
            if (DeptList.SelectedIndex != 0)
            {
                deptSelected();
                string getDeptTableInfoQuery = DataProvider.DiscDeptWard.getDeptInfo(DeptList.SelectedItem.Text);
                DataTable dt = HospitalClass.getDataTable(getDeptTableInfoQuery);
                DeptIdLabel.Text = dt.Rows[0]["DEPT_ID"].ToString();
                DeptNameLabel.Text = dt.Rows[0]["DEPT_NAME"].ToString();
                DeptDescLabel.Text = dt.Rows[0]["DESCRIPTION"].ToString();
                CancelButton.Visible = true;
                BindListView();
                StatusLabel.CssClass += " addColor";
                StatusLabel.Text = "choose wards to be unrelated from table above";
            }
            else
            {
                StatusLabel.CssClass = "error";
                StatusLabel.Text = "Please select a department";
            }
            ListView1.SelectedIndex = -1;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            noDeptSelected();
            ListView1.SelectedIndex = -1;
            populateDeptList();
            DeptList.SelectedIndex = 0;
            DeptNameLabel.Text = "";
        }

        protected void DiscOneButton_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndex >= 0)
            {
                string updaterId = "",
                    updateCode = "DISC_DW1";
                if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                else updaterId = (string)Session["Admin"];
                List<string> values = new List<string>();
                values.Add(DeptNameLabel.Text);
                values.Add(((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardNameLabel")).Text);
                values.Add(HospitalClass.getTransactionId());
                values.Add(updateCode);
                values.Add(updaterId);
                values.Add("one");
                int status = DataConsumer.executeProcedure("dept_ward_disc", values);
                StatusLabel.CssClass = "success";
                StatusLabel.Text = "Successful removal.<br/>Disconnected ward name: " +
                    ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardNameLabel")).Text;
                BindListView();
                if (ListView1.Items.Count <= 0) CancelButton_Click(new object(), new EventArgs());
            }
            else
            {
                StatusLabel.CssClass = "error paraNormal";
                if (ListView1.SelectedIndex < 0) StatusLabel.Text = "No item selected for disconnection/unrelation";
            }
            ListView1.SelectedIndex = -1;
        }

        protected void DiscAllButton_Click(object sender, EventArgs e)
        {
            string updaterId = "",
                updateCode = "DISC_DWA";
            if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
            else updaterId = (string)Session["Admin"];
            List<string> values = new List<string>();
            values.Add(DeptNameLabel.Text);
            values.Add("");
            values.Add(HospitalClass.getTransactionId());
            values.Add(updateCode);
            values.Add(updaterId);
            values.Add("all");
            int status = DataConsumer.executeProcedure("dept_ward_disc", values);
            StatusLabel.CssClass = "success";
            StatusLabel.Text = "Successful removal.<br/>Department name: " + DeptNameLabel.Text + ".<br/>Number of wards removed: " +
                ListView1.Items.Count;
            BindListView();
            CancelButton_Click(new object(), new EventArgs());
        }

    }
}
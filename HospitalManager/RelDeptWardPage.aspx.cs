using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class DeptWardRelPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null)
            {
                Session["Info"] = "You do not have access to the requested page";
                Response.Redirect("LoggedinPage.aspx");
            }
            if (!IsPostBack)
            {
                populateDeptList();
                clearWardTemp();
            }
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
            DeptMessageLabel.Text = "You can select another department after this relationship (or upon cancellation).";
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

        protected void clearWardTemp()  //clear temporary ward table
        {
            List<string> values = new List<string>();
            values.Add("ward_temp");
            DataConsumer.executeProcedure("del_temp_proc", values);
        }

        protected void populateDeptList()
        {
            string getDeptsQuery = DataProvider.RelDeptWardPage.getDepartments();
            DataTable dt = HospitalClass.getDataTable(getDeptsQuery);
            DeptList.DataSource = dt;
            DeptList.DataTextField = "DEPT_NAME";
            DeptList.DataBind();
            DeptList.Items.Insert(0, new ListItem("Please select..."));
        }

        protected void populateWardList()
        {
            string getWardsQuery = DataProvider.RelDeptWardPage.getWards();
            DataTable dt = HospitalClass.getDataTable(getWardsQuery);
            WardList.DataSource = dt;
            WardList.DataTextField = "WARD_NAME";
            WardList.DataBind();
            WardList.Items.Insert(0, new ListItem("Please select..."));
        }

        protected void BindListView()
        {
            SqlDataSource1.ConnectionString = HospitalClass.getConnectionString().Substring(0, 60);
            SqlDataSource1.ProviderName = "System.Data.OracleClient";
            SqlDataSource1.SelectCommand = DataProvider.RelDeptWardPage.addWardsToListView();
            ListView1.DataSourceID = "SqlDataSource1";
            ListView1.DataBind();
        }

        protected void DeptSelectButton_Click(object sender, EventArgs e)
        {
            if (DeptList.SelectedIndex != 0)
            {
                deptSelected();
                string getDeptTableInfoQuery = DataProvider.RelDeptWardPage.getDeptInfo(DeptList.SelectedItem.Text);
                DataTable dt = HospitalClass.getDataTable(getDeptTableInfoQuery);
                DeptIdLabel.Text = dt.Rows[0]["DEPT_ID"].ToString();
                DeptNameLabel.Text = dt.Rows[0]["DEPT_NAME"].ToString();
                DeptDescLabel.Text = dt.Rows[0]["DESCRIPTION"].ToString();
                populateWardList();
                CancelButton.Visible = true;
                BindListView();
                StatusLabel.CssClass += " addColor";
                StatusLabel.Text = "Add wards to be related from the drop down list";
            }
            else
            {
                StatusLabel.CssClass = "error";
                StatusLabel.Text = "Please select a department";
            }
            ListView1.SelectedIndex = -1;
        }

        protected void WardAddButton_Click(object sender, EventArgs e)
        {
            if (WardList.SelectedIndex != 0)
            {
                string getWardInfo = DataProvider.RelDeptWardPage.getWardInfo(WardList.SelectedItem.Text);
                DataTable dt = HospitalClass.getDataTable(getWardInfo);
                List<string> values = new List<string>();
                values.Add(dt.Rows[0]["WARD_ID"].ToString());
                values.Add(dt.Rows[0]["WARD_NAME"].ToString());
                values.Add(dt.Rows[0]["DESCRIPTION"].ToString());
                values.Add("add");
                DataConsumer.executeProcedure("ward_temp_proc", values);
                BindListView();
                populateWardList();
                StatusLabel.CssClass += "success";
                StatusLabel.Text = "Added";
            }
            else
            {
                StatusLabel.CssClass = "error";
                StatusLabel.Text = "Please select a ward";
            }
            ListView1.SelectedIndex = -1;
        }

        protected void RemoveButton_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndex >= 0)
            {
                List<string> values = new List<string>();
                values.Add("");
                values.Add(((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardNameLabel")).Text);
                values.Add("");
                values.Add("remove");
                DataConsumer.executeProcedure("ward_temp_proc", values);
                BindListView();
                populateWardList();
                StatusLabel.CssClass += "success";
                StatusLabel.Text = "Removed";
            }
            else
            {
                StatusLabel.CssClass = "error";
                StatusLabel.Text = "Please select a ward to remove";
            }
            ListView1.SelectedIndex = -1;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            clearWardTemp();
            noDeptSelected();
            ListView1.SelectedIndex = -1; 
            DeptList.SelectedIndex = 0;
            DeptNameLabel.Text = "";
        }

        protected void RelateButton_Click(object sender, EventArgs e)
        {
            string updaterId = "",
                updateCode = "DEPT_WRD";
            if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
            else updaterId = (string)Session["Admin"];
            List<string> values = new List<string>();
            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                values.Add(DeptNameLabel.Text);
                values.Add(((Label)ListView1.Items[i].FindControl("WardNameLabel")).Text);
                values.Add("");
                values.Add("");
                values.Add("");
                values.Add("add");
                int status = DataConsumer.executeProcedure("dept_ward_rel", values);
                values.Clear();
            }
            values.Add(DeptNameLabel.Text);
            values.Add("");
            values.Add(HospitalClass.getTransactionId());
            values.Add(updateCode);
            values.Add(updaterId);
            values.Add("final");
            int statusFinal = DataConsumer.executeProcedure("dept_ward_rel", values);
            StatusLabel.CssClass = "success";
            StatusLabel.Text = "Successful relationship.<br/>Department Name: " + DeptNameLabel.Text + "<br/>Ward added: " + 
                ListView1.Items.Count.ToString() + ".";
            clearWardTemp();
            DeptNameLabel.Text = "";
            noDeptSelected();
            populateDeptList();
        }

    }
}
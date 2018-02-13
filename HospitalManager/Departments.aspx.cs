using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null)
            {
                Session["Info"] = "You do not have access to the requested page";
                Response.Redirect("LoggedinPage.aspx");
            }
            SortButton_Click(new object(), new EventArgs());
            StatusLabel.Text = "";
            DivStatusLabel.Text = "";
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            ListView1.SelectedIndex = -1;
            addDelDiv.Visible = true;
            DivHeaderLabel.Text = "Add Department";
            DivAddButton.Visible = true;
            DivModifyButton.Visible = false;
            DivNameBox.Text = "";
            DivDescBox.Text = "";
            DivIdBox.Text = "";
            StatusLabel.CssClass = "addColor paraNormal";
            StatusLabel.Text = "Fill the required fields below and enforce addition below";
        }

        protected void ModifyButton_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndex >= 0)
            {
                addDelDiv.Visible = true;
                DivHeaderLabel.Text = "Modify/Edit Department";
                DivModifyButton.Visible = true;
                DivAddButton.Visible = false;
                DivNameBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptNameLabel")).Text;
                DivDescBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptDescLabel")).Text;
                DivIdBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptIdLabel")).Text;
                StatusLabel.CssClass = "modifyColor paraNormal";
                StatusLabel.Text = "Edit the required fields below and enforce modification below";
            }
            else
            {
                StatusLabel.CssClass = "error paraNormal";
                StatusLabel.Text = "No item selected for modification";
                addDelDiv.Visible = false;
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            addDelDiv.Visible = false;
            ListView1.SelectedIndex = -1;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                addDelDiv.Visible = false;
                if (ListView1.SelectedIndex >= 0)
                {
                    string deptName = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptNameLabel")).Text;
                    if (((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptWardsLabel")).Text == "0")
                    {
                        string updaterId = "",
                            updateCode = "DEPT_DEL";
                        if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                        else updaterId = (string)Session["Admin"];
                        List<string> values = new List<string>();
                        values.Add("");
                        values.Add(deptName);
                        values.Add("");
                        values.Add("");
                        values.Add(HospitalClass.getTransactionId());
                        values.Add(updateCode);
                        values.Add(updaterId);
                        values.Add("delete");
                        int status = DataConsumer.executeProcedure("dept_proc", values);
                        StatusLabel.CssClass = "success normal";
                        StatusLabel.Text = "Successful department deletion.<br>Department Id: "  + ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptIdLabel")).Text +
                            "<br/>Department Name: " + deptName + ".";
                        SortButton_Click(new object(), new EventArgs());
                    }
                    else
                    {
                        StatusLabel.CssClass = "error normal";
                        StatusLabel.Text = "This department is already linked to ward(s).<br/>Disconnect/Unrelate before deleting.";
                    }
                }
                else
                {
                    StatusLabel.CssClass = "error paraNormal";
                    StatusLabel.Text = "No item selected for deletion";
                }
                ListView1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                StatusLabel.CssClass = "error paraNormal";
                StatusLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void DivAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                addDelDiv.Visible = true;
                if (DivNameBox.Text.Trim().Length >= 2 && DivDescBox.Text.Length >= 3 && DivIdBox.Text.Trim().Length >= 1)
                {
                    //check for existence
                    string checkDeptIdName = DataProvider.Departments.deptIdName(DivIdBox.Text.Trim().ToUpper(), HospitalClass.PascalCasing(DivNameBox.Text.Trim()));
                    DataTable dt = HospitalClass.getDataTable(checkDeptIdName);
                    if (dt.Rows.Count == 0)
                    {
                        string updaterId = "",
                            updateCode = "DEPT_ADD";
                        if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                        else updaterId = (string)Session["Admin"];
                        List<string> values = new List<string>();
                        values.Add(DivIdBox.Text.Trim().ToUpper());
                        values.Add(HospitalClass.PascalCasing(DivNameBox.Text.Trim()));
                        values.Add("");
                        values.Add(DivDescBox.Text);
                        values.Add(HospitalClass.getTransactionId());
                        values.Add(updateCode);
                        values.Add(updaterId);
                        values.Add("insert");
                        int status = DataConsumer.executeProcedure("dept_proc", values);
                        DivStatusLabel.CssClass = "success normal";
                        DivStatusLabel.Text = "Successful ward addition. <br/>Department ID: " + DivIdBox.Text.Trim().ToUpper() +
                            "<br/>Department Name:" + HospitalClass.PascalCasing(DivNameBox.Text.Trim()) + ".";
                        StatusLabel.CssClass = "success paraNormal";
                        StatusLabel.Text = "Done";
                        SortButton_Click(new object(), new EventArgs());
                    }
                    else
                    {
                        StatusLabel.CssClass = "error paraNormal";
                        StatusLabel.Text = "User input error below";
                        DivStatusLabel.CssClass = "error normal";
                        if (dt.Rows[0][0].ToString() == DivIdBox.Text.Trim().ToUpper()) DivStatusLabel.Text = "Department ID already exists";
                        else DivStatusLabel.Text = "Department name already exists";
                    }
                }
                else
                {
                    StatusLabel.CssClass = "error paraNormal";
                    StatusLabel.Text = "User input error below";
                    DivStatusLabel.CssClass = "error normal";
                    if (DivIdBox.Text.Trim().Length < 1) DivStatusLabel.Text = "Enter a valid department id";
                    else if (DivNameBox.Text.Length < 2) DivStatusLabel.Text = "Enter a valid department name";
                    else DivStatusLabel.Text = "Enter a valid department description";
                }
                ListView1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                StatusLabel.CssClass = "error paraNormal";
                StatusLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void DivModifyButton_Click(object sender, EventArgs e)
        {
            try
            {
                addDelDiv.Visible = true;
                //check for changes
                bool check = DivNameBox.Text.ToUpper() == ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptNameLabel")).Text.ToUpper();
                check = check && DivIdBox.Text.ToUpper() == ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptIdLabel")).Text.ToUpper();
                check = check && DivDescBox.Text == ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptDescLabel")).Text;
                //check for existence
                string checkDeptIdName = DataProvider.Departments.deptIdName(DivIdBox.Text.Trim().ToUpper(), HospitalClass.PascalCasing(DivNameBox.Text.Trim()));
                DataTable dt = HospitalClass.getDataTable(checkDeptIdName);
                //check fo acceptance of change
                bool check2 = DivNameBox.Text.ToUpper() != ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptNameLabel")).Text.ToUpper();
                check2 = check2 && DivIdBox.Text.ToUpper() != ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptIdLabel")).Text.ToUpper();
                if (DivNameBox.Text.Trim().Length >= 2 && DivDescBox.Text.Length >= 3 && DivIdBox.Text.Trim().Length >= 1 && !check &&
                    ((check2 && dt.Rows.Count == 0) || (!check2 && dt.Rows.Count == 1)))
                {
                    string updaterId = "",
                        updateCode = "DEPT_UPD";
                    if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                    else updaterId = (string)Session["Admin"];
                    List<string> values = new List<string>();
                    values.Add(DivIdBox.Text.Trim().ToUpper());
                    values.Add(HospitalClass.PascalCasing(DivNameBox.Text.Trim()));
                    values.Add(((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DeptNameLabel")).Text);
                    values.Add(DivDescBox.Text);
                    values.Add(HospitalClass.getTransactionId());
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add("update");
                    int status = DataConsumer.executeProcedure("dept_proc", values);
                    StatusLabel.CssClass = "success normal";
                    StatusLabel.Text = "Successful ward modification.<br/>Department ID: " + DivIdBox.Text.Trim().ToUpper() +
                        "<br/>Department Name:" + HospitalClass.PascalCasing(DivNameBox.Text.Trim()) + ".";
                    addDelDiv.Visible = false;
                    SortButton_Click(new object(), new EventArgs());
                    ListView1.SelectedIndex = -1;
                }
                else
                {
                    StatusLabel.CssClass = "error paraNormal";
                    StatusLabel.Text = "User input error below";
                    DivStatusLabel.CssClass = "error normal";
                    if (DivNameBox.Text.Trim().Length < 2) DivStatusLabel.Text = "Enter a valid department name";
                    else if (DivDescBox.Text.Length < 3) DivStatusLabel.Text = "Enter a valid department description";
                    else if (DivIdBox.Text.Trim().Length < 1) DivStatusLabel.Text = "Enter a department ID";
                    else if (check) DivStatusLabel.Text = "No change made";
                    else DivStatusLabel.Text = "Department ID/name already exists for another department";
                }
            }
            catch (Exception ex)
            {
                StatusLabel.CssClass = "error paraNormal";
                StatusLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
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
                if (DescRadioButton.Checked) selectOrder = "dept_name desc";
                else selectOrder = "dept_name asc";
            }
            if (!AlphabeticRadioButton.Checked && !DateRadioButton.Checked) BindListView();
            else BindListView(selectOrder);
            addDelDiv.Visible = false;
        }

        protected void BindListView(string selectOrder = "dept_name")
        {
            SqlDataSource1.ConnectionString = HospitalClass.getConnectionString().Substring(0, 60);
            SqlDataSource1.ProviderName = "System.Data.OracleClient";
            SqlDataSource1.SelectCommand = DataProvider.Departments.fillListView(selectOrder);
            ListView1.DataSourceID = "SqlDataSource1";
            ListView1.DataBind();
        }

        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            addDelDiv.Visible = false;
        }

        //to handle change in paging during execution
        protected void ListView1_PagePropertiesChanged(object sender, EventArgs e)
        {
            addDelDiv.Visible = false;
            ListView1.SelectedIndex = -1;
        }

    }
}
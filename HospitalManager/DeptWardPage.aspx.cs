using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class DeptWardPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["SuperUser"] == null && Session["Admin"] == null)
                {
                    Session["Info"] = "You do not have access to the requested page";
                    Response.Redirect("LoggedinPage.aspx");
                }
                if (!IsPostBack) populateLists();
                DeptDelDescLabel.Text = "Description: " + DeptDelList.SelectedItem.Value;
                WardDelDescLabel.Text = "Name of ward to be deleted: " + WardDelList.SelectedItem.Value;
            }
            catch (Exception ex)
            {
                DeptAddLabel.CssClass = "error";
                DeptAddLabel.Text = "Error: " + ex.Message; 
                HospitalClass.Log(ex);
            }
        }

        protected void DeptAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                DeptAddLabel.CssClass = "error";
                string checkQuery = DataProvider.DeptWardPage.verifyDeptAdd(DeptAddNameBox.Text);
                DataTable dt = HospitalClass.getDataTable(checkQuery);
                if (DeptAddNameBox.Text.Trim().Length > 2 && DeptAddDescBox.Text.Trim().Length > 3 && dt.Rows.Count == 0)
                {
                    string updateCode = "DEPT_ADD";
                    string updaterId = "";
                    if (Session["SuperUser"] != null) updaterId = Session["SuperUser"].ToString();
                    else updaterId = Session["Admin"].ToString();
                    List<string> values = new List<string>();
                    values.Add(DeptAddNameBox.Text.Trim());
                    values.Add(DeptAddDescBox.Text);
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add("insert");
                    int status = DataConsumer.executeProcedure("add_del_dept", values);
                    DeptAddLabel.CssClass = "success";
                    DeptAddLabel.Text = "Operation Successful.<br/>Department added: " + DeptAddNameBox.Text.Trim();
                    populateLists();
                }
                else
                {
                    if (dt.Rows.Count != 0) DeptAddLabel.Text = "This department exists";
                    else if (DeptAddNameBox.Text.Trim().Length <= 2) DeptAddLabel.Text = "Enter a valid department name";
                    else DeptAddLabel.Text = "Enter a valid department description";
                }
            }
            catch (Exception ex)
            {
                DeptAddLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void WardAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                WardAddLabel.CssClass = "error";
                string checkQuery = DataProvider.DeptWardPage.verifyWardAdd(WardAddNameBox.Text);
                DataTable dt = HospitalClass.getDataTable(checkQuery);
                if (WardAddNameBox.Text.Trim().Length > 2 && dt.Rows.Count == 0)
                {
                    string updateCode = "WARD_ADD";
                    string updaterId = "";
                    if (Session["SuperUser"] != null) updaterId = Session["SuperUser"].ToString();
                    else updaterId = Session["Admin"].ToString();
                    List<string> values = new List<string>();
                    values.Add(WardAddNameBox.Text.Trim());
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add("insert");
                    int status = DataConsumer.executeProcedure("add_del_ward", values);
                    WardAddLabel.CssClass = "success";
                    WardAddLabel.Text = "Operation Successful.<br/>Department added: " + WardAddNameBox.Text.Trim();
                    populateLists();
                }
                else
                {
                    if (dt.Rows.Count != 0) WardAddLabel.Text = "This ward exists";
                    else WardAddLabel.Text = "Enter a valid ward name";
                }
            }
            catch (Exception ex)
            {
                WardAddLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void DeptDelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeptDelList.SelectedIndex != 0 && DeptDelCheckBox.Checked)
                {
                    string updateCode = "DEPT_DEL";
                    string updaterId = "";
                    if (Session["SuperUser"] != null) updaterId = Session["SuperUser"].ToString();
                    else updaterId = Session["Admin"].ToString();
                    List<string> values = new List<string>();
                    values.Add(DeptDelList.SelectedItem.Text);
                    values.Add("X");
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add("delete");
                    int status = DataConsumer.executeProcedure("add_del_dept", values);
                    DeptDelLabel.CssClass = "success";
                    DeptDelLabel.Text = "Operation Successful.<br/>Department deleted: " + DeptDelList.SelectedItem.Text;
                    populateLists();
                }
                else
                {
                    DeptDelLabel.CssClass = "error";
                    if (DeptDelList.SelectedIndex == 0) DeptDelLabel.Text = "Please select a department to delete";
                    else DeptDelLabel.Text = "Please tick the checkbox to enforce deletion";
                }
            }
            catch (Exception ex)
            {
                DeptDelLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void WardDelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (WardDelList.SelectedIndex != 0 && WardDelCheckBox.Checked)
                {
                    string updateCode = "WARD_DEL";
                    string updaterId = "";
                    if (Session["SuperUser"] != null) updaterId = Session["SuperUser"].ToString();
                    else updaterId = Session["Admin"].ToString();
                    List<string> values = new List<string>();
                    values.Add(WardDelList.SelectedItem.Text);
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add("delete");
                    int status = DataConsumer.executeProcedure("add_del_ward", values);
                    WardDelLabel.CssClass = "success";
                    WardDelLabel.Text = "Operation Successful.<br/>Ward deleted: " + WardDelList.SelectedItem.Text;
                    populateLists();
                }
                else
                {
                    WardDelLabel.CssClass = "error";
                    if (WardDelList.SelectedIndex == 0) WardDelLabel.Text = "Please select a ward to delete";
                    else WardDelLabel.Text = "Please tick the checkbox to enforce deletion";
                }
            }
            catch (Exception ex)
            {
                WardDelLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void populateLists()
        {
            try
            {
                string deptListQuery = DataProvider.DeptWardPage.fillDeptList();
                DataTable deptDt = HospitalClass.getDataTable(deptListQuery);
                DeptDelList.DataSource = deptDt;
                DeptDelList.DataTextField = "DEPT_NAME";
                DeptDelList.DataValueField = "DESCRIPTION";
                DeptDelList.DataBind();
                DeptDelList.Items.Insert(0, new ListItem("Please select...", "None Selected"));
                string wardListQuery = "select ward_name from tobehospital.wards where no_of_rooms='0' and parent_dept='None'";
                DataTable wardDt = HospitalClass.getDataTable(wardListQuery);
                WardDelList.DataSource = wardDt;
                WardDelList.DataTextField = "WARD_NAME";
                WardDelList.DataValueField = "WARD_NAME";
                WardDelList.DataBind();
                WardDelList.Items.Insert(0, new ListItem("Please select...", "None"));
                DeptDelDescLabel.Text = "Description: None Selected";
                WardDelDescLabel.Text = "Name of ward to be deleted: none";
            }
            catch { throw; }
        }
    }
}
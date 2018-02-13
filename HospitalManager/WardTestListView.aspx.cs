using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class WardTestListView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        //    if (Session["SuperUser"] == null && Session["Admin"] == null)
        //    {
        //        Session["Info"] = "You do not have access to the requested page";
        //        Response.Redirect("LoggedinPage.aspx");
        //    }
            SortButton_Click(new object(), new EventArgs());
            StatusLabel.Text = "";
            DivStatusLabel.Text = "";
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            ListView1.SelectedIndex = -1;
            DivHeaderLabel.Text = "Add Wards";
            DivAddButton.Visible = true;
            DivModifyButton.Visible = false;
            DivNameBox.Text = "";
            DivDescBox.Text = "";
            DivIdBox.Text = "";
            SortButton_Click(new object(), new EventArgs());
            addDelDiv.Visible = true;
            StatusLabel.CssClass = "addColor paraNormal";
            StatusLabel.Text = "Fill the required fields below and enforce addition below";
        }

        protected void ModifyButton_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndex >= 0)
            {
                addDelDiv.Visible = true;
                DivHeaderLabel.Text = "Modify/Edit Wards";
                DivModifyButton.Visible = true;
                DivAddButton.Visible = false;
                DivNameBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardNameLabel")).Text;
                DivDescBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardDescLabel")).Text;
                DivIdBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardIdLabel")).Text;
                StatusLabel.CssClass = "addColor paraNormal";
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
            SortButton_Click(new object(), new EventArgs());
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                addDelDiv.Visible = false;
                if (ListView1.SelectedIndex >= 0)
                {
                    string wardName = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardNameLabel")).Text;
                    string wardNoQuery = DataProvider.Wards.roomDeptAvail(wardName);  //check for room linkage
                    DataTable dt = HospitalClass.getDataTable(wardNoQuery);
                    if (dt.Rows[0][0].ToString() == "0" && ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardDeptLabel")).Text == "None")
                    {
                        string updaterId = "",
                            updateCode = "WARD_DEL";
                        if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                        else updaterId = (string)Session["Admin"];
                        List<string> values = new List<string>();
                        values.Add("");
                        values.Add(wardName);
                        values.Add("");
                        values.Add("");
                        values.Add(HospitalClass.getTransactionId());
                        values.Add(updateCode);
                        values.Add(updaterId);
                        values.Add("delete");
                        int status = DataConsumer.executeProcedure("ward_proc", values);
                        StatusLabel.CssClass = "success normal";
                        StatusLabel.Text = "Successful ward deletion.<br>Ward ID: " + ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardIdLabel")).Text +
                            "<br/>Ward Name: " + wardName + ".";
                        ListView1.SelectedIndex = -1;
                        SortButton_Click(new object(), new EventArgs());
                    }
                    else
                    {
                        StatusLabel.CssClass = "error normal";
                        if (dt.Rows[0][0].ToString() != "0")
                        { StatusLabel.Text = "This ward is already linked to room(s).<br/>Disconnect/Unrelate before deleting."; }
                        else StatusLabel.Text = "This ward is linked to a parent department.<br/>Disconnect/Unrelate before deleting";
                    }
                }
                else
                {
                    StatusLabel.CssClass = "error paraNormal";
                    if (ListView1.SelectedIndex < 0) StatusLabel.Text = "No item selected for deletion";
                }
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
                    string checkWardIdName = DataProvider.Wards.wardIdName(DivIdBox.Text.Trim().ToUpper(), HospitalClass.PascalCasing(DivNameBox.Text.Trim()));
                    DataTable dt = HospitalClass.getDataTable(checkWardIdName);
                    if (dt.Rows.Count == 0)
                    {
                        string updaterId = "",
                            updateCode = "WARD_ADD";
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
                        int status = DataConsumer.executeProcedure("ward_proc", values);
                        DivStatusLabel.CssClass = "success normal";
                        DivStatusLabel.Text = "Successful ward addition. <br/>Ward ID: " + DivIdBox.Text.Trim().ToUpper() +
                            "<br/>Ward Name:" + HospitalClass.PascalCasing(DivNameBox.Text.Trim()) + ".";
                        StatusLabel.CssClass = "success paraNormal";
                        StatusLabel.Text = "Done";
                        ListView1.SelectedIndex = -1;
                        SortButton_Click(new object(), new EventArgs());
                    }
                    else
                    {
                        StatusLabel.CssClass = "error paraNormal";
                        StatusLabel.Text = "User input error below";
                        DivStatusLabel.CssClass = "error normal";
                        if (dt.Rows[0][0].ToString() == DivIdBox.Text.Trim().ToUpper()) DivStatusLabel.Text = "Ward ID already exists";
                        else DivStatusLabel.Text = "Ward name already exists";
                    }
                }
                else
                {
                    StatusLabel.CssClass = "error paraNormal";
                    StatusLabel.Text = "User input error below";
                    DivStatusLabel.CssClass = "error normal";
                    if (DivIdBox.Text.Trim().Length < 1) DivStatusLabel.Text = "Enter a valid ward id";
                    else if (DivNameBox.Text.Length < 2) DivStatusLabel.Text = "Enter a valid ward name";
                    else DivStatusLabel.Text = "Enter a valid ward description";
                }
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
                //check for change in values
                bool check = DivNameBox.Text.ToUpper() == ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardNameLabel")).Text.ToUpper();
                check = check && DivIdBox.Text.ToUpper() == ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardIdLabel")).Text.ToUpper();
                check = check && DivDescBox.Text == ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardDescLabel")).Text;
                //check for existence
                string checkWardIdName = DataProvider.Wards.wardIdName(DivIdBox.Text.Trim().ToUpper(), HospitalClass.PascalCasing(DivNameBox.Text.Trim()));
                DataTable dt = HospitalClass.getDataTable(checkWardIdName);
                //check for acceptance of change
                bool check2 = DivNameBox.Text.ToUpper() != ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardNameLabel")).Text.ToUpper();
                check2 = check2 && DivIdBox.Text.ToUpper() != ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardIdLabel")).Text.ToUpper();
                if (DivNameBox.Text.Trim().Length >= 2 && DivDescBox.Text.Length >= 3 && DivIdBox.Text.Trim().Length >= 1 && !check &&
                    ((check2 && dt.Rows.Count == 0) || (!check2 && dt.Rows.Count == 1)))
                {
                    string updaterId = "",
                        updateCode = "WARD_UPD";
                    if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                    else updaterId = (string)Session["Admin"];
                    List<string> values = new List<string>();
                    values.Add(DivIdBox.Text.Trim().ToUpper());
                    values.Add(HospitalClass.PascalCasing(DivNameBox.Text.Trim()));
                    values.Add(((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WardNameLabel")).Text);
                    values.Add(DivDescBox.Text);
                    values.Add(HospitalClass.getTransactionId());
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add("update");
                    int status = DataConsumer.executeProcedure("ward_proc", values);
                    StatusLabel.CssClass = "success normal";
                    StatusLabel.Text = "Successful ward modification.<br/> Ward ID: " + DivIdBox.Text.Trim().ToUpper() +
                        "<br/>Ward Name:" + HospitalClass.PascalCasing(DivNameBox.Text.Trim()) + ".";
                    addDelDiv.Visible = false;
                    ListView1.SelectedIndex = -1;
                    SortButton_Click(new object(), new EventArgs());
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
                    else DivStatusLabel.Text = "Ward ID/name already exists for another ward";
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
                if (DescRadioButton.Checked) selectOrder = "ward_name desc";
                else selectOrder = "ward_name asc";
            }
            if (!AlphabeticRadioButton.Checked && !DateRadioButton.Checked) BindListView();
            else BindListView(selectOrder);
            addDelDiv.Visible = false;
        }

        protected void BindListView(string selectOrder = "LPAD(ward_name,64)")
        {
            string query = DataProvider.Wards.fillListView(selectOrder);
            DataTable dt = HospitalClass.getDataTable(query);
            BindListView(dt);
        }

        private void BindListView(DataTable dt)
        {
            ViewState["ListViewTable"] = dt;
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }

        //to handle change in paging during execution
        protected void ListView1_PagePropertiesChanged(object sender, EventArgs e)
        {
            addDelDiv.Visible = false;
            ListView1.SelectedIndex = -1;
            BindListView((DataTable)ViewState["ListViewTable"]);
        }

        protected void ListView1_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListView1.SelectedIndex = e.NewSelectedIndex;
            BindListView((DataTable)ViewState["ListViewTable"]);
            addDelDiv.Visible = false;
        }

    }
}
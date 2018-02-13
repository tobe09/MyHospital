using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace HospitalManager
{
    public partial class DoctorsHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null && Session["User"] == null) Response.Redirect("~/Login.aspx");
            if (Session["User"].ToString().StartsWith("PAT") && Session["SuperUser"] == null && Session["Admin"] == null)
            {
                Session["Info"] = "You do not have access to the requested page";
                Response.Redirect("~/LoggedInPage.aspx");
            }
            if (!IsPostBack)
            {
                DivUserIdBox.Text = Session["User"].ToString();  //for addition of user work history information
                if (Session["Quote"] != null) TempLabel.Text = Session["Quote"].ToString();  //set according to previous page
                if (TempLabel.Text == "AdminStf") RoleList.SelectedIndex = 1;
            }
            if ((Session["SuperUser"] == null && Session["Admin"] == null && (Session["User"].ToString().StartsWith("ST") || 
                Session["User"].ToString().StartsWith("DC"))) || TempLabel.Text == "Employee's History")
            {
                if (Session["User"].ToString().StartsWith("ST")) RoleList.SelectedIndex = 1;
                SearchDiv.Visible = true;
                MainDiv.Visible = false;
                SearchTopDiv.Visible = false;
                CloseButton.Visible = false;
                SearchBox.Text = Session["User"].ToString();
                BindSearchListView();
                SearchListView.SelectedItemTemplate = SearchListView.ItemTemplate;  //flunctuates (due to selectedIndexChanged event handler)
                if (SearchListView.Items.Count > 0) ((Button)(SearchListView.FindControl("SearchDeleteButton"))).Visible = false;
                SearchStatusLabel.CssClass = "paraNormal";
                SearchStatusLabel.Text = "<br/>To add, update or remove entries, meet the system administrator.";
            }
            else BindListView(); //only true for admin/superusers redirecting from their section
            if (SearchDiv.Visible && !(Session["SuperUser"] == null && Session["Admin"] == null)) BindSearchListView();  //to avoid binding twice
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            ListView1.SelectedIndex = -1;
            SearchListView.SelectedIndex = -1;
            DivUserIdBox.Text = Session["User"].ToString();
            DivWorkPlaceBox.Text = "";
            DivWorkTypeBox.Text = "";
            DivPositionBox.Text = "";
            DivStartDateBox.Text = "";
            DivEndDateBox.Text = "";
            addDelDiv.Visible = true;
            ModifyDivButton.Visible = false;
            DivAddButton.Visible = true;
            DivHeaderLabel.Text = "Add work history information";
            StatusLabel.CssClass = "addColor paraNormal";
            StatusLabel.Text = "Fill the required fields and enforce addition below";
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            addDelDiv.Visible = false;
            ListView1.SelectedIndex = -1;
        }

        protected void DivAddButton_Click(object sender, EventArgs e)
        {
            string check = "";
            if (RoleList.SelectedIndex == 0) check =
                   DataProvider.DoctorStaffHistory.checkExistDoc(DivUserIdBox.Text, DivWorkPlaceBox.Text.Trim(), DivPositionBox.Text.Trim());
            else check = DataProvider.DoctorStaffHistory.checkExistStf(DivUserIdBox.Text, DivWorkPlaceBox.Text.Trim(), DivPositionBox.Text.Trim());
            System.Data.DataTable dt = HospitalClass.getDataTable(check);
            bool validateDoc = DivUserIdBox.Text.StartsWith("DC") && RoleList.SelectedIndex == 0;
            bool validateStf = DivUserIdBox.Text.StartsWith("ST") && RoleList.SelectedIndex == 1;
            string dateRegex = @"^(?:0[1-9]|[12]\d|3[01])([\/.-])(?:0[1-9]|1[012])\1(?:19|20)\d\d$";
            if (DivWorkPlaceBox.Text.Trim().Length > 1 && DivWorkTypeBox.Text.Trim().Length > 1 && DivPositionBox.Text.Trim().Length > 1 && dt.Rows.Count == 0 &&
               Regex.IsMatch(DivStartDateBox.Text.Trim(), dateRegex) && Regex.IsMatch(DivEndDateBox.Text.Trim(), dateRegex) && (validateDoc || validateStf))
            {
                string updaterId = "",
                         updateCode = "";
                if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                else updaterId = (string)Session["Admin"];
                if (RoleList.SelectedIndex == 0) updateCode = "DOC_WKAD";
                else updateCode = "STF_WKAD";
                List<string> values = new List<string>();
                values.Add(DivWorkPlaceBox.Text.Trim());
                values.Add(DivWorkTypeBox.Text.Trim());
                values.Add(DivPositionBox.Text.Trim());
                values.Add(DivStartDateBox.Text.Trim());
                values.Add(DivEndDateBox.Text.Trim());
                values.Add(HospitalClass.getTransactionId());
                values.Add(updateCode);
                values.Add(updaterId);
                values.Add(DivUserIdBox.Text);
                if (RoleList.SelectedIndex == 0) values.Add("doc insert");
                else values.Add("stf insert");
                int status = DataConsumer.executeProcedure("docstf_wk_proc", values);
                DivStatusLabel.CssClass = "success normal";
                DivStatusLabel.Text = "Successfully added.<br>Work place: " + DivWorkPlaceBox.Text.Trim() + ".";
                StatusLabel.CssClass = "success paraNormal";
                StatusLabel.Text = "Done";
                BindListView();
                BindSearchListView();
            }
            else
            {
                StatusLabel.CssClass = "error paraNormal";
                StatusLabel.Text = "User input error below";
                string form = "(Format: 25/12/2000 or 25-12-2000).";
                DivStatusLabel.CssClass = "error paraNormal";
                if (!(validateDoc || validateStf))
                {
                    if (RoleList.SelectedIndex == 0) DivStatusLabel.Text = "Invalid user. User must be a doctor";
                    else DivStatusLabel.Text = "Invalid user. User must be a staff";
                }
                else if (dt.Rows.Count > 0) DivStatusLabel.Text = "This entry already exists";
                else if (DivWorkPlaceBox.Text.Trim().Length <= 1) DivStatusLabel.Text = "Please enter a valid work place";
                else if (DivWorkTypeBox.Text.Trim().Length <= 1) DivStatusLabel.Text = "Please enter a valid work type";
                else if (DivPositionBox.Text.Trim().Length <= 1) DivStatusLabel.Text = "Please enter a valid position";
                else if (!Regex.IsMatch(DivStartDateBox.Text.Trim(), dateRegex)) DivStatusLabel.Text = "Invalid start date. " + form;
                else DivStatusLabel.Text = "Invalid end date. " + form;
            }
            ListView1.SelectedIndex = -1;
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            BindSearchListView();
        }

        protected void BindListView()
        {
            SqlDataSource1.ConnectionString = HospitalClass.getConnectionString().Substring(0, 60);
            SqlDataSource1.ProviderName = "System.Data.OracleClient";
            if (RoleList.SelectedIndex == 0)
            {
                SqlDataSource1.SelectCommand = DataProvider.DoctorStaffHistory.fillListViewDoc();
                NameLabel.Text = "Work History (All Doctors)";
                HeadLabel.Text = "Doctor's Employment History";
            }
            else
            {
                SqlDataSource1.SelectCommand = DataProvider.DoctorStaffHistory.fillListViewStf();
                NameLabel.Text = "Work History (All Staff)";
                HeadLabel.Text = "Staff's Employment History";
            }
            ListView1.DataSourceID = "SqlDataSource1";
            ListView1.DataBind();
        }

        public void BindSearchListView()
        {
            SearchDiv.Visible = true;
            SearchUserIdLabel.Text = "";
            SearchNameLabel.Text = "";
            SearchStatusLabel.CssClass = "error";
            if (SearchBox.Text.Trim().Length == 11 && ((SearchBox.Text.Trim().ToUpper().StartsWith("DC") && RoleList.SelectedIndex == 0) ||
                SearchBox.Text.Trim().ToUpper().StartsWith("ST") && RoleList.SelectedIndex == 1))
            {
                SearchListView.Visible = true;
                SqlDataSource2.ConnectionString = HospitalClass.getConnectionString().Substring(0, 60);
                SqlDataSource2.ProviderName = "System.Data.OracleClient";
                if (RoleList.SelectedIndex == 0) SqlDataSource2.SelectCommand = DataProvider.DoctorStaffHistory.fillListViewDoc(SearchBox.Text.Trim().ToUpper());
                else SqlDataSource2.SelectCommand = DataProvider.DoctorStaffHistory.fillListViewStf(SearchBox.Text.Trim().ToUpper());
                SearchListView.DataSourceID = "SqlDataSource2";
                SearchListView.DataBind();
                if (SearchListView.Items.Count == 0) SearchStatusLabel.Text = "Not found";
                else completeAction();
            }
            else
            {
                SearchListView.Visible = false;
                if (SearchBox.Text.Trim().Length == 0) SearchStatusLabel.Text = "No value entered";
                else if (SearchBox.Text.Trim().ToUpper().StartsWith("DC")) SearchStatusLabel.Text = "Invalid staff id";
                else if (SearchBox.Text.Trim().ToUpper().StartsWith("ST")) SearchStatusLabel.Text = "Invalid doctor id";
                else SearchStatusLabel.Text = "Invalid employee Id";
            }
        }

        public void completeAction()
        {
            System.Data.DataTable nameDt;
            if(RoleList.SelectedIndex==0) nameDt = HospitalClass.getDataTable(DataProvider.DoctorStaffHistory.getNameDoc(SearchBox.Text.Trim().ToUpper()));
            else nameDt = HospitalClass.getDataTable(DataProvider.DoctorStaffHistory.getNameStf(SearchBox.Text.Trim().ToUpper()));
            SearchNameLabel.Text = "Name: " + HospitalClass.PascalCasing(nameDt.Rows[0][0].ToString()) + ", " +
                HospitalClass.PascalCasing(nameDt.Rows[0][1].ToString());
            if (!DBNull.Value.Equals(nameDt.Rows[0][2])) SearchNameLabel.Text += " " + HospitalClass.PascalCasing(nameDt.Rows[0][2].ToString());
            SearchNameLabel.Text += "<br/>User Id: ";
            SearchUserIdLabel.Text = SearchBox.Text.Trim().ToUpper();
            SearchStatusLabel.CssClass = "success";
            SearchStatusLabel.Text = "Found";
        }

        protected void CloseButton_Click(object sender, EventArgs e)
        {
            if (SearchListView.SelectedIndex >= 0) addDelDiv.Visible = false;
            SearchListView.SelectedIndex = -1;
            SearchDiv.Visible = false;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndex >= 0)
            {
                string updaterId = "",
                         updateCode = "";
                if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                else updaterId = (string)Session["Admin"];
                if (RoleList.SelectedIndex == 0) updateCode = "DOC_WKDL";
                else updateCode = "STF_WKDL";
                List<string> values = new List<string>();
                values.Add(((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WorkPlaceLabel")).Text);
                values.Add("");
                values.Add(((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("PositionLabel")).Text);
                values.Add("");
                values.Add("");
                values.Add(HospitalClass.getTransactionId());
                values.Add(updateCode);
                values.Add(updaterId);
                values.Add(((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("UserIdLabel")).Text);
                if (RoleList.SelectedIndex == 0) values.Add("doc delete");
                else values.Add("stf delete");
                int status = DataConsumer.executeProcedure("docstf_wk_proc", values);
                StatusLabel.CssClass = "success paraNormal";
                StatusLabel.Text = "Successfully deleted";
                BindListView();
                BindSearchListView();
            }
            else
            {
                StatusLabel.CssClass = "error paraNormal";
                StatusLabel.Text = "No item selected for deletion";
            }
            ListView1.SelectedIndex = -1;
        }

        protected void SearchDeleteButton_Click(object sender, EventArgs e)
        {
            if (SearchListView.SelectedIndex >= 0)
            {
                string updaterId = "",
                         updateCode = "DISB_DEL";
                if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                else updaterId = (string)Session["Admin"]; if (RoleList.SelectedIndex == 0) updateCode = "DOC_WKDL";
                else updateCode = "STF_WKDL";
                List<string> values = new List<string>();
                values.Add(((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("WorkPlaceLabel")).Text);
                values.Add("");
                values.Add(((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("PositionLabel")).Text);
                values.Add("");
                values.Add("");
                values.Add(HospitalClass.getTransactionId());
                values.Add(updateCode);
                values.Add(updaterId);
                values.Add(SearchUserIdLabel.Text);
                if (RoleList.SelectedIndex == 0) values.Add("doc delete");
                else values.Add("stf delete");
                int status = DataConsumer.executeProcedure("docstf_wk_proc", values);
                BindSearchListView();
                BindListView();
                SearchStatusLabel.CssClass = "success normal";
                SearchStatusLabel.Text = "Successfully deleted";
            }
            else
            {
                SearchStatusLabel.CssClass = "error normal";
                SearchStatusLabel.Text = "No item selected for deletion";
            }
            SearchListView.SelectedIndex = -1;
        }

        protected void RoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            addDelDiv.Visible = false;
            SearchDiv.Visible = false;
        }

        protected void ModifyButton_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndex >= 0)
            {
                HoldLabel.Text = "";
                DivUserIdBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("UserIdLabel")).Text;
                DivWorkPlaceBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WorkPlaceLabel")).Text;
                DivWorkTypeBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WorkTypeLabel")).Text;
                DivPositionBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("PositionLabel")).Text;
                DivStartDateBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DateStartLabel")).Text;
                DivEndDateBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DateEndLabel")).Text;
                ModifyClear();
            }
            else
            {
                StatusLabel.CssClass = "error paraNormal";
                StatusLabel.Text = "No item selected for modification";
                ListView1.SelectedIndex = -1;
                addDelDiv.Visible = false;
            }
        }

        protected void SearchModifyButton_Click(object sender, EventArgs e)
        {
            if (SearchListView.SelectedIndex >= 0)
            {
                HoldLabel.Text = "Search";
                DivUserIdBox.Text = SearchUserIdLabel.Text;
                DivWorkPlaceBox.Text = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("WorkPlaceLabel")).Text;
                DivWorkTypeBox.Text = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("WorkTypeLabel")).Text;
                DivPositionBox.Text = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("PositionLabel")).Text;
                DivStartDateBox.Text = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("DateStartLabel")).Text;
                DivEndDateBox.Text = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("DateEndLabel")).Text;
                ModifyClear();
            }
            else
            {
                SearchStatusLabel.CssClass = "error paraNormal";
                SearchStatusLabel.Text = "No item selected for modification";
                SearchListView.SelectedIndex = -1;
                addDelDiv.Visible = false;
            }
        }

        protected void ModifyClear()
        {
            addDelDiv.Visible = true;
            ModifyDivButton.Visible = true;
            DivAddButton.Visible = false;
            DivHeaderLabel.Text = "Modify work history information";
            StatusLabel.CssClass = "addColor paraNormal";
            StatusLabel.Text = "Modify the required fields and enforce modification below";
        }

        protected void ModifyDivButton_Click(object sender, EventArgs e)
        {
            string createId = "",
                   workPlace = "";
            string dateRegex = @"^(?:0[1-9]|[12]\d|3[01])([\/.-])(?:0[1-9]|1[012])\1(?:19|20)\d\d$";
            bool check = true;
            if (HoldLabel.Text == "Search")
            {
                check &= ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("WorkPlaceLabel")).Text == DivWorkPlaceBox.Text;
                check &= ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("WorkTypeLabel")).Text == DivWorkTypeBox.Text;
                check &= ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("PositionLabel")).Text == DivPositionBox.Text;
                check &= ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("DateStartLabel")).Text == DivStartDateBox.Text;
                check &= ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("DateEndLabel")).Text == DivEndDateBox.Text;
                createId = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("IdLabel")).Text;
                workPlace = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("WorkPlaceLabel")).Text;
            }
            else
            {
                check &= ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WorkPlaceLabel")).Text == DivWorkPlaceBox.Text;
                check &= ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WorkTypeLabel")).Text == DivWorkTypeBox.Text;
                check &= ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("PositionLabel")).Text == DivPositionBox.Text;
                check &= ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DateStartLabel")).Text == DivStartDateBox.Text;
                check &= ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DateEndLabel")).Text == DivEndDateBox.Text;
                createId = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("IdLabel")).Text;
                workPlace = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("WorkPlaceLabel")).Text;
            }
            if (!check && Regex.IsMatch(DivStartDateBox.Text.Trim(), dateRegex) && Regex.IsMatch(DivEndDateBox.Text.Trim(), dateRegex))
            {
                string updaterId = "",
                         updateCode = "";
                if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                else updaterId = (string)Session["Admin"];
                if (RoleList.SelectedIndex == 0) updateCode = "DOC_WKUD";
                else updateCode = "STF_WKUD";
                List<string> values = new List<string>();
                values.Add(DivWorkPlaceBox.Text);
                values.Add(DivWorkTypeBox.Text);
                values.Add(DivPositionBox.Text);
                values.Add(DivStartDateBox.Text);
                values.Add(DivEndDateBox.Text);
                values.Add(HospitalClass.getTransactionId());
                values.Add(updateCode);
                values.Add(updaterId);
                values.Add(createId);
                if (RoleList.SelectedIndex == 0) values.Add("doc update");
                else values.Add("stf update");
                int status = DataConsumer.executeProcedure("docstf_wk_proc", values);
                addDelDiv.Visible = false;
                ListView1.SelectedIndex = -1;
                SearchListView.SelectedIndex = -1;
                StatusLabel.CssClass = "success normal";
                StatusLabel.Text = "Successful update.<br/>User Id: " + DivUserIdBox.Text + "<br/>Work place: " + workPlace;
                BindListView();
                BindSearchListView();
            }
            else
            {
                StatusLabel.CssClass = "error paraNormal";
                StatusLabel.Text = "User input error below";
                string form = "(Format: 25/12/2000 or 25-12-2000).";
                DivStatusLabel.CssClass = "error paraNormal";
                if (check) DivStatusLabel.Text = "No changes made";
                else if (!Regex.IsMatch(DivStartDateBox.Text.Trim(), dateRegex)) DivStatusLabel.Text = "Invalid start date. " + form;
                else DivStatusLabel.Text = "Invalid end date. " + form;
            }
        }

        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            addDelDiv.Visible = false;
            SearchListView.SelectedIndex = -1;
        }

        protected void ListView1_PagePropertiesChanged(object sender, EventArgs e)
        {
            addDelDiv.Visible = false;
            ListView1.SelectedIndex = -1;
        }

        protected void SearchListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            addDelDiv.Visible = false;
            ListView1.SelectedIndex = -1;
        }

    }
}
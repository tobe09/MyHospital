using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManager
{
    public partial class Patient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null && Session["User"] == null) Response.Redirect("~/Login.aspx");
            if ((Session["User"].ToString().StartsWith("DC") || Session["User"].ToString().StartsWith("ST")) &&
               Session["SuperUser"] == null && Session["Admin"] == null)
            {
                Session["Info"] = "You do not have access to the requested page";
                Response.Redirect("~/LoggedInPage.aspx");
            }
            if (!IsPostBack)
            {
                DivUserIdBox.Text = Session["User"].ToString();  //for addition of patient information
                if (Session["Quote"] != null) TempLabel.Text = Session["Quote"].ToString();  //set according to previous page
            }
            if (TempLabel.Text == "Patient's Issues" || (Session["SuperUser"] == null && Session["Admin"] == null))
            {
                if (Session["User"].ToString().StartsWith("PAT"))
                {
                    SearchDiv.Visible = true;
                    MainDiv.Visible = false;
                    SearchTopDiv.Visible = false;
                    CloseButton.Visible = false;
                    SearchBox.Text = Session["User"].ToString();
                    BindSearchListView();
                    SearchListView.SelectedItemTemplate = SearchListView.ItemTemplate;  //flunctuates (due to selectedIndexChanged event handler)
                    if (SearchListView.Items.Count > 0)
                    {
                        ((Button)(SearchListView.FindControl("SearchModifyButton"))).Visible = false;
                        ((Button)(SearchListView.FindControl("SearchDeleteButton"))).Visible = false;
                    }
                    SearchStatusLabel.CssClass = "paraNormal";
                    SearchStatusLabel.Text = "<br/>To add, update or remove entries, meet the system administrator.";
                }
            }
            else BindListView();
            if (SearchDiv.Visible && !(Session["SuperUser"] == null && Session["Admin"] == null)) BindSearchListView();  //to avoid duplicate binding for privileged user
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            ListView1.SelectedIndex = -1;
            SearchListView.SelectedIndex = -1;
            DivHeaderLabel.Text = "Add Disability";
            DivUserIdBox.Text = Session["User"].ToString();
            DivOrganList.SelectedIndex = 0;
            DivDescBox.Text = "";
            DivStatusList.SelectedIndex = 0;
            StatusLabel.CssClass = "addColor paraNormal";
            StatusLabel.Text = "Fill the required fields and enforce addition below";
            addDelDiv.Visible = true;
            ModifyDivButton.Visible = false;
            DivAddButton.Visible = true;
        }

        protected void DivOrganList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DivOrganList.SelectedIndex == 10) OtherOrganBox.Visible = true;
            else
            {
                OtherOrganBox.Visible = false;
                OtherOrganBox.Text = "";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            addDelDiv.Visible = false;
            ListView1.SelectedIndex = -1;
            SearchListView.SelectedIndex = -1;
        }

        protected void DivAddButton_Click(object sender, EventArgs e)
        {
            if (DivOrganList.SelectedIndex != 0 && DivDescBox.Text.Length > 2 && DivStatusList.SelectedIndex != 0 && DivUserIdBox.Text.StartsWith("PAT"))
            {
                string updaterId = "",
                           updateCode = "DISB_ADD";
                if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                else updaterId = (string)Session["Admin"];
                List<string> values = new List<string>();
                values.Add(DivOrganList.SelectedItem.Text);
                values.Add(DivDescBox.Text);
                values.Add(DivStatusList.SelectedItem.Text);
                values.Add(HospitalClass.getTransactionId());
                values.Add(updateCode);
                values.Add(updaterId);
                values.Add(DivUserIdBox.Text);
                values.Add("insert");
                int status = DataConsumer.executeProcedure("pat_disab_proc", values);
                DivStatusLabel.CssClass = "success normal";
                DivStatusLabel.Text = "Successfully added.<br/>Patient Id: " + DivUserIdBox.Text + ". <br/>Affected Organ: " + DivOrganList.SelectedItem.Text;
                StatusLabel.CssClass = "success paraNormal";
                StatusLabel.Text = "Done";
                BindListView();
                BindSearchListView();
            }
            else
            {
                StatusLabel.CssClass = "error paraNormal";
                StatusLabel.Text = "User input error below";
                DivStatusLabel.CssClass = "error paraNormal";
                if (!DivUserIdBox.Text.StartsWith("PAT")) DivStatusLabel.Text = "This user is not a patient. Please login using patient id";
                else if (DivOrganList.SelectedIndex == 0) DivStatusLabel.Text = "Please select an organ";
                else if (DivDescBox.Text.Length <= 2) DivStatusLabel.Text = "Please enter a valid description";
                else DivStatusLabel.Text = "Please select a status";
            }
            ListView1.SelectedIndex = -1;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndex>=0)
            {
                string updaterId = "",
                         updateCode = "DISB_DEL";
                if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                else updaterId = (string)Session["Admin"];
                List<string> values = new List<string>();
                values.Add("");
                values.Add(((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DescLabel")).Text);
                values.Add(((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("OrganLabel")).Text);
                values.Add(HospitalClass.getTransactionId());
                values.Add(updateCode);
                values.Add(updaterId);
                values.Add(((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("UserIdLabel")).Text);
                values.Add("delete");
                int status = DataConsumer.executeProcedure("pat_disab_proc", values);
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

        protected void BindListView()
        {
            SqlDataSource1.ConnectionString = HospitalClass.getConnectionString().Substring(0, 60);
            SqlDataSource1.ProviderName = "System.Data.OracleClient";
            SqlDataSource1.SelectCommand = DataProvider.Patients.fillListView();
            ListView1.DataSourceID = "SqlDataSource1";
            ListView1.DataBind();
        }

        public void BindSearchListView()
        {
            SearchDiv.Visible = true;
            SearchUserIdLabel.Text = "";
            SearchNameLabel.Text = "";
            SearchStatusLabel.CssClass = "error";
            if (SearchBox.Text.Trim().Length == 11 && SearchBox.Text.Trim().ToUpper().StartsWith("PAT"))
            {
                SearchListView.Visible = true;
                SqlDataSource2.ConnectionString = HospitalClass.getConnectionString().Substring(0, 60);
                SqlDataSource2.ProviderName = "System.Data.OracleClient";
                SqlDataSource2.SelectCommand = DataProvider.Patients.fillListView(SearchBox.Text.Trim().ToUpper());
                SearchListView.DataSourceID = "SqlDataSource2";
                SearchListView.DataBind();
                if (SearchListView.Items.Count == 0) SearchStatusLabel.Text = "Not found";
                else completeAction();
            }
            else
            {
                SearchListView.Visible = false;
                if (SearchBox.Text.Trim().Length == 0) SearchStatusLabel.Text = "No value entered";
                else SearchStatusLabel.Text = "Invalid patient id";
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            BindSearchListView();
        }


        public void completeAction()
        {
            System.Data.DataTable nameDt = HospitalClass.getDataTable(DataProvider.Patients.getName(SearchBox.Text.Trim().ToUpper()));
            SearchNameLabel.Text = "Name: " + HospitalClass.PascalCasing(nameDt.Rows[0][0].ToString()) + ", " +
                HospitalClass.PascalCasing(nameDt.Rows[0][1].ToString());
            if (!DBNull.Value.Equals(nameDt.Rows[0][2])) SearchNameLabel.Text += " "+ HospitalClass.PascalCasing(nameDt.Rows[0][2].ToString());
            SearchNameLabel.Text += "<br/>User Id: ";
            SearchUserIdLabel.Text = SearchBox.Text.Trim().ToUpper();
            SearchStatusLabel.CssClass = "success";
            SearchStatusLabel.Text = "Found";
        }

        protected void SearchDeleteButton_Click(object sender, EventArgs e)
        {
            if (SearchListView.SelectedIndex >= 0)
            {
                string updaterId = "",
                         updateCode = "DISB_DEL";
                if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                else updaterId = (string)Session["Admin"];
                List<string> values = new List<string>();
                values.Add("");
                values.Add(((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("SearchDescLabel")).Text);
                values.Add(((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("SearchOrganLabel")).Text);
                values.Add(HospitalClass.getTransactionId());
                values.Add(updateCode);
                values.Add(updaterId);
                values.Add(SearchUserIdLabel.Text);
                values.Add("delete");
                int status = DataConsumer.executeProcedure("pat_disab_proc", values);
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

        protected void CloseButton_Click(object sender, EventArgs e)
        {
            if (SearchListView.SelectedIndex >= 0) addDelDiv.Visible = false;
            SearchListView.SelectedIndex = -1;
            SearchDiv.Visible = false;
        }

        protected void ModifyButton_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndex >= 0)
            {
                HoldLabel.Text = "";
                DivUserIdBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("UserIdLabel")).Text;
                string organ = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("OrganLabel")).Text;
                if (organ == "Ear") DivOrganList.SelectedIndex = 1;
                else if (organ == "Eye") DivOrganList.SelectedIndex = 2;
                else if (organ == "Hand") DivOrganList.SelectedIndex = 3;
                else if (organ == "Heart") DivOrganList.SelectedIndex = 4;
                else if (organ == "Kidney") DivOrganList.SelectedIndex = 5;
                else if (organ == "Leg") DivOrganList.SelectedIndex = 6;
                else if (organ == "Lung") DivOrganList.SelectedIndex = 7;
                else if (organ == "Mouth") DivOrganList.SelectedIndex = 8;
                else if (organ == "Nose") DivOrganList.SelectedIndex = 9;
                else DivOrganList.SelectedIndex = 10;
                DivDescBox.Text = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DescLabel")).Text;
                string status = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("StatusLabel")).Text;
                if (status == "Being Treated") DivStatusList.SelectedIndex = 1;
                else if (status == "Treated") DivStatusList.SelectedIndex = 2;
                else DivStatusList.SelectedIndex = 3;
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
                string organ = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("SearchOrganLabel")).Text;
                if (organ == "Ear") DivOrganList.SelectedIndex = 1;
                else if (organ == "Eye") DivOrganList.SelectedIndex = 2;
                else if (organ == "Hand") DivOrganList.SelectedIndex = 3;
                else if (organ == "Heart") DivOrganList.SelectedIndex = 4;
                else if (organ == "Kidney") DivOrganList.SelectedIndex = 5;
                else if (organ == "Leg") DivOrganList.SelectedIndex = 6;
                else if (organ == "Lung") DivOrganList.SelectedIndex = 7;
                else if (organ == "Mouth") DivOrganList.SelectedIndex = 8;
                else if (organ == "Nose") DivOrganList.SelectedIndex = 9;
                else DivOrganList.SelectedIndex = 10;
                DivDescBox.Text = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("SearchDescLabel")).Text;
                string status = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("SearchStatusLabel")).Text;
                if (status == "Being Treated") DivStatusList.SelectedIndex = 1;
                else if (status == "Treated") DivStatusList.SelectedIndex = 2;
                else DivStatusList.SelectedIndex = 3;
                ModifyClear();
            }
            else
            {
                SearchStatusLabel.CssClass = "error";
                SearchStatusLabel.Text = "No item selected for modification";
                SearchListView.SelectedIndex = -1;
                addDelDiv.Visible = false;
            }
        }

        protected void ModifyClear()
        {
            addDelDiv.Visible = true;
            DivAddButton.Visible = false;
            ModifyDivButton.Visible = true;
            DivHeaderLabel.Text = "Modify Disability";
            StatusLabel.CssClass = "addColor paraNormal";
            StatusLabel.Text = "Modify the required fields and enforce modification below";
        }

        protected void ModifyDivButton_Click(object sender, EventArgs e)
        {
            string createId = "",
                organ = "";
            bool check=true;
            if (HoldLabel.Text == "Search")
            {
                check &= ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("SearchOrganLabel")).Text==DivOrganList.SelectedItem.Text;
                check &= ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("SearchDescLabel")).Text == DivDescBox.Text;
                check &= ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("SearchStatusLabel")).Text == DivStatusList.SelectedItem.Text;
                createId = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("SearchIdLabel")).Text;
                organ = ((Label)SearchListView.Items[SearchListView.SelectedIndex].FindControl("SearchOrganLabel")).Text;
            }
            else
            {
                check &= ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("OrganLabel")).Text == DivOrganList.SelectedItem.Text;
                check &= ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("DescLabel")).Text == DivDescBox.Text;
                check &= ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("StatusLabel")).Text == DivStatusList.SelectedItem.Text;
                createId = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("IdLabel")).Text;
                organ = ((Label)ListView1.Items[ListView1.SelectedIndex].FindControl("OrganLabel")).Text;
            }
            if (!check && DivStatusList.SelectedIndex!=0)
            {
                string updaterId = "",
                         updateCode = "DISB_UPD";
                if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                else updaterId = (string)Session["Admin"];
                List<string> values = new List<string>();
                values.Add(DivOrganList.SelectedItem.Text);
                values.Add(DivDescBox.Text);
                values.Add(DivStatusList.SelectedItem.Text);
                values.Add(HospitalClass.getTransactionId());
                values.Add(updateCode);
                values.Add(updaterId);
                values.Add(createId);
                values.Add("update");
                int status = DataConsumer.executeProcedure("pat_disab_proc", values);
                addDelDiv.Visible = false;
                ListView1.SelectedIndex = -1;
                SearchListView.SelectedIndex = -1;
                StatusLabel.CssClass = "success normal";
                StatusLabel.Text = "Successful update.<br/>User Id: " + DivUserIdBox.Text + "<br/>Organ: " + organ;
                BindListView();
                BindSearchListView();
            }
            else
            {
                StatusLabel.CssClass = "error paraNormal";
                StatusLabel.Text = "User input error below";
                DivStatusLabel.CssClass = "error paraNormal";
                if (check) DivStatusLabel.Text = "No changes made";
                else DivStatusLabel.Text = "Please select a treatment status";
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
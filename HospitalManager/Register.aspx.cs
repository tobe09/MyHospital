using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class Register : System.Web.UI.Page
    {
        string userId = "",
            role = "",
            updateCode = "",
            updaterId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //check login status
            if (Session["User"] != null && Session["SuperUser"] == null && Session["Admin"] == null)
            {
                Session["Info"] = "You are already registered";
                Response.Redirect("~/LoggedInPage.aspx");
            }
            if (Session["Admin"] != null || Session["SuperUser"] != null)  //preliminary settings according to roles
            {
                CheckBoxDiv.Visible = true;
                if (!IsPostBack) InfoDiv.Visible = false;
                if (DocRadioButton.Checked) RegLabel.Text = "Doctor's Registration Page";
                else if (StfRadioButton.Checked) RegLabel.Text = "Staff's Registration Page";
                else if (PatientRadioButton.Checked) RegLabel.Text = "Patients Registration Page";
                else RegLabel.Text = "&nbsp;&nbsp;User Registration Page";
            }
            else
            {
                CheckBoxDiv.Visible = false;
                InfoDiv.Visible = true;
                RegLabel.Text = "Patient's Registration Page";
            }
            StatusLabel.Text = "";
        }

        protected void PatientRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            InfoDiv.Visible = true;
            DocRoleTable.Visible = false;
            StfRoleTable.Visible = false;
            RegLabel.Text = "Patient's Registration Page";
        }

        protected void DoctorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InfoDiv.Visible = true;
                DocRoleTable.Visible = true;
                StfRoleTable.Visible = false;
                RegLabel.Text = "Doctor's Registration Page";
                string docQuery = DataProvider.RegistrationPage.DocListQuery();
                DataTable dt = HospitalClass.getDataTable(docQuery);  //populate drop down list for doctors
                DocRoleList.DataSource = dt;
                DocRoleList.DataTextField = "description".ToUpper();
                DocRoleList.DataValueField = "ROLE_ID";
                DocRoleList.DataBind();
                DocRoleList.Items.Insert(0, new ListItem("Please select..."));
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void StaffRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InfoDiv.Visible = true;
                DocRoleTable.Visible = false;
                StfRoleTable.Visible = true;
                RegLabel.Text = "Staff's Registration Page";
                string stfQuery = DataProvider.RegistrationPage.StfListQuery();
                DataTable dt = HospitalClass.getDataTable(stfQuery);  //populate drop down list for staffs
                StfRoleList.DataSource = dt;
                StfRoleList.DataTextField = "DESCRIPTION";
                StfRoleList.DataValueField = "ROLE_ID";
                StfRoleList.DataBind();
                StfRoleList.Items.Insert(0, new ListItem("Please select..."));
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                StatusDiv.Visible = true;
                StatusLabel.Visible = true;
                bool buttonChk;
                if (DocRadioButton.Checked) buttonChk = DocRoleList.SelectedIndex != 0;
                else if (StfRadioButton.Checked) buttonChk = StfRoleList.SelectedIndex != 0;
                else buttonChk = true;
                string existQuery = DataProvider.RegistrationPage.ExistQuery(EmailBox.Text.ToUpper().Trim());
                DataTable dt = HospitalClass.getDataTable(existQuery);
                bool checkName = HospitalClass.sqlProtect(FirstNameBox.Text);
                bool checkPassword = HospitalClass.sqlProtect(PasswordBox.Text);
                if (buttonChk && GenderList.SelectedIndex != 0 && dt.Rows.Count==0 && checkName && checkPassword)  //validate selection of drop down list values
                {
                    if (DocRadioButton.Checked == true)
                    {
                        role = DocRoleList.SelectedItem.Value;
                        updateCode = "DOC_REG";
                    }
                    else if (StfRadioButton.Checked == true)
                    {
                        role = StfRoleList.SelectedItem.Value;
                        updateCode = "STF_REG";
                    }
                    else
                    {
                        role = "PAT";
                        updateCode = "PAT_REG"; ;
                    }
                        string month,
                            year;
                        if (DateTime.Now.Month < 10) month = "0" + DateTime.Now.Month;
                        else month = DateTime.Now.Month.ToString();
                        year = (DateTime.Now.Year.ToString()).Remove(0, 2);
                        string lastIdQuery = DataProvider.RegistrationPage.LastIdQuery(role.Remove(2));
                        DataTable lastDt = HospitalClass.getDataTable(lastIdQuery);
                        if (lastDt.Rows.Count == 0)    //first role user registration
                        {
                            if (PatientRadioButton.Checked || !CheckBoxDiv.Visible) userId = role + month + year + "0001";
                            else userId = role + month + year + "001";
                        }
                        else  //generate new id for patient, doctor or staff
                        {
                            string lastId = (string)lastDt.Rows[0][0];
                            string editId;
                            if (PatientRadioButton.Checked || !CheckBoxDiv.Visible) editId = lastId.Remove(0, lastId.Length - 4);
                            else editId = lastId.Remove(0, lastId.Length - 3);
                            int newIdInt = int.Parse(editId) + 1;
                            string newId;
                            if (newIdInt < 10) newId = editId.Remove(editId.Length - 1) + newIdInt.ToString();
                            else if (newIdInt < 100) newId = editId.Remove(editId.Length - 2) + newIdInt.ToString();
                            else if (newIdInt < 1000) newId = editId.Remove(editId.Length - 3) + newIdInt.ToString();
                            else newId = newIdInt.ToString();
                            userId = role + month + year + newId;
                        }
                        if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                        else if (Session["Admin"] != null) updaterId = (string)Session["Admin"];
                        else updaterId = userId;
                        List<string> values = new List<string>();
                        values.Add(EmailBox.Text.Trim());
                        values.Add(FirstNameBox.Text.Trim());
                        values.Add(GenderList.SelectedItem.Text);
                        values.Add(LastnameBox.Text.Trim());
                        values.Add(OtherNameBox.Text.Trim());
                        values.Add(HospitalClass.Encrypt(PasswordBox.Text));
                        values.Add(PhoneBox.Text);
                        values.Add(updateCode);
                        values.Add(updaterId);
                        values.Add(userId);
                        values.Add(HospitalClass.getTransactionId());
                        int status = DataConsumer.executeProcedure("initial_reg", values);
                        StatusLabel.CssClass = "success big";
                        StatusLabel.Text = "You have been successfully registered.<br/>Your ID is: " + userId + ".<br/>";
                        goToLogin.Visible = true;
                        InfoDiv.Visible = false;
                        RegLabel.Visible = false;
                        CheckBoxDiv.Visible = false;
                }
                else
                {
                    if (!checkName) StatusLabel.Text = "Unsecure name entry. Please remove all ' and -- symbols";
                    else if (!checkPassword) StatusLabel.Text = "Unsecure password choice. Please remove all ' and -- symbols";
                    else if (GenderList.SelectedIndex == 0) StatusLabel.Text = "Please select your sex";
                    else if (dt.Rows.Count > 0) StatusLabel.Text = "This email address has already been registered";
                    else StatusLabel.Text = "Please choose a classification/role";
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class UpdateRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //redirect if not logged in
                if (Session["Admin"] == null && Session["SuperUser"] == null && Session["User"] == null) Response.Redirect("~/Login.aspx");
                //Only SuperUser and admin has access to validate
                if ((Session["SuperUser"] == null && Session["Admin"] == null) ||
                    (Session["User"].ToString().StartsWith("SUP") || Session["User"].ToString().StartsWith("ADM")))
                {
                    ValidateButton.Visible = false;
                    ReadOnly();
                    StatusLabel.CssClass = "error paraNormal";
                    StatusLabel.ForeColor = System.Drawing.Color.Black;
                    if (Session["SuperUser"] == null && Session["Admin"] == null)
                    {
                        StatusLabel.Text = "Note: Meet the system administrator to make modifications";
                    }
                    else
                    {
                        StatusLabel.Text = "Note: This user cannot be validated here.<br/>Meet the super user for modification.";
                    }
                }
                else ValidateButton.Visible = true;
                if (Session["User"].ToString().StartsWith("PAT")) EduRefTable.Visible = false;  //remove eduucation and referee table for patients
                if (Session["User"].ToString().StartsWith("DC") || Session["User"].ToString().StartsWith("ST")) OtherInfoLabel.Text = "About yourself";
                //populate form controls
                if (!IsPostBack)
                {
                    string infoQuery = DataProvider.UpdateRegistration.getInfo                      //get user information from the database
                        (HospitalClass.getTableName(Session["User"].ToString().Substring(0, 2)), Session["User"].ToString());

                    DataTable dt = HospitalClass.getDataTable(infoQuery);
                    UserIdBox.Text = Session["User"].ToString();                                                                                  //1
                    FirstNameBox.Text = DBNull.Value.Equals(dt.Rows[0]["FIRST_NAME"]) ? "" : (string)dt.Rows[0]["FIRST_NAME"];               
                    if (!DBNull.Value.Equals(dt.Rows[0]["LAST_NAME"])) LastNameBox.Text = (string)dt.Rows[0]["LAST_NAME"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["OTHER_NAME"])) OtherNameBox.Text = (string)dt.Rows[0]["OTHER_NAME"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["GENDER"]))
                    {
                        if ((string)dt.Rows[0]["GENDER"] == "Male") GenderList.SelectedIndex = 1;
                        else if ((string)dt.Rows[0]["GENDER"] == "Female") GenderList.SelectedIndex = 2;
                        else GenderList.SelectedIndex = 3;
                    }
                    if (!DBNull.Value.Equals(dt.Rows[0]["DOB"]))                                                                        //6
                    {
                        DateTime dob = (DateTime)dt.Rows[0]["DOB"];
                        DobBox.Text = dob.ToString("dd/MM/yyyy");
                    }
                    if (!DBNull.Value.Equals(dt.Rows[0]["MARITAL_STAT"]))
                    {
                        if ((string)dt.Rows[0]["MARITAL_STAT"] == "Single") MaritalList.SelectedIndex = 1;
                        else if ((string)dt.Rows[0]["MARITAL_STAT"] == "Married") MaritalList.SelectedIndex = 2;
                        else MaritalList.SelectedIndex = 3;
                    }
                    if (!DBNull.Value.Equals(dt.Rows[0]["COUNTRY_ORI"])) CountryOriBox.Text = (string)dt.Rows[0]["COUNTRY_ORI"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["STATE_ORI"])) StateOriBox.Text = (string)dt.Rows[0]["STATE_ORI"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["LGA_ORI"])) LocalOriBox.Text = (string)dt.Rows[0]["LGA_ORI"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["PHONE_NO"])) PhoneBox.Text = (string)dt.Rows[0]["PHONE_NO"];                   //11
                    if (!DBNull.Value.Equals(dt.Rows[0]["EMAIL"])) EmailBox.Text = (string)dt.Rows[0]["EMAIL"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["COUNTRY_RES"])) CountryResBox.Text = (string)dt.Rows[0]["COUNTRY_RES"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["STATE_RES"])) StateResBox.Text = (string)dt.Rows[0]["STATE_RES"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["LGA_RES"])) LocalResBox.Text = (string)dt.Rows[0]["LGA_RES"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["HOME_ADR"])) HomeAdrBox.Text = (string)dt.Rows[0]["HOME_ADR"];                 //16
                    if (!DBNull.Value.Equals(dt.Rows[0]["ID_TYPE"]))
                    {
                        if ((string)dt.Rows[0]["ID_TYPE"] == "National Id") IdTypeList.SelectedIndex = 1;
                        else if ((string)dt.Rows[0]["ID_TYPE"] == "School Id") IdTypeList.SelectedIndex = 2;
                        else if ((string)dt.Rows[0]["ID_TYPE"] == "Voters Card") IdTypeList.SelectedIndex = 3;
                        else IdTypeList.SelectedIndex = 4;
                    }
                    if (!DBNull.Value.Equals(dt.Rows[0]["ID_NO"])) IdNoBox.Text = (string)dt.Rows[0]["ID_NO"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["NEXT_OF_KIN"])) NextNameBox.Text = (string)dt.Rows[0]["NEXT_OF_KIN"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["NXT_OF_KIN_REL"])) NextRelBox.Text = (string)dt.Rows[0]["NXT_OF_KIN_REL"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["NXT_PHONE_NO"])) NextPhoneBox.Text = (string)dt.Rows[0]["NXT_PHONE_NO"];       //21
                    if (!DBNull.Value.Equals(dt.Rows[0]["NXT_EMAIL"])) NextEmailBox.Text = (string)dt.Rows[0]["NXT_EMAIL"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["NXT_ADR"])) NextAdrBox.Text = (string)dt.Rows[0]["NXT_ADR"];
                    if (!DBNull.Value.Equals(dt.Rows[0]["GENOTYPE"]))
                    {
                        if ((string)dt.Rows[0]["GENOTYPE"] == "AA") TypeList.SelectedIndex = 1;
                        else if ((string)dt.Rows[0]["GENOTYPE"] == "AS") TypeList.SelectedIndex = 2;
                        else if ((string)dt.Rows[0]["GENOTYPE"] == "SS") TypeList.SelectedIndex = 3;
                        else TypeList.SelectedIndex = 4;
                    }
                    if (!DBNull.Value.Equals(dt.Rows[0]["BLOOD_GRP"]))
                    {
                        if ((string)dt.Rows[0]["BLOOD_GRP"] == "A") GroupList.SelectedIndex = 1;
                        else if ((string)dt.Rows[0]["BLOOD_GRP"] == "B") GroupList.SelectedIndex = 2;
                        else if ((string)dt.Rows[0]["BLOOD_GRP"] == "AB") GroupList.SelectedIndex = 3;
                        else if ((string)dt.Rows[0]["BLOOD_GRP"] == "O") GroupList.SelectedIndex = 4;
                        else TypeList.SelectedIndex = 5;
                    }
                    if (!DBNull.Value.Equals(dt.Rows[0]["OTHER_INFO"])) OtherInfoBox.Text = (string)dt.Rows[0]["OTHER_INFO"];           //26
                    if (Session["User"].ToString().StartsWith("PAT"))  //for patients
                    {
                        if (!DBNull.Value.Equals(dt.Rows[0]["MATRIC_NO"])) SchoolIdBox.Text = (string)dt.Rows[0]["MATRIC_NO"];          //27
                    }
                    else  //for other users
                    {
                        if (!DBNull.Value.Equals(dt.Rows[0]["SCHL_ID_NO"])) SchoolIdBox.Text = (string)dt.Rows[0]["SCHL_ID_NO"];        //27
                        if (!DBNull.Value.Equals(dt.Rows[0]["PRI_SCHL"])) PriBox.Text = (string)dt.Rows[0]["PRI_SCHL"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["PRI_CERT"]))
                        {
                            if ((string)dt.Rows[0]["PRI_CERT"] == "First School Leaving Certificate (FSLT)") PriList.SelectedIndex = 1;
                            else if ((string)dt.Rows[0]["PRI_CERT"] == "None") PriList.SelectedIndex = 2;
                            else PriList.SelectedIndex = 3;
                        }
                        if (!DBNull.Value.Equals(dt.Rows[0]["SEC_SCHL"])) SecBox.Text = (string)dt.Rows[0]["SEC_SCHL"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["SEC_CERT"]))                                                               //31
                        {
                            if ((string)dt.Rows[0]["SEC_CERT"] == "WASSCE/WAEC") SecList.SelectedIndex = 1;
                            else if ((string)dt.Rows[0]["SEC_CERT"] == "GCE") SecList.SelectedIndex = 2;
                            else if ((string)dt.Rows[0]["SEC_CERT"] == "NECO") SecList.SelectedIndex = 3;
                            else if ((string)dt.Rows[0]["SEC_CERT"] == "None") SecList.SelectedIndex = 4;
                            else SecList.SelectedIndex = 5;
                        }
                        if (!DBNull.Value.Equals(dt.Rows[0]["UNI"])) UniBox.Text = (string)dt.Rows[0]["UNI"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["UNI_CERT"]))
                        {
                            if ((string)dt.Rows[0]["UNI_CERT"] == "Pass (Doctor)") UniList.SelectedIndex = 1;
                            else if ((string)dt.Rows[0]["UNI_CERT"] == "First Class") UniList.SelectedIndex = 2;
                            else if ((string)dt.Rows[0]["UNI_CERT"] == "Second class, Upper Division") UniList.SelectedIndex = 3;
                            else if ((string)dt.Rows[0]["UNI_CERT"] == "Second class, Lower division") UniList.SelectedIndex = 4;
                            else if ((string)dt.Rows[0]["UNI_CERT"] == "Third Class") UniList.SelectedIndex = 5;
                            else if ((string)dt.Rows[0]["UNI_CERT"] == "Pass (Staff)") UniList.SelectedIndex = 6;
                            else if ((string)dt.Rows[0]["UNI_CERT"] == "None") UniList.SelectedIndex = 7;
                            else UniList.SelectedIndex = 8;
                        }
                        if (!DBNull.Value.Equals(dt.Rows[0]["OTHER_INST1"])) OtherBox1.Text = (string)dt.Rows[0]["OTHER_INST1"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["OTHER_CERT1"])) OtherCert1.Text = (string)dt.Rows[0]["OTHER_CERT1"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["OTHER_INST2"])) OtherBox2.Text = (string)dt.Rows[0]["OTHER_INST2"];         //36
                        if (!DBNull.Value.Equals(dt.Rows[0]["OTHER_CERT2"])) OtherCert2.Text = (string)dt.Rows[0]["OTHER_CERT2"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["REF_NAME1"])) RefNameBox1.Text = (string)dt.Rows[0]["REF_NAME1"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["REF_OF_KIN_REL1"])) RefRelBox1.Text = (string)dt.Rows[0]["REF_OF_KIN_REL1"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["REF_PHONE_NO1"])) RefPhoneBox1.Text = (string)dt.Rows[0]["REF_PHONE_NO1"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["REF_EMAIL1"])) RefEmailBox1.Text = (string)dt.Rows[0]["REF_EMAIL1"];       //41
                        if (!DBNull.Value.Equals(dt.Rows[0]["REF_ADR1"])) RefAdrBox1.Text = (string)dt.Rows[0]["REF_ADR1"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["REF_NAME2"])) RefNameBox2.Text = (string)dt.Rows[0]["REF_NAME2"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["REF_OF_KIN_REL2"])) RefRelBox2.Text = (string)dt.Rows[0]["REF_OF_KIN_REL2"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["REF_PHONE_NO2"])) RefPhoneBox2.Text = (string)dt.Rows[0]["REF_PHONE_NO2"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["REF_EMAIL2"])) RefEmailBox2.Text = (string)dt.Rows[0]["REF_EMAIL2"];
                        if (!DBNull.Value.Equals(dt.Rows[0]["REF_ADR2"])) RefAdrBox2.Text = (string)dt.Rows[0]["REF_ADR2"];             //47
                    }
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void ReadOnly()
        {
            UserIdBox.ReadOnly = true;
            FirstNameBox.ReadOnly = true;
            LastNameBox.ReadOnly = true;
            OtherNameBox.ReadOnly = true;
            DobBox.ReadOnly = true;
            CountryOriBox.ReadOnly = true;
            StateOriBox.ReadOnly = true;
            LocalOriBox.ReadOnly = true;
            PhoneBox.ReadOnly = true;
            EmailBox.ReadOnly = true;
            CountryResBox.ReadOnly = true;
            StateResBox.ReadOnly = true;
            LocalResBox.ReadOnly = true;
            HomeAdrBox.ReadOnly = true;
            IdNoBox.ReadOnly = true;
            SchoolIdBox.ReadOnly = true;
            NextNameBox.ReadOnly = true;
            NextRelBox.ReadOnly = true;
            NextPhoneBox.ReadOnly = true;
            NextAdrBox.ReadOnly = true;
            NextEmailBox.ReadOnly = true;
            OtherInfoBox.ReadOnly = true;
            PriBox.ReadOnly = true;
            SecBox.ReadOnly = true;
            UniBox.ReadOnly = true;
            OtherBox1.ReadOnly = true;
            OtherBox2.ReadOnly = true;
            OtherCert1.ReadOnly = true;
            OtherCert2.ReadOnly = true;
            RefNameBox1.ReadOnly = true;
            RefNameBox2.ReadOnly = true;
            RefRelBox1.ReadOnly = true;
            RefRelBox2.ReadOnly = true;
            RefAdrBox1.ReadOnly = true;
            RefAdrBox2.ReadOnly = true;
            RefPhoneBox1.ReadOnly = true;
            RefPhoneBox2.ReadOnly = true;
            RefEmailBox1.ReadOnly = true;
            RefEmailBox2.ReadOnly = true;
        }

        protected void ClearAll()
        {
            FirstNameBox.Text = "";
            LastNameBox.Text = "";
            OtherNameBox.Text = "";
            GenderList.SelectedIndex = 0;
            DobBox.Text = "";
            MaritalList.SelectedIndex = 0;
            CountryOriBox.Text = "";
            StateOriBox.Text = "";
            LocalOriBox.Text = "";
            PhoneBox.Text = "";
            EmailBox.Text = "";
            CountryResBox.Text = "";
            StateResBox.Text = "";
            LocalResBox.Text = "";
            HomeAdrBox.Text = "";
            IdTypeList.SelectedIndex = 0;
            IdNoBox.Text = "";
            SchoolIdBox.Text = "";
            NextNameBox.Text = "";
            NextRelBox.Text = "";
            NextPhoneBox.Text = "";
            NextEmailBox.Text = "";
            NextAdrBox.Text = "";
            GroupList.SelectedIndex = 0;
            TypeList.SelectedIndex = 0;
            OtherInfoBox.Text = "";
            PriBox.Text = "";
            PriList.SelectedIndex = 0;
            SecBox.Text = "";
            SecList.SelectedIndex = 0;
            UniBox.Text = "";
            UniList.SelectedIndex = 0;
            OtherBox1.Text = "";
            OtherCert1.Text = "";
            OtherBox2.Text = "";
            OtherCert2.Text = "";
            RefNameBox1.Text = "";
            RefRelBox1.Text = "";
            RefPhoneBox1.Text = "";
            RefEmailBox1.Text = "";
            RefAdrBox1.Text = "";
            RefNameBox2.Text = "";
            RefRelBox2.Text = "";
            RefPhoneBox2.Text = "";
            RefEmailBox2.Text = "";
            RefAdrBox2.Text = "";
        }

        protected void ValidateButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool verify = true;
                if (Session["User"].ToString().StartsWith("PAT"))
                {
                    verify = GenderList.SelectedIndex != 0 && MaritalList.SelectedIndex != 0 && IdTypeList.SelectedIndex != 0 &&
                        TypeList.SelectedIndex != 0 && GroupList.SelectedIndex != 0;
                }
                else
                {
                    verify = GenderList.SelectedIndex != 0 && MaritalList.SelectedIndex != 0 && IdTypeList.SelectedIndex != 0 && TypeList.SelectedIndex != 0 &&
                    GroupList.SelectedIndex != 0 && PriList.SelectedIndex != 0 && SecList.SelectedIndex != 0 && UniList.SelectedIndex != 0;
                }
                if (verify)
                {
                    //submit values after verification
                    string userId = UserIdBox.Text;
                    string updaterId = "";
                    if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                    else updaterId = (string)Session["Admin"];
                    string updateCode = "";
                    if (Session["User"].ToString().StartsWith("PAT")) updateCode = "PAT_UPD";
                    else if (Session["User"].ToString().StartsWith("DC")) updateCode = "DOC_UPD";
                    else updateCode = "STF_UPD";
                    List<string> values = new List<string>();
                    values.Add(GroupList.SelectedItem.Text);    //1
                    values.Add(TypeList.SelectedItem.Text);
                    values.Add(CountryOriBox.Text);
                    values.Add(CountryResBox.Text);
                    values.Add(DobBox.Text);
                    values.Add(EmailBox.Text);                  //6
                    values.Add(FirstNameBox.Text);
                    values.Add(GenderList.SelectedItem.Text);
                    values.Add(HomeAdrBox.Text);
                    values.Add(IdNoBox.Text);
                    values.Add(IdTypeList.SelectedItem.Text);   //11
                    values.Add(LastNameBox.Text);
                    values.Add(LocalOriBox.Text);
                    values.Add(LocalResBox.Text);
                    values.Add(MaritalList.SelectedItem.Text);
                    values.Add(NextAdrBox.Text);                //16
                    values.Add(NextEmailBox.Text);
                    values.Add(NextNameBox.Text);
                    values.Add(NextRelBox.Text);
                    values.Add(NextPhoneBox.Text);
                    values.Add(OtherNameBox.Text);              //21
                    values.Add(PhoneBox.Text);
                    values.Add(SchoolIdBox.Text);
                    values.Add(StateOriBox.Text);
                    values.Add(StateResBox.Text);
                    values.Add(OtherInfoBox.Text);              //26
                    values.Add(PriBox.Text);
                    values.Add(PriList.SelectedItem.Text);
                    values.Add(SecBox.Text);
                    values.Add(SecList.SelectedItem.Text);
                    values.Add(UniBox.Text);                    //31
                    values.Add(UniList.SelectedItem.Text);
                    values.Add(OtherBox1.Text);
                    values.Add(OtherCert1.Text);
                    values.Add(OtherBox2.Text);
                    values.Add(OtherCert2.Text);                //36
                    values.Add(RefNameBox1.Text);
                    values.Add(RefRelBox1.Text);
                    values.Add(RefPhoneBox1.Text);
                    values.Add(RefEmailBox1.Text);
                    values.Add(RefAdrBox1.Text);                //41
                    values.Add(RefNameBox2.Text);
                    values.Add(RefRelBox2.Text);
                    values.Add(RefPhoneBox2.Text);
                    values.Add(RefEmailBox2.Text);
                    values.Add(RefAdrBox2.Text);                //46
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add(userId);
                    values.Add(HospitalClass.getTransactionId());  //50
                    int status = DataConsumer.executeProcedure("user_val", values);
                    Session["FirstName"] = FirstNameBox.Text;
                    Session["RegStatus"] = "Validated";
                    StatusLabel.Text = "Update Successful.<br/>Performer: " + updaterId;
                    StatusLabel.CssClass = "success paraNormal";
                }
                else
                {
                    StatusLabel.CssClass = "error paraNormal";
                    if (GenderList.SelectedIndex == 0) StatusLabel.Text = "Please select your gender";
                    else if (MaritalList.SelectedIndex == 0) StatusLabel.Text = "Please select your marital status";
                    else if (IdTypeList.SelectedIndex == 0) StatusLabel.Text = "Please select your Identificaton type";
                    else if (TypeList.SelectedIndex == 0) StatusLabel.Text = "Please select your blood type";
                    else if (GroupList.SelectedIndex == 0) StatusLabel.Text = "Please select your blood group";
                    else if (PriList.SelectedIndex == 0) StatusLabel.Text = "Please select your primary school certificate. (Select none if unavailable)";
                    else if (SecList.SelectedIndex == 0) StatusLabel.Text = "Please select your secondary school certificate. (Select none if unavailable)";
                    else if (UniList.SelectedIndex == 0) StatusLabel.Text = "Please select your university certificate grade. (Select none if unavailable)";
                    else StatusLabel.Text = "Fill all relevalt information";  //will not occur
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
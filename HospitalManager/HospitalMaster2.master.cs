using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManager
{
    public partial class HospitalMaster2 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get user Id
            if (Session["User"] != null)
            {
                UserIdLabel.Text = Session["User"].ToString();
                //determining visibility options according to roles
                if (Session["User"].ToString().StartsWith("PAT")) PatientDiv.Visible = true;
                if (Session["User"].ToString().StartsWith("ST") || Session["User"].ToString().StartsWith("DC")) StaffDiv.Visible = true;
                if (Session["User"].ToString().StartsWith("DC")) DocStfSectionLabel.Text = "Doctor's Section";
                if (Session["User"].ToString().StartsWith("ST")) DocStfSectionLabel.Text = "Staff's Section";
            }
            if (Session["SuperUser"] != null) { suDiv.Visible = true; adminDiv.Visible = true; }
            if (Session["Admin"] != null) adminDiv.Visible = true;
            if (Session["Unsubscribe"] != null && (Session["SuperUser"] != null || Session["Admin"] != null))  //set visibility of the option to unsubscribe
            {
                unSubDiv.Visible = true;
                Session["Unsubscribe"] = null;
            }
            if (!this.IsPostBack && Session["Link"] != null) { ShowSelected(Session["Link"].ToString()); }
        }

        protected void PatientIssuesButton_Click(object sender, EventArgs e)
        {
            Session["Quote"] = "Patient's Issues";
            Response.Redirect("~/PatientDisabilities.aspx");
        }

        protected void EmpHistButton_Click(object sender, EventArgs e)
        {
            Session["Quote"] = "Employee's History";
            Response.Redirect("~/DoctorStaffHistory.aspx");
        }

        protected void PatientsLinkBtn_Click(object sender, EventArgs e)
        {
            Session["Quote"] = "AdminPat";                                       //for page loading of sublinks
            HideAll("pat");
            InvertVisibility(ref PatientDivAdm);
            if (PatientDivAdm.Visible) Session["Link"] = "pat";                  //to persist visibility
            else Session["Link"] = null;
        }

        protected void Doctors_Click(object sender, EventArgs e)
        {
            Session["Quote"] = "AdminDoc";
            HideAll("doc");
            InvertVisibility(ref DocDivAdm);
            if (DocDivAdm.Visible) Session["Link"] = "doc";                
            else Session["Link"] = null;
        }

        protected void StaffLinkBtn_Click(object sender, EventArgs e)
        {
            Session["Quote"] = "AdminStf";
            HideAll("stf");
            InvertVisibility(ref StfDivAdm);
            if (StfDivAdm.Visible) Session["Link"] = "stf";
            else Session["Link"] = null;
        }

        protected void InfoLinkBtn_Click(object sender, EventArgs e)
        {
            HideAll("info");
            InvertVisibility(ref InfoDivAdm);
            if (InfoDivAdm.Visible) Session["Link"] = "info";
            else Session["Link"] = null;
        }

        protected void DeptWardLinkBtn_Click(object sender, EventArgs e)
        {
            HideAll("dept");
            InvertVisibility(ref DeptDivAdm);
            if (DeptDivAdm.Visible) Session["Link"] = "dept";
            else Session["Link"] = null;
        }

        private void InvertVisibility(ref Panel a)
        {
            if (!a.Visible) a.Visible = true;
            else a.Visible = false;
        }

        private void HideAll(string from)
        {
            if (from != "pat") PatientDivAdm.Visible = false;
            if (from != "doc") DocDivAdm.Visible = false;
            if (from != "stf") StfDivAdm.Visible = false;
            if (from != "info") InfoDivAdm.Visible = false;
            if (from != "dept") DeptDivAdm.Visible = false;
        }

        private void ShowSelected(string from)
        {
            if (from == "pat") PatientDivAdm.Visible = true;
            else if (from == "doc") DocDivAdm.Visible = true;
            else if (from == "stf") StfDivAdm.Visible = true;
            else if (from == "info") InfoDivAdm.Visible = true;
            else if (from == "dept") DeptDivAdm.Visible = true;
            else HideAll("none");
        }

    }
}
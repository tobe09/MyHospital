using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class Login1 : System.Web.UI.Page
    {
        string userId = "",
            updateCode = "",
            updaterId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null && Session["SuperUser"] == null && Session["Admin"] == null)    //to use in logged in page
            {
                Session["Info"] = "You are already logged in";
                Response.Redirect("~/LoggedInPage.aspx");
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool checkId = HospitalClass.sqlProtect(UserIdBox.Text);     //security for user id input against sqlInjection
                bool checkPwd = HospitalClass.sqlProtect(PasswordBox.Text);  //security for password input against sqlInjection
                if (UserIdBox.Text.Length != 0 && checkId && checkPwd)
                {
                    string passwordQuery = DataProvider.LoginPage.PasswordQuery(UserIdBox.Text.ToUpper().Trim());
                    DataTable dt = HospitalClass.getDataTable(passwordQuery);
                    if (dt.Rows.Count > 0)
                    {
                        string encryptPassword = HospitalClass.Encrypt(PasswordBox.Text);
                        int checkPassword = 0;
                        string userIdPass = "";
                        foreach (DataRow row in dt.Rows)
                        {
                            if (encryptPassword == row[0].ToString())  //check if password correlate
                            {
                                checkPassword = 1;
                                userIdPass = row[1].ToString();  //check first name repetition
                            }
                            userId = row[1].ToString();
                            if (userIdPass.Length > 0) userId = userIdPass;
                        }
                        if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                        else if (Session["Admin"] != null) updaterId = (string)Session["Admin"];
                        else updaterId = userId;
                        bool rights;
                        if (userId.StartsWith("ADM") || userId.StartsWith("SUP"))
                        { rights = false; }
                        else rights = true;
                        //for either a basic user or a privileged user
                        if ((Session["SuperUser"] != null || Session["Admin"] != null || (checkPassword == 1 && rights)) || (!rights && checkPassword == 1))
                        {
                            bool auditTrailValidator = false;
                            string oldUser = "";  //to check the access of an administrator or a superuser
                            if (Session["SuperUser"] != null) oldUser = Session["SuperUser"].ToString();
                            else if (Session["Admin"] != null) oldUser = Session["Admin"].ToString();
                            Session["User"] = userId;
                            //using linq to datasets to query the datatable (to guard against two users with the same first name)
                            Session["FirstName"] = (from FirstName in dt.AsEnumerable()
                                                    where FirstName.Field<string>("USER_ID") == userId
                                                    select FirstName.Field<string>("FIRST_NAME")).First().ToString();
                            Session["RegStatus"] = (from Status in dt.AsEnumerable()
                                                    where Status.Field<string>("USER_ID") == userId
                                                    select Status.Field<string>("STATUS")).First().ToString();
                            //login for users
                            if ((Session["SuperUser"] != null || Session["Admin"] != null || checkPassword == 1) && rights)
                            {
                                if (userId.StartsWith("PAT")) { updateCode = "PAT_LGN"; }
                                else if (userId.StartsWith("DC")) { updateCode = "DOC_LGN"; }
                                else { updateCode = "STF_LGN"; };
                                Response.Redirect("~/LoggedInPage.aspx", false);
                                auditTrailValidator = true;
                            }
                            //login for admin and superuser
                            else if (!rights && checkPassword == 1)
                            {
                                if (userId.StartsWith("SUP"))
                                {
                                    Session["SuperUser"] = userId;
                                    updateCode = "SUP_LGN";
                                }
                                else
                                {
                                    Session["Admin"] = userId;
                                    updateCode = "ADM_LGN";
                                }
                                Response.Redirect("~/LoggedInPage.aspx", false);
                                auditTrailValidator = true;
                            }
                            //extraneous login for superuser and administrator
                            else
                            {
                                //to catch unauthorized access to another privileged user's account
                                if ((Session["SuperUser"] != null && !userId.StartsWith("SUP")) || oldUser == userId) //for privileged user relogin
                                {
                                    Session["User"] = userId;
                                    if (oldUser == userId) Session["Info"] = "Welcome Back";
                                    if (userId.ToUpper().StartsWith("ADM")) { updateCode = "ADM_LGN"; }
                                    else updateCode = "SUP_LGN";
                                    Response.Redirect("~/LoggedInPage.aspx", false);
                                    auditTrailValidator = true;
                                }
                                else
                                {
                                    StatusLabel.Text = "You do not have access to this profile";
                                    if (oldUser.StartsWith("SUP")) Session["User"] = Session["SuperUser"].ToString();
                                    else Session["User"] = Session["Admin"].ToString();
                                    auditTrailValidator = false;
                                }
                            }
                            if (auditTrailValidator)
                            {
                                object[] values = new object[4];
                                values[0] = (HospitalClass.getTransactionId());
                                values[1] = (updateCode);
                                values[2] = (updaterId);
                                values[3] = (userId);
                                int status = DataConsumer.executeProc("audit_trail_proc", values);
                            }
                        }
                        else
                        {
                            if (PasswordBox.Text.Length == 0) StatusLabel.Text = "Please enter your password";
                            else StatusLabel.Text = "Wrong user Id/password combination.<br/>Note: Password is case-sensitive.";
                        }
                    }
                    else StatusLabel.Text = "You are not a user on our database. Please register.";
                }
                else
                {
                    if(UserIdBox.Text.Length==0) StatusLabel.Text = "Please enter your user id, first name or email address";
                    else if (!checkId) StatusLabel.Text = "Unsecure user Id";
                    else StatusLabel.Text = "Unsecure password";
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
                //ex.Logger();
            }
        }
    }
}
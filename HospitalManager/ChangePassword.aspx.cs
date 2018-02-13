using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;

namespace HospitalManager
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuperUser"] == null && Session["Admin"] == null && Session["User"] == null) Response.Redirect("~/Login.aspx");
            if (Session["SuperUser"] != null || Session["Admin"] != null) SuDiv.Visible = true;
            UserIdBox.Text = Session["User"].ToString();
            UserIdBox.ReadOnly = true;
            if (!IsPostBack)
            {
                DataTable dt = HospitalClass.getDataTable(DataProvider.ChangePassword.getEmail(Session["User"].ToString()));  //get user email
                EmailBox.Text = HospitalClass.PascalCasing(dt.Rows[0][0].ToString());
            }
            EmailBox.ReadOnly = true;
            if (Session["SuperUser"] != null || Session["Admin"] != null) EmailBox.ReadOnly = false;
        }

        protected void ChangeButton_Click(object sender, EventArgs e)
        {
            try
            {
                string checkPassword = DataProvider.ChangePassword.getPassword(Session["User"].ToString());  //get the former password and check
                DataTable dt = HospitalClass.getDataTable(checkPassword);
                if (dt.Rows[0][0].ToString() == HospitalClass.Encrypt(OldPasswordBox.Text) && OldPasswordBox.Text!=NewPasswordBox.Text)
                {
                    string updaterId = "",
                        updateCode = "PWD_CHG";
                    if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                    else if (Session["Admin"] != null) updaterId = (string)Session["Admin"];
                    else updaterId = (string)Session["User"];
                    List<string> values = new List<string>();
                    values.Add(HospitalClass.Encrypt(NewPasswordBox.Text));
                    values.Add(HospitalClass.getTransactionId());
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add((string)Session["User"]);
                    int status = DataConsumer.executeProcedure("pwd_change", values);
                    UserStatusLabel.CssClass = "success paraNormal";
                    UserStatusLabel.Text = "Password was successfully changed";
                }
                else
                {
                    UserStatusLabel.CssClass = "error normal";
                    if (OldPasswordBox.Text == NewPasswordBox.Text) UserStatusLabel.Text = "Same password entered";
                    else UserStatusLabel.Text = "Wrong password entered.<br/>Meet the system administrator for assistance"; 
                }
            }
            catch (Exception ex)
            {
                UserStatusLabel.CssClass = "error paraNormal";
                UserStatusLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void getPasswordButton_Click(object sender, EventArgs e)
        {
            try
            {
                string getPassword = DataProvider.ChangePassword.getPassword(UserIdBox.Text.Trim().ToUpper());  //get the user's password
                DataTable dt = HospitalClass.getDataTable(getPassword);
                if (UserIdBox.Text.Trim().Length == 6 || UserIdBox.Text.Trim().Length == 11 && dt.Rows.Count > 0)  //unnecessary due to access restrictions
                {
                    string updaterId = "",
                        updateCode = "PWD_GET";
                    if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                    else updaterId = (string)Session["Admin"];
                    List<string> values = new List<string>();
                    values.Add(HospitalClass.getTransactionId());
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add(UserIdBox.Text.Trim().ToUpper());
                    int status = DataConsumer.executeProcedure("audit_trail_proc", values);
                    PasswordStatusLabel.CssClass = "success paraNormal";
                    PasswordStatusLabel.Text = "Your password is: " + HospitalClass.Decrypt(dt.Rows[0][0].ToString());  //display password
                }
                else
                {
                    PasswordStatusLabel.CssClass = "error paraNormal";
                    if (dt.Rows.Count == 0) PasswordStatusLabel.Text = UserIdBox.Text.Trim().ToUpper() + " is not a user on this system";
                    else if (UserIdBox.Text.Length == 0) PasswordStatusLabel.Text = "User Id cannot be empty";
                    else PasswordStatusLabel.Text = UserIdBox.Text.Trim().ToUpper() + " is not a valid id";
                }
            }
            catch (Exception ex)
            {
                UserStatusLabel.CssClass = "error paraNormal";
                UserStatusLabel.Text = "Error: " + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void EmailButton_Click(object sender, EventArgs e)
        {
            try
            {
                string emailInfo = DataProvider.ChangePassword.getEmailPassword(EmailBox.Text.Trim().ToUpper());  //get user's email
                DataTable dt = HospitalClass.getDataTable(emailInfo);
                if (dt.Rows.Count > 0)
                {
                    string firstName = HospitalClass.PascalCasing(dt.Rows[0][0].ToString());
                    string userId = dt.Rows[0][1].ToString();
                    string password = HospitalClass.Decrypt(dt.Rows[0][2].ToString());
                    string message = string.Format("Good day user {0}.\r\n\r\nYour user Id is: {1}\r\nYour password is: {2}", firstName, userId, password);
                    MailMessage myMessage = new MailMessage();
                    myMessage.Body = message;
                    //myMessage.From = new MailAddress(EmailBox.Text, UsernameBox.Text); //Unneccesary, set at web.config
                    myMessage.To.Add(new MailAddress(EmailBox.Text.Trim()));
                    SmtpClient mySender = new SmtpClient();
                    mySender.Send(myMessage);   //uses config file settings to send message
                    string updaterId = "",
                        updateCode = "PWD_SND";
                    if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
                    else if (Session["Admin"] != null) updaterId = (string)Session["Admin"];
                    else updaterId = (string)Session["User"];
                    List<string> values = new List<string>();
                    values.Add(HospitalClass.getTransactionId());
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add((string)Session["User"]);
                    int status = DataConsumer.executeProcedure("audit_trail_proc", values);
                    EmailLabel.CssClass = "success normal";
                    EmailLabel.Text = "Successful.<br/>Check your E-mail box for details.";
                }
                else
                {
                    EmailLabel.CssClass = "error paraNormal";
                    if (EmailBox.Text.Length == 0) EmailLabel.Text = "Please enter an email";
                    else EmailLabel.Text = "Email does not exist";
                }
            }
            catch (Exception ex)
            {
                EmailLabel.CssClass = "error paraNormal";
                EmailLabel.Text = "Error: " + ex.Message;
            }
        }
    }
}
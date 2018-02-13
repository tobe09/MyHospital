using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail; //important import

namespace HospitalManager
{
    public partial class MailForm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null) { UsernameBox.Text = Session["User"].ToString(); UsernameBox.ReadOnly = true; }
        }

        protected void MessageButton_Click(object sender, EventArgs e)
        {

            try
            {
                if (SubjectBox.Text != "" && MessageBox.Text != "" && EmailBox.Text != "" && UsernameBox.Text != "")
                {
                    string info = string.Format("\r\n\r\n\r\nUsername: {0}.\t\tEmail Address: {1}", UsernameBox.Text, EmailBox.Text);
                    MailMessage myMessage = new MailMessage();
                    myMessage.Body = MessageBox.Text + info;
                    myMessage.Subject = SubjectBox.Text;
                    //myMessage.From = new MailAddress(EmailBox.Text, UsernameBox.Text); !Unneccesary
                    myMessage.To.Add(new MailAddress("Philipbradley09@gmail.com", "Phil"));
                    if (CheckBox1.Checked == true) myMessage.To.Add(new MailAddress(EmailBox.Text, UsernameBox.Text));
                    SmtpClient mySender = new SmtpClient();
                    mySender.Send(myMessage);
                    SentLabel.CssClass = "success";
                    SentLabel.Text = "Message Sent";
                }
                else
                {
                    SentLabel.CssClass = "error";
                    if (UsernameBox.Text == "") SentLabel.Text = "Please enter your Username";
                    else if (EmailBox.Text == "") SentLabel.Text = "Enter your Email Address";
                    else if (SubjectBox.Text == "") SentLabel.Text = "Subject cannot be empty";
                    else if (MessageBox.Text == "") SentLabel.Text = "Message cannot be empty";
                    else SentLabel.Text = "IMPOSSIBLE!!";
                }
            }
            catch (Exception ex)
            {
                SentLabel.CssClass = "error";
                SentLabel.Text = "Error: " + ex.Message;
            }
        }
    }
}
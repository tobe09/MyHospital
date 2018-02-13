using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class UserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null && Session["Admin"] == null && Session["SuperUser"] == null) Response.Redirect("~/Login.aspx");  //not logged in
            Session["Unsubscribe"] = "Unsubscribe"; //set session to display unsubscribe link only on profile page
            WelcomeLabel.Text = "Welcome " + HospitalClass.PascalCasing((string)Session["FirstName"]);
            string regStatus = "";
            if (Session["RegStatus"] != null) regStatus = (string)Session["RegStatus"];  //registration status of user
            if (regStatus.ToUpper() != "VALIDATED")
            {
                HistLabel.CssClass = "error";
                HistLabel.Text = "Incomplete Registration".ToUpper();
                InfoLabel.Visible = false;
                HistoryDiv.Visible = false;
                ActivitiesDiv.Visible = false;
            }
            try
            {
                //get profile picture
                string getPicAdr = DataProvider.LoggedInPage.getPicAddress((string)Session["User"]);
                DataTable picDt = HospitalClass.getDataTable(getPicAdr);
                if (picDt.Rows.Count > 0) ProfilePic.ImageUrl = picDt.Rows[0][0].ToString();
                else ProfilePic.ImageUrl = @"~\Images\UploadProfilePicture.PNG";
                //get user history information
                string histInfoQuery = DataProvider.LoggedInPage.getHistoryInfo((string)Session["User"]);
                DataTable histDt = HospitalClass.getDataTable(histInfoQuery);
                //populate user history table
                if (histDt.Rows.Count > 0)
                {
                    for (int i = 0; i < histDt.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();
                        for (int j = 0; j <= histDt.Columns.Count; j++)
                        {
                            TableCell cell = new TableCell();
                            if (j == 0)
                            {
                                cell.Text = (i + 1).ToString();
                            }
                            else
                            {
                                if (!DBNull.Value.Equals(histDt.Rows[i][j-1])) cell.Text = histDt.Rows[i][j-1].ToString();
                                else cell.Text = "";
                            }
                            row.Cells.Add(cell);
                        }
                        HistoryTable.Rows.Add(row);
                    }
                }
                //get general information pertaining to user
                string userSubstring = "";
                if (Session["User"].ToString().StartsWith("SUP") || Session["User"].ToString().StartsWith("ADM")) userSubstring = "";  //gets all information
                else userSubstring = ((string)Session["User"]).Substring(0, 2);  //gets related information
                string generalInfoQuery = DataProvider.LoggedInPage.getGeneralInfo(userSubstring);
                DataTable genInfoDt = HospitalClass.getDataTable(generalInfoQuery);
                //populate information table
                if (genInfoDt.Rows.Count > 0)
                {
                    for (int i = 0; i < genInfoDt.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();
                        for (int j = 0; j <= genInfoDt.Columns.Count; j++)
                        {
                            TableCell cell = new TableCell();
                            if (j == 0)
                            {
                                cell.Text = (i + 1).ToString();
                            }
                            else
                            {
                                if (!DBNull.Value.Equals(genInfoDt.Rows[i][j-1])) cell.Text = genInfoDt.Rows[i][j-1].ToString();
                                else cell.Text = "";
                            }
                            row.Cells.Add(cell);
                        }
                        InformationTable.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            { 
                WelcomeLabel.CssClass = "error";
                WelcomeLabel.Text += " (An error has occured)";
                HospitalClass.Log(ex);
            }
        }

        protected void showPicUploadButton1_Click(object sender, EventArgs e)
        {
            PicUploadDiv.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PicUploadDiv.Visible = false;
        }

        protected void UploadPicButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile && (FileUpload1.FileName.ToLower().EndsWith(".gif") || FileUpload1.FileName.ToLower().EndsWith(".jpg") ||
                    FileUpload1.FileName.ToLower().EndsWith(".bmp") || FileUpload1.FileName.ToLower().EndsWith(".png")) &&
                    FileUpload1.PostedFile.ContentLength <= 1048576)
                {
                    string path = @"~\Uploads\";
                    string savedPath = Server.MapPath(path) + FileUpload1.FileName; //better for saving into IIS
                    FileUpload1.SaveAs(savedPath);
                    ////To test image dimensions
                    //System.Drawing.Image i = System.Drawing.Image.FromFile(savedPath);
                    //if (i.PhysicalDimension.Height != 200 || i.PhysicalDimension.Width != 200)
                    //{
                    //    FileUpload1.Dispose();
                    //    i.Dispose();
                    //    File.Delete(savedPath);
                    //    throw new Exception("Incompatible dimensions. <br>Image must be 200px by 200px!");
                    //}
                    string chkQuery = DataProvider.LoggedInPage.getPictureAvailability((string)Session["User"]);
                    DataTable dt = HospitalClass.getDataTable(chkQuery);
                    string updateCode = "";
                    if (dt.Rows.Count > 0) { chkQuery = "update"; updateCode = "IMG_UPD"; }
                    else { chkQuery = "insert"; updateCode = "IMG_UPL"; }
                    string updaterId = UpdaterId();
                    List<string> values = new List<string>();
                    values.Add((string)Session["User"]);
                    values.Add(path + FileUpload1.FileName);
                    values.Add(HospitalClass.getTransactionId());
                    values.Add(updateCode);
                    values.Add(updaterId);
                    values.Add(chkQuery);
                    int status = DataConsumer.executeProcedure("image_upload", values);
                    string insertPicData = "update tobehospital.images set img=:image where user_id='" + (string)Session["User"] + "'";
                    status = DataConsumer.sendPictureToDatabase(insertPicData, FileUpload1.FileBytes);
                    ProfilePic.ImageUrl = path + FileUpload1.FileName;
                    UploadPicLabel.CssClass = "success";
                    UploadPicLabel.Text = "Done";
                }
                else
                {
                    UploadPicLabel.CssClass = "error";
                    if (!FileUpload1.HasFile) UploadPicLabel.Text = "Choose a picture";
                    else if (FileUpload1.PostedFile.ContentLength > 1048576) UploadPicLabel.Text = "Too Large";
                    else UploadPicLabel.Text = "Wrong Format";
                }
            }
            catch (Exception ex)
            {
                UploadPicLabel.CssClass = "error";
                UploadPicLabel.Text = "Error" + ex.Message;
                HospitalClass.Log(ex);
            }
        }

        protected void DownloadButton_Click(object sender, EventArgs e)
        {
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            string[] extArray = ProfilePic.ImageUrl.Split('.');
            string ext = extArray[extArray.Length - 1];
            response.Clear();
            response.AddHeader("Content-Disposition",
                               "attachment; filename= " + Session["User"].ToString() + "ProfilePicture." + ext + ";");
            response.TransmitFile(Server.MapPath(ProfilePic.ImageUrl));
            response.Flush();
            response.End();
            List<string> values = new List<string>();
            values.Add("IMG_DWL");
            values.Add(UpdaterId());
            values.Add(Session["User"].ToString());
            int status = DataConsumer.executeProcedure("audit_proc", values);
        }

        protected void DeletePicButton_Click(object sender, EventArgs e)
        {
            string chkQuery = DataProvider.LoggedInPage.getPicAddress((string)Session["User"]);
            DataTable dt = HospitalClass.getDataTable(chkQuery);
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "~\\Images\\UploadProfilePicture.PNG")
            {
                chkQuery = "delete";
                string updaterId = UpdaterId();
                //delete picture from server
                if (dt.Rows.Count == 1) System.IO.File.Delete(Server.MapPath(dt.Rows[0][0].ToString()));
                //update information at the database
                List<string> values = new List<string>();
                values.Add((string)Session["User"]);
                values.Add("~\\Images\\UploadProfilePicture.PNG");
                values.Add(HospitalClass.getTransactionId());
                values.Add("IMG_DEL");
                values.Add(updaterId);
                values.Add(chkQuery);
                int status = DataConsumer.executeProcedure("image_upload", values);
                ProfilePic.ImageUrl = "~\\Images\\UploadProfilePicture.PNG";
                UploadPicLabel.CssClass = "success";
                UploadPicLabel.Text = "Deleted";
            }
            else
            {
                UploadPicLabel.CssClass = "error";
                UploadPicLabel.Text = "No picture exists";
            }
        }

        public string UpdaterId()
        {
            string updaterId = "";
            if (Session["SuperUser"] != null) updaterId = (string)Session["SuperUser"];
            else if (Session["Admin"] != null) updaterId = (string)Session["Admin"];
            else updaterId = (string)Session["User"];
            return updaterId;
        }

    }
}
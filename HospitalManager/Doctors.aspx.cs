using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HospitalManager
{
    public partial class Doctors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string docQuery = DataProvider.GeneralClass.getDoctorsInfo();
            DataTable dt = HospitalClass.getDataTable(docQuery);
            //populate doctors table
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.Text = i + 1 + "";
                row.Cells.Add(cell);
                Image img = new Image();
                img.Width = (System.Web.UI.WebControls.Unit)150;
                img.Height = (System.Web.UI.WebControls.Unit)150;
                img.ID = "docImage" + (i + 1);
                if (!DBNull.Value.Equals(dt.Rows[i]["IMG_ADR"])) img.ImageUrl = dt.Rows[i]["IMG_ADR"].ToString();
                else img.ImageUrl = "~//images//UploadProfilePicture.png";
                cell = new TableCell();
                cell.Height = (System.Web.UI.WebControls.Unit)200;
                cell.Controls.Add(img);
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = "Name: " + HospitalClass.PascalCasing(dt.Rows[i]["LAST_NAME"].ToString()) + " " +HospitalClass.PascalCasing(dt.Rows[i]["FIRST_NAME"].ToString()) + 
                    ".<br/>" + "Mobile: " + dt.Rows[i]["PHONE_NO"].ToString() + ".<br/>E-mail: " + dt.Rows[i]["EMAIL"].ToString() + ".";
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = dt.Rows[i]["OTHER_INFO"].ToString();
                row.Cells.Add(cell);
                DocTable.Rows.Add(row);
            }
        }
    }
}
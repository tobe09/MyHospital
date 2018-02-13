using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Data;

namespace HospitalManager
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        //    if (Session["SuperUser"] == null)
        //    {
        //        Session["Info"] = "You do not have access to the requested page";
        //        Response.Redirect("LoggedinPage.aspx");
        //    }
            CreateTableAndSortUsingLinq();
        }

        private void CreateTableAndSortUsingLinq()
        {
            DataColumn dc1 = new DataColumn("Code", typeof(string));
            DataColumn dc2 = new DataColumn("Description", typeof(string));
            DataTable dt = new DataTable();
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Rows.Add(dt.NewRow());
            dt.Rows[0]["Code"] = "3";
            dt.Rows[0]["Description"] = "Three";
            dt.Rows.Add(dt.NewRow());
            dt.Rows[1]["Code"] = "1";
            dt.Rows[1]["Description"] = "One";
            dt.Rows.Add(dt.NewRow());
            dt.Rows[2]["Code"] = "2";
            dt.Rows[2]["Description"] = "Two";
            dt.Rows.Add(dt.NewRow());
            dt.Rows[3]["Code"] = "10";
            dt.Rows[3]["Description"] = "Ten";
            dt.AcceptChanges();

            DataTable dt2 = dt;
            DataView dv2 = dt2.AsDataView();
            dv2.Sort = "Code Asc";
            DataTable dt3 = dv2.ToTable();

            EnumerableRowCollection<DataRow> sortedTable = from row in dt.AsEnumerable()
                                                           orderby int.Parse(row.Field<string>("Code")) ascending
                                                           select row;
            dt = sortedTable.AsDataView().ToTable();

            dt2.Columns.Add("Sort", typeof(int));
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dt2.Rows[i]["Sort"] = int.Parse(dt2.Rows[i]["Code"].ToString());
            }
            dt2.AcceptChanges();
            dt2.AsDataView().Sort = "Sort";
            dt3 = dt2.AsDataView().ToTable();
            dv2 = dt2.AsDataView();
            dv2.Sort = "Sort asc";
            dt2 = dv2.ToTable();
            dt2.Columns.Remove("Sort");
            dt2.AcceptChanges();

            TestPage t = new TestPage();
            ddlTest.DataSource = dt;
            ddlTest.DataTextField = "Description";
            ddlTest.DataValueField = "Code";
            ddlTest.DataBind();
            ddlTest.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlTest.SelectedIndex = 0;
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (first.Text.Trim().Length > 4)
                {
                    List<string> values = new List<string>();
                    values.Add(date.Text.Trim());
                    values.Add(first.Text.Trim());
                    values.Add(last.Text.Trim());
                    values.Add("1");
                    int status = DataConsumer.executeProcedure("test_procedure", values);
                    statusLabel.CssClass = "success big";
                    statusLabel.Text = "Successful";
                }
                else
                {
                    statusLabel.CssClass = "error";
                    statusLabel.Text = "First name must have at least four characters";
                }
            }
            catch (Exception ex) 
            { 
                statusLabel.CssClass = "error big"; 
                statusLabel.Text = "Error: " + ex.Message; 
            }
        }

    }
}
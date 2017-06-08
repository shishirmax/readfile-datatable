using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ReadFile
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string fileExt = Path.GetExtension(FileUpload1.PostedFile.FileName);
            if (fileExt == ".csv")
            {
                string filepath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(filepath);

                //Add columns to Datatable to bind data
                DataTable dt = new DataTable("mtsnew");
                dt.Columns.AddRange(new DataColumn[8]
                {
                new DataColumn("DatePosted",typeof(string)),
                new DataColumn("TransactionRef",typeof(string)),
                new DataColumn("AttorneyDocket",typeof(string)),
                new DataColumn("Status",typeof(string)),
                new DataColumn("TransactionID",typeof(string)),
                new DataColumn("Type",typeof(string)),
                new DataColumn("TotalPayment",typeof(string)),
                new DataColumn("CustomerName",typeof(string)),
                });

                //Read all lines of the text file and close it.
                string data = File.ReadAllText(filepath);               
                //iterate over each row and split it to new line.
                foreach (string row in data.Split('\n'))
                {
                    //check for is null or empty row record
                    if (!string.IsNullOrEmpty(row))
                    {
                        //add rows                        
                        dt.Rows.Add();
                        int i = 0;
                        foreach (string cell in row.Split(','))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }                   
                }
                dt.Rows[0].Delete();
                //database connection string
                string mainconn = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection sqlconn = new SqlConnection(mainconn))
                {
                    //class use to bulk load data from another source
                    using (SqlBulkCopy sqlbkcpy = new SqlBulkCopy(sqlconn))
                    {
                        //set the db table name in which data to be added
                        sqlbkcpy.DestinationTableName = "mtsnew";
                        sqlconn.Open();
                        //copy all the rows from datatable to the destination db
                        sqlbkcpy.WriteToServer(dt);
                        sqlconn.Close();
                        lblMsg.Text = "Done...";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                }
            }
            else
            {
                lblMsg.Text = "Please Upload valid File with .csv Extension";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
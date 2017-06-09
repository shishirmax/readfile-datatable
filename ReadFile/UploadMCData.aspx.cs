using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReadFile
{
    public partial class UploadMCData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BtnMCData_Click(object sender, EventArgs e)
        {
            string fileExt = Path.GetExtension(MCFileUpload.PostedFile.FileName);
            if (fileExt == ".txt")
            {
                string filepath = Server.MapPath("~/Files/") + Path.GetFileName(MCFileUpload.PostedFile.FileName);
                MCFileUpload.SaveAs(filepath);

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[75]
                {
                    new DataColumn("REQUESTING_CONTROL_ACCOUNT",typeof(string)),
                    new DataColumn("BASIC_CONTROL_ACCOUNT",typeof(string)),
                    new DataColumn("CARDMEMBER_ACCOUNT_NUMBER",typeof(string)),
                    new DataColumn("SE_NUMBER",typeof(string)),
                    new DataColumn("ROC_ID",typeof(string)),
                    new DataColumn("DB_CR_INDICATOR",typeof(string)),
                    new DataColumn("TRANSACTION_TYPE_CODE",typeof(string)),
                    new DataColumn("FINANCIAL_CATEGORY",typeof(string)),
                    new DataColumn("BATCH_NUMBER",typeof(string)),
                    new DataColumn("DATE_OF_CHARGE",typeof(string)),
                    new DataColumn("LOCAL_CURRENCY_AMOUNT",typeof(string)),
                    new DataColumn("CURRENCY_CODE",typeof(string)),
                    new DataColumn("CAPTURE_DATE",typeof(string)),
                    new DataColumn("PROCESS_DATE",typeof(string)),
                    new DataColumn("BILLING_DATE",typeof(string)),
                    new DataColumn("BILLING_AMOUNT",typeof(string)),
                    new DataColumn("SALES_TAX_AMOUNT",typeof(string)),
                    new DataColumn("TIP_AMOUNT",typeof(string)),
                    new DataColumn("CARDMEMBER_NAME",typeof(string)),
                    new DataColumn("SPECIAL_BILL_IND",typeof(string)),
                    new DataColumn("ORIGINATING_BCA",typeof(string)),
                    new DataColumn("ORIGINATING_ACCOUNT_NUMBER",typeof(string)),
                    new DataColumn("CM_REFERENCE_NUMBER",typeof(string)),
                    new DataColumn("SUPPLIER_REFERENCE_NUMBER",typeof(string)),
                    new DataColumn("SHIP_TO_ZIP",typeof(string)),
                    new DataColumn("SIC_CODE",typeof(string)),
                    new DataColumn("COST_CENTER",typeof(string)),
                    new DataColumn("EMPLOYEE_ID",typeof(string)),
                    new DataColumn("SOCIAL_SECURITY_HASH_CODE",typeof(string)),
                    new DataColumn("UNIVERSALHASH_CODE",typeof(string)),
                    new DataColumn("STREET",typeof(string)),
                    new DataColumn("CITY",typeof(string)),
                    new DataColumn("STATE",typeof(string)),
                    new DataColumn("ZIP_PLUS__4",typeof(string)),
                    new DataColumn("TRANS_LIMIT",typeof(string)),
                    new DataColumn("MONTHLY_LIMIT",typeof(string)),
                    new DataColumn("EXPOSURE_LIMIT",typeof(string)),
                    new DataColumn("REV_CODE",typeof(string)),
                    new DataColumn("COMPANY_NAME",typeof(string)),
                    new DataColumn("CHARGE_DESCRIPTION_LINE1",typeof(string)),
                    new DataColumn("CHARGE_DESCRIPTION_LINE2",typeof(string)),
                    new DataColumn("CHARGE_DESCRIPTION_LINE3",typeof(string)),
                    new DataColumn("CHARGE_DESCRIPTION_LINE4",typeof(string)),
                    new DataColumn("CAR_RENTAL_CUSTOMER_NAME",typeof(string)),
                    new DataColumn("CAR_RENTAL_CITY",typeof(string)),
                    new DataColumn("CAR_RENTAL_STATE",typeof(string)),
                    new DataColumn("CAR_RENTAL_DATE",typeof(string)),
                    new DataColumn("CAR_RETURN_CITY",typeof(string)),
                    new DataColumn("CAR_RETURN_STATE",typeof(string)),
                    new DataColumn("CAR_RETURN_DATE",typeof(string)),
                    new DataColumn("CAR_RENTAL_DAYS",typeof(string)),
                    new DataColumn("HOTEL_ARRIVAL_DATE",typeof(string)),
                    new DataColumn("HOTEL_CITY",typeof(string)),
                    new DataColumn("HOTEL_STATE",typeof(string)),
                    new DataColumn("HOTEL_DEPART_DATE",typeof(string)),
                    new DataColumn("HOTEL_STAY_DURATION",typeof(string)),
                    new DataColumn("HOTEL_ROOM_RATE",typeof(string)),
                    new DataColumn("AIR_AGENCY_NUMBER",typeof(string)),
                    new DataColumn("AIR_TICKET_ISSUER",typeof(string)),
                    new DataColumn("AIR_CLASS_OF_SERVICE",typeof(string)),
                    new DataColumn("AIR_CARRIER_CODE",typeof(string)),
                    new DataColumn("AIR_ROUTING",typeof(string)),
                    new DataColumn("AIR_DEPARTURE_DATE",typeof(string)),
                    new DataColumn("AIR_PASSENGER_NAME",typeof(string)),
                    new DataColumn("TELE_DATE_OF_CALL",typeof(string)),
                    new DataColumn("TELE_FROM_CITY",typeof(string)),
                    new DataColumn("TELE_FROM_STATE",typeof(string)),
                    new DataColumn("TELE_CALL_LENGTH",typeof(string)),
                    new DataColumn("TELE_REFERENCE_NUMBER",typeof(string)),
                    new DataColumn("TELE_TIME_OF_CALL",typeof(string)),
                    new DataColumn("TELE_TO_NUMBER",typeof(string)),
                    new DataColumn("INDUSTRY_CODE",typeof(string)),
                    new DataColumn("SEQUENCE_NUMBER",typeof(string)),
                    new DataColumn("MERCATOR_KEY",typeof(string)),
                    new DataColumn("FEE_ALLOCATOR_IND",typeof(string)),
                });

                string data = File.ReadAllText(filepath);
                data = data.Replace("\r", "\n");
                foreach(string row in data.Split('\n'))
                {
                    if(!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;
                        foreach(string cell in row.Split(','))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Replace("\"", "");
                            i++;
                        }
                    }
                }
                dt.Rows[0].Delete();//delete first row of the file
                //dt.Rows[1].Delete();//delete second row of the file

                string conn = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection sqlcon = new SqlConnection(conn))
                {
                    using (SqlBulkCopy sqlbkcpy = new SqlBulkCopy(sqlcon))
                    {
                        sqlbkcpy.DestinationTableName = "MasterCard";
                        sqlcon.Open();
                        sqlbkcpy.WriteToServer(dt);
                        sqlcon.Close();
                        lblMsg.Text = "MasterCard File Imported into DataBase";
                        lblMsg.ForeColor = System.Drawing.Color.Green;                        
                    }
                }
            }
            else
            {
                lblMsg.Text = "Please upload valid file with .txt extension";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void ExportBtn_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_MasterCardUpdate;select * from MasterCard"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            string csv = string.Empty;

                            foreach(DataColumn column in dt.Columns)
                            {
                                csv += column.ColumnName + ',';
                            }
                            csv += "\r\n";
                            foreach(DataRow row in dt.Rows)
                            {
                                foreach(DataColumn column in dt.Columns)
                                {
                                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';

                                }
                                csv += "\r\n";
                            }
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment;filename=MasterCardFile.csv");
                            Response.Charset = "";
                            Response.ContentType = "spplication/text";
                            Response.Output.Write(csv);
                            Response.Flush();
                            Response.End();

                            lblMsg.Text = "Data Exported";
                            lblMsg.ForeColor = System.Drawing.Color.Aqua;
                        }
                    }
                }
            }
                
        }
    }
}
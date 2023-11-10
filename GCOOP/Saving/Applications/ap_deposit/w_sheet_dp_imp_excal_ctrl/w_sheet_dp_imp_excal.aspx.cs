using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
//////////////////////////////////////////
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OracleClient;
using System.Globalization;

/////////////////////////////////////////
using System.IO;
using System.Text;
using OfficeOpenXml;
/////////////////////////////////////////


namespace Saving.Applications.ap_deposit.w_sheet_dp_imp_excal_ctrl
{
    public partial class w_sheet_dp_imp_excal : PageWebSheet, WebSheet
    {

        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                string ls_month = "", ls_date = "";
                ls_date = state.SsWorkDate.Day.ToString();
                ls_month = state.SsWorkDate.Month.ToString();
                if (state.SsWorkDate.Month.ToString().Length < 2)
                {
                    ls_month = '0' + state.SsWorkDate.Month.ToString();
                }
                if (state.SsWorkDate.Day.ToString().Length < 2)
                {
                    ls_date = '0' + state.SsWorkDate.Day.ToString();
                }
                entry_date.Text = ls_date + "/" + ls_month + "/" + (state.SsWorkDate.Year + 543).ToString();
                type_code.Items.Insert(0, new ListItem("-----  เลือกรายการ  -----", ""));
                type_code.Items.Insert(1, new ListItem("DTR : ฝากเพื่อการโอนภายใน", "DTR"));
                type_code.Items.Insert(1, new ListItem("DTM : ฝากรายเดือน", "DTM"));
                type_code.Items.Insert(2, new ListItem("WTR : ถอนเพื่อการโอนภายใน", "WTR"));
                type_code.Items.Insert(2, new ListItem("WTC : ถอนเพื่อเงินฝากสงเคราะห์", "WTC"));
            }
        }

        protected void B_Delete_Click(object sender, EventArgs e)
        {
            if (type_code.SelectedValue != "")
            {
                string date_full = entry_date.Text.ToString().Split('/')[0].ToString() + entry_date.Text.ToString().Split('/')[1].ToString() + (Int32.Parse(entry_date.Text.ToString().Split('/')[2].ToString()) - 543).ToString();
                string year = entry_date.Text.ToString().Split('/')[2].ToString();
                try
                {
                    string sql = "delete from dpdepttran  where coop_id={0} and tran_year = {1} and tran_date = {2} and tran_status = '0' and system_code = '" + type_code.SelectedValue.ToString() + "'";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopId, year, state.SsWorkDate);
                    WebUtil.ExeSQL(sql);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลที่ Impost เสร็จสิ้น");
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลที่ Impost ไม่สำเร็จ");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณาเลือกประเภทรายการ');", true);
                return;
            }

        }
        public void CheckJsPostBack(string eventArg)
        {

        }

        public void SaveWebSheet()
        {
            string date_full = entry_date.Text.ToString().Split('/')[0].ToString() + entry_date.Text.ToString().Split('/')[1].ToString() + (Int32.Parse(entry_date.Text.ToString().Split('/')[2].ToString()) - 543).ToString();
            // date_full = 02072018

            string day_str = date_full.Substring(0, 2); // 02
            string mount_str = date_full.Substring(2, 2); //07
            string year_str = date_full.Substring(4, 4); //2018


            date_full = mount_str + '/' + day_str + '/' + year_str;


            string year = entry_date.Text.ToString().Split('/')[2].ToString();
            string error = "";

            if (type_code.SelectedValue != "")
            {
                if (txtInput.HasFile)
                {
                    if (System.IO.Path.GetExtension(txtInput.FileName) == ".xlsx" || System.IO.Path.GetExtension(txtInput.FileName) == ".xls")
                    {

                        ExecuteDataSource exe = new ExecuteDataSource(this);
                        string into = Server.MapPath("~/WSRPDF/") + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + txtInput.FileName;
                        txtInput.PostedFile.SaveAs(into);
                        FileInfo excel = new FileInfo(into);
                        using (var package = new ExcelPackage(excel))
                        {
                            var workbook = package.Workbook;
                            var worksheet = workbook.Worksheets.First();
                            string ls_deptno = ""; decimal ld_amountamt = 0;
                            string ls_coopid = state.SsCoopId;
                            string ls_deptitemtype = type_code.SelectedValue.ToString();
                            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                            {
                                try
                                {
                                    //ls_deptno = worksheet.Cells[i, 1].Text.ToString().Trim();
                                    try { ls_deptno = worksheet.Cells[i, 1].Text.ToString().Trim(); }
                                    catch
                                    { i = worksheet.Dimension.End.Row; continue; }
                    
                                    ld_amountamt = Convert.ToDecimal(worksheet.Cells[i, 2].Text);
                                    string sql = "insert into DPDEPTTRAN ( COOP_ID, DEPTACCOUNT_NO, MEMCOOP_ID, MEMBER_NO, SYSTEM_CODE, TRAN_YEAR, TRAN_DATE, SEQ_NO, DEPTITEM_AMT,TRAN_STATUS, BRANCH_OPERATE) values ";
                                    sql += "('" + ls_coopid + "','" + ls_deptno + "','" + ls_coopid + "',(select member_no from dpdeptmaster where  rtrim(ltrim(deptaccount_no)) = '" + ls_deptno + "'),'" + ls_deptitemtype + "','" + year + "',convert(datetime,'" + date_full + "'),";
                                    sql += "(select isnull(max(seq_no),0) +1 from dpdepttran where rtrim(ltrim(deptaccount_no)) = '" + ls_deptno + "' and  tran_date = convert(datetime,'" + date_full + "') and rtrim(ltrim(system_code)) = '" + ls_deptitemtype + "'),'" + ld_amountamt + "','0','000')";
                                    WebUtil.Query(sql);
                                }
                                catch
                                {
                                    error += ls_deptno + ",";
                                }
                            }
                        }
                        if (error.Trim().Length > 0) {
                            error = "แต่บัญชีที่ไม่ผ่านมีดังนี้ " + error;
                        }
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จสิ้น" + error);
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ต้องเป็น ไฟล์ .xlsx หรือ .xls เท่านั้น");
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("เลือกข้อมูลที่จะนำเข้า");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณาเลือกประเภทรายการ');", true);
                return;
            }
        }

        public void WebSheetLoadEnd()
        {


        }
    }
}
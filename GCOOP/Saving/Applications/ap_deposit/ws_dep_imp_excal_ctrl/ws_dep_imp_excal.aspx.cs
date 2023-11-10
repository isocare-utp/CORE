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
using System.IO;
using System.Text;
using OfficeOpenXml;


namespace Saving.Applications.ap_deposit.ws_dep_imp_excal_ctrl
{
    public partial class ws_dep_imp_excal : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String JsPostProcess { get; set; }
        [JsPostBack]
        public String JsPostDelete { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].entry_date = state.SsWorkDate;
                dsMain.DD_Recppaytype();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "JsPostProcess")
            {
                PostProcess();
            }
            else if (eventArg == "JsPostDelete")
            {
                PostDelete();
            }
        }
        private void PostProcess()
        {
            try
            {
                string ls_coopid = state.SsCoopId;
                DateTime entry_date = dsMain.DATA[0].entry_date;
                string ls_typecode = dsMain.DATA[0].type_code;
                int ln_year = state.SsWorkDate.Year+543;
                string error = "";

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
                            string ls_deptno = "";
                            decimal ld_amountamt = 0;
                            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                            {
                                try
                                {
                                    ls_deptno = worksheet.Cells[i, 1].Text.ToString().Trim();
                                    ld_amountamt = Convert.ToDecimal(worksheet.Cells[i, 2].Text.Trim());
                                    string insert = @"insert into DPDEPTTRAN 
                                ( COOP_ID, DEPTACCOUNT_NO,  MEMBER_NO, SYSTEM_CODE,  TRAN_DATE, SEQ_NO, DEPTITEM_AMT,TRAN_STATUS,MEMCOOP_ID,TRAN_YEAR,BRANCH_OPERATE) 
                                values ({0},{1},(select member_no from dpdeptmaster where rtrim(ltrim(deptaccount_no)) = {1}),{2},convert(datetime,{4}) "
                               + " ,(select isnull(max(seq_no),0) +1 from dpdepttran where rtrim(ltrim(deptaccount_no)) = {1} and  tran_date = convert(datetime,{4}) and rtrim(ltrim(system_code)) = {2}),{3},'0',{0},{5},'000')";
                                    insert = WebUtil.SQLFormat(insert, ls_coopid, ls_deptno, ls_typecode, ld_amountamt, entry_date, ln_year);
                                    WebUtil.ExeSQL(insert);
                                }
                                catch
                                {
                                    error += ls_deptno + ",";
                                }
                            }
                            if (error == "")
                            {
                                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จสิ้น");
                            }
                            else
                            {
                                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเลขบัญชีดังนี้ไม่สำเร็จ " + error);
                            }
                        }
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
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถ import ข้อมูลได้ " + ex);
            }
        }
        private void PostDelete()
        {
            try
            {
                DateTime entry_date = dsMain.DATA[0].entry_date;
                string ls_typecode = dsMain.DATA[0].type_code;
                string sql = "delete from dpdepttran  where coop_id={0} and tran_date = convert(datetime,{1}) and tran_status = 0 and system_code = {2}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, entry_date, ls_typecode);
                WebUtil.ExeSQL(sql);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลที่ Import เสร็จสิ้น");
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลที่ Import ไม่สำเร็จ");
            }
        }
        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {


        }
    }
}
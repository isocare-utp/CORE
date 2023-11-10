using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Saving.WcfCommon;
using Saving.WcfShrlon;
using System.IO;
using System.Data;
using DataLibrary;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Services.Protocols;
using Saving.WcfReport;

namespace Saving.Applications.keeping.w_sheet_sl_update_data_from_disk_ctrl
{
    public partial class w_sheet_sl_update_data_from_disk : PageWebSheet, WebSheet
    {
        //private ShrlonClient shrlonService;

        [JsPostBack]
        public string postDatatxt { get; set; }
        [JsPostBack]
        public string ImportDatatxt { get; set; }
        [JsPostBack]
        public string ClearDatatxt { get; set; }
        [JsPostBack]
        public string LoadDatatxt { get; set; }
        [JsPostBack]
        public string UpdateFileData { get; set; }
        
        public void InitJsPostBack()
        {
            dsFilepath.InitDsFilepath(this);
            dwlist.InitDwlist(this);
        }

        public void WebSheetLoadBegin()
        {
            //try
            //{
            //    shrlonService = wcf.Shrlon;
            //}
            //catch
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
            //    return;
            //}
            //this.ConnectSQLCA();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDatatxt")
            {
                JsPostDatatxt();
            }
            else if (eventArg == "ImportDatatxt")
            {
                JsImportDatatxt();
            }
            else if (eventArg == "ClearDatatxt")
            {
                dsFilepath.ResetRow();
                dwlist.ResetRow();
                File_Name.Text = "";
            }
            else if (eventArg == "LoadDatatxt")
            {
                Runreport();
            }
            else if (eventArg == "UpdateFileData")
            {
                DbUpdateFileData();
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }

        //อ่านข้อมูลไฟล์ txt 
        public void JsPostDatatxt()
        {
            ExecuteDataSource bx = new ExecuteDataSource(this);
            ExecuteDataSource del = new ExecuteDataSource(this);

            try
            {
                dwlist.ResetRow();

                FileUpload fu = txtInput;
                string filename = txtInput.FileName.ToString().Trim();
                File_Name.Text = filename;
                Stream stream = fu.PostedFile.InputStream;
                byte[] b = new byte[stream.Length];
                stream.Read(b, 0, (int)stream.Length);
                string txt = Encoding.GetEncoding("TIS-620").GetString(b); //sr.ReadToEnd();
                txt = Regex.Replace(txt, "\r", "");
                string[] lines = Regex.Split(txt, "\n");
                int txtLength, row = 0;

                decimal itemtype_code, item_amt, salary_amount, electrictcity_amt;
                string salary_id, member_no, contract_type, membgroup_code, period, bank_code, bank_accid;
                DateTime imp_date;

                foreach (string line in lines)
                {
                    txtLength = line.Length;

                    if (txtLength > 3)
                    {
                        //init value
                        salary_id = line.Substring(63, 6);
                        member_no = line.Substring(6, 6);
                        member_no = WebUtil.MemberNoFormat(member_no);
                        itemtype_code = Convert.ToDecimal(line.Substring(12, 2));
                        contract_type = line.Substring(14, 1);
                        item_amt = (Convert.ToDecimal(line.Substring(15, 10))) / 100;
                        membgroup_code = line.Substring(25, 14);
                        period = line.Substring(39, 4);
                        salary_amount = Convert.ToDecimal(line.Substring(43, 6));
                        electrictcity_amt = (Convert.ToDecimal(line.Substring(49, 7))) / 100;
                        bank_code = line.Substring(56, 3);
                        bank_accid = line.Substring(59, 10);
                        imp_date = state.SsWorkDate;

                        string sql_chkimpfile = @"select * from kptempimport where member_no = {0} and itemtype_code = {1} and contract_type = {2} and period = {3}";
                        sql_chkimpfile = WebUtil.SQLFormat(sql_chkimpfile, member_no, itemtype_code, contract_type, period);
                        Sdt dtBN = WebUtil.QuerySdt(sql_chkimpfile);

                        if (dtBN.Next())
                        {
                            string sql_chkimpfilepost = @"select * from kptempimport where member_no = {0} and itemtype_code = {1} and contract_type = {2} and period = {3} and imp_status = '0'";
                            sql_chkimpfilepost = WebUtil.SQLFormat(sql_chkimpfilepost, member_no, itemtype_code, contract_type, period);
                            Sdt dtps = WebUtil.QuerySdt(sql_chkimpfilepost);

                            if (dtps.Next())
                            {
                                string sql_delkptempimp = @"delete from kptempimport where member_no = {0} and itemtype_code = {1} and contract_type = {2} and period = {3} and imp_status = '0'";
                                object[] argslist_delkptempimp = new object[] { member_no, itemtype_code, contract_type, period };

                                sql_delkptempimp = WebUtil.SQLFormat(sql_delkptempimp, argslist_delkptempimp);
                                del.SQL.Add(sql_delkptempimp);

                                string sql_inskptempimp = @"insert into kptempimport(salary_id, member_no, itemtype_code, contract_type, item_amt, membgroup_code, period,
                                salary_amount, electrictcity_amt, bank_code, bank_accid, file_name, imp_date) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})";
                                object[] argslist_dpdepttran = new object[] { salary_id, member_no, itemtype_code, contract_type, item_amt, membgroup_code, period, 
                                salary_amount, electrictcity_amt, bank_code, bank_accid, filename, imp_date };

                                sql_inskptempimp = WebUtil.SQLFormat(sql_inskptempimp, argslist_dpdepttran);
                                bx.SQL.Add(sql_inskptempimp);
                            }
                        }
                        else
                        {
                            string sql_inskptempimp = @"insert into kptempimport(salary_id, member_no, itemtype_code, contract_type, item_amt, membgroup_code, period,
                            salary_amount, electrictcity_amt, bank_code, bank_accid, file_name, imp_date) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})";
                            object[] argslist_dpdepttran = new object[] { salary_id, member_no, itemtype_code, contract_type, item_amt, membgroup_code, period, 
                            salary_amount, electrictcity_amt, bank_code, bank_accid, filename, imp_date };

                            sql_inskptempimp = WebUtil.SQLFormat(sql_inskptempimp, argslist_dpdepttran);
                            bx.SQL.Add(sql_inskptempimp);
                        }
                    }

                    row++;
                }

                del.Execute();
                del.SQL.Clear();

                bx.Execute();
                bx.SQL.Clear();

                ChkDatatxt();

                LtServerMessage.Text = WebUtil.CompleteMessage("ทำรายการเรียบร้อย");
            }
            catch (Exception eX)
            {
                //เอาไว้ดู err เวลามีปัญหาตอน import
                LtServerMessage.Text = WebUtil.ErrorMessage(eX);
            }
        }

        public void DbUpdateFileData()
        {
            string KPRECEIPTNO = Get_NumberDOC("KPRECEIPTNO");


        }

        //เช็คจำนวนข้อมูล
        public void ChkDatatxt()
        {
            DateTime imp_getdate = state.SsWorkDate;
            string imp_date = (imp_getdate.Year + 543).ToString() + imp_getdate.Month.ToString("00");
            imp_date = imp_date.Substring(2,4);

            try
            {
                string sql_chkimpfile = @"select count(distinct member_no) as membcount, count(*) as rowcount from kptempimport where file_name = {0} and period = {1}";
                sql_chkimpfile = WebUtil.SQLFormat(sql_chkimpfile, File_Name.Text, imp_date);
                Sdt dtBN = WebUtil.QuerySdt(sql_chkimpfile);

                if (dtBN.Next())
                {
                    dsFilepath.DATA[0].readdata_count = dtBN.GetDecimal("rowcount");
                    dsFilepath.DATA[0].readmem_count = dtBN.GetDecimal("membcount");
                }

                string sql_chkimpfilepost = @"select count(distinct member_no) as membcount, count(*) as rowcount from kptempimport where file_name = {0} and period = {1} and imp_status = 1";
                sql_chkimpfilepost = WebUtil.SQLFormat(sql_chkimpfilepost, File_Name.Text, imp_date);
                Sdt dtps = WebUtil.QuerySdt(sql_chkimpfilepost);

                if (dtps.Next())
                {
                    dsFilepath.DATA[0].writedata_count = dtps.GetDecimal("rowcount");
                    dsFilepath.DATA[0].writemem_count = dtps.GetDecimal("membcount");
                }
            }
            catch (Exception eX)
            {
                //เอาไว้ดู err เวลามีปัญหาตอน import
                LtServerMessage.Text = WebUtil.ErrorMessage(eX);
            }
        }

        //Funtion นี้เอาไว้ get เลขเอกสารต่างๆ รายละเอียดดูใน table cmdocumentcontrol
        public string Get_NumberDOC(string typecode)
        {
            string coop_id = state.SsCoopControl;
            Sta ta = new Sta(state.SsConnectionString);
            string postNumber = "";
            try
            {
                ta.AddInParameter("AVC_COOPID", coop_id, System.Data.OracleClient.OracleType.VarChar);
                ta.AddInParameter("AVC_DOCCODE", typecode, System.Data.OracleClient.OracleType.VarChar);
                ta.AddReturnParameter("return_value", System.Data.OracleClient.OracleType.VarChar);
                ta.ExePlSql("N_PK_DOCCONTROL.OF_GETNEWDOCNO");
                postNumber = ta.OutParameter("return_value").ToString();
                ta.Close();
            }
            catch
            {
                ta.Close();
            }
            return postNumber.ToString();
        }

        public void JsImportDatatxt()
        {
            try
            {

            }
            catch (Exception eX)
            {
                //เอาไว้ดู err เวลามีปัญหาตอน import
                LtServerMessage.Text = WebUtil.ErrorMessage(eX);
            }
        }

        public void Runreport()
        {
            DateTime imp_date = state.SsWorkDate;

            //Add Arg[]
            iReportArgument args = new iReportArgument();
            iReportBuider report = new iReportBuider(this, "กำลังสร้างรายงาน");

            args.Add("as_filename", iReportArgumentType.String, File_Name.Text);
            args.Add("as_impdate", iReportArgumentType.Date, imp_date);

            report.AddCriteria("r_kp_importfile", "รายงาน importfile (ทั้งหมด)", ReportType.pdf, args);

            report.AutoOpenPDF = true;
            report.Retrieve();
        }
    }
}
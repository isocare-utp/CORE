using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Criteria
{
    public partial class u_cri_npl_beforethreemonth : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String JsPostRangeType;
        private DwThDate tdw_criteria;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            JsPostRangeType = WebUtil.JsPostBack(this, "JsPostRangeType");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
        }

        public void WebSheetLoadBegin()
        {
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_criteria);
            }
            else
            {
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDecimal(1, "year_th", state.SsWorkDate.Year + 543);
                dw_criteria.SetItemDecimal(1, "month", state.SsWorkDate.Month);
                for (int i = 0; i <= 7; i++)
                {
                    dw_criteria.SetItemDecimal(1, "lawtype_" + i, 0);
                }
                for (int i = 1; i <= 4; i++)
                {
                    dw_criteria.SetItemDecimal(1, "lawtype_" + i, 1);
                }
                //dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate);
                //dw_criteria.SetItemDateTime(1, "end_date", state.SsWorkDate);
                //tdw_criteria.Eng2ThaiAllRow();
                //DwUtil.RetrieveDDDW(dw_criteria, "coop_id", "criteria.pbl", state.SsCoopControl);
                //dw_criteria.SetItemString(1, "coop_id", state.SsCoopControl);
            }


            //--- Page Arguments
            try
            {
                app = Request["app"].ToString();
            }
            catch { }
            if (app == null || app == "")
            {
                app = state.SsApplication;
            }
            try
            {
                gid = Request["gid"].ToString();
            }
            catch { }
            try
            {
                rid = Request["rid"].ToString();
            }
            catch { }

            //Report Name.
            try
            {
                Sta ta = new Sta(state.SsConnectionString);
                String sql = "";
                sql = @"SELECT REPORT_NAME  
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
                Sdt dt = ta.Query(sql);
                ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }

            //Link back to the report menu.
            LinkBack.PostBackUrl = String.Format("~/ReportDefault.aspx?app={0}&gid={1}", app, gid);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "runProcess")
            {
                RunProcess();
            }
            else if (eventArg == "JsPostRangeType")
            {
                decimal range_type = dw_criteria.GetItemDecimal(1, "range_type");

                if (range_type == 1)
                {
                    dw_criteria.SetItemDecimal(1, "range_type", 2);
                }
                else if (range_type == 2)
                {
                    dw_criteria.SetItemDecimal(1, "range_type", 1);
                }
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
        }

        #endregion

        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            decimal year_th = 0, month = 0;
            string array = "";

            try
            {
                year_th = dw_criteria.GetItemDecimal(1, "year_th");
            }
            catch { }
            try
            {
                month = dw_criteria.GetItemDecimal(1, "month");
            }
            catch { }
            for (int i = 0; i <= 7; i++)
            {
                //lawtype_
                //lawtype_
                if (dw_criteria.GetItemDecimal(1, "lawtype_" + i) == 1)
                {
                    array += "," + i;
                }
            }

            if (array.Length > 1)
            {
                array = array.Substring(1);
            }

            //ยิงเข้า db ก่อน
            InsertDataReport(array, month, year_th);

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(state.SsCoopControl, ArgumentType.String);
            lnv_helper.AddArgument(month.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(year_th.ToString(), ArgumentType.Number);
            //lnv_helper.AddArgument(array, ArgumentType.String);

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();
            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                String criteriaXML = lnv_helper.PopArgumentsXML();
                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;

                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);

                //String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
                //if (li_return == "true")
                //{
                //    HdOpenIFrame.Value = "True";
                //}
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }

        }

        public void PopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdf + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }

        private void InsertDataReport(string lawtypes, decimal mm, decimal yyyyTH)
        {
            string sql = @"select 
                lnnplmaster.member_no,   
                lnnplmaster.loancontract_no, 
                mbmembmaster.memb_name,   
                mbmembmaster.memb_surname,
                lnnplmaster.lawtype_code,   
                lnucfnpllawtype.lawtype_desc,   
                mbucfprename.prename_desc,   
                lncontmaster.period_payment,   
                lncontmaster.principal_balance,   
                lnnplmaster.debtor_class,   
                lnnplmaster.int_lastcal  ,
                lnnplmaster.period_payment,
                lnnplmaster.status ,
                lncontmaster.lastpayment_date,
                lncontmaster.contract_status,
                lnnplmaster.work_order,
                n_pk_lnnpl.of_getpaystmt(lnnplmaster.coop_id, lnnplmaster.loancontract_no, 2, {1}, {2}) as cmp_two,
                n_pk_lnnpl.of_getpaystmt(lnnplmaster.coop_id, lnnplmaster.loancontract_no, 1, {1}, {2}) as cmp_one,
                n_pk_lnnpl.of_getpaystmt(lnnplmaster.coop_id, lnnplmaster.loancontract_no, 0, {1}, {2}) as cmp_prn,
                n_pk_lnnpl.of_getpaystmt(lnnplmaster.coop_id, lnnplmaster.loancontract_no, -1, {1}, {2}) as cmp_int
            from 
                lnnplmaster,   
                lncontmaster,   
                mbmembmaster,   
                lnucfnpllawtype,   
                mbucfprename  
            where 
                lnnplmaster.coop_id = lncontmaster.coop_id and
                lnnplmaster.coop_id = mbmembmaster.coop_id and
                lnnplmaster.coop_id = lnucfnpllawtype.coop_id and
                lnnplmaster.loancontract_no = lncontmaster.loancontract_no and  
                lnnplmaster.member_no = mbmembmaster.member_no and  
                lnnplmaster.lawtype_code = lnucfnpllawtype.lawtype_code and  
                mbmembmaster.prename_code = mbucfprename.prename_code and
                lnnplmaster.coop_id = {0} and
                lnnplmaster.lawtype_code in (" + lawtypes + @") and
                (lnnplmaster.debtor_class < 'C' or lncontmaster.principal_balance <= 0)
                and lnnplmaster.debtor_class <> 'D'
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, mm, yyyyTH);
            Sta ta = new Sta(state.SsConnectionString);
            ta.Transection();
            try
            {
                ta.Exe("DELETE FROM LNNPLTEMPREPORT");
                Sdt dt = ta.Query(sql);
                int ii = 0;
                while (dt.Next())
                {
                    decimal intEstimate = 0;
                    try
                    {
                        DateTime dtEst = new DateTime(Convert.ToInt32(yyyyTH) - 543, Convert.ToInt32(mm), 1);
                        intEstimate = wcf.NShrlon.of_computeinterest(state.SsWsPass, state.SsCoopControl, dt.GetString("loancontract_no"), dtEst);
                    }
                    catch { }
                    object[] arrg = new object[15];
                    arrg[0] = dt.GetString("member_no");
                    arrg[1] = dt.GetString("loancontract_no");
                    arrg[2] = dt.GetString("prename_desc") + dt.GetString("memb_name") + " " + dt.GetString("memb_surname");
                    arrg[3] = dt.GetDecimal("period_payment");
                    arrg[4] = dt.GetDecimal("cmp_two"); ;
                    arrg[5] = dt.GetDecimal("cmp_one");
                    arrg[6] = dt.GetDecimal("cmp_prn");
                    arrg[7] = dt.GetDecimal("cmp_int");
                    arrg[8] = dt.GetDecimal("principal_balance");
                    arrg[9] = intEstimate;
                    arrg[10] = dt.GetDecimal("status");
                    arrg[11] = dt.GetDecimal("lawtype_code");
                    arrg[12] = dt.GetString("lawtype_desc");
                    arrg[13] = dt.GetString("debtor_class");
                    arrg[14] = state.SsCoopControl;
                    //arrg[15] = dt.GetString("work_order");
                    string sqlInsert = @"insert into LNNPLTEMPREPORT(
                        MEMBER_NO,          LOANCONTRACT_NO,        MEMBER_FULLNAME,
                        PERIOD_PAYMENT,     BEFORE_TWO,             BEFORE_ONE,
                        LAST_PRN_PAYMENT,   LAST_INT_PAYMENT,       PRINCIPAL_BALANCE,
                        INT_ESTIMATE,       PAY_BY,                 LAWTYPE_CODE,
                        LAWTYPE_DESC,       DEBTOR_CLASS,           COOP_ID
                    ) values (
                        {0},                {1},                    {2},
                        {3},                {4},                    {5},
                        {6},                {7},                    {8},
                        {9},                {10},                   {11},
                        {12},               {13},                   {14}
                    )";
                    sqlInsert = WebUtil.SQLFormat(sqlInsert, arrg);
                    ta.Exe(sqlInsert);

                    ii++;
                }
                ta.Commit();
                ta.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    ta.RollBack();
                }
                catch { }
                ta.Close();
                throw ex;
            }
        }
    }
}

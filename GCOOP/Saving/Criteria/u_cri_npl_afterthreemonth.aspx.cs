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
    public partial class u_cri_npl_afterthreemonth : PageWebSheet, WebSheet
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
            InsertDataReport(array);

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(state.SsCoopControl, ArgumentType.String);

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
                //String li_return = lws_report.Run(state.SsWsPass, app, gid, rid, criteriaXML, pdfFileName);
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

        private void InsertDataReport(string lawtypes)
        {
            decimal yyyy = dw_criteria.GetItemDecimal(1, "year_th");
            decimal mm = dw_criteria.GetItemDecimal(1, "month");
            DateTime dateCal = new DateTime(Convert.ToInt32(yyyy) - 543, Convert.ToInt32(mm), 1);
            string sql = @"SELECT 
              d.PRENAME_DESC,
              c.MEMB_NAME,
              c.MEMB_SURNAME,
              a.LOANCONTRACT_NO,
              trim( substr( a.LOANCONTRACT_NO , 1 , 9 ) ) AS LOAN2,
              a.PRINC_BALANCE,
              a.INT_LASTCAL, 
              b.LASTPAYMENT_DATE,
              a.REMARK,
              b.STARTCONT_DATE,
              a.WORK_ORDER,
              a.JUDGE_DATE,   
              b.PRINCIPAL_BALANCE,   
              e.MEMBGROUP_DESC,   
              e.MEMBGROUP_CODE,   
              f.RESIGNCAUSE_CODE,   
              f.RESIGNCAUSE_DESC,
              a.MEMBER_NO,   
              a.LAWTYPE_CODE,
              g.LAWTYPE_DESC,
              n_pk_lnnpl.of_diff_year_month({1}, decode(a.lawtype_code, 4, a.JUDGE_DATE, b.LASTPAYMENT_DATE)) as loan_age,
              n_pk_lnnpl.of_getcollmast ({0}, a.LOANCONTRACT_NO) as collmast
            FROM lnnplmaster a
              inner join lncontmaster b on a.coop_id = b.coop_id and  a.loancontract_no = b.loancontract_no
              inner join mbmembmaster c on a.coop_id = c.coop_id and a.member_no = c.member_no
              inner join mbucfprename d on c.prename_code = d.prename_code
              inner join mbucfmembgroup e on a.coop_id = e.coop_id and c.membgroup_code = e.membgroup_code
              inner join mbucfresigncause f on c.resigncause_code = f.resigncause_code
              inner join lnucfnpllawtype g on a.coop_id = g.coop_id and a.lawtype_code = g.lawtype_code
            WHERE 
              a.coop_id = {0} and
              a.lawtype_code in (" + lawtypes + @") and
              a.debtor_class = 'C' and
              b.principal_balance > 0
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dateCal);
            Sta ta = new Sta(state.SsConnectionString);
            ta.Transection();
            try
            {
                ta.Exe("DELETE FROM LNNPLTEMPREPORT_III");
                Sdt dt = ta.Query(sql);
                int ii = 0;
                while (dt.Next())
                {
                    decimal intEstimate = 0;
                    try
                    {

                        //DateTime dtEst = new DateTime(Convert.ToInt32(yyyy) - 543, Convert.ToInt32(mm), 1);
                        intEstimate = wcf.NShrlon.of_computeinterest(state.SsWsPass, state.SsCoopControl, dt.GetString("loancontract_no"), dateCal);
                    }
                    catch { }

                    object[] arrg = new object[13];
                    arrg[0] = dt.GetString("loancontract_no");
                    arrg[1] = dt.GetString("member_no");
                    arrg[2] = dt.GetString("prename_desc") + dt.GetString("memb_name") + " " + dt.GetString("memb_surname");
                    arrg[3] = dt.GetDecimal("principal_balance");
                    arrg[4] = intEstimate;// dt.GetDecimal("advance_int"); ;
                    arrg[5] = dt.GetDate("LASTPAYMENT_DATE");
                    arrg[6] = dt.GetString("WORK_ORDER") == "" ? "00" : dt.GetString("WORK_ORDER");
                    arrg[7] = dt.GetString("collmast");
                    arrg[8] = dt.GetString("remark");
                    arrg[9] = dt.GetDecimal("loan_age");
                    arrg[10] = dt.GetDecimal("lawtype_code");
                    arrg[11] = dt.GetString("lawtype_desc");
                    arrg[12] = state.SsCoopControl;
                    string sqlInsert = @"INSERT INTO LNNPLTEMPREPORT_III(
                        loancontract_no,    member_no,      fullname,
                        principal_balance,  advance_int,    last_payment,
                        work_step,          collmast,       remark, 
                        loan_age,           lawtype_code,   lawtype_desc,
                        coop_id
                    ) VALUES (
                        {0},                {1},            {2},
                        {3},                {4},            {5},
                        {6},                {7},            {8},
                        {9},                {10},           {11},
                        {12}
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

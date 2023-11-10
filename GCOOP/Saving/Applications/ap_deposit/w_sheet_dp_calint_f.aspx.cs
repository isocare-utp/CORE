using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
//using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNCommon; //new common
using System.Globalization;
using Sybase.DataWindow;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit;    //new deposit
using System.Web.Services.Protocols;
using DataLibrary;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_calint_f : PageWebSheet, WebSheet
    {
        protected string PostRetrive;
        protected string PostRetriveDetail;
        protected string PostGenInt;
        protected string PostData;
        protected string PostDelete;
        protected string PostReport;
        protected string PostAddrow;

        //private DepositClient depServ;
        private n_depositClient ndept;
        private String pblFileName = "dept_calint_f.pbl";
        private DwThDate thDwMain;
        private DwThDate thDwlist;
        public void InitJsPostBack()
        {
            PostRetrive = WebUtil.JsPostBack(this, "PostRetrive");
            PostRetriveDetail = WebUtil.JsPostBack(this, "PostRetriveDetail");
            PostGenInt = WebUtil.JsPostBack(this, "PostGenInt");
            PostData = WebUtil.JsPostBack(this, "PostData");
            PostDelete = WebUtil.JsPostBack(this, "PostDelete");
            PostReport = WebUtil.JsPostBack(this, "PostReport");
            PostAddrow = WebUtil.JsPostBack(this, "PostAddrow");

            thDwMain = new DwThDate(DwMain, this);
            thDwMain.Add("lastcalint_date", "lastcalint_tdate");
            thDwlist = new DwThDate(DwList, this);
            thDwlist.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            { ndept = wcf.NDeposit; }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }

            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
              //  DwList.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostRetrive")
            {
                JsRetrive();
            }
            else if (eventArg == "PostGenInt")
            {
                JsOfGenint();
            }
            else if (eventArg == "PostData")
            {
                String deptaccount_no = "";
                try { deptaccount_no = DwMain.GetItemString(1, "deptaccount_no"); }
                catch { deptaccount_no = ""; }
                JsRetriveData(deptaccount_no);
            }
            else if (eventArg == "PostDelete")
            {
                JsDelete();
            }
            else if (eventArg == "PostAddrow")
            {
                JsAddrow();
            }
            else if (eventArg == "PostReport")
            {
                JsCallIReport();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwList.SaveDataCache();
        }
        #region WebSheet Members

        public void JsRetrive()
        {
            String accountno = Hd_deptaccountno.Value;
            //accountno = depServ.BaseFormatAccountNo(state.SsWsPass, accountno);           
              accountno = wcf.NDeposit.of_analizeaccno(state.SsWsPass, accountno);
            DwUtil.RetrieveDataWindow(DwMain, pblFileName, null, accountno, state.SsCoopId);
            //DwMain.SetItemDateTime(DwMain.RowCount, "lastcalint_date",);
            thDwMain.Eng2ThaiAllRow();
        }

        //ปุ่มคำนวณ
        public void JsOfGenint()
        {
            try
            {
                thDwlist.Thai2EngAllRow();
                String as_xml_report = DwList.Describe("datawindow.data.xml");
                String as_xml_headdata = DwMain.Describe("datawindow.data.xml");

                //int i = depServ.OfGenintF(state.SsWsPass, as_xml_headdata, ref as_xml_report);

                int i = ndept.of_genint_f(state.SsWsPass, as_xml_headdata, ref as_xml_report);

                DwUtil.ImportData(as_xml_report, DwList, thDwlist, FileSaveAsType.Xml);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        //ปุ่มดึงข้อมูล
        public void JsRetriveData(String deptaccount_no)
        {
            String as_xml_statement = "";
            //int i = depServ.OfIntiStmF(state.SsWsPass, deptaccount_no, ref as_xml_statement);
            int i = ndept.of_init_statement_f(state.SsWsPass, deptaccount_no, ref as_xml_statement);
            DwUtil.ImportData(as_xml_statement, DwList, thDwlist, FileSaveAsType.Xml);
        }

        //ปุ่มพิมพ์
        public void JsCallIReport()
        {
            try
            {
                String as_xml_report = "";
                String as_xml_main = DwMain.Describe("datawindow.data.xml");
                String as_xml_detail = DwList.Describe("datawindow.data.xml");
                //int i = depServ.OfPrintStmF(state.SsWsPass, as_xml_main, as_xml_detail, ref as_xml_report);
                int i = ndept.of_print_statement_f(state.SsWsPass, as_xml_main, as_xml_detail, ref as_xml_report);

                //Code for call IReport
                //
                string deptacc_no = DwMain.GetItemString(1, "deptaccount_no");
                iReportArgument arg = new iReportArgument();

                //  arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("as_deptaccount", iReportArgumentType.String, deptacc_no);
                iReportBuider report = new iReportBuider(this, "กำลังสร้างรายงาน");
                report.AddCriteria("d_dp_correct_statement_detial_ext_report", "รายงานจำลองดอกเบี้ย", ReportType.pdf, arg);
                report.Retrieve();
            }
            catch (Exception ex) {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void JsDelete()
        {
            try
            {
                DwList.DeleteRow(int.Parse(HdListRow.Value));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void JsAddrow()
        {
            decimal lastSeq = 0;
            try
            {
                lastSeq = DwList.GetItemDecimal(DwList.RowCount, "seq_no");
            }
            catch
            {
                string sql = "select max(seq_no) as maxseq from dpdeptstatement where deptaccount_no = '" + DwMain.GetItemString(1, "deptaccount_no") + "'";
                DataTable dt = new DataTable();
                dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    lastSeq = Convert.ToDecimal(dt.Rows[0]["maxseq"].ToString());
                }
            }
            DwList.InsertRow(0);
            DwList.SetItemDecimal(DwList.RowCount, "seq_no", lastSeq+1);
        }
        #endregion
    }
}
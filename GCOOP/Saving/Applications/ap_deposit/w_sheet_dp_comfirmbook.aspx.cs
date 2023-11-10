using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit; //new deposit
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_comfirmbook : PageWebSheet, WebSheet
    {
        //private DepositClient depServ;
        private n_depositClient ndept; // new deposit
        protected String PostChkMember;
        protected String PostData;

        private String pblFileName = "deposit.pbl";


        public void InitJsPostBack()
        {
            PostChkMember = WebUtil.JsPostBack(this, "PostChkMember");
            PostData = WebUtil.JsPostBack(this, "PostData");
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
                DwUtil.RetrieveDDDW(DwMain, "name_apv", pblFileName, state.SsCoopControl);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostData")
            {
                JsReport();
            }
            else if (eventArg == "PostChkMember")
            {

                String member_no = "";
                try { member_no = DwMain.GetItemString(1, "member_no"); }
                catch { member_no = ""; }
                member_no = WebUtil.MemberNoFormat(member_no);
                DwMain.SetItemString(1, "member_no", member_no);
                JsMember(member_no);
            }

        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        private void JsNewClear()
        {
            DwMain.Reset();
        }

        private void JsReport()
        {

            String as_xml_report = "";
            String as_xml_headdata = DwMain.Describe("datawindow.data.xml");
            ndept.of_comfirmbook(state.SsWsPass, as_xml_headdata, ref as_xml_report);

            //if (as_xml_report != "")
            //{
            //    Printing.PrintApplet(this, "dept_confirm", as_xml_report);
            //}
            iReportArgument arg = new iReportArgument();

            arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);

            iReportBuider report = new iReportBuider(this, "กำลังสร้างรายงาน");
            report.AddCriteria("report_dp_confirm", "หนังสือรับรอง", ReportType.pdf, arg);
            report.Retrieve();
        }

        private void JsMember(String member_no)
        {
            String name_to = "";
            String sql = @"select p.prename_desc || ' ' || m.memb_name || '  ' || m.memb_surname as name_to
                        from mbmembmaster m , mbucfprename p
                        where m.prename_code = p.prename_code and
                        m.coop_id = '" + state.SsCoopControl + @"' and
                        m.member_no = '" + member_no + "' ";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                name_to = dt.GetString("name_to");
            }

            DwMain.SetItemString(1, "name_to", name_to);
        }
    }
}
using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Saving.Applications.shrlon.ws_sl_cfinttable_ctrl
{
    public partial class ws_sl_cfinttable : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostLoanintrateCode { get; set; }
        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }
        [JsPostBack]
        public String PostInsertNew { get; set; }
        [JsPostBack]
        public String PrintReport { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
            dsList.InitDsList(this);
            dsNew.InitDsNew(this);
        }

        public void WebSheetLoadBegin()
        {
            dsMain.Retrieve();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostLoanintrateCode")
            {
                int r = dsMain.GetRowFocus();
                string loanintrate_code = dsMain.DATA[r].LOANINTRATE_CODE;
                dsDetail.Retrieve(loanintrate_code);
                dsList.Retrieve(loanintrate_code);

                dsMain.FindTextBox(r, "loanintrate_code").BackColor = Color.SkyBlue;
                dsMain.FindTextBox(r, "loanintrate_desc").BackColor = Color.SkyBlue;

            }
            else if (eventArg == "PostInsertRow")
            {
                dsList.InsertLastRow();

                int row = dsList.RowCount - 1;
                dsList.DATA[row].COOP_ID = state.SsCoopControl;
                dsList.DATA[row].LOANINTRATE_CODE = dsDetail.DATA[0].LOANINTRATE_CODE;
            }
            else if (eventArg == "PostDeleteRow")
            {
                dsList.DeleteRow(dsList.GetRowFocus());
            }
            else if (eventArg == "PostInsertNew")
            {

                dsNew.InsertLastRow();

                int row = dsNew.RowCount - 1;
                dsNew.DATA[row].COOP_ID = state.SsCoopControl;
            }
            else if (eventArg == "PrintReport")
            {
                RunReport();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddRepeater(dsNew);
                exe.AddFormView(dsDetail, ExecuteType.Update);
                exe.AddRepeater(dsList);
                exe.Execute();

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");

                dsNew.ResetRow();
                dsMain.Retrieve();
                dsList.ResetRow();
                dsDetail.ResetRow();


            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ " + ex); }
        }

        public void WebSheetLoadEnd()
        {

        }

        public void RunReport()
        {
            try
            {
                iReportArgument args = new iReportArgument();
                string loadintrate_code = dsDetail.DATA[0].LOANINTRATE_CODE;
                string coop_id = state.SsCoopControl;
                //ใส่อกิวเม้น

                args.Add("coop_id", iReportArgumentType.String, coop_id);
                args.Add("loadintrate_code", iReportArgumentType.String, loadintrate_code);
                iReportBuider report1 = new iReportBuider(this, "รายงานตารางดอกเบี้ย");
                report1.AddCriteria("r_sl_lncfloanintrate", "เปิด เพื่อพิมพ์รายงานตารางดอกเบี้ย", ReportType.pdf, args);
                report1.AutoOpenPDF = true;
                report1.Retrieve();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }
    }
}
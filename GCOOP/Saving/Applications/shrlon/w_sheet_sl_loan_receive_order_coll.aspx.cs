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
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using System.Globalization;
using System.Web.Services.Protocols;
using System.Threading;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_loan_receive_order_coll : PageWebSheet, WebSheet
    {
        private n_shrlonClient srvShrlon;
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String openDialogDetail;
        protected String initShareWithdrawList;
        protected String popupReport;
        protected String postcheckAll;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            openDialogDetail = WebUtil.JsPostBack(this, "openDialogDetail");
            initShareWithdrawList = WebUtil.JsPostBack(this, "initShareWithdrawList");
            popupReport = WebUtil.JsPostBack(this, "popupReport");
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            postcheckAll = WebUtil.JsPostBack(this, "postcheckAll");
            HfPageCommand.Value = "";
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            try
            {
                srvShrlon = wcf.NShrlon;
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

            if (IsPostBack)
            {
                this.RestoreContextDw(dw_list);
            }
            else
            {
                this.InitShareWithdrawList();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "openDialogDetail")
            {
                this.OpenDialogDetail();
                HfPageCommand.Value = "opendialog";
            }
            else if (eventArg == "initShareWithdrawList")
            {
                this.InitShareWithdrawList();
            }
            else if (eventArg == "popupReport")
            {
                PopupReport();
            }
            else if (eventArg == "postcheckAll")
            {
                JsCheckAll();
            }
        }
        private void JsCheckAll()
        {
            Boolean Select = CheckAll.Checked;
            Decimal Set = 1;

            if (Select == true)
            {
                Set = 1;
            }
            else if (Select == false)
            {
                Set = 0;
            }
            for (int i = 1; i <= dw_list.RowCount; i++)
            {
                dw_list.SetItemDecimal(i, "operate_flag", Set);
            }
        }
        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            dw_list.SaveDataCache();
        }

        #endregion

        private void RunProcess()
        {
            app = state.SsApplication;
            try
            {
                //gid = Request["gid"].ToString();
                gid = "MBSHR_DAILY";
            }
            catch { }
            try
            {
                //rid = Request["rid"].ToString();
                rid = "MBSHR_DAILY00";
            }
            catch { }

            //string member_no = "";
            //for (int i = 0; i < dw_list.RowCount; i++)
            //{
            //    int rowIndex = i + 1;
            //    decimal flag = dw_list.GetItemDecimal(rowIndex, "operate_flag");
            //    if (flag == 1)
            //    {
            //        member_no = dw_list.GetItemString(rowIndex, "member_no");
            //    }

            //}

            String list_memno = "";
            int iii = 0;
            for (int i = 1; i <= dw_list.RowCount; i++)
            {
                if (dw_list.GetItemDecimal(i, "operate_flag") == 1)
                {
                    if (iii == 0)
                    {
                        list_memno += "'" + dw_list.GetItemString(i, "member_no") + "'";
                    }
                    else
                    {
                        list_memno += ", '" + dw_list.GetItemString(i, "member_no") + "'";
                    }
                    iii++;
                }
            }
            list_memno = "(" + list_memno + ")";

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(state.SsCoopControl, ArgumentType.String);
            lnv_helper.AddArgument(list_memno, ArgumentType.String);

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF

            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                //String criteriaXML = lnv_helper.PopArgumentsXML();
                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                //String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
                //if (li_return == "true")
                //{
                //    HdOpenIFrame.Value = "True";
                //    HdcheckPdf.Value = "True";

                //}
                //else if (li_return != "true")
                //{
                //    HdcheckPdf.Value = "False";
                //}
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            Session["pdf"] = pdf;
            //PopupReport();
        }
        public void PopupReport()
        {
            RunProcess();
            // Thread.Sleep(5000);
            Thread.Sleep(4500);
            //เด้ง Popup ออกรายงานเป็น PDF.
            //String pop = "Gcoop.OpenPopup('http://localhost/GCOOP/WebService/Report/PDF/20110223093839_shrlonchk_SHRLONCHK001.pdf')";

            String pop = "Gcoop.OpenPopup('" + Session["pdf"].ToString() + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);

        }

        private void InitShareWithdrawList()
        {
            try
            {
                dw_list.Reset();
                String xmlList = srvShrlon.of_initlist_lnrcv(state.SsWsPass, state.SsCoopId);
                dw_list.ImportString(xmlList, FileSaveAsType.Xml);
            }
            catch (Exception ex) { }
        }


        private void OpenDialogDetail()
        {
            string member_no = "";
            for (int i = 0; i < dw_list.RowCount; i++)
            {
                int rowIndex = i + 1;
                decimal flag = dw_list.GetItemDecimal(rowIndex, "operate_flag");
                if (flag == 1)
                {
                    member_no = dw_list.GetItemString(rowIndex, "member_no");
                }

            }
            //DataTable dt = WebUtil.Query("select count(deptaccount_no)as count_dept from dpdeptmaster where coop_id ='" + state.SsCoopControl + "' and member_no ='" + member_no + "' and deptclose_status =0");
            //int count = 0;
            //try
            //{
            //    count = Convert.ToInt32(dt.Rows[0]["count_dept"]);
            //}
            //catch (Exception ex)
            //{

            //}
            //if (count > 0)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage("ยังไม่ได้ปิดบัญชีเงินฝาก กรุณาออกใบแจ้งปิดบัญชีเพื่อไปทำรายการปิดบัญชีที่แผนกเงินฝาก");
            //    Hidchkdept.Value = "TRUE";
            //}
            //else
            //{
                ArrayList dwlist = new ArrayList();
                int allRow = dw_list.RowCount;
                for (int i = 0; i < allRow; i++)
                {
                    int rowIndex = i + 1;
                    decimal flag = dw_list.GetItemDecimal(rowIndex, "operate_flag");
                    if (flag == 1)
                    {
                        str_slippayout strslippayout = new str_slippayout();
                        strslippayout.initfrom_type = "";
                        strslippayout.member_no = dw_list.GetItemString(rowIndex, "member_no");
                        //   strslippayout.loancontract_no = dw_list.GetItemString(rowIndex, "loancontract_no");
                        dwlist.Add(strslippayout);
                    }

                }
                Session["SSList"] = dwlist;
                Hidchkdept.Value = "FALSE";
           // }
        }
    }
}

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

using Sybase.DataWindow;
using System.Data.OracleClient;
using System.Globalization;
using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Web.Services.Protocols;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_cancel_apvchq : PageWebSheet,WebSheet 
    {
        private CultureInfo th;
        private DwThDate tdw_date;
        private DwThDate tdw_list;
        private n_depositClient depService;

        // JavaSctipt PostBack
        protected String postCancelPastChq;
        protected String postSaveCancelPastChq;
        protected String postCheckStatus;
        protected String postCheckDetail;
        protected String RejectDate;

        protected void JspostCanclePastChq()
        {
            // ส่วนติดต่อ SERVICE
            try
            {
                DateTime dept_date = Dw_date.GetItemDate(1, "start_date");
                String dept_tdate = Dw_date.GetItemString(1, "start_tdate");
                String wsPass = state.SsWsPass;
                Int16 ai_status = Convert.ToInt16(Dw_date.GetItemDecimal(1, "ai_flag"));
                String branchId = state.SsCoopId;
                String xmlFromService = depService.of_get_chqlist(wsPass, dept_date, ai_status, branchId);
                Dw_list.Reset(); //ลบแถวก่อนแล้วค่อย retrieve
                if (xmlFromService != null && xmlFromService != "")
                {
                    DwUtil.ImportData(xmlFromService, Dw_list, null, FileSaveAsType.Xml);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลรายการเช็คประจำวันที่ : " + dept_tdate);
                }

               // HdIsFinished.Value = "true";
            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                // error ทั่วไป
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        protected void JspostSaveCanclePastChq()
        {
            //ส่วนติดต่อ SERVICE
            try
            {
                Dw_list.SetFilter("checkclear_status = -9");
                Dw_list.Filter();

                // การโยน ไฟล์ xml ไปให้ service
                String postlist = Dw_list.Describe("Datawindow.Data.Xml");
                String entry_id = state.SsUsername;
                String wsPass = state.SsWsPass;
                String branchID = state.SsCoopId;
                DateTime Entry_date = state.SsWorkDate;
                String machineId = state.SsClientIp;
                Int16 ai_status = Convert.ToInt16(Dw_date.GetItemDecimal(1, "ai_flag"));

                //เรียกใช้ webservice
                Int32 result = depService.of_cancelchq_bylist(wsPass, entry_id, Entry_date, machineId, branchID, postlist, ai_status );
           
                HdIsFinished.Value = "true";

                if (result != 1)
                {
                    LtServerMessage.Text =  WebUtil.ErrorMessage("ไม่สามารถยกเลิกรายการเช็คได้");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ยกเลิกเช็คเรียบร้อยแล้ว");
                }
            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                // error ทั่วไป
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        protected void JspostCheckStatus()
        {
            DateTime Workdate = new DateTime();
            Int16 RowDw_list = Convert.ToInt16(HdRowDw_list.Value);
            Workdate = state.SsWorkDate;
            
            Dw_list.SetItemDate(RowDw_list, "p_r_date", Workdate);
            tdw_list.Eng2ThaiAllRow();

            DateTime Entry_date = Dw_list.GetItemDate(RowDw_list, "Entry_date");
            String deptacc_no = Dw_list.GetItemString(RowDw_list, "deptaccount_no");
            Dw_detail.Retrieve(deptacc_no, Entry_date, state.SsCoopId);
            HdEntryDate.Value = Entry_date.ToString("yyyy-MM-dd", WebUtil.EN);
            //Dw_list.SetItemString(RowDw_list, "p_r_tdate", Workdate.ToString("ddMMyyyy", new CultureInfo("th-TH")));
        }

        private void JspostCheckDetail()
        {
            int RowDwdetailclick = int.Parse(Hd_RowClick.Value);
            DateTime Entry_date = Dw_list.GetItemDate(RowDwdetailclick, "Entry_date");
            String deptacc_no = Dw_list.GetItemString(RowDwdetailclick, "deptaccount_no");
            try
            {
                Dw_detail.Retrieve(deptacc_no, Entry_date, state.SsCoopId);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsRejectDate()
        {
            int RowDwdetailclick = int.Parse(HdRowDw_list.Value);
            Dw_list.SetItemDateTime(RowDwdetailclick, "p_r_date", state.SsWorkDate);
            tdw_list.Eng2ThaiAllRow();
            JspostCheckStatus();
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postCancelPastChq = WebUtil.JsPostBack(this, "postCancelPastChq");
            postSaveCancelPastChq = WebUtil.JsPostBack(this, "postSaveCancelPastChq");
            postCheckStatus = WebUtil.JsPostBack(this, "postCheckStatus");
            postCheckDetail = WebUtil.JsPostBack(this, "postCheckDetail");
            RejectDate = WebUtil.JsPostBack(this, "RejectDate");

            tdw_date = new DwThDate(Dw_date,this);
            tdw_date.Add("start_date", "start_tdate");
            //tdw_date.Eng2ThaiAllRow();

            tdw_list = new DwThDate(Dw_list,this);
            tdw_list.Add("p_r_date", "p_r_tdate");
            //tdw_list.Eng2ThaiAllRow();

        }

        public void WebSheetLoadBegin()
        {
            HdWorkDate.Value = state.SsWorkDate.ToString("yyyy-MM-dd", WebUtil.EN);
            this.ConnectSQLCA();

            try
            {
                depService = wcf.NDeposit;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }

            th = new CultureInfo("th-TH");

            Dw_date.SetTransaction(sqlca);
            Dw_list.SetTransaction(sqlca);
            Dw_detail.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                Dw_date.InsertRow(0);
               // Dw_list.InsertRow(0);
                Dw_detail.InsertRow(0);
                Dw_date.SetItemDateTime(1, "start_date", state.SsWorkDate);
                tdw_date.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(Dw_date);
                this.RestoreContextDw(Dw_list);
                this.RestoreContextDw(Dw_detail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCancelPastChq")
            {
                JspostCanclePastChq();
            }
            else if (eventArg == "postSaveCancelPastChq")
            {
                JspostSaveCanclePastChq();
            }
            else if (eventArg == "postCheckStatus") 
            {
                JspostCheckStatus();    
            }
            else if (eventArg == "postCheckDetail")
            {
                JspostCheckDetail();
            }
            else if (eventArg == "RejectDate")
            {
                JsRejectDate();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                JspostSaveCanclePastChq();
                
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
                JspostCanclePastChq();
        }

        public void WebSheetLoadEnd()
        {
            if (Dw_detail.RowCount > 1)
            {
                Dw_detail.DeleteRow(1);
            }
            Dw_date.SaveDataCache();
            Dw_list.SaveDataCache();
            Dw_detail.SaveDataCache();
        }

        #endregion
    }
}

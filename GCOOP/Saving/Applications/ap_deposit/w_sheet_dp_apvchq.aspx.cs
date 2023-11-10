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

using CoreSavingLibrary.WcfNCommon;//new common
using Sybase.DataWindow;
using System.Data.OracleClient;
using System.Globalization;
using System.Web.Services.Protocols;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit; //new deposit

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_apvchq : PageWebSheet, WebSheet
    {
        //=============================
        //private CultureInfo th;
        private DwThDate tdw_date;
        private DwThDate tdw_list;
        protected String postCheckList;
        protected String postCheckStatus;
        protected String postCheckDetail;
        protected String RejectDate;
        //private DepositClient depService;        
        private n_depositClient ndept;
        //=============================
        private void JspostCheckDetail()
        {
            int RowDwdetailclick = int.Parse(Hd_RowClick.Value);
            DateTime Entry_date = Dw_list.GetItemDate(RowDwdetailclick, "Entry_date");
            String deptacc_no = Dw_list.GetItemString(RowDwdetailclick, "deptaccount_no");
            String coop_id = Dw_list.GetItemString(RowDwdetailclick, "coop_id");
            Dw_detail.Retrieve(deptacc_no, Entry_date, coop_id);
        }

        private void JspostCheckStatus()
        {
            int RowDwdetailclick = int.Parse(Hd_RowCheckStatus.Value);
            Decimal CheckStatus = Dw_list.GetItemDecimal(RowDwdetailclick, "checkclear_status");
            if (CheckStatus == 1)
            {
                Dw_list.SetItemDateTime(RowDwdetailclick, "p_r_date", state.SsWorkDate);
                tdw_list.Eng2ThaiAllRow();  
            }
                DateTime Entry_date = Dw_list.GetItemDate(RowDwdetailclick, "Entry_date");
                String deptacc_no = Dw_list.GetItemString(RowDwdetailclick, "deptaccount_no");
                HdEntryDate.Value = Entry_date.ToString("yyyy-MM-dd", WebUtil.EN);
                Dw_detail.Retrieve(deptacc_no, Entry_date, state.SsCoopId);
        }

        private void JspostCheckList(int save_reset)
        {
            try
            {
                DateTime dept_date = Dw_date.GetItemDate(1, "start_date");
                String dept_tdate = Dw_date.GetItemString(1,"start_tdate");
                String wsPass = state.SsWsPass;
                Int16 ai_status = 8;
                String branchId = state.SsCoopId;

                //String xmlFromService = depService.GetChequeList(wsPass, dept_date, ai_status, branchId);

                String xmlFromService = ndept.of_get_chqlist(wsPass, dept_date, ai_status, branchId);

                Dw_list.Reset(); //ลบแถวก่อนแล้วค่อย retrieve

                if (xmlFromService == "" || xmlFromService == null)
                {
                    if (save_reset == 0)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลรายการเช็คประจำวันที่ : " + dept_tdate);
                    }
                }
                else 
                {
                    DwUtil.ImportData(xmlFromService, Dw_list, null, FileSaveAsType.Xml);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsRejectDate()
        {
            int RowDwdetailclick = int.Parse(Hd_RowCheckStatus.Value);
            Dw_list.SetItemDateTime(RowDwdetailclick, "p_r_date", state.SsWorkDate);
            tdw_list.Eng2ThaiAllRow();
            JspostCheckStatus();
        }

        //=============================
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postCheckList = WebUtil.JsPostBack(this, "postCheckList");
            postCheckStatus = WebUtil.JsPostBack(this, "postCheckStatus");
            postCheckDetail = WebUtil.JsPostBack(this, "postCheckDetail");
            RejectDate = WebUtil.JsPostBack(this, "RejectDate");

            tdw_date = new DwThDate(Dw_date, this);
            tdw_date.Add("start_date", "start_tdate");
            //tdw_date.Eng2ThaiAllRow();

            tdw_list = new DwThDate(Dw_list, this);
            tdw_list.Add("p_r_date", "p_r_tdate");
            //tdw_list.Eng2ThaiAllRow();
        }

        public void WebSheetLoadBegin()
        {
            HdWorkDate.Value = state.SsWorkDate.ToString("yyyy-MM-dd", WebUtil.EN);
            
            try 
            {
                this.ConnectSQLCA(); 
            }
            catch 
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Database ไม่ได้");
                return;
            }

            try 
            {
                ndept = wcf.NDeposit;
            }
            catch 
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Webservice ไม่ได้");
                return;
            }
         
            Dw_date.SetTransaction(sqlca);
            Dw_list.SetTransaction(sqlca);
            Dw_detail.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                Dw_date.InsertRow(0);
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
            if (eventArg == "postCheckList")
            {
                JspostCheckList(0);
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
                Dw_list.SetFilter("checkclear_status = 1");
                Dw_list.Filter();

                // การโยน ไฟล์ xml ไปให้ service
                String postlist = Dw_list.Describe("Datawindow.Data.Xml");
                String wsPass = state.SsWsPass;
                String CoopId = state.SsCoopId;
                String machineId = state.SsClientIp;

                //เรียกใช้ webservice
                //bool result = depService.PastChequeByList(wsPass, state.SsUsername, state.SsWorkDate, machineId, CoopId, postlist);

                Int16 result = ndept.of_pastchq_bylist(wsPass, state.SsUsername, state.SsWorkDate, machineId, CoopId, postlist);


                if (result != 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถผ่านรายการเช็คได้");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ผ่านเช็คเรียบร้อยแล้ว");
                    JspostCheckList(1);
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

        public void WebSheetLoadEnd()
        {
            //if (Dw_date.RowCount > 1)
            //{
            //    Dw_date.DeleteRow(Dw_date.RowCount);
            //}
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

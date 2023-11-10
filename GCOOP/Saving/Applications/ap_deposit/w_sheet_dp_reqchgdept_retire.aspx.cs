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
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit;  // new deposit
using CoreSavingLibrary.WcfNCommon; // new common
using System.Web.Services.Protocols;
using System.ServiceModel.Channels;
using System.Xml;
using Sybase.DataWindow;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_reqchgdept_retire : PageWebSheet, WebSheet

    {
        //private DepositClient depService;
        //private CommonClient cmdService;
        private n_depositClient ndept; // new deposit
        private n_commonClient ncommon; // new common
        private DwThDate tDwMain;
        private DwThDate tDwDetail;
        //POSTBACK
   
        protected String postChgAcc;
        protected String postAmt;

        public void InitJsPostBack()
        {
            postChgAcc = WebUtil.JsPostBack(this, "postChgAcc");
            postAmt = WebUtil.JsPostBack(this, "postAmt");

            //----------------------------------------
            tDwMain = new DwThDate(DwMain, this);
            tDwDetail = new DwThDate(DwDetail, this);
            tDwMain.Add("reqchg_date", "reqchg_tdate");
            tDwMain.Add("deptmontchg_date", "deptmontchg_tdate");
            tDwMain.Add("entry_date", "entry_tdate");
            tDwDetail.Add("entry_date", "entry_tdate");
            tDwDetail.Add("deptmontchg_date", "chgmountdept_tdate");

        }

        public void WebSheetLoadBegin()
        {

            try
            {
                //depService = wcf.Deposit;
                ncommon = wcf.NCommon;
                ndept = wcf.NDeposit;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถติดต่อ WebService ได้");
            }

            try
            {
                if (!IsPostBack)
                {
                    DwMain.InsertRow(0);
                    DwMain.SetItemString(1, "coop_id",state.SsCoopId);
                    DwMain.SetItemDateTime(1, "reqchg_date", state.SsWorkDate);
                    DwMain.SetItemDateTime(1, "deptmontchg_date", state.SsWorkDate);
                    DwMain.SetItemDateTime(1, "entry_date", DateTime.Now);
                    DwMain.SetItemString(1, "entry_id", state.SsUsername);
                    tDwMain.Eng2ThaiAllRow();
                    HdFocusdeptmonth.Value = "true";

                }
                else
                {
                    HdFocusdeptmonth.Value = "false";
                    this.RestoreContextDw(DwMain);
                    this.RestoreContextDw(DwDetail);
                }
            }
            catch { }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postChgAcc") {
                JsPostChgAcc();
            }
            else if (eventArg == "postAmt")
            {
                JspostAmt();
            }
        }

        private void JsPostChgAcc()
        {
            String errMessage = "";
            try
            {
              
                    String xmlreturn = "";
                    String deptno = HfAccNo.Value;              
                    //deptno = depService.BaseFormatAccountNo(state.SsWsPass, deptno);
                    deptno = wcf.NDeposit.of_analizeaccno(state.SsWsPass, deptno); //new
                    //int re = depService.of_reqchgdept(state.SsWsPass, deptno, ref xmlreturn,ref errMessage);
                    int re = wcf.NDeposit.of_init_reqchgdept(state.SsWsPass, deptno, ref xmlreturn, ref errMessage); //new
                    DwMain.Reset();
                    DwMain.ImportString(xmlreturn, FileSaveAsType.Xml);
                    DwMain.SetItemDateTime(1, "reqchg_date", state.SsWorkDate);
                    DwMain.SetItemDateTime(1, "deptmontchg_date", state.SsWorkDate);
                    DwMain.SetItemDateTime(1, "entry_date", DateTime.Now);
                    tDwMain.Eng2ThaiAllRow();
                    //String viewAccNo = depService.ViewAccountNoFormat(state.SsWsPass, deptno);
                    String viewAccNo = WebUtil.ViewAccountNoFormat(deptno); //new
                    DwMain.SetItemString(1, "deptaccount_no", viewAccNo);
                    DwMain.SetItemString(1, "entry_id", state.SsUsername);
                    try
                    {
                        DwDetail.InsertRow(0);
                        DwUtil.RetrieveDataWindow(DwDetail, "dp_reqsequest.pbl", tDwDetail, state.SsCoopControl, deptno);
                        tDwDetail.Eng2ThaiAllRow();
                    }
                    catch( Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex); 
                    }
          
            }
            catch
            { LtServerMessage.Text = WebUtil.ErrorMessage(errMessage); }

            HdFocusdeptmonth.Value = "true";
        }


        private void JspostAmt() 
        {
            Decimal amt = DwMain.GetItemDecimal(1, "deptmonth_newamt");
            try {
                DwMain.SetItemDecimal(1, "deptmonth_appamt", amt);
            }
            catch { }
        }

        public void SaveWebSheet()
        {
            try
            {
            if (!(HfAccNo.Value == null || HfAccNo.Value == ""))
                {
              //String  deptno = depService.BaseFormatAccountNo(state.SsWsPass, HfAccNo.Value);
                    String deptno = wcf.NDeposit.of_analizeaccno(state.SsWsPass, HfAccNo.Value); //new
              DwMain.SetItemString(1, "deptaccount_no",deptno);
               string xml = "";
                xml = DwMain.Describe("DataWindow.Data.XML");
                //int re = depService.save_of_reqchgdept(state.SsWsPass, xml,2);
                int re = wcf.NDeposit.of_reqchgdept(state.SsWsPass, xml, 2); //new
                DwMain.Reset();
                DwMain.InsertRow(0);
                DwMain.SetItemString(1, "coop_id", state.SsCoopId);
                DwMain.SetItemDateTime(1, "reqchg_date", state.SsWorkDate);
                DwMain.SetItemDateTime(1, "deptmontchg_date", state.SsWorkDate);
                DwMain.SetItemDateTime(1, "entry_date", DateTime.Now);
                DwMain.SetItemString(1, "entry_id", state.SsUsername);
                tDwMain.Eng2ThaiAllRow();
                LtServerMessage.Text = WebUtil.CompleteMessage("เลขบัญชี  "+deptno+"  บันทึกการทำรายการเรียบร้อยแล้ว   ");
                }
              else
               {
               LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาใส่เลขที่บัญชี");
              }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
 
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }
    }
}
using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using System.Text;
using Sybase.DataWindow;

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_sl_popup_loanreceive : PageWebDialog, WebDialog
    {
        private n_shrlonClient slService;
        private n_commonClient commonSrv;
        private DwThDate tDwMain;

        protected String saveSlipLnRcv;
        protected String initDataWindow;
        protected String initLnRcvReCalInt;
        protected ArrayList dwList;
        protected String[] arrValue = new String[3];

        #region WebDialog Members

        public void InitJsPostBack()
        {
            saveSlipLnRcv = WebUtil.JsPostBack(this, "saveSlipLnRcv");
            initDataWindow = WebUtil.JsPostBack(this, "initDataWindow");
            initLnRcvReCalInt = WebUtil.JsPostBack(this, "initLnRcvReCalInt");
            tDwMain = new DwThDate(DwMain);
            tDwMain.Add("operate_date", "operate_tdate");

        }

        public void WebDialogLoadBegin()
        {
            slService = wcf.NShrlon;
            commonSrv = wcf.NCommon;

            if (!IsPostBack)
            {
                this.InitDataWindow();
                DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_loan_receive.pbl", null);
                //DataWindowChild moneyType = DwMain.GetChild("moneytype_code");
                //String xml = commonSrv.GetDDDWXml(state.SsWsPass, "dddw_sl_ucfmoneytypeday");
                //moneyType.ImportString(xml, FileSaveAsType.Xml);
                
                
            }
            else
            {
                this.RestoreContextDw(DwMain);
                try
                {
                    String dtString = DwMain.GetItemString(1, "operate_tdate");
                    dtString = dtString.Replace("/", "");
                    DwMain.SetItemDateTime(1, "operate_date", DateTime.ParseExact(dtString, "ddMMyyyy", WebUtil.TH));
                }
                catch { }
                DwOperateLoan.RestoreContext();
                DwOperateEtc.RestoreContext();
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "saveSlipLnRcv")
            {
                this.SaveSlipLnRcv();
            }
            if (eventArg == "initLnRcvReCalInt")
            {
                this.InitLnRcvReCalInt();
            }
            if (eventArg == "initDataWindow")
            {
                this.InitDataWindow();
            }
            if (eventArg == "fieldProperty")
            {

            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion

        private void InitDataWindow()
        {
            int listIndex=0;
            ArrayList dwList = new ArrayList();
            dwList = (ArrayList)Session["SSList"];

            HfAllIndex.Value = dwList.Count.ToString(); //จำนวน Index ของ Array Data ที่ส่งมาจากหน้า Sheet


            try { listIndex = Convert.ToInt32(HfIndex.Value); }
            catch { HfIndex.Value = "0"; listIndex = 0; }

            LbSaveStatus.Text = "(" + (listIndex+1) + "/" + HfAllIndex.Value + ")";

            String loanContractNo = "";
            String memberNo = "";
            String formtype = "";
            String saveStatus = "";
            try
            {
                arrValue = dwList[listIndex] as String[];
                loanContractNo = arrValue[0];
                formtype = arrValue[1];
                memberNo = arrValue[2];
                LbMenberNo.Text = memberNo;
                saveStatus = arrValue[3];
                if (saveStatus != "") { throw new Exception(WebUtil.ErrorMessage("มีการบันทึกรายการ สมาชิกรายนี้ไปแล้ว")); }
            }
            catch (Exception ex) { }

            str_slippayout sSlipPayOut = new str_slippayout();
            sSlipPayOut.coop_id = state.SsCoopId;
            sSlipPayOut.entry_id = state.SsUsername;
            sSlipPayOut.operate_date = state.SsWorkDate;
            sSlipPayOut.loancontract_no = loanContractNo;
            sSlipPayOut.member_no = memberNo;
            sSlipPayOut.slip_date = state.SsWorkDate;
            sSlipPayOut.initfrom_type = formtype;

            HfFormtype.Value = formtype;

            slService.of_initlnrcv(state.SsWsPass, ref sSlipPayOut);

            try
            {
                DwMain.Reset();
                DwMain.ImportString(sSlipPayOut.xml_sliphead, Sybase.DataWindow.FileSaveAsType.Xml);
                DwUtil.DeleteLastRow(DwMain);
                tDwMain.Eng2ThaiAllRow();
                DwMain.SetItemString(1, "moneytype_code", "CSH");
                
            }
            catch (Exception ex) { String ext = ex.ToString(); }
            try
            {
                DwOperateLoan.Reset();
                DwOperateLoan.ImportString(sSlipPayOut.xml_slipcutlon, Sybase.DataWindow.FileSaveAsType.Xml);
            }
            catch (Exception ex) { String ext = ex.ToString(); }
            try
            {
                DwOperateEtc.Reset();
                DwOperateEtc.ImportString(sSlipPayOut.xml_slipcutetc, Sybase.DataWindow.FileSaveAsType.Xml);
            }
            catch (Exception ex) { String ext = ex.ToString(); }


        }

        private void InitLnRcvReCalInt()
        {
            str_slippayout strPayOut = new str_slippayout();
            strPayOut.coop_id = state.SsCoopId;
            strPayOut.entry_id = state.SsUsername;
            strPayOut.operate_date = DwMain.GetItemDateTime(1, "operate_date");
            strPayOut.loancontract_no = DwMain.GetItemString(1, "loancontract_no");
            strPayOut.member_no = DwMain.GetItemString(1, "member_no"); ;
            strPayOut.slip_date = state.SsWorkDate;
            strPayOut.initfrom_type = HfFormtype.Value;

            String dwMainXML ="";
            String dwLoanXML = "";
            String dwEtcXML = "";

            dwMainXML = DwMain.Describe("DataWindow.Data.XML");
            dwLoanXML = DwOperateLoan.Describe("DataWindow.Data.XML");
            try { dwEtcXML = DwOperateEtc.Describe("DataWindow.Data.XML"); }
            catch { dwEtcXML = ""; }

            strPayOut.xml_sliphead = dwMainXML;
            strPayOut.xml_slipcutlon = dwLoanXML;
            strPayOut.xml_slipcutetc = dwEtcXML;


            slService.of_initlnrcv_recalint(state.SsWsPass, ref strPayOut);

            try
            {
                DwMain.Reset();
                DwMain.ImportString(strPayOut.xml_sliphead, FileSaveAsType.Xml);
                if (DwMain.RowCount > 1)
                {
                    DwMain.DeleteRow(DwMain.RowCount);
                }
            }
            catch { DwMain.Reset(); }
            try
            {
                DwOperateLoan.Reset();
                DwOperateLoan.ImportString(strPayOut.xml_slipcutlon, FileSaveAsType.Xml);
            }
            catch { DwOperateLoan.Reset(); }
            try
            {
                DwOperateEtc.Reset();
                DwOperateEtc.ImportString(strPayOut.xml_slipcutetc, FileSaveAsType.Xml);
            }
            catch { DwOperateEtc.Reset(); }
        }

        private void SaveSlipLnRcv()
        {
            String memno = DwMain.GetItemString(1, "member_no");
            int index = Convert.ToInt32(HfIndex.Value);
            int allIndex = Convert.ToInt32(HfAllIndex.Value);

            str_slippayout strPayOut = new str_slippayout();
            strPayOut.coop_id = state.SsCoopId;
            strPayOut.entry_id = state.SsUsername;
            strPayOut.operate_date = DwMain.GetItemDateTime(1, "operate_date");
            try
            {
                strPayOut.loancontract_no = DwMain.GetItemString(1, "loancontract_no");
            }
            catch { strPayOut.loancontract_no = ""; }
            strPayOut.member_no = memno;
            strPayOut.slip_date = state.SsWorkDate;
            strPayOut.initfrom_type = HfFormtype.Value;

            String dwMainXML = "";
            String dwLoanXML = "";
            String dwEtcXML = "";

            dwMainXML = DwMain.Describe("DataWindow.Data.XML");
            try { dwLoanXML = DwOperateLoan.Describe("DataWindow.Data.XML"); }
            catch { dwLoanXML = ""; }
            try { dwEtcXML = DwOperateEtc.Describe("DataWindow.Data.XML"); }
            catch { dwEtcXML = ""; }

            strPayOut.xml_sliphead = dwMainXML;
            strPayOut.xml_slipcutlon = dwLoanXML;
            strPayOut.xml_slipcutetc = dwEtcXML;

            try
            {
                int result = slService.of_saveslip_lnrcv(state.SsWsPass, ref strPayOut);
                int nextIndex = index + 1;
                if (nextIndex > allIndex)
                {
                    nextIndex = index - 1;
                }
                HfIndex.Value = nextIndex.ToString() ;
                //Response.Write("<script>alert('บันทึกสำเร็จ');</script>");
                if (nextIndex != allIndex)
                {
                    this.InitDataWindow();
                }
            }
            catch (Exception ex) { Response.Write("<script>alert('ไม่สามารถบันทึกเลขทะเบียน " + memno + " ได้');</script>"); }

        }
    }
}

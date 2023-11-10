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
using System.Web.Services.Protocols;
using System.Threading;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_transloan_collateral : PageWebSheet, WebSheet
    {
        private DwThDate tDwMain;
        private n_shrlonClient shrService;
        protected String postAccountNo;
        protected String postTrnPrnAmt;
      
        protected str_lntrncoll strLnTrnColl;

        private void JsPostTrnPrnAmt()
        {
            Int32 strXML ;
            string xml_trnmast = dw_main.Describe("DataWindow.Data.XML");
            string xml_trndetail = dw_detail.Describe("DataWindow.Data.XML");
            try
            {
                
                strXML = shrService.of_initlntrncoll_recaltrn(state.SsWsPass,ref xml_trnmast,ref xml_trndetail);

                dw_main.Reset();
                dw_main.ImportString(xml_trnmast, Sybase.DataWindow.FileSaveAsType.Xml);
                while (dw_main.RowCount > 1)
                {
                    dw_main.DeleteRow(2);
                }
                dw_detail.Reset();
                dw_detail.ImportString(xml_trndetail, Sybase.DataWindow.FileSaveAsType.Xml);
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JsPostAccountNo()
        {
            try
            {
                strLnTrnColl.loancontract_no = dw_main.GetItemString(1, "loancontract_no");
                strLnTrnColl.trncollreq_code = dw_main.GetItemString(1, "trncollreq_code");
                strLnTrnColl.trncollreq_date = dw_main.GetItemDateTime(1, "trncollreq_date"); 
                
                Int32 result = shrService.of_initlntrncoll(state.SsWsPass,ref strLnTrnColl);

                try
                {
                    dw_main.Reset();
                    dw_main.ImportString(strLnTrnColl.xml_trnmast, Sybase.DataWindow.FileSaveAsType.Xml);
                }
                catch (Exception ex) { DwUtil.ImportData(strLnTrnColl.xml_trnmast, dw_main, null, FileSaveAsType.Xml); }
                tDwMain.Eng2ThaiAllRow();
                try
                {
                    dw_detail.Reset();
                    dw_detail.ImportString(strLnTrnColl.xml_trndetail, Sybase.DataWindow.FileSaveAsType.Xml);
                }
                catch (Exception ex) { DwUtil.ImportData(strLnTrnColl.xml_trndetail, dw_detail, null, FileSaveAsType.Xml); }                
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postAccountNo = WebUtil.JsPostBack(this, "postAccountNo");
            postTrnPrnAmt = WebUtil.JsPostBack(this, "postTrnPrnAmt");

            tDwMain = new DwThDate(dw_main, this);
            tDwMain.Add("trncollreq_date", "trncollreq_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();

            dw_main.SetTransaction(sqlca);
            dw_detail.SetTransaction(sqlca);
            strLnTrnColl = new str_lntrncoll(); // 510137200 
            try
            {
                shrService = wcf.NShrlon;
            }
            catch
            { }

            if (!IsPostBack)
            {
                dw_main.InsertRow(0);
                dw_main.SetItemDateTime(1, "trncollreq_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_detail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postTrnPrnAmt")
            {
                JsPostTrnPrnAmt();
            }
            else if (eventArg == "postAccountNo")
            {
                JsPostAccountNo();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                strLnTrnColl.xml_trnmast = dw_main.Describe("DataWindow.Data.XML");

                strLnTrnColl.xml_trndetail = dw_detail.Describe("DataWindow.Data.XML");
                strLnTrnColl.entry_id = state.SsClientIp;
                strLnTrnColl.coop_id = state.SsCoopId;

                shrService.of_savetrn_lntrncoll(state.SsWsPass,ref strLnTrnColl);


                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อยแล้ว");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            string loanContractNo = dw_main.GetItemString(1, "loancontract_no");
            if (loanContractNo != null && loanContractNo != "")
            {
                try
                {
                    DataTable dt = WebUtil.Query(
                        "select c.prename_desc ||  b.memb_name || ' ' || b.memb_surname as \"fullname\" from lncontmaster a" + @"
                        left join mbmembmaster b on a.member_no = b.member_no 
                        left join mbucfprename c on b.prename_code = c.prename_code
                        where a.loancontract_no = '" + loanContractNo + "' ");
                    DataTable dt2 = WebUtil.Query(
                        "select b.prefix || ' - ' || b.loantype_desc as \"loantype\" from lncontmaster a" + @" 
                        left join lnloantype b on a.loantype_code = b.loantype_code
                        where a.loancontract_no = '" + loanContractNo + "' ");
                    DataTable dt3 = WebUtil.Query(
                        "select a.description_short as \"description\" from lnucfcontracttype a" + @"
                        left join lncontmaster b on b.contract_type = a.contract_type
                        where b.loancontract_no = '" + loanContractNo + "' ");

                    if (dt.Rows.Count > 0)
                    {
                        dw_main.Modify("t_fullname.text='" + dt.Rows[0]["fullname"].ToString() + "'");
                        dw_main.Modify("t_loantype.text='" + dt2.Rows[0]["loantype"].ToString() + "'");
                        dw_main.Modify("t_description_short.text='" + dt3.Rows[0]["description"].ToString() + "'");

                    }
                }
                catch (Exception ex) { ex.ToString(); }
                dw_main.SaveDataCache();
                dw_detail.SaveDataCache();
            }
        }
        
        #endregion
    }
}

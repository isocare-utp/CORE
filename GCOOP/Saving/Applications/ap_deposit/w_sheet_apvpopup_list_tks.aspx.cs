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
using CoreSavingLibrary.WcfNDeposit;
using System.Web.Services.Protocols;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_apvpopup_list_tks : PageWebSheet,WebSheet
    {
        private n_depositClient depService;
        protected String ShowAvpListDetail;
        protected String ApvPermis;
        private Decimal widthMax;
        private Decimal deptMax;


        public void PermissAmount()
        {
            Decimal[] result = new Decimal[2];
            try
            {
                result = depService.of_get_permiss_amount(state.SsWsPass, state.SsUsername, state.SsCoopId);
                HdwidthMax.Value = result[0].ToString();
                HddeptMax.Value = result[1].ToString();
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            GetApvList();
        }

        public void GetApvList()
        {
            string xml_list = "";
            deptMax = Convert.ToDecimal(HddeptMax.Value);
            widthMax = Convert.ToDecimal(HdwidthMax.Value);
            try
            {
                
                DwHead.Reset();
                DwDetail.Reset();
                xml_list = depService.of_get_apvlist(state.SsWsPass, deptMax, widthMax, state.SsCoopId);
                if (xml_list != "")
                {
                    DwHead.ImportString(xml_list, Sybase.DataWindow.FileSaveAsType.Xml);
                }
                DwDetail.InsertRow(0);
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

        private void JsApvPermis()
        {
            
            string status = HdAppr.Value;
            int row = Convert.ToInt16(HdRow.Value);
            string proccessId = DwHead.GetItemString(row, "proccessid");
            string itemType = DwHead.GetItemString(row, "itemtype");
            Decimal amountReq = DwHead.GetItemDecimal(row, "amountreq");
            string apvCode = DwHead.GetItemString(row, "sysapv_code");

            try
            {
                //depService.ApvPermiss(state.SsWsPass, proccessId, status, amountReq, state.SsClientIp, state.SsUsername, state.SsWorkDate, apvCode, itemType, state.SsCoopId);
                GetApvList();
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

        private void JsShowAvpListDetail()
        {
            string proccessId = HdProccessId.Value;
            string xml_detail = "";
            try
            {
                DwDetail.Reset();
                xml_detail = depService.of_get_apvlist_detail(state.SsWsPass, proccessId, state.SsCoopId);
                DwDetail.ImportString(xml_detail, Sybase.DataWindow.FileSaveAsType.Xml);
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
            ShowAvpListDetail = WebUtil.JsPostBack(this, "ShowAvpListDetail");
            ApvPermis = WebUtil.JsPostBack(this, "ApvPermis");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            DwHead.SetTransaction(sqlca);
            DwDetail.SetTransaction(sqlca);
            try
            {
                depService = wcf.NDeposit;
            }
            catch
            { }
            
            if (!IsPostBack)
            {
                PermissAmount();
            }
            else
            {
                this.RestoreContextDw(DwHead);
                this.RestoreContextDw(DwDetail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "ShowAvpListDetail")
            {
                JsShowAvpListDetail();
            }
            else if (eventArg == "ApvPermis")
            {
                JsApvPermis();
            }
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            DwHead.SaveDataCache();
            DwDetail.SaveDataCache();
        }

        #endregion

        protected void BtnGetData_Click(object sender, EventArgs e)
        {
            GetApvList();
        }
    }
}

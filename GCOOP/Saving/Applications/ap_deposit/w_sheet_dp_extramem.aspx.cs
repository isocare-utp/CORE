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
using CoreSavingLibrary.WcfNCommon; // new common
using CoreSavingLibrary.WcfNDeposit;// new deposit
using Sybase.DataWindow;
using System.Web.Services.Protocols;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_extramem : PageWebSheet,WebSheet
    {

        //private DepositClient depService;
        private n_depositClient ndept; //new
        protected String FilterProvince;
        protected String FilterDistrict;
        protected String postMemberDetail;

        private void JsFilterProvince()
        {
            try
            {
                if (DwMain.GetItemString(1, "deptmem_province") != null)
                {
                    String provinceCode = DwMain.GetItemString(1, "deptmem_province");
                    DataWindowChild dc2 = DwMain.GetChild("deptmem_district");
                    dc2.SetTransaction(sqlca);
                    dc2.Retrieve();
                    dc2.SetFilter("province_code = '" + provinceCode + "'");
                    dc2.Filter();
                }
            }
            catch (Exception)
            {
    
            }
        }


        private void JsFilterDistrict()
        {
            try
            {
                if (DwMain.GetItemString(1, "deptmem_district") != null)
                {
                    String districtCode = DwMain.GetItemString(1, "deptmem_district");
                    DataWindowChild dc2 = DwMain.GetChild("deptmem_tambol");
                    dc2.SetTransaction(sqlca);
                    dc2.Retrieve();
                    dc2.SetFilter("district_code = '" + districtCode + "'");
                    dc2.Filter();
                }
            }
            catch (Exception)
            {

            }
        }

        private void JspostMemberDetail()
        {
            string xml_extramem = "";
            string depMemId = DwMain.GetItemString(1, "deptmem_id");
            try
            {
                DwMain.Reset();
                xml_extramem = ndept.of_get_extramem_detail(state.SsWsPass, depMemId, ref xml_extramem);
                DwMain.ImportString(xml_extramem, Sybase.DataWindow.FileSaveAsType.Xml);
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
            postMemberDetail = WebUtil.JsPostBack(this, "postMemberDetail");
            FilterProvince = WebUtil.JsPostBack(this, "FilterProvince");
            FilterDistrict = WebUtil.JsPostBack(this, "FilterDistrict");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);

            try
            {
                //depService = wcf.Deposit;
                ndept = wcf.NDeposit;
            }
            catch
            { }

            if (!IsPostBack)
            {
                DwMain.InsertRow(0);

                DwMain.SetItemString(1, "coop_id", state.SsCoopId);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postMemberDetail")
            {
                JspostMemberDetail();
            }
            else if (eventArg == "FilterProvince")
            {
                JsFilterProvince();
            }
            else if (eventArg == "FilterDistrict")
            {
                JsFilterDistrict();
            }
        }

        public void SaveWebSheet()
        {
            string xml_extramem = "";
            try
            {
                xml_extramem = DwMain.Describe("DataWindow.Data.XML");
                ndept.of_update_extramem(state.SsWsPass, xml_extramem);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");       
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
            DwMain.SaveDataCache();
        }

        #endregion
    }
}

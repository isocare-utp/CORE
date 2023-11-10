using System;
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
using System.Web.Services.Protocols;
//using Saving.ConstantConfig;
using CoreSavingLibrary;
using CoreSavingLibrary.WcfNDeposit;
using System.Globalization;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_examin_process : PageWebSheet, WebSheet
    {
        private DwThDate tDwHead;
        private n_depositClient depService;
        protected string xmlCalIntEsimate;
        protected String postEstimateDate;
        protected String CheckCoop;

        public string outputProcess;

        private void JsxmlCalIntEsimate()
        {
            DateTime CalIntTo = dw_head.GetItemDateTime(1, "cal_to_date");
            dw_head.SetItemString(1, "cal_to_tdate", CalIntTo.ToString("yyyyddMM", new CultureInfo("th-TH")));
            tDwHead.Eng2ThaiAllRow();
            string xml = dw_menu.Describe("DataWindow.Data.XML");
            string xml_head = dw_head.Describe("DataWindow.Data.XML");
            
          //  bool isComplete = false;
            try
            {
                //depService.RunProgressImageInt(state.SsWsPass, CalIntTo, state.SsCoopControl, xml, state.SsApplication, state.CurrentPage);
                //isComplete = true;
               // isComplete = depService.of_calint_estimate(state.SsWsPass, CalIntTo, state.SsCoopControl, xml);


                outputProcess = WebUtil.runProcessing(state, "DPINTREMAIN", xml_head, xml, "");


                //if (isComplete)
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("คำนวนประมาณการดอกเบี้ย เรียบร้อยแล้ว");
                //}
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
            xmlCalIntEsimate = WebUtil.JsPostBack(this, "xmlCalIntEsimate");
            postEstimateDate = WebUtil.JsPostBack(this, "postEstimateDate");
            CheckCoop = WebUtil.JsPostBack(this, "CheckCoop");
            //----------------------------------------------
            tDwHead = new DwThDate(dw_head, this);
            tDwHead.Add("cal_to_date", "cal_to_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            
            dw_head.SetTransaction(sqlca);
            dw_menu.SetTransaction(sqlca);

            try
            {
                depService = wcf.NDeposit;
            }
            catch
            {

            }

            if (!IsPostBack)
            {
                dw_head.InsertRow(0);
                dw_menu.InsertRow(0);
                DwListCoop.InsertRow(0);
                dw_head.SetItemDateTime(1, "cal_to_date", state.SsWorkDate);
                tDwHead.Eng2ThaiAllRow();
                try
                {
                    dw_menu.Retrieve();
                }
                catch { }
                HfCoopid.Value = state.SsCoopId;
            }
            else
            {
                this.RestoreContextDw(dw_head);
                this.RestoreContextDw(dw_menu);
                this.RestoreContextDw(DwListCoop);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "xmlCalIntEsimate")
            {
               
                JsxmlCalIntEsimate();
            }
            else if (eventArg == "CheckCoop")
            {
                checkCoop();
            }
            else if (eventArg == "postEstimateDate")
            {
                tDwHead.Eng2ThaiAllRow();
            }

        }

        private void checkCoop()
        {
            decimal i = 0;
            decimal crossflag = DwListCoop.GetItemDecimal(1, "cross_coopflag");
            if (crossflag == 1)
            {
                try
                {
                    i = DwListCoop.GetItemDecimal(1, "cross_coopflag");
                }
                catch
                { }
                DwUtil.RetrieveDDDW(DwListCoop, "dddwcoopname", "cm_constant_config.pbl", state.SsCoopControl, state.SsCoopId);
            }
            else
            {
                try
                {
                    HfCoopid.Value = state.SsCoopId + "";
                }
                catch
                { }
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_head.SaveDataCache();
            dw_menu.SaveDataCache();
            DwListCoop.SaveDataCache();
            tDwHead.Eng2ThaiAllRow();
        }

        #endregion

        //protected void cb_process_Click(object sender, EventArgs e)
        //{
        //    string xml = dw_menu.Describe("DataWindow.Data.XML");
        //    DateTime CalIntTo = dw_head.GetItemDateTime(1, "cal_to_date");
        //    bool isComplete = false;
        //    try
        //    {
        //        depService.RunProgressImageInt(state.SsWsPass, CalIntTo, state.SsCoopControl, xml, state.SsApplication, state.CurrentPage);
        //        isComplete = true;
        //        //isComplete = depService.CalIntEstimate(state.SsWsPass, dt,state.SsCoopControl, xml);
        //        if (isComplete)
        //        {
        //            LtServerMessage.Text = WebUtil.CompleteMessage("คำนวนประมาณการดอกเบี้ย เรียบร้อยแล้ว");
        //        }
        //    }
        //    catch (SoapException ex)
        //    {
        //        LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
        //    }
        //    catch (Exception ex)
        //    {
        //        LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
        //    }
        //}


    }
}

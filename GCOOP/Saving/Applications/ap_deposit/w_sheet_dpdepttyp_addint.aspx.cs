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

using CoreSavingLibrary.WcfNCommon;  //new common
using CoreSavingLibrary.WcfNDeposit; //new deposit
using System.Web.Services.Protocols;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dpdepttyp_addint : PageWebSheet,WebSheet
    {
        protected String postNewDwHead;
        protected String postRate;
        protected String DeleteRate;
        private DwThDate tDwHeadEffect;
        private DwThDate tDwHeadExprie;
        //private DepositClient depService;

        private n_depositClient ndept; //new deposit

        private void JspostRate()
        {
            string xml_head = "";
            string xml_rate = "";
            xml_head = DwHead.Describe("DataWindow.Data.XML");

            try
            {
                DwIntRate.Reset();
                xml_rate = ndept.of_initintrate_onedate(state.SsWsPass, xml_head);

                //xml_rate = ndept.of_initintrate_onedate(state.SsWsPass, xml_head);

                if (xml_rate != null && xml_rate != "")
                {
                    DwIntRate.ImportString(xml_rate, Sybase.DataWindow.FileSaveAsType.Xml);
                }
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
            postNewDwHead = WebUtil.JsPostBack(this, "postNewDwHead");
            postRate = WebUtil.JsPostBack(this, "postRate");
            DeleteRate = WebUtil.JsPostBack(this, "DeleteRate");
            tDwHeadEffect = new DwThDate(DwHead,this);
            tDwHeadEffect.Add("date_effect", "date_teffect");
            tDwHeadExprie = new DwThDate(DwHead,this);
            tDwHeadExprie.Add("exprie_date", "exprie_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            DwHead.SetTransaction(sqlca);
            DwIntRate.SetTransaction(sqlca);
            try
            {
                ndept = wcf.NDeposit;
            }
            catch
            { }

            if (!IsPostBack)
            {
                DwHead.InsertRow(0);
                try
                {
                    WebUtil.RetrieveDDDW(DwHead, "depttype_group", "dp_depttype_addint.pbl", state.SsCoopControl);
                }
                catch (Exception ex) { WebUtil.ErrorMessage(ex); }
                DwHead.SetItemDateTime(1, "date_effect", state.SsWorkDate);
                tDwHeadEffect.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(DwHead);
                this.RestoreContextDw(DwIntRate);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewDwHead")
            {
                decimal proFlag = DwHead.GetItemDecimal(1, "pro_flag");
                if (proFlag == 1)
                {
                    DwHead.SetItemDateTime(1, "exprie_date", state.SsWorkDate);
                    tDwHeadExprie.Eng2ThaiAllRow();
                }
                else
                {
                    object dateExprie = new DateTime();
                    dateExprie = null;
                    DateTime dt = (DateTime)dateExprie;
                    DwHead.SetItemDateTime(1, "exprie_date", dt);
                    tDwHeadExprie.Eng2ThaiAllRow();
                }
            }
            else if (eventArg == "postRate")
            {
                JspostRate();
            }
            else if (eventArg == "DeleteRate")
            {
                int row = Convert.ToInt16(HdRow.Value);
                DwIntRate.DeleteRow(row);
            }
        }

        public void SaveWebSheet()
        {
            string xml_head = "";
            string xml_rate = "";
            string deptTypeGrp;
            string dateEffect;
            decimal UseallFlag = DwHead.GetItemDecimal(1, "useall_flag");
            decimal proFlag = DwHead.GetItemDecimal(1, "pro_flag");
            bool checkHead = true;
            try
            {
                deptTypeGrp = DwHead.GetItemString(1, "depttype_group");

            }
            catch (Exception)
            {
                deptTypeGrp = "";
            }
            try
            {
                dateEffect = DwHead.GetItemString(1, "date_teffect");
            }
            catch (Exception)
            {
                dateEffect = "";
            }

            if (deptTypeGrp == "" || deptTypeGrp == null)
            {
                checkHead = false;
            }
            if (UseallFlag == 1)
            {
                string personGrp;
                try
                {
                    personGrp = DwHead.GetItemString(1, "person_group");
                }
                catch (Exception)
                {
                    personGrp = "";
                }
                if (personGrp == "" || personGrp == null)
                {
                    checkHead = false;
                }
            }
            if (dateEffect == "" || dateEffect == null)
            {
                checkHead = false;
            }
            if (proFlag == 1)
            {
                string exprieDate;
                try
                {
                    exprieDate = DwHead.GetItemString(1, "exprie_tdate");
                }
                catch (Exception)
                {
                    exprieDate = "";
                }
                if (exprieDate == "" || exprieDate == null)
                {
                   checkHead = false;
                }
            }
            if(checkHead == true)
            {
                xml_head = DwHead.Describe("DataWindow.Data.XML");
            }

            bool checkRate = true;
            for (int i = 1; i <= DwIntRate.RowCount; i++)
            {        
                decimal endAmt;
                decimal intRate;
                try
                {
                    endAmt = DwIntRate.GetItemDecimal(i, "end_amt");
                }
                catch (Exception)
                {
                    endAmt = 0;
                }

                try
                {
                    intRate = DwIntRate.GetItemDecimal(i, "int_rate");
                }
                catch (Exception)
                {
                    intRate = 0;
                }

                if (endAmt == 0 || intRate == 0)
                {   
                    checkRate = false;
                }
            }

            if (checkRate == true)
            {
                xml_rate = DwIntRate.Describe("DataWindow.Data.XML");
            }

            if (xml_head != "" && xml_head != null && xml_rate != "" && xml_rate != null)
            {
                try
                {
                    //depService.InsertNewRate(state.SsWsPass, xml_head, xml_rate, state.SsUsername, state.SsWorkDate);

                    ndept.of_insert_newrate_int(state.SsWsPass, xml_head, xml_rate, state.SsUsername, state.SsWorkDate);
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
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูลให้ครบ");
            }
        }

        public void WebSheetLoadEnd()
        {
            DwHead.SaveDataCache();
            DwIntRate.SaveDataCache();
        }

        #endregion
    }
}

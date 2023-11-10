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
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNFinance;
using System.Web.Services.Protocols;
using DataLibrary;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_reprintpayslip_ir : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private DwThDate tDwHead;
        protected String postPaySlipRetrieve;
        protected String postSetItem;
        protected String postPrint;


        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwHead = new DwThDate(DwHead, this);
            tDwHead.Add("adtm_date", "adtm_tdate");
            postPrint = WebUtil.JsPostBack(this, "postPrint");
            postPaySlipRetrieve = WebUtil.JsPostBack(this, "postPaySlipRetrieve");
            postSetItem = WebUtil.JsPostBack(this, "postSetItem");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                DwHead.InsertRow(0);

                DwUtil.RetrieveDDDW(DwHead, "from_system", "reprintpayslip.pbl", null);
                DwHead.SetItemDateTime(1, "adtm_date", state.SsWorkDate);
                tDwHead.Eng2ThaiAllRow();

                String ls_cond_xml = DwHead.Describe("DataWindow.Data.Xml");
                string slipXml = "";
                Int32 resultXml = fin.of_retrievereprintpayslip_ir(state.SsWsPass, state.SsCoopId, ls_cond_xml, ref slipXml);

                if (slipXml != "")
                {
                    DwList.Reset();
                    //DwList.ImportString(slipXml, FileSaveAsType.Xml);
                    DwUtil.ImportData(slipXml, DwList, tDwHead, FileSaveAsType.Xml);
                    DwList.Sort();
                }
            }
            else
            {
                this.RestoreContextDw(DwHead);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postPaySlipRetrieve":
                    PaySlipRetrieve();
                    break;
                case "postSetItem":
                    SetItem();
                    break;
                case "postPrint":
                    Print();
                    break;
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            DwHead.SaveDataCache();
            DwList.SaveDataCache();
        }

        #endregion


        private void Print()
        {
            string recOrPay = "";

            string slipno;
            decimal member_flag;
            decimal numrow = DwList.RowCount;
            member_flag = DwHead.GetItemDecimal(1, "ai_memberflag");

            //Add Arg[]
            iReportArgument args = new iReportArgument();
            iReportBuider report = new iReportBuider(this, "");

            for (int i = 1; i < numrow + 1; i++)
            {
                decimal ai_select = DwList.GetItemDecimal(i, "ai_select");

                if (ai_select > 0)
                {
                    slipno = DwList.GetItemString(i, "slip_no");
                    slipno = slipno.Trim();
                    args.Add("as_slipno", iReportArgumentType.String, slipno);

                    if (member_flag == 1)
                    {
                        report.AddCriteria("slip_fin_etc_cr", "as_slipno", ReportType.pdf, args);
                    }
                    else
                    {
                        report.AddCriteria("slip_fin_etc_cr_", "as_slipno", ReportType.pdf, args);
                    }
                }
            }
            //
            report.AutoOpenPDF = true;
            report.Retrieve();
        }

        private void SetItem()
        {
            String Coloum = HfColoum.Value;
            String NewValue = HfNewValue.Value;

            if (NewValue == "//" && Coloum == "adtm_tdate")
            {
                DwHead.SetItemNull(1, "adtm_date");
                tDwHead.Eng2ThaiAllRow();
            }
            else if (Coloum == "adtm_tdate")
            {
                DwHead.SetItemDateTime(1, "adtm_date", Convert.ToDateTime(NewValue));
            }
            else if (Coloum == "as_memberno")
            {
                String memberNo = WebUtil.MemberNoFormat(DwHead.GetItemString(1, "as_memberno"));
                DwHead.SetItemString(1, "as_memberno", memberNo);
            }
        }

        private void PaySlipRetrieve()
        {
            try
            {
                tDwHead.Eng2ThaiAllRow();
                String ls_cond_xml = DwHead.Describe("DataWindow.Data.Xml");
                string slipXml = "";
                Int32 resultXml = fin.of_retrievereprintpayslip_ir(state.SsWsPass, state.SsCoopId, ls_cond_xml,ref slipXml);
                if (slipXml != "")
                {
                    DwList.Reset();
                    //DwList.ImportString(slipXml, FileSaveAsType.Xml);
                    DwUtil.ImportData(slipXml, DwList, tDwHead, FileSaveAsType.Xml);
                    DwList.Sort();
                }
            }
            catch (Exception ex)
            {
                DwList.Reset();
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}

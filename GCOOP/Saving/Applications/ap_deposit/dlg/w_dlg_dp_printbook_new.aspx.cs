using System;
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
using Saving.WcfCommon;
using Saving.WcfDeposit;
using System.Web.Services.Protocols;
using DataLibrary;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_printbook_new : PageWebDialog, WebDialog
    {

        private DepositClient dep;
        private String deptAccountNo;
        protected String postPrintBook;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postPrintBook = WebUtil.JsPostBack(this, "postPrintBook");
        }

        public void WebDialogLoadBegin()
        {
            dep = wcf.Deposit;
            this.ConnectSQLCA();
            DwNewBook.SetTransaction(sqlca);
            DwStm.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                HdIsPostBack.Value = "false";
                try
                {

                    HdAccountNo.Value = Request["mem_no"];
                    deptAccountNo = HdAccountNo.Value;
                    DwStm.Retrieve(deptAccountNo, state.SsCoopControl);
                    String xmlInitDwNewBook = dep.InitPrintBook(state.SsWsPass, deptAccountNo, state.SsCoopId);
                    //DwPrintPrompt.InsertRow(0);
                    //string smltest = DwPrintPrompt.Describe("Datawindow.Data.Xml");
                    DwPrintPrompt.ImportString(xmlInitDwNewBook, Sybase.DataWindow.FileSaveAsType.Xml);
                }
                catch (SoapException ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
                    DwPrintPrompt.InsertRow(0);
                    DwStm.InsertRow(0);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                    DwPrintPrompt.InsertRow(0);
                    DwStm.InsertRow(0);
                }
            }
            else
            {
                HdIsPostBack.Value = "true";
                this.RestoreContextDw(DwPrintPrompt);
                this.RestoreContextDw(DwStm);
            }
            this.deptAccountNo = HdAccountNo.Value;
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postPrintBook")
            {
                try
                {
                    Int16 seq = Convert.ToInt16(DwPrintPrompt.GetItemDecimal(1, "lastrec_no_pb"));
                    Int16 page = Convert.ToInt16(DwPrintPrompt.GetItemDecimal(1, "lastpage_no_pb"));
                    Int16 line = Convert.ToInt16(DwPrintPrompt.GetItemDecimal(1, "lastline_no_pb"));

                    //XmlConfigService xml = new XmlConfigService();
                    int ai_status = 1;// xml.DepositPrintMode;
                    String as_xml_return = "";
                    string printset = state.SsPrinterSet;
                    String returnValue = dep.PrintBook(state.SsWsPass, deptAccountNo, state.SsCoopId, seq, page, line, true, state.SsPrinterSet, Convert.ToInt16(ai_status), ref as_xml_return);
                    String[] re = returnValue.Split('@');
                    int rePage = int.Parse(re[0]);
                    int reReq = int.Parse(re[1]);

                    HdIsZeroPage.Value = rePage == 0 ? "true" : "false";
                    HdIsNewBook.Value = rePage == 1 ? "true" : "false"; //เพิ่มเพื่อรับค่าจาก pb srv ว่าขึ้นเล่มใหม่
                    //HdIsNewBook.Value = "true";

                    //if (ai_status == 1)
                    //{
                    //    Printing.Print(this, "Slip/ap_deposit/PrintBook.aspx", as_xml_return, 25);
                    //}
                    //else if (ai_status == 2)
                    //{
                    Printing.DeptPrintBook(this, as_xml_return);
                    //}

                    DwPrintPrompt.SetItemDecimal(1, "lastpage_no_pb", rePage);
                    DwPrintPrompt.SetItemDecimal(1, "lastrec_no_pb", reReq);
                    DwPrintPrompt.SetItemDecimal(1, "lastline_no_pb", 1);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }

        public void WebDialogLoadEnd()
        {
            DwPrintPrompt.SaveDataCache();
        }

        #endregion
    }
}

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
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNDeposit;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_iframe_dp_printfirstpage : PageWebDialog, WebDialog
    {
        protected String printFirstPage;
        #region WebDialog Members
        private n_depositClient ndept;
        private n_commonClient ncommon;


        public void InitJsPostBack()
        {
            printFirstPage = WebUtil.JsPostBack(this, "printFirstPage");
        }

        public void WebDialogLoadBegin()
        {
            HdCloseIFrame.Value = "false";
            HdSubmit.Value = "false";
            if (!IsPostBack)
            {
                HdDeptAccountNo.Value = Request["deptAccountNo"].Trim();
                HdPassBookNo.Value = Request["deptPassBookNo"].Trim();
                HdSubmit.Value = "false";
            }
            else { }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "printFirstPage")
            {
                //    DepositClient dep = wcf.Deposit;
                //    String apvId = state.SsUsername;
                //    int normFlag = 1;
                //    try
                //    {
                //        XmlConfigService xml = new XmlConfigService();
                //        int printStatus = xml.DepositPrintMode;
                //        string xml_return = "", xml_return_bf = "";
                //        dep.PrintBookFirstPage(state.SsWsPass, state.SsApplication, HdDeptAccountNo.Value, state.SsCoopId, state.SsUsername, HdPassBookNo.Value, "00", apvId, state.SsPrinterSet, normFlag, state.SsWorkDate, 1, printStatus, ref xml_return, ref xml_return_bf);
                //        if (xml_return != "" && printStatus == 1)
                //        {
                //            Printing.Print(this, "Slip/ap_deposit/PrintBookFirstPage.aspx", xml_return, 1);
                //        }
                //        HdCloseIFrame.Value = "true";
                //    }
                //    catch (Exception ex)
                //    {
                //        Label1.Text = WebUtil.ErrorMessage(ex);
                //    }
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        #endregion

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            ndept = wcf.NDeposit;
            String apvId = state.SsUsername;
            short normFlag = 1;
            try
            {
                //XmlConfigService xml = new XmlConfigService();
                short printStatus = 1;// xml.DepositPrintMode;
                string xml_return = "", xml_return_bf = "";
                String sql =  " select MIN(DPDEPTBOOKHIS.BOOK_NO ) as BOOK_NO "+ 
                              " from DPDEPTMASTER "+ 
							  "	inner join DPDEPTTYPE "+ 
							  "	on DPDEPTMASTER.COOP_ID= DPDEPTTYPE.COOP_ID "+
							  "	and DPDEPTMASTER.DEPTTYPE_CODE=DPDEPTTYPE.DEPTTYPE_CODE "+
							  "	inner join DPDEPTBOOKHIS "+
							  "	on   DPDEPTTYPE.book_group=DPDEPTBOOKHIS.book_grp "+
                              " WHERE ( DPDEPTBOOKHIS.BOOK_TYPE	= 'B' ) "+      
                              "       AND (DPDEPTBOOKHIS.COOP_ID = '"+state.SsCoopControl+"' ) "+
                              "       AND ( DPDEPTBOOKHIS.book_status = 8 ) "+
                              "       AND (DPDEPTMASTER.DEPTACCOUNT_NO = '" + HdDeptAccountNo.Value + "')";
                try
                {
                    DataTable dt = WebUtil.Query(sql);
                    HdPassBookNo.Value = dt.Rows[0]["BOOK_NO"].ToString().Trim();
                }
                catch 
                {
                    Label1.Text = WebUtil.ErrorMessage("เลขสมุดไม่พอในการทำรายการ");
                }



                //ndept.PrintBookFirstPage(state.SsWsPass, state.SsApplication, HdDeptAccountNo.Value, state.SsCoopId, state.SsUsername, HdPassBookNo.Value, "00", apvId, state.SsPrinterSet, normFlag, state.SsWorkDate, 0, printStatus, ref xml_return, ref xml_return_bf);
                ndept.of_print_book_firstpage(state.SsWsPass, HdDeptAccountNo.Value, state.SsCoopId, state.SsWorkDate, state.SsUsername, HdPassBookNo.Value, "00", apvId, normFlag, state.SsPrinterSet, 0, printStatus, ref xml_return);
                if (xml_return != "" && printStatus == 1)
                {
                    //if (printStatus == 1)
                    //{
                    //    Printing.Print(this, "Slip/ap_deposit/PrintBookFirstPage.aspx", xml_return, 1);
                    //}
                    //else
                    //{
                    Printing.DeptPrintBookFirstPage(this, xml_return);
                    //}
                }
                HdCloseIFrame.Value = "true";
                HdSubmit.Value = "true";
            }
            catch (Exception ex)
            {
                Label1.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}

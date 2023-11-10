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
using CoreSavingLibrary.WcfNCommon;
//using Saving.WcfNShrlon;
using CoreSavingLibrary.WcfNDeposit;
using System.Web.Services.Protocols;
using System.ServiceModel.Channels;
using System.Xml;
using DataLibrary;
using CoreSavingLibrary;


namespace Saving.Applications.mbshr
{
    public partial class w_sheet_dp_conteck_new : PageWebSheet, WebSheet
    {

        
        private String pbl = "dp_conteck.pbl";
        protected String postAccountNo;
        protected String jsPostPrintNewBook;
        //private DepositClient DepService;
        private n_depositClient depService;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postAccountNo = WebUtil.JsPostBack(this, "postAccountNo");
            jsPostPrintNewBook = WebUtil.JsPostBack(this, "jsPostPrintNewBook");
        }

        public void WebSheetLoadBegin()
        {
            depService = wcf.NDeposit;
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            DwDetail.SetTransaction(sqlca);
            
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                //DwMain.SetItemString(1, "coop_id", state.SsCoopControl);
                
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }
        }

        public void SaveWebSheet()
        {
           
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postAccountNo")
            {
                JsPostAccountNo();
            }
            else if (eventArg == "jsPostPrintNewBook")
            {
                PostPrintNewBook();
            }
        }

        public void WebSheetLoadEnd()
        {

           
            //DwDetail.PageNavigationBarSettings.Visible = (DwDetail.RowCount > 10);
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }

        #endregion

        private void JsPostAccountNo()
        {
          
            String member_no = DwMain.GetItemString(1, "check_no");
            String sql;
            
            
            Hdmem_no.Value = member_no;
            if (member_no.Length < 10)
            {
                for (int i = member_no.Length; i < 8; i++)
                   {
                       member_no = ("0" + member_no);
                   }
                    Hdmem_no.Value = member_no;
                    DwUtil.RetrieveDataWindow(DwMain, pbl, null, state.SsCoopControl, member_no);
                    DwUtil.RetrieveDataWindow(DwDetail, pbl, null, state.SsCoopControl, member_no);

                
            }
            else
            {
                sql = @"select member_no from shsharemaster where sharepassbook_no = '" + member_no + "'";
                Sdt result = WebUtil.QuerySdt(sql);
                if (result.Next())
                {
                    string mem_no = result.GetString("member_no");
                    Hdmem_no.Value = mem_no;
                    DwUtil.RetrieveDataWindow(DwMain, pbl, null, state.SsCoopControl, mem_no);
                    DwUtil.RetrieveDataWindow(DwDetail, pbl, null, state.SsCoopControl, mem_no);
                }
            }

            DwMain.SetItemString(1, "check_no", Hdmem_no.Value);
        }

        private void PostPrintNewBook()
        {
            try
            {
                string as_member_no = Hdmem_no.Value;
                DateTime adtm_date = state.SsWorkDate;
                string as_xml_return = "";
                String as_entryid = "";
                //short ai_reprint = 0 ;
                String as_newpassbook_no = "";
                String as_reson = "";



                int result = depService.of_print_bookshare_firstpage(state.SsWsPass, as_member_no, state.SsWorkDate, as_entryid, ref as_xml_return, 0, as_newpassbook_no, as_reson);

                Printing.Shareprintbookfristpage(this, as_xml_return);
            }
            catch { }
        }
    }
}

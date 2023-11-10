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
using CoreSavingLibrary.WcfNKeeping;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_det_detail : PageWebSheet,WebSheet
    {
        private n_keepingClient keepingService;
        public String pbl = "kp_det_detail.pbl";
        protected String postNewClear;
        protected String postInit;
        protected String postInitMember;
        //===============================
        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postInitMember = WebUtil.JsPostBack(this, "postInitMember");
        }

        public void WebSheetLoadBegin()
        {
            keepingService = wcf.NKeeping;
            this.ConnectSQLCA();
            Dw_detail.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_detail);
            }
        }

        public void CheckJsPostBack(String eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postInit")
            {
                JspostInit();
            }
            else if (eventArg == "postInitMember")
            {
                JspostInitMember();
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
        }

        //===============================
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_detail.Reset();
            
        }

        private void JspostInit()
        {
            try 
            {
                String member_no = Dw_main.GetItemString(1, "member_no");
                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);

                str_keep_detail astr_keep_detail = new str_keep_detail();
                astr_keep_detail.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                
                int result = keepingService.of_init_kpmast_detail(state.SsWsPass, ref astr_keep_detail);
                if (result == 1)
                {
                    DwUtil.ImportData(astr_keep_detail.xml_main, Dw_main, null, FileSaveAsType.Xml);
                    Dw_detail.SetSqlSelect(astr_keep_detail.sql_select_detail);
                    Dw_detail.Retrieve();
                } 
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostInitMember()
        {
            try
            {
                String member_no = Hfmember_no.Value.Trim();
                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);

                str_keep_detail astr_keep_detail = new str_keep_detail();
                astr_keep_detail.xml_main = Dw_main.Describe("DataWindow.Data.XML");

                int result = keepingService.of_init_kpmast_detail(state.SsWsPass, ref astr_keep_detail);
                if (result == 1)
                {
                    DwUtil.ImportData(astr_keep_detail.xml_main, Dw_main, null, FileSaveAsType.Xml);
                    Dw_detail.SetSqlSelect(astr_keep_detail.sql_select_detail);
                    Dw_detail.Retrieve();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }


    }
}
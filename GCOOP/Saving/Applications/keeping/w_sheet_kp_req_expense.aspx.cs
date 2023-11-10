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
using CoreSavingLibrary.WcfNKeeping;
using Sybase.DataWindow;
using DataLibrary;


namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_req_expense : PageWebSheet, WebSheet
    {
        public String pbl = "kp_req_expense.pbl";
        private DwThDate tdwmain;
        protected String jsPostMember;
        protected String jsPostGroup;
        protected String newClear;
        protected String jsRefresh;
        protected String jsmembgroup_code;
        protected String jsCoopSelect;
        protected String jsChangmidgroupcontrol;
        protected String jsRetreiveDwdetail;
        protected String jsChangeEnpenseBank;
        protected String jsAddBtn;
        


        void WebSheet.InitJsPostBack()
        {
            DwUtil.RetrieveDDDW(dw_detail, "moneytype_code", pbl, null);
            DwUtil.RetrieveDDDW(dw_detail, "expense_bank", pbl, null);
            //DwUtil.RetrieveDDDW(dw_detail, "expense_branch", pbl, null);
            //DwUtil.RetrieveDDDW(dw_detail, "monthlycut_type", pbl, null);

            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            newClear = WebUtil.JsPostBack(this, "newClear");
            jsChangmidgroupcontrol = WebUtil.JsPostBack(this, "jsChangmidgroupcontrol");
            jsmembgroup_code = WebUtil.JsPostBack(this, "jsmembgroup_code");
            jsCoopSelect = WebUtil.JsPostBack(this, "jsCoopSelect");
            jsRetreiveDwdetail = WebUtil.JsPostBack(this, "jsRetreiveDwdetail");
            jsChangeEnpenseBank = WebUtil.JsPostBack(this, "jsChangeEnpenseBank");
            jsRefresh = WebUtil.JsPostBack(this, "jsRefresh");
            jsAddBtn = WebUtil.JsPostBack(this, "jsAddBtn");
            tdwmain = new DwThDate(dw_main, this);
            tdwmain.Add("operate_date", "operate_tdate");

        }

        void WebSheet.WebSheetLoadBegin()
        {

            this.ConnectSQLCA();
            dw_main.SetTransaction(sqlca);
            dw_detail.SetTransaction(sqlca);


            if (IsPostBack)
            {
                try
                {

                    this.RestoreContextDw(dw_main);
                    this.RestoreContextDw(dw_detail);

                    HdIsPostBack.Value = "true";
                }
                catch { }
            }
            else
            {
                dw_main.InsertRow(0);
                dw_detail.InsertRow(0);
                dw_main.SetItemDate(1, "operate_date", state.SsWorkDate);
                dw_main.SetItemString(1, "entry_id", state.SsUsername);
                tdwmain.Eng2ThaiAllRow();
                HdIsPostBack.Value = "False";

            }
        }

        void WebSheet.CheckJsPostBack(String eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
            else if (eventArg == "jsRetreiveDwdetail")
            {
                JsRetreiveDwdetail();

            }
            else if (eventArg == "jsChangeEnpenseBank")
            {
                JsChangeEnpenseBank();

            }
            else if (eventArg == "jsAddBtn")
            {
                JsAddBtn();

            }
            else if (eventArg == "jsRefresh")
            {
                JsRefresh();

            }


        }

        private void JsRefresh()
        {
            
        }

        private void JsAddBtn()
        {
            dw_detail.InsertRow(0);
            int rowdetail = dw_detail.RowCount;
            //int rowlist = Convert.ToInt32(HiddenRowlist.Value);
            dw_detail.SetItemDecimal(rowdetail, "chg_status", 0);
            string member_no = dw_main.GetItemString(1, "member_no");
            

        }

        private void JsChangeEnpenseBank()
        {
            int rowdetail = Convert.ToInt32(HiddenRowdetail.Value);
            string expensebank = dw_detail.GetItemString(rowdetail, "expense_bank");
            DwUtil.RetrieveDDDW(dw_detail, "expense_branch", pbl, expensebank);
        }

        private void JsRetreiveDwdetail()
        {
            int rowlist = Convert.ToInt32(HiddenRowlist.Value);
            String coop_id = state.SsCoopControl;
            String member_no = dw_main.GetItemString(1, "member_no");
            dw_detail.Retrieve(coop_id, member_no);
        }

        void WebSheet.SaveWebSheet()
        {
            int j = 0; int k = 0;

            try
            {
                str_keep_request strSaveReqExpense = new str_keep_request();
                strSaveReqExpense.xml_main = dw_main.Describe("DataWindow.Data.XML");
                strSaveReqExpense.xml_detail = dw_detail.Describe("DataWindow.Data.XML");
                int result = wcf.NKeeping.of_save_req_expense(state.SsWsPass,ref strSaveReqExpense);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                JsNewClear();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }


        }

        void WebSheet.WebSheetLoadEnd()
        {

            dw_main.SaveDataCache();
            dw_detail.SaveDataCache();

        }
        private void JsPostMember()
        {
            String memcoop_id = "";
            String member_no = dw_main.GetItemString(1, "member_no");

            if (1 == 1)
            {
                memcoop_id = state.SsCoopControl;

            }
            else
            {
                memcoop_id = state.SsCoopId;
            }

            dw_main.SetItemString(1, "memcoop_id", memcoop_id);

            try
            {
                str_keep_request strInitReqExpense = new str_keep_request();
                strInitReqExpense.xml_main = dw_main.Describe("DataWindow.Data.XML");
                strInitReqExpense.xml_detail = dw_detail.Describe("DataWindow.Data.XML");
                int result = wcf.NKeeping.of_init_req_expense(state.SsWsPass, ref strInitReqExpense);
                dw_main.Reset();
                dw_detail.Reset();
                DwUtil.ImportData(strInitReqExpense.xml_main, dw_main, tdwmain, FileSaveAsType.Xml);
                DwUtil.ImportData(strInitReqExpense.xml_detail, dw_detail, null, FileSaveAsType.Xml);
                dw_main.SetItemDecimal(1, "approve_status", 1);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }
        }
        //JS-EVENT
        private void JsNewClear()
        {
            dw_main.Reset();
            dw_detail.Reset();
            dw_main.InsertRow(0);
            dw_detail.InsertRow(0);

            HdIsPostBack.Value = "False";
        }
    }
}

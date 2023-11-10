using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using System.Threading;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_member_fund : PageWebSheet, WebSheet
    {
        private DwThDate tDwMain;
        private DwThDate tDwStateList;
        protected String changeGetMember;
        protected String pbl = "sl_member_fund.pbl";
        public void InitJsPostBack()
        {
            changeGetMember = WebUtil.JsPostBack(this, "changeGetMember");

            tDwMain = new DwThDate(dw_main, this);
            tDwMain.Add("birth_date", "birth_tdate");
            tDwMain.Add("member_date", "member_tdate");
            tDwMain.Add("resign_date", "resign_tdate");
            tDwMain.Add("retry_date", "retry_tdate");
            tDwMain.Add("dead_date", "dead_tdate");
            tDwMain.Add("approved_date", "approved_tdate");
            tDwMain.Add("stoppay_date", "stoppay_tdate");
            tDwMain.Add("lastprocess_date", "lastprocess_tdate");
            tDwMain.Add("lastkeep_date", "lastkeep_tdate");
            tDwMain.Add("receive_date", "receive_tdate");

            tDwStateList = new DwThDate(dw_statelist, this);
            tDwStateList.Add("ref_date", "ref_tdate");
            tDwStateList.Add("desc_date", "desc_tdate");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dw_main.InsertRow(0);
                dw_statelist.InsertRow(0);
                dw_gaindetail.InsertRow(0);
                DwUtil.RetrieveDDDW(dw_main, "resign_cause", pbl, state.SsCoopControl);
                DwUtil.RetrieveDDDW(dw_gaindetail, "relation_gain", pbl,null);

            }
            else {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_statelist);
                this.RestoreContextDw(dw_gaindetail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
           switch (eventArg) {
               case "changeGetMember":
                   getMember();
                   break;

            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
            dw_statelist.SaveDataCache();
            dw_gaindetail.SaveDataCache();
        }

        private void getMember() {
            String memb_no = hdMemb_no.Value;
            //string sqldapt = @"select * from mbmembmaster where coop_id = " + state.SsCoopId + " and member_no = '" + memb_no + "'"; //เพิ่มเงื่อนไข คือ and wftype_code = '01' โดยดึงมาเฉพาะประเภทของสมาชิกเท่านั้น
            //Sdt dtdapt = WebUtil.QuerySdt(sqldapt);
            //if (dtdapt.Next())
            //{
            //    String member_name = dtdapt.GetString("member_name");
            //    dw_main.SetItemString(1, "deptaccount_no", member_name);
            //}
            try
            {
                DwUtil.RetrieveDataWindow(dw_main, pbl, tDwMain, memb_no);
                tDwMain.Eng2ThaiAllRow();
            }
            catch {
                dw_main.InsertRow(0);
            }
            DwUtil.RetrieveDataWindow(dw_statelist, pbl, tDwStateList, memb_no);
            tDwStateList.Eng2ThaiAllRow();

            DwUtil.RetrieveDataWindow(dw_gaindetail, pbl, null, memb_no);
        }
    }
}
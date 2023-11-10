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

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_slip_search_tran : PageWebDialog,WebDialog
    {
        protected String SlipSearch;
        protected String postDwMain;
        private DwThDate tDwMain;
        

        #region WebDialog Members

        public void InitJsPostBack()
        {
            SlipSearch = WebUtil.JsPostBack(this, "SlipSearch");
            postDwMain = WebUtil.JsPostBack(this, "postDwMain");

            tDwMain = new DwThDate(DwMain, this);
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("slip_date", "slip_tdate");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            DwList.SetTransaction(sqlca);
            if (IsPostBack)
            {
                this.RestoreContextDw(DwMain);
            }
            else
            {
                //try
                //{
                //    DwMain.SetItemDateTime(1, "slip_date", state.SsWorkDate);
                //    tDwMain.Eng2ThaiAllRow();
                //}
                //catch (Exception ex)
                //{
                //}
            }
            if (DwMain.RowCount < 1)
            {
                DwMain.InsertRow(0);
                try
                {
                    DwMain.SetItemDateTime(1, "slip_date", state.SsWorkDate);
                    tDwMain.Eng2ThaiAllRow();
                }
                catch (Exception ex)
                {
                }
            }
            if (!hidden_search.Value.Equals(""))
            {
                DwList.SetSqlSelect(hidden_search.Value);
                DwList.Retrieve();
            }

            if (!IsPostBack) {
                string member_no = "";

                try
                {
                    member_no = Request["member_no"];
                }
                catch
                { }

                DwMain.SetItemString(1, "member_no", member_no);
            
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "SlipSearch")
            {
                JsSlipSearch();
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        #endregion

        private void JsSlipSearch()
        {
            String ls_accno = "", ls_accname = "", ls_acctype = "";
            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_memgrpno = "", ls_memgrpname = "", ls_slipdate = "";
            String ls_sql = "", ls_sqlext = "", ls_temp = "";

            ls_sql = @"  SELECT DPDEPTSLIP.DEPTSLIP_DATE,   
         DPDEPTSLIP.RECPPAYTYPE_CODE,   
         DPDEPTSLIP.DEPTSLIP_AMT,   
         DPDEPTSLIP.ENTRY_ID,   
         DPDEPTMASTER.DEPTACCOUNT_NO,   
         DPDEPTMASTER.MEMBER_NO,   
         DPDEPTMASTER.DEPTACCOUNT_NAME,   
         DPDEPTMASTER.DEPTTYPE_CODE,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         DPDEPTMASTER.DEPT_OBJECTIVE,   
         DPDEPTSLIP.ENTRY_DATE,   
         DPDEPTSLIP.DEPTSLIP_NETAMT,   
         DPDEPTSLIP.ITEM_STATUS,   
         DPDEPTMASTER.COOP_ID,   
         DPDEPTSLIP.COOP_ID,   
         DPDEPTSLIP.DEPTSLIP_NO,   
         DPDEPTSLIP.DEPTCOOP_ID,   
         DPDEPTMASTER.CONDFORWITHDRAW,   
         mbmembmaster.membgroup_code+ ' ' +mbucfmembgroup.membgroup_desc as memb_group  
    FROM DPDEPTSLIP, 
         DPUCFRECPPAYTYPE,   
         MBUCFMEMBGROUP ,
MBMEMBMASTER LEFT JOIN DPDEPTMASTER on dpdeptmaster.member_no = mbmembmaster.member_no
   WHERE ( DPDEPTSLIP.DEPTACCOUNT_NO = DPDEPTMASTER.DEPTACCOUNT_NO ) and  
         ( DPDEPTSLIP.RECPPAYTYPE_CODE = DPUCFRECPPAYTYPE.RECPPAYTYPE_CODE ) and  
         ( DPDEPTMASTER.COOP_ID = DPDEPTSLIP.DEPTCOOP_ID ) and  
         ( MBMEMBMASTER.COOP_ID = DPDEPTMASTER.MEMCOOP_ID ) and  
         ( MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP.COOP_ID ) and  
         ( MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE )";
            //accno
            try
            {
                ls_accno = DwMain.GetItemString(1, "account_no").Trim();
            }
            catch { ls_accno = ""; }
            //accname
            try
            {
                ls_accname = DwMain.GetItemString(1, "account_name").Trim();
            }
            catch { ls_accname = ""; }
            //acctype
            try
            {
                ls_acctype = DwMain.GetItemString(1, "account_type").Trim();
            }
            catch { ls_acctype = ""; }
            //member_no
            try
            {
                ls_memno = DwMain.GetItemString(1, "member_no").Trim();
            }
            catch { ls_memno = ""; }
            //memb_name
            try
            {
                ls_memname = DwMain.GetItemString(1, "member_name").Trim();
            }
            catch { ls_memname = ""; }
            //memb_surname
            try
            {
                ls_memsurname = DwMain.GetItemString(1, "member_surname").Trim();
            }
            catch { ls_memsurname = ""; }
            //memgrpno
            try
            {
                ls_memgrpno = DwMain.GetItemString(1, "member_group_no_1").Trim();
            }
            catch { ls_memgrpno = ""; }
            //memgrpname
            try
            {
                ls_memgrpname = DwMain.GetItemString(1, "member_group_no").Trim();
            }
            catch { ls_memgrpname = ""; }
            //slipdate
            try
            {
                ls_slipdate = DwMain.GetItemDateTime(1, "slip_date").ToString("MM/dd/yyyy");
            }
            catch { ls_slipdate = ""; }

            if (ls_accno.Length > 0)
            {
                ls_sqlext += " and ( dpdeptmaster.deptaccount_no like '" + ls_accno + "%') ";
            }
            if (ls_accname.Length > 0)
            {
                ls_sqlext += " and ( dpdeptmaster.deptaccount_name like '" + ls_accname + "%') ";
            }
            if (ls_acctype.Length > 0)
            {
                ls_sqlext += " and ( dpdeptmaster.depttype_code = '" + ls_acctype + "') ";
            }
            if (ls_memno.Length > 0)
            {
                ls_sqlext += " and (  dpdeptmaster.member_no like '" + ls_memno + "%') ";
            }
            if (ls_memname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_name like '" + ls_memname + "%') ";
            }
            if (ls_memsurname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_surname like '" + ls_memsurname + "%') ";
            }
            if (ls_memgrpno.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.membgroup_code like '" + ls_memgrpno + "%') ";
            }
            if( ls_slipdate.Length > 0)
            {
                ls_sqlext += " and ( dpdeptslip.deptslip_date = '" + ls_slipdate + "')";
            }

            ls_sqlext += " and ( dpdeptslip.cash_type = 'TRN') and (  DPUCFRECPPAYTYPE.GROUP_ITEMTPE in ('CLS','WID') ) ";
            ls_sqlext += " and ( dpdeptslip.coop_id = '" + state.SsCoopId +"' ) ";
            ls_sqlext += " and ( dpdeptslip.item_status = 1 ) ";

            ls_temp = ls_sql + ls_sqlext;
            hidden_search.Value = ls_temp;
            DwList.SetSqlSelect(hidden_search.Value);
            DwList.Retrieve();
        }
    }
}

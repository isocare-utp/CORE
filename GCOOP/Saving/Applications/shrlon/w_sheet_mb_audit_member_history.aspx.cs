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
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNShrlon;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_mb_audit_member_history : PageWebSheet, WebSheet
    {

        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsGetData;
        protected String jsGetDataList;
        protected String jsGetDataselect;
        private DwThDate thDwlist;
        private DwThDate thDwsearch;
      
        public void InitJsPostBack()
        {
            jsGetData = WebUtil.JsPostBack(this, "jsGetData");
            jsGetDataList = WebUtil.JsPostBack(this, "jsGetDataList");
            jsGetDataselect = WebUtil.JsPostBack(this, "jsGetDataselect");
            thDwlist = new DwThDate(dw_list, this);
            thDwlist.Add("modified_date", "modified_tdate");
            thDwsearch = new DwThDate(dw_search, this);
            thDwsearch.Add("start_date", "start_tdate");
            thDwsearch.Add("end_date", "end_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {

                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();
            if (IsPostBack)
            {

                try
                {
                    dw_search.RestoreContext();
                    dw_list.RestoreContext();
                    dw_detail.RestoreContext();

                }
                catch { }
            }
            if (dw_search.RowCount < 1)
            {
                dw_search.InsertRow(0);
                dw_detail.InsertRow(0);
                dw_list.InsertRow(0);
                //dw_search.SetItemDate(1, "start_date", state.SsWorkDate);
                //dw_search.SetItemDate(1, "end_date", state.SsWorkDate);
                thDwsearch.Eng2ThaiAllRow();
            }
            if (!hidden_search.Value.Equals(""))
            {
                JsGetDataselect();
            }
        }


        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsGetData")
            {
                JsGetData();

            }
            else if (eventArg == "jsGetDataList")
            {
                JsGetDataList();
            }
            else if (eventArg == "jsGetDataselect")
            {
                JsGetDataselect();
            }
        }

        public void SaveWebSheet()
        {
            throw new NotImplementedException();
        }

        public void WebSheetLoadEnd()
        {
            thDwsearch.Eng2ThaiAllRow();
        }

        private void JsGetData()
        {
            try
            {

                String sql = @"   SELECT WEBAUDIT.AUDIT_NO,   WEBAUDIT.TAB_NAME,      WEBAUDIT.PK_NAME,    WEBAUDIT.PK_DESC,   
                                  WEBAUDIT.PK_VALUE,    WEBAUDIT.MODIFIED_DATE,  WEBAUDIT.MODIFIED_USER,    WEBAUDIT.MODIFIED_APP,  
                                  MBMEMBMASTER.MEMB_NAME,    MBMEMBMASTER.MEMB_SURNAME,     MBMEMBMASTER.MEMBER_NO  ,'          ' as  modified_tdate
                                  FROM WEBAUDIT,      MBMEMBMASTER    WHERE WEBAUDIT.PK_VALUE=MBMEMBMASTER.MEMBER_NO 
                                  and  WEBAUDIT.TAB_NAME = 'MBMEMBMASTER'   and  WEBAUDIT.MODIFIED_APP='shrlon'  ";
                DataTable dt = WebUtil.Query(sql);

                try
                {
                    dw_list.Reset();
                    DwUtil.ImportData(dt, dw_list, thDwlist);
                }
                catch { }
                if (dw_search.RowCount > 1)
                {
                    int row = dw_search.RowCount - 1;
                    dw_search.DeleteRow(dw_search.RowCount - row);
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

        private void JsGetDataList()
        {
            try
            {

               Int32 audit_no = Convert.ToInt32(Hlist.Value);
                String sql = @"SELECT WEBAUDITDETAIL.COL_NAME,      WEBAUDITDETAIL.NEW_VALUE,      
                             WEBAUDITDETAIL.OLD_VALUE,      WEBAUDITDETAIL.TAB_NAME   FROM WEBAUDITDETAIL  
                             WHERE WEBAUDITDETAIL.AUDIT_NO = " + audit_no + "  ORDER BY WEBAUDITDETAIL.TAB_NAME ASC, WEBAUDITDETAIL.COL_NAME ASC   ";
                DataTable dt = WebUtil.Query(sql);

                try
                {
                    DwUtil.ImportData(dt, dw_detail, null);
                }
                catch { }

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

        protected void JsGetDataselect()
        {
            try
            {
                thDwsearch.Eng2ThaiAllRow();
                String entry_end = "", entry_start = "";
                String ls_memno = "", ls_apvid = "", ls_start = "", ls_end="";
                String ls_sql = "", ls_sqlext = "";
                ls_sql = dw_list.GetSqlSelect();
                try
                {
                    ls_memno = dw_search.GetItemString(1, "member_no").Trim();

                }
                catch { ls_memno = ""; }
                try
                {
                    ls_apvid = dw_search.GetItemString(1, "apvid").Trim();

                }
                catch { ls_apvid = ""; }
                try
                {
                    ls_start = dw_search.GetItemString(1, "start_tdate");
                    String year = WebUtil.Right(ls_start, 4);
                    int iyear = Convert.ToInt32(year) - 543;
                    String entry_day = WebUtil.Left(ls_start, 6);
                    entry_start = entry_day + iyear.ToString();

                }
                catch { ls_start = ""; }
                try
                {
                    ls_end = dw_search.GetItemString(1, "end_tdate");
                    String year = WebUtil.Right(ls_end, 4);
                    int iyear = Convert.ToInt32(year) - 543;
                    String entry_day = WebUtil.Left(ls_end, 6);
                    entry_end = entry_day + iyear.ToString();

                }
                catch { ls_end = ""; }

                if (ls_memno.Length > 0)
                {
                    ls_sqlext = " and (  mbmembmaster.member_no ='" + ls_memno + "') ";
                }
                if (ls_apvid.Length > 0)
                {
                    ls_sqlext += " and (  webaudit.modified_user = '" + ls_apvid + "') ";
                }
                if (ls_start.Length > 0 && ls_end.Length > 0)
                {
                    ls_sqlext += " and ( webaudit.modified_date >=  to_date('" + entry_start + "','dd/mm/yyyy') and webaudit.modified_date <=  to_date('" + entry_end + "','dd/mm/yyyy')) ";
                }


            
                String sql = @"   SELECT WEBAUDIT.AUDIT_NO,   WEBAUDIT.TAB_NAME,      WEBAUDIT.PK_NAME,    WEBAUDIT.PK_DESC,   
                                  WEBAUDIT.PK_VALUE,    WEBAUDIT.MODIFIED_DATE,  WEBAUDIT.MODIFIED_USER,    WEBAUDIT.MODIFIED_APP,  
                                  MBMEMBMASTER.MEMB_NAME,    MBMEMBMASTER.MEMB_SURNAME,     MBMEMBMASTER.MEMBER_NO  ,'          ' as  modified_tdate
                                  FROM WEBAUDIT,      MBMEMBMASTER    WHERE WEBAUDIT.PK_VALUE=MBMEMBMASTER.MEMBER_NO 
                                  and  WEBAUDIT.TAB_NAME = 'MBMEMBMASTER'   and  WEBAUDIT.MODIFIED_APP='shrlon'  ";
                hidden_search.Value = sql + ls_sqlext;
                DataTable dt = WebUtil.Query(hidden_search.Value);

                try
                {
                    dw_list.Reset();
                    DwUtil.ImportData(dt, dw_list, thDwlist);
                }
                catch { }

                if (dw_search.RowCount > 1)
                {
                    int row = dw_search.RowCount - 1;
                    dw_search.DeleteRow(dw_search.RowCount - row);
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
    }
}

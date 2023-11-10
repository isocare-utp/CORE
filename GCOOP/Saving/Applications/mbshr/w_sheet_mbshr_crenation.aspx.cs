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
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mbshr_crenation : PageWebSheet, WebSheet
    {
        public String pbl = "mb_req_trnmb.pbl";
        private DwThDate tdwlist;
        private DwThDate tdwlist2;
        protected String postNewClear;
        protected String postMemberNo;
        protected String postCmAccount;
        protected String postMember_delete;
        protected String postMember_delete_2;

        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postMemberNo = WebUtil.JsPostBack(this, "postMemberNo");
            postCmAccount = WebUtil.JsPostBack(this, "postCmAccount");
            postMember_delete = WebUtil.JsPostBack(this, "postMember_delete");
            postMember_delete_2 = WebUtil.JsPostBack(this, "postMember_delete_2");

            //tdwlist = new DwThDate(dw_list, this);
            //tdwlist.Add("cm_date", "cm_tdate");
            //tdwlist2 = new DwThDate(dw_list2, this);
            //tdwlist2.Add("cm_date2", "cm_tdate2");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                this.ConnectSQLCA();

                if (!IsPostBack)
                {
                    JspostNewClear();
                }
                else
                {
                    dw_main.RestoreContext();
                    dw_list.RestoreContext();
                    dw_list2.RestoreContext();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postMemberNo")
            {
                JsPostMemberNo(null);
            }
            else if (eventArg == "postCmAccount")
            {
                JsPostCmAccount();
            }
            else if (eventArg == "postMember_delete")
            {
                JspostMember_delete();
            }
            else if (eventArg == "postMember_delete_2")
            {
                JspostMember_delete_2();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                string coop_id = state.SsCoopId;
                string member_no = dw_main.GetItemString(1, "member_no");
                string fullname = dw_main.GetItemString(1, "c_membname");
                DateTime work_Date = state.SsWorkDate;
                string sqlStr = "";
                //clear data ประกัน
                sqlStr = @"delete from mbcremationthai where  CMTTYPE_CODE<>'99' and  coop_id = {0} and  member_no ={1}";
                sqlStr = WebUtil.SQLFormat(sqlStr, coop_id, member_no);
                WebUtil.ExeSQL(sqlStr);

                //บันทึกประกัน
                if (dw_list.RowCount > 0)
                {
                    for (int i = 1; i <= dw_list.RowCount; i++)
                    {
                        string ls_cretype = dw_list.GetItemString(i, "cmttype_code");
                        string ls_crename = dw_list.GetItemString(i, "cmtaccount_name");
                        decimal ld_creamt = dw_list.GetItemDecimal(i, "cremation_amt");
                        try
                        {
                            sqlStr = @"insert into mbcremationthai
                           ( COOP_ID,                CMTTYPE_CODE,           MEMBER_NO,   
                           CMTACCOUNT_NAME,          CMTCLOSE_STATUS,        CREMATION_AMT  ,APPLY_DATE ) 
                           values
                           (    {0}                 ,{1}                        ,{2}
                                ,{3}                ,{4}                        ,{5}        ,{6})";
                            sqlStr = WebUtil.SQLFormat(sqlStr
                                , coop_id, ls_cretype, member_no
                                , ls_crename, 1, ld_creamt, work_Date);
                            WebUtil.ExeSQL(sqlStr);
                        }
                        catch
                        {
                            sqlStr = @"update mbcremationthai 
                            set CMTACCOUNT_NAME = {3} , CREMATION_AMT = {4} , APPLY_DATE = {5}
                            where COOP_ID = {0} and MEMBER_NO={1} and CMTTYPE_CODE = {2}";
                            sqlStr = WebUtil.SQLFormat(sqlStr, coop_id, member_no, ls_cretype, ls_crename, ld_creamt, work_Date);
                            WebUtil.ExeSQL(sqlStr);
                        }
                    }
                }
                //กองทุน
                if (dw_list2.RowCount > 0)
                {
                    decimal ld_creamt = dw_list2.GetItemDecimal(1, "cremation_amt");
                    try
                    {
                        sqlStr = @"insert into mbcremationthai
                           ( COOP_ID,                CMTTYPE_CODE,           MEMBER_NO,   
                             CMTCLOSE_STATUS,        CREMATION_AMT ,         APPLY_DATE ) 
                           values
                           (    {0}                 ,{1}                        ,{2}
                                ,{3}                ,{4}                        ,{5})";
                        sqlStr = WebUtil.SQLFormat(sqlStr
                                , coop_id, "99", member_no
                                , 1, ld_creamt, work_Date);
                        WebUtil.ExeSQL(sqlStr);

                    }
                    catch (Exception ex)
                    {
                        sqlStr = @"update mbcremationthai 
                            set CREMATION_AMT = {2} , APPLY_DATE = {3}
                            where COOP_ID = {0} and MEMBER_NO={1} and CMTTYPE_CODE = '99' ";
                        sqlStr = WebUtil.SQLFormat(sqlStr, coop_id, member_no, ld_creamt, work_Date);
                        WebUtil.ExeSQL(sqlStr);
                    }
                    //update mbmembmaster
                    sqlStr = @"update mbmembmaster 
                            set hgr_amt = {2} 
                            where COOP_ID = {0} and MEMBER_NO={1}  ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, coop_id, member_no, ld_creamt);
                    WebUtil.ExeSQL(sqlStr);
                }

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                JspostNewClear();
                JsPostMemberNo(member_no);
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

        }

        public void WebSheetLoadEnd()
        {
            dw_list.SaveDataCache();
            dw_list2.SaveDataCache();
            //tdwlist.Eng2ThaiAllRow();
            //tdwlist2.Eng2ThaiAllRow();
        }

        private void JspostNewClear()
        {
            dw_main.Reset();
            dw_main.InsertRow(0);
            dw_list.Reset();
            dw_list2.Reset();
        }

        private void JsPostMemberNo(string member_no)
        {
            String memberNo = "";
            if (member_no == null)
            {
                memberNo = dw_main.GetItemString(1, "member_no");
            }
            else
            {
                memberNo = member_no;
            }
            memberNo = WebUtil.MemberNoFormat(memberNo.Trim());
            string coop_id = state.SsCoopId;
            DwUtil.RetrieveDataWindow(dw_main, pbl, null, coop_id, memberNo);
            if (dw_main.RowCount > 0)
            {
                DwUtil.RetrieveDataWindow(dw_list, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_list2, pbl, null, coop_id, memberNo);
                if (dw_list2.RowCount == 0)
                {
                    dw_list2.InsertRow(0);
                    dw_list2.SetItemDecimal(1, "cremation_amt", 0);
                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขสมาชิก " + memberNo);
                this.JspostNewClear();
            }
        }

        private void JsPostCmAccount()
        {
            int row_coll = Convert.ToInt32(Hdlist_row.Value);
            string cmaccount = WebUtil.MemberNoFormat(dw_list.GetItemString(row_coll, "cmtaccount_no"));
            dw_list.SetItemString(row_coll, "cmtaccount_no", cmaccount);
        }

        private void JspostMember_delete()
        {
            int detailRow = Convert.ToInt32(HRow.Value);
            string member_no = dw_main.GetItemString(1, "member_no");
            if (detailRow > 0)
            {
                dw_list.DeleteRow(detailRow);
                //string ls_cmtype = "";
                //try
                //{
                //    ls_cmtype = dw_list.GetItemString(detailRow, "cmttype_code");
                //}
                //catch
                //{
                //    dw_list.DeleteRow(detailRow);                    
                //}
                //if (ls_cmtype != null || ls_cmtype != "")
                //{
                //    string sql = "delete from mbcremationthai where  coop_id = '" + state.SsCoopId + "' and  cmttype_code = '" + dw_list.GetItemString(detailRow, "cmttype_code").Trim() + "' and member_no = '" + member_no + "'";
                //    WebUtil.Query(sql);                    
                //}
                //LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จสิ้น");
                //DwUtil.RetrieveDataWindow(dw_list, pbl, null, state.SsCoopId, member_no);
            }
        }
        private void JspostMember_delete_2()
        {
            int detailRow = Convert.ToInt32(HRow_2.Value);

            if (detailRow > 0)
            {
                string member_no = dw_main.GetItemString(1, "member_no");

                string sql = "delete from mbcremationthai where  coop_id = '" + state.SsCoopId + "' and cmtaccount_no = 'IS" + member_no.Substring(2, 6) + "' and cmttype_code = '" + dw_list2.GetItemString(detailRow, "cmttype_code").Trim() + "' and member_no = '" + member_no + "'";
                WebUtil.Query(sql);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จสิ้น");
                DwUtil.RetrieveDataWindow(dw_list2, pbl, null, state.SsCoopId, member_no);
            }
        }

    }
}
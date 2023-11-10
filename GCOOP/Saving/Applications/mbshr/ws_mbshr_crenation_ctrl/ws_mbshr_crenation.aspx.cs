using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_crenation_ctrl
{
    public partial class ws_mbshr_crenation : PageWebSheet, WebSheet
    {       
        [JsPostBack]
        public string PostMember { get; set; }
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            //dsFund.InitDsFund(this);
            dsInsurance.InitDsInsurance(this);
        }

        public void WebSheetLoadBegin()
        {            
            if (!IsPostBack)
            {
                this.SetOnLoadedScript("dsMain.Focus(0,'member_no');");
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostMember")
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                RetriveDataAll(memb_no);
                dsInsurance.DdCmttypeCode();
            }
            else if (eventArg == PostInsertRow)
            {
                dsInsurance.InsertAtRow(0);
                dsInsurance.DATA[0].MEMBER_NO = dsMain.DATA[0].MEMBER_NO;
                dsInsurance.DATA[0].CMTCLOSE_STATUS = 1;
                dsInsurance.DATA[0].COOP_ID = state.SsCoopId;
                dsInsurance.DdCmttypeCode();
            }
            else if (eventArg == PostDelRow)
            {
                int ln_row = dsInsurance.GetRowFocus();
                dsInsurance.DeleteRow(ln_row);
                dsInsurance.DdCmttypeCode();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                string sqlStr = "";
                string ls_memberno = dsMain.DATA[0].MEMBER_NO;
                DateTime work_Date = state.SsWorkDate;
                string coop_id = state.SsCoopId;
                //กองทุน
                //decimal ld_creamt = dsFund.DATA[0].CREMATION_AMT;
                //decimal ld_oldcreamt = dsFund.DATA[0].CREMATIONOLD_AMT;
                string ls_username = state.SsUsername;
//                กองทุน
//                try
//                {
//                    sqlStr = @"insert into mbcremationthai
//                        ( COOP_ID,                CMTTYPE_CODE,           MEMBER_NO,   
//                            CMTCLOSE_STATUS,        CREMATION_AMT,          CMTMONTH_AMT, APPLY_DATE,  REMARK ) 
//                        values
//                        (    {0}                 ,{1}                        ,{2}
//                            ,{3}                ,{4}                        ,{5},       {6},    {7})";
//                    sqlStr = WebUtil.SQLFormat(sqlStr
//                            , coop_id, "99", ls_memberno
//                            , 1, ld_creamt, ld_oldcreamt, work_Date, ls_username);
//                    WebUtil.ExeSQL(sqlStr);

//                }
//                catch (Exception ex) 
//                {
//                    sqlStr = @"update mbcremationthai 
//                        set CREMATION_AMT = {2} , APPLY_DATE = {3}, CMTMONTH_AMT = {4},  REMARK = {5}
//                        where COOP_ID = {0} and MEMBER_NO={1} and CMTTYPE_CODE = '99' ";
//                    sqlStr = WebUtil.SQLFormat(sqlStr, coop_id, ls_memberno, ld_creamt, work_Date, ld_oldcreamt, ls_username);
//                    WebUtil.ExeSQL(sqlStr);
//                }
//                //update mbmembmaster
//                sqlStr = @"update mbmembmaster 
//                        set hgr_amt = {2} 
//                        where COOP_ID = {0} and MEMBER_NO={1}  ";
//                sqlStr = WebUtil.SQLFormat(sqlStr, coop_id, ls_memberno, ld_creamt);
//                WebUtil.ExeSQL(sqlStr);

                //ประกัน
                //clear data ประกัน
                sqlStr = @"delete from mbcremationthai where  CMTTYPE_CODE<>'99' and  coop_id = {0} and  member_no ={1}";
                sqlStr = WebUtil.SQLFormat(sqlStr, coop_id, ls_memberno);
                WebUtil.ExeSQL(sqlStr);
                if (dsInsurance.RowCount > 0)
                {
                    for (int i = 0; i < dsInsurance.RowCount; i++)
                    {
                        string ls_cretype = dsInsurance.DATA[i].CMTTYPE_CODE;
                        string ls_crename = dsInsurance.DATA[i].CMTACCOUNT_NAME;
                        decimal ld_cremastamt = dsInsurance.DATA[i].CREMATION_AMT;
                        decimal ld_insuamt = dsInsurance.DATA[i].INS_AMT;
                        DateTime ldt_credate = dsInsurance.DATA[i].CREMATION_DATE;
                        try
                        {
                            sqlStr = @"insert into mbcremationthai
                           ( COOP_ID,                CMTTYPE_CODE,           MEMBER_NO,   
                            CMTACCOUNT_NAME,         CMTCLOSE_STATUS,       CREMATION_AMT,
                            APPLY_DATE ,             CREMATION_DATE,         REMARK,
                            INS_AMT ) 
                           values
                           (    {0}                 ,{1}                        ,{2}
                                ,{3}                ,{4}                        ,{5}        
                                ,{6}                ,{7}                        ,{8}
                                ,{9})";
                            sqlStr = WebUtil.SQLFormat(sqlStr
                                , coop_id, ls_cretype, ls_memberno
                                , ls_crename, 1, ld_cremastamt
                                , work_Date, ldt_credate, ls_username
                                ,ld_insuamt);
                            WebUtil.ExeSQL(sqlStr);
                        }
                        catch
                        {
                            
                        }
                    }
                }

                ResetData();
                RetriveDataAll(ls_memberno);
                dsInsurance.DdCmttypeCode();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        private void RetriveDataAll(string ls_memberno)
        {
            dsMain.RetrieveData(ls_memberno);
            //dsFund.RetrieveData(ls_memberno);
            dsInsurance.RetrieveData(ls_memberno);
            dsInsurance.DdCmttypeCode();
        }
        private void ResetData() {
            dsMain.ResetRow();
            //dsFund.ResetRow();
            dsInsurance.ResetRow();
        }
        public void WebSheetLoadEnd()
        {
            
        }
    }
}
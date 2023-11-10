using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.divavg.ws_divsrv_reqchg_sequest_ctrl
{
    public partial class ws_divsrv_reqchg_sequest : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostDivYear { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                JsGetYear();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);

                String sql = @"select yrc.member_no
                    from yrreqchg yrc , yrreqchgsequest yrcs
                    where yrc.coop_id = yrcs.coop_id
                    and yrc.reqchg_docno = yrcs.reqchg_docno
                    and yrc.coop_id = {0}
                    and yrc.div_year = {1}
                    and yrc.member_no = {2}
                    and yrc.reqchg_status = 1";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].DIV_YEAR, member_no);
                Sdt dt = WebUtil.QuerySdt(sql); ;
                if (dt.Next())
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกทะเบียน " + member_no + " ได้ทำการอายัดแล้ว");
                    dsMain.ResetRow();
                    JsGetYear();
                }
                else
                {                    
                    dsMain.RetrieveMain(member_no);
                    dsMain.DATA[0].REQCHG_DATE = state.SsWorkDate;
                    JsGetYear();
                }
            }
            else if (eventArg == PostDivYear)
            {

            }
        }

        private void JsGetYear()
        {
            int account_year = 0;
            try
            {
                String sql = @"select max(current_year) from yrcfconstant";
                Sdt dt = WebUtil.QuerySdt(sql); ;
                if (dt.Next())
                {
                    account_year = int.Parse(dt.GetString("max(current_year)"));
                    dsMain.DATA[0].DIV_YEAR = Convert.ToString(account_year);
                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                account_year = DateTime.Now.Year;
                account_year = account_year + 543;
                dsMain.DATA[0].DIV_YEAR = Convert.ToString(account_year);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                string last_docno = wcf.NCommon.of_getnewdocno(state.SsWsPass,state.SsCoopId, "YRREQCHG");
                dsMain.DATA[0].REQCHG_DOCNO = last_docno;
                dsMain.DATA[0].ENTRY_ID = state.SsUsername;
                dsMain.DATA[0].ENTRY_DATE = DateTime.Now;
                dsMain.DATA[0].ENTRY_BYCOOPID = state.SsCoopControl;
                dsDetail.DATA[0].COOP_ID = state.SsCoopControl;
                dsDetail.DATA[0].REQCHG_DOCNO = last_docno;
            }
            catch(Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }   

           
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddFormView(dsMain, ExecuteType.Insert);
                exe.AddFormView(dsDetail, ExecuteType.Insert);
                exe.Execute();
                exe.SQL.Clear();
                String sqlupdate_sequest = (@"update yrbgmaster
                    set sequest_flag = {0}
                    where coop_id = {1}
                    and member_no = {2}
                    and div_year = {3}");
                sqlupdate_sequest = WebUtil.SQLFormat(sqlupdate_sequest, dsMain.DATA[0].REQCHG_STATUS, state.SsCoopControl, dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].DIV_YEAR);
                Sta taupdate = new Sta(state.SsConnectionString);
                int result = taupdate.Exe(sqlupdate_sequest);

                sqlupdate_sequest = (@"update yrdivmethpay
                    set sequest_flag = {0}
                    where coop_id = {1}
                    and member_no = {2}
                    and div_year = {3}");
                sqlupdate_sequest = WebUtil.SQLFormat(sqlupdate_sequest, dsMain.DATA[0].REQCHG_STATUS, state.SsCoopControl, dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].DIV_YEAR);
                result = taupdate.Exe(sqlupdate_sequest);

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                dsMain.ResetRow();
                dsDetail.ResetRow();
                JsGetYear();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ " + ex); }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}
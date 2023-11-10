using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.mbshr.ws_sl_reqgain_true_ctrl
{
    public partial class ws_sl_reqgain_true : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string postMemberno { get; set; }

        [JsPostBack]
        public string PostInsertRow { get; set; }

        [JsPostBack]
        public string PostDeleteRow { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);

        }

        public void WebSheetLoadBegin()
        {
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == postMemberno)
            {
                String memno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.DATA[0].MEMBER_NO = memno;
                dsMain.Retrieve(memno);
                dsDetail.Retrieve(memno);
            }
            else if (eventArg == PostInsertRow)
            {
                dsDetail.InsertLastRow();
            }
            else if (eventArg == PostDeleteRow)
            {
                int r = dsDetail.GetRowFocus();
                dsDetail.DeleteRow(r);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                string last_documentno = wcf.NCommon.of_getnewdocno(state.SsWsPass,state.SsCoopId, "MBREGGAINDOCNO");
                ExecuteDataSource exedinsertmbreqgain = new ExecuteDataSource(this);
                ///ลบข้อมูลก่อน insert
                string delgain = "delete mbreqgain where member_no = {0}";
                delgain = WebUtil.SQLFormat(delgain, dsMain.DATA[0].MEMBER_NO);
                Sdt td = WebUtil.QuerySdt(delgain);

                string sqlInsertmbreqgain = @"insert into mbreqgain 
                (coop_id,gain_docno,memcoop_id,member_no,
                write_date,write_at,age,entry_id,entry_date,remark,condition_type,condition_etc)
                values
                ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})";
                object[] argslistInsertmbreqgain = new object[] { state.SsCoopControl, last_documentno, state.SsCoopId, WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO),
                dsMain.DATA[0].GAINCOND_DATE, dsMain.DATA[0].write_at, dsMain.DATA[0].c_age,
                state.SsUsername,state.SsWorkDate,"",dsMain.DATA[0].GAINCOND_TYPE,dsMain.DATA[0].GAINCOND_DESC};
                sqlInsertmbreqgain = WebUtil.SQLFormat(sqlInsertmbreqgain, argslistInsertmbreqgain);
                exedinsertmbreqgain.SQL.Add(sqlInsertmbreqgain);
                exedinsertmbreqgain.Execute();

                for (int r = 0; r < dsDetail.RowCount; r++)
                {
                    dsDetail.DATA[r].COOP_ID = state.SsCoopControl;
                    dsDetail.DATA[r].MEMBER_NO = dsMain.DATA[0].MEMBER_NO;
                    dsDetail.DATA[r].SEQ_NO = r + 1;
                    dsDetail.DATA[r].WRITE_AT = dsMain.DATA[0].write_at;
                    dsDetail.DATA[r].WRITE_DATE = dsMain.DATA[0].GAINCOND_DATE;
                    dsDetail.DATA[r].AGE = dsMain.DATA[0].c_age;
                    dsDetail.DATA[r].REMARK = "";
                    dsDetail.DATA[r].CONDITION_TYPE = dsMain.DATA[0].GAINCOND_TYPE;
                    dsDetail.DATA[r].CONDITION_ETC = dsMain.DATA[0].GAINCOND_DESC;
                }
                ExecuteDataSource exe1 = new ExecuteDataSource(this);
                exe1.AddRepeater(dsDetail);
                exe1.Execute();

                exe1.SQL.Clear();
                //update mbmembmaster เงื่อนไขรับผลประโยชน์
                string sqlupdatemembmas = "update mbmembmaster set gaincond_type = {2},gaincond_desc ={3},gaincond_date ={4}, write_at = {5} where member_no = {1} and coop_id = {0}";
                sqlupdatemembmas = WebUtil.SQLFormat(sqlupdatemembmas, state.SsCoopControl, dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].GAINCOND_TYPE, dsMain.DATA[0].GAINCOND_DESC, dsMain.DATA[0].GAINCOND_DATE, dsMain.DATA[0].write_at);
                exe1.SQL.Add(sqlupdatemembmas);
                exe1.Execute();

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย");
                dsMain.ResetRow();
                dsDetail.ResetRow();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ" + ex);
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}
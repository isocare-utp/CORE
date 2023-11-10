using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_est_moneyreturn_ctrl
{
    public partial class ws_fin_est_moneyreturn : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public String PostRetrieveList { get; set; }
        [JsPostBack]
        public String PostBlank { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {


            if (!IsPostBack)
            {
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                dsMain.DATA[0].KSS_FLAG = 1;
                //dsMain.DATA[0].MEMBER_NO = "%";
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostRetrieveList)
            {
                try
                {
                    decimal mem_flag = 0, kss_flag = 0;
                    string member_no = "";
                    mem_flag = dsMain.DATA[0].CHOOSEMEM_FLAG;
                    kss_flag = dsMain.DATA[0].KSS_FLAG;
                    DateTime op_date = dsMain.DATA[0].OPERATE_DATE;
                    if (mem_flag == 1)
                    {
                        member_no = dsMain.DATA[0].MEMBER_NO;
                    }
                    else if (mem_flag == 0)
                    {
                        member_no = "%";
                    }
                    if (kss_flag == 1)
                    {
                        dsList.RetrieveList(member_no, op_date);
                    }
                }catch(Exception ex){
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }

            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                string sqlSeqno = "";
                string memb_no = "";
                string coop_id="";
                decimal seqno=0;
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    seqno=0;
                    dsList.DATA[i].COOP_ID = state.SsCoopId;
                    dsList.DATA[i].ENTRY_DATE = dsMain.DATA[0].OPERATE_DATE;
                    if (dsList.DATA[i].OPERATE_FLAG == 0)
                    {
                        dsList.ChangeRowStatusNone(i);
                        //dsList.DeleteRow(i);
                    }
                    else {
                        dsList.ChangeRowStatusInsert(i);
                        memb_no = dsList.DATA[i].MEMBER_NO;
                        coop_id = dsList.DATA[i].COOP_ID;
                        sqlSeqno = "select nvl(max(seq_no),0) as max_seqno from mbmoneyreturn where member_no = {0} and coop_id={1}";
                        sqlSeqno = WebUtil.SQLFormat(sqlSeqno,memb_no,coop_id);
                        Sdt dt = WebUtil.QuerySdt(sqlSeqno);
                        if (dt.Next())
                        {
                            seqno = dt.GetDecimal("max_seqno")+1;
                        }
                        else {
                            seqno = 1;
                        }
                        dsList.DATA[i].ENTRY_ID = state.SsUsername;
                        dsList.DATA[i].BIZZCOOP_ID = state.SsCoopId;
                        dsList.DATA[i].SYSTEM_FROM = "KEP";
                        dsList.DATA[i].SEQ_NO = seqno;
                        dsList.DATA[i].RETURN_STATUS = 8;
                    }
                }


                exed1.AddRepeater(dsList);

                string sqlUpdate = "";
                //for (int j = 0; j < dsList.RowCount; j++) {
                //    sqlUpdate = "";
                //    if (dsList.DATA[j].OPERATE_FLAG == 1)
                //    {
                //        sqlUpdate = "update wrtfundstatement set return_status=1 where coop_id={0} and member_no={1} and seq_no={2}";
                //        sqlUpdate = WebUtil.SQLFormat(sqlUpdate, dsList.DATA[j].COOP_ID, dsList.DATA[j].MEMBER_NO, dsList.DATA[j].SEQ_WRT);
                //        exed1.SQL.Add(sqlUpdate);
                //    }
                //}


                int ii = exed1.Execute();

                dsList.ResetRow();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ " + (ii) + " รายการ");
            }catch(Exception ex){
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

        }

        public void WebSheetLoadEnd()
        {
            decimal mem_flag = 0;
            mem_flag = dsMain.DATA[0].CHOOSEMEM_FLAG;
            if (mem_flag == 1)
            {
                dsMain.FindTextBox(0, dsMain.DATA.MEMBER_NOColumn).Enabled = true;
            }
            else if (mem_flag == 0)
            {
                dsMain.FindTextBox(0, dsMain.DATA.MEMBER_NOColumn).Enabled = false;
            }
        }
    }
}
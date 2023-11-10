using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.w_sheet_transfer_wrt_ctrl
{
    public partial class w_sheet_transfer_wrt : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostBlank { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdWrtAccount();
                dsMain.DATA[0].operate_date = state.SsWorkDate;
                dsMain.DATA[0].acc_id = "0011001598";
                GetWrtfund();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void SaveWebSheet()
        {
            try
            {
                DateTime opd = dsMain.DATA[0].operate_date;
                string acc_id = dsMain.DATA[0].acc_id;
                decimal wss = dsMain.DATA[0].wss;
                decimal dss = dsMain.DATA[0].dss;

                string sqlWss = @"insert into dpdepttran
(coop_id,       deptaccount_no, memcoop_id,     member_no,
system_code,    tran_year,      tran_date,      seq_no,
deptitem_amt,   tran_status,    sequest_status, ref_slipno,
branch_operate)
values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})";
                sqlWss = WebUtil.SQLFormat(sqlWss,
                    state.SsCoopId, acc_id, state.SsCoopId, "00000000",
                    "WSS", (opd.Year + 543), opd, 1,
                    wss, 0, 0, "LON","001");




                string sqlDss = @"insert into dpdepttran
(coop_id,       deptaccount_no, memcoop_id,     member_no,
system_code,    tran_year,      tran_date,      seq_no,
deptitem_amt,   tran_status,    sequest_status, ref_slipno,
branch_operate)
values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})";
                sqlDss = WebUtil.SQLFormat(sqlDss,
                    state.SsCoopId, acc_id, state.SsCoopId, "00000000",
                    "DSS", (opd.Year + 543), opd, 1,
                    dss, 0, 0, "LON","001");
                if (!IsPostDpdepttran())
                {
                    if (wss > 0)
                    {
                        Sdt dtWss = WebUtil.QuerySdt(sqlWss);
                    }

                    if (dss > 0)
                    {
                        Sdt dtDss = WebUtil.QuerySdt(sqlDss);
                    }
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                }
                else {
                    LtServerMessage.Text = WebUtil.ErrorMessage("มีการทำรายการโอนกองทุนแล้ว กรุณาตรวจสอบ");
                }
                
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {

        }

        public void GetWrtfund()
        {
            try
            {
                DateTime operate_date = dsMain.DATA[0].operate_date;
                bool chkdss = false, chkwss = false;
                string sql = @"select sum(sd.item_payamt) as dss from slslippayin si ,slslippayindet  sd 
where trunc(si.slip_date) = {0}
and sd.slipitemtype_code = 'MUT'
and si.payinslip_no = sd.payinslip_no
and si.coop_id = {1}
and si.coop_id = sd.coop_id
and si.slip_status =1";
                sql = WebUtil.SQLFormat(sql, operate_date, state.SsCoopId);
                Sdt dt = WebUtil.QuerySdt(sql);

                string sql2 = @"select sum(returnetc_amt) as wss from slslippayout 
where trunc(slip_date)={0}
and coop_id={1}
and slip_status=1";
                sql2 = WebUtil.SQLFormat(sql2, operate_date, state.SsCoopId);
                Sdt dt2 = WebUtil.QuerySdt(sql2);

                if (dt.Next())
                {
                    try
                    {
                        dsMain.DATA[0].dss = dt.GetDecimal("dss");
                        chkdss = true;
                    }
                    catch
                    {

                    }
                }

                if (dt2.Next())
                {
                    try
                    {
                        dsMain.DATA[0].wss = dt2.GetDecimal("wss");
                        chkwss = true;
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public bool IsPostDpdepttran() {
            bool chk = false;
            try
            {
                
                DateTime opd = dsMain.DATA[0].operate_date;
                string sql = "select * from dpdepttran where tran_date={0} and (system_code='DSS' or system_code='WSS') and coop_id={1}";
                sql = WebUtil.SQLFormat(sql, opd, state.SsCoopId);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    chk = true;
                }
            }
            catch { }

            return chk;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.ap_deposit.ws_dep_closedayprocess_ctrl
{
    public partial class ws_dep_closedayprocess : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostCloseDay { get; set; }

        public string outputProcess;
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            //HdCloseday.Value = "false";
            if (!IsPostBack)
            {               
                CheckPrncfix();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostCloseDay)
            {
                JsPostCloseDay();

            }
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            
        }
        public void JsPostCloseDay()
        {
            //n_depositClient depService = wcf.NDeposit;
            DateTime closeDate = new DateTime(1370, 1, 1);
            try
            {
                closeDate = dsMain.DATA[0].atm_date;//Dw_date.GetItemDateTime(1, "proc_date");
            }
            catch { }
            try
            {
                HdCloseday.Value = "true";
                int ln_ckcloseday = 0;
                string sql = @" 
                select count(*) as c_row from dpdepttran where coop_id={0} and tran_Date={1} and tran_status=0 and deptitem_amt > 0";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, state.SsWorkDate);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ln_ckcloseday = dt.GetInt32("c_row");
                }
                if (ln_ckcloseday > 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ค้างรายการที่ต้องผ่านรายการที่ >>หน้าจอประมวลฝากจากระบบอื่น<<!!!"); return;
                }
                DateTime workdate = state.SsWorkDate;
                sql = @"select closeday_status,workdate from amappstatus where coop_id = {0} and application= 'ap_deposit' ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, closeDate);
                dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ln_ckcloseday = dt.GetInt32("closeday_status");
                    workdate = dt.GetDate("workdate");
                }
                if (ln_ckcloseday == 1 && workdate == closeDate)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ระบบเงินฝากมีการปิดงานประจำวันแล้ว ไม่สามารถปิดงานประจำวันได้อีก"); return;
                }
                outputProcess = WebUtil.runProcessing(state, "DPCLSDAY", closeDate.ToString("dd/MM/yyyy"), state.SsClientIp, "");
                CheckPrncfix();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        private void CheckPrncfix() {
            try
            {
                dsMain.ResetRow();
                dsMain.DATA[0].atm_date = state.SsWorkDate;
                dsList.ResetRow();
                dsList.RetrieveList(dsMain.DATA[0].atm_date);
                if (dsList.RowCount > 0)
                {
                    //show display 
                    this.SetOnLoadedScript("document.getElementById('F_dsList').style.display = 'block';");
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาย้อนวันทำรายการปิดวันเข้าดอกเบี้ยครบกำหนด ก่อนวันที่ปัจจุบัน");
                    dsMain.DATA[0].check_save = "กรุณาย้อนวันทำรายการปิดวันเข้าดอกเบี้ยครบกำหนด ก่อนวันที่ปัจจุบัน";
                    return;
                }
                else
                {
                    this.SetOnLoadedScript("document.getElementById('F_dsList').style.display = 'none';");
                }                
            }
            catch { }
        }
    }
}
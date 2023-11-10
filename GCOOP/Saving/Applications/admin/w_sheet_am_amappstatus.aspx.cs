using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
namespace Saving.Applications.admin
{
    public partial class w_sheet_am_amappstatus : PageWebSheet, WebSheet
    {
        private String pbl = "am_amappstatus.pbl";
        private String tbName = "amappstatus";
        private DwThDate tDwMain;

        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(dwMain, this);
            tDwMain.Add("workdate", "work_tdate");
            tDwMain.Add("last_workdate", "last_work_tdate");
        }

        public void WebSheetLoadBegin()
        {
            //this.ConnectSQLCA();
            //dwMain.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                DwUtil.RetrieveDataWindow(dwMain, pbl, tDwMain, state.SsCoopId);
            }
            else
            {
                this.RestoreContextDw(dwMain, tDwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void SaveWebSheet()
        {
            string application_name = "";
            decimal closeday_status = 8, updatefin_status = 0;
            DateTime finwork_date = new DateTime();
            try
            {
                for (int i = 1; i <= dwMain.RowCount; i++)
                {
                    dwMain.SetItemString(i, "coop_id", state.SsCoopId);
                    dwMain.SetItemString(i, "coop_control", state.SsCoopControl);
                    application_name = dwMain.GetItemString(i, "application").Trim();
                    if (application_name == "app_finance")
                    {
                        closeday_status = dwMain.GetItemDecimal(i, "closeday_status");
                        finwork_date = dwMain.GetItemDateTime(i, "workdate");
                        if (closeday_status == 0)
                        { updatefin_status = 1; }
                    }
                }
                //dwMain.UpdateData();
                DwUtil.UpdateDataWindow(dwMain, pbl, tbName);

                //เปิดวันที่การการเงินหลังจากปิดวัน ณ วันที่ทำรายการให้อัพเดท fincashflowmas
//                if (updatefin_status == 1)
//                {
//                    decimal cfin = 0;
//                    string sqlck = @"select count(1) as cfincash from fincashflowmas where operate_date > {0}
//                            and coop_id = {1}";
//                    sqlck = WebUtil.SQLFormat(sqlck, finwork_date, state.SsCoopId);
//                    Sdt ta = WebUtil.QuerySdt(sqlck);
//                    if (ta.Next())
//                    {
//                        cfin = ta.GetDecimal("cfincash");
//                    }

//                    if (cfin == 0)
//                    {
//                        string upfincashflow = @"update fincashflowmas set close_status = 0 
//                                where operate_date = {0} and coop_id = {1}";
//                        upfincashflow = WebUtil.SQLFormat(upfincashflow, finwork_date, state.SsCoopId);
//                        Sdt ta2 = WebUtil.QuerySdt(upfincashflow);
//                    }
//                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                tDwMain.Eng2ThaiAllRow();
            }
            catch { }
            dwMain.SaveDataCache();
        }

        protected void BtChangDate_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= dwMain.RowCount; i++)
            {
                DateTime dt;
                try
                {
                    dt = dwMain.GetItemDateTime(i, "workdate");
                }
                catch
                {
                    dt = DateTime.Today.AddDays(-1);
                }
                if (dt != DateTime.Today)
                {
                    DateTime workDate = DateTime.Today;
                    dwMain.SetItemDateTime(i, "workdate", DateTime.Today);
                    dwMain.SetItemDateTime(i, "last_workdate", dt);
                }
            }
            tDwMain.Eng2ThaiAllRow();
        }

        protected void BtOpenDate_Click(object sender, EventArgs e)
        {
            //"AMAPPSTATUS"."APPLICATION",   
            //"AMAPPSTATUS"."WORKDATE",   
            //"AMAPPSTATUS"."LAST_WORKDATE",   
            //"AMAPPSTATUS"."CLOSEDAY_STATUS",   
            //"AMAPPSTATUS"."CLOSEDAY_ID",   
            //"AMAPPSTATUS"."CLOSEDAY_DATE",   
            //"AMAPPSTATUS"."CLOSEMONTH_STATUS",   
            //"AMAPPSTATUS"."CLOSEMONTH_ID",   
            //"AMAPPSTATUS"."CLOSEMONTH_DATE",   
            //"AMAPPSTATUS"."CLOSEYEAR_STATUS",   
            //"AMAPPSTATUS"."CLOSEYEAR_ID",   
            //"AMAPPSTATUS"."CLOSEYEAR_DATE",   
            //"AMAPPSTATUS"."DESCRIPTION",   
            //"AMAPPSTATUS"."MENU_ORDER",   
            //"AMAPPSTATUS"."USED_FLAG",   
            //'        ' as work_tdate,   
            //'        ' as last_work_tdate  
            for (int i = 1; i <= dwMain.RowCount; i++)
            {
                dwMain.SetItemDecimal(i, "closeday_status", 0);
                dwMain.SetItemString(i, "CLOSEDAY_ID", state.SsUsername);
                dwMain.SetItemDateTime(i, "CLOSEDAY_DATE", DateTime.Now);
            }
        }

        protected void BtOpenMonth_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= dwMain.RowCount; i++)
            {
                dwMain.SetItemDecimal(i, "closemonth_status", 0);
                dwMain.SetItemString(i, "CLOSEMONTH_ID".ToLower(), state.SsUsername);
                dwMain.SetItemDateTime(i, "CLOSEMONTH_DATE".ToLower(), DateTime.Now);
            }
        }

        protected void BtOpenYear_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= dwMain.RowCount; i++)
            {
                dwMain.SetItemDecimal(i, "closeyear_status", 0);
                dwMain.SetItemString(i, "CLOSEYEAR_ID".ToLower(), state.SsUsername);
                dwMain.SetItemDateTime(i, "CLOSEYEAR_DATE".ToLower(), DateTime.Now);
            }
        }

       
    }
}
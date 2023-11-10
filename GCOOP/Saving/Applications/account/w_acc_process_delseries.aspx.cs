using System;
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
using System.Data.OracleClient; //เพิ่มเข้ามา
using Sybase.DataWindow;//เพิ่มเข้ามา
using System.Globalization; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using CoreSavingLibrary.WcfNAccount;
using System.Web.Services.Protocols;
using CoreSavingLibrary; //เรียกใช้ service

namespace Saving.Applications.account
{
    public partial class w_acc_process_delseries : PageWebSheet, WebSheet
    {
        protected String jsPostProcess;
        private DwThDate tDwMain;
        string pbl = "asset.pbl";
        private n_accountClient accService;//ประกาศตัวแปร WebService

        #region WebSheet Members
        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(Dwmain, this);
            tDwMain.Add("end_date", "end_tdate");
            jsPostProcess = WebUtil.JsPostBack(this, "jsPostProcess");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            accService = wcf.NAccount;//ประกาศ new

            if (!IsPostBack)
            {
                Dwmain.InsertRow(0);
                DateTime dt = state.SsWorkDate;
                int day_of_month = DateTime.DaysInMonth(dt.Year, dt.Month);
                Dwmain.SetItemDateTime(1, "end_date", new DateTime(dt.Year, dt.Month, day_of_month));
            }
            else
            {
                this.RestoreContextDw(Dwmain);
                this.RestoreContextDw(Dwlist);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostProcess":
                    Process();
                    break;
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            tDwMain.Eng2ThaiAllRow();
            Dwmain.SaveDataCache();
            Dwlist.SaveDataCache();
        }
        #endregion

        #region Function
        private void Getyear(DateTime dt)
        {
            short year = 0;
            short period = 0;
            short result = accService.of_get_year_period(state.SsWsPass, dt, state.SsCoopId, ref year, ref period);
            if (result == 1)
            {
                Hdyear.Value = year.ToString();
                Hdperiod.Value = period.ToString();
            }
        }

        private void Process()
        {

            Sta ta = new Sta(sqlca.ConnectionString);
            DateTime ending_of_dp = DateTime.Now;
            DateTime start_of_dp = DateTime.Now;
            int dp_year = 0;
            int acc_year = 0;
            //หาวันที่สิ้นสุดของปีค่าเสื่อมปัจจุบัน
            try
            {

                String sql = @"select  dp_year, beginning_of_dp , ending_of_dp from acc_dp_year  where  coop_id ='" + state.SsCoopId + "' and dp_status = 0 ";

                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    dp_year = int.Parse(dt.GetString("dp_year"));
                    start_of_dp = DateTime.Parse(dt.GetString("beginning_of_dp"));
                    ending_of_dp = DateTime.Parse(dt.GetString("ending_of_dp"));

                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลปีของค่าเสื่อม ");
                    sqlca.Rollback();
                }
                ta.Close();

                String sql2 = @"select  max(account_year) as acccount_year from accaccountyear  where  coop_id ='" + state.SsCoopId + "'";

                Sdt dt2 = ta.Query(sql2);
                if (dt2.Next())
                {
                    acc_year = int.Parse(dt2.GetString("acccount_year"));

                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลปีของปีบัญชี ");
                    sqlca.Rollback();
                }
                ta.Close();

                DateTime end_date = Dwmain.GetItemDateTime(1, "end_date");

                //เช็คปีบัญชีของบัญชีว่ามีแล้วรึยัง
                if (dp_year <= acc_year)
                {
                    //ตรวจสอบวันที่ ที่จะคำนวณว่าอยู่ในช่วงปีของค่าเสื่อมปัจจุบันหรือไม่
                    if (end_date <= ending_of_dp && end_date >= start_of_dp)
                    {

                        int result = accService.of_cal_dp(state.SsWsPass, end_date, state.SsCoopId);
                        if (result == 1)
                        {
                            Getyear(end_date);
                            LtServerMessage.Text = WebUtil.CompleteMessage("ประมวลผลเรียบร้อยแล้ว");
                            DwUtil.RetrieveDataWindow(Dwlist, pbl, null, Convert.ToInt16(Hdyear.Value), Convert.ToInt16(Hdperiod.Value));
                            Filter();
                        }
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("วันที่ที่ใช้ในการคำนวณ ไม่อยู่ในช่วงของปีค่าเสื่อม กรุณาตรวจสอบ");
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถคำนวณค่าเสื่อมได้ เนื่องจากยังไม่ปิดสิ้นปีบัญชี");
                }
            }
            

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถประมวลค่าเสื่อมได้ กรุณาลองใหม่อีกครั้ง");
            }
             
        }

        protected void Radio1_CheckChanged(object sender, EventArgs e)
        {
            if (Radio1.Checked)
            {
                //Radio2.Checked = false;
            }
            if (Hdyear.Value != "")
            {
                DwUtil.RetrieveDataWindow(Dwlist, pbl, null, Convert.ToInt16(Hdyear.Value), Convert.ToInt16(Hdperiod.Value));
                Filter();
            }
        }

        //protected void Radio2_CheckChanged(object sender, EventArgs e)
        //{
        //    if (Radio2.Checked)
        //    {
        //        Radio1.Checked = false;
        //    }
        //    if (Hdyear.Value != "")
        //    {
        //        DwUtil.RetrieveDataWindow(Dwlist, pbl, null, Convert.ToInt16(Hdyear.Value), Convert.ToInt16(Hdperiod.Value));
        //        Filter();
        //    }
        //}

        private void Filter()
        {
            if (Radio1.Checked)
            {
                Dwlist.SetFilter("type_of_caldp = 1");
                Dwlist.Filter();
            }
            else
            {
                Dwlist.SetFilter("type_of_caldp <> 1");
                Dwlist.Filter();
            }
        }
        #endregion
    }
}
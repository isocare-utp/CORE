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
using CoreSavingLibrary.WcfNAccount;  //เพิ่มเข้ามา
using Sybase.DataWindow;  //เพิ่มเข้ามา
using System.Globalization;  //เพิ่มเข้ามา
using System.Data.OracleClient;  //เพิ่มเข้ามา
using DataLibrary;
using System.Web.Services.Protocols;


namespace Saving.Applications.account
{
    public partial class w_acc_yearclose_depreciation : PageWebSheet, WebSheet
    {

        private DwThDate tdw_begin;
        //=======================
        protected String postCloseYear;
        protected String postNewClear;

        //===========================
        //ค้างไว้ก่อน
        private void JspostBeginEndDate()
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            int dpyear = 0;
            try
            {
                int dp_year = 0;
                String sql = @"select  dp_year from acc_dp_year  where  coop_id ='" + state.SsCoopId + "' and dp_status = 0 ";

                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    dp_year = int.Parse(dt.GetString("dp_year"));
                    dpyear = int.Parse(dt.GetString("dp_year"));
                    dp_year = dp_year + 543;               
                    Dw_main.SetItemDecimal(1, "dp_year", Convert.ToDecimal(dp_year));

                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลปีของค่าเสื่อม ");
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                sqlca.Rollback();
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
            ta.Close();

            Sta ta2 = new Sta(sqlca.ConnectionString);
            try
            {
                String sql = @"select  isnull(max(period),0) as period, max( calto_date ) as calto_date from acc_cal_dp  where  coop_id ='" + state.SsCoopId + "' and account_year = '" + dpyear + "' ";

                Sdt dt2 = ta2.Query(sql);
                if (dt2.Next())
                {
                    int dp_period = 0;
                    DateTime calto_date = state.SsWorkDate;
                    dp_period = int.Parse(dt2.GetString("period"));
                    if (dp_period > 0)
                    {
                        calto_date = dt2.GetDate("calto_date");
                        Dw_main.SetItemDateTime(1, "cal_date", calto_date);
                        Dw_main.SetItemString(1, "calto_tdate", calto_date.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    }
                    else
                    {
                        Dw_main.SetItemString(1, "calto_tdate", "01/01/" + (dpyear + 543).ToString());
                    }
                   
                    Dwlist.Retrieve(dpyear, dp_period);

                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลการประมวลผลค่าเสื่อม ");
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                sqlca.Rollback();
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
            ta2.Close();

            
        }

        private void JspostCloseYear()
        {
            //ส่วนติดต่อ SERVICE
            try
            {
                n_accountClient accService = wcf.NAccount;
                Decimal accyear = Dw_main.GetItemDecimal(1, "dp_year") - 543;
                //DateTime calto_date = Convert.ToDateTime(Dw_main.GetItemString(1, "calto_tdate"));
                DateTime calto_date = Dw_main.GetItemDateTime(1, "cal_date");
                String wsPass = state.SsWsPass;
                String coop_id = state.SsCoopId;

                //เรียกใช้ webservice
                int result = wcf.NAccount.of_close_depreciation(wsPass, (short)accyear, calto_date, coop_id);


                HdIsFinished.Value = "true";
                if (result != 1)
                {
                    throw new Exception("ไม่สามารถยกยอดค่าเสื่อมราคาได้");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ยกยอดค่าเสื่อมราคาเรียบร้อยแล้ว");
                }

            }
            catch (SoapException ex)
            {
                //webutl จัดตัวหนังสือไว้ทำสีแดงให้ ตรงกลางจอ                    //webutil.soapmessage จะเอาerror มาใส่แทน      
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                // error ทั่วไป
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postCloseYear = WebUtil.JsPostBack(this, "postCloseYear");
            tdw_begin.Add("cal_date", "calto_tdate");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");

            tdw_begin = new DwThDate(Dw_main, this);
            tdw_begin.Eng2ThaiAllRow();

        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();

            Dw_main.SetTransaction(sqlca);
            Dwlist.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                Dw_main.InsertRow(0);
                Dw_main.Retrieve(state.SsCoopId);
                JspostBeginEndDate();
            }
            else
            {
                Dw_main.RestoreContext();
            }
        }


        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCloseYear")
            {
                JspostCloseYear();
            }
            else if (eventArg == "postNewClear")
            {
                JspostBeginEndDate();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion


    }
}

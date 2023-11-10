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
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using DataLibrary;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
//using CoreSavingLibrary.WcfFinance;
using CoreSavingLibrary.WcfNFinance;


namespace Saving.Applications.app_finance
{
    public partial class w_dlg_startday : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        public DwThDate tDwMain;
        decimal pb125_flag = 0;
        decimal c_openday = 0;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain);
            tDwMain.Add("entry_date", "entry_tdate");
            tDwMain.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            pb125_flag = GetSql_Value("select pb125_flag as sql_value from finconstant");

            if (pb125_flag == 1)
            {
                try
                {
                    string statday_info = "";
                    string errmessage = "";
                    int result = 0;
                    DateTime mm = state.SsWorkDate;

                    result = wcf.NFinance.of_init_openday(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsClientIp, ref statday_info, ref errmessage);

                    DwMain.Reset();
                    DwMain.ImportString(statday_info, FileSaveAsType.Xml);
                    DwMain.Modify("t_coopname.text = '" + state.SsCoopName + "'");
                    DwMain.Modify("t_entry_time.text = '" + DateTime.Now.ToString("hh:mm:ss") + "'");
                    tDwMain.Eng2ThaiAllRow();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else
            {
                try
                {
                    Int32 resultXml;
                    string statday_info = "";
                    string errmessage = "";
                    fin = wcf.NFinance;


                    c_openday = GetSql_Value("select close_status as sql_value  from fincashflowmas where operate_date = (select max(operate_date) from fincashflowmas where coop_id = '" + state.SsCoopId + "' ) and coop_id = '" + state.SsCoopId + "'");
                    string date_openday = "";
                    String sql = @"select  operate_date as operatedate  from fincashflowmas where operate_date = (select max(operate_date) from fincashflowmas where coop_id = '" + state.SsCoopId + "') and coop_id = '" + state.SsCoopId + "'";

                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        date_openday = dt.GetString("operatedate");
                    }

                    if (c_openday == 0 && date_openday != state.SsWorkDate.ToString())
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage("ยังไม่ได้ปิดสิ้นวันก่อนหน้านี้ กรุณาปิดสิ้นวันก่อนเปิดวันใหม่");
                    }
                    else
                    {


                        resultXml = fin.of_init_openday(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsClientIp, ref statday_info, ref errmessage);

                        if (errmessage != "")
                        {
                            LtServerMessage.Text = WebUtil.WarningMessage(errmessage);
                        }

                        DwMain.Reset();
                        DwMain.ImportString(statday_info, FileSaveAsType.Xml);
                        DwMain.Modify("t_coopname.text = '" + state.SsCoopName + "'");
                        DwMain.Modify("t_entry_time.text = '" + DateTime.Now.ToString("hh:mm:ss") + "'");
                        tDwMain.Eng2ThaiAllRow();
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            //======================================คือ ?=========================================//
            //DataWindowChild DcCoopBranch = DwMain.GetChild("coopbranch_id");
            //DcCoopBranch.ImportString(fin.GetChildBranch(state.SsWsPass), FileSaveAsType.Xml);
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void SaveWebSheet()
        {
            pb125_flag = GetSql_Value("select pb125_flag as sql_value from finconstant");

            if (pb125_flag == 1)
            {
                try
                {
                    String DwMainXml = DwMain.Describe("DataWindow.Data.XML");
                    int result = wcf.NFinance.of_open_day(state.SsWsPass, DwMainXml);

                    if (result == 1)
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("เปิดงานประจำวันเรียบร้อย");
                    }
                }
                catch (SoapException ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
            else
            {
                try
                {
                    String DwMainXml = DwMain.Describe("DataWindow.Data.XML");
                    int result = fin.of_open_day(state.SsWsPass, DwMainXml);

                    if (result == 1)
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("เปิดงานประจำวันเรียบร้อย");
                    }
                }
                catch (SoapException ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }

        public void WebSheetLoadEnd()
        {
        }

        public decimal GetSql_Value(string Select_Condition)
        {
            decimal max_value = 0;
            string sqlf = Select_Condition;
            Sdt dt = WebUtil.QuerySdt(sqlf);

            if (dt.Next())
            {
                max_value = dt.GetDecimal("sql_value");
            }
            return max_value;
        }

        #endregion
    }
}

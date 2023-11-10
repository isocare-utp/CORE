using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNDeposit;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_close_day : PageWebDialog, WebDialog
    {
        protected String postLooping;
        private n_depositClient depService;
        private DateTime closeDate;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postLooping = WebUtil.JsPostBack(this, "postLooping");
        }

        public void WebDialogLoadBegin()
        {
            depService = wcf.NDeposit;
            if (!IsPostBack)
            {
                closeDate = new DateTime(1370, 1, 1);
                try
                {
                    closeDate = DateTime.ParseExact(Request["closeDate"], "ddMMyyyy", WebUtil.TH);
                    HdCloseDate.Value = Request["closeDate"];
                }
                catch { }
                HdMaxLoop.Value = depService.of_get_loopcloseday(state.SsWsPass, closeDate) + "";
                HdCurrentLoop.Value = "0";
                try
                {
                    depService.of_close_day(state.SsWsPass, closeDate, state.SsWorkDate, state.SsApplication, state.SsCoopId, state.SsUsername, state.SsClientIp);
                    HdIsLoop.Value = "true";
                    LtServerMessage.Text = WebUtil.WarningMessage("กำลังประมวลผลอยู่ ห้ามปิดหน้าจอ");
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else
            {
                closeDate = DateTime.ParseExact(HdCloseDate.Value, "ddMMyyyy", WebUtil.TH);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postLooping")
            {
                string errror_meassage="";
                try
                {
                    HdCurrentLoop.Value = (int.Parse(HdCurrentLoop.Value) + 1) + "";
                    //1.
                    depService.of_operate_endday(state.SsWsPass, state.SsWorkDate, state.SsCoopId, state.SsUsername, state.SsClientIp);
                    //2.
                    depService.of_process_upint(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsClientIp, state.SsWorkDate, closeDate);
                    //3.
                    depService.of_updatereport_balday(state.SsWsPass, state.SsWorkDate, state.SsCoopId, state.SsUsername);
                    //4.
                    if (depService.of_is_endmonth_date(state.SsWsPass, closeDate))
                    {
                        //5.
                        depService.of_calint_remain(state.SsWsPass, closeDate);
                        depService.of_close_month(state.SsWsPass, closeDate, state.SsApplication, Convert.ToInt16(closeDate.Month), Convert.ToInt16(closeDate.Year), state.SsCoopId, state.SsUsername);
                    }
                    //6.
                    if (depService.of_is_endyear_date(state.SsWsPass, closeDate))
                    {
                        //7.
                        depService.of_close_year(state.SsWsPass, Convert.ToInt16(closeDate.Year), state.SsWorkDate, state.SsUsername, state.SsClientIp, state.SsApplication, state.SsCoopId);
                    }
                    //if (IsLastDayOfMonth(closeDate))// Last day of month
                    //{
                    //    depService.CalIntRemain(state.SsWsPass, state.SsCoopId, closeDate);
                    //    depService.IsCloseMonth(state.SsWsPass, state.SsApplication, state.SsCoopId);
                    //}
                    //if (false)//last day of year
                    //{
                    //    depService.IsCloseYear(state.SsWsPass, state.SsApplication, state.SsCoopId);
                    //}
                    //8.
                    depService.of_genreport_balday(state.SsWsPass, closeDate, state.SsCoopId, state.SsUsername);
                    //9.
                    depService.of_postint_nextday(state.SsWsPass, closeDate, state.SsWorkDate, state.SsUsername, state.SsCoopId, state.SsClientIp, errror_meassage);
                    //10.
                    closeDate = closeDate.AddDays(1);
                    HdCloseDate.Value = closeDate.ToString("ddMMyyyy", WebUtil.TH);
                    int maxLoop = 0;
                    int currLoop = 0;
                    try
                    {
                        maxLoop = int.Parse(HdMaxLoop.Value);
                        currLoop = int.Parse(HdCurrentLoop.Value);
                    }
                    catch { }
                    if (currLoop >= maxLoop)
                    {
                        depService.of_update_closedaystatus(state.SsWsPass, state.SsWorkDate, state.SsApplication, state.SsCoopId);
                        LtServerMessage.Text = WebUtil.CompleteMessage("ปิดสิ้นวันสำเร็จ");
                        Session.Abandon();
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage("กำลังประมวลผลอยู่ ห้ามปิดหน้าจอ");
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }

        public void WebDialogLoadEnd()
        {
            //Label1.Text = HdCurrentLoop.Value;
            //Label2.Text = HdMaxLoop.Value;
        }

        #endregion
    }
}

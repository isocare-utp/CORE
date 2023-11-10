using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using CoreSavingLibrary.WcfNDivavg;

namespace Saving.Applications.divavg.ws_divsrv_oper_cls_ctrl
{
    public partial class ws_divsrv_oper_cls : PageWebSheet, WebSheet
    {
        public String outputProcess;

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            Hd_process.Value = "false";
            if (!IsPostBack)
            {
                JsGetYear();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {            
        }

        private void JsGetYear()
        {
            int account_year = 0;
            try
            {
                String sql = @"select max(current_year) from yrcfconstant";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    account_year = int.Parse(dt.GetString("max(current_year)"));
                    dsMain.DATA[0].div_year = account_year.ToString();
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
                dsMain.DATA[0].div_year = account_year.ToString();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                dsMain.DATA[0].operate_date = state.SsWorkDate;
                dsMain.DATA[0].entry_id = state.SsUsername;
                string xml_option = dsMain.ExportXml();
                //เรียก pbprocess
                outputProcess = WebUtil.runProcessing(state, "", xml_option, "", "");

                //เรียก service เดิม
                //n_divavgClient DivavgService = wcf.NDivavg;              
                //DivavgService = wcf.NDivavg;
                //str_divsrv_oper astr_divsrv_oper = new str_divsrv_oper();
                //astr_divsrv_oper.xml_option = dsMain.ExportXml();
                //DivavgService.RunSaveSlipClsProcess(state.SsWsPass, ref astr_divsrv_oper, state.SsApplication, state.CurrentPage);
                //Hd_process.Value = "true";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {            
        }
    }
}
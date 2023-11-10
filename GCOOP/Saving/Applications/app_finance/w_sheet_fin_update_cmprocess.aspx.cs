using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_fin_update_cmprocess : PageWebSheet, WebSheet
    {
        protected string jsPostUpCmprocessFin;

        public void InitJsPostBack()
        {
            jsPostUpCmprocessFin = WebUtil.JsPostBack(this, "jsPostUpCmprocessFin");
        }

        public void WebSheetLoadBegin()
        {
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostUpCmprocessFin")
            {
                OfPostUseProcess();
            }
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            
        }

        private void OfPostUseProcess()
        {
            try
            {
                String se = @"update cmprocessing set runtime_status = 1, show_flag = -9
                        where object_name = 'POSTTOFIN'and runtime_status = 0 
                        and workdate = {0} ";
                se = WebUtil.SQLFormat(se, state.SsWorkDate);
                Sdt ta = WebUtil.QuerySdt(se);
                LtServerMessage.Text = WebUtil.CompleteMessage("อัพเดทสถานะสำเร็จ");
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);}
        }
    }
}
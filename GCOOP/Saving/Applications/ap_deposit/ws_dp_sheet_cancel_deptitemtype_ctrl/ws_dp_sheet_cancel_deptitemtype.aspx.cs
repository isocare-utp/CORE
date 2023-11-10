using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.ap_deposit.ws_dp_sheet_cancel_deptitemtype_ctrl
{
    public partial class ws_dp_sheet_cancel_deptitemtype : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostSave { get; set; }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSave)
            {
                DateTime lt_trandate = dsMain.DATA[0].tran_date;
                String ls_systemcode = dsMain.DATA[0].system_code;
                try
                {
                    String sqlStr = @"update dpdepttran 
                                            set    
                                            tran_status = -9 
                                            where tran_date = {0} and system_code = {1}  and tran_status=0";
                    sqlStr = WebUtil.SQLFormat(sqlStr, lt_trandate, ls_systemcode);
                    WebUtil.ExeSQL(sqlStr);
                }
                catch { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถปรับปรุงข้อมูลได้"); }
                LtServerMessage.Text = WebUtil.CompleteMessage("ปรับปรุงข้อมูลสำเร็จ");
            }
        }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void SaveWebSheet()
        {
            throw new NotImplementedException();
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                //dsMain.DDDeptitemtype();
                dsMain.DATA[0].tran_date = state.SsWorkDate;
            }
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }
    }
}
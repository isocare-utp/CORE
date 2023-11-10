using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.app_finance.ws_fin_ucf_namemanage_ctrl
{
    public partial class ws_fin_ucf_namemanage : PageWebSheet, WebSheet
    {        
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }
        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.RetrieveData(state.SsCoopId);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
           
        }

        public void SaveWebSheet()
        {
            try
            {
                string sqlStr = @"update finconstant 
                        set manager_name = {1} ,finanecial_name={2},accountant_name={3}
                        where coop_id = {0} ";
                sqlStr = WebUtil.SQLFormat(sqlStr,state.SsCoopId, dsMain.DATA[0].manager_name.Trim(), dsMain.DATA[0].finanecial_name.Trim(), dsMain.DATA[0].accountant_name.Trim());
                WebUtil.ExeSQL(sqlStr);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
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
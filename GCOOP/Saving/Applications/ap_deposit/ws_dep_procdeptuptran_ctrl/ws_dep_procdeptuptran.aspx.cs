using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.ap_deposit.ws_dep_procdeptuptran_ctrl
{
    public partial class ws_dep_procdeptuptran : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String JsPostRetriveData { get; set; }
        [JsPostBack]
        public String JsPostProcess { get; set; }
        [JsPostBack]
        public String JsPostFormatMemno { get; set; }
        
        public string outputProcess;

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].process_date = state.SsWorkDate;
                dsMain.DD_RecpPayType();
                COUNT_MEM.Text = "0";
                COUNT_NUM.Text = "0";
                SUM_TOTAL.Text = "0.00";
                try
                {
                    string sqlStr = @"update dpdeptmaster 
                    set laststmseq_no = ( select max(ds.seq_no) from dpdeptstatement ds 
                    where dpdeptmaster.deptaccount_no = ds.deptaccount_no )
                    where dpdeptmaster.deptclose_status = 0 and dpdeptmaster.laststmseq_no 
                    not in (select b.seq_no from dpdeptstatement b where dpdeptmaster.deptaccount_no = b.deptaccount_no)";
                    sqlStr = WebUtil.SQLFormat(sqlStr);
                    WebUtil.ExeSQL(sqlStr);
                }
                catch { }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == JsPostRetriveData)
            {
                PostRetriveData();
            }
            else if (eventArg == JsPostProcess)
            {
                PostProcess();
            }
            else if (eventArg == JsPostFormatMemno) {
                string ls_memno = dsMain.DATA[0].member_no.Trim();
                dsMain.DATA[0].member_no = WebUtil.MemberNoFormat(ls_memno);
            }
        }
        private void PostRetriveData() {
            try
            {
                string ls_coopid = state.SsCoopId;
                string ls_system = dsMain.DATA[0].system_code;
                DateTime tran_date = dsMain.DATA[0].process_date;
                string sql = "";
                if (dsMain.DATA[0].member_flag > 0)
                {
                    sql = "and dpdepttran.member_no = '" + dsMain.DATA[0].member_no.Trim() + "'";
                }
                dsList.Retrieve(ls_coopid, ls_system, tran_date, sql);
                decimal[] ar_func = dsList.of_Total(ls_coopid, ls_system, tran_date, sql);
                COUNT_MEM.Text = ar_func[0].ToString("#,##0");
                SUM_TOTAL.Text = ar_func[1].ToString("#,##0.00");
                COUNT_NUM.Text = ar_func[2].ToString("#,##0");
            }
            catch { }
        }
        private void PostProcess() 
        {
            try
            {
                DateTime tran_date = dsMain.DATA[0].process_date; 
                string option_xml = dsMain.ExportXml();
                outputProcess = WebUtil.runProcessing(state, "DPTRANDEPT", option_xml, state.SsClientIp, "");
                COUNT_MEM.Text = "0";
                SUM_TOTAL.Text = "0.00";
                COUNT_NUM.Text = "0";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }
        
        }
        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            
        }
    }
}
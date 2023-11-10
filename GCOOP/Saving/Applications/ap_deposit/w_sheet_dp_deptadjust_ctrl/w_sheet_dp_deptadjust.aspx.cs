using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;
using CoreSavingLibrary;
using CoreSavingLibrary.WcfNDeposit;

namespace Saving.Applications.ap_deposit.w_sheet_dp_deptadjust_ctrl
{
    public partial class w_sheet_dp_deptadjust : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string JsPostDeptAcc { get; set; }



        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsDetail.DATA[0].AI_ADJ_TYPE = -1;
                dsDetail.DATA[0].AI_TYPE_DEC = 2;
                dsDetail.DATA[0].DATE_INT = state.SsWorkDate;
                Hdas_apvdoc.Value = "";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == JsPostDeptAcc) {
                try
                {
                    string deptacc_no = "";
                    deptacc_no = dsMain.DATA[0].DEPTACCOUNT_NO;
                    dsMain.Retrieve(deptacc_no);
                }
                catch (Exception ex){ 
                }
            }
        }

        public void SaveWebSheet()
        {
            try {
                string xml_main = "";
                string xml_detail = "";
                string apv_doc = Hdas_apvdoc.Value;
                xml_main = dsMain.ExportXml();
                xml_detail = dsDetail.ExportXml();
                int result = wcf.NDeposit.of_dept_adjust(state.SsWsPass, xml_main, xml_detail, state.SsUsername, state.SsWorkDate, state.SsClientIp,apv_doc);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    Hdas_apvdoc.Value="";
                    dsMain.ResetRow();
                    dsDetail.ResetRow();
                    dsDetail.DATA[0].AI_ADJ_TYPE = -1;
                    dsDetail.DATA[0].AI_TYPE_DEC = 2;
                    dsDetail.DATA[0].DATE_INT = state.SsWorkDate;
                }
                else {

                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ");
                }
            }
            catch (Exception ex) { 
                //LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ"+ex.Message); 
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                String msg = WebUtil.ErrorMessage(ex);
                try
                {
                    msg = msg.Substring(0, msg.LastIndexOf("*"));
                    msg = msg.Substring(msg.LastIndexOf("*") + 1, 10);
                    Hdas_apvdoc.Value = msg.Trim();
                }
                catch
                {
                }
            }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using CoreSavingLibrary.WcfNKeeping;

namespace Saving.Applications.keeping.ws_kp_adjmth_ccl_ctrl
{
    public partial class ws_kp_adjmth_ccl : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String postMemberNo { get; set; }
        [JsPostBack]
        public String postSlipNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            dsList.InitDsList(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == postMemberNo)
            {
                try
                {
                    dsMain.SetItem(0, dsMain.DATA.MEMBER_NOColumn, WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO));                    
                    str_keep_adjust astr_keep_adjust = new str_keep_adjust();
                    astr_keep_adjust.xml_main = dsMain.ExportXml();

                    int result = wcf.NKeeping.of_init_adjmth_ccl(state.SsWsPass, ref astr_keep_adjust);

                    if (result == 1)
                    {
                        if (astr_keep_adjust.xml_main != null && astr_keep_adjust.xml_main != "")
                        {
                            dsMain.ResetRow();
                            dsMain.ImportData(astr_keep_adjust.xml_main);                            
                        }
                        if (astr_keep_adjust.xml_list != null && astr_keep_adjust.xml_list != "")
                        {
                            dsList.ResetRow();
                            dsList.ImportData(astr_keep_adjust.xml_list);
                        }
                        //if (astr_keep_adjust.xml_detail != null && astr_keep_adjust.xml_detail != "")
                        //{
                        //    dsDetail.ResetRow();
                        //    dsDetail.ImportData(astr_keep_adjust.xml_detail);
                        //    dsDetail.DdTofromaccid();
                        //    SumDsDetail();
                        //}
                    }
                } catch(Exception ex){
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if (eventArg == postSlipNo)
            {
                int row = dsList.GetRowFocus();
                String ls_adjno = dsList.DATA[row].ADJSLIP_NO;
                dsDetail.ResetRow();
                dsDetail.Retrieve(ls_adjno);
                SumDsDetail();
            }
        }

        public void SumDsDetail()
        {
            decimal sum_itemadj = 0;

            for (int i = 0; i < dsDetail.RowCount; i++)
            {
                sum_itemadj += dsDetail.DATA[i].ITEM_ADJAMT;
            }
            dsDetail.itemadj_amt.Text = sum_itemadj.ToString("#,##0.00");            
        }

        public void SaveWebSheet()
        {
            str_keep_adjust astr_keep_adjust = new str_keep_adjust();
            astr_keep_adjust.xml_list = dsList.ExportXml();
            astr_keep_adjust.cancel_id = state.SsUsername;
            astr_keep_adjust.operate_date = state.SsWorkDate;

            int result = wcf.NKeeping.of_save_adjmth_ccl(state.SsWsPass, ref astr_keep_adjust);

            if (result == 1)
            {
                dsMain.ResetRow();
                dsList.ResetRow();
                dsDetail.ResetRow();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
            }
        }

        public void WebSheetLoadEnd()
        {
            
        }
    }
}
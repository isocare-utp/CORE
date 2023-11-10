using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using CoreSavingLibrary.WcfDivavg;
using System.Web.Services.Protocols;

namespace Saving.Applications.divavg.ws_divsrv_opr_slip_ccl_ctrl
{
    public partial class ws_divsrv_opr_slip_ccl : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitList(this);
            dsDetail.InitDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                JspostNewClear();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                JspostMemberNo();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                decimal li_member_status = dsMain.DATA[0].MEMBER_STATUS;
                if (li_member_status == 1)
                {
                    dsMain.DATA[0].MEMBER_STATUS = -1;
                }
                //DivavgService = wcf.Divavg;
                str_divsrv_oper astr_divavg_oper = new str_divsrv_oper();
                astr_divavg_oper.xml_main = dsMain.ExportXml();//Dw_main.Describe("DataWindow.Data.XML");
                astr_divavg_oper.xml_list = dsList.ExportXml();//Dw_list.Describe("DataWindow.Data.XML");
                int result = wcf.Divavg.of_save_slip_ccl(state.SsWsPass, ref astr_divavg_oper);//DivavgService.of_save_slip_ccl(state.SsWsPass, ref astr_divavg_oper);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                    JspostNewClear();
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

        public void WebSheetLoadEnd()
        {

        }

        private void JspostNewClear()
        {
            dsMain.ResetRow();
            dsList.ResetRow();
            dsDetail.ResetRow();
            dsMain.DATA[0].COOP_ID = state.SsCoopId;
            dsMain.DATA[0].ENTRY_ID = state.SsUsername;
            dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
            dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;


            String divyear = Hddiv_year.Value.Trim();
            if (divyear == "" || divyear == null)
            {
                JsGetYear();

            }
            else
            {
                dsMain.DATA[0].DIV_YEAR = divyear;
            }
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
                    dsMain.DATA[0].DIV_YEAR = Convert.ToString(account_year);
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
                dsMain.DATA[0].DIV_YEAR = account_year.ToString();
            }
        }

        protected void JspostMemberNo()
        {
            try
            {
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO).Trim();
                dsMain.DATA[0].MEMBER_NO = member_no;


                // DivavgService = wcf.Divavg;
                str_divsrv_oper astr_divavg_oper = new str_divsrv_oper();
                astr_divavg_oper.xml_main = dsMain.ExportXml();//Dw_main.Describe("DataWindow.Data.XML");
                astr_divavg_oper.xml_detail = dsDetail.ExportXml();//Dw_detail.Describe("DataWindow.Data.XML");
                astr_divavg_oper.xml_list = dsList.ExportXml();//Dw_list.Describe("DataWindow.Data.XML");
                //  int result = DivavgService.of_init_slip_ccl(state.SsWsPass, ref astr_divavg_oper);
                int result = wcf.Divavg.of_init_slip_ccl(state.SsWsPass, ref astr_divavg_oper);
                if (result == 1)
                {
                    dsMain.ResetRow();
                    dsMain.ImportData(astr_divavg_oper.xml_main);
                    decimal li_member_status = dsMain.DATA[0].MEMBER_STATUS;
                    if (li_member_status == -1)
                    {
                        dsMain.DATA[0].MEMBER_STATUS = 1;
                    }
                    dsList.ResetRow();
                    dsList.ImportData(astr_divavg_oper.xml_list);

                    dsDetail.ResetRow();
                    dsDetail.ImportData(astr_divavg_oper.xml_detail);
                    //   DwUtil.RetrieveDDDW(Dw_detail, "methpaytype_code", pbl, null);
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
}
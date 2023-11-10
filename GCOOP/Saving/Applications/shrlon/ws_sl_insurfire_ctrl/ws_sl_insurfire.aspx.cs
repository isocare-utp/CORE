using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_insurfire_ctrl
{
    public partial class ws_sl_insurfire : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostGetRow { get; set; }
        [JsPostBack]
        public string PostInsuranceNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsDetail.DATA[0].OPERATE_DATE = state.SsWorkDate;

                string sql = "select insvat_rate, inscoordinate_rate, insdiscount_rate, insflood_amt from lnloanconstant where coop_id = {0}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl);

                Sdt result = WebUtil.QuerySdt(sql);
                if (result.Next())
                {
                    dsDetail.DATA[0].VAT_PERCENT = result.GetDecimal("insvat_rate");
                    dsDetail.DATA[0].COORDINATE_PERCENT = result.GetDecimal("inscoordinate_rate");
                    dsDetail.DATA[0].DISCOUNT_PERCENT = result.GetDecimal("insdiscount_rate");
                    dsDetail.DATA[0].FLOODINSURE_AMT = result.GetDecimal("insflood_amt");
                }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMembNo(memb_no);
                dsList.RetrieveInsurfire(memb_no);
            }
            else if (eventArg == PostGetRow)
            {
                int rowGet = dsList.GetRowFocus();
                string ins_no = dsList.DATA[rowGet].INSURANCE_NO;
                dsDetail.RetrieveInsurance(ins_no);
            }
            else if (eventArg == PostInsuranceNo)
            {
                string insurance_no = dsDetail.DATA[0].INSURANCE_NO;
                String sql = "select insurance_no from lninsurancefire where trim(insurance_no) = '" + insurance_no + "'";
                sql = WebUtil.SQLFormat(sql, insurance_no);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    String insur_no = dt.GetString("insurance_no");
                    if (insurance_no == insur_no)
                    {
                        dsDetail.DATA[0].INSURANCE_NO = "";
                        LtServerMessage.Text = WebUtil.ErrorMessage("เลขที่กรมธรรม์ซ้ำ " + insur_no + " กรุณาตรวจสอบ");
                        this.SetOnLoadedScript("dsDetail.Focus(0, 'insurance_no')");
                    }
                }
                else
                {
                    this.SetOnLoadedScript("dsDetail.Focus(0, 'loancontract_no')");
                }
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                string insur_no = dsDetail.DATA[0].INSURANCE_NO;
                string memb_no = dsMain.DATA[0].MEMBER_NO;
                dsDetail.DATA[0].MEMBER_NO = memb_no;
                dsDetail.DATA[0].COOP_ID = state.SsCoopControl;
                dsDetail.DATA[0].MEMCOOP_ID = state.SsCoopId;
                dsDetail.DATA[0].CONCOOP_ID = state.SsCoopId; 
                dsDetail.DATA[0].ENTRY_ID = state.SsUsername;
                dsDetail.DATA[0].ENTRY_DATE = DateTime.Now;
                ExecuteDataSource exed = new ExecuteDataSource(this);

                string sql = "select * from lninsurancefire where coop_id = {0} and insurance_no = {1}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, insur_no);
                DataTable dt_con = WebUtil.Query(sql);

                if (dt_con.Rows.Count > 0)
                {
                    exed.AddFormView(dsDetail, ExecuteType.Update);
                    exed.Execute();
                    exed.SQL.Clear();
                    LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขข้อมูลสำเร็จ");
                    dsDetail.ResetRow();
                }
                else
                {
                    exed.AddFormView(dsDetail, ExecuteType.Insert);
                    exed.Execute();
                    exed.SQL.Clear();
                    exed.Execute();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                    dsDetail.ResetRow();
                }
                memb_no = dsMain.DATA[0].MEMBER_NO;
                dsMain.RetrieveMembNo(memb_no);
                dsList.RetrieveInsurfire(memb_no);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}
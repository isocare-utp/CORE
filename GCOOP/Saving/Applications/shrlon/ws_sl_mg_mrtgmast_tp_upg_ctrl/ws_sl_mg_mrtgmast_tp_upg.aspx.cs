using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_tp_upg_ctrl
{
    public partial class ws_sl_mg_mrtgmast_tp_upg : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostMrtgeNo { get; set; }
        [JsPostBack]
        public String PostUpgradeNo { get; set; }
        [JsPostBack]
        public String PostTemplateNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                dsList.ResetRow();
                dsDetail.ResetRow();

                string ls_memno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.DATA[0].MEMBER_NO = ls_memno;
                dsMain.MemberNoRetrieve();                
            }
            else if (eventArg == PostMrtgeNo)
            {
                dsDetail.DATA[0].UPGRADE_DATE = state.SsWorkDate;
                dsList.Retrieve(dsDetail.DATA[0].MRTGMAST_NO);
            }
            else if (eventArg == PostUpgradeNo)
            {
                int li_row = dsList.GetRowFocus();
                decimal ls_upgradeno = dsList.DATA[li_row].upgrade_no;                
                dsDetail.Retrieve(ls_upgradeno);
            }
            else if (eventArg == PostTemplateNo)
            {
                DateTime ldt_upgrade = dsDetail.DATA[0].UPGRADE_DATE;
                string ls_mtrgno = dsDetail.DATA[0].MRTGMAST_NO;
                string ls_autrz_name1 = dsDetail.DATA[0].AUTRZ_NAME1;
                string ls_autrz_pos1 = dsDetail.DATA[0].AUTRZ_POS1;
                string ls_autrz_name2 = dsDetail.DATA[0].AUTRZ_NAME2;
                string ls_autrz_pos2 = dsDetail.DATA[0].AUTRZ_POS2;
                dsDetail.RetrieveAutzd(Convert.ToDecimal(HdTemplateNo.Value));
                dsDetail.DATA[0].MRTGMAST_NO = ls_mtrgno;
                dsDetail.DATA[0].UPGRADE_DATE = ldt_upgrade;
                dsDetail.DATA[0].AUTRZ_NAME1 = ls_autrz_name1;
                dsDetail.DATA[0].AUTRZ_POS1 = ls_autrz_pos1;
                dsDetail.DATA[0].AUTRZ_NAME2 = ls_autrz_name2;
                dsDetail.DATA[0].AUTRZ_POS2 = ls_autrz_pos2;
                dsDetail.DATA[0].MRTGMAST_NO = dsDetail.DATA[0].MRTGMAST_NO;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                dsDetail.DATA[0].COOP_ID = state.SsCoopControl;

                ExecuteDataSource exed = new ExecuteDataSource(this);                
                decimal ls_upgradeno = dsDetail.DATA[0].UPGRADE_NO;
                string ls_sql = @"select upgrade_no from lnmrtgmastupgrade
                where coop_id = {0}
                and upgrade_no = {1}";
                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_upgradeno);
                Sdt dt = WebUtil.QuerySdt(ls_sql);

                if (dt.Next())
                {
                    exed.AddFormView(dsDetail, ExecuteType.Update);
                    exed.Execute();
                    exed.SQL.Clear();
                    LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขเรียบร้อย");
                }
                else
                {
                    exed.AddFormView(dsDetail, ExecuteType.Insert);                    
                    exed.Execute();
                    exed.SQL.Clear();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                }

                dsMain.ResetRow();
                dsList.ResetRow();
                dsDetail.ResetRow();
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
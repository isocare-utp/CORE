using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.ap_deposit.ws_dep_edit_sliptofrom_ctrl
{
    public partial class ws_dep_edit_sliptofrom : PageWebSheet, WebSheet
    {
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].START_DATE = state.SsWorkDate;
            }           
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }
        protected void BtSearch_Click(object sender, EventArgs e)
        {
            RetriveDate();
        }
        private void RetriveDate() {
            try {
                string ls_deptno = "", ls_deptname = "", ls_memname = "", ls_memsurname = "";
                string ls_memno = "";
                string ls_sqlext = "";
                string coop_id = state.SsCoopId;
                ls_deptno = dsMain.DATA[0].DEPTACCOUNT_NO.Trim();
                ls_deptname = dsMain.DATA[0].DEPTACCOUNT_NAME.Trim();
                ls_memno = dsMain.DATA[0].MEMBER_NO.Trim();
                ls_memname = dsMain.DATA[0].MEMB_NAME.Trim();
                ls_memsurname = dsMain.DATA[0].MEMB_SURNAME.Trim();
                DateTime start_date = dsMain.DATA[0].START_DATE;

                if (ls_deptno.Length > 0)
                {
                    ls_sqlext += " and (  DPDEPTMASTER.DEPTACCOUNT_NO like '%" + ls_deptno + "%') ";
                }
                if (ls_deptname.Length > 0)
                {
                    ls_sqlext += " and (  DPDEPTMASTER.DEPTACCOUNT_NAME like '%" + ls_deptname + "%') ";
                }
                if (ls_memno.Length > 0)
                {
                    ls_sqlext += " and ( DPDEPTMASTER.MEMBER_NO like '%" + ls_memno + "%') ";
                }
                if (ls_memname.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_name like '%" + ls_memname + "%')";
                }
                if (ls_memsurname.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_surname  like '%" + ls_memsurname + "%')";
                }

                dsList.Retrieve(coop_id, start_date, ls_sqlext);
                int rowdate = dsList.RowCount;
                if (rowdate > 0)
                {
                    for (int i = 0; i < rowdate; i++)
                    {
                        dsList.DD_Tofromaccid(dsList.DATA[i].CASH_TYPE, i);
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
            
        }
        public void SaveWebSheet()
        {
            try {
                string sqlStr="",ls_slipno="",ls_tofromaccid="";
                string ls_coopid = state.SsCoopId;
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    ls_slipno = dsList.DATA[i].DEPTSLIP_NO;
                    ls_tofromaccid = dsList.DATA[i].TOFROM_ACCID;
                    if (dsList.DATA[i].CHOOSE_FLAG == 1)
                    {
                        sqlStr = @"update dpdeptslip set tofrom_accid={2}
                                where coop_id = {0} and deptslip_no= {1} and item_status = 1 and posttovc_flag = 0";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopid, ls_slipno, ls_tofromaccid);
                        WebUtil.ExeSQL(sqlStr);
                    }
                }
                RetriveDate();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อยแล้ว ");
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
           
        }
    }
}
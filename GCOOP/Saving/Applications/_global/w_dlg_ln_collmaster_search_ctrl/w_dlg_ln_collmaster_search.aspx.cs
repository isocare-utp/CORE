using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications._global.w_dlg_ln_collmaster_search_ctrl
{
    public partial class w_dlg_ln_collmaster_search : PageWebDialog, WebDialog
    {
        public void InitJsPostBack()
        {
            dsCriteria.InitDs(this);
            dsList.InitDs(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsCriteria.DATA[0].coop_id = state.SsCoopControl;
                try
                {
                    if (Request["cmd"] == "memco_no")
                    {
                        try
                        {
                            dsCriteria.DATA[0].member_no = Request["member_no"];
                            Search();
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        }
                    }
                }
                catch { }
                dsCriteria.DdCollmastTypeCode();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
        }

        protected void BtSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            /*
             select
                lncollmaster.coop_id,
                lncollmaster.collmast_no,
                lncollmaster.collmast_refno,
                lncollmaster.collmasttype_code,
                lncollmastmemco.memco_no,
                lncollmastmemco.collmastmain_flag,
                mbucfprename.prename_desc,
                mbmembmaster.memb_name,
                mbmembmaster.memb_surname,
                lncollmaster.redeem_flag,
                lncollmaster.collmast_desc,
                lncollmaster.mortgage_price,
                lnucfcollmasttype.collmasttype_desc
            from 
                lncollmaster
                left join lncollmastmemco on lncollmaster.coop_id = lncollmastmemco.coop_id and lncollmaster.collmast_no = lncollmastmemco.collmast_no
                left join lnucfcollmasttype on lncollmaster.collmasttype_code = lnucfcollmasttype.collmasttype_code
                left join mbmembmaster on lncollmaster.coop_id = mbmembmaster.coop_id and lncollmastmemco.memco_no = mbmembmaster.member_no
                left join mbucfprename on mbmembmaster.prename_code = mbucfprename.prename_code
            where lncollmaster.coop_id = '010001'
             */
            try
            {
                string where = " and lncollmaster.coop_id = '" + dsCriteria.DATA[0].coop_id + "'";
                if (dsCriteria.DATA[0].member_no.Length > 0)
                {
                    string membNo = WebUtil.MemberNoFormat(dsCriteria.DATA[0].member_no);
                    dsCriteria.DATA[0].member_no = membNo;
                    dsCriteria.DATA[0].memb_name = "";
                    dsCriteria.DATA[0].memb_surname = "";
                    dsCriteria.DATA[0].collmast_no = "";
                    dsCriteria.DATA[0].collmast_refno = "";
                    where += " and lncollmastmemco.memco_no = '" + membNo + "' ";
                }
                if (dsCriteria.DATA[0].collmast_no.Length > 0)
                {
                    where += " and lncollmaster.collmast_no like '%" + dsCriteria.DATA[0].collmast_no + "%'";
                }
                if (dsCriteria.DATA[0].collmast_refno.Length > 0)
                {
                    where += " and lncollmaster.collmast_refno like '%" + dsCriteria.DATA[0].collmast_refno + "%'";
                }
                if (dsCriteria.DATA[0].memb_name.Length > 0)
                {
                    where += " and mbmembmaster.memb_name like '%" + dsCriteria.DATA[0].memb_name + "%'";
                }
                if (dsCriteria.DATA[0].memb_surname.Length > 0)
                {
                    where += " and mbmembmaster.memb_surname like '%" + dsCriteria.DATA[0].memb_surname + "%'";
                }
                if (dsCriteria.DATA[0].collmasttype_code.Length > 0)
                {
                    where += " and lncollmaster.collmasttype_code = '" + dsCriteria.DATA[0].collmasttype_code + "'";
                }
                string sql = @"
                    select
                        lncollmaster.coop_id,
                        lncollmaster.collmast_no,
                        lncollmaster.collmast_refno,
                        lncollmaster.collmasttype_code,
                        lncollmastmemco.memco_no,
                        lncollmastmemco.collmastmain_flag,
                        mbucfprename.prename_desc,
                        mbmembmaster.memb_name,
                        mbmembmaster.memb_surname,
                        lncollmaster.redeem_flag,
                        lncollmaster.collmast_desc,
                        lncollmaster.mortgage_price,
                        lnucfcollmasttype.collmasttype_desc
                    from 
                        lncollmaster,
                        lncollmastmemco,
                        mbmembmaster,
                        mbucfprename,
                        lnucfcollmasttype
                    where
                        lncollmaster.coop_id = lncollmastmemco.coop_id and
                        lncollmaster.coop_id = mbmembmaster.coop_id and
                        lncollmaster.collmast_no = lncollmastmemco.collmast_no and
                        lncollmastmemco.memco_no = mbmembmaster.member_no and
                        mbmembmaster.prename_code = mbucfprename.prename_code and
                        lncollmaster.collmasttype_code = lnucfcollmasttype.collmasttype_code
                        " + where;
                DataTable dt = WebUtil.Query(sql);
                dsList.ImportData(dt);
                LbCount.Text = "ดึงข้อมูล" + (dt.Rows.Count >= 300 ? "แบบสุ่ม" : "ได้") + " " + dt.Rows.Count + " รายการ";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}
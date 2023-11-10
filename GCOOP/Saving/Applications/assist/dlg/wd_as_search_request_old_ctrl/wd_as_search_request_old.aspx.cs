using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.assist.dlg.wd_as_search_request_old_ctrl
{
    public partial class wd_as_search_request_old : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string InitAsssistpaytype { get; set; }

        public void InitJsPostBack()
        {
            dsCriteria.InitDsCriteria(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string ls_assisttype = "";
                dsCriteria.GetAssYear();
                dsCriteria.AssistType(ref ls_assisttype);
                dsCriteria.DATA[0].assisttype_code = ls_assisttype;

                string sqlStr = @"select max(ass_year) ass_year from assucfyear where coop_id = {0}";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl);
                Sdt dt1 = WebUtil.QuerySdt(sqlStr);
                dt1.Next();
                dsCriteria.DATA[0].assist_year = dt1.GetInt32("ass_year");
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == InitAsssistpaytype)
            {
                string ls_minpaytype = "", ls_maxpaytype = "", ls_assisttype = "";
                if (dsCriteria.DATA[0].assisttype_code == "00")
                {
                    dsCriteria.ResetRow();
                    dsCriteria.AssistType(ref ls_assisttype);
                    dsCriteria.DATA[0].assisttype_code = ls_assisttype;

                }
                else
                {
                    dsCriteria.AssistPayType(dsCriteria.DATA[0].assisttype_code, ref ls_minpaytype, ref ls_maxpaytype);
                    dsCriteria.DATA[0].assistpay_code1 = ls_minpaytype;
                    dsCriteria.DATA[0].assistpay_code2 = ls_maxpaytype;
                }
            }
        }

        public void WebDialogLoadEnd()
        {

        }

        protected void BtSearch_Click(object sender, EventArgs e)
        {
            try
            {
                String ls_memno = "", ls_asstype = "", ls_asspay1 = "";
                String ls_memname = "", ls_memsurname = "", ls_asspay2 = "";
                Decimal ldc_assyear = 0;

                string ls_sqlext = "";

                string coop_id = state.SsCoopControl;

                ls_memno = dsCriteria.DATA[0].member_no.Trim();
                ls_asstype = dsCriteria.DATA[0].assisttype_code;
                ls_asspay1 = dsCriteria.DATA[0].assistpay_code1;
                ls_asspay2 = dsCriteria.DATA[0].assistpay_code2;
                ls_memname = dsCriteria.DATA[0].memb_name.Trim();
                ls_memsurname = dsCriteria.DATA[0].memb_surname.Trim();
                ldc_assyear = dsCriteria.DATA[0].assist_year;

                if (ls_memno.Length > 0)
                {
                    ls_sqlext += " and (  assreqmaster.member_no like '%" + ls_memno + "%') ";
                }
                if (ls_asstype != "00")
                {
                    ls_sqlext += " and (  assreqmaster.assisttype_code = '" + ls_asstype + "') ";
                }
                if (ls_asspay1.Length > 0)
                {
                    ls_sqlext += " and (  assreqmaster.assistpay_code between '" + ls_asspay1 + "' and '" + ls_asspay2 + "' ) ";
                }
                if (ls_memname.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_name like '%" + ls_memname + "%') ";
                }
                if (ls_memsurname.Length > 0)
                {
                    ls_sqlext += " and ( mbmembmaster.memb_surname like '%" + ls_memsurname + "%') ";
                }


                string sql = sql = @"
                SELECT DISTINCT TOP 300
                    ASSREQMASTER.ASSIST_DOCNO,
                    ASSREQMASTER.REQ_DATE,
                    ASSREQMASTER.MEMBER_NO, 
                    MBUCFPRENAME.PRENAME_DESC + MBMEMBMASTER.MEMB_NAME + ' ' + MBMEMBMASTER.MEMB_SURNAME as MEMB_NAME, 
                    ASSUCFASSISTTYPE.ASSISTTYPE_DESC,
                    ASSUCFASSISTTYPE.ASSISTTYPE_CODE
                FROM         
                    ASSREQMASTER , ASSUCFASSISTTYPE, MBMEMBMASTER, MBUCFPRENAME
                WHERE     
                    ASSREQMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO AND 
                    ASSREQMASTER.COOP_ID = MBMEMBMASTER.COOP_ID AND 
                    ASSREQMASTER.ASSISTTYPE_CODE = ASSUCFASSISTTYPE.ASSISTTYPE_CODE AND
                    MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE AND
                    ASSREQMASTER.REQ_STATUS = 8 AND
                    
                    ASSREQMASTER.ASSIST_YEAR = '" + ldc_assyear + "' AND ASSREQMASTER.COOP_ID = '" + coop_id + "' " + ls_sqlext;

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
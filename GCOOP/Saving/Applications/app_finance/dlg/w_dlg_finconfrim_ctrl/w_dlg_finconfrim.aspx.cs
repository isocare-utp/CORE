using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.app_finance.dlg.w_dlg_finconfrim_ctrl
{
    public partial class w_dlg_finconfrim : PageWebDialog, WebDialog
    {        
        [JsPostBack]
        public string PostSearch { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {

                dsList.RetrieveDetail(state.SsCoopId,"");
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                SearchData();
            }   
        }
        public void SearchData() {
            try
            {
                String ls_memno = "", ls_fullname = "",ls_pay_rec="";
                string ls_sqlext = "";

                
                ls_memno = dsMain.DATA[0].member_no.Trim();
                ls_fullname = dsMain.DATA[0].full_name.Trim();
                ls_pay_rec = dsMain.DATA[0].PAY_RECV_STATUS.Trim();
                
                if (ls_memno.Length > 0)
                {
                    ls_sqlext = " and (  finslip.member_no like '%" + ls_memno + "%') ";
                }
                if (ls_fullname.Length > 0)
                {
                    ls_sqlext = " and (  finslip.nonmember_detail like '%" + ls_fullname + "%') ";
                }
                if (ls_pay_rec.Length > 0)
                {
                    ls_sqlext = " and (  finslip.PAY_RECV_STATUS = " + ls_pay_rec + ") ";
                }
                dsList.RetrieveDetail(state.SsCoopId, ls_sqlext);
//                string sql = sql = @"
//                SELECT DISTINCT
//                    MBMEMBMASTER.MEMBER_NO, 
//                    MBUCFPRENAME.PRENAME_DESC, 
//                    MBMEMBMASTER.MEMB_NAME, 
//                    MBMEMBMASTER.MEMB_SURNAME, 
//                    MBMEMBMASTER.MEMBGROUP_CODE, 
//                    MBUCFMEMBGROUP.MEMBGROUP_DESC
//                FROM         
//                    MBMEMBMASTER, MBUCFMEMBGROUP, MBUCFPRENAME
//                WHERE     
//                    MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE AND 
//                    MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP.COOP_ID AND 
//                    MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE AND 
//                    ROWNUM <= 300 AND
//                    MBMEMBMASTER.COOP_ID = '" + coop_id + "' " + ls_sqlext;
//                DataTable dt = WebUtil.Query(sql);
//                dsList.ImportData(dt);
//                LbCount.Text = "ดึงข้อมูล" + (dt.Rows.Count >= 300 ? "แบบสุ่ม" : "ได้") + " " + dt.Rows.Count + " รายการ";

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        public void WebDialogLoadEnd()
        {
            
        }
    }
}
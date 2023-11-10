using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications._global.w_dlg_sl_detail_contract_ctrl
{
    public partial class w_dlg_sl_detail_contract : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }


        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            dsDetail.InitDs(this);
            dsStatement.InitDs(this);
            dsCollateral.InitDs(this);
            dsChgpay.InitDs(this);
        }
        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string member_no = Request["member_no"];
                string loancontract_no = Request["loancontract_no"];
                dsMain.RetrieveMembNo(member_no, loancontract_no);
                dsDetail.RetrieveMembNo(member_no,loancontract_no);
                string yearstart = dsDetail.DATA[0].STARTKEEP_PERIOD.Substring(0,4);
                string mouthstart = dsDetail.DATA[0].STARTKEEP_PERIOD.Substring(4);
                dsDetail.DATA[0].STARTKEEP_PERIOD = yearstart + "/" + mouthstart;
                dsStatement.Retrieve(loancontract_no);
                dsCollateral.Retrieve(loancontract_no);
                dsChgpay.Retrieve(loancontract_no);
            } 

        }


        public void CheckJsPostBack(string eventArg)
        {
            /*if (eventArg == PostMemberNo)
            {
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);


            }*/



        }


        public void WebDialogLoadEnd()
        {

        }
    }
}
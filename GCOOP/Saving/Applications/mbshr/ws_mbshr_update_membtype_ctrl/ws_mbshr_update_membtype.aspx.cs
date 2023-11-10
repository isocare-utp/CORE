using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_update_membtype_ctrl
{
    public partial class ws_mbshr_update_membtype : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMember { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostMember")
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(memb_no);
                dsMain.DdMemType(dsMain.DATA[0].MEMBTYPE_CODE);
                dsMain.DdMemTypeNew();
            }
        }

        public void SaveWebSheet()
        {
            String lastdoc_no = "", membtype_code = "", new_membtype_code = "", member_no = "";
            try
            {
                lastdoc_no = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopControl, "MBCHGMEMBTYPE");
                member_no = dsMain.DATA[0].MEMBER_NO;
                membtype_code = dsMain.DATA[0].MEMBTYPE_CODE;
                new_membtype_code = dsMain.DATA[0].new_membtype_code;
                dsMain.DATA[0].MEMCOOP_ID = state.SsCoopControl;

                String sqlinsert = @"INSERT INTO MBREQCHANGEMBTYPE 
                            (COOP_ID , CHGMBTYPE_DOCNO, MEMCOOP_ID , MEMBER_NO , REQ_DATE , 
                            OLD_MBTYPE , NEW_MBTYPE , REQUEST_STATUS , ENTRY_ID , ENTRY_DATE , 
                            ENTRY_BYCOOPID, APV_ID , APV_DATE ) values 
                            ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})";
                sqlinsert = WebUtil.SQLFormat(sqlinsert, state.SsCoopControl, lastdoc_no, dsMain.DATA[0].MEMCOOP_ID, member_no, state.SsWorkDate,
                            membtype_code, new_membtype_code, 1, state.SsUsername, state.SsWorkDate,
                            state.SsCoopId, state.SsUsername, state.SsWorkDate);
                WebUtil.Query(sqlinsert);

                String sqlupdate = @"update mbmembmaster set membtype_code = {0} where member_no = {1}";
                sqlupdate = WebUtil.SQLFormat(sqlupdate, new_membtype_code, member_no);
                WebUtil.Query(sqlupdate);

                LtServerMessage.Text = WebUtil.CompleteMessage("ปรับปรุงประเภทสมาชิกสำเร็จ");
                dsMain.ResetRow();
                dsMain.RetrieveMain(member_no);
                dsMain.DdMemType(dsMain.DATA[0].MEMBTYPE_CODE);
                dsMain.DdMemTypeNew();
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
           
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_transloan_collateral_ctrl
{
    public partial class ws_sl_transloan_collateral : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostLoancont { get; set; }
        [JsPostBack]
        public string PostMembno { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostLoancont")
            {

                try
                {
                    //ShrlonClient ShrlonService = wcf.NShrlon;
                    str_lntrnrespons astr = new str_lntrnrespons();
                    astr.concoop_id = state.SsCoopControl;
                    astr.loancontract_no = dsMain.DATA[0].LOANCONTRACT_NO;
                    astr.trnreq_code = "TRN";
                    astr.trnreq_date = state.SsWorkDate;
                    astr.entry_id = state.SsUsername;

                    // wcf.NShrlon.of_initlntrnres(state.SsWsPass, ref astr); // of_initlntrnres(state.SsWsPass, ref astr_lntrnrespons);  //pb125
                    wcf.NShrlon.of_initlntrnres(state.SsWsPass, ref astr); // of_initlntrnres(state.SsWsPass, ref astr_lntrnrespons);  //pb120
                    dsMain.ResetRow();
                    dsMain.ImportData(astr.xml_trnmast);
                    dsList.ResetRow();
                    dsList.ImportData(astr.xml_trndetail);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if (eventArg == "PostMembno")
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                String sql = @"select mbucfprename.prename_desc,
                                    mbmembmaster.memb_name,
                                    mbmembmaster.memb_surname,
                                    mbmembmaster.member_no
                               from 
                                    mbmembmaster,   
                                    mbucfprename
                               where
                                    ( mbmembmaster.prename_code = mbucfprename.prename_code ) and 
                                    (mbmembmaster.member_no = {0})";
                sql = WebUtil.SQLFormat(sql, memb_no);
                Sdt result = WebUtil.QuerySdt(sql);
                while (result.Next())
                {
                    dsMain.ResetRow();
                    dsList.ResetRow();
                    dsMain.DATA[0].MEMBER_NO = result.GetString("member_no");
                    dsMain.DATA[0].MEMB_NAME = result.GetString("memb_name");
                    dsMain.DATA[0].PRENAME_DESC = result.GetString("prename_desc");
                    dsMain.DATA[0].MEMB_SURNAME = result.GetString("memb_surname");
                }

            }
        }

        public void SaveWebSheet()
        {
            try
            {
                dsMain.DATA[0].TRNLNREQ_DOCNO = "";
                //n_shrlonClient = wcf.NShrlon;
                str_lntrnrespons str = new str_lntrnrespons();
                str.concoop_id = state.SsCoopId;
                str.entry_id = state.SsUsername;
                str.loancontract_no = dsMain.DATA[0].LOANCONTRACT_NO;
                str.trnreq_code = "TRN";
                str.trnreq_date = state.SsWorkDate;
                str.xml_trnmast = dsMain.ExportXml();
                str.xml_trndetail = dsList.ExportXml();

                //wcf.NShrlon.of_savetrn_lntrnres(state.SsWsPass, ref str);  //pb125
                wcf.NShrlon.of_savetrn_lntrnres(state.SsWsPass, ref str); //pob120

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");

                dsMain.ResetRow();
                dsList.ResetRow();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}
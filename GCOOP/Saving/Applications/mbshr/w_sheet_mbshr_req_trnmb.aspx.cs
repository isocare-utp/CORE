using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfShrlon;
using CoreSavingLibrary.WcfNKeeping;

using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mbshr_req_trnmb : PageWebSheet, WebSheet
    {
        //private ShrlonClient shrlonService;
        //private CommonClient commonService;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        public String pbl = "mb_req_trnmb.pbl";
        private DwThDate tdwmain;
        protected String postNewClear;
        protected String postInitMember;
        protected String postSetMemberNo;

        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInitMember = WebUtil.JsPostBack(this, "postInitMember");
            postSetMemberNo = WebUtil.JsPostBack(this, "postSetMemberNo");

            tdwmain = new DwThDate(Dw_main, this);
            tdwmain.Add("trnmbreq_date", "trnmbreq_tdate");

        }

        public void WebSheetLoadBegin()
        {
            try
            {
                //shrlonService = wcf.NShrlon;
                //commonService = wcf.NCommon;
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
            }

            try
            {
                this.ConnectSQLCA();

                if (!IsPostBack)
                {
                    JspostNewClear();
                }
                else
                {
                    this.RestoreContextDw(Dw_main);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postInitMember")
            {
                JsInitMember();
            }
            else if (eventArg == "postSetMemberNo")
            {
                JspostSetMemberNo();
            }
        }

        public void SaveWebSheet()
        {
            string document_code = "MBMEMBERNO";
            string memnew_no = Dw_main.GetItemString(1, "memnew_no");
            decimal last_member = Convert.ToDecimal(memnew_no);
            String sqlupdate = "update cmdocumentcontrol set last_documentno = '" + last_member + "' where document_code = '" + document_code + "'and coop_id = '" + state.SsCoopControl + "'";
            WebUtil.Query(sqlupdate);
            try
            {
                Dw_main.SetItemString(1, "entry_id", state.SsUsername);
                str_mbreqtrnmb astr_mbreqtrnmb = new str_mbreqtrnmb();
                astr_mbreqtrnmb.entry_id = state.SsUsername;
                astr_mbreqtrnmb.xml_request = Dw_main.Describe("DataWindow.Data.XML");
                //int result = shrlonService.SaveReqTrnmb(state.SsWsPass, ref astr_mbreqtrnmb);
                int result = shrlonService.of_savereq_trnmb(state.SsWsPass, ref astr_mbreqtrnmb);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
                    JspostNewClear();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {            
            tdwmain.Eng2ThaiAllRow();
        }

        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_main.SetItemDate(1, "trnmbreq_date", state.SsWorkDate);
            Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_main.SetItemString(1, "entry_id", state.SsUsername);
            tdwmain.Eng2ThaiAllRow();
        }

        private void JsInitMember()
        {
            try
            {
                DateTime ldt_trndate = Dw_main.GetItemDateTime(1, "trnmbreq_date");
                String memold_no = WebUtil.MemberNoFormat(Dw_main.GetItemString(1, "memold_no"));
                //memold_no = WebUtil.MemberNoFormat(memold_no);
                Dw_main.SetItemString(1, "memold_no", memold_no);

                try
                {
                    str_mbreqtrnmb astr_mbreqtrnmb = new str_mbreqtrnmb();
                    astr_mbreqtrnmb.member_no = memold_no;                    
                    //int result = shrlonService.InitReqTrnmb(state.SsWsPass, ref astr_mbreqtrnmb);
                    int result = shrlonService.of_initreq_trnmb(state.SsWsPass, ref astr_mbreqtrnmb);
                    if (result == 1)
                    {
                        Dw_main.Reset();
                        DwUtil.ImportData(astr_mbreqtrnmb.xml_request, Dw_main, null, FileSaveAsType.Xml);
                        Dw_main.SetItemDateTime(1, "trnmbreq_date", ldt_trndate);
                    }
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostSetMemberNo()
        {
            string document_code, ls_memberno;
            decimal last_documentno = 0, last_member = 0;

            document_code = "MBMEMBERNO";            

            //หาเลขลำดับล่าสุด
            try
            {
                String sql = @"select last_documentno from cmdocumentcontrol where coop_id = '" + state.SsCoopId + "' and document_code = '" + document_code + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    last_documentno = Convert.ToDecimal(dt.GetString("last_documentno"));
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }

            last_member = last_documentno + 1;
            ls_memberno = Convert.ToString(last_member);
            ls_memberno = WebUtil.MemberNoFormat(ls_memberno);
            Dw_main.SetItemString(1, "memnew_no", ls_memberno);

            //String sqlupdate = "update cmdocumentcontrol set last_documentno = '" + last_member + "' where document_code = '" + document_code + "'and coop_id = '" + state.SsCoopControl + "'";
            //WebUtil.Query(sqlupdate);
        }
    }
}
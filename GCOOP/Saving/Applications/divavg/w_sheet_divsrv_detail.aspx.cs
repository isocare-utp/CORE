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
using Sybase.DataWindow;
using DataLibrary;
using CoreSavingLibrary.WcfNDivavg;
using System.Web.Services.Protocols;

namespace Saving.Applications.divavg
{
    public partial class w_sheet_divsrv_detail : PageWebSheet,WebSheet
    {
        private n_divavgClient DivavgService;


        protected String postInit;
        protected String postNewClear;
        protected String postInitMember;
        protected String postrefresh;
        protected String postFilter;
        public String pbl = "divsrv_detail.pbl";
        //==========================
        private void JspostFilter()
        {
            try 
            {
                int RowClickList = int.Parse(Hdrow.Value);
                Dw_year.SelectRow(0, false);
                Dw_year.SelectRow(RowClickList, true);
                Dw_year.SetRow(RowClickList);

                String div_year = HdDivyear.Value;
                Dw_master.SetFilter("div_year = '" + div_year + "'");
                Dw_master.Filter();

                Dw_methodpayment.SetFilter("div_year = '" + div_year + "'");
                Dw_methodpayment.Filter();

                Dw_statement.SetFilter("div_year = '" + div_year + "'");
                Dw_statement.Filter();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JsSetMemberFormat()
        {
            String membNo = Dw_main.GetItemString(1, "member_no");
            membNo = WebUtil.MemberNoFormat(membNo);
            Dw_main.SetItemString(1, "member_no", membNo);
        }

        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_main.SetItemString(1, "coop_id", state.SsCoopControl);

            Dw_year.Reset();

            Dw_methodpayment.Reset();
            Dw_master.Reset();
        }



        protected void JspostInitMember()
        {
            try
            {
                String member_no = Hdmember_no.Value.Trim();
                Dw_main.SetItemString(1, "member_no", member_no);

                DivavgService = wcf.NDivavg;
                str_divsrv_det astr_divsrv_det = new str_divsrv_det();
                astr_divsrv_det.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_init_detail(state.SsWsPass, ref astr_divsrv_det);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_main, Dw_main, null, FileSaveAsType.Xml);

                    Dw_year.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_list, Dw_year, null, FileSaveAsType.Xml);

                    Dw_master.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_master, Dw_master, null, FileSaveAsType.Xml);

                    Dw_methodpayment.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_methpay, Dw_methodpayment, null, FileSaveAsType.Xml);

                    Dw_statement.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_statement, Dw_statement, null, FileSaveAsType.Xml);

                    Dw_shrday.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_shr_day, Dw_shrday, null, FileSaveAsType.Xml);

                    Dw_shrmonth.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_shr_mth, Dw_shrmonth, null, FileSaveAsType.Xml);

                    Dw_loan.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_lon_con, Dw_loan, null, FileSaveAsType.Xml);

                    Dw_year.SelectRow(1, true);
                    Dw_year.SetRow(1);


                    string div_year = Dw_year.GetItemString(1, "div_year");
                    Dw_master.SetFilter("div_year = '" + div_year + "'");
                    Dw_master.Filter();

                    Dw_methodpayment.SetFilter("div_year = '" + div_year + "'");
                    Dw_methodpayment.Filter();

                    Dw_statement.SetFilter("div_year = '" + div_year + "'");
                    Dw_statement.Filter();
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JspostInit()
        {
            try
            {
                String member_no = Dw_main.GetItemString(1, "member_no");

                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);

                DivavgService = wcf.NDivavg;
                str_divsrv_det astr_divsrv_det = new str_divsrv_det();
                astr_divsrv_det.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_init_detail(state.SsWsPass, ref astr_divsrv_det);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_main, Dw_main, null, FileSaveAsType.Xml);

                    Dw_year.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_list, Dw_year, null, FileSaveAsType.Xml);

                    Dw_master.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_master, Dw_master, null, FileSaveAsType.Xml);

                    Dw_methodpayment.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_methpay, Dw_methodpayment, null, FileSaveAsType.Xml);

                    Dw_statement.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_statement, Dw_statement, null, FileSaveAsType.Xml);

                    Dw_shrday.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_shr_day, Dw_shrday, null, FileSaveAsType.Xml);

                    Dw_shrmonth.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_shr_mth, Dw_shrmonth, null, FileSaveAsType.Xml);

                    Dw_loan.Reset();
                    DwUtil.ImportData(astr_divsrv_det.xml_lon_con, Dw_loan, null, FileSaveAsType.Xml);




                    Dw_year.SelectRow(1, true);
                    Dw_year.SetRow(1);

                    string div_year = Dw_year.GetItemString(1, "div_year");
                    Dw_master.SetFilter("div_year = '" + div_year + "'");
                    Dw_master.Filter();

                    Dw_methodpayment.SetFilter("div_year = '" + div_year + "'");
                    Dw_methodpayment.Filter();

                    Dw_statement.SetFilter("div_year = '" + div_year + "'");
                    Dw_statement.Filter();

                    //Dw_master.SetFilter("div_year = ")
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

        }


        #region WebSheet Members

        public void InitJsPostBack()
        {
            postInit = WebUtil.JsPostBack(this, "postInit");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInitMember = WebUtil.JsPostBack(this, "postInitMember");
            postFilter = WebUtil.JsPostBack(this, "postFilter");

            //====================
            DwUtil.RetrieveDDDW(Dw_statement, "divpaytype_code", pbl, null);
            DwUtil.RetrieveDDDW(Dw_methodpayment, "divpaytype_code", pbl, null);
            //====================
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_year);
                this.RestoreContextDw(Dw_statement);
                this.RestoreContextDw(Dw_methodpayment);
                this.RestoreContextDw(Dw_master);
                this.RestoreContextDw(Dw_shrday);
                this.RestoreContextDw(Dw_shrmonth);
                this.RestoreContextDw(Dw_loan);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postInit")
            {
                JspostInit();
            }
            else if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postInitMember")
            {
                JspostInitMember();
            }
            else if (eventArg == "postFilter")
            {
                JspostFilter();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            //========================
            DwUtil.RetrieveDDDW(Dw_methodpayment, "methpaytype_code", pbl, null);
            DwUtil.RetrieveDDDW(Dw_methodpayment, "expense_bank", pbl, null);
            DwUtil.RetrieveDDDW(Dw_methodpayment, "expense_branch", pbl, null);
            DwUtil.RetrieveDDDW(Dw_statement, "divitemtype_code", pbl, null);

            //=======================

            Dw_main.SaveDataCache();
            Dw_year.SaveDataCache();
            Dw_statement.SaveDataCache();
            Dw_methodpayment.SaveDataCache();
            Dw_master.SaveDataCache();
            Dw_shrday.SaveDataCache();
            Dw_shrmonth.SaveDataCache();
            Dw_loan.SaveDataCache();

        }

        #endregion
    }
}
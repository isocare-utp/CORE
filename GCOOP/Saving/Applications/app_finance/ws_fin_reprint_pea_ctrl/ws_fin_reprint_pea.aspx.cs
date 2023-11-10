using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_reprint_pea_ctrl
{
    public partial class ws_fin_reprint_pea : PageWebSheet, WebSheet
    {
        string URL = "";
        [JsPostBack]
        public string PostRetrieve { get; set; }
        [JsPostBack]
        public string PostPrint { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdCode();
                DateTime slip_date_s = state.SsWorkDate;
                DateTime slip_date_e = state.SsWorkDate;
                dsMain.DATA[0].SLIP_DATE_S = slip_date_s;
                dsMain.DATA[0].SLIP_DATE_E = slip_date_e;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostRetrieve)
            {
                try
                {
                    string member_no = "";
                    string entry_id = "";
                    string sliptype_code = "";
                    string payinslip_no_s = "";
                    string payinslip_no_e = "";
                    DateTime slip_date_s = dsMain.DATA[0].SLIP_DATE_S;
                    DateTime slip_date_e = dsMain.DATA[0].SLIP_DATE_E;

                    member_no = dsMain.DATA[0].MEMBER_NO;
                    entry_id = dsMain.DATA[0].ENTRY_ID;
                    sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
                    payinslip_no_s = dsMain.DATA[0].PAYINSLIP_NO_S;
                    payinslip_no_e = dsMain.DATA[0].PAYINSLIP_NO_E;

                    dsList.Retrieve(member_no, entry_id, sliptype_code, payinslip_no_s, payinslip_no_e, slip_date_s, slip_date_e);
                }
                catch { }

            }
            else if (eventArg == PostPrint)
            {
                bool chk = true;
                string payinslip_no = "";
                for (int j = 0; j < dsList.RowCount && chk; j++)
                {
                    if (dsList.DATA[j].checkselect == 1)
                    {
                        try
                        {
                            payinslip_no = dsList.DATA[j].PAYINSLIP_NO;
                            Printing.PrintSlipSlpayin(this, payinslip_no, state.SsCoopId);
                            chk = false;
                            LtServerMessage.Text = WebUtil.CompleteMessage("พิมพ์ใบเสร็จสำเร็จ");
                        }catch(Exception ex){
                            LtServerMessage.Text = WebUtil.ErrorMessage("พิมพ์ใบเสร็จไม่สำเร็จ กรุณาตรวจสอบ"+ex.Message);
                        }
                    }
                }
            }
        }

        private static string XmlReadVar(string responseData, string szVar)
        {
            int i1stLoc = responseData.IndexOf("<" + szVar + ">");
            if (i1stLoc < 0)
                return string.Empty;
            int ilstLoc = responseData.IndexOf("</" + szVar + ">");
            if (ilstLoc < 0)
                return string.Empty;
            int len = szVar.Length;
            return responseData.Substring(i1stLoc + len + 2, ilstLoc - i1stLoc - len - 2);
        }

        public string GetSql(string rslip)
        {
            String sql = @"select
                    a.payinslip_no,
                    a.member_no,
                    a.sliptype_code,
                    a.moneytype_code,
                    a.document_no,
                    a.slip_date,
                    a.operate_date,
                    a.sharestk_value,
                    a.intaccum_amt,
                    a.sharestkbf_value,
                    a.slip_amt,
                    a.slip_status,
                    a.entry_id,
                    a.entry_bycoopid,
                    b.slipitemtype_code,
                    b.shrlontype_code,
                    b.loancontract_no,
                    b.slipitem_desc,
                    b.period,
                    b.principal_payamt,
                    b.interest_payamt,
                    b.item_payamt,
                    b.item_balance,
                    b.calint_to,
                    d.prename_desc||c.memb_name||'  '||c.memb_surname as member_name,
                    a.membgroup_code,
                    e.membgroup_desc,
                    c.membtype_code,
				    c.addr_no,
                    c.addr_moo,
				    c.addr_soi,
				    c.addr_village,
                    c.addr_road,
				    h.tambol_desc,
				    i.district_desc,
				    j.province_desc,
				    c.addr_postcode,
                    f.membtype_desc,
                    g.receipt_remark1 as remark_line1,
                    g.receipt_remark2 as remark_line2,
                    ftreadtbaht( a.slip_amt ) AS  money_thaibaht 
                    from slslippayin a, slslippayindet b, mbmembmaster c, mbucfprename d, mbucfmembgroup e, mbucfmembtype f, cmcoopmaster g, mbucftambol h, mbucfdistrict i, mbucfprovince j                    
                    where	a.coop_id = '" + state.SsCoopControl + @"'
                    and		a.payinslip_no in (" + rslip + @")
                    and     a.coop_id		    = b.coop_id
                    and		a.payinslip_no	    = b.payinslip_no
                    and		a.memcoop_id	    = c.coop_id
                    and		a.member_no			= c.member_no
                    and		c.prename_code		= d.prename_code
                    and		a.memcoop_id	    = e.coop_id
                    and		a.membgroup_code	= e.membgroup_code
                    and		c.coop_id		    = f.coop_id
                    and		c.membtype_code		= f.membtype_code
                    and		a.coop_id		    = g.coop_id
                    and		c.tambol_code	    = h.tambol_code (+)
                    and		c.amphur_code	    = i.district_code (+)
                    and		c.province_code		= j.province_code (+)";
            return sql;
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
    }
}
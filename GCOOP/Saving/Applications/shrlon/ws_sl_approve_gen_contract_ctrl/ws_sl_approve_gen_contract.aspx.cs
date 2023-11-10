using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using CoreSavingLibrary.WcfNShrlon;

namespace Saving.Applications.shrlon.ws_sl_approve_gen_contract_ctrl
{
    public partial class ws_sl_approve_gen_contract : PageWebSheet, WebSheet
    {
        private n_shrlonClient shrlonService;

        [JsPostBack]
        public string PostSearch { get; set; }
        [JsPostBack]
        public string PostGenContNo { get; set; }
        static string apvlist = "";

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                dsList.ResetRow();
                dsMain.DATA[0].select_check = "0";
                dsMain.DATA[0].appv_status = "11";

                string sqlwhere = "";

                if (dsMain.DATA[0].entry_id != "")
                {
                    sqlwhere += " and lnreqloan.entry_id ='" + dsMain.DATA[0].entry_id + "' ";
                }
                else { sqlwhere += ""; }

                if (dsMain.DATA[0].member_no != "")
                {
                    sqlwhere += " and lnreqloan.member_no like'%" + dsMain.DATA[0].member_no + "%' ";
                }
                else { sqlwhere += ""; }

                if (dsMain.DATA[0].loantype_code != "")
                {
                    sqlwhere += " and lnreqloan.loantype_code ='" + dsMain.DATA[0].loantype_code + "' ";
                }
                else { sqlwhere += ""; }


                dsList.RetrieveList(sqlwhere);
            }
            else if (eventArg == PostGenContNo)
            {
                GenContNo();
            }
        }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void SaveWebSheet()
        {
            string str = "";
            int count = dsList.RowCount;
            for (int i = 0; i < count; i++)
            {
                if (dsList.DATA[i].LOANCONTRACT_NO.Trim() != "")
                {
                    str += ";" + dsList.DATA[i].LOANREQUEST_DOCNO + ",1," + dsList.DATA[i].LOANCONTRACT_NO;
                    
                }
            }
            str = str.Substring(1);
            try
            {
                int result = wcf.NShrlon.of_save_lnapv(state.SsWsPass, str, state.SsUsername, state.SsWorkDate);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    dsList.ResetRow();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();

            if (!IsPostBack)
            {
                dsMain.DDloantype();
                dsList.RetrieveList("");
            }
        }

        public void WebSheetLoadEnd()
        {

        }

        private void GenContNo()
        {
            int count = dsList.RowCount;
            for (int i = 0; i < count; i++)
            {
                if (dsList.DATA[i].choose_flag != 1)
                {
                    continue;
                }

                string req_coopid = state.SsCoopControl;

                String lncont_no = "";
                try { lncont_no = dsList.DATA[i].LOANCONTRACT_NO; }
                catch { lncont_no = ""; }

                decimal lncont_status = dsList.DATA[i].LOANREQUEST_STATUS;

                if (lncont_no == "")
                {
                    String loantype_code = dsList.DATA[i].LOANTYPE_CODE.Trim();
                    String newReqDocNo = wcf.NShrlon.of_gennewcontractno(state.SsWsPass, req_coopid, loantype_code);


                    Jschklastdocnonew(newReqDocNo, loantype_code);

                    dsList.DATA[i].LOANCONTRACT_NO = newReqDocNo;
                }


            }
        }
        protected void PrintCont_Click(object sender, EventArgs e)
        {
            //apvlist = "'Q580001066','Q580001814','Q580001017','Q580001028'";

            if (apvlist != "")
            {
                try
                {
                    string sql = @"SELECT  mn.PRENAME_DESC||''||mb.MEMB_NAME||' '||mb.MEMB_SURNAME as fullname,
trunc(months_between(sysdate,mb.BIRTH_DATE)/12) as age,
         lr.MEMBER_NO,
         lr.LOANTYPE_CODE,
         lr.LOANCREDIT_AMT,
         lr.LOANPERMISS_AMT,
         lr.LOANREQUEST_AMT,
ftreadtbaht(lr.LOANREQUEST_AMT) as thbathloan,
ftreadtbaht(lr.PERIOD_PAYMENT) as thbathpayment,
         lr.PERIOD_PAYMENT,
         lr.LOANPAYMENT_TYPE,
         mb.MEMB_NAME,
         mb.MEMB_SURNAME,
         mb.MEMBGROUP_CODE,
         mg.MEMBGROUP_DESC,
         cc.COOP_NAME,
         cc.MANAGER,
TO_CHAR(lr.LOANREQUEST_DATE, 'dd MON yyyy', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') as datethai,
         lr.LOANREQUEST_DATE,
         lr.LOANCONTRACT_NO,
         lr.PERIOD_LASTPAYMENT,
         lr.SALARY_AMT,
         lr.SHARE_LASTPERIOD,
         lr.SHARE_PERIODVALUE,
         mb.ADDR_NO,
         mb.ADDR_MOO,
         mb.ADDR_SOI,
         mb.ADDR_VILLAGE,
         mb.ADDR_ROAD,
         md.DISTRICT_DESC,
         mp.PROVINCE_DESC,
         mt.TAMBOL_DESC,
         mb.TAMBOL_CODE,
         mb.AMPHUR_CODE,
         mb.PROVINCE_CODE,
         mb.ADDR_POSTCODE,
mb.POSITION_DESC,
NVL(mb.POSITION_DESC,'   ') as POSDESC ,
TO_CHAR(lr.LOANREQUEST_DATE, 'Month', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') as monththai,
         lr.PERIOD_PAYAMT,
		mb.ADDR_PHONE,
		lr.PERIOD_LASTPAYMENT,
		li.INTEREST_RATE,
		lo.LOANOBJECTIVE_DESC,
		 FT_CALAGEMTH(mb.BIRTH_DATE,SYSDATE) AS BIRTH_DATE
    FROM LNREQLOAN lr,
         MBMEMBMASTER mb,
         MBUCFMEMBGROUP mg,
         MBUCFPRENAME mn,
         CMCOOPCONSTANT cc,
		LNLOANTYPE lt,
		LNUCFLOANOBJECTIVE lo,
		LNCFLOANINTRATEDET li,
         MBUCFDISTRICT md,
         MBUCFPROVINCE mp,
         MBUCFTAMBOL mt
   WHERE ( mp.province_code (+) = md.province_code) and
         ( mb.amphur_code = md.district_code (+)) and
         ( trim(mb.province_code) = md.province_code (+)) and
         ( mb.tambol_code = mt.tambol_code (+)) and
         ( mb.amphur_code = mt.district_code (+)) and
         ( lr.COOP_ID = mb.COOP_ID ) and
		(lr.LOANTYPE_CODE = lt.LOANTYPE_CODE) and
(lr.LOANTYPE_CODE = lo.LOANTYPE_CODE) and
		(lr.LOANOBJECTIVE_CODE = lo.LOANOBJECTIVE_CODE) and
         ( mg.COOP_ID = mb.COOP_ID ) and
         ( mg.MEMBGROUP_CODE = mb.MEMBGROUP_CODE ) and
         ( mn.PRENAME_CODE = mb.PRENAME_CODE ) and
         ( lr.MEMBER_NO = mb.MEMBER_NO )  and
		(li.LOANINTRATE_CODE = lt.INTTABRATE_CODE )and
		(lr.APPROVE_DATE between li.EFFECTIVE_DATE and li.EXPIRE_DATE ) and
		(lr.LOANREQUEST_STATUS=11) and
		(lr.COOP_ID = {0}) and
		( lr.LOANREQUEST_DOCNO in (" + apvlist + @")  )
    ORDER By lr.LOANREQUEST_DOCNO";

                    //LtServerMessage.Text = apvlist;

                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl);

                    iReportArgument args = new iReportArgument(sql);
                    //args.Add("as_coop_id", iReportArgumentType.String, state.SsCoopId);
                    //args.Add("as_loanreqdocno", iReportArgumentType.String, apvlist);
                    iReportBuider report = new iReportBuider(this, "กำลังสร้างใบปะหน้าพิจารณาการขอกู้");
                    report.AddCriteria("r_ln_print_loan_req_doc_gsb", "ใบปะหน้าพิจารณาการขอกู้", ReportType.pdf, args);
                    report.AutoOpenPDF = true;
                    report.Retrieve();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }

        protected void PrintColl_Click(object sender, EventArgs e)
        {
            //apvlist = "'Q580000207','Q580000208'";
            if (apvlist != "")
            {
                try
                {

                    string sql = @"select ft_getmbname(lc.coop_id,lr.member_no) as full_name,
mb.member_no,
lr.loanrequest_date,
TO_CHAR(lr.loanrequest_date,'dd') as day,
TO_CHAR(lr.loanrequest_date,'fmMonth') as month,
TO_CHAR(lr.loanrequest_date,'yyyy')+543 as year,
mb.card_person,
mb.Addr_No,
mb.Addr_Moo,
mb.Addr_Soi,
mb.Addr_Village,
mb.Addr_Road,
mb.Tambol_Code,
tb.tambol_desc,
mb.Amphur_Code,
dt.district_desc,
mb.Province_Code,
mb.Addr_Postcode,
mb.Addr_Mobilephone,
lr.loanapprove_amt,
ftreadtbaht(lr.loanapprove_amt) as loanapprove_tbaht,
ft_getmbname(lc.coop_id,trim(lc.ref_collno)) as coll_name ,
lc.ref_collno,
ft_memgrp(lc.coop_id,mb.membgroup_code) as membgroup_desc,
lc.collactive_amt,
ftreadtbaht(lc.collactive_amt) as collactive_tbaht,
trunc(Ft_Calage( birth_date , sysdate , 4 )) as birth,
pr.province_desc
from
lnreqloan lr,
lnreqloancoll  lc,
mbmembmaster mb,
mbucfprovince pr,
mbucfdistrict dt,
mbucftambol tb
where  lr.loanrequest_docno = lc.loanrequest_docno
and mb.province_code = pr.province_code
and mb.amphur_code = dt.district_code
and mb.tambol_code = tb.tambol_code
and lr.coop_id = lc.coop_id
and lr.coop_id = mb.coop_id
and trim(lc.ref_collno) = mb.member_no
and lr.loanrequest_docno in (" + apvlist + @")
and lr.coop_id = {0}
and lc.loancolltype_code='01'";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
                    iReportArgument args = new iReportArgument(sql);
                    //args.Add("as_coop_id", iReportArgumentType.String, state.SsCoopId);
                    //args.Add("as_loanrequest_docno", iReportArgumentType.String, apvlist);

                    iReportBuider report = new iReportBuider(this, "กำลังสร้างใบปะหน้าพิจารณาการขอกู้");
                    report.AddCriteria("r_ln_print_loan_coll_doc_gsb", "ใบปะหน้าพิจารณาการขอกู้", ReportType.pdf, args);
                    report.AutoOpenPDF = true;
                    report.Retrieve();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }

        protected void PrintIns_Click(object sender, EventArgs e)
        {
            if (apvlist != "")
            {
                try
                {
                    string sql = @"SELECT mp.PRENAME_DESC||''||mb.MEMB_NAME||' '|| mb.MEMB_SURNAME as fullname,
         lr.MEMBER_NO,
TO_CHAR(lr.LOANREQUEST_DATE, 'dd MON yyyy', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') as datethai,
         lr.LOANTYPE_CODE,
         lr.LOANREQUEST_DOCNO,
         lr.LOANPERMISS_AMT,
         lr.LOANREQUEST_AMT,
         lr.LOANPAYMENT_TYPE,
         lr.PERIOD_PAYMENT,
         mb.MEMB_NAME,
         mb.MEMB_SURNAME,
         mb.MEMBGROUP_CODE,
         mg.MEMBGROUP_DESC,
         cc.COOP_NAME,
         cc.MANAGER,
         lr.LOANREQUEST_DATE,
         lr.LOANCONTRACT_NO,
         mb.MEMBER_NO,
         mb.SALARY_ID,
         mb.POSITION_DESC,
TO_CHAR(lr.APPROVE_DATE, 'dd MON yyyy', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') as dateAPP,
         lr.APPROVE_DATE,
         lr.LOANAPPROVE_AMT,
ftreadtbaht( lr.LOANAPPROVE_AMT) as LOANAPPROVE_TBAHT
    FROM LNREQLOAN lr,
         MBMEMBMASTER mb,
         MBUCFMEMBGROUP mg,
         MBUCFPRENAME mp,
         CMCOOPCONSTANT cc,
         LNLOANTYPE lt,
         LNUCFLOANOBJECTIVE lo,
         LNCFLOANINTRATEDET li
   WHERE ( lr.COOP_ID = mb.COOP_ID ) and
         ( lr.LOANTYPE_CODE = lt.LOANTYPE_CODE ) and
         ( lr.LOANOBJECTIVE_CODE = lo.LOANOBJECTIVE_CODE ) and
         ( mg.COOP_ID = mb.COOP_ID ) and
         ( mg.MEMBGROUP_CODE = mb.MEMBGROUP_CODE ) and
         ( mp.PRENAME_CODE = mb.PRENAME_CODE ) and
         ( lr.MEMBER_NO = mb.MEMBER_NO ) and
         ( li.LOANINTRATE_CODE = lt.INTTABRATE_CODE ) and
	(lr.APPROVE_DATE between li.EFFECTIVE_DATE and li.EXPIRE_DATE) and
	(lo.LOANTYPE_CODE  =lr.LOANTYPE_CODE ) and
         ( lr.COOP_ID = {0}) AND
         ( lr.LOANREQUEST_DOCNO in (" + apvlist + @"))
    ORDER By lr.LOANREQUEST_DOCNO";

                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
                    iReportArgument args = new iReportArgument(sql);
                    //args.Add("as_coop_id", iReportArgumentType.String, state.SsCoopId);
                    //args.Add("as_loanrequest_docno", iReportArgumentType.String, apvlist);

                    iReportBuider report = new iReportBuider(this, "");
                    report.AddCriteria("r_ln_print_loan_ins_doc_gsb", "ใบปะหน้าพิจารณาการขอกู้", ReportType.pdf, args);
                    report.AutoOpenPDF = true;
                    report.Retrieve();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }

        protected void PrintContSpc_Click(object sender, EventArgs e)
        {

            if (apvlist != "")
            {
                try
                {

                    string sql = @"select ft_getmbname(lr.coop_id,lr.member_no) as full_name,
mb.member_no,
mb.salary_id,
lr.loanrequest_date,
TO_CHAR(lr.loanrequest_date,'dd') as day,
TO_CHAR(lr.loanrequest_date,'fmMonth') as month,
TO_CHAR(lr.loanrequest_date,'yyyy')+543 as year,
mb.card_person,
lr.loanrequest_docno,
lr.coop_id,
mb.Addr_No,
mb.Addr_Moo,
mb.Addr_Soi,
mb.Addr_Village,
mb.Addr_Road,
mb.Tambol_Code,
mb.Amphur_Code,
mb.Province_Code,
mb.Addr_Postcode,
mb.Addr_Mobilephone,
lr.loanapprove_amt,
ftreadtbaht(lr.loanapprove_amt) as loanapprove_tbaht,
ft_memgrp(lr.coop_id,mb.membgroup_code) as membgroup_desc,
trunc(Ft_Calage( birth_date , sysdate , 4 )) as birth,
pr.province_desc,
mt.tambol_desc,
md.district_desc,
lr.period_payment,
ftreadtbaht(lr.period_payment) as tperiod_payment,
lr.period_payamt,
lo.loanobjective_desc,
ld.interest_rate
from
lnreqloan lr,
mbmembmaster mb,
mbucfprovince pr,
mbucfdistrict md,
mbucftambol mt,
lnucfloanobjective lo,
lncfloanintratedet ld,
lnloantype lc
where
lr.member_no = mb.member_no
and lr.loantype_code = lc.loantype_code
and lc.inttabrate_code = ld.loanintrate_code
and lr.approve_date between ld.effective_date and ld.expire_date
and trim(mb.province_code) = trim(pr.province_code)
and trim(mb.Amphur_Code) = trim(md.district_code)
and trim(mb.Tambol_Code) = trim(mt.tambol_code)
and lr.loantype_code = lo.loantype_code
and lr.loanobjective_code = lo.loanobjective_code
and lr.coop_id = mb.coop_id
and lr.loanrequest_docno in (" + apvlist + @")
and lr.coop_id = {0}";

                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
                    iReportArgument args = new iReportArgument(sql);
                    iReportBuider report = new iReportBuider(this, "กำลังสร้างใบปะหน้าพิจารณาการขอกู้เงินกู้พิเศษ");
                    report.AddCriteria("r_ln_print_loan_req_doc_spc_gsb", "ใบปะหน้าพิจารณาการขอกู้เงินกู้พิเศษ", ReportType.pdf, args);
                    report.AutoOpenPDF = true;
                    report.Retrieve();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }
        private int Jschklastdocnonew(string as_contno, string as_loantype_code)
        {

            try
            {
                string lncont_no = as_contno, last_condocno = "", ls_lastdocnonew = "";// dw_master.GetItemString(i, "loancontract_no");
                int lendocno = lncont_no.Substring(4, 6).Length;//lncont_no.Length - 4
                double lastdocno = Convert.ToDouble(lncont_no.Substring(4, Convert.ToInt16(lendocno)));
                string coop_id = state.SsCoopControl;
                lastdocno--;
                string ls_lastdocno = "00000000000" + lastdocno.ToString();
                ls_lastdocnonew = ls_lastdocno.Substring(ls_lastdocno.Length - lendocno, lendocno);
                string sql = @"select document_code from lnloantype where loantype_code = {0}";
                sql = WebUtil.SQLFormat(sql, as_loantype_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                string documentcode = dt.GetString("document_code");
                //a.document_code,
                string ls_sql = @"select   max( a.document_code || substr(  b.loancontract_no,3,2)||' ' || substr( b.loancontract_no,5,6) ) as lastdocno
                        from lncontmaster b , lnloantype a 
                        where a.loantype_code = b.loantype_code and  a.coop_id = b.coop_id and a.coop_id = '" + coop_id + "' and a.document_code = '" + documentcode + "'";
                try
                {
                    Sdt dt2 = WebUtil.QuerySdt(ls_sql);

                    if (dt2.Next())
                    {
                        last_condocno = dt2.GetString("lastdocno");

                    }
                    // last_condocno = ls_lastdocno.Substring(ls_lastdocno.Length - lendocno, lendocno);
                    string lastcontno22 = lncont_no.Substring(0, 3) + ls_lastdocno + lncont_no.Substring(8, 2);
                    string last_condocno4 = last_condocno.Substring(last_condocno.Length - 4, 4);

                    if (Convert.ToDouble(last_condocno4) != lastdocno)
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage("การอนุมัติเลขสัญญาเงินกู้ เลขสัญญา " + as_contno + " ไม่เป็นเลขต่อจากเลขที่ล่าสุดของก่อนหน้านั้น(เลขล่าสุด = " + last_condocno + " ) <br />กรุณาตรวจสอบด้วย");
                    }
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("การอนุมัติเลขสัญญาเงินกู้ เลขสัญญา " + as_contno + " ไม่พบเลขที่ล่าสุดของก่อนหน้านั้น <br />กรุณาตรวจสอบด้วย");
                }
            }
            catch
            {

            }
            return 1;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using CoreSavingLibrary.WcfNShrlon;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_approve_loan_ctrl
{
    public partial class ws_sl_approve_loan : PageWebSheet, WebSheet
    {
        private n_shrlonClient shrlonService;

        [JsPostBack]
        public string PostSearch { get; set; }
        [JsPostBack]
        public string PostGenContNo { get; set; }
        [JsPostBack]
        public string PrintCont { get; set; }
        [JsPostBack]
        public string PrintColl { get; set; }
        [JsPostBack]
        public string PrintIns { get; set; }
        [JsPostBack]
        public string PrintContSpc { get; set; }
        static string str = "";
        static string apvlist = "";

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                dsList.ResetRow();
                dsMain.DATA[0].select_check = "0";
                dsMain.DATA[0].appv_status = "8";

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


                dsList.RetrieveList(sqlwhere, dsMain.DATA[0].order_type);
            }
            else if (eventArg == PostGenContNo)
            {
                GenContNo();
            }
            else if (eventArg == PrintCont)
            {
                PrintCont_Click();
            }
            else if (eventArg == PrintColl)
            {
                PrintColl_Click();
            }
            else if (eventArg == PrintIns)
            {
                PrintIns_Click();
            }
            else if (eventArg == PrintContSpc)
            {
                PrintContSpc_Click();
            }
            this.SetOnLoadedScript(" parent.Setfocus();");
        }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void SaveWebSheet()
        {
            

            apvlist = "";
            str = "";
            
            for (int i = 0; i < dsList.RowCount; i++)
            {
                if (dsList.DATA[i].LOANREQUEST_STATUS != 8)
                {
                    string req_doc = "";
                    str += ";" + dsList.DATA[i].LOANREQUEST_DOCNO + "," + dsList.DATA[i].LOANREQUEST_STATUS + "," + dsList.DATA[i].LOANCONTRACT_NO;
                    if (apvlist == "")
                    {
                        apvlist = "'" + dsList.DATA[i].LOANREQUEST_DOCNO.Trim() + "'";
                    }
                    else
                    {
                        apvlist = apvlist + ",'" + dsList.DATA[i].LOANREQUEST_DOCNO.Trim() + "'";
                    }
                    if(state.SsCoopId == "013001"){
                    req_doc = dsList.DATA[i].LOANREQUEST_DOCNO.Trim();

                    string sqlinsert = @"insert into lnspcreport(entry_id,loanrequest_docno,entry_date) 
values ({0},{1},sysdate)";

                    sqlinsert = WebUtil.SQLFormat(sqlinsert, state.SsUsername, req_doc);
                    DataTable dt = WebUtil.Query(sqlinsert);
                    }
                }
            }
            str = str.Substring(1);

            

            try
            {
                int result = wcf.NShrlon.of_save_lnapv(state.SsWsPass, str, state.SsUsername, state.SsWorkDate);
                if (result == 1)
                {
                    //keng แก้ให้ subport หน้าจ่ายกู้ ATM 22/01/2018
                    for (int ii = 0; ii < dsList.RowCount; ii++)
                    {
                        if (dsList.DATA[ii].choose_flag == 1)
                        {
                            if (dsList.DATA[ii].LOANREQUEST_STATUS == 1)
                            {
                                try
                                {
                                    string sql_atm = @"select loantype_desc from lnloantype where loantype_code = {0}";
                                    sql_atm = WebUtil.SQLFormat(sql_atm, dsList.DATA[ii].LOANTYPE_CODE);
                                    Sdt rs_atm = WebUtil.QuerySdt(sql_atm);
                                    rs_atm.Next();
                                    if (rs_atm.GetString("loantype_desc").IndexOf("ATM") > 0)
                                    {
                                        string sql_update = @"update lnreqloan set loanrequest_status = 13
                                    where loantype_code = {0} and loanrequest_docno = {1}";
                                        sql_update = WebUtil.SQLFormat(sql_update, dsList.DATA[ii].LOANTYPE_CODE, dsList.DATA[ii].LOANREQUEST_DOCNO);
                                        Sdt result_update = WebUtil.QuerySdt(sql_update);
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    ////////////////////////////////////////

                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    dsList.ResetRow();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            this.SetOnLoadedScript(" parent.Setfocus();");
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
                this.SetOnLoadedScript(" parent.Setfocus();");
                dsMain.DDloantype();
                if (state.SsCoopControl == "003001")
                {
                    dsMain.DATA[0].order_type = "3";
                    dsList.RetrieveList("", "3");
                }
                else
                {
                    dsList.RetrieveList("", "1");
                }
            }
        }

        public void WebSheetLoadEnd()
        {

        }

        private void GenContNo()
        {
            int k = 0;
            int count = dsList.RowCount;
            for (int i = 0; i < count; i++)
            {
                string req_coopid = state.SsCoopControl;

                String lncont_no = "";
                try { lncont_no = dsList.DATA[i].LOANCONTRACT_NO; }
                catch { lncont_no = ""; }

                decimal lncont_status = dsList.DATA[i].LOANREQUEST_STATUS;
                if (lncont_status == 1)
                {
                    if (lncont_no == "")
                    {
                        String loantype_code = dsList.DATA[i].LOANTYPE_CODE.Trim();
                        String newReqDocNo = wcf.NShrlon.of_gennewcontractno(state.SsWsPass, req_coopid, loantype_code);

                        k++;
                        if (k == 1)
                        {
                            Jschklastdocnonew(newReqDocNo, loantype_code);

                        }
                        dsList.DATA[i].LOANCONTRACT_NO = newReqDocNo;
                    }
                }
            }
        }
        public void PrintCont_Click()
        {
            //apvlist = "'Q580001066','Q580001814','Q580001017','Q580001028'";

            if (apvlist != "")
            {
                try
                {
                    string sql = @"SELECT  {fn concat ({fn concat ({fn concat (mn.PRENAME_DESC,mb.MEMB_NAME)},' ')},mb.MEMB_SURNAME)} as fullname,
//trunc(months_between(sysdate,mb.BIRTH_DATE)/12) 
' ' as age,
         lr.MEMBER_NO,
         lr.LOANTYPE_CODE,
         lr.LOANCREDIT_AMT,
         lr.LOANPERMISS_AMT,
         lr.LOANREQUEST_AMT,
//ftreadtbaht(lr.LOANREQUEST_AMT) 
' 'as thbathloan,
//ftreadtbaht(lr.PERIOD_PAYMENT) 
' 'as thbathpayment,
         lr.PERIOD_PAYMENT,
         lr.LOANPAYMENT_TYPE,
         mb.MEMB_NAME,
         mb.MEMB_SURNAME,
         mb.MEMBGROUP_CODE,
         mg.MEMBGROUP_DESC,
         cc.COOP_NAME,
         cc.MANAGER,

//{ fn concat( { fn concat(date_format(lr.LOANREQUEST_DATE,'%d/%m'),'/')},date_format(lr.LOANREQUEST_DATE,'%Y') + 543)} as datethai ,
dateadd(year,+543,lr.LOANREQUEST_DATE) as datethai ,

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
         //mp.PROVINCE_DESC,
         mt.TAMBOL_DESC,
         mb.TAMBOL_CODE,
         mb.AMPHUR_CODE,
         mb.PROVINCE_CODE,
         mb.ADDR_POSTCODE,
pt.POSITION_DESC,
case when (mb.POSITION_DESC) is null then '' else mb.POSITION_DESC end  as POSDESC ,

lr.LOANREQUEST_DATE ,

         lr.PERIOD_PAYAMT,
		mb.ADDR_PHONE,
		lr.PERIOD_LASTPAYMENT,
		li.INTEREST_RATE,
		lo.LOANOBJECTIVE_DESC,
		 //FT_CALAGEMTH(mb.BIRTH_DATE,SYSDATE)
'' AS BIRTH_DATE

    FROM LNREQLOAN lr,
     
         MBUCFMEMBGROUP mg,
         MBUCFPRENAME mn,
         CMCOOPCONSTANT cc,
		LNLOANTYPE lt,
		LNUCFLOANOBJECTIVE lo,
		LNCFLOANINTRATEDET li,
         MBUCFPOSITION pt,
       MBMEMBMASTER mb
        
        
  


left join  MBUCFDISTRICT md  on mb.amphur_code = md.district_code
and   rtrim(ltrim(mb.province_code)) = md.province_code 
left join MBUCFPROVINCE mp  on mp.province_code  = md.province_code

left join MBUCFTAMBOL mt  on mb.tambol_code = mt.tambol_code 
and mb.amphur_code = mt.district_code
         
//( mp.province_code (+) = md.province_code) and
         //( mb.amphur_code = md.district_code (+)) and
        // ( rtrim(ltrim(mb.province_code)) = md.province_code (+)) and

         //( mb.tambol_code = mt.tambol_code (+)) and
         //( mb.amphur_code = mt.district_code (+)) and
 WHERE 
         ( lr.COOP_ID = mb.COOP_ID ) and
		(lr.LOANTYPE_CODE = lt.LOANTYPE_CODE) and
(lr.LOANTYPE_CODE = lo.LOANTYPE_CODE) and
		(lr.LOANOBJECTIVE_CODE = lo.LOANOBJECTIVE_CODE) and
         ( mg.COOP_ID = mb.COOP_ID ) and
        ( pt.POSITION_CODE = mb.POSITION_CODE ) and
         ( mg.MEMBGROUP_CODE = mb.MEMBGROUP_CODE ) and
         ( mn.PRENAME_CODE = mb.PRENAME_CODE ) and
         ( lr.MEMBER_NO = mb.MEMBER_NO )  and
		(li.LOANINTRATE_CODE = lt.INTTABRATE_CODE )and
		(lr.APPROVE_DATE between li.EFFECTIVE_DATE and li.EXPIRE_DATE ) and
		(lr.LOANREQUEST_STATUS=11) and
		(lr.COOP_ID = {0}) and
		( lr.LOANREQUEST_DOCNO in (" + apvlist + @")  )
    ORDER By lr.LOANREQUEST_DOCNO ";

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

        public void PrintColl_Click()
        {
            //apvlist = "'Q580000207','Q580000208'";
            if (apvlist != "")
            {
                try
                {

                    string sql = @"select DISTINCT ft_getmbname(lc.coop_id,lr.member_no) as full_name,
mb.member_no,
lr.loanrequest_date,


//TO_CHAR(lr.loanrequest_date,'dd') as day,
//TO_CHAR(lr.loanrequest_date,'fmMonth') as month,

//TO_CHAR(lr.loanrequest_date,'yyyy')+543 as year,
dateadd(year,-543,lr.loanrequest_date),

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
mg.membgroup_desc,
mp.position_desc,
//ftreadtbaht(lr.loanapprove_amt) 
''as loanapprove_tbaht,
//ft_getmbname(lc.coop_id,trim(lc.ref_collno)) 
''as coll_name ,
lc.ref_collno,
//ft_memgrp(lc.coop_id,mb.membgroup_code) 
''as membgroup_desc,
lc.collactive_amt,
//ftreadtbaht(lc.collactive_amt) 
''as collactive_tbaht,
//trunc(Ft_Calage( birth_date , sysdate , 4 )) 
''as birth,
pr.province_desc,
LNUCFLOANOBJECTIVE.LOANOBJECTIVE_DESC
from
lnreqloan lr,
lnreqloancoll  lc,
mbmembmaster mb,
mbucfmembgroup mg,
mbucfprovince pr,
mbucfdistrict dt,
mbucftambol tb,
mbucfposition mp,
LNUCFLOANOBJECTIVE
where  (lr.LOANOBJECTIVE_CODE = LNUCFLOANOBJECTIVE.LOANOBJECTIVE_CODE)  and lr.loanrequest_docno = lc.loanrequest_docno
and mb.province_code = pr.province_code
and mb.amphur_code = dt.district_code
and mb.tambol_code = tb.tambol_code
and lr.coop_id = lc.coop_id
and lr.coop_id = mb.coop_id
and mb.position_code = mp.position_code
and mb.membgroup_code = mg.membgroup_code
and rtrim(ltrim(lc.ref_collno)) = mb.member_no
and lr.loanrequest_docno in (" + apvlist + @")
and lr.coop_id = {0}
and lc.loancolltype_code in ('01','55','99')";
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

        public void PrintIns_Click()
        {

            if (apvlist != "")
            {
                try
                {
                    string sql = @"SELECT 
{ fn concat({ fn concat({ fn concat(mp.PRENAME_DESC,mb.MEMB_NAME) },'')},mb.MEMB_SURNAME)} as fullname,


         lr.MEMBER_NO,
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
         lr.LOANCONTRACT_NO,
         mb.MEMBER_NO,
         mb.SALARY_ID,
         pt.POSITION_DESC,

//TO_CHAR(lr.APPROVE_DATE, 'dd MON yyyy', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') as dateAPP,
dateadd(year,+543,lr.APPROVE_DATE)  as dateAPP,

         lr.LOANAPPROVE_AMT,

//ftreadtbaht( lr.LOANAPPROVE_AMT) 
''as LOANAPPROVE_TBAHT
    FROM LNREQLOAN lr,
         MBMEMBMASTER mb,
         MBUCFMEMBGROUP mg,
         MBUCFPRENAME mp,
         MBUCFPOSITION pt,
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
         ( pt.POSITION_CODE = mb.POSITION_CODE ) and
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
                string ls_sql = @"select   max( a.document_code + substr(  b.loancontract_no,3,2) +' ' + substr( b.loancontract_no,5,6) ) as lastdocno
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
        public void PrintContSpc_Click()
        {
            try
            { 
                iReportArgument args = new iReportArgument();
                args.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                args.Add("as_entryid", iReportArgumentType.String, state.SsUsername);
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

}
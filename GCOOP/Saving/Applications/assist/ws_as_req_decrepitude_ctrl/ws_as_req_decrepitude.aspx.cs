using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using CoreSavingLibrary.WcfNCommon;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace Saving.Applications.assist.ws_as_req_decrepitude_ctrl
{
    public partial class ws_as_req_decrepitude : PageWebSheet, WebSheet
    {
        Sdt dt = new Sdt();
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostAssistType { get; set; }
        [JsPostBack]
        public string PostAssistYear { get; set; }
        [JsPostBack]
        public string PostCalage { get; set; }
        [JsPostBack]
        public string PostRetriveBankBranch { get; set; }
        [JsPostBack]
        public string PostLinkAddress { get; set; }
        [JsPostBack]
        public string PostGetChildAge { get; set; }
        [JsPostBack]
        public string PostCardPerson { get; set; }
        [JsPostBack]
        public string PostCardPersonParent { get; set; }
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        [JsPostBack]
        public string PostInitPermiss { get; set; }
        [JsPostBack]
        public string PostCalMedDay { get; set; }
        [JsPostBack]
        public string PostAssistPay { get; set; }
        [JsPostBack]
        public string JsInsertrow { get; set; }
        [JsPostBack]
        public string Postmembname_ref { get; set; }
        [JsPostBack]
        public string Jspostdel { get; set; }
        [JsPostBack]
        public string CheckCardperson { get; set; }
        [JsPostBack]
        public string InitHistory { get; set; }
        [JsPostBack]
        public string CheckDocdate { get; set; }
        [JsPostBack]
        public string PostReqOld { get; set; }
        [JsPostBack]
        public string JsClearrow { get; set; }
        [JsPostBack]
        public string JsPostDispay { get; set; }
        [JsPostBack]
        public string JsInsertRowBonus { get; set; }


        public string sqlStr;

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsPatronize.InitDsPatronize(this);
            dsAmount.InitDsAmount(this);
            dsList.InitDsList(this);

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)//show data first  
            {
                HdTokenIMG.Value = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                this.of_setdefaultassist();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string ls_memno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);

                dsMain.DATA[0].MEMBER_NO = ls_memno;
                this.of_reset();
                this.of_settap();
                this.of_initpermiss();
            }
            else if (eventArg == PostAssistType)
            {
                if (dsMain.DATA[0].ASSIST_DOCNO == "")
                {
                    this.of_reset();
                    this.of_settap();
                    this.of_initpermiss();
                }
                else
                {
                    this.of_settap();
                    this.of_initpermiss();
                }
            }
            else if (eventArg == PostCalage)
            {
                this.of_calagemb();
            }
            else if (eventArg == PostRetriveBankBranch)
            {
                dsAmount.RetrieveBranch(dsAmount.DATA[0].EXPENSE_BANK);
            }
  
            else if (eventArg == PostAssistYear)
            {
                this.of_initpermiss();
            }
            else if (eventArg == PostInitPermiss)
            {
                this.of_initpermiss();
            }
            else if (eventArg == PostReqOld)
            {
                this.of_postreqold();
            }
            else if (eventArg == PostAssistPay)
            {
                this.of_setpermiss(dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].ASSISTTYPE_CODE);
            }
           
            else if (eventArg == CheckDocdate)
            {
                string ls_assgrp = hdassgrp.Value;
                DateTime old_date = state.SsWorkDate;
                Int32 docdate_num = 0;
                string sql = @"select docdate_num                       
                         from assucfassisttype 
		                 where assisttype_code = {0} and coop_id = {1}";
                sql = WebUtil.SQLFormat(sql, dsMain.DATA[0].ASSISTTYPE_CODE, state.SsCoopId);

                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    docdate_num = dt.GetInt32("docdate_num");
                }

                if (docdate_num > 0)
                {
                    TimeSpan a = TimeSpan.FromDays(0);
                    TimeSpan b = TimeSpan.FromDays(docdate_num);


                    if (ls_assgrp == "05")
                    {
                        old_date = dsPatronize.DATA[0].FAM_DOCDATE;
                        a = dsMain.DATA[0].REQ_DATE - dsPatronize.DATA[0].FAM_DOCDATE;
                        if (b < a)
                        {
                            dsPatronize.DATA[0].FAM_DOCDATE = old_date;
                            LtServerMessage.Text = WebUtil.ErrorMessage("วันที่ยื่นเอกสารเกินกว่ากำหนดที่ตั้งไว้ " + docdate_num.ToString() + " วัน");
                        }
                    }
                }
            }

        }


        private void of_reset()
        {

            dsPatronize.ResetRow();
            dsAmount.ResetRow();
            dsList.ResetRow();
        }

        private void of_setdefaultassist()
        {
            Sdt dt1;

            dsMain.AssistType();
            dsMain.GetAssYear();
            dsAmount.RetrieveMoneyType();
            dsAmount.DATA[0].MONEYTYPE_CODE = "CSH";

            sqlStr = @"select max(ass_year) ass_year from assucfyear where coop_id = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl);
            dt1 = WebUtil.QuerySdt(sqlStr);
            dt1.Next();
            dsMain.DATA[0].ASSIST_YEAR = dt1.GetInt32("ass_year");

            sqlStr = @"select min(assisttype_code) as assisttype_code from assucfassisttype where coop_id = {0} and assisttype_code = '50'";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl);
            dt1 = WebUtil.QuerySdt(sqlStr);
            dt1.Next();

            dsMain.DATA[0].ASSISTTYPE_CODE = dt1.GetString("assisttype_code");
            dsMain.DATA[0].reqstatus_desc = "ปกติ";

            dsMain.DATA[0].REQ_DATE = state.SsWorkDate;
            dsMain.DATA[0].CALAGE_DATE = state.SsWorkDate;

            this.of_settap();
        }

        private void of_settap()
        {
            string ls_asstype = "", ls_assgrp = "", ls_minpaytype = "", ls_mingaincode = "", ls_disaster = "", ls_invcode = "", ls_unitcode = "", ls_minunitcode = "";
            string ls_acttap = "0", invt_id = "", ls_housestatus = "";

            ls_asstype = dsMain.DATA[0].ASSISTTYPE_CODE;

            if (string.IsNullOrEmpty(ls_asstype) || ls_asstype == "00")
            {
                return;
            }

            sqlStr = @"select * from ASSUCFASSISTTYPE where assisttype_code = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, ls_asstype);
            Sdt dt = WebUtil.QuerySdt(sqlStr);

            if (dt.Next())
            {
                ls_assgrp = dt.GetString("assisttype_group");

                hdasscondition.Value = Convert.ToString(dt.GetDecimal("calculate_flag"));
                hdassgrp.Value = ls_assgrp;
                hdDateflag.Value = Convert.ToString(dt.GetDecimal("date_flag"));
            }
        if (ls_assgrp == "05") //เกื้อกูลสมาชิก
            {
                dsPatronize.Visible = true;

                ls_acttap = "1";

                dsPatronize.DdAsspaytype(ls_asstype, ref ls_minpaytype);
                dsPatronize.DATA[0].ASSISTPAY_CODE = ls_minpaytype;
            }

            hdTabIndex.Value = ls_acttap;

        }

        private void of_initpermiss()
        {
            string ls_memno = "", ls_asstype = "", ls_reqno = "";
            decimal li_year = 0;

            ls_memno = dsMain.DATA[0].MEMBER_NO;
            ls_asstype = dsMain.DATA[0].ASSISTTYPE_CODE;
            li_year = dsMain.DATA[0].ASSIST_YEAR;

            if (string.IsNullOrEmpty(ls_memno))
            {
                return;
            }

            // ตรวจว่ามีใบคำขอหรือยัง ถ้ามีไปเปิด
            if (this.of_haveoldreq(ls_memno, ls_asstype, li_year, ref ls_reqno))
            {
                this.of_retrieve(ls_memno, ls_asstype, ls_reqno);
                return;
            }

            // ตรวจสอบว่าเป็นสมาชิกหรือมีสิทธิ์ได้รับสวัสดิการมั้ย
            if (!this.of_chkassistmb(ls_memno, ls_asstype))
            {

                dsMain.ResetRow();
                this.of_setdefaultassist();
                return;
            }
            this.of_setmbinfo(ls_memno);
            this.of_setshare_loan(ls_memno, ls_asstype);
            this.of_setpermiss(ls_memno, ls_asstype);

            dsAmount.RetrieveBank();
            dsAmount.RetrieveBranch(dsAmount.DATA[0].EXPENSE_BANK);
            dsAmount.RetrieveMoneyType();
            dsAmount.RetrieveDeptaccount(ls_memno);

            //dsGain.DdGainRelation(ref ls_mingaincode);
            //dsGain.DATA[0].GAINRELATION_CODE = ls_mingaincode;

            dsList.RetrieveHistory(ls_memno, ls_asstype);
        }

        private void of_setshare_loan(string as_memno, string as_asstype)
        {
            string loantype_code = "";
            decimal principal_cal = 0, loan_percent = 0, ldc_sharevalue = 0, princal = 0;
            int loan_flag = 0;

            //set หุ้น
            string sql = @" select shsharemaster.sharestk_amt as sharestk_amt , shsharemaster.sharestk_amt * shsharetype.unitshare_value as share_value
                        from shsharemaster
                        left join shsharetype on shsharemaster.sharetype_code = shsharetype.sharetype_code and shsharemaster.coop_id = shsharetype.coop_id
                        where shsharemaster.coop_id = {0} and shsharemaster.member_no = {1} ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, as_memno);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {

                dsAmount.DATA[0].SHARE_VALUE = dt.GetDecimal("share_value");
                dsAmount.DATA[0].SHARESTK_AMT = dt.GetDecimal("sharestk_amt");
                ldc_sharevalue = dt.GetDecimal("share_value");

            }

            //set หนี้
            sql = @" select sum( principal_balance ) as principal_balance
                        from lncontmaster
                        where coop_id = {0} and member_no = {1} and contract_status <> -1 and principal_balance > 0";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, as_memno);
            dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsAmount.DATA[0].PRINCIPAL_BALANCE = dt.GetDecimal("principal_balance");
            }


            sql = @" select loan_flag , loan_percent
                        from assucfassisttype
                        where coop_id = {0} and assisttype_code = {1} ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, as_asstype);
            dt = WebUtil.QuerySdt(sql);

            if (dt.Next())
            {
                loan_flag = dt.GetInt32("loan_flag");
                loan_percent = dt.GetDecimal("loan_percent");
            }

            if (loan_flag == 1) //ตรวจสอบการหักชำระหนี้
            {
                //set หนี้ที่นำมาหักสวัสดิการ
                sql = @" select assisttype_loan 
                        from assucfassisttypeloan
                        where coop_id = {0} and assisttype_code = {1} ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, as_asstype);
                dt = WebUtil.QuerySdt(sql);
                while (dt.Next())
                {
                    loantype_code = dt.GetString("assisttype_loan");

                    string sql2 = @" select sum( principal_balance ) as principal_cal
                        from lncontmaster
                        where coop_id = {0} and member_no = {1} and contract_status <> -1 and principal_balance > 0
                        and loantype_code = {2}";
                    sql2 = WebUtil.SQLFormat(sql2, state.SsCoopId, as_memno, loantype_code);
                    Sdt dt2 = WebUtil.QuerySdt(sql2);
                    if (dt2.Next())
                    {
                        principal_cal += dt2.GetDecimal("principal_cal");
                    }

                }
            }

            //ตรวจสอบมูลค่าหนี้ว่ามากกว่าหุ้นตามจำนวน % ที่ตั้งไว้หรือไม่

            princal = principal_cal;
            ldc_sharevalue = ldc_sharevalue * ((100 + loan_percent) / 100);

            if (princal > ldc_sharevalue)
            {
                princal = princal - ldc_sharevalue;
                dsAmount.DATA[0].PRINCIPAL_CAL = princal;
            }
            else
            {
                dsAmount.DATA[0].PRINCIPAL_CAL = 0;
            }

        }

        private void of_setmbinfo(string as_memno)
        {
            string ls_assgrp = "", ls_asstype = "", ls_membage = "", ls_membyear = "", ls_membmonth = "", ls_mingaincode = "", ls_tranmemage = "", ls_tranyear = "";
            string ls_birthage = "", ls_birthyear = "", ls_birthmonth = "", default_paytype = "", ls_tranmonth = "";
            DateTime ldtm_reqdate;

            ls_asstype = dsMain.DATA[0].ASSISTTYPE_CODE;
            ldtm_reqdate = dsMain.DATA[0].CALAGE_DATE;

            string sql = @"select 
		                        m.member_no,
		                        ft_getmbname(m.coop_id,m.member_no) as mbname,
		                        trim( m.membgroup_code ) || ' : ' || trim( mbgroup.membgroup_desc ) as mbgroup,
                                m.membtype_code,
		                        t.membtype_code || ' ' || t.membtype_desc mbtypedesc,
                                t.membcat_code,
		                        m.birth_date,
		                        m.member_date,
                                m.tranmem_date,
		                        ftcm_calagemth(m.birth_date,{1}) birth_age,
		                        ftcm_calagemth(m.member_date,{1}) as member_age,
                                ftcm_calagemth(m.tranmem_date,{1}) as tranmem_age,
                                nvl( trunc(months_between({1},m.tranmem_date)) , 0 ) as age_tranmth,
                                nvl( trunc(months_between({1},m.member_date)), 0) as age_membmth,
                                m.salary_amount,
		                        m.card_person,
                                ft_getmbaddr(m.coop_id, m.member_no, 1) as mbaddr,
                                expense_code, 
		                        expense_bank, 
		                        expense_branch, 
		                        case expense_code when 'TRN' then '' else expense_accid end expense_accid,
                                case expense_code when 'TRN' then 'DEP' else '' end as send_system,
		                        case expense_code when 'TRN' then expense_accid else '' end deptaccount_no,
                                ' ' as mate_cardperson,
                                case m.mariage_status when 1 then 'โสด' when 2 then 'สมรส' when 3 then 'หย่า' when 4 then 'หม้าย' else 'ไม่ระบุ' end as mariage_desc,
                                'ปกติ' as reqstatus_desc
                         from mbmembmaster m
                              join mbucfmembtype t on t.membtype_code = m.membtype_code
                              join mbucfmembgroup mbgroup on m.membgroup_code = mbgroup.membgroup_code
		                 where m.member_no = {0} ";
            sql = WebUtil.SQLFormat(sql, as_memno, ldtm_reqdate);

            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMain.DATA[0].mbname = dt.GetString("mbname");
                dsMain.DATA[0].mbgroup = dt.GetString("mbgroup");
                dsMain.DATA[0].membtype_code = dt.GetString("membtype_code");
                dsMain.DATA[0].mbtypedesc = dt.GetString("mbtypedesc");
                dsMain.DATA[0].membcat_code = dt.GetString("membcat_code");
                dsMain.DATA[0].birth_date = dt.GetDate("birth_date");
                dsMain.DATA[0].member_date = dt.GetDate("member_date");
                dsMain.DATA[0].tranmem_date = dt.GetDate("tranmem_date");
                dsMain.DATA[0].birth_age = dt.GetDecimal("birth_age");
                dsMain.DATA[0].member_age = dt.GetDecimal("member_age");
                dsMain.DATA[0].tranmem_age = dt.GetDecimal("tranmem_age");
                dsMain.DATA[0].salary_amount = dt.GetDecimal("salary_amount");
                dsMain.DATA[0].card_person = dt.GetString("card_person");
                dsMain.DATA[0].mbaddr = dt.GetString("mbaddr");
                dsMain.DATA[0].age_membmth = dt.GetInt32("age_membmth");
                dsMain.DATA[0].age_tranmth = dt.GetInt32("age_tranmth");
                dsMain.DATA[0].mariage_desc = dt.GetString("mariage_desc");
                dsMain.DATA[0].reqstatus_desc = dt.GetString("reqstatus_desc");

                dsAmount.DATA[0].MONEYTYPE_CODE = dt.GetString("expense_code");
                dsAmount.DATA[0].EXPENSE_BANK = dt.GetString("expense_bank");
                dsAmount.DATA[0].EXPENSE_BRANCH = dt.GetString("expense_branch");
                dsAmount.DATA[0].EXPENSE_ACCID = dt.GetString("expense_accid");
                dsAmount.DATA[0].DEPTACCOUNT_NO = dt.GetString("deptaccount_no");
                dsAmount.DATA[0].SEND_SYSTEM = dt.GetString("send_system");

                ls_membage = Convert.ToString(dt.GetDecimal("member_age"));
                ls_birthage = Convert.ToString(dt.GetDecimal("birth_age"));
                ls_tranmemage = Convert.ToString(dt.GetDecimal("tranmem_age"));
            }
            //แปลงวันที่เป็นข้อความ
            string[] ls_age = ls_birthage.Split('.');
            ls_birthyear = ls_age[0] + " ปี ";
            ls_birthmonth = ls_age[1] + " เดือน";
            dsMain.DATA[0].birthdate_th = ls_birthyear + ls_birthmonth;

            string[] ls_memage = ls_membage.Split('.');
            ls_membyear = ls_memage[0] + " ปี ";
            ls_membmonth = ls_memage[1] + " เดือน";
            dsMain.DATA[0].membdate_th = ls_membyear + ls_membmonth;

            string[] ls_tranage = ls_tranmemage.Split('.');
            ls_tranyear = ls_tranage[0] + " ปี ";
            ls_tranmonth = ls_tranage[1] + " เดือน";
            dsMain.DATA[0].tranmem_th = ls_tranyear + ls_tranmonth;


            dsAmount.DATA[0].MONEYTYPE_CODE = "TRN";
            dsAmount.DATA[0].EXPENSE_BANK = "";
            dsAmount.DATA[0].EXPENSE_BRANCH = "";
            dsAmount.DATA[0].EXPENSE_ACCID = "";
            dsAmount.DATA[0].DEPTACCOUNT_NO = "";
            dsAmount.DATA[0].SEND_SYSTEM = "MRT";
  
            ls_assgrp = hdassgrp.Value;

           
        }

        private void of_calagemb()
        {
            string ls_memno, ls_membage, ls_birthage, ls_birthyear, ls_birthmonth, ls_membyear, ls_membmonth, ls_tranage, ls_tranyear, ls_tranmonth;
            DateTime ldtm_caldate;

            ls_memno = dsMain.DATA[0].MEMBER_NO;
            ldtm_caldate = dsMain.DATA[0].CALAGE_DATE;

            if (string.IsNullOrEmpty(ls_memno))
            {
                return;
            }
            string sql = @"select 
		                        ftcm_calagemth(m.birth_date,{1}) birth_age,
		                        ftcm_calagemth(m.member_date,{1}) as member_age,
                                ftcm_calagemth(m.tranmem_date,{1}) as tran_age,
                                nvl( trunc(months_between({1},m.tranmem_date)) , 0 ) as age_tranmth,
                                nvl( trunc(months_between({1},m.member_date)), 0) as age_membmth,
                         from mbmembmaster m
                              join mbucfmembtype t on t.membtype_code = m.membtype_code
                              join mbucfmembgroup mbgroup on m.membgroup_code = mbgroup.membgroup_code
		                 where m.member_no = {0} ";
            sql = WebUtil.SQLFormat(sql, ls_memno, ldtm_caldate);

            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMain.DATA[0].birth_age = dt.GetDecimal("birth_age");
                dsMain.DATA[0].member_age = dt.GetDecimal("member_age");
                dsMain.DATA[0].age_membmth = dt.GetInt32("age_membmth");
                dsMain.DATA[0].tranmem_age = dt.GetDecimal("tran_age");
                dsMain.DATA[0].age_tranmth = dt.GetInt32("age_tranmth");
            }

            ls_membage = Convert.ToString(dt.GetDecimal("member_age"));
            ls_birthage = Convert.ToString(dt.GetDecimal("birth_age"));
            ls_tranage = Convert.ToString(dt.GetDecimal("tran_age"));

            //แปลงวันที่เป็นข้อความ
            string[] ls_age = ls_birthage.Split('.');
            ls_birthyear = ls_age[0] + " ปี ";
            ls_birthmonth = ls_age[1] + " เดือน";
            dsMain.DATA[0].birthdate_th = ls_birthyear + ls_birthmonth;

            string[] ls_memage = ls_membage.Split('.');
            ls_membyear = ls_memage[0] + " ปี ";
            ls_membmonth = ls_memage[1] + " เดือน";
            dsMain.DATA[0].membdate_th = ls_membyear + ls_membmonth;

            string[] ls_tranmemage = ls_tranage.Split('.');
            ls_tranyear = ls_tranmemage[0] + " ปี ";
            ls_tranmonth = ls_tranmemage[1] + " เดือน";
            dsMain.DATA[0].membdate_th = ls_tranyear + ls_tranmonth;
        }

        public void SaveWebSheet()
        {

            string ls_reqno = "", ls_assgrp = "";
            Boolean lb_isnew = false;

            ls_reqno = dsMain.DATA[0].ASSIST_DOCNO;

            // เป็นคำขอใหม่
            if (string.IsNullOrEmpty(ls_reqno))
            {
                ls_reqno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "ASSISTDOCNO");
                lb_isnew = true;
            }

            dsAmount.DATA[0].COOP_ID = state.SsCoopControl;
            dsAmount.DATA[0].ASSIST_DOCNO = ls_reqno;
            dsAmount.DATA[0].MEMBER_NO = dsMain.DATA[0].MEMBER_NO;
            dsAmount.DATA[0].ASSISTTYPE_CODE = dsMain.DATA[0].ASSISTTYPE_CODE;
            dsAmount.DATA[0].ASSIST_YEAR = dsMain.DATA[0].ASSIST_YEAR;
            dsAmount.DATA[0].REQ_DATE = dsMain.DATA[0].REQ_DATE;
            dsAmount.DATA[0].CALAGE_DATE = dsMain.DATA[0].CALAGE_DATE;
            dsAmount.DATA[0].ASS_RCVNAME = dsMain.DATA[0].mbname;
            dsAmount.DATA[0].ASS_RCVCARDID = dsMain.DATA[0].card_person;

            dsAmount.DATA[0].REQ_STATUS = 8;
            dsAmount.DATA[0].ENTRY_ID = state.SsUsername;

            //////////////////////////init ค่าไม่ให้ null/////////////////////////////////////////

            dsAmount.DATA[0].ASS_RCVNAME = "";
            dsAmount.DATA[0].ASS_RCVCARDID = "";
            dsAmount.DATA[0].ASS_PRCARDID = "";

            dsAmount.DATA[0].EDU_SCHOOL = "";
            dsAmount.DATA[0].EDU_LEVELCODE = "";
            dsAmount.DATA[0].EDU_GPA = 0;
            dsAmount.DATA[0].ASSISTPAY_CODE = "";

            dsAmount.DATA[0].DEC_CAUSE = "";


            dsAmount.DATA[0].DIS_ADDR = "";
            dsAmount.DATA[0].DIS_DISAMT = 0;
            dsAmount.DATA[0].disaster_code = "";//////////////////////////////////////////////////////////////////
            dsAmount.DATA[0].dis_homedoc = "";//////////////////////////////////////////////////////////////////

            dsAmount.DATA[0].MED_HPNAME = "";
            dsAmount.DATA[0].MED_CAUSE = "";
            dsAmount.DATA[0].MED_DAY = 0;

            //////////////////////////////////////////////////////////////////

            ls_assgrp = hdassgrp.Value;

                dsAmount.DATA[0].ASSISTPAY_CODE = dsPatronize.DATA[0].ASSISTPAY_CODE;

           
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);

                if (lb_isnew)
                {
                    string sqlStr = @"INSERT INTO ASSREQMASTER
                    (COOP_ID 			            , ASSIST_DOCNO 		            , ASSISTTYPE_CODE               , ASSIST_YEAR                   , REQ_DATE 
                    , CALAGE_DATE                   , MEMBER_NO                     , ASSISTPAY_CODE                , ASSIST_AMT                    , ASSISTMAX_AMT
                    , ASSISTEVER_AMT                , ASSISTCUT_AMT                 , ASSISTNET_AMT                 , REMARK                        , REQ_STATUS 
                    , ENTRY_ID                     
                    , MONEYTYPE_CODE                , EXPENSE_BANK                  , EXPENSE_BRANCH                , EXPENSE_ACCID                 , SEND_SYSTEM 
                    , DEPTACCOUNT_NO                , ASS_RCVNAME                   , ASS_RCVCARDID                 , ASS_PRCARDID                  , EDU_CHILDBIRTHDATE 
                    , EDU_SCHOOL                    , EDU_LEVELCODE                 , EDU_GPA                       , DEC_DEADDATE                  , DEC_CAUSE 
                    , FAM_DOCDATE                   , DIS_ADDR                      , DIS_DISDATE                   , DIS_DISAMT                    , MED_HPNAME 
                    , MED_CAUSE                     , MED_STARTDATE                 , MED_ENDDATE                   , MED_DAY                       , STM_FLAG
                    , STMPAY_TYPE                   , STMPAY_NUM                    , PRINCIPAL_BALANCE             , PRINCIPAL_CAL                 ,  SHARESTK_AMT
                    , SHARE_VALUE                   , dis_homedoc                   , dis_distype                   , dis_house_status
                    )
                    values
                    ({0}                            , {1}                           , {2}                           , {3}                           , {4} 
                    , {5}                           , {6}                           , {7}                           , {8}                           , {9}  
                    , {10}                          , {11}                          , {12}                          , {13}                          , {14}   
                    , {15}                         
                    , {16}                          , {17}                          , {18}                          , {19}                          , {20}
                    , {21}                          , {22}                          , {23}                          , {24}                          , {25}
                    , {26}                          , {27}                          , {28}                          , {29}                          , {30}
                    , {31}                          , {32}                          , {33}                          , {34}                          , {35}
                    , {36}                          , {37}                          , {38}                          , {39}                          , {40}
                    , {41}                          , {42}                          , {43}                          , {44}                          , {45}
                    , {46}                          , {47}                          , {48}                          , {49}
                    )";

                    sqlStr = WebUtil.SQLFormat(sqlStr
                  , state.SsCoopId, ls_reqno, dsAmount.DATA[0].ASSISTTYPE_CODE, dsAmount.DATA[0].ASSIST_YEAR, dsAmount.DATA[0].REQ_DATE
                  , dsAmount.DATA[0].CALAGE_DATE, dsAmount.DATA[0].MEMBER_NO, dsAmount.DATA[0].ASSISTPAY_CODE, dsAmount.DATA[0].ASSIST_AMT, dsAmount.DATA[0].ASSISTMAX_AMT
                  , dsAmount.DATA[0].ASSISTEVER_AMT, dsAmount.DATA[0].ASSISTCUT_AMT, dsAmount.DATA[0].ASSISTNET_AMT, dsAmount.DATA[0].REMARK, dsAmount.DATA[0].REQ_STATUS
                  , state.SsUsername
                  , dsAmount.DATA[0].MONEYTYPE_CODE, dsAmount.DATA[0].EXPENSE_BANK, dsAmount.DATA[0].EXPENSE_BRANCH, dsAmount.DATA[0].EXPENSE_ACCID, dsAmount.DATA[0].SEND_SYSTEM
                  , dsAmount.DATA[0].DEPTACCOUNT_NO, dsAmount.DATA[0].ASS_RCVNAME, dsAmount.DATA[0].ASS_RCVCARDID, dsAmount.DATA[0].ASS_PRCARDID, dsAmount.DATA[0].EDU_CHILDBIRTHDATE
                  , dsAmount.DATA[0].EDU_SCHOOL, dsAmount.DATA[0].EDU_LEVELCODE, dsAmount.DATA[0].EDU_GPA, dsAmount.DATA[0].DEC_DEADDATE, dsAmount.DATA[0].DEC_CAUSE
                  , dsAmount.DATA[0].FAM_DOCDATE, dsAmount.DATA[0].DIS_ADDR, dsAmount.DATA[0].DIS_DISDATE, dsAmount.DATA[0].DIS_DISAMT, dsAmount.DATA[0].MED_HPNAME
                  , dsAmount.DATA[0].MED_CAUSE, dsAmount.DATA[0].MED_STARTDATE, dsAmount.DATA[0].MED_ENDDATE, dsAmount.DATA[0].MED_DAY, dsAmount.DATA[0].STM_FLAG
                  , dsAmount.DATA[0].STMPAY_TYPE, dsAmount.DATA[0].STMPAY_NUM, dsAmount.DATA[0].PRINCIPAL_BALANCE, dsAmount.DATA[0].PRINCIPAL_CAL, dsAmount.DATA[0].SHARESTK_AMT
                  , dsAmount.DATA[0].SHARE_VALUE, dsAmount.DATA[0].dis_homedoc, dsAmount.DATA[0].disaster_code, dsAmount.DATA[0].DIS_HOUSE_STATUS
                  );


                    WebUtil.ExeSQL(sqlStr);
                }
                else
                {
                    string sqlStr_update = "UPDATE ASSREQMASTER SET" +
                        " ASSISTTYPE_CODE   = '" + dsAmount.DATA[0].ASSISTTYPE_CODE + "', ASSISTNET_AMT     = '" + dsAmount.DATA[0].ASSISTNET_AMT + "'  , ASSISTCUT_AMT     = '" + dsAmount.DATA[0].ASSISTCUT_AMT + "'" +
                        ", ASSISTMAX_AMT    = '" + dsAmount.DATA[0].ASSISTMAX_AMT + "'	, ASSIST_AMT        = '" + dsAmount.DATA[0].ASSIST_AMT + "'     , ASSISTPAY_CODE    = '" + dsAmount.DATA[0].ASSISTPAY_CODE + "'" +
                        ", REMARK           = '" + dsAmount.DATA[0].REMARK + "'         , ENTRY_ID          = '" + state.SsUsername + "'	            , MONEYTYPE_CODE 	= '" + dsAmount.DATA[0].MONEYTYPE_CODE + "'" +
                        ", EXPENSE_BRANCH   = '" + dsAmount.DATA[0].EXPENSE_BRANCH + "' , EXPENSE_ACCID     = '" + dsAmount.DATA[0].EXPENSE_ACCID + "'  , DEPTACCOUNT_NO    = '" + dsAmount.DATA[0].DEPTACCOUNT_NO + "'" +
                        ", EXPENSE_BANK     = '" + dsAmount.DATA[0].EXPENSE_BANK + "'   , ASS_RCVNAME       = '" + dsAmount.DATA[0].ASS_RCVNAME + "'    , ASS_RCVCARDID     = '" + dsAmount.DATA[0].ASS_RCVCARDID + "'" +
                        ", EDU_SCHOOL       = '" + dsAmount.DATA[0].EDU_SCHOOL + "'     , EDU_GPA           = '" + dsAmount.DATA[0].EDU_GPA + "'        , SEND_SYSTEM       = '" + dsAmount.DATA[0].SEND_SYSTEM + "'" +
                        ", DIS_DISAMT       = '" + dsAmount.DATA[0].DIS_DISAMT + "'     , CALAGE_DATE       = to_date('" + dsAmount.DATA[0].CALAGE_DATE.ToString("dd/MM/yyyy") + @"','dd/MM/yyyy')    , EDU_CHILDBIRTHDATE  =  to_date('" + dsAmount.DATA[0].EDU_CHILDBIRTHDATE.ToString("dd/MM/yyyy") + @"','dd/MM/yyyy')" +
                        ", DIS_HOUSE_STATUS = '" + dsAmount.DATA[0].DIS_HOUSE_STATUS + "'  , EDU_LEVELCODE    = '" + dsAmount.DATA[0].EDU_LEVELCODE + "'" +
                        " WHERE ASSIST_DOCNO = '" + dsAmount.DATA[0].ASSIST_DOCNO + "' and coop_id = '" + state.SsCoopId + "'";
                    Sdt sql_update = WebUtil.QuerySdt(sqlStr_update);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ" + ex);
                return;
            }

            //try //บันทึกรูป
            //{
            //    WebUtil.SaveImageREQ(state.SsCoopId, state.SsApplication, "assist_docno", dsMain.DATA[0].ASSIST_DOCNO, HdTokenIMG.Value, state.SsUsername, state.SsWorkDate);
            //}
            //catch
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกรูปภาพไม่สำเร็จ");
            //    return;
            //}

            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");

            dsMain.ResetRow();
            dsList.ResetRow();
            dsPatronize.ResetRow();
            dsAmount.ResetRow();

            this.of_setdefaultassist();
        }


        public void WebSheetLoadEnd()
        {
            if (dsAmount.DATA[0].MONEYTYPE_CODE == "CSH" || dsAmount.DATA[0].MONEYTYPE_CODE == "")
            {
                dsAmount.FindDropDownList(0, "expense_bank").Enabled = false;
                dsAmount.FindTextBox(0, "expense_accid").Enabled = false;
                dsAmount.FindDropDownList(0, "expense_branch").Enabled = false;
                dsAmount.FindDropDownList(0, "send_system").Enabled = false;
                dsAmount.FindDropDownList(0, "deptaccount_no").Enabled = false;
            }
            else if (dsAmount.DATA[0].MONEYTYPE_CODE == "TRN")
            {
                dsAmount.FindDropDownList(0, "expense_bank").Enabled = false;
                dsAmount.FindTextBox(0, "expense_accid").Enabled = false;
                dsAmount.FindDropDownList(0, "expense_branch").Enabled = false;
                dsAmount.FindDropDownList(0, "send_system").Enabled = true;
                dsAmount.FindDropDownList(0, "deptaccount_no").Enabled = false;
            }
            else
            {
                dsAmount.FindDropDownList(0, "expense_bank").Enabled = true;
                dsAmount.FindTextBox(0, "expense_accid").Enabled = true;
                dsAmount.FindDropDownList(0, "expense_branch").Enabled = true;
                dsAmount.FindDropDownList(0, "send_system").Enabled = false;
                dsAmount.FindDropDownList(0, "deptaccount_no").Enabled = false;
            }
        }

        // สิทธิ์สวัสดิการ
        private decimal of_getpermissmax(string as_memno, string as_asscode, string as_paycode, decimal adc_rank, ref decimal adc_max)
        {
            string ls_sqlext = "";
            decimal ldc_permiss = 0;

            sqlStr = @"select * from asscontmaster ass
                      where ass.member_no = '" + as_memno + "' and ass.assisttype_code='" + as_asscode + "' and ass.asscont_status <> -9 ";
            sqlStr = sqlStr + ls_sqlext;
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                ldc_permiss = dtd.GetDecimal("approve_amt");
                adc_max = dtd.GetDecimal("approve_amt");
            }

            return ldc_permiss;
        }

        private decimal of_getpermissever(string as_memno, string as_asscode)
        {
            string ls_sqlext = "";
            Int32 li_numper = 0, li_perunit = 0, li_cuttype = 0;
            decimal ldc_usedamt = 0, ldc_apvamt = 0;

            sqlStr = @"select * from asscontmaster ass
                      where ass.member_no = '" + as_memno + "' and ass.assisttype_code='" + as_asscode + "' and ass.asscont_status <> -9 ";
            sqlStr = sqlStr + ls_sqlext;
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                ldc_usedamt = dtd.GetDecimal("pay_balance");
            }

            return ldc_usedamt;
        }

        private decimal of_getpermisscut(string as_memno, string as_asscode)
        {
            string ls_sqlext = "";
            Int32 li_numper = 0, li_perunit = 0;
            decimal ldc_cutamt = 0, ldc_apvamt = 0;

            sqlStr = @"select * from ASSUCFASSISTTYPE where assisttype_code = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, as_asscode);
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                li_numper = dtd.GetInt32("limitper_num");
                li_perunit = dtd.GetInt32("limitper_unit");
            }

            // หักสิทธิที่เบิกไปแล้ว หาก่อนว่าช่วงมันเป็นแบบไหน
            // 1=นับปี 2=นับเดือน(ชนเดือน) 3=นับเดือน(ชนวัน)
            ls_sqlext = this.of_getsqlrank(li_perunit, li_numper, "ass");

            sqlStr = @"select * from asscontmaster ass
                       where exists ( select 1 from assucfassisttypecut acut where acut.assisttype_code = '" + as_asscode + @"' and ass.assisttype_code = acut.assisttype_cut )
                       and ass.member_no = '" + as_memno + "' and ass.asscont_status <> -9 ";
            sqlStr = sqlStr + ls_sqlext;
            dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                ldc_apvamt = dtd.GetDecimal("pay_balance");
                ldc_cutamt = ldc_cutamt + ldc_apvamt;
            }

            return ldc_cutamt;
        }

        private Boolean of_chkassistmb(string as_memno, string as_asscode)
        {
            string ls_assgrp = "", ls_mbtypecode = "", ls_mbtypedesc = "", ls_sqlext = "", ls_sqlunion = "", ls_sqlextrq = "", ls_mbcatcode = "";
            string ls_cardid = "", ls_rcvtype = "", ls_memrcv = "", ls_asspausetype = "", ls_assinstype = "";
            Int32 li_resignstt = 0, li_deadstt = 0, li_mbtypechk = 0, li_mbcattypechk = 0;
            Int32 li_numreq = 0, li_numper = 0, li_perunit = 1, li_round = 0;
            Int32 li_cntass = 0, li_family = 0, li_age = 0, li_loan = 0;
            Decimal ldc_memage = 0, ldc_age = 0, ldc_agenum = 0, ldc_tranage;

            // ตรวจสอบสถานะของสมาชิกที่ทำการขอสวัสดิการ
            sqlStr = @" select mb.card_person, mb.resign_status, mb.dead_status, mb.membtype_code, mt.membtype_desc , mg.membcat_code, 
                        ftcm_calagemth(mb.birth_date, {1} ) birth_age,
		                ftcm_calagemth(mb.member_date,{1}) as member_age,
                        ftcm_calagemth(mb.tranmem_date,{1}) as tran_age
                        from mbmembmaster mb 
                            join mbucfmembtype mt on mb.membtype_code = mt.membtype_code 
                            join mbucfcategory mg on mt.membcat_code = mg.membcat_code
                        where mb.member_no = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, as_memno, dsMain.DATA[0].CALAGE_DATE);
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                ls_cardid = dtd.GetString("card_person");
                li_resignstt = dtd.GetInt32("resign_status");
                li_deadstt = dtd.GetInt32("dead_status");
                ls_mbtypecode = dtd.GetString("membtype_code");
                ls_mbtypedesc = dtd.GetString("membtype_desc");
                ls_mbcatcode = dtd.GetString("membcat_code");
                ldc_age = dtd.GetDecimal("birth_age");
                ldc_memage = dtd.GetDecimal("member_age");
                ldc_tranage = dtd.GetDecimal("tran_age");
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลสมาชิกเลขที่ " + as_memno + " กรุณาตรวจสอบ ");
                return false;
            }

            if (li_resignstt == 1)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ " + as_memno + " ได้ลาออกไปแล้วไม่สามารถขอสวัสดิการได้อีก ");
                return false;
            }

            if (ls_assgrp == "05" && li_deadstt == 0)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ยังไม่มีสถานะเป็นเสียชีวิต " + as_memno + " กรุณาปรับสถานะก่อน");
                return false;
            }

            sqlStr = @"select * from asscontmaster ass
                      where ass.member_no = '" + as_memno + "' and ass.assisttype_code='" + as_asscode + "' and ass.asscont_status <> -9 ";
            dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {

            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลการขอสวัสดิการทุนสงเคราะห์สมาชิกทุพพลภาพของ " + as_memno + " กรุณาตรวจสอบ ");
                return false;
            }


            return true;
        }

        private void of_chkassistrcvidcardpause(string as_rcvcardid, string as_asscode)
        {
            string ls_asspausetype = "";
            sqlStr = "select assisttype_pause from assucfassisttypepause where assisttype_code = '" + as_asscode + "' and coop_id = '" + state.SsCoopId + "'";
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                ls_asspausetype = dtd.GetString("assisttype_pause");

                Boolean result = of_chkassistrcvidcard(as_rcvcardid, ls_asspausetype);
                if (result == false)
                {
                    string ls_assgrp = hdassgrp.Value;
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ถูกงดสวัสดิการประเภทนี้ เนื่องจากเคยได้รับสวัสดิการประเภท " + ls_asspausetype + " แล้ว");
                }


            }

        }



        private Boolean of_chkassistrcvidcard(string as_rcvcardid, string as_asscode)
        {
            string ls_sqlext = "", ls_sqlextrq = "", ls_memno = "", ls_assgrp = "";
            Int32 li_numreq = 0, li_numper = 0, li_perunit = 1;
            Int32 li_cntass = 0;

            // config สำหรับการตรวจสอบสวัสดิการ
            sqlStr = @"select * from ASSUCFASSISTTYPE where assisttype_code = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, as_asscode);
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                ls_assgrp = dtd.GetString("assisttype_group");
                li_numreq = dtd.GetInt32("limrcvreq_num");
                li_numper = dtd.GetInt32("limrcvper_num");
                li_perunit = dtd.GetInt32("limrcvper_unit");
            }

            // ระยะเวลาสำหรับการหา
            ls_sqlext = this.of_getsqlrank(li_perunit, li_numper, "ass");
            ls_sqlextrq = this.of_getsqlrankreq(li_perunit, li_numper, "req");

            // ตรวจสอบจากบัตรประชาชนที่ขอ
            sqlStr = @"select member_no from asscontmaster ass 
                       where ass.ass_rcvcardid = '" + as_rcvcardid + "' and ass.assisttype_code = '" + as_asscode + @"'
                       and ass.asscont_status <> -9 " + ls_sqlext + @"
                       union
                       select member_no from assreqmaster req 
                       where req.ass_rcvcardid = '" + as_rcvcardid + "' and req.assisttype_code='" + as_asscode + @"'
                       and req.req_status = 8 " + ls_sqlextrq;


            dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                ls_memno = dtd.GetString("member_no");

                li_cntass++;

                if (li_cntass >= li_numreq)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกครอบครัวคนนี้ ได้เคยขอสวัสดิการประเภทนี้ไปแล้ว สมาชิกที่ขอ (" + ls_memno + ") ยังไม่ถึงเวลาที่จะขอสวัสดิการได้อีก");
                    return false;
                }
            }

            return true;
        }

        private Boolean of_chkassistparentidcard(string as_prcardid, string as_asscode)
        {
            string ls_sqlext = "", ls_sqlextrq = "", ls_memno = "", ls_memlist = "", ls_rcvtype = "", ls_sqlunion = "";
            Int32 li_numreq = 0, li_numper = 0, li_perunit = 1;
            Int32 li_cntass = 0, li_family = 0;

            // config สำหรับการตรวจสอบสวัสดิการ
            sqlStr = @"select * from ASSUCFASSISTTYPE where assisttype_code = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, as_asscode);
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                li_family = dtd.GetInt32("family_flag");

                li_numreq = dtd.GetInt32("limitreq_num");
                li_numper = dtd.GetInt32("limitper_num");
                li_perunit = dtd.GetInt32("limitper_unit");
            }

            if (li_family != 1)
            {
                return true;
            }

            // ระยะเวลาสำหรับการหา
            ls_sqlext = this.of_getsqlrank(li_perunit, li_numper, "ass");
            ls_sqlextrq = this.of_getsqlrankreq(li_perunit, li_numper, "req");

            // ตรวจสอบจากบัตรประชาชนที่ขอ
            sqlStr = @" select 'CO' as rcvtype, ass.member_no 
                        from asscontmaster ass
                        where ass.ass_prcardid = '" + as_prcardid + "' and ass.assisttype_code='" + as_asscode + @"' and ass.asscont_status <> -9 " + ls_sqlext + @"
                        union
                        select 'OWN' as rcvtype, ass.member_no 
                        from asscontmaster ass
                        where ass.ass_rcvcardid = '" + as_prcardid + "' and ass.assisttype_code='" + as_asscode + @"' and ass.asscont_status <> -9 " + ls_sqlext;

            ls_sqlunion = @" select 'CO' as rcvtype, req.member_no 
                        from assreqmaster req
                        where req.ass_prcardid = '" + as_prcardid + "' and req.assisttype_code='" + as_asscode + @"' and req.req_status = 8 " + ls_sqlextrq + @"
                        union
                        select 'OWN' as rcvtype, ass.member_no 
                        from asscontmaster ass
                        where req.ass_rcvcardid = '" + as_prcardid + "' and req.assisttype_code='" + as_asscode + @"' and req.req_status = 8 " + ls_sqlextrq;

            dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                ls_rcvtype = dtd.GetString("rcvtype");
                ls_memno = dtd.GetString("member_no");

                if (ls_rcvtype == "OWN")
                {
                    ls_memlist = ls_memlist + ls_memno + "(ขอเอง) ";
                }
                else
                {
                    ls_memlist = ls_memlist + ls_memno + "(ผู้ปกครอง) ";
                }

                li_cntass++;

                if (li_cntass >= li_numreq)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ผู้ปกครองคนนี้ ได้เคยขอสวัสดิการประเภทนี้ไปแล้ว สมาชิกที่ขอ (" + ls_memlist + ") ยังไม่ถึงเวลาที่จะขอสวัสดิการได้อีก");
                }
            }

            return true;
        }

        private string of_getsqlrank(Int32 ai_perunit, Int32 ai_numper, string as_table)
        {
            string ls_lastdate, ls_sqlext = "";
            Int32 li_assyear = 0;
            DateTime ldtm_caldate;

            li_assyear = Convert.ToInt32(dsMain.DATA[0].ASSIST_YEAR);
            ldtm_caldate = dsMain.DATA[0].CALAGE_DATE;

            // 1=นับปี 2=นับเดือน(ชนเดือน) 3=นับเดือน(ชนวัน)
            if (ai_perunit == 1)
            {
                li_assyear = li_assyear - ai_numper;
                ls_sqlext = " and " + as_table + ".assist_year > " + Convert.ToString(li_assyear);
            }
            else if (ai_perunit == 2)
            {
                ls_lastdate = ldtm_caldate.AddMonths(ai_numper * -1).ToString("yyyyMM");
                ls_sqlext = " and to_char(" + as_table + ".approve_date,'yyyymm') > '" + ls_lastdate + "'";
            }
            else if (ai_perunit == 3)
            {
                ls_lastdate = ldtm_caldate.AddMonths(ai_numper * -1).ToString("yyyyMMdd");
                ls_sqlext = " and " + as_table + ".approve_date > to_date('" + ls_lastdate + "','yyyymmdd')";
            }

            return ls_sqlext;
        }

        private string of_getsqlrankreq(Int32 ai_perunit, Int32 ai_numper, string as_table)
        {
            string ls_lastdate, ls_sqlext = "";
            Int32 li_assyear = 0;
            DateTime ldtm_caldate;

            li_assyear = Convert.ToInt32(dsMain.DATA[0].ASSIST_YEAR);
            ldtm_caldate = dsMain.DATA[0].CALAGE_DATE;

            // 1=นับปี 2=นับเดือน(ชนเดือน) 3=นับเดือน(ชนวัน)
            if (ai_perunit == 1)
            {
                li_assyear = li_assyear - ai_numper;
                ls_sqlext = " and " + as_table + ".assist_year > " + Convert.ToString(li_assyear);
            }
            else if (ai_perunit == 2)
            {
                ls_lastdate = ldtm_caldate.AddMonths(ai_numper * -1).ToString("yyyyMM");
                ls_sqlext = " and to_char(" + as_table + ".calage_date,'yyyymm') > '" + ls_lastdate + "'";
            }
            else if (ai_perunit == 3)
            {
                ls_lastdate = ldtm_caldate.AddMonths(ai_numper * -1).ToString("yyyyMMdd");
                ls_sqlext = " and " + as_table + ".calage_date > to_date('" + ls_lastdate + "','yyyymmdd')";
            }

            return ls_sqlext;
        }

        private void of_setpermiss(string as_memno, string as_asstype)
        {
            DateTime ldtm_famdocdate = state.SsWorkDate;
            string ls_assgrp = "", ls_sql = "", ls_dateflag = "";
            string ls_paycode = "", ls_paychoose = "", ls_asscond = "0";
            decimal ldc_rank = 0, ldc_maxamt = 0, ldc_permiss = 0, ldc_everamt = 0, ldc_cutamt = 0, ldc_netamt = 0;
            Sdt dtchk, dtchs;

            ls_assgrp = hdassgrp.Value;
            ls_asscond = hdasscondition.Value;
            ls_dateflag = hdDateflag.Value;  //0 = วันสมัครสมาชิก , 1 = วันโอนย้าย

            //เช็คเงื่อนไข กรณีที่ไม่มีวันโอนย้าย จะคิดจากวันที่เป็นสมาชิก
            if (ls_dateflag == "1" && dsMain.DATA[0].age_tranmth == 0)
            {
                ls_dateflag = "0";
            }

            // เงื่อนไขประกอบ
            if (ls_asscond == "2")  // อายุ
            {
                ldc_rank = dsMain.DATA[0].birth_age;
            }
            else if (ls_asscond == "3") // อายุสมาชิก
            {
                if (ls_dateflag == "0")
                {
                    ldc_rank = dsMain.DATA[0].age_membmth;
                }
                else
                {
                    ldc_rank = dsMain.DATA[0].age_tranmth;
                }
            }
            else if (ls_asscond == "4") // เงินเดือน
            {
                ldc_rank = dsMain.DATA[0].salary_amount;
            }

            // ดูเงื่อนไขจากกลุ่มสวัสดิการอีกรอบ
            if (ls_assgrp == "05") // เกื้อกูลสมาชิก
            {
                ls_paycode = dsPatronize.DATA[0].ASSISTPAY_CODE;
                ldtm_famdocdate = dsPatronize.DATA[0].FAM_DOCDATE;
            }


            if (string.IsNullOrEmpty(ls_paycode))
            {
                ls_sql = " select count(assistpay_code) as cntpay from assucfassisttypepay where assisttype_code = '" + as_asstype + "' ";

                dtchk = WebUtil.QuerySdt(ls_sql);
                if (dtchk.Next())
                {
                    if (dtchk.GetInt32("cntpay") == 1)
                    {
                        ls_sql = " select assistpay_code from assucfassisttypepay where assisttype_code = '" + as_asstype + "' ";

                        dtchs = WebUtil.QuerySdt(ls_sql);
                        if (dtchs.Next())
                        {
                            ls_paychoose = dtchs.GetString("assistpay_code");
                        }
                    }
                }

                if (string.IsNullOrEmpty(ls_paychoose))
                {
                    return;
                }

                ls_paycode = ls_paychoose;

                if (ls_assgrp == "05") // เกื้อกูลสมาชิก
                {
                    dsPatronize.DATA[0].ASSISTPAY_CODE = ls_paycode;
                    dsPatronize.DATA[0].FAM_DOCDATE = ldtm_famdocdate;
                }
            }



            ldc_permiss = this.of_getpermissmax(as_memno, as_asstype, ls_paycode, ldc_rank, ref ldc_maxamt);
            ldc_everamt = this.of_getpermissever(as_memno, as_asstype);
            ldc_cutamt = this.of_getpermisscut(as_memno, as_asstype);

            //ประเภทอุทกภัยจ่ายตามความเสียหาย
            if (ls_asscond == "5")
            {
                ldc_permiss = ldc_rank;
            }

            dsAmount.DATA[0].ASSISTMAX_AMT = ldc_maxamt;
            dsAmount.DATA[0].ASSIST_AMT = ldc_permiss;
            dsAmount.DATA[0].ASSISTEVER_AMT = ldc_everamt;
            dsAmount.DATA[0].ASSISTCUT_AMT = ldc_cutamt;

            if (ldc_permiss + ldc_everamt > ldc_maxamt)
            {
                ldc_netamt = ldc_maxamt - ldc_everamt;
            }
            else
            {
                ldc_netamt = ldc_permiss;
            }

            ldc_netamt = ldc_netamt - ldc_cutamt;

            dsAmount.DATA[0].ASSISTNET_AMT = ldc_netamt;
        }

        private Boolean of_haveoldreq(string as_memno, string as_assistcode, decimal ai_asststyear, ref string as_reqno)
        {
            //ตรวจสอบว่าเคยขอสวัสดิการนี้ไปละยัง
            sqlStr = @"select * from assreqmaster 
                    where req_status = 8 and coop_id = {0} and member_no = {1} and assisttype_code = {2} and assist_year = {3}";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_memno, as_assistcode, ai_asststyear);
            Sdt dt = WebUtil.QuerySdt(sqlStr);
            if (dt.Next())
            {
                as_reqno = dt.GetString("assist_docno");
                LtServerMessage.Text = WebUtil.WarningMessage(as_memno + " มีใบคำขอสวัสดิการประเภทนี้อยู่ ระบบได้ทำการเปิดใบคำขอให้");
                return true;
            }

            return false;
        }

        private void of_retrieve(string as_memno, string as_asstype, string as_reqno)
        {
            string ls_assgrp = "", ls_birthyear = "", ls_birthmonth = "", ls_membage = "", ls_birthage = "", ls_membyear = "", ls_membmonth = "", ls_minpaytype = "", ls_mingaincode = "";
            string ls_housestatus = "";
            dsMain.Retrieve(as_reqno);

            //แปลงวันที่เป็นข้อความ

            ls_membage = Convert.ToString(dsMain.DATA[0].member_age);
            ls_birthage = Convert.ToString(dsMain.DATA[0].birth_age);
            string[] ls_age = ls_birthage.Split('.');
            ls_birthyear = ls_age[0] + " ปี ";
            ls_birthmonth = ls_age[1] + " เดือน";
            dsMain.DATA[0].birthdate_th = ls_birthyear + ls_birthmonth;

            string[] ls_memage = ls_membage.Split('.');
            ls_membyear = ls_memage[0] + " ปี ";
            ls_membmonth = ls_memage[1] + " เดือน";
            dsMain.DATA[0].membdate_th = ls_membyear + ls_membmonth;
            ////////
            dsAmount.Retrieve(as_reqno);

            dsMain.AssistType();
            dsMain.GetAssYear();
            dsAmount.RetrieveMoneyType();
            dsAmount.RetrieveBank();
            dsAmount.RetrieveDeptaccount(as_memno);

            ls_assgrp = hdassgrp.Value;

            if (ls_assgrp == "05")
            {
                dsPatronize.Retrieve(as_reqno);
                dsPatronize.DdAsspaytype(as_asstype, ref ls_minpaytype);
            }
        }


        private void of_postreqold()
        {

            string ls_memno = "", ls_asstype = "", ls_reqno = "";
            decimal li_year = 0;

            ls_memno = dsMain.DATA[0].MEMBER_NO;
            ls_asstype = dsMain.DATA[0].ASSISTTYPE_CODE;
            li_year = dsMain.DATA[0].ASSIST_YEAR;
            ls_reqno = dsMain.DATA[0].ASSIST_DOCNO;

            this.of_retrieve(ls_memno, ls_asstype, ls_reqno);
            this.of_settap();
            return;

        }
    }
}
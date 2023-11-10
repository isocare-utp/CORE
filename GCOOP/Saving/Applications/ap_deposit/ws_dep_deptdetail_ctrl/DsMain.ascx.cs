using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_searchdeptno");
            this.Button.Add("b_calaccuint");
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void RetrieveMain(String ls_dept_no)
        {
            String sql = @"  
                         SELECT  DPDEPTMASTER.DEPTACCOUNT_NO,   
                         DPDEPTMASTER.DEPTTYPE_CODE,   
                         DPDEPTMASTER.MEMBER_NO,   
                         DPDEPTMASTER.DEPTOPEN_DATE,   
                         DPDEPTMASTER.DEPTCLOSE_STATUS,   
                         DPDEPTMASTER.DEPTCLOSE_DATE,   
                         DPDEPTMASTER.DEPTACCOUNT_NAME,   
                         DPDEPTMASTER.DEPT_OBJECTIVE,   
                         DPDEPTMASTER.DEPTPASSBOOK_NO  as DEPTPASSBOOK_NO,   
                         (CASE WHEN DPDEPTMASTER.DEPTMONTH_STATUS > 0 THEN 1 ELSE DPDEPTMASTER.DEPTMONTH_STATUS END) AS DEPTMONTH_STATUS,   
                         DPDEPTMASTER.DEPTMONTH_AMT,   
                         DPDEPTMASTER.DEPTMONTCHG_DATE,      
                         DPDEPTMASTER.MONTHINTPAY_METH,   
                         DPDEPTMASTER.TRAN_DEPTACC_NO as TRAN_DEPTACC_NO,   
                         DPDEPTMASTER.TRAN_BANKACC_NO as TRAN_BANKACC_NO,   
                         DPDEPTMASTER.SPCINT_RATE_STATUS,   
                         DPDEPTMASTER.SPCINT_RATE,   
                         DPDEPTMASTER.BEGINBAL,   
                         DPDEPTMASTER.PRNCBAL,   
                         DPDEPTMASTER.CHECKPEND_AMT,   
                         DPDEPTMASTER.LOANGARANTEE_AMT,   
                         DPDEPTMASTER.SEQUEST_STATUS,   
                         DPDEPTMASTER.WITHDRAWABLE_AMT,   
                         DPDEPTMASTER.LASTCALINT_DATE,   
                         DPDEPTMASTER.ACCUINT_AMT,   
                         DPDEPTMASTER.INTARREAR_AMT,   
                         DPDEPTMASTER.ACCUINTPAY_AMT,   
                         DPDEPTMASTER.ACCUTAXPAY_AMT,       
                         DPDEPTMASTER.LASTREC_NO_PB,   
                         DPDEPTMASTER.LASTPAGE_NO_PB,   
                         DPDEPTMASTER.LASTLINE_NO_PB,             
                         DPDEPTMASTER.LASTACCESS_DATE,   
                         DPDEPTMASTER.LASTSTMSEQ_NO,   
                         '        ' as lastcalint_tdate,   
                         '        ' as deptopen_tdate,      
                         DPDEPTMASTER.PRNC_NO,   
                         DPDEPTMASTER.CONDFORWITHDRAW,   
                         DPDEPTMASTER.BANK_BRANCH,   
                         DPDEPTMASTER.SEQUEST_AMOUNT,   
                         DPDEPTMASTER.DEPT_TRANACC_NAME,   
                         DPDEPTMASTER.SEQUEST_DATE,   
                         DPDEPTMASTER.BANK_CODE,   
                         DPDEPTMASTER.BOOK_BALANCE,     
                         DPDEPTTYPE.PERSONGRP_CODE,   
                         DPDEPTMASTER.COOP_ID,   
                         DPDEPTTYPE.BOOK_GROUP,   
                         DPDEPTTYPE.DEPTGROUP_CODE,   
                         CMUCFBANK.BANK_DESC,   
                         MBUCFPRENAME.PRENAME_DESC,   
                         MBMEMBMASTER.MEMB_SURNAME,   
                         MBMEMBMASTER.CARD_PERSON as CARD_PERSON,   
                         DPDEPTTYPE.DEPTTYPE_CODE,   
                         MBMEMBMASTER.MEMB_NAME,      
                         CMUCFBANKBRANCH.BRANCH_NAME as BRANCH_NAME,   
                         0 as cross_coopflag,   
                         '      ' as dwcrosscoop,  
                         (DPDEPTMASTER.WITHDRAWABLE_AMT - DPDEPTMASTER.SEQUEST_AMOUNT )  as sum_withdraw, 
                         DPDEPTTYPE.DEPTTYPE_CODE+' - '+ DPDEPTTYPE.DEPTTYPE_DESC as DEPTTYPE_DESC,
                         isnull(DPDEPTTYPE.minprncbal,0)as minprncbal
                     FROM DPDEPTMASTER
					 inner join DPDEPTTYPE on  DPDEPTTYPE.DEPTTYPE_CODE = DPDEPTMASTER.DEPTTYPE_CODE  and DPDEPTTYPE.COOP_ID = DPDEPTMASTER.COOP_ID
					 inner join  MBMEMBMASTER on MBMEMBMASTER.COOP_ID = DPDEPTMASTER.COOP_ID and MBMEMBMASTER.MEMBER_NO = DPDEPTMASTER.MEMBER_NO
					 left join CMUCFBANK on cmucfbank.bank_code =  dpdeptmaster.bank_code   
					 left join cmucfbankbranch on cmucfbankbranch.bank_code = dpdeptmaster.bank_code and cmucfbankbranch.branch_id = dpdeptmaster.bank_branch
					 inner join MBUCFPRENAME on MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE
					 inner join CMCOOPMASTER on CMCOOPMASTER.COOP_ID =  DPDEPTMASTER.COOP_ID
                   WHERE
                         ( ( DPDEPTMASTER.COOP_ID =  {0}) AND  
                         (  DPDEPTMASTER.DEPTACCOUNT_NO = {1} ) )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_dept_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

            string card_person = this.DATA[0].CARD_PERSON.Trim();
            if (card_person.Length == 13)
            {
                card_person = card_person.Substring(0, 1) + "-" + card_person.Substring(1, 4) + "-" + card_person.Substring(5, 5) + "-" + card_person.Substring(10, 2) + "-" + card_person.Substring(12);
            }
            this.DATA[0].CARD_PERSON = card_person;
        }
    }
}
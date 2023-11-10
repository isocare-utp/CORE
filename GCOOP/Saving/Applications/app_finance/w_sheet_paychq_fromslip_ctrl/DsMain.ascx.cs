using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.Applications.app_finance.w_sheet_paychq_fromslip_ctrl
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
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_accno");
            this.Button.Add("b_search");
            this.Register();
        }

        public void RetrieveMain()
        {
            string sql = @"
              ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void Ddbank()
        {
            string sql = @"
                     SELECT BANK_CODE,(BANK_CODE+' '+BANK_DESC) as BANK_DESC,1 as sorter  FROM CMUCFBANK
                    union
                    select '','--เลือกธนาคาร--',0  order by sorter,BANK_CODE asc";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "as_bank", "BANK_DESC", "BANK_CODE");
        }
        public void Ddbankbranch(string bank)
        {
            string sql = @"
                        SELECT BANK_CODE,(BRANCH_ID+'  '+BRANCH_NAME) as BRANCH_NAME,   BRANCH_ID,1 as sorter FROM CMUCFBANKBRANCH  WHERE BANK_CODE ={0}
                        union
                        select '','--เลือกสาขา--','',0  order by sorter, BRANCH_ID asc";
            sql = WebUtil.SQLFormat(sql, bank);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "as_bankbranch", "BRANCH_NAME", "BRANCH_ID");
        }
        public void DdChqbookno(string coopid,string bank,string bankbranch)
        {
            string sql = @"
                        SELECT FINCHEQUEMAS.CHEQUEBOOK_NO,   
                        FINCHEQUEMAS.BANK_CODE,   
                        FINCHEQUEMAS.BANK_BRANCH,   
                        FINCHEQUEMAS.COOP_ID ,
		                1 as sorter
                        FROM FINCHEQUEMAS  
                        WHERE
                        ( FINCHEQUEMAS.COOP_ID = {0} ) AND  
                        ( FINCHEQUEMAS.BANK_CODE = {1} ) AND  
                        ( FINCHEQUEMAS.BANK_BRANCH = {2} ) AND  
                        ( FINCHEQUEMAS.AVAILABLE_FLAG = 1 )    
            union
            select '--เลือกเล่มที่เช็ค--','','','',0  order by sorter,CHEQUEBOOK_NO asc
            ";
            sql = WebUtil.SQLFormat(sql,coopid, bank, bankbranch);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "as_chqbookno", "CHEQUEBOOK_NO", "CHEQUEBOOK_NO");
        }
        
        public void DdChqno(string coopid, string bank, string bankbranch,string chqbook_no)
        {
            string sql = @"
                        SELECT  CHEQUE_NO ,1 as sorter FROM FINCHQEUESTATEMENT   
                       WHERE (  FINCHQEUESTATEMENT.USE_STATUS = 0 ) and  
                            ( FINCHQEUESTATEMENT.COOP_ID = {0}) and    
                          ( FINCHQEUESTATEMENT.BANK_CODE = {1} ) And   
                           ( FINCHQEUESTATEMENT.BANK_BRANCH = {2} ) And     
                         ( FINCHQEUESTATEMENT.CHEQUEBOOK_NO = {3} )   
                     union
                      select '--เลือกเลขที่เช็ค--',0  order by sorter";
            sql = WebUtil.SQLFormat(sql, coopid, bank, bankbranch, chqbook_no);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "as_chqstartno", "CHEQUE_NO", "CHEQUE_NO");
        }
        public void DdChqType(string coopid)
        {
            string sql = @"
                        SELECT FINCHEQUETYPE.CHEQUE_TYPE,   
                        FINCHEQUETYPE.CHEQUE_DESC  
                        FROM FINCHEQUETYPE  
                        WHERE FINCHEQUETYPE.COOP_ID = {0}   
                        order by  CHEQUE_TYPE";
            sql = WebUtil.SQLFormat(sql, coopid);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "as_chqtype", "CHEQUE_DESC", "CHEQUE_TYPE");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_genrequest_education_to_promote_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MainDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_Main;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_process");
       //     this.Button.Add("b_save");
            this.Register();
        }

        public void AssistType()
        {
            string sql = @"select * from
                        (
	                        select
		                        ASSISTTYPE_CODE, 
		                        ASSISTTYPE_CODE+' - '+ASSISTTYPE_DESC as display, 
		                        1 as sorter
	                        from ASSUCFASSISTTYPE 
	                        union
	                        select top 1
		                        '00', 
		                        'กรุณาเลือกสวัสดิการ' as display, 
		                        1 as sorter
	                        from ASSUCFASSISTTYPE 
                        )as display
                        order by sorter, assisttype_code
                        ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_code", "display", "assisttype_code");

        }
        public void AssistType2()
        {
            string sql = @"select * from
                        (
	                        select
		                        ASSISTTYPE_CODE, 
		                        ASSISTTYPE_CODE+' - '+ASSISTTYPE_DESC as display, 
		                        1 as sorter
	                        from ASSUCFASSISTTYPE 
	                        union
	                        select top 1
		                        '00', 
		                        'กรุณาเลือกสวัสดิการ' as display, 
		                        1 as sorter
	                        from ASSUCFASSISTTYPE 
                        )display
                        order by sorter, assisttype_code
                        ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_code2", "display", "assisttype_code");

        }
        public void GetAssYear()
        {
            string sql = @"select ass_year + 543 ass_show from assucfyear order by ass_year desc";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assist_year", "ass_show", "ass_show");
        }
        public void DdMoneyType()
        {
            string sql = @" 
                   SELECT MONEYTYPE_CODE,  
                          MONEYTYPE_GROUP, 
                          MONEYTYPE_DESC,   
                          SORT_ORDER  ,
                          MONEYTYPE_CODE + ' - '+ MONEYTYPE_DESC as MONEYTYPE_DISPLAY
                     FROM CMUCFMONEYTYPE where MONEYTYPE_CODE in ('CSH','CHQ','CBT','TRN') order by sort_order
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "moneytype_code", "MONEYTYPE_DISPLAY", "MONEYTYPE_CODE");
        }
        public void DdFromAccId()
        {
            string sql = @"SELECT  
                            ACCMASTER.ACCOUNT_ID,    
                            ACCMASTER.ACCOUNT_ID +'-'+ACCMASTER.ACCOUNT_NAME AS fromacc_display,
                            ACCMASTER.ACCOUNT_NAME  ,  
                            1 as sorter
                        FROM ACCMASTER  WHERE ACCOUNT_TYPE_ID = '3'
                        union
                        select '', '-----กรุณาเลือก---','', 0  order by sorter, ACCOUNT_ID
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "tofrom_accid", "fromacc_display", "ACCOUNT_ID");
        }
        public void DdBankDesc()
        {
            string sql = @"
            select  bank_code,bank_code+ ' '+bank_desc  as bank_desc, 1 as sorter from cmucfbank 
            union 
            select '','', 0 order by sorter, bank_desc
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "expense_bank", "bank_desc", "bank_code");
        }
        public void DdBranch(string bank_code)
        {
            string sql = @"
            select bank_code,branch_id, branch_id+'-'+ branch_name branch_name, 1 as sorter from cmucfbankbranch where bank_code = {0} 
            union
            select '', '','', 0  order by sorter, branch_id
            ";
            sql = WebUtil.SQLFormat(sql, bank_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "expense_branch", "branch_name", "branch_id");
        }
    }
}
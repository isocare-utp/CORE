using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_adt_mbaudit_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBMEMBMASTERDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBMEMBMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");

            this.Button.Add("b_search");
            this.Button.Add("b_linkaddress");
            this.Button.Add("b_bank");
            this.Button.Add("b_branch");
        
            this.Register();
        }
        public void RetrieveMain(String member_no)
        {
            String sql = @"SELECT MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                                 '        ' as birth_tdate,   
                                 '        ' as member_tdate,   
                                 '        ' as work_tdate,   
                                 '        ' as retry_tdate,   
                                 MBMEMBMASTER.MEMBER_NO,   
                                 MBMEMBMASTER.PRENAME_CODE,   
                                 MBMEMBMASTER.MEMB_NAME,   
                                 MBMEMBMASTER.MEMB_SURNAME,   
                                 MBMEMBMASTER.MEMBER_DATE,   
                                 MBMEMBMASTER.BIRTH_DATE,   
                                 MBMEMBMASTER.SEX,   
                                 MBMEMBMASTER.MARIAGE_STATUS,   
                                 MBMEMBMASTER.MATE_NAME,   
                                 MBMEMBMASTER.ADDR_NO,   
                                 MBMEMBMASTER.ADDR_MOO,   
                                 MBMEMBMASTER.ADDR_SOI,   
                                 MBMEMBMASTER.ADDR_VILLAGE,   
                                 MBMEMBMASTER.ADDR_ROAD,   
                                 MBMEMBMASTER.TAMBOL_CODE,   
                                 rtrim(ltrim(MBMEMBMASTER.AMPHUR_CODE)) as AMPHUR_CODE,   
                                 MBMEMBMASTER.PROVINCE_CODE,    
                                 MBMEMBMASTER.ADDR_POSTCODE,   
                                 MBMEMBMASTER.ADDR_PHONE,   
                                 MBMEMBMASTER.ADDR_MOBILEPHONE,   
                                 MBMEMBMASTER.ADDR_EMAIL,   
                                 MBMEMBMASTER.CARD_PERSON,   
                                 MBMEMBMASTER.WORK_DATE,   
                                 MBMEMBMASTER.RETRY_DATE, 
                                 MBMEMBMASTER.POSITION_CODE,
                                 MBMEMBMASTER.POSITION_DESC,    
                                 MBMEMBMASTER.LEVEL_CODE,   
                                 MBMEMBMASTER.SALARY_AMOUNT,   
                                 MBMEMBMASTER.SALARY_ID,   
                                 MBMEMBMASTER.MEMBTYPE_CODE,   
                                 MBMEMBMASTER.MEMB_ENAME,   
                                 MBMEMBMASTER.MEMB_ESURNAME,   
                                 MBMEMBMASTER.RESIGN_STATUS,   
                                 MBMEMBMASTER.MEMBER_STATUS,   
                                 MBMEMBMASTER.MEMBGROUP_CODE,   
                                 MBMEMBMASTER.REMARK,   
                                 shsharemaster.periodshare_amt * shsharetype.unitshare_value as periodshare_value,   
                                 shsharemaster.periodbase_amt * shsharetype.unitshare_value as periodbase_value,   
                                 MBMEMBMASTER.CURRADDR_NO,   
                                 MBMEMBMASTER.CURRADDR_MOO,   
                                 MBMEMBMASTER.CURRADDR_SOI,   
                                 MBMEMBMASTER.CURRADDR_VILLAGE,   
                                 MBMEMBMASTER.CURRADDR_ROAD,   
                                 MBMEMBMASTER.CURRTAMBOL_CODE,   
                                 MBMEMBMASTER.CURRAMPHUR_CODE,   
                                 MBMEMBMASTER.CURRPROVINCE_CODE,   
                                 MBMEMBMASTER.CURRADDR_POSTCODE,   
                                 MBMEMBMASTER.CURRADDR_PHONE,   
                                 SHSHAREMASTER.SHARETYPE_CODE,   
                                 MBMEMBMASTER.COOP_ID,   
                                 0 as maxshare_value,   
                                 MBMEMBMASTER.DROPGURANTEE_FLAG,   
                                 MBMEMBMASTER.DROPLOANALL_FLAG,   
                                 MBMEMBMASTER.INCOMEETC_AMT,   
                                 MBMEMBMASTER.KLONGTOON_FLAG,   
                                 MBMEMBMASTER.PAUSEKEEP_FLAG,   
                                 MBMEMBMASTER.ALLOWLOAN_FLAG,   
                                 MBMEMBMASTER.TRANSRIGHT_FLAG,   
                                 MBMEMBMASTER.MATE_CARDPERSON,   
                                 MBMEMBMASTER.MATE_SALARYID,   
                                 MBMEMBMASTER.UPDATE_BYENTRYID,   
                                 MBMEMBMASTER.UPDATE_BYENTRYIP,   
                                 MBMEMBMASTER.EXPENSE_CODE,   
                                 rtrim( ltrim(MBMEMBMASTER.EXPENSE_BANK)) as expense_bank,   
                                 rtrim( ltrim(MBMEMBMASTER.EXPENSE_BRANCH))as EXPENSE_BRANCH,   
                                 MBMEMBMASTER.EXPENSE_ACCID,   
                                 CMUCFBANKBRANCH.BRANCH_NAME,   
                                 MBMEMBMASTER.RETRY_STATUS,   
                                 MBMEMBMASTER.MEMBER_REF,   
                                 '' as age,   
                                 '' as memb_age,   
                                 '' as retry_age,   
                                 '' as work_age,   
                                 MBMEMBMASTER.MEMBER_TYPE,  
                                 MBMEMBMASTER.NATIONALITY,
                                 MBMEMBMASTER.MATECONFIRMLOAN_DATE
                            FROM    
                                 MBUCFMEMBGROUP,   
                                 SHSHAREMASTER,   
                                 SHSHARETYPE,   
                               
                          
                                 MBMEMBMASTER left join CMUCFBANKBRANCH on mbmembmaster.expense_bank = cmucfbankbranch.bank_code
                                   and mbmembmaster.expense_branch = cmucfbankbranch.branch_id

                                   WHERE 
                                 ( MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE ) and  
                                 ( MBMEMBMASTER.MEMBER_NO = SHSHAREMASTER.MEMBER_NO ) and  
                                 ( SHSHARETYPE.SHARETYPE_CODE = SHSHAREMASTER.SHARETYPE_CODE ) and  
                                 ( MBMEMBMASTER.COOP_ID = SHSHAREMASTER.COOP_ID ) and  
                                 ( MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP.COOP_ID ) and  
                                 ( SHSHAREMASTER.COOP_ID = SHSHARETYPE.COOP_ID ) and  
                                 ( ( MBMEMBMASTER.COOP_ID ={0} ) AND  
                                 ( MBMEMBMASTER.MEMBER_NO = {1}) )";
   

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            this.DdMemType();
            this.DdPrename();
        }

        public void DdMemType()
        {
            string sql = @"
             SELECT MEMBTYPE_CODE,MEMBTYPE_DESC  ,1 as sorter
                    FROM MBUCFMEMBTYPE
                    union
                    select '','',0  order by sorter,MEMBTYPE_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "MEMBTYPE_CODE", "MEMBTYPE_DESC", "MEMBTYPE_CODE");
        }
        public void DdPrename()
        {
            string sql = @"
              SELECT PRENAME_CODE,   
                     PRENAME_DESC,   
                     SEX,   
                     PRENAME_SHORT  ,1 as sorter
                FROM MBUCFPRENAME  
                union
                select '','','','',0 order by sorter,PRENAME_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "PRENAME_CODE", "PRENAME_DESC", "PRENAME_CODE");
        }
        public void DdTambol(string district_code)
        {
            string sql = @"
              SELECT MBUCFTAMBOL.TAMBOL_CODE,   
                     MBUCFTAMBOL.TAMBOL_DESC,   
                     MBUCFTAMBOL.DISTRICT_CODE  ,1 as sorter
                FROM MBUCFTAMBOL,   
                     MBUCFDISTRICT  
               WHERE ( MBUCFTAMBOL.DISTRICT_CODE = MBUCFDISTRICT.DISTRICT_CODE )  and
                     (MBUCFTAMBOL.DISTRICT_CODE={0})
             union
            select '','','',0  order by sorter,TAMBOL_DESC ASC ";
            sql = WebUtil.SQLFormat(sql, district_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "TAMBOL_CODE", "TAMBOL_DESC", "TAMBOL_CODE");
        }
        public void DdDistrict(string province_code)
        {
            string sql = @"
              SELECT DISTRICT_CODE,   
                     PROVINCE_CODE,   
                     DISTRICT_DESC,   
                     POSTCODE  ,1 as sorter
                FROM MBUCFDISTRICT 
              where (PROVINCE_CODE={0}) 
            union
            select '','','','',0  ORDER BY sorter,DISTRICT_DESC ASC,   
                     DISTRICT_CODE ASC  ";
            sql = WebUtil.SQLFormat(sql, province_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "AMPHUR_CODE", "DISTRICT_DESC", "DISTRICT_CODE");
        }
        public void DdProvince()
        {
            string sql = @"
              SELECT PROVINCE_CODE,   
                     PROVINCE_DESC  ,1 as sorter
                FROM MBUCFPROVINCE  
            union 
            select '','',0  order by sorter,PROVINCE_DESC ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "PROVINCE_CODE", "PROVINCE_DESC", "PROVINCE_CODE");
        }
        public void DdCurrTambol(string district_code)
        {
            string sql = @"
              SELECT MBUCFTAMBOL.TAMBOL_CODE,   
                     MBUCFTAMBOL.TAMBOL_DESC,   
                     MBUCFTAMBOL.DISTRICT_CODE  ,1 as sorter
                FROM MBUCFTAMBOL,   
                     MBUCFDISTRICT  
               WHERE ( MBUCFTAMBOL.DISTRICT_CODE = MBUCFDISTRICT.DISTRICT_CODE )  and
                     (MBUCFTAMBOL.DISTRICT_CODE={0})
             union
            select '','','',0 order by sorter,TAMBOL_DESC ASC ";
            sql = WebUtil.SQLFormat(sql, district_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "CURRTAMBOL_CODE", "TAMBOL_DESC", "TAMBOL_CODE");
        }
        public void DdCurrDistrict(string province_code)
        {
            string sql = @"
              SELECT DISTRICT_CODE,   
                     PROVINCE_CODE,   
                     DISTRICT_DESC,   
                     POSTCODE  ,1 as sorter
                FROM MBUCFDISTRICT 
              where (PROVINCE_CODE={0}) 
            union
            select '','','','',0  ORDER BY sorter,DISTRICT_DESC ASC,   
                     DISTRICT_CODE ASC  ";
            sql = WebUtil.SQLFormat(sql, province_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "CURRAMPHUR_CODE", "DISTRICT_DESC", "DISTRICT_CODE");
        }
        public void DdCurrProvince()
        {
            string sql = @"
              SELECT PROVINCE_CODE,   
                     PROVINCE_DESC  ,1 as sorter
                FROM MBUCFPROVINCE  
            union 
            select '','',0  order by sorter,PROVINCE_DESC ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "CURRPROVINCE_CODE", "PROVINCE_DESC", "PROVINCE_CODE");
        }
        public void DdBank()
        {
            string sql = @"
              SELECT rtrim(ltrim(BANK_CODE)) as BANK_CODE ,   
                     BANK_DESC,   
                     EDIT_FORMAT ,1 as sorter 
                FROM CMUCFBANK 
            union
            select '','','',0  order by sorter,BANK_DESC ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_name", "BANK_DESC", "BANK_CODE");
        }
        public void DdBranch(string bank_code)
        {
            string sql = @"
                  SELECT BANK_CODE,   
                         rtrim(ltrim(BRANCH_ID)) as BRANCH_ID ,   
                         BRANCH_NAME,   
                         1 as sorter
                    FROM CMUCFBANKBRANCH
                   where rtrim(ltrim(bank_code)) = {0}
                    union 
                    select '','','',0  order by sorter,  BRANCH_NAME ASC";
            sql = WebUtil.SQLFormat(sql, bank_code.Trim());
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "branch_name", "BRANCH_NAME", "BRANCH_ID");
        }
//        public void DdPosition(string position_code)
//        {
//            string sql = @"
//              SELECT POSITION_CODE,   
//                     POSITION_CODE +' - '+ POSITION_DESC as POSITION_DESC ,1 as sorter
//                FROM MBUCFPOSITION 
//                where POSITION_CODE = {0}
//            union
//            select '','',99  ORDER BY sorter,POSITION_CODE ASC  ";
//            sql = WebUtil.SQLFormat(sql, position_code.Trim());
//            DataTable dt = WebUtil.Query(sql);
//            this.DropDownDataBind(dt, "POSITION_CODE", "POSITION_DESC", "POSITION_CODE");
//        }
    }
}
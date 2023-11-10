using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;


namespace Saving.Applications.mbshr.ws_mbshr_req_mbnew_2_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBREQAPPLDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBREQAPPL;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_linkaddress");
            this.Button.Add("b_search");
            this.Button.Add("b_membsearch");
            this.Button.Add("b_bank");
            this.Button.Add("b_branch");
            this.Button.Add("b_linkaddress2");
            this.Register();
        }
        public void RetrieveMain(string docno)
        {
            String sql = @"select * 
            from mbreqappl
            where coop_id = {0} 
            and appl_docno = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, docno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void DdAppltypeCode()
        {
            string sql = @"
            select appltype_code,
                'ค่าธรรมเนียมแรกเข้า' || appltype_desc || ' ' || first_fee || ' บาท' as appltype_desc 
            from mbucfappltype
            where coop_id = {0}
            order by appltype_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "appltype_code", "appltype_desc", "appltype_code");
        }

        public void DdMemType()
        {
            string sql = @"
             SELECT MEMBTYPE_CODE,MEMBTYPE_DESC
                    FROM MBUCFMEMBTYPE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "MEMBTYPE_CODE", "MEMBTYPE_DESC", "MEMBTYPE_CODE");
        }

        public void DdMembgroupCode()
        {
            string sql = @"
            select membgroup_code,
                membgroup_desc
                , 1 as sorter
            from mbucfmembgroup
            where coop_id = {0}
            union
            select '','',0 from dual order by sorter , membgroup_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "membgroup", "membgroup_desc", "membgroup_code");
        }

        public void DdMembgroupCodeFind(string membgroupcode)
        {
            string sql = @"
            select membgroup_code,
                membgroup_desc
                , 1 as sorter
            from mbucfmembgroup
            where coop_id = {0}
            and membgroup_code like {1} 
            order by membgroup_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, membgroupcode);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "membgroup", "membgroup_desc", "membgroup_code");
        }

        public void DdPrename()
        {
            string sql = @"
            SELECT PRENAME_CODE,   
                PRENAME_DESC
            FROM MBUCFPRENAME
            order by PRENAME_CODE";
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
            select '','','',0 from dual order by sorter,TAMBOL_DESC ASC ";
            sql = WebUtil.SQLFormat(sql, district_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "TAMBOL_CODE", "TAMBOL_DESC", "TAMBOL_CODE");
        }

        public void DdCurrTambol(string currdistrict_code)
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
            select '','','',0 from dual order by sorter,TAMBOL_DESC ASC ";
            sql = WebUtil.SQLFormat(sql, currdistrict_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "CURRTAMBOL_CODE", "TAMBOL_DESC", "TAMBOL_CODE");
        }

        public void DdMateTambol(string matedistrict_code)
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
            select '','','',0 from dual order by sorter,TAMBOL_DESC ASC ";
            sql = WebUtil.SQLFormat(sql, matedistrict_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "MATETAMBOL_CODE", "TAMBOL_DESC", "TAMBOL_CODE");
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
            select '','','','',0 from dual ORDER BY sorter,DISTRICT_DESC ASC,   
                     DISTRICT_CODE ASC  ";
            sql = WebUtil.SQLFormat(sql, province_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "DISTRICT_CODE", "DISTRICT_DESC", "DISTRICT_CODE");
        }

        public void DdCurrDistrict(string currprovince_code)
        {
            string sql = @"
              SELECT DISTRICT_CODE,   
                     PROVINCE_CODE,   
                     DISTRICT_DESC,   
                     POSTCODE  ,1 as sorter
                FROM MBUCFDISTRICT 
              where (PROVINCE_CODE={0}) 
            union
            select '','','','',0 from dual ORDER BY sorter,DISTRICT_DESC ASC,   
                     DISTRICT_CODE ASC  ";
            sql = WebUtil.SQLFormat(sql, currprovince_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "CURRAMPHUR_CODE", "DISTRICT_DESC", "DISTRICT_CODE");
        }

        public void DdMateDistrict(string mateprovince_code)
        {
            string sql = @"
              SELECT DISTRICT_CODE,   
                     PROVINCE_CODE,   
                     DISTRICT_DESC,   
                     POSTCODE  ,1 as sorter
                FROM MBUCFDISTRICT 
              where (PROVINCE_CODE={0}) 
            union
            select '','','','',0 from dual ORDER BY sorter,DISTRICT_DESC ASC,   
                     DISTRICT_CODE ASC  ";
            sql = WebUtil.SQLFormat(sql, mateprovince_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "MATEAMPHUR_CODE", "DISTRICT_DESC", "DISTRICT_CODE");
        }

        public void DdProvince()
        {
            string sql = @"
              SELECT PROVINCE_CODE,   
                     PROVINCE_DESC  ,1 as sorter
                FROM MBUCFPROVINCE  
            union 
            select '','',0 from dual order by sorter,PROVINCE_DESC ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "PROVINCE_CODE", "PROVINCE_DESC", "PROVINCE_CODE");
            this.DropDownDataBind(dt, "currprovince_code", "PROVINCE_DESC", "PROVINCE_CODE");
            this.DropDownDataBind(dt, "mateprovince_code", "PROVINCE_DESC", "PROVINCE_CODE");
        }
        public void DdBank()
        {
            string sql = @"
              SELECT trim(BANK_CODE) as BANK_CODE,   
                     BANK_DESC,   
                     EDIT_FORMAT ,1 as sorter 
                FROM CMUCFBANK 
            union
            select '','','',0 from dual order by sorter,BANK_DESC ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_desc", "BANK_DESC", "BANK_CODE");
        }
        public void DdBranch(string bank_code)
        {
            string sql = @"
                  SELECT BANK_CODE,   
                         trim(BRANCH_ID) as BRANCH_ID ,   
                         BRANCH_NAME,   
                         1 as sorter
                    FROM CMUCFBANKBRANCH
                   where trim(bank_code) ={0}
                    union 
                    select '','','',0 from dual order by sorter,  BRANCH_NAME ASC";
            sql = WebUtil.SQLFormat(sql, bank_code.Trim());
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "branch_name", "BRANCH_NAME", "BRANCH_ID");

            //Sdt dtt = WebUtil.QuerySdt(sql);
            //if (dtt.Next())
            //{
            //    this.DATA[0].branch_name2 = dtt.GetString("BRANCH_NAME");
            //}

        }
    }
}
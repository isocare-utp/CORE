using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin.ws_am_coopmaster_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.CMCOOPCONSTANTDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMCOOPCONSTANT;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void Retrieve()
        {
            String sql = @"select CMCOOPCONSTANT.DISTRICT_CODE
                            , CMCOOPCONSTANT.PROVINCE_CODE
                            , CMCOOPCONSTANT.PROVINCE_DESC
                            , CMCOOPCONSTANT.DISTRICT_DESC
                            , COOP_NO
                            , COOP_NAME
                            , COOP_ADDR
                            , TAMBOL
                            , CMCOOPCONSTANT.POSTCODE
                            , COOP_TEL
                            , COOP_FAX
                            , CHAIRMAN
                            , MANAGER
                            , VICEMANAGER
                            , ASSISTANT
                            , OFFICE_ACCOUNT
                            , OFFICE_FINANCE
                            , AUDITOR
                            , AUDITOR_ADDR
                            , RETRY_AGE
                            , RETRY_MONTH 
                            from MBUCFPROVINCE,MBUCFDISTRICT,CMCOOPCONSTANT
                             where CMCOOPCONSTANT.PROVINCE_CODE = MBUCFPROVINCE.PROVINCE_CODE
                               and MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE
                               and CMCOOPCONSTANT.DISTRICT_CODE = mbucfdistrict.DISTRICT_CODE";
                            
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
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
        }

        //public void DDRdSatangTabCode()
        //{
        //    string sql = "SELECT LNCFROUNDINGFACTOR.FACTOR_CODE, LNCFROUNDINGFACTOR.FACTOR_NAME  FROM LNCFROUNDINGFACTOR ";
        //    DataTable dt = WebUtil.Query(sql);
        //    this.DropDownDataBind(dt, "rdintsatang_tabcode", "FACTOR_NAME", "FACTOR_CODE");
        //}

       
    }
}
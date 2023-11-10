using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr_const.ws_mb_mbucfmemgrp_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBUCFMEMBGROUPDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFMEMBGROUP;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            // this.Button.Add("b_search");
            this.Register();
        }

        public void RetrieveMembGroup(string membgroup_code)
        {
            string sql = @"
                         select *
                           from mbucfmembgroup
                          where (coop_id={0}) and (membgroup_code ={1})";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, membgroup_code);
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
            this.DropDownDataBind(dt, "addr_amphur", "DISTRICT_DESC", "DISTRICT_CODE");
        }
        public void DdTambol(string district_code)
        {
            string sql = @"
              SELECT tambol_code,   
                     district_code,   
                     tambol_desc,1 as sorter
               FROM MBUCFTAMBOL 
              where (district_code={0}) 
             
            union 
            select '','','',0 from dual order by sorter,tambol_code ASC";
            sql = WebUtil.SQLFormat(sql, district_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "addr_tambol", "tambol_desc", "tambol_code");
        }
        public void DdProvince()
        {
            string sql = @"
              SELECT PROVINCE_CODE,   
                     PROVINCE_DESC  ,1 as sorter
                FROM MBUCFPROVINCE 
            union 
            select '','',0 from dual order by sorter,PROVINCE_DESC ASC";
            //sql = WebUtil.SQLFormat(sql, province_code);
            //DataTable dt = WebUtil.Query(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "addr_province", "PROVINCE_DESC", "PROVINCE_CODE");
        }
        
        public void DdGroupControl()
        {
            string sql = @"
              select membgroup_control ,1 as sorter
                from mbucfmembgroup
                union 
                select '',0 from dual order by sorter,membgroup_control asc";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "membgroup_control", "membgroup_control", "membgroup_control");
        }
    }
}
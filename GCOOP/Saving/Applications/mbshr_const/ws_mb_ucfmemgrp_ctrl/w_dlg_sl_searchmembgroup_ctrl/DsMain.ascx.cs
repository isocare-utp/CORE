using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr_const.ws_mb_ucfmemgrp_ctrl.w_dlg_sl_searchmembgroup_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBUCFMEMBGROUPDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFMEMBGROUP;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_save");
            this.Register();
        }
        
        public void RetrieveMembGroup(string membgroup_code)
        {
            string sql = @"select coop_id,
                membgroup_code,
                membgroup_control,
                membgroup_desc,
                membgroup_agentgrg,
                addr_no,
                addr_moo,
                addr_soi,
                addr_road,
                addr_tambol,
                addr_amphur,
                trim(addr_province) as addr_province,
                addr_phone,
                addr_fax,
                addr_postcode,
                membgrptype_code
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

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "addr_province", "PROVINCE_DESC", "PROVINCE_CODE");
        }

        public void DdGroupControl()
        {
            string sql = @"
            select membgroup_code,
                membgroup_code || ' - ' || membgroup_desc as membgroup_desc, 1 as sorter
            from mbucfmembgroup
            where coop_id = {0}
            union 
            select '','',0 from dual order by sorter,membgroup_code asc";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "membgroup_control", "membgroup_desc", "membgroup_code");
        }

        public void DdMembtype()
        {
            string sql = @"
            SELECT membgrptype_code,   
                membgrptype_code || ' - ' || membgrptype_desc as display,   
                1 as sorter
            FROM mbucfmembgrptype 
               union
            select '','',0 from dual ORDER BY sorter,membgrptype_code ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "membgrptype_code", "display", "membgrptype_code");
        }
    }
}
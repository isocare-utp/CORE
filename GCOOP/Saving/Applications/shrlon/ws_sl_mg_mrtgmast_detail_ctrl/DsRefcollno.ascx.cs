using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl
{
    public partial class DsRefcollno : DataSourceRepeater
    {
        public DataSet1.DT_REFCOLLNODataTable DATA { get; set; }

        public void InitDsRefcollno(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_REFCOLLNO;
            this.EventItemChanged = "OnDsRefcollnoItemChanged";
            this.EventClicked = "OnDsRefcollnoClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsRefcollno");
            this.Register();
        }

        public void Retrieve(string as_mrtgno)
        {
            string ls_sql = @"select lnmrtgreflncollmast.coop_id,
                lnmrtgreflncollmast.mrtgmast_no,
                lnmrtgreflncollmast.collmast_no,
                 CASE 
                                    WHEN lncollmaster.collmasttype_grp = '01'
                                    THEN 'ตั้งอยู่ที่ตำบล '+ rtrim(ltrim(lncollmaster.pos_tambol))+' อำเภอ '+ rtrim(ltrim(lncollmaster.pos_amphur)) +' จังหวัด '+ rtrim(ltrim(lncollmaster.pos_province)) + ' จำนวน '+ rtrim(ltrim(lncollmaster.size_rai)) +' ไร่ '+ rtrim(ltrim(lncollmaster.size_ngan)) + ' งาน '+ rtrim(ltrim(lncollmaster.size_wa))+' วา '
                                    WHEN lncollmaster.collmasttype_grp = '02'
                                    THEN 'หมู่บ้าน '+ rtrim(ltrim(lncollmaster.bd_village)) +' บ้านเลขที่ '+ rtrim(ltrim(lncollmaster.bd_addrno)) +' หมู่ที่ '+rtrim(ltrim(lncollmaster.bd_addrmoo))+' ซอย '+ rtrim(ltrim(lncollmaster.bd_soi))+' ถนน '+rtrim(ltrim(lncollmaster.bd_road)) +' ตำบล '+ rtrim(ltrim(lncollmaster.bd_tambol)) + ' อำเภอ '+ rtrim(ltrim(lncollmaster.bd_amphur)) +' จังหวัด '+ rtrim(ltrim(lncollmaster.bd_province))
                                    WHEN lncollmaster.collmasttype_grp = '03'
                                    THEN 'ชื่อคอนโด '+ rtrim(ltrim(lncollmaster.condo_name)) + ' ตึกที่ '+ rtrim(ltrim(lncollmaster.condo_towerno)) +' ชั้นที่ '+ rtrim(ltrim(lncollmaster.condo_floor))+' ห้องที่ '+ rtrim(ltrim(lncollmaster.condo_roomno)) +' ขนาดห้อง '+ rtrim(ltrim(lncollmaster.condo_roomsize))
                                else 'ไม่มีข้อมูล'  
                                END AS collmast_desc,
                lncollmaster.est_price as estimate_price
                from lnmrtgreflncollmast , lncollmaster
                where lnmrtgreflncollmast.coop_id = lncollmaster.coop_id
                and lnmrtgreflncollmast.collmast_no = lncollmaster.collmast_no
                and lnmrtgreflncollmast.coop_id = {0}
                and lnmrtgreflncollmast.mrtgmast_no = {1}";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_mrtgno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}
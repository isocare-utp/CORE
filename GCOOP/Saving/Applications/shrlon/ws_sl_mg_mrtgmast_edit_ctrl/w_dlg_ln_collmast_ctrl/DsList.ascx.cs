using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl.w_dlg_ln_collmast_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_ListDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_List;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }

        public void Retrieve(string as_memno)
        {
            string ls_sql = @"select 
                                coop_id,collmast_no,decode(collmasttype_grp,'01','โฉนด','02','สิ่งปลูกสร้าง','03','อาคารชุด') as collmasttype_desc,
                                CASE 
                                    WHEN collmasttype_grp = '01'
                                    THEN 'ตั้งอยู่ที่ตำบล '||trim(pos_tambol)||' อำเภอ '||trim(pos_amphur)||' จังหวัด '||trim(pos_province)||' จำนวน '||trim(size_rai)||' ไร่ '||trim(size_ngan)||' งาน '||trim(size_wa)||' วา '
                                    WHEN collmasttype_grp = '02'
                                    THEN 'หมู่บ้าน '||trim(bd_village)||' บ้านเลขที่ '||trim(bd_addrno)||' หมู่ที่ '||trim(bd_addrmoo)||' ซอย '||trim(bd_soi)||' ถนน '||trim(bd_road)||' ตำบล '||trim(bd_tambol)||' อำเภอ '||trim(bd_amphur)||' จังหวัด '||trim(bd_province)
                                    WHEN collmasttype_grp = '03'
                                    THEN 'ชื่อคอนโด '||trim(condo_name)||' ตึกที่ '||trim(condo_towerno)||' ชั้นที่ '||trim(condo_floor)||' ห้องที่ '||trim(condo_roomno)||' ขนาดห้อง '||trim(condo_roomsize)
                                else 'ไม่มีข้อมูล'  
                                END AS collmast_desc,
                                est_price as estimate_price
                                from lncollmaster
                                where coop_id= {0}
                                and member_no = {1}
                                and redeem_flag = 0";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_memno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}
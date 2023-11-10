using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.mbshr_const.w_sheet_mb_mbucfmembgroup_ctrl
{
    public partial class w_sheet_mb_mbucfmembgroup : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMembGroup { get; set; }
        [JsPostBack]
        public String PostProvince { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMembGroup)
            {
                string membgroup_code = dsMain.DATA[0].MEMBGROUP_CODE;
                dsMain.RetrieveMembGroup(membgroup_code);
                string province_code = dsMain.DATA[0].ADDR_PROVINCE;
                dsMain.DdProvince();
                dsMain.DdDistrict(province_code);

            }
            else if (eventArg == PostProvince)
            {
                string province_code = dsMain.DATA[0].ADDR_PROVINCE;
                dsMain.DdDistrict(province_code);

            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                string membgroup_code = dsMain.DATA[0].MEMBGROUP_CODE;
                string membgroup_desc = dsMain.DATA[0].MEMBGROUP_DESC;
                string membgroup_control = dsMain.DATA[0].MEMBGROUP_CONTROL;
                string membgroup_agentgrg = dsMain.DATA[0].MEMBGROUP_AGENTGRG;
                string addr_no = dsMain.DATA[0].ADDR_NO;
                string addr_moo = dsMain.DATA[0].ADDR_MOO;
                string addr_soi = dsMain.DATA[0].ADDR_SOI;
                string addr_road = dsMain.DATA[0].ADDR_ROAD;
                string addr_tambol = dsMain.DATA[0].ADDR_TAMBOL;
                string addr_amphur = dsMain.DATA[0].ADDR_AMPHUR;
                string addr_province = dsMain.DATA[0].ADDR_PROVINCE;
                string addr_postcode = dsMain.DATA[0].ADDR_POSTCODE;
                string addr_phone = dsMain.DATA[0].ADDR_PHONE;
                string addr_fax = dsMain.DATA[0].ADDR_FAX;
                string savedisk_type = dsMain.DATA[0].SAVEDISK_TYPE;

                string sql_update = "update mbucfmembgroup set membgroup_desc='" + membgroup_desc + "',membgroup_control='" + membgroup_control + "',membgroup_agentgrg='" + membgroup_agentgrg + "',addr_no='" + addr_no + "',addr_moo='" + addr_moo + "',addr_soi='" + addr_soi + "',addr_road='" + addr_road + "',addr_tambol='" + addr_tambol + "',addr_amphur='" + addr_amphur + "',addr_province='" + addr_province + "',addr_postcode='" + addr_postcode + "',addr_phone='" + addr_phone + "',addr_fax='" + addr_fax + "',savedisk_type='" + savedisk_type + "' where coop_id='" + state.SsCoopId + "' and membgroup_code='" + membgroup_code + "'";
                exed1.SQL.Add(sql_update);
                exed1.Execute();

                LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("แก้ไขข้อมูลไม่สำเร็จ");

            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}
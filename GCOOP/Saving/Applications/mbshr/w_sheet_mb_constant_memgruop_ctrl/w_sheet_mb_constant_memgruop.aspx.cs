using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.mbshr.w_sheet_mb_constant_memgruop_ctrl
{
    public partial class w_sheet_mb_constant_memgruop : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String postMemberGroup { get; set; }
        [JsPostBack]
        public String postTambol { get; set; }
        [JsPostBack]
        public String postAmphur { get; set; }
        [JsPostBack]
        public String postProvince { get; set; }
        [JsPostBack]
        public String postGroupCode { get; set; }


        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            dsDetail.InitDs(this);
            dsMainDetail.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == postMemberGroup)
            {
                //dsDetail.RetrieveMemberGroup(dsMain.DATA[0].membgroup_codet);
                dsDetail.RetrieveMemberGroup(HdRow.Value);
                dsDetail.RetrieveProvince();
            }
            else if (eventArg == postTambol)
            {
                String sql = @"
                                SELECT   postcode 
                                FROM     mbucfdistrict
                                WHERE    district_code = '" + dsDetail.DATA[0].ADDR_AMPHUR + @"' 
                                         and province_code = '" + dsDetail.DATA[0].ADDR_PROVINCE + @"'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsDetail.DATA[0].ADDR_POSTCODE = dt.GetString("postcode");
                }

            }
            else if (eventArg == postAmphur)
            {
                dsDetail.RetrieveTambol(dsDetail.DATA[0].ADDR_AMPHUR);
            }
            else if (eventArg == postProvince)
            {
                dsDetail.RetrieveDistrict(dsDetail.DATA[0].ADDR_PROVINCE);
            }
            else if (eventArg == postGroupCode)
            {
                dsMainDetail.RetrieveMemberGroup(dsMain.DATA[0].membgroup_codet);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String coop_id = state.SsCoopControl;
                String membgroup_code = dsDetail.DATA[0].MEMBGROUP_CODE;

                ExecuteDataSource exe = new ExecuteDataSource(this);
                String sql = @" 
                        update  mbucfmembgroup
                        set     membgroup_code = '" + dsDetail.DATA[0].MEMBGROUP_CODE + @"' , 
                                membgroup_desc = '" + dsDetail.DATA[0].MEMBGROUP_DESC + "' , addr_no = '" + dsDetail.DATA[0].ADDR_NO + @"',
                                addr_moo = '" + dsDetail.DATA[0].ADDR_MOO + "', addr_soi = '" + dsDetail.DATA[0].ADDR_SOI + @"' , 
                                addr_road = '" + dsDetail.DATA[0].ADDR_ROAD + "' , addr_tambol='" + dsDetail.DATA[0].ADDR_TAMBOL + @"' , 
                                addr_province = '" + dsDetail.DATA[0].ADDR_PROVINCE + @"' ,
MEMBGROUP_AGENTGRG = '" + dsDetail.DATA[0].MEMBGROUP_AGENTGRG + @"' ,
MEMBGROUP_CONTROL = '" + dsDetail.DATA[0].MEMBGROUP_CONTROL + @"' ,
MANAGER_GROUP = '" + dsDetail.DATA[0].MANAGER_GROUP + @"' ,
DISTRICTGROUP_CODE = '" + dsDetail.DATA[0].DISTRICTGROUP_CODE + @"' ,
FUNDGROUP_CODE = '" + dsDetail.DATA[0].FUNDGROUP_CODE + @"' ,
AREA_CODE = '" + dsDetail.DATA[0].AREA_CODE + @"' ,
MONEY_ETC = " + dsDetail.DATA[0].MONEY_ETC + @" ,
                                addr_postcode = '" + dsDetail.DATA[0].ADDR_POSTCODE + "', addr_phone = '" + dsDetail.DATA[0].ADDR_PHONE + @"' , 
                                addr_fax = '" + dsDetail.DATA[0].ADDR_FAX + @"'
                        where   coop_id = '" + coop_id + @"'
                                and membgroup_code = '" + membgroup_code + "'    ";
                sql = WebUtil.SQLFormat(sql);
                exe.SQL.Add(sql);
                exe.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
        }

        // string sql_value = GetSql_Value(select max(appl_docno) as sql_value from mbreqappl)
        public string GetSql_Value(string Select_Condition)
        {
            string max_value = "";
            string sqlf = Select_Condition;
            Sdt dt = WebUtil.QuerySdt(sqlf);

            if (dt.Next())
            {
                max_value = dt.GetString("sql_value");
            }
            return max_value;
        }
    }
}
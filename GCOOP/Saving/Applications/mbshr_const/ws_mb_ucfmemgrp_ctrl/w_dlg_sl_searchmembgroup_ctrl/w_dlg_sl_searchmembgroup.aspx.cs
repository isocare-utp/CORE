using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr_const.ws_mb_ucfmemgrp_ctrl.w_dlg_sl_searchmembgroup_ctrl
{
    public partial class w_dlg_sl_searchmembgroup : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public String PostProvince { get; set; }
        [JsPostBack]
        public String PostAmphur { get; set; }
        [JsPostBack]
        public String PostSave { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            //dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string ls_membgroup_code = Request["ls_membgroup_code"];

                dsMain.RetrieveMembGroup(ls_membgroup_code);
                string ls_province_code = dsMain.DATA[0].ADDR_PROVINCE;
                string ls_district_code = dsMain.DATA[0].ADDR_AMPHUR;
                dsMain.DdProvince();
                dsMain.DdDistrict(ls_province_code);
                dsMain.DdTambol(ls_district_code);
                dsMain.DdGroupControl();
                dsMain.DdMembtype();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostProvince)
            {
                string ls_province_code = dsMain.DATA[0].ADDR_PROVINCE;
                dsMain.DdDistrict(ls_province_code);

            }
            else if (eventArg == PostAmphur)
            {
                string ls_addr_amphur = dsMain.DATA[0].ADDR_AMPHUR;
                dsMain.DdTambol(ls_addr_amphur);
                PostGetPostcode();
            }
            else if (eventArg == PostSave)
            {
                string ls_membgroup_code2 = Request["ls_membgroup_code"];
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                // decimal li_countgroup = 0;
                string ls_membgroup_code = dsMain.DATA[0].MEMBGROUP_CODE;
                string ls_membgroup_desc = dsMain.DATA[0].MEMBGROUP_DESC;
                string ls_membgroup_control = dsMain.DATA[0].MEMBGROUP_CONTROL;
                string ls_membgroup_agentgrg = dsMain.DATA[0].MEMBGROUP_AGENTGRG;
                string ls_addr_no = dsMain.DATA[0].ADDR_NO;
                string ls_addr_moo = dsMain.DATA[0].ADDR_MOO;
                string ls_addr_soi = dsMain.DATA[0].ADDR_SOI;
                string ls_addr_road = dsMain.DATA[0].ADDR_ROAD;
                string ls_addr_tambol = dsMain.DATA[0].ADDR_TAMBOL;
                string ls_addr_amphur = dsMain.DATA[0].ADDR_AMPHUR;
                string ls_addr_province = dsMain.DATA[0].ADDR_PROVINCE;
                string ls_addr_postcode = dsMain.DATA[0].ADDR_POSTCODE;
                string ls_addr_phone = dsMain.DATA[0].ADDR_PHONE;
                string ls_addr_fax = dsMain.DATA[0].ADDR_FAX;
                string ls_membgrptype_code = dsMain.DATA[0].membgrptype_code;
                if (ls_membgroup_code2 == "")
                {

                    try
                    {
                        string sql_insert = @"INSERT INTO mbucfmembgroup (coop_id,         membgroup_code,      membgroup_control,
                                                                          membgroup_desc,  membgroup_agentgrg,  addr_no,
                                                                          addr_moo,        addr_soi,            addr_road,
                                                                          addr_tambol,     addr_amphur,         addr_province,   
                                                                          addr_postcode,   addr_phone,          addr_fax,
                                                                          membgrptype_code) 
                                                          VALUES ('" + state.SsCoopId + "','" + ls_membgroup_code + "','" + ls_membgroup_control + "','"
                                                                     + ls_membgroup_desc + "','" + ls_membgroup_agentgrg + "','" + ls_addr_no + "','"
                                                                     + ls_addr_moo + "','" + ls_addr_soi + "','" + ls_addr_road + "','"
                                                                     + ls_addr_tambol + "','" + ls_addr_amphur + "','" + ls_addr_province + "','"
                                                                     + ls_addr_postcode + "','" + ls_addr_phone + "','" + ls_addr_fax + "','"
                                                                     + ls_membgrptype_code + "')";
                        exed1.SQL.Add(sql_insert);
                        exed1.Execute();
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                        dsMain.ResetRow();
                        this.SetOnLoadedScript("alert('บันทึกข้อมูลสำเร็จ') \n parent.RefreshSheet(" + ls_membgroup_control + ")");

                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    }
                }
                else
                {
                    string sql_update = "update mbucfmembgroup set membgroup_desc='" + ls_membgroup_desc +
                                                                "',membgroup_control='" + ls_membgroup_control +
                                                                "',membgroup_agentgrg='" + ls_membgroup_agentgrg +
                                                                "',addr_no='" + ls_addr_no +
                                                                "',addr_moo='" + ls_addr_moo +
                                                                "',addr_soi='" + ls_addr_soi +
                                                                "',addr_road='" + ls_addr_road +
                                                                "',addr_tambol='" + ls_addr_tambol +
                                                                "',addr_amphur='" + ls_addr_amphur +
                                                                "',addr_province='" + ls_addr_province +
                                                                "',addr_postcode='" + ls_addr_postcode +
                                                                "',addr_phone='" + ls_addr_phone +
                                                                "',addr_fax='" + ls_addr_fax +
                                                                "',membgrptype_code='" + ls_membgrptype_code +
                                                                "' where coop_id='" + state.SsCoopId +
                                                                "' and membgroup_code='" + ls_membgroup_code + "'";
                    exed1.SQL.Add(sql_update);
                    exed1.Execute();

                    LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขข้อมูลสำเร็จ");
                    dsMain.ResetRow();
                    this.SetOnLoadedScript("alert('แก้ไขข้อมูลสำเร็จ') \n parent.RefreshSheet(" + ls_membgroup_control + ")");
                }
            }
        }

        public void WebDialogLoadEnd()
        {
            string ls_membgroup_code = Request["ls_membgroup_code"];
            if (ls_membgroup_code != "")
            {
                dsMain.FindTextBox(0, dsMain.DATA.MEMBGROUP_CODEColumn).ReadOnly = true;
            }
        }
        public void PostGetPostcode()
        {
            dsMain.DATA[0].ADDR_TAMBOL = "";
            dsMain.DdTambol(dsMain.DATA[0].ADDR_AMPHUR);

            String sql = @"SELECT MBUCFDISTRICT.POSTCODE   
                        FROM MBUCFDISTRICT,  
        	                MBUCFPROVINCE         	                        
                        WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE )   
        	                and ( MBUCFDISTRICT.PROVINCE_CODE = {0} ) 
        	                and ( MBUCFDISTRICT.DISTRICT_CODE = {1} )";
            sql = WebUtil.SQLFormat(sql, dsMain.DATA[0].ADDR_PROVINCE, dsMain.DATA[0].ADDR_AMPHUR);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMain.DATA[0].ADDR_POSTCODE = dt.Rows[0]["postcode"].ToString();
            }
        }
    }
}
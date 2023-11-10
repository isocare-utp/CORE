using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.mbshr.dlg.ws_dlg_sl_shareucftype_ctrl
{
    public partial class ws_dlg_sl_shareucftype : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostSave { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            dsMain.Ddshrtype();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostSave")
            {
                string sqlStr, ls_shrcode, ls_shrcode_copy, ls_shrdesc;
                
                try
                {
                    //for chk
                    ls_shrcode = dsMain.DATA[0].SHARETYPE_CODE.ToString();
                    ls_shrdesc = dsMain.DATA[0].SHARETYPE_DESC.ToString();
                    if (ls_shrcode == "")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณากรอกรหัสประเภทหุ้น');", true); return;
                    }
                    else if (ls_shrdesc == "")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณากรอกชื่อประเภทหุ้น');", true); return;
                    }
                    //chk ซ้ำ
                    sqlStr = @"select sharetype_code from shsharetype where sharetype_code={0}";
                    sqlStr = WebUtil.SQLFormat(sqlStr, ls_shrcode);
                    Sdt dt = WebUtil.QuerySdt(sqlStr);                    
                    if (dt.Next())
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('รหัสประเภทประเภทหุ้นซ้ำกัน กรุณาตรวจสอบ');", true); return;
                    }
                    else
                    {                                                
                        if (dsMain.DATA[0].checkbox != "1")
                        {
                            sqlStr = @"insert into shsharetype 
                            (sharetype_code, sharetype_desc, coop_id)
                            values
                            ({0}, {1}, {2})";
                            sqlStr = WebUtil.SQLFormat(sqlStr, ls_shrcode, ls_shrdesc, state.SsCoopId);
                            WebUtil.ExeSQL(sqlStr);
                        }else{
                            ls_shrcode_copy = dsMain.DATA[0].usepattern_shrcode;
                            if (ls_shrcode_copy == "") {
                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณาเลือกข้อกำหนดจากประเภทหุ้นที่ต้องการคัดลอก');", true); return;
                            }
                            else
                            {
                                sqlStr = @"  INSERT INTO SHSHARETYPE  
                             ( COOP_ID,   
                               SHARETYPE_CODE,   
                               SHARETYPE_DESC,   
                               UNITSHARE_VALUE,   
                               MINSHARE_HOLD,   
                               MAXSHARE_HOLD,   
                               MINSHARE_LOW,   
                               MINSHARE_STOP,   
                               TIMEMINSHARE_LOW,   
                               TIMEMINSHARE_STOP,   
                               COUNTPERIOD_STATUS,   
                               BUYSHARE_BEFORE,   
                               MAXMISS_PAY,   
                               CHGCOUNT_TYPE,   
                               CHGCOUNTALL_AMT,   
                               CHGCOUNTADD_AMT,   
                               CHGCOUNTLOW_AMT,   
                               CHGCOUNTSTOP_AMT,   
                               CHGCOUNTCONT_AMT,   
                               LIMITBUY_PERYEAR_NMLMBR,   
                               LIMITBUY_ITEMTYPE_NMLMBR,   
                               LIMITBUY_PERYEAR_COMBR,   
                               LIMITBUY_ITEMTYPE_COMBR,   
                               CALSHAREBASE_TYPE,   
                               DIVIDEND_STATUS,   
                               CALLONPRMS_STATUS,   
                               SHAREGROUP_CODE,   
                               MINSHARE_PAY,   
                               DIVIDEND_RATE,   
                               ALLOWANCE_RATE,   
                               ALLOWANCE_AMT,   
                               ALLOWANCE_COMPARE,   
                               ALLOW_LOWSHPERYEAR,   
                               ACCOUNT_ID,   
                               MONEYSUP_TSL,   
                               MONEYSUP_SWD,   
                               MAXSHARE_PAY,   
                               MAXTIMEBUYSHR_AMT,   
                               ROUNND_SHARETYPE,   
                               ROUND_SHARETYPE,   
                               MAXBUYSHRPXPERMTH_AMT,   
                               MAXBUYSHRPER_YEAR,   
                               MINSHRPERIOD_TYPE,   
                               MINSHAREPERIOD_PERCENT,   
                               MINTIMEMBSENSHR_AMT,   
                               MINTIMESTOP_SHARE,   
                               MAXSTOPSHR_PERIOD,   
                               CHKNOTLOAN_FLAG,   
                               LOAN90SHR_FLAG,   
                               MINALLOWSHRBASE_FLAG,   
                               MINALLOWSHRLASTMTH_FLAG,   
                               MININCSHRLASTMTH_FLAG,   
                               STOPSHAREFOREVER_FLAG,   
                               SHRWTDPARTIAL_MAXAMT,   
                               SHRWTDPARTIAL_PERCENT,   
                               SHRWTDPARTIAL_TYPE,   
                               MAXMISSPER_YEAR,   
                               LOANDIVSTART_DATE,   
                               LOANDIVEND_DATE,   
                               SHARE_VALUE,   
                               ADJSALARYCHGSHRPERIOD_FLAG )  
                         SELECT SHSHARETYPE.COOP_ID,   
                                {0},   
                                {1},   
                                SHSHARETYPE.UNITSHARE_VALUE,   
                                SHSHARETYPE.MINSHARE_HOLD,   
                                SHSHARETYPE.MAXSHARE_HOLD,   
                                SHSHARETYPE.MINSHARE_LOW,   
                                SHSHARETYPE.MINSHARE_STOP,   
                                SHSHARETYPE.TIMEMINSHARE_LOW,   
                                SHSHARETYPE.TIMEMINSHARE_STOP,   
                                SHSHARETYPE.COUNTPERIOD_STATUS,   
                                SHSHARETYPE.BUYSHARE_BEFORE,   
                                SHSHARETYPE.MAXMISS_PAY,   
                                SHSHARETYPE.CHGCOUNT_TYPE,   
                                SHSHARETYPE.CHGCOUNTALL_AMT,   
                                SHSHARETYPE.CHGCOUNTADD_AMT,   
                                SHSHARETYPE.CHGCOUNTLOW_AMT,   
                                SHSHARETYPE.CHGCOUNTSTOP_AMT,   
                                SHSHARETYPE.CHGCOUNTCONT_AMT,   
                                SHSHARETYPE.LIMITBUY_PERYEAR_NMLMBR,   
                                SHSHARETYPE.LIMITBUY_ITEMTYPE_NMLMBR,   
                                SHSHARETYPE.LIMITBUY_PERYEAR_COMBR,   
                                SHSHARETYPE.LIMITBUY_ITEMTYPE_COMBR,   
                                SHSHARETYPE.CALSHAREBASE_TYPE,   
                                SHSHARETYPE.DIVIDEND_STATUS,   
                                SHSHARETYPE.CALLONPRMS_STATUS,   
                                SHSHARETYPE.SHAREGROUP_CODE,   
                                SHSHARETYPE.MINSHARE_PAY,   
                                SHSHARETYPE.DIVIDEND_RATE,   
                                SHSHARETYPE.ALLOWANCE_RATE,   
                                SHSHARETYPE.ALLOWANCE_AMT,   
                                SHSHARETYPE.ALLOWANCE_COMPARE,   
                                SHSHARETYPE.ALLOW_LOWSHPERYEAR,   
                                SHSHARETYPE.ACCOUNT_ID,   
                                SHSHARETYPE.MONEYSUP_TSL,   
                                SHSHARETYPE.MONEYSUP_SWD,   
                                SHSHARETYPE.MAXSHARE_PAY,   
                                SHSHARETYPE.MAXTIMEBUYSHR_AMT,   
                                SHSHARETYPE.ROUNND_SHARETYPE,   
                                SHSHARETYPE.ROUND_SHARETYPE,   
                                SHSHARETYPE.MAXBUYSHRPXPERMTH_AMT,   
                                SHSHARETYPE.MAXBUYSHRPER_YEAR,   
                                SHSHARETYPE.MINSHRPERIOD_TYPE,   
                                SHSHARETYPE.MINSHAREPERIOD_PERCENT,   
                                SHSHARETYPE.MINTIMEMBSENSHR_AMT,   
                                SHSHARETYPE.MINTIMESTOP_SHARE,   
                                SHSHARETYPE.MAXSTOPSHR_PERIOD,   
                                SHSHARETYPE.CHKNOTLOAN_FLAG,   
                                SHSHARETYPE.LOAN90SHR_FLAG,   
                                SHSHARETYPE.MINALLOWSHRBASE_FLAG,   
                                SHSHARETYPE.MINALLOWSHRLASTMTH_FLAG,   
                                SHSHARETYPE.MININCSHRLASTMTH_FLAG,   
                                SHSHARETYPE.STOPSHAREFOREVER_FLAG,   
                                SHSHARETYPE.SHRWTDPARTIAL_MAXAMT,   
                                SHSHARETYPE.SHRWTDPARTIAL_PERCENT,   
                                SHSHARETYPE.SHRWTDPARTIAL_TYPE,   
                                SHSHARETYPE.MAXMISSPER_YEAR,   
                                SHSHARETYPE.LOANDIVSTART_DATE,   
                                SHSHARETYPE.LOANDIVEND_DATE,   
                                SHSHARETYPE.SHARE_VALUE,   
                                SHSHARETYPE.ADJSALARYCHGSHRPERIOD_FLAG  
                           FROM SHSHARETYPE where  SHARETYPE_CODE ={2}  ";
                                sqlStr = WebUtil.SQLFormat(sqlStr, ls_shrcode, ls_shrdesc, ls_shrcode_copy);
                                WebUtil.ExeSQL(sqlStr);
                            }
                        }                                          
                        //dsMain.DATA[0].SHARETYPE_CODE="";
                        //dsMain.DATA[0].SHARETYPE_DESC="";
                        //dsMain.DATA[0].checkbox = "0";
                        //dsMain.DATA[0].usepattern_shrcode="";
                        //LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");                        
                        this.SetOnLoadedScript("parent.GetValueFromDlg(" + dsMain.DATA[0].SHARETYPE_CODE + ");");
                    }                                    
                }
                catch
                {                    
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ");
                }
            }
        }

        public void WebDialogLoadEnd()
        {
           
        }
    }
}
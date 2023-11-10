using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfShrlon;

using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using DataLibrary;


namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mb_add_newgroup : PageWebSheet, WebSheet
    {
        private DwThDate tdwmain;
        //private ShrlonClient shrlonService;
        //private CommonClient commonService;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsRefresh;
        protected String jsPostGroup;
        protected String newClear;
        protected String jsmembgroup_code;
        protected String jsCoopSelect;
        protected String jsChangmidgroupcontrol;
        protected String jsRetreivemidgroup;
        protected String jsRetreivemidgroup2;
        protected String jsConfirmDelete;

        void WebSheet.InitJsPostBack()
        {
            jsRefresh = WebUtil.JsPostBack(this, "jsRefresh");
            newClear = WebUtil.JsPostBack(this, "newClear");
            jsChangmidgroupcontrol = WebUtil.JsPostBack(this, "jsChangmidgroupcontrol");
            jsmembgroup_code = WebUtil.JsPostBack(this, "jsmembgroup_code");
            jsCoopSelect = WebUtil.JsPostBack(this, "jsCoopSelect");
            jsRetreivemidgroup = WebUtil.JsPostBack(this, "jsRetreivemidgroup");
            jsRetreivemidgroup2 = WebUtil.JsPostBack(this, "jsRetreivemidgroup2");
            jsConfirmDelete = WebUtil.JsPostBack(this, "jsConfirmDelete");
            tdwmain = new DwThDate(dw_main, this);

        }

        void WebSheet.WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            dw_main.SetTransaction(sqlca);
            dw_detail.SetTransaction(sqlca);
            try
            {
                //shrlonService = wcf.NShrlon;
                //commonService = wcf.NCommon;
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();

            if (IsPostBack)
            {
                try
                {
                    //this.RestoreContextDw(dw_coop);
                    this.RestoreContextDw(dw_main);
                    this.RestoreContextDw(dw_detail);

                    HdIsPostBack.Value = "true";
                    
                }
                catch { }
            }
            else
            {
                dw_main.InsertRow(0);
                dw_detail.InsertRow(0);
                dw_detail.DataWindowObject = "d_mb_addgroup_detail_level1";
                DwUtil.RetrieveDataWindow(dw_detail, "mb_add_newgroup.pbl", null, state.SsCoopControl);

                //dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
                //dw_main.SetItemString(1, "entry_id", state.SsUsername);
                //DwUtil.RetrieveDDDW(dw_coop, "memcoop_select", "mbshr_common.pbl", state.SsCoopControl);
                //RetrieveDDDW(); tdwmain.Eng2ThaiAllRow();
                HdIsPostBack.Value = "False";
                HdCheckRow.Value = "False";

            }
        }



        void WebSheet.CheckJsPostBack(String eventArg)
        {
            if (eventArg == "jsRefresh")
            {
                JsRefresh();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();

            }
            else if (eventArg == "jsmembgroup_code")
            {
                Jsmembgroup_code();
            }

            else if (eventArg == "jsChangmidgroupcontrol")
            {
                JsChangmidgroupcontrol();
            }
            else if (eventArg == "jsRetreivemidgroup")
            {
                JsRetreivemidgroup();
            }
            else if (eventArg == "jsRetreivemidgroup2")
            {
                JsRetreivemidgroup2();
            }
            else if (eventArg == "jsConfirmDelete")
            {
                JsConfirmDelete();
            }

        }

        private void JsConfirmDelete()
        {
            try
            {
                if (state.IsWritable)
                {
                    int RowDelete = int.Parse(HdRowDelete.Value);
                    Sta ta = new Sta(state.SsConnectionString);
                    bool isSaved = true;
                    String membgroupcode = "";
                    try
                    {
                        try
                        {
                            membgroupcode = DwUtil.GetString(dw_detail, RowDelete, "membgroup_code", "");
                        }
                        catch { }
                        string sql = "delete from  mbucfmembgroup where  membgroup_code ='"+membgroupcode+"'";
                        int ii = ta.Exe(sql);
                    }
                    catch (Exception ex)
                    {
                        isSaved = false;
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    }
                    ta.Close();
                    if (isSaved)
                    {
                        dw_detail.DeleteRow(RowDelete);
                        LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
                    }
                }
               
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("คุณไม่มีสิทธิ์แก้ไขข้อมูลหน้าจอนี้");
                }
            }
            catch { }
        }
      
        
        private void JsNewClear()
        {
            try
            {
                dw_main.Reset();
                dw_detail.Reset();

                dw_main.InsertRow(0);
                dw_detail.InsertRow(0);

                // dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
                ////H-Code  เรื่อง Session
                //DateTime entry_date = Convert.ToDateTime(Session["entry_date"].ToString());
                //dw_main.SetItemDate(1, "entry_date", entry_date);
                //DwUtil.RetrieveDDDW(dw_coop, "memcoop_select", "mbshr_common.pbl", state.SsCoopControl);
                //dw_main.SetItemString(1, "entry_id", state.SsUsername);
                //tdwmain.Eng2ThaiAllRow();
                HdIsPostBack.Value = "false";
            }
            catch (Exception ex)
            {
            }
        }

        private void JsRetreivemidgroup2()
        {
            dw_detail.DataWindowObject = "d_mb_addgroup_detail";
            String membgroup_code1 = dw_main.GetItemString(1, "membgroup_2");

            DwUtil.RetrieveDataWindow(dw_detail, "mb_add_newgroup.pbl", null, state.SsCoopControl, membgroup_code1);

        }


        private void JsRetreivemidgroup()
        {
            decimal add_type = dw_main.GetItemDecimal(1, "add_type");
            if (add_type == 2)
            {
                dw_detail.DataWindowObject = "d_mb_addgroup_detail";
                String membgroup_code1 = dw_main.GetItemString(1, "memb_group1");

                DwUtil.RetrieveDataWindow(dw_detail, "mb_add_newgroup.pbl", null, state.SsCoopControl, membgroup_code1);
            }
            else if (add_type == 3)
            {
                String membgroup_code1 = dw_main.GetItemString(1, "memb_group1");
                DwUtil.RetrieveDDDW(dw_main, "membgroup_2", "mb_add_newgroup.pbl", state.SsCoopControl, membgroup_code1);
            }
        }

        private void JsRefresh()
        {
            dw_detail.Reset();
            decimal add_type = dw_main.GetItemDecimal(1, "add_type");
            if (add_type == 1)
            {

                dw_detail.DataWindowObject = "d_mb_addgroup_detail_level1";
                DwUtil.RetrieveDataWindow(dw_detail, "mb_add_newgroup.pbl", null, state.SsCoopControl);
            }
            else
            {
                dw_detail.DataWindowObject = "d_mb_addgroup_detail";

                if (add_type == 2)
                {

                    DwUtil.RetrieveDDDW(dw_main, "memb_group1", "mb_add_newgroup.pbl", state.SsCoopControl, 1);

                }

                else if (add_type == 3)
                {

                    DwUtil.RetrieveDDDW(dw_main, "memb_group1", "mb_add_newgroup.pbl", state.SsCoopControl, 1);
                    //string membgroup_code = dw_main.GetItemString(1, "memb_group1");
                    //DwUtil.RetrieveDDDW(dw_main, "memb_group1", "mb_add_newgroup.pbl", state.SsCoopControl, membgroup_code);
                }
            }
        }


        private void Jsmembgroup_code()
        {
            String group = Hidgroup.Value;
            Sta ta = new Sta(sqlca.ConnectionString);
            //            String sql = @"   SELECT MBUCFMEMBGROUP.MEMBGROUP_CODE,   
            //         MBUCFMEMBGROUP.MEMBGROUP_DESC,   
            //         MBUCFMEMBGROUP.MEMBGROUP_CONTROL  
            //    FROM MBUCFMEMBGROUP  
            //   WHERE length ( trim( mbucfmembgroup.membgroup_code ) ) > ( select length( max( trim( b.membgroup_control ) ) ) from mbucfmembgroup b )  AND  MBUCFMEMBGROUP.MEMBGROUP_CODE='" + group + "' AND  MBUCFMEMBGROUP.COOP_ID='" + state.SsCoopControl + "' ORDER BY MBUCFMEMBGROUP.MEMBGROUP_CODE ASC   ";
            String sql = @"   SELECT MBUCFMEMBGROUP.MEMBGROUP_CODE,   
                              MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                              MBUCFMEMBGROUP.MEMBGROUP_CONTROL  
                              FROM MBUCFMEMBGROUP  
                              WHERE MBUCFMEMBGROUP.MEMBGROUP_LEVEL=2 AND  MBUCFMEMBGROUP.COOP_ID='" + state.SsCoopControl + "' and MBUCFMEMBGROUP.membgroup_code ='" + group + "'  ORDER BY MBUCFMEMBGROUP.MEMBGROUP_CODE ASC   ";

            Sdt dt = ta.Query(sql);
            if (dt.Next())
            {
                String MEMBGROUP_DESC = dt.GetString("MEMBGROUP_DESC");
                String MEMBGROUP_CODE = dt.GetString("MEMBGROUP_CODE");
                dw_main.SetItemString(1, "new_group_1", MEMBGROUP_DESC);
                dw_main.SetItemString(1, "new_group", MEMBGROUP_CODE);
            }
            string group_control = dw_main.GetItemString(1, "new_group");
            dw_main.SetItemString(1, "new_membsection", "");
            DwUtil.RetrieveDDDW(dw_main, "new_membsection_1", "mb_req_chggroup.pbl", state.SsCoopControl, group_control);
        }
        private void JsChangmidgroupcontrol()
        {
            string group = Hidmembsection.Value;
            Sta ta = new Sta(sqlca.ConnectionString);
            String sql = @"   SELECT MBUCFMEMBGROUP.MEMBGROUP_CODE,   
                              MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                              MBUCFMEMBGROUP.MEMBGROUP_CONTROL  
                              FROM MBUCFMEMBGROUP  
                              WHERE MBUCFMEMBGROUP.MEMBGROUP_LEVEL=3 AND  MBUCFMEMBGROUP.COOP_ID='" + state.SsCoopControl + "'  and mbucfmembgroup.membgroup_code ='" + group + "' ORDER BY MBUCFMEMBGROUP.MEMBGROUP_CODE ASC   ";

            Sdt dt = ta.Query(sql);
            if (dt.Next())
            {
                String MEMBGROUP_DESC = dt.GetString("MEMBGROUP_DESC");
                dw_main.SetItemString(1, "new_membsection_1", MEMBGROUP_DESC);
                String MEMBGROUP_CODE = dt.GetString("MEMBGROUP_CODE");
                dw_main.SetItemString(1, "new_membsection", MEMBGROUP_CODE);

            }

        }
        public void SaveWebSheet()
        {
            try
            {
                String membgroupcode="";
                String membgroupdesc="";
                String membg_Control="";
                Sta ta = new Sta(sqlca.ConnectionString);
                decimal add_type = dw_main.GetItemDecimal(1, "add_type");
                if (HdCheckRow.Value == "TRUE")
                {

                    try
                    {
                        membgroupcode = dw_detail.GetItemString(dw_detail.RowCount, "membgroup_code");
                    }
                    catch
                    {
                    }
                    try
                    {
                        membgroupdesc = dw_detail.GetItemString(dw_detail.RowCount, "membgroup_desc");
                    }
                    catch { }
                   
                    try
                    {
                        if (add_type == 1)
                        {
                            dw_detail.DataWindowObject = "d_mb_addgroup_detail_level1";
                            try
                            {
                                String sql = @"insert into mbucfmembgroup (coop_id,membgroup_code, membgroup_desc,mbgrpsect_code,membgroup_level ) values ('" + state.SsCoopId + "','" + membgroupcode + "','" + membgroupdesc + "' , 'ฮฮฮ'," + 1 + " )";
                                ta.Exe(sql);
                                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                            }
                            catch (Exception ex)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                            }
                            ta.Close();
                        }

                        else if (add_type == 2)
                        {
                            dw_detail.DataWindowObject = "d_mb_addgroup_detail";
                            try
                            {
                                membg_Control = dw_main.GetItemString(1, "memb_group1");
                            }
                            catch { }

                            try
                            {
                                String sql = @"insert into mbucfmembgroup (coop_id,membgroup_code,membgroup_control, membgroup_desc,mbgrpsect_code,membgroup_level ) values ('" + state.SsCoopId + "','" + membgroupcode + "','" + membg_Control + "','" + membgroupdesc + "' , 'ฮฮฮ'," + 2 + " )";
                                ta.Exe(sql);
                                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                            }
                            catch (Exception ex)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                            }
                            ta.Close();

                        }
                        else if (add_type == 3)
                        {
                            dw_detail.DataWindowObject = "d_mb_addgroup_detail";
                            try
                            {
                                membg_Control = dw_main.GetItemString(1, "membgroup_2");
                            }
                            catch { }

                            try
                            {
                                String sql = @"insert into mbucfmembgroup (coop_id,membgroup_code,membgroup_control, membgroup_desc,mbgrpsect_code,membgroup_level ) values ('" + state.SsCoopId + "','" + membgroupcode + "','" + membg_Control + "','" + membgroupdesc + "' , 'ฮฮฮ'," + 3 + " )";
                                ta.Exe(sql);
                                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                            }
                            catch (Exception ex)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                            }
                            ta.Close();

                        }
                       
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    }
                }
                else
                {
                    this.ConnectSQLCA();
                    dw_main.SetTransaction(sqlca);
                    dw_detail.SetTransaction(sqlca);                
                    if (add_type == 1)
                    {
                       dw_detail.DataWindowObject = "d_mb_addgroup_detail_level1";
                       DwUtil.UpdateDataWindow(dw_detail, "mb_add_newgroup.pbl","mbucfmembgroup");
                     } else
                        {
                            dw_detail.DataWindowObject = "d_mb_addgroup_detail";
                            DwUtil.UpdateDataWindow(dw_detail, "mb_add_newgroup.pbl", "mbucfmembgroup");
                         }
                 }               
            } 
            catch (Exception ex)
            {
            }
        }


        void WebSheet.WebSheetLoadEnd()
        {
            //tdwmain.Eng2ThaiAllRow();
            // dw_coop.SaveDataCache();
            dw_main.SaveDataCache();
            dw_detail.SaveDataCache();

        }

    }
}




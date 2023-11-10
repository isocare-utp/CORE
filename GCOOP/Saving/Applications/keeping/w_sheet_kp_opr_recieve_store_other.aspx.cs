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
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using DataLibrary;


namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_opr_recieve_store_other : PageWebSheet, WebSheet
    {
        private DwThDate tdwmain;
        protected String postInsertDwDetail; //insert แถว
        protected String jsPostMember;
        protected String jsPostGroup;
        protected String newClear;
        protected String jsRefresh;
        protected String jsmembgroup_code;
        protected String jsCoopSelect;
        protected String jsChangmidgroupcontrol;
        void WebSheet.InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            newClear = WebUtil.JsPostBack(this, "newClear");
            jsChangmidgroupcontrol = WebUtil.JsPostBack(this, "jsChangmidgroupcontrol");
            jsmembgroup_code = WebUtil.JsPostBack(this, "jsmembgroup_code");
            jsCoopSelect = WebUtil.JsPostBack(this, "jsCoopSelect");
            postInsertDwDetail = WebUtil.JsPostBack(this, "postInsertDwDetail");
            tdwmain = new DwThDate(dw_main, this);
            //tdwmain.Add("entry_date", "entry_tdate");

        }

        void WebSheet.WebSheetLoadBegin()
        {
            //try
            //{
            //    shrlonService = wcf.NShrlon;
            //    commonService = wcf.NCommon;
            //}
            //catch
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
            //    return;
            //}
            //this.ConnectSQLCA();
            //dw_main.SetTransaction(sqlca);
            //dw_detail.SetTransaction(sqlca);

            if (IsPostBack)
            {
                try
                {

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
                DwUtil.RetrieveDDDW(dw_detail, "keepothitemtype_code", "kp_opr_receive_store_other.pbl", state.SsCoopControl);
                dw_detail.SetItemDecimal(1, "keepother_type", 1);
                tdwmain.Eng2ThaiAllRow();
                HdIsPostBack.Value = "False";
                

            }
        }

        void WebSheet.CheckJsPostBack(String eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();

            }
            else if (eventArg == "postInsertDwDetail")
            {
                dw_detail.InsertRow(dw_detail.RowCount + 1);
                dw_detail.SetItemString(dw_detail.RowCount, "coop_id", state.SsCoopControl);
                dw_detail.SetItemString(dw_detail.RowCount, "memcoop_id", state.SsCoopControl);
            }


        }




        void WebSheet.SaveWebSheet()
        {
            int j = 0; int k = 0;

            try
            {
                for (int i = 1; i <= dw_detail.RowCount; i++)
                {
                    String member_no = dw_main.GetItemString(1, "member_no");
                    decimal seq_no = dw_detail.GetItemDecimal(i, "cRow");
                    dw_detail.SetItemString(i, "coop_id", state.SsCoopControl);
                    dw_detail.SetItemString(i, "memcoop_id", state.SsCoopId);
                    dw_detail.SetItemString(i, "member_no", member_no);
                    dw_detail.SetItemDecimal(i, "seq_no", seq_no);
                    dw_detail.SetItemDateTime(i, "entry_date", state.SsWorkDate);
                    dw_detail.SetItemString(i, "entry_id", state.SsUsername);

                }
                for (int i = 1; i <= dw_detail.RowCount; i++)
                {
                    try
                    {
                        string recv_period = dw_detail.GetItemString(i, "startkeep_period");
                        j++;
                    }
                    catch 
                    {
                        j--;
                    }
                }
                for (int i = 1; i <= dw_detail.RowCount; i++)
                {
                    if (dw_detail.GetItemDecimal(i, "item_payment") == 0)
                    {
                        k--;
                    }
                    else
                    {
                        k++;
                    }
                }
                if (j < dw_detail.RowCount)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกงวดเริ่มเก็บ");
                }
                else if (k < dw_detail.RowCount)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนเงินเป็นศูนย์");
                }
                else
                {
                    //DwUtil.UpdateDataWindow(dw_detail, "kp_opr_receive_store_other.pbl", "KPRCVKEEPOTHER");
                    //JsNewClear();
                    //LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    string sql = "";
                    string error = "";
                    string ls_memno = "", ls_keepothercode = "", ls_period = "", ls_keetypedesc = "", ls_keepitemtype = "";
                    decimal ldc_amount = 0 , li_kptype = 1;
                    string ls_coopid = state.SsCoopId;

                    ls_memno = dw_main.GetItemString(1, "member_no");

                    sql = "DELETE FROM KPRCVKEEPOTHER  where COOP_ID ='" + state.SsCoopId + "' and  MEMBER_NO = '" + ls_memno + "' ";
                    WebUtil.ExeSQL(sql);

                    int ln_seq = 0;
                    for (int i = 1; i <= dw_detail.RowCount; i++)
                    {
                        try
                        {
                            ls_keepothercode = dw_detail.GetItemString(i, "keepothitemtype_code").Trim();
                            ls_period = dw_detail.GetItemString(i, "startkeep_period");
                            ldc_amount = dw_detail.GetItemDecimal(i, "item_payment");
                            li_kptype = dw_detail.GetItemDecimal(i, "keepother_type");
                            try
                            {
                                ls_keetypedesc = dw_detail.GetItemString(i, "description").Trim();
                            }
                            catch { ls_keetypedesc = ""; }

                            sql = "select isnull(max(seq_no),0)+1  as seq_no from KPRCVKEEPOTHER where coop_id = '" + ls_coopid + "' and  MEMBER_NO = '" + ls_memno + "' ";
                            Sdt dt = WebUtil.QuerySdt(sql);
                            if (dt.Next())
                            { ln_seq = dt.GetInt32("seq_no"); }                            
                            
                            sql = "select keepothitemtype_code,keepothitemtype_desc,keepitemtype_code from KPUCFKEEPOTHITEMTYPE where coop_id = '" + state.SsCoopControl + "' and  keepothitemtype_code = '" + ls_keepothercode + "' ";
                            dt = WebUtil.QuerySdt(sql);
                            if (dt.Next())
                            {
                                ls_keepitemtype = dt.GetString("keepitemtype_code");
                                if (ls_keetypedesc.Length < 1)
                                {
                                    ls_keetypedesc = dt.GetString("keepothitemtype_desc");
                                }
                                
                            }                         

                            //if (state.SsCoopId == "040001")
                            //{
                            //    if (ls_keepothercode == "01")
                            //    {
                            //        ls_keepitemtype = "INS";
                            //    }
                            //    else if (ls_keepothercode == "02")
                            //    {
                            //        ls_keepitemtype = "CMT";
                            //    }
                            //    else if (ls_keepothercode == "03")
                            //    {
                            //        ls_keepitemtype = "GLP";
                            //    }
                            //    else
                            //    {
                            //        ls_keepitemtype = "OTH";
                            //    }
                                    
                            //}
                            //else
                            //{
                            //    ls_keepitemtype = "OTH";
                            //}
                            sql = "INSERT INTO KPRCVKEEPOTHER ( COOP_ID ,MEMBER_NO ,	SEQ_NO,	MEMCOOP_ID,	KEEPITEMTYPE_CODE,	KEEPOTHITEMTYPE_CODE,	KEEPOTHER_TYPE,";
                            sql += "STARTKEEP_PERIOD,	LASTKEEP_PERIOD,	DESCRIPTION,	ITEM_PAYMENT,	ENTRY_ID) values ";
                            sql += "('" + state.SsCoopId.ToString() + "','" + ls_memno + "','" + i + "','" + state.SsCoopId.ToString() + "','" + ls_keepitemtype + "','" + ls_keepothercode + "','" + li_kptype + "',";
                            sql += "'" + ls_period + "','" + ls_period + "','" + ls_keetypedesc + "'," + ldc_amount + ",'" + state.SsUsername + "' )";
                            WebUtil.Query(sql);
                        }
                        catch
                        {
                            error += ls_memno + " ";
                        }
                    }
                    if (error.Trim() == "")
                    {
                        JsNewClear();
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จสิ้น");
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกรายการได้ กรุณาตรวจสอบ");
                    }
                }

            }
            catch (Exception ex)
            {

                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }


        }

        void WebSheet.WebSheetLoadEnd()
        {

            dw_main.SaveDataCache();
            dw_detail.SaveDataCache();

        }
        private void JsPostMember()
        {
            String coop_id = state.SsCoopControl;
            String member_no = dw_main.GetItemString(1, "member_no");

            try
            {
                
                DwUtil.RetrieveDataWindow(dw_main, "kp_opr_receive_store_other.pbl", null, coop_id, member_no); //dw_main.Retrieve(coop_id, member_no);
                DwUtil.RetrieveDataWindow(dw_detail, "kp_opr_receive_store_other.pbl", null, coop_id, member_no); //dw_detail.Retrieve(coop_id, member_no);
                DwUtil.RetrieveDDDW(dw_detail, "keepothitemtype_code", "kp_opr_receive_store_other.pbl", state.SsCoopControl);
                if (dw_detail.RowCount == 0)
                {
                    dw_detail.InsertRow(0);               
                    dw_detail.SetItemDecimal(1, "keepother_type", 1);
                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }
        }
        //JS-EVENT
        private void JsNewClear()
        {
            dw_main.Reset();
            dw_detail.Reset();
            dw_main.InsertRow(0);
            dw_detail.InsertRow(0);
            DwUtil.RetrieveDDDW(dw_detail, "keepothitemtype_code", "kp_opr_receive_store_other.pbl", state.SsCoopControl);
            dw_detail.SetItemDecimal(1, "keepother_type", 1);
            HdIsPostBack.Value = "False";
        }
    }
}

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
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using DataLibrary;
using System.Threading;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_collateral_master : PageWebSheet, WebSheet
    {
        public String app = "";
        public String gid = "";
        public String rid = "";
        protected String pdf = "";
        private DwThDate tdwhead;
        private DwThDate tdw_detail_02;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsPostMember;
        protected String PostMember1;
        protected String jsPostCollmast;
        protected String newClear;
        protected String jsRefresh;
        protected String postInsertRow;
        protected String jsgetmember;
        protected String jsDeleteRow;
        protected String jspostPorvince;
        protected String jsprintColl;
        //==============
        private void RunProcess()
        {
            String print_id = dw_printset.GetItemString(1, "coll_printset").Trim();
            String as_mastno = Hmastno.Value;


            app = state.SsApplication;
            try
            {
                gid = "LN_PRINT";
            }
            catch { }
            try
            {
                rid = print_id;
            }
            catch { }


            if (as_mastno == null || as_mastno == "")
            {
                return;
            }
            else
            {
                ReportHelper lnv_helper = new ReportHelper();
                lnv_helper.AddArgument(as_mastno, ArgumentType.String);

                //****************************************************************

                //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
                String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
                pdfFileName += "_" + gid + "_" + rid + ".pdf";
                pdfFileName = pdfFileName.Trim();

                //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
                try
                {
                    //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                    //String criteriaXML = lnv_helper.PopArgumentsXML();
                    //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                    //String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
                    //if (li_return == "true")
                    //{
                    //    HdOpenIFrame.Value = "True";
                    //}
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    return;
                }
            }
        }
        //===============
        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            PostMember1 = WebUtil.JsPostBack(this, "PostMember1");
            jsPostCollmast = WebUtil.JsPostBack(this, "jsPostCollmast");
            jsRefresh = WebUtil.JsPostBack(this, "jsRefresh");
            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            jsgetmember = WebUtil.JsPostBack(this, "jsgetmember");
            jsDeleteRow = WebUtil.JsPostBack(this, "jsDeleteRow");
            jspostPorvince = WebUtil.JsPostBack(this, "jspostPorvince");
            jsprintColl = WebUtil.JsPostBack(this, "jsprintColl");
            newClear = WebUtil.JsPostBack(this, "newClear");
            tdwhead = new DwThDate(dw_head, this);
            tdwhead.Add("mortgage_date", "mortgage_tdate");
            tdwhead.Add("redeem_date", "redeem_tdate");
            tdw_detail_02 = new DwThDate(dw_detail_02, this);
            tdw_detail_02.Add("mortgage_date", "mortgage_tdate");

        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();
            dw_collddetail.SetTransaction(sqlca);
            dw_detail_01.SetTransaction(sqlca);
            if (IsPostBack)
            {

                try
                {
                    dw_main.RestoreContext();
                    dw_list.RestoreContext();
                    dw_head.RestoreContext();
                    dw_detail.RestoreContext();
                    dw_collddetail.RestoreContext();
                    dw_detail_01.RestoreContext();
                    dw_detail_02.RestoreContext();
                    dw_printset.RestoreContext();

                }
                catch { }

            }
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(0);
                dw_list.InsertRow(0);
                dw_head.InsertRow(0);
                dw_collddetail.InsertRow(0);
                dw_detail_01.InsertRow(0);
                dw_detail_02.InsertRow(0);
                dw_printset.InsertRow(1);


                //  dw_detail.InsertRow(0);

                dw_head.SetItemDate(1, "mortgage_date", state.SsWorkDate);
                dw_head.SetItemDate(1, "redeem_date", state.SsWorkDate);
                dw_detail_02.SetItemDate(1, "mortgage_date", state.SsWorkDate);

                tdwhead.Eng2ThaiAllRow();
                tdw_detail_02.Eng2ThaiAllRow();
                GetDDDW();

            }

        }


        public void GetDDDW()
        {
            try
            {
                WebUtil.RetrieveDDDW(dw_list, "collmasttype_code", "sl_collateral_master.pbl", null);

                //WebUtil.RetrieveDDDW(dw_collddetail, "pos_district", "sl_collateral_master.pbl", null);
                WebUtil.RetrieveDDDW(dw_collddetail, "pos_province", "sl_collateral_master.pbl", null);
                //WebUtil.RetrieveDDDW(dw_detail_01, "mrtg_district", "sl_collateral_master.pbl", null);
                WebUtil.RetrieveDDDW(dw_detail_01, "mrtg_province", "sl_collateral_master.pbl", null);
                //WebUtil.RetrieveDDDW(dw_detail_01, "autrz_district", "sl_collateral_master.pbl", null);
                WebUtil.RetrieveDDDW(dw_detail_01, "autrz_province", "sl_collateral_master.pbl", null);
                WebUtil.RetrieveDDDW(dw_collddetail, "buildingtype_code", "sl_collateral_master.pbl", null);
                WebUtil.RetrieveDDDW(dw_printset, "coll_printset", "sl_collateral_master.pbl", null);
            }
            catch { }

        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "PostMember1")
            {
                jsPostMember1();
            }
            else if (eventArg == "jsPostCollmast")
            {
                JsPostCollmast();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
            else if (eventArg == "jsRefresh")
            {

            }
            else if (eventArg == "postInsertRow")
            {
                JsPostInsertRow();
            }
            else if (eventArg == "jsgetmember")
            {
                Jsgetmember();
            }
            else if (eventArg == "jsDeleteRow")
            { JsDeleteRow(); }
            else if (eventArg == "jspostPorvince")
            {
                JspostPorvince();
            }
            else if (eventArg == "jsprintColl")
            {
                JsprintColl();
            }
        }

        private void JsprintColl()
        {
            //Mai
            RunProcess();
            // Thread.Sleep(5000);
            Thread.Sleep(4500);

            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdf + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }


        private void JspostPorvince()
        {

        }

        public void SaveWebSheet()
        {
            try
            {
                str_lncollmast strlncoll = new str_lncollmast();
                strlncoll.member_no = Hfmember_no.Value;
                strlncoll.xml_collmastdet = dw_head.Describe("DataWindow.Data.XML");
                // strlncoll.xml_collmemco = dw_detail.RowCount > 0 ? dw_detail.Describe("DataWindow.Data.XML") : "";
                if (dw_detail.RowCount == 0)
                {
                    dw_detail.InsertRow(1);
                    dw_detail.SetItemString(1, "memco_no", Hfmember_no.Value);
                    dw_detail.SetItemDecimal(1, "collmastmain_flag", 1);
                    dw_detail.SetItemString(1, "coop_id", state.SsCoopId);


                }
                strlncoll.xml_collmemco = dw_detail.Describe("DataWindow.Data.XML");
                strlncoll.xml_mrtg1 = dw_collddetail.Describe("DataWindow.Data.XML");
                strlncoll.xml_mrtg2 = dw_detail_01.Describe("DataWindow.Data.XML");
                strlncoll.xml_mrtg3 = dw_detail_02.Describe("DataWindow.Data.XML");

                strlncoll.entry_id = state.SsUsername;
                strlncoll.coop_id = state.SsCoopControl;

                int result = shrlonService.of_savelncollmast(state.SsWsPass, ref strlncoll);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อยแล้ว");

                }
                JsPostInsertRow();
                dw_detail.Reset(); dw_detail.InsertRow(0);
                JsPostMember();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

        }

        public void WebSheetLoadEnd()
        {
            WebUtil.RetrieveDDDW(dw_head, "collmasttype_code", "sl_collateral_master.pbl", null);
            WebUtil.RetrieveDDDW(dw_head, "collrelation_code", "sl_collateral_master.pbl", null);
            WebUtil.RetrieveDDDW(dw_printset, "coll_printset", "sl_collateral_master.pbl", null);
            tdwhead.Eng2ThaiAllRow();

            ShowDistrict();
            //  GetDDDW();

        }
        private void jsPostMember1()
        {
            Sta ta = new Sta(new DwTrans().ConnectionString);
            String memberno = Hmemno.Value;
            String sql = @"SELECT
         MBMEMBMASTER.MEMBER_NO,   
         MBUCFPRENAME.PRENAME_DESC||MBMEMBMASTER.MEMB_NAME||' '||MBMEMBMASTER.MEMB_SURNAME AS name ,   
         MBMEMBMASTER.CARD_PERSON  
    FROM MBMEMBMASTER,          
         MBUCFPRENAME  
         
   WHERE ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and
          MBMEMBMASTER.MEMBER_NO = '" + memberno + "'  ";
            Sdt dt = ta.Query(sql);
            String memno = dt.Rows[0]["member_no"].ToString();
            String name = dt.Rows[0]["name"].ToString();
            String CARD_PERSON = dt.Rows[0]["CARD_PERSON"].ToString();

            dw_detail_01.SetItemString(1, "mrtg_memberno", memno);
            dw_detail_01.SetItemString(1, "mrtg_name", name);
            dw_detail_01.SetItemString(1, "mrtg_personid", CARD_PERSON);

        }

        private void JsPostMember()
        {
            try
            {
                str_lncollmast strlncoll = new str_lncollmast();
                strlncoll.member_no = Hfmember_no.Value;
                strlncoll.coop_id = state.SsCoopControl;
                strlncoll.xml_memdet = dw_main.Describe("DataWindow.Data.XML");
                strlncoll.xml_collmastlist = dw_list.Describe("DataWindow.Data.XML");

                int result = shrlonService.of_initlncollmastall(state.SsWsPass, ref strlncoll);
                if (result == 1)
                {
                    try
                    {
                        dw_main.Reset();
                        dw_main.ImportString(strlncoll.xml_memdet, FileSaveAsType.Xml);

                        if (strlncoll.xml_collmastlist == "")
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบหลักทรัพย์ กรุณเพิ่มหลักทรัพย์ " + strlncoll.xml_collmastlist);
                            JsPostInsertRow();
                        }
                        else
                        {

                            dw_list.Reset();
                            dw_list.ImportString(strlncoll.xml_collmastlist, FileSaveAsType.Xml);
                            DwUtil.RetrieveDDDW(dw_list, "collmasttype_code", "sl_collateral_master.pbl", null);
                        }
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบสมาชิกท่านนี้ กรุณากรอกเลขสมาชิกอีกครั้ง");
                        dw_main.Reset(); dw_main.InsertRow(0);

                    }
                    if (dw_main.RowCount > 1)
                    {
                        dw_main.DeleteRow(dw_main.RowCount);

                    }



                }


            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }

        }

        private void JsPostCollmast()
        {
            try
            {
                str_lncollmast strlncoll = new str_lncollmast();
                strlncoll.collmast_no = HfSlipNo.Value;
                strlncoll.coop_id = state.SsCoopControl;
                int li_row = dw_list.FindRow("collmast_no = '" + strlncoll.collmast_no.Trim() + "'", 0, dw_list.RowCount);
                if (li_row > 0)
                {

                    strlncoll.xml_collmastdet = dw_head.Describe("DataWindow.Data.XML");
                    strlncoll.xml_collmemco = dw_detail.Describe("DataWindow.Data.XML");
                    strlncoll.xml_mrtg1 = dw_collddetail.Describe("DataWindow.Data.XML");
                    strlncoll.xml_mrtg2 = dw_detail_01.Describe("DataWindow.Data.XML");
                    strlncoll.xml_mrtg3 = dw_detail_02.Describe("DataWindow.Data.XML");


                    int result = shrlonService.of_initlncollmastdet(state.SsWsPass, ref  strlncoll);
                    try
                    {


                        dw_head.Reset();
                        dw_head.ImportString(strlncoll.xml_collmastdet, FileSaveAsType.Xml);
                        tdwhead.Eng2ThaiAllRow();
                        if (dw_head.RowCount > 1)
                        {
                            dw_head.DeleteRow(dw_head.RowCount);

                        }

                        try
                        {
                            dw_detail.Reset();
                            dw_detail.ImportString(strlncoll.xml_collmemco, FileSaveAsType.Xml);
                        }
                        catch (Exception ex) { DwUtil.ImportData(strlncoll.xml_collmemco, dw_detail, null, FileSaveAsType.Xml); }
                        try
                        {
                            dw_collddetail.Reset();
                            dw_collddetail.ImportString(strlncoll.xml_mrtg1, FileSaveAsType.Xml);
                        }
                        catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                        try
                        {
                            dw_detail_01.Reset();
                            dw_detail_01.ImportString(strlncoll.xml_mrtg2, FileSaveAsType.Xml);
                        }
                        catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                        try
                        {
                            dw_detail_02.Reset();
                            dw_detail_02.ImportString(strlncoll.xml_mrtg3, FileSaveAsType.Xml);
                        }
                        catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                        GetDDDW();

                    }
                    catch
                    {
                        //  LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
                        //dw_head.Reset(); dw_head.InsertRow(0);
                        //dw_detail.Reset();
                    }

                    dw_list.SelectRow(0, false);
                    dw_list.SelectRow(li_row, true);
                    dw_list.SetRow(li_row);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }

        }

        public void JsRefresh()
        {


        }

        //JS-EVENT
        private void JsPostInsertRow()
        {
            dw_list.Reset();
            dw_list.InsertRow(1);
            dw_detail.Reset();
            dw_head.Reset();
            dw_head.InsertRow(1);
            dw_head.SetItemDate(1, "mortgage_date", state.SsWorkDate);
            dw_head.SetItemDate(1, "redeem_date", state.SsWorkDate);

            if (dw_head.RowCount > 1)
            {
                dw_head.DeleteRow(dw_head.RowCount);
            }
            GetDDDW();

        }

        private void Jsgetmember()
        {
            String member_no = Hmemco_no.Value;
            int row = dw_detail.RowCount;
            String sql = @" SELECT MBMEMBMASTER.MEMBER_NO,   
                           ( MBMEMBMASTER.MEMB_NAME||'  '||  MBMEMBMASTER.MEMB_SURNAME)as MEMB_NAME
                        FROM MBMEMBMASTER       WHERE   MBMEMBMASTER.MEMBER_NO='" + member_no + "' ";
            DataTable dt = WebUtil.Query(sql);
            if (dt.Rows.Count > 0)
            {
                dw_detail.SetItemString(row, "memco_no", dt.Rows[0]["MEMBER_NO"].ToString());
                dw_detail.SetItemString(row, "mem_name", dt.Rows[0]["MEMB_NAME"].ToString());
            }
        }
        private void JsDeleteRow()
        {
            try
            {

                Int32 row = Convert.ToInt32(HDeleteRow.Value);
                dw_detail.DeleteRow(row);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

        }
        //JS-EVENT
        private void JsNewClear()
        {
            dw_main.Reset();
            dw_list.Reset();
            dw_head.Reset();
            dw_detail.Reset();
            dw_detail_01.Reset();
            dw_detail_02.Reset();
            dw_collddetail.Reset();
            dw_main.InsertRow(0);
            dw_list.InsertRow(0);
            dw_head.InsertRow(1);
            dw_collddetail.InsertRow(0);
            dw_detail_01.InsertRow(0);
            dw_detail_02.InsertRow(0);
            dw_head.SetItemDate(1, "mortgage_date", state.SsWorkDate);
            dw_head.SetItemDate(1, "redeem_date", state.SsWorkDate);
            dw_detail_02.SetItemDate(1, "mortgage_date", state.SsWorkDate);

            tdwhead.Eng2ThaiAllRow();
            tdw_detail_02.Eng2ThaiAllRow();
            tdwhead.Eng2ThaiAllRow();
            GetDDDW();

        }

        private void ShowDistrict()
        {
            //แสดงอำเภอ
            try
            {
                String province = dw_collddetail.GetItemString(1, "pos_province").Trim();

                String mrtgProv = dw_detail_01.GetItemString(1, "mrtg_province").Trim();
                String autrzProv = dw_detail_01.GetItemString(1, "autrz_province").Trim();
                //String posDist, mrtgDist, autrzDist = "";

                //try { posDist = dw_collddetail.GetItemString(1, "pos_district");  }
                //catch { posDist = ""; }
                //try { mrtgDist = dw_detail_01.GetItemString(1, "mrtg_district"); }
                //catch { mrtgDist = ""; }
                //try { autrzDist = dw_detail_01.GetItemString(1, "autrz_district"); }
                //catch { autrzDist = ""; }
                if (province != "")
                {

                    //dw_collddetail.SetItemString(1, "pos_district", "");
                    DwUtil.RetrieveDDDW(dw_collddetail, "pos_district", "sl_collateral_master.pbl", province);
                }
                if (mrtgProv != "")
                {
                    //dw_detail_01.SetItemString(1, "mrtg_district", "");
                    DwUtil.RetrieveDDDW(dw_detail_01, "mrtg_district", "sl_collateral_master.pbl", mrtgProv);
                }
                if (autrzProv != "")
                {
                    //dw_detail_01.SetItemString(1, "autrz_district", "");
                    DwUtil.RetrieveDDDW(dw_detail_01, "autrz_district", "sl_collateral_master.pbl", autrzProv);
                }
            }
            catch
            { }
        }
    }
}

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
using CoreSavingLibrary.WcfNKeeping;
using Sybase.DataWindow;
using DataLibrary;


namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_kptp_cancel : PageWebSheet, WebSheet
    {
        protected String postInit;
        protected String postInitMember;
        private n_keepingClient keepingService;
        
        public String pbl = "kp_adjust_monthly.pbl";
        //==============================     
        public void InitJsPostBack()
        {
            postInit = WebUtil.JsPostBack(this, "postInit");
            postInitMember = WebUtil.JsPostBack(this, "postInitMember");
        }

        public void WebSheetLoadBegin()
        {
            keepingService = wcf.NKeeping;

            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else 
            {
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_list);
                this.RestoreContextDw(Dw_detmain);
                this.RestoreContextDw(Dw_detdetail);
            }
        }

        public void CheckJsPostBack(String eventArg)
        {
            if (eventArg == "postInit")
            {
                JspostInit();
            }
            else if (eventArg == "postInitMember")
            {
                JspostInitMember();
            }
        }

       public void SaveWebSheet()
        {
           //ตรวจสอบแถว list ว่ามีหรือเปล่า
            if (Dw_list.RowCount > 0)
            {
                //ตรวจสอบว่ามีการเลือกเช็คถูกหรือไม่
                int rowcheck = 0;
                for (int j = 1; j <= Dw_list.RowCount; j++)
                {
                    Decimal operate_flag = Dw_list.GetItemDecimal(j, "operate_flag");
                    if (operate_flag == 1)
                    {
                        rowcheck = rowcheck + 1;
                    }
                }

                //กรณีถ้าหากมีการเลือกรายการที่ list
                if (rowcheck > 0)
                {
                    Decimal cancel = 0;
                    Decimal ok = 0;
                    for (int i = 1; i <= Dw_detdetail.RowCount; i++)
                    {
                        Decimal keepitem_status = Dw_detdetail.GetItemDecimal(i, "keepitem_status");
                        Dw_detdetail.SetItemString(i, "bizzcoop_id", state.SsCoopId);
                        Dw_detdetail.SetItemString(i, "cancel_id", state.SsUsername);

                        if (keepitem_status == -9)
                        {
                            cancel = cancel + 1;
                        }
                        else
                        {
                            ok = ok + 1;
                        }

                        //ถ้ากรณีที่เลือกหมดทุกรายการ
                        Decimal keeping_status = Dw_detmain.GetItemDecimal(1, "keeping_status");
                        
                        if (cancel == Dw_detdetail.RowCount && keeping_status != -9 || ok == Dw_detdetail.RowCount && keeping_status != 1)
                        {
                           // Decimal keeping_status = Dw_detmain.GetItemDecimal(1, "keeping_status");
                            //กรณีที่ปกติให้ update เป็น -9 , กรณีที่ -9 update ให้เป็น 1
                            if (keeping_status == 1)
                            {
                                Dw_detmain.SetItemDecimal(1, "keeping_status", -9);
                            }
                            else
                            {
                                Dw_detmain.SetItemDecimal(1, "keeping_status", 1);
                            }
                        }
                    }

                    try
                    {
                        String xml_main = Dw_main.Describe("DataWindow.Data.XML");
                        String xml_detmain = Dw_detmain.Describe("DataWindow.Data.XML");
                        String xml_detdetail = Dw_detdetail.Describe("DataWindow.Data.XML");
                        str_keep_adjust astr_keep_adjust = new str_keep_adjust();
                        astr_keep_adjust.xml_main = xml_main;
                        astr_keep_adjust.xml_det_main = xml_detmain;
                        astr_keep_adjust.xml_det_detail = xml_detdetail;
                        int result = keepingService.of_save_kptp_ccl(state.SsWsPass, ref astr_keep_adjust);
                        if (result == 1)
                        {
                            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                            JspostNewClear();
                        }
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกงวดที่ต้องการทำรายการ");
                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลการยกเลิกกระดาษทำการ");
            }
            
          
        }

       public void WebSheetLoadEnd()
        {
            Dw_main.SaveDataCache();
            Dw_list.SaveDataCache();
            Dw_detmain.SaveDataCache();
            Dw_detdetail.SaveDataCache();
        }
    //================================
       private void JspostNewClear()
       {
           Dw_main.Reset();
           Dw_main.InsertRow(0);
           Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
           Dw_list.Reset();
           Dw_list.InsertRow(0);
           Dw_detmain.Reset();
           Dw_detdetail.Reset();
           HdIsPostBack.Value = "false";
       }

       private void JspostInit()
       {
           try 
           {
               String member_no = Dw_main.GetItemString(1, "member_no");
               member_no = WebUtil.MemberNoFormat(member_no);
               Dw_main.SetItemString(1, "member_no",member_no);
               String xml_main = Dw_main.Describe("DataWindow.Data.XML");
               str_keep_adjust astr_keep_adjust = new str_keep_adjust();
               astr_keep_adjust.xml_main = xml_main;
               int result = keepingService.of_init_kptp_ccl(state.SsWsPass, ref astr_keep_adjust);
               if (result == 1)
               {
                   Dw_main.Reset();
                   DwUtil.ImportData(astr_keep_adjust.xml_main, Dw_main, null, FileSaveAsType.Xml);
                   Dw_list.Reset();
                   DwUtil.ImportData(astr_keep_adjust.xml_list, Dw_list, null, FileSaveAsType.Xml);
                   Dw_detmain.Reset();
                   DwUtil.ImportData(astr_keep_adjust.xml_det_main, Dw_detmain, null, FileSaveAsType.Xml);
                   Dw_detdetail.Reset();
                   DwUtil.ImportData(astr_keep_adjust.xml_det_detail, Dw_detdetail, null, FileSaveAsType.Xml);
               }
           }
           catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
       }

       private void JspostInitMember()
       {
           try
           {
               String member_no = Hd_memberno.Value.Trim();
               member_no = WebUtil.MemberNoFormat(member_no);
               Dw_main.SetItemString(1, "member_no", member_no);
               String xml_main = Dw_main.Describe("DataWindow.Data.XML");
               str_keep_adjust astr_keep_adjust = new str_keep_adjust();
               astr_keep_adjust.xml_main = xml_main;
               int result = keepingService.of_init_kptp_ccl(state.SsWsPass, ref astr_keep_adjust);
               if (result == 1)
               {
                   Dw_main.Reset();
                   DwUtil.ImportData(astr_keep_adjust.xml_main, Dw_main, null, FileSaveAsType.Xml);
                   Dw_list.Reset();
                   DwUtil.ImportData(astr_keep_adjust.xml_list, Dw_list, null, FileSaveAsType.Xml);
                   Dw_detmain.Reset();
                   DwUtil.ImportData(astr_keep_adjust.xml_det_main, Dw_detmain, null, FileSaveAsType.Xml);
                   Dw_detdetail.Reset();
                   DwUtil.ImportData(astr_keep_adjust.xml_det_detail, Dw_detdetail, null, FileSaveAsType.Xml);
               }
           }
           catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
       }


       protected void CheckAll_CheckedChanged(object sender, EventArgs e)
       {
           if (Dw_detdetail.RowCount > 0)
           {
               for (int i = 1; i <= Dw_detdetail.RowCount; i++)
               {
                   Decimal keepitem_status = Dw_detdetail.GetItemDecimal(i, "keepitem_status");
                   if (keepitem_status == 1)
                   {
                       Dw_detdetail.SetItemDecimal(i, "keepitem_status", -9);
                   }
                   else
                   {
                       Dw_detdetail.SetItemDecimal(i, "keepitem_status", 1);
                   }
               }
           }
       }
    }
}

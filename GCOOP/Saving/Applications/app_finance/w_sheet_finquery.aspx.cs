using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;
using DataLibrary;
//using CoreSavingLibrary.WcfFinance;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_finquery : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private DwThDate tDwUser;
        private String pbl = "finquery.pbl";
        protected String newClear;
        protected String postFinQuery;
        protected string postInitUser;
        protected String postProcess;
        public string outputProcess;
        DataStore DStore;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwUser = new DwThDate(DwUser, this);
            tDwUser.Add("adtm_date", "adtm_tdate");

            newClear = WebUtil.JsPostBack(this, "newClear");
            postFinQuery = WebUtil.JsPostBack(this, "postFinQuery");
            postInitUser = WebUtil.JsPostBack(this, "postInitUser");
            postProcess = WebUtil.JsPostBack(this, "postProcess");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            fin = wcf.NFinance;
            DStore = new DataStore();
            DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\app_finance\finquery.pbl";
            DStore.DataWindowObject = "d_finquery_userid";

            if (!IsPostBack)
            {
                NewClear();
                //DwUser.SetTransaction(sqlca);
                //DwUser.Retrieve(state.SsCoopId, state.SsWorkDate);
            }
            else
            {
                this.RestoreContextDw(DwUser);
                this.RestoreContextDw(DwUserDetail);
                this.RestoreContextDw(DwRecv);
                this.RestoreContextDw(DwPay);
                this.RestoreContextDw(DwProc);
            }

            DataWindowChild DcBranch = DwUser.GetChild("as_coopid");
            DwUtil.RetrieveDDDW(DwUser, "as_coopid", pbl, state.SsCoopId);

            tDwUser.Eng2ThaiAllRow();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postFinQuery")
            {
                FinQuery();
            }
            else if (eventArg == "newClear")
            {
                NewClear();
            }
            else if (eventArg == "postInitUser")
            {
                InitUser();
            }
            else if (eventArg == "postProcess")
            {
                Process();
            }
        }
        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            DwUser.SaveDataCache();
            DwUserDetail.SaveDataCache();
            DwRecv.SaveDataCache();
            DwPay.SaveDataCache();
            DwProc.SaveDataCache();
        }

        #endregion


        public void NewClear()
        {
            DwUser.Reset();
            DwUserDetail.Reset();
            DwRecv.Reset();
            DwPay.Reset();
            DwProc.Reset();

            DwUser.InsertRow(0);
            DwUserDetail.InsertRow(0);
            DwProc.InsertRow(0);

            DwUser.SetItemDateTime(1, "adtm_date", state.SsWorkDate);
            tDwUser.Eng2ThaiAllRow();

            DwUtil.RetrieveDDDW(DwUser, "as_coopid", "finquery.pbl", state.SsCoopId);
            DataWindowChild dc = DwUser.GetChild("as_coopid");
            DwUser.SetItemString(1, "as_coopid", state.SsCoopId);
        }

        private void Process()
        {
            String prcoXml = "";
            prcoXml = DwProc.Describe("DataWindow.Data.XML");
            string xmldate = DwUser.Describe("DataWindow.Data.XML");
            try
            {
                //int re = fin.OfProcessOtherToFin(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsClientIp, prcoXml);
                //int re = wcf.NFinance.of_postprocessotherto_fin(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsClientIp, prcoXml);
                //if (re == 1)
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("อัตเดตสถานะเรียบร้อย");
                //}
                string process_name = "POSTTOFIN";
                int use_status = OfCheckUseProcess(process_name);
                if (use_status == 0)
                {
                    outputProcess = WebUtil.runProcessing(state, "POSTTOFIN", state.SsClientIp, prcoXml, xmldate);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.WarningMessage2("ยังไม่สามารถทำการประมวลได้ กรุณารอสักครู่ เนื่องจากมีผู้ใช้งานอื่นทำการประมวล");
                    NewClear();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        protected void InitUser()
        {
            String DwUserXml, fullname = "";
            Int32 resultXml;

            try
            {
                DwUserXml = DwUser.Describe("DataWindow.Data.XML");
                resultXml = fin.of_init_fincashcontrol_user(state.SsWsPass, ref DwUserXml, ref fullname);

                DwUserDetail.Reset();
                DwUserDetail.ImportString(DwUserXml, FileSaveAsType.Xml);
                DwUserDetail.Modify("amsecusers_full_name ='" + fullname + "'");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }

        }

        //protected void InitUser()
        //{
        //    String DwUserXml;
        //    String fincash = "", as_fullname = "";
        //    try
        //    {
        //        DwUserXml = DwUser.Describe("DataWindow.Data.XML");
        //        wcf.NFinance.of_init_fincashcontrol_user(state.SsWsPass, ref fincash, ref as_fullname);
        //        DwUserDetail.Reset();
        //        DwUserDetail.ImportString(fincash, FileSaveAsType.Xml);
        //        DwUserDetail.Modify("amsecusers_full_name ='" + as_fullname + "'");

        //    }
        //    catch (SoapException ex)
        //    {
        //        LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
        //    }
        //}

        protected void FinQuery()
        {
            String userXml = "";
            String[] resultXml;
            int result=0;
            string refRecXml = "", refPayXml = "", refUserXml = "" ;
            try
            {
                userXml = DwUser.Describe("DataWindow.Data.XML");
                //resultXml = wcf.Finance.FinQuery(state.SsWsPass, state.SsApplication, userXml);
                result = wcf.NFinance.of_finquery(state.SsWsPass, state.SsApplication, userXml, ref refUserXml, ref refRecXml, ref refPayXml);

                DwUserDetail.Reset();
                DwRecv.Reset();
                DwPay.Reset();
                try
                {
                    DwUtil.ImportData(refUserXml, DwUserDetail, null, FileSaveAsType.Xml);
                    DwUtil.ImportData(refRecXml, DwRecv, null, FileSaveAsType.Xml);
                    DwUtil.ImportData(refPayXml, DwPay, null, FileSaveAsType.Xml);
                }
                catch { }
                try
                {
                    string ls_coopid = state.SsCoopId;
                    string ls_username = DwUser.GetItemString(1, "as_userid");
                    DateTime work_date = DwUser.GetItemDateTime(1, "adtm_date");
                    decimal ld_teller = 0;
                    decimal[] rerult = financeFunction.of_is_teller(ls_coopid, ls_username, work_date);
                    ld_teller = rerult[2];//เงินคงเหลือในลิ้นชัก
                    decimal retail_cash = financeFunction.of_checkamsecapvlevel(state.SsCoopControl, ls_username);
                    if (ld_teller > retail_cash)
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage(ls_username + " มียอดเงินสดในระบบมากกว่ายอดที่กำหนด " + ld_teller.ToString("#,##0.00") + "/" + retail_cash.ToString("#,##0.00"));
                    }
                }
                catch { }
            }
            catch (SoapException ex)
            {
                DwUserDetail.Reset();
                DwUserDetail.InsertRow(0);
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }

        }
        //เปลี่ยน Array เป็น ตัวแปร Reference และเปลี่ยนจาก SoapException เป็น Exception เปลี่ยน ErrorMessage จาก WebUtil.SoapMessage(ex) เป็น ex


        //protected void FinQuery()
        //{
        //    String userXml = "";
        //    //String xml1 = "", xml2 = "", xml3 = "";
        //    String UserDetailXml = "", RecvXml = "", PayXml = "";
        //    String[] Xml = new String[3];
        //    try
        //    {
        //        userXml = DwUser.Describe("DataWindow.Data.XML");
        //        //wcf.NFinance.of_finquery(state.SsWsPass, state.SsApplication, userXml, ref xml1, ref xml2, ref xml3);
        //        wcf.NFinance.of_finquery(state.SsWsPass, state.SsApplication, userXml, ref UserDetailXml, ref RecvXml, ref PayXml);
        //        DwUserDetail.Reset();
        //        DwRecv.Reset();
        //        DwPay.Reset();

        //        DwUtil.ImportData(UserDetailXml, DwUserDetail, null, FileSaveAsType.Xml);
        //        DwUtil.ImportData(RecvXml, DwRecv, null, FileSaveAsType.Xml);
        //        DwUtil.ImportData(PayXml, DwPay, null, FileSaveAsType.Xml);
        //    }
        //    catch (Exception ex)
        //    {
        //        DwUserDetail.Reset();
        //        DwUserDetail.InsertRow(0);
        //        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
        //    }

        //}

        private int OfCheckUseProcess(string processsName)
        {
            int useprocess_status = 0;
            String se = @"select count(1) as checkstate from cmprocessing 
                        where object_name = '"+processsName+"'and runtime_status = 0 and workdate = convert(datetime,'"+ state.SsWorkDate.ToString("yyyy/MM/dd") +"')";
            Sdt ta = WebUtil.QuerySdt(se);
            if (ta.Next())
            {
                useprocess_status = Convert.ToInt32(ta.GetDecimal("checkstate"));
            }
            return useprocess_status;
        }
    }
}

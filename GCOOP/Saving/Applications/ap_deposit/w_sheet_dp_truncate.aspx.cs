using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
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
using Sybase.DataWindow;
using System.Data.OracleClient;
using System.Globalization;
using CoreSavingLibrary.WcfNDeposit;
using Saving;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Web.Services.Protocols;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_truncate : PageWebSheet, WebSheet
    {

        protected String PostCutProcess;
        protected String PostRetriveDepttrans;
        protected String PostRetrive; 
        private DwThDate tdw_processday;
        protected str_procdeptupmonth strdps;
        private n_depositClient dpService;
        String YYMM = "";
        public string outputProcess;
        string pbl = "dp_depttrans.pbl";
        public void InitJsPostBack()
        {
            PostRetriveDepttrans = WebUtil.JsPostBack(this, "PostRetriveDepttrans");
            PostCutProcess = WebUtil.JsPostBack(this, "PostCutProcess");
            PostRetrive = WebUtil.JsPostBack(this, "PostRetrive");
            tdw_processday = new DwThDate(Dw_Main, this);
            tdw_processday.Add("adtm_operatedate", "adtm_tdate");

        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_Main.SetTransaction(sqlca);
            Dw_Detail.SetTransaction(sqlca);

            dpService = wcf.NDeposit;

            if (!IsPostBack)
            {
                Dw_Main.InsertRow(0);
                Dw_Main.SetItemDateTime(1, "adtm_operatedate", state.SsWorkDate);
                DateTime adtm_operatedate = Dw_Main.GetItemDateTime(1, "adtm_operatedate");
                tdw_processday.Eng2ThaiAllRow();
                String year = adtm_operatedate.ToString("yyyy", new CultureInfo("th-TH"));
                int year2 = Convert.ToInt16(year);
                Dw_Main.SetItemDecimal(1, "ai_year", year2);
                String month = adtm_operatedate.ToString("MM", new CultureInfo("th-TH"));
                int month2 = Convert.ToInt16(month);
                Dw_Main.SetItemDecimal(1, "ai_month", month2);

                
               // String YYMM = year + month;
                //Dw_Main.SetItemDecimal(1, "ai_year", adtm_operatedate.To("ddMMyyyy", new CultureInfo("th-TH")));
            }
            else
            {
                this.RestoreContextDw(Dw_Main, tdw_processday);
                this.RestoreContextDw(Dw_Detail);
                String month = Dw_Main.GetItemDecimal(1, "ai_month").ToString("00");
                String year = Dw_Main.GetItemDecimal(1, "ai_year").ToString("0000");
                YYMM = year + month + '%';
               
            }
        }

        public void CheckJsPostBack(string eventArg)
        {


            if (eventArg == "PostRetriveDepttrans")
            {
                JsPostRetriveDepttrans();
            }
            if (eventArg == "PostCutProcess")
            {
                JsPostCutProcess();
                
            }
            if (eventArg == "PostRetrive")
            {
                JsPostRetrive();
            }
            
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            Dw_Main.SaveDataCache();
            Dw_Detail.SaveDataCache();
        }

        private void JsPostRetrive()
        {
            DwUtil.RetrieveDDDW(Dw_Main, "start_group", pbl, null);
            DwUtil.RetrieveDDDW(Dw_Main, "end_group", pbl, null);
            DwUtil.RetrieveDDDW(Dw_Main, "as_smembtype", pbl, null);
            DwUtil.RetrieveDDDW(Dw_Main, "end_group", pbl, null);
        }
       
        private void JsPostRetriveDepttrans()
        {
            //Dw_Detail.InsertRow(0);
           // Dw_Detail.Retrieve();
            String smemtype_code ="", ememtype_code ="";
            String sgroup_type = "", egroup_type = "";
      
            Decimal status_memtype  = Dw_Main.GetItemDecimal(1, "as_statusmem");
            Decimal status_group = Dw_Main.GetItemDecimal(1, "group_status");

            if (status_memtype == 1)
            {
                smemtype_code = Dw_Main.GetItemString(1, "as_smembtype").Trim();
                ememtype_code = Dw_Main.GetItemString(1, "as_emembtype").Trim();

            }
            else {
                String SQLtype = "select min( membtype_code ) as as_smembtype , max( membtype_code ) as as_emembtype  " +
                                    "from mbucfmembtype";
                Sdt memtype_code = WebUtil.QuerySdt(SQLtype);
                if (memtype_code.Next() )
                {
                    smemtype_code = memtype_code.GetString("as_smembtype").Trim();
                    ememtype_code = memtype_code.GetString("as_emembtype").Trim();
                }
            
            }

            if (status_group == 1)
            {
                sgroup_type = Dw_Main.GetItemString(1, "start_group").Trim();
                egroup_type = Dw_Main.GetItemString(1, "end_group").Trim();
            }
            else {

                String SQLtype = "select min( membgroup_code ) as sgroup_type, max( membgroup_code ) as egroup_type from mbucfmembgroup ";
                Sdt memtype_code = WebUtil.QuerySdt(SQLtype);

                if (memtype_code.Next() )
                {
                    sgroup_type = memtype_code.GetString("sgroup_type").Trim();
                    egroup_type = memtype_code.GetString("egroup_type").Trim();
                }
            
            }

            DwUtil.RetrieveDataWindow(Dw_Detail, "dp_depttrans.pbl", null, YYMM, smemtype_code, ememtype_code, sgroup_type, egroup_type);
        }

        private void JsPostCutProcess()
        {
            try
            {
                try
                {
                    strdps.ai_year = Convert.ToInt16(Dw_Main.GetItemDecimal(1, "ai_year"));                   
                    
                }
                catch { strdps.ai_year = 0; }
                try
                {
                    strdps.ai_month = Convert.ToInt16(Dw_Main.GetItemDecimal(1, "ai_month"));
                    
                }
                catch { strdps.ai_month = 0; }
                try
                {
                    strdps.adtm_operatedate = Dw_Main.GetItemDateTime(1, "adtm_operatedate");
                  
                }
                catch { LtServerMessage.Text = WebUtil.ErrorMessage("โปรดระบุวันที่"); }
                try
                {
                    strdps.adtm_pmondate = Dw_Main.GetItemDateTime(1, "adtm_operatedate");
                    tdw_processday.Eng2ThaiAllRow();
                }
                catch { LtServerMessage.Text = WebUtil.ErrorMessage("โปรดระบุวันที่"); }
                try
                {
                    strdps.as_entry_id = state.SsUsername;
                }
                catch { strdps.as_entry_id = ""; }
                try
                {
                    strdps.as_terminal_id = state.SsClientIp;
                }
                catch { strdps.as_terminal_id = ""; }
                try
                {
                    strdps.as_coopid = state.SsCoopId;
                }
                catch { strdps.as_coopid = ""; }
                try
                {
                    strdps.as_memno = Dw_Main.GetItemString(1, "as_memno"); ;
                }
                catch { strdps.as_memno = ""; }
                // try
                //{
                //strdps.group_status = Convert.ToInt16(Dw_Main.GetItemDecimal(1, "group_status"));
                //}catch { }
                // try
                //{
                //strdps.start_group = Dw_Main.GetItemString(1, "start_group");
                //}catch { }
                // try
                //{
                //strdps.end_group = Dw_Main.GetItemString(1, "end_group");
                //}catch { }
                // try
                //{
                //strdps.ai_status_pension = Convert.ToInt16(Dw_Main.GetItemDecimal(1, "ai_status_pension"));
                //}catch { }
                // try
                //{
                //strdps.ai_status_deptacc = Convert.ToInt16(Dw_Main.GetItemDecimal(1, "ai_status_deptacc"));
                //}catch { }
                // try
                //{
                //strdps.as_sdeptaccount = Dw_Main.GetItemString(1, "as_sdeptaccount");
                // }catch { }
                // try
                //{
                //strdps.as_edeptaccount = Dw_Main.GetItemString(1, "as_edeptaccount");
                //}catch { }

                //int result = dpService.of_procdeptupmonth(state.SsWsPass, state.CurrentPage, state.SsApplication,ref strdps);
                //strdps.adtm_tdate = Dw_Main.GetItemString(1, "adtm_tdate");
                string option_xml = Dw_Main.Describe("DataWindow.Data.XML");
                outputProcess = WebUtil.runProcessing(state, "DPUPMONTH", option_xml, state.SsClientIp, "");

                Dw_Main.Reset();
                Dw_Detail.Reset();
                LtServerMessage.Text = WebUtil.CompleteMessage("ประมวลผลสำเร็จ");
            }
            catch(Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            //try
            //{
            //   //// DateTime ProcessDate = new DateTime(1370, 1, 1);
            //   // try
            //   // {
            //   //     ProcessDate = state.SsWorkDate;//Dw_Main.GetItemDateTime(1, "adtm_operatedate",state.);
            //   // }
            //   // catch { LtServerMessage.Text = WebUtil.ErrorMessage("โปรดระบุวันที่"); }

            //    try
            //    {
            //        int result = dpService.PostCutProcess(state.SsWsPass, state.CurrentPage, state.SsApplication, strdps);
            //        HdMountCut.Value = "true";
            //        HdSkipError.Value = "false";
            //    }
            //    catch (Exception ex)
            //    {
            //        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            //}


        }
    }
}
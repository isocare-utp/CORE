using System;
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
using Saving.WcfCommon;
using DataLibrary;
using Sybase.DataWindow;
using Saving.WcfDeposit;
using System.Web.Services.Protocols;
using Saving.ConstantConfig;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_edit_int : PageWebSheet, WebSheet
    {
       
        private String deptAccountNo = null;
        private DepositClient depService;       
        private DepositConfig depConfig;
        private bool IsAutoDeptWith
        {
            get
            {
                try
                {
                    return Session["is_auto_deptwith"].ToString().ToLower() == "true";
                }
                catch { return false; }
            }
            set
            {
                Session["is_auto_deptwith"] = value;
            }
        }


        //POSTBACK
        protected String postPost;
        protected String postNewAccount;
        protected String newClear;
        protected String postCalint;     

        /// <summary>
        /// process save diffent of int in fixed deptaccount
        /// 
        /// </summary>
        private void SaveSheet()
        {
            String accNo = HdNewAccountNo.Value;
            String ls_xml_main = DwMain.Describe("DataWindow.Data.XML");
           
            String ls_xml_item = "";
            String cashType = DwMain.GetItemString(1, "cash_type");
            String as_apvdoc = Hdas_apvdoc.Value;
            ls_xml_item = DwItem.Describe("DataWindow.Data.XML");
           
            int result = depService.of_save_genint_masdue(state.SsWsPass,accNo, ls_xml_main);
            JsNewClear();
            String endMessage = "ทำรายดอกเการบัญชี " + depConfig.CnvDeptAccountFormat(deptAccountNo) + " เรียบร้อยแล้ว";
            Hdas_apvdoc.Value = "";
        }


      

        //JS
        private void JsNewClear()
        {
            DwMain.Reset();
            DwItem.Reset();
            DwItem.SaveDataCache();
            DwMain.InsertRow(0);
          //  tDwMain.Eng2ThaiAllRow();
            DwMain.SaveDataCache();            
            HdIsPostBack.Value = "false";

        }

        //JS-EVENT
        /// <summary>
        /// init deptaccount 
        /// </summary>
        private void JsNewAccountNo()
        {
            String accNo = null;
            String ls_xml_int = null;
       //     DwItem.Reset();
            try
            {
                
           //     accNo = DwMain.GetItemString(1, "deptaccount_no");
                accNo = HdNewAccountNo.Value;
                accNo = depService.BaseFormatAccountNo(state.SsWsPass, accNo);
                DwUtil.RetrieveDataWindow(DwMain, "dept_genintmasdue.pbl", null,accNo, state.SsCoopId );
                DwUtil.RetrieveDataWindow(DwItem, "dept_genintmasdue.pbl", null, state.SsCoopId, accNo);
        //        accNo = depService.BaseFormatAccountNo(state.SsWsPass, accNo);
            }
            catch
            {
                this.deptAccountNo = null;
                return;
            }
      
            this.deptAccountNo = accNo;
        }

    
        //JS-EVENT
        private void JsPostCalint()
        {
            int res = 0;
            try
            {
                String accNo = DwMain.GetItemString(1, "deptaccount_no");
                
                String xmlDwItem = DwItem.Describe("DataWindow.Data.XML");
                res = depService.of_genint_masdue_new(state.SsWsPass,accNo,ref xmlDwItem);
                DwItem.Reset();
                DwItem.ImportString(xmlDwItem, FileSaveAsType.Xml);
                Hdoldint.Value = (DwItem.GetItemDecimal(25, "prnc_amt").ToString("F2"))+"";
                Hdnewint.Value = (DwItem.GetItemDecimal(1, "compute_4"))+"";

             }   
    
            catch (Exception ex)
            {
                ex.ToString();
            }
            // ย้ายจุด focus ไปฟิวอื่น
            this.SetFocusByClientId("ctl00_Image4", this.GetType());
        }

       
        #region WebSheet Members

        public void InitJsPostBack()
        {
            //----------------------------------------------------------------------
            //----------------------------------------------------------------------
            postNewAccount = WebUtil.JsPostBack(this, "postNewAccount");          
            newClear = WebUtil.JsPostBack(this, "newClear");
            postCalint = WebUtil.JsPostBack(this, "postCalint");
           
        }

        public void WebSheetLoadBegin()
        {
           
            try
            {
                depService = wcf.Deposit;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            depConfig = new DepositConfig(depService);
       
            //---------------------------------------------------------------------
            if (IsPostBack)
            {
                try
                {
                    this.RestoreContextDw(DwMain);                    
                    this.RestoreContextDw(DwItem);                   
                }
                catch { }
               
            }
            
            if (DwMain.RowCount < 1)
            {
                DwMain.InsertRow(0);    
                DwMain.SetItemString(1, "deptcoop_id", state.SsCoopId);
               
            }
           
        }

        public void CheckJsPostBack(string eventArg)
        {           
           
           if (eventArg == "postNewAccount")
            {
                JsNewAccountNo();
            }
            
           else if (eventArg == "postCalint")
            {
                JsPostCalint();
            }
          
        }
        /// <summary>
        /// process save websheet  
        /// </summary>
        public void SaveWebSheet()
        {            
            try
            {
                
                deptAccountNo = DwMain.GetItemString(1, "deptaccount_no");
                String slipXml = DwItem.Describe("datawindow.data.xml");                
                String deptcoop_id = HfCoopid.Value;
                String as_apvdoc = Hdas_apvdoc.Value;
                //String SHdas_apvdoc = WebUtil.Right(as_apvdoc, 10);
                /// HARD
                int res = depService.of_save_genint_masdue(state.SsWsPass, deptAccountNo, slipXml);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                JsNewClear();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }
            HdIsPostBack.Value = "false";
                        
        }     


        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwItem.SaveDataCache();

            //เช็ค deptAccountNo
            try
            {
                deptAccountNo = DwMain.GetItemString(1, "deptaccount_no");
                deptAccountNo = string.IsNullOrEmpty(deptAccountNo) ? null : deptAccountNo; 
            }
            catch { deptAccountNo = null; }          
        }

       

        #endregion



    }



}
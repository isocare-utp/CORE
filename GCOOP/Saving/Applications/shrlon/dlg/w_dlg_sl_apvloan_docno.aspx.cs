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
using Sybase.DataWindow;
using DataLibrary;
namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_apvloan_docno : PageWebDialog,WebDialog
    {
        public String pbl = "sl_approve_loan.pbl";
        
        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
            try 
            {
                this.ConnectSQLCA();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
           

            if (!IsPostBack)
            {
                DwUtil.RetrieveDataWindow(Dw_main, pbl, null, state.SsCoopId);
            }
            else
            {
                this.RestoreContextDw(Dw_main);
            }
        }



        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {
            Dw_main.SaveDataCache();
        }

        protected void B_save_Click(object sender, EventArgs e)
        {
            try 
            {
                //DwUtil.UpdateDataWindow(Dw_main, pbl, "cmdocumentcontrol");

                string ls_sqlup = "";
                string documentcode = "", document_prev = "";
                string coop_id = state.SsCoopId ;
                Double ll_lastdocno = 0;
                document_prev = "";
               
                for (int i = 1; i <= Dw_main.RowCount; i++)
                {
                    documentcode = Dw_main.GetItemString(i, "document_code");
                    ll_lastdocno = Dw_main.GetItemDouble(i, "last_documentno");
                    if (document_prev != documentcode)
                    {
                        ls_sqlup = " Update cmdocumentcontrol set last_documentno = " + ll_lastdocno.ToString() + " where document_code = '" + documentcode + "' and coop_id = '" + coop_id + "'";
                        try
                        {
                            WebUtil.Query(ls_sqlup);
                        }
                        catch( Exception ex) {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        }
                    }
                    document_prev = documentcode;
                }

              //  Dw_main.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อย");
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
        protected void B_getdocno_Click(object sender, EventArgs e)
        {
            try
            {
               
                //select  a.document_code, max( a.document_code || substr(  b.loancontract_no,9,2)||' ' || substr( b.loancontract_no,4,5) ) from lncontmaster b , lnloantype a 
                //where a.loantype_code = b.loantype_code and  a.coop_id = b.coop_id and a.coop_id = '001001'  
                //group by a.document_code  ;

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
    }
}
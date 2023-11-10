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
using System.Data.OracleClient;
using Sybase.DataWindow;
using System.Globalization;
using CoreSavingLibrary.WcfNAccount;

namespace Saving.Applications.account
{
    public partial class w_dlg_vc_generate_wizard : PageWebSheet, WebSheet
    {
        private CultureInfo th;
        private DwThDate tdw_wizard;
        private n_accountClient accService;
        protected String postNext;

        //===============================
        private void JspostBack()
        {
            Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vcdate";
            Dw_wizard.Reset();
            Dw_wizard.InsertRow(0);
            
            tdw_wizard.Eng2ThaiAllRow();


        }
        private  void JspostChangeDw()
        {
           switch (Dw_wizard.GetItemString(1,"systemgen_code").Trim())
          
            {
                case "CSH":
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vctype_cash";
                    break;
                case "LON":
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vctype_loan";
                    break;
                case "SHR":
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vctype_share";
                    break;
                case "DEP":
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vctype_dept";
                    break;
                case "FIN":
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vctype_fin";
                    break;
                case "LAN":
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vctype_loan_iv";
                    break;
                case "AST":
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vctype_ast";
                    break;
                case "HRM":
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vctype_hrm";
                    break;
                case "KEP":
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vctype_dept_kep";
                    break;
                case "TRL":
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vctype_loan_trn";
                    break;
                case "TRS":
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_vctype_shr_trn";
                    break;
                case ("ALL"):
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_finish";
                    break;
                case ("ANC"):
                    Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_finish";
                    break;
            }
            Dw_wizard.Reset();
            Dw_wizard.InsertRow(0);
        }

        //===============================

        #region WebSheet Members
      

        public void InitJsPostBack()
        {
          


            tdw_wizard = new DwThDate(Dw_wizard,this);
            tdw_wizard.Add("voucher_date", "voucher_tdate");
        }

        public void WebSheetLoadBegin()
        {

            try
            {
                accService = wcf.NAccount;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Webservice ไม่ได้");
                return;
            }

            this.ConnectSQLCA();
            th = new CultureInfo("th-TH");

            Dw_wizard.SetTransaction(sqlca);
            if (!IsPostBack) {
                Dw_wizard.InsertRow(0);
                //Dw_wizard.SetItemString(1, "coop_id", state.SsCoopControl);
                //Dw_wizard.SetItemString(1, "branch_id", state.SsCoopId);
                Dw_wizard.SetItemString(1, "systemgen_code", "ALL");
                Dw_wizard.SetItemDate(1, "voucher_date", state.SsWorkDate);
                Dw_wizard.SetItemString(1,"voucher_tdate",state.SsWorkDate.ToString("ddMMyyyy", new CultureInfo("th-TH")));

                tdw_wizard.Eng2ThaiAllRow();

                //B_back.Enabled = false;
            } else {
                Dw_wizard.RestoreContext();
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
           
            
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion

        protected void B_next_Click(object sender, EventArgs e)
        {
           // JspostChangeDw();
            try
            {
                B_next.Visible = false;
                String systemgen_code = Dw_wizard.GetItemString(1, "systemgen_code");
                DateTime voucher_date = Dw_wizard.GetItemDateTime(1, "voucher_date");
                if (state.SsCoopControl == "008001")  //กฟภ.
                {
                    int result = accService.of_vcproc_pea(state.SsWsPass, voucher_date, systemgen_code, state.SsCoopControl, state.SsUsername);
                    if (result == 1) { Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_finish"; }
                }
                else if (state.SsCoopControl == "003001")  // แม่ฮ่องสอน
                {
                    int result = accService.of_vcproc_mhs(state.SsWsPass, voucher_date, systemgen_code, state.SsCoopControl, state.SsUsername);
                    if (result == 1) { Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_finish"; }
                }
                else if (state.SsCoopControl == "024001")  // โรงพยาบาลแพร่ 
                {
                    int result = accService.of_vcproc_mhs(state.SsWsPass, voucher_date, systemgen_code, state.SsCoopControl, state.SsUsername);
                    if (result == 1) { Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_finish"; }
                }
                else if (state.SsCoopControl == "018001")  // น่าน
                {
                    int result = accService.of_vcproc_nan(state.SsWsPass, voucher_date, systemgen_code, state.SsCoopControl, state.SsUsername);
                    if (result == 1) { Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_finish"; }
                }
                else if (state.SsCoopControl == "027001")  // ลำปาง
                {
                    int result = accService.of_vcproc_lap(state.SsWsPass, voucher_date, systemgen_code, state.SsCoopControl, state.SsUsername);
                    if (result == 1) { Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_finish"; }
                }

                else if (state.SsCoopControl == "022001")  // การทางพิเศษ
                {
                    int result = accService.of_vcproc_exat(state.SsWsPass, voucher_date, systemgen_code, state.SsCoopControl, state.SsUsername);
                    if (result == 1) { Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_finish"; }
                }
                else if (state.SsCoopControl == "028001")  // ครูราชบุรี
                {
                    int result = accService.of_vcproc_rbt(state.SsWsPass, voucher_date, systemgen_code, state.SsCoopControl, state.SsUsername);
                    if (result == 1) { Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_finish"; }
                }
                else if (state.SsCoopControl == "040001")  // สอ.กระทรวงตาก
                {
                    int result = accService.of_vcproc_stk(state.SsWsPass, voucher_date, systemgen_code, state.SsCoopControl, state.SsUsername);
                    if (result == 1) { Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_finish"; }
                }
                else
                {
                    int result = accService.of_vcproc(state.SsWsPass, voucher_date, systemgen_code, state.SsCoopControl, state.SsUsername);
                    if (result == 1) { Dw_wizard.DataWindowObject = "d_vc_vcgen_wizard_finish"; }
                }
                //int result = wcf.NAccount.of_vcproc(state.SsWsPass, voucher_date, systemgen_code, state.SsCoopControl, state.SsUsername);
                
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        protected void B_back_Click(object sender, EventArgs e)
        {
            JspostBack();
        }
    }
}

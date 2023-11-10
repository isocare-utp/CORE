using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;
using System.Xml;
using System.Xml.Xsl;
using DataLibrary;
using System.Text;


namespace Saving.Applications.shrlon.w_sheet_ln_collredeem_pawn_ctrl
{
    public partial class w_sheet_ln_collredeem_pawn : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string UpdateMortgage { get; set; }
       
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].REDEEM_DATE = state.SsWorkDate;                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == UpdateMortgage) 
            {
                string mrtgmast_no = dsMain.DATA[0].MRTGMAST_NO;
                dsMain.Retrieve(mrtgmast_no);
                dsMain.DATA[0].REDEEM_DATE = state.SsWorkDate;   
            }
        }

        public void SaveWebSheet()
        {
            Sta ta = new Sta(state.SsConnectionString);
            try
            {
                DateTime redeem_date = dsMain.DATA[0].REDEEM_DATE;
                string mrtgmast_no = dsMain.DATA[0].MRTGMAST_NO;                

                string sql = "update lnmrtgmaster set redeem_date = {0}, mrtgmast_status = '1' where mrtgmast_no = {1} ";
                sql = WebUtil.SQLFormat(sql, redeem_date, mrtgmast_no);
                Sdt dt = WebUtil.QuerySdt(sql);
                

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                dsMain.ResetRow();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            ta.Close();
        }

        public void WebSheetLoadEnd()
        {
        }      
    }
}
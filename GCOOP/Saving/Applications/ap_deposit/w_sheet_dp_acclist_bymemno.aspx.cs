using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_acclist_bymemno : PageWebSheet, WebSheet
    {
        public String pbl = "dp_acclist_bymemno.pbl";
        public String mem_no;

        //====== DwMain
        protected String postShowMember;
        protected String postSelectFlag;
        protected String postShowDetail;
        private DwThDate tdw_detail3;
        private DwThDate tdw_detail1;

        private void JspostShowDetail()
        {
            try
            {
                mem_no = HdMembno.Value;
                String deptaccount_no = Hd_deptaccountno.Value;
                dw_detail1.Reset();
                dw_detail2.Reset();
                dw_detail3.Reset();
                
                DwUtil.RetrieveDataWindow(dw_detail1, pbl, null, deptaccount_no, mem_no);
                DwUtil.RetrieveDataWindow(dw_detail2, pbl, null, deptaccount_no, mem_no);
                DwUtil.RetrieveDataWindow(dw_detail3, pbl, null, deptaccount_no, mem_no);
                

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void InitJsPostBack()
        {
            tdw_detail3 = new DwThDate(dw_detail3, this);
            tdw_detail1 = new DwThDate(dw_detail1, this);
            tdw_detail3.Add("prncdue_date", "prncdue_tdate");//deptopen_tdate
            tdw_detail3.Add("prnc_date", "prnc_tdate");
            tdw_detail1.Add("deptopen_date", "deptopen_tdate");
            postSelectFlag = WebUtil.JsPostBack(this, "postSelectFlag");
            postShowMember = WebUtil.JsPostBack(this, "postShowMember");
            postShowDetail = WebUtil.JsPostBack(this, "postShowDetail");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                LoadBegin();
            }
            else 
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwAccdeptList);
                this.RestoreContextDw(dw_detail1);
                this.RestoreContextDw(dw_detail2);
                this.RestoreContextDw(dw_detail3);
                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postSelectFlag":
                    PostSelectFlag();
                    break;
                case "postShowMember":
                    PostShowMember();
                    break;
                case "postShowDetail":
                    JspostShowDetail();
                    break;
            }
            
        }
        

        public void SaveWebSheet()
        {
            tdw_detail3.Eng2ThaiAllRow();
            DwUtil.UpdateDataWindow(dw_detail3, pbl, "DPDEPTPRNCFIXED");
        }

        public void WebSheetLoadEnd()
        {
            tdw_detail3.Eng2ThaiAllRow();
            tdw_detail1.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
            DwAccdeptList.SaveDataCache();
            dw_detail1.SaveDataCache();
            dw_detail2.SaveDataCache();
            dw_detail3.SaveDataCache();       
        }

        private void LoadBegin()
        {
            DwMain.InsertRow(0);
        }


        private void PostShowMember()
        {
            String member_no = "";
            try
            {
                member_no = WebUtil.MemberNoFormat(HdMembno.Value);
                for (int i = member_no.Length; i < 6; i++) {
                    member_no = "0" + member_no;
                }
                DwUtil.RetrieveDataWindow(DwMain, pbl, null, member_no);
                DwUtil.RetrieveDataWindow(DwAccdeptList, pbl, null, member_no);
                DwMain.SetItemString(1, "member_no", member_no);
                HdMembno.Value = member_no;

                try
                {
                    dw_detail1.Reset();
                    dw_detail2.Reset();
                    dw_detail3.Reset();
                    Hd_deptaccountno.Value = DwAccdeptList.GetItemString(1, "deptaccount_no");
                    JspostShowDetail();
                }
                catch
                {

                }
                //DwMain.SetItemDecimal(1, "select_flag", 1);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void PostSelectFlag()
        {
            try
            {
                decimal select_flag = DwMain.GetItemDecimal(1, "select_flag");
                if (select_flag == 1)
                {
                    DwMain.SetItemDecimal(1, "select_flag", 1);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }

    }
}
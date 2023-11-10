using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNDeposit;
using System.Globalization;
using System.IO;
using System.Text;
using DataLibrary;



namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_atm_memberdetail : PageWebSheet, WebSheet
    {
        protected String jsRetive;
        protected String jsinitAccNo;
        protected String postPost;
        protected String postProvince;
        protected String other_postPost;
        protected String other_postProvince;
        protected String jsAddRelateRow;
        protected String jsDeleteRelateRow;
        private String pbl = "dp_atm_memberdetail.pbl";

        private DwThDate tDwMain;

        public void InitJsPostBack()
        {
            jsRetive = WebUtil.JsPostBack(this, "jsRetive");
            jsinitAccNo = WebUtil.JsPostBack(this, "jsinitAccNo");
            postPost = WebUtil.JsPostBack(this, "postPost");
            postProvince = WebUtil.JsPostBack(this, "postProvince");
            other_postPost = WebUtil.JsPostBack(this, "other_postPost");
            other_postProvince = WebUtil.JsPostBack(this, "other_postProvince");
            jsAddRelateRow = WebUtil.JsPostBack(this, "jsAddRelateRow");
            jsDeleteRelateRow = WebUtil.JsPostBack(this, "jsDeleteRelateRow");

            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("apply_date", "apply_tdate");
            tDwMain.Add("wfbirthday_date", "wfbirthday_tdate");
            tDwMain.Add("deptopen_date", "deptopen_tdate");
            tDwMain.Add("deptclose_date", "deptclose_tdate");
            tDwMain.Add("die_date", "die_tdate");
            tDwMain.Add("effective_date", "effective_tdate");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);

                //  tDwMain.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwAtmdept);
                this.RestoreContextDw(DwAtmloan);
                this.RestoreContextDw(DwAtmtrans);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {

                case "jsRetive":
                    jsReTive();
                    break;

            }
        }

        public void SaveWebSheet()
        {
            //if (state.SsUserType == 2 && state.SsCsType == "8")
            //{
            //     LtServerMessage.Text = WebUtil.ErrorMessage("คุณไม่มีสิทธิในการแก้ไขข้อมูล กรุณาติดต่อ สมาคม");
            //     return;
            //}
            //try
            //{
            //    string tdeptopen_date = DwMain.GetItemString(1, "apply_tdate");
            //    if (tdeptopen_date.Length == 8)
            //    {
            //        DateTime dt = DateTime.ParseExact(tdeptopen_date, "ddMMyyyy", WebUtil.TH);
            //        DwMain.SetItemDateTime(1, "apply_date", dt);
            //    }
            //}
            //catch { }
            //try
            //{
            //    string tbirth_date = DwMain.GetItemString(1, "wfbirthday_tdate");
            //    if (tbirth_date.Length == 8)
            //    {
            //        DateTime dt = DateTime.ParseExact(tbirth_date, "ddMMyyyy", WebUtil.TH);
            //        DwMain.SetItemDateTime(1, "wfbirthday_date", dt);
            //    }
            //}
            //catch { }
            //try
            //{
            //    string tdeptopen_date = DwMain.GetItemString(1, "deptopen_tdate");
            //    if (tdeptopen_date.Length == 8)
            //    {
            //        DateTime dt = DateTime.ParseExact(tdeptopen_date, "ddMMyyyy", WebUtil.TH);
            //        DwMain.SetItemDateTime(1, "deptopen_date", dt);
            //    }
            //}
            //catch { }
            //try
            //{
            //    string tdeptclose_date = DwMain.GetItemString(1, "deptclose_tdate");
            //    if (tdeptclose_date.Length == 8)
            //    {
            //        DateTime dt = DateTime.ParseExact(tdeptclose_date, "ddMMyyyy", WebUtil.TH);
            //        DwMain.SetItemDateTime(1, "deptclose_date", dt);
            //    }
            //}
            //catch { }
            //try
            //{
            //    string tdie_date = DwMain.GetItemString(1, "die_tdate");
            //    if (tdie_date.Length == 8)
            //    {
            //        DateTime dt = DateTime.ParseExact(tdie_date, "ddMMyyyy", WebUtil.TH);
            //        DwMain.SetItemDateTime(1, "die_date", dt);
            //    }
            //}
            //catch { }
            //try
            //{
            //    string teffective_date = DwMain.GetItemString(1, "effective_tdate");
            //    if (teffective_date.Length == 8)
            //    {
            //        DateTime dt = DateTime.ParseExact(teffective_date, "ddMMyyyy", WebUtil.TH);
            //        DwMain.SetItemDateTime(1, "effective_date", dt);
            //    }
            //}
            //catch { }

            /////chec สถานะสมาชิก
            //try
            //{
            //    int wfmember_status = Convert.ToInt32(DwMain.GetItemDecimal(1, "wfmember_status"));

            //    if (wfmember_status == -1 && state.SsUserType != 1)
            //    {
            //        try
            //        {
            //            string resigncause_code = DwMain.GetItemString(1, "resigncause_code");
            //            switch (resigncause_code)
            //            {
            //                case "01":
            //                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกได้ สมาชิกคนนี้ได้ทำการแจ้งลาออกแล้ว");
            //                    break;
            //                case "02":
            //                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกได้ สมาชิกคนนี้ได้ทำการแจ้งเสียชีวิตแล้ว");
            //                    break;
            //                case "03":
            //                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกได้ สมาชิกคนนี้ถูกยกเลิกไปแล้ว");
            //                    break;
            //                case "04":
            //                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกได้ สมาชิกคนนี้สิ้นสุดสมาชิกภาพไปแล้ว");
            //                    break;
            //            }
            //        }
            //        catch
            //        { }
            //    }
            //    else
            //    {
            //        string prename_code = DwMain.GetItemString(1, "prename_code");
            //        if (prename_code == "00")
            //        {
            //            LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุคำนำหน้า");
            //            return;
            //        }
            //        string prename_desc = "";
            //        String sqlpre = "select prename_desc from mbucfprename where prename_code='" + prename_code + "'";

            //        ///format member_no
            //        //string member_no = DwMain.GetItemString(1, "member_no").Trim();
            //        //member_no = int.Parse(member_no).ToString("000000");
            //        //DwMain.SetItemString(1, "member_no", member_no);

            //        try{
            //            string membNO;
            //            if ((state.SsCsType == "6" && DwMain.GetItemString(1, "wftype_code") == "02") || (state.SsCsType == "1" && (DwMain.GetItemString(1, "wftype_code") == "12" || DwMain.GetItemString(1, "wftype_code") == "03" || DwMain.GetItemString(1, "wftype_code") == "08")) || (state.SsCsType == "8" && (DwMain.GetItemString(1, "wftype_code") == "03" || DwMain.GetItemString(1, "wftype_code") == "05" || DwMain.GetItemString(1, "wftype_code") == "06")))
            //            {
            //                if (state.SsBranchId != "0084")
            //                {
            //                    membNO = "ส" + int.Parse(DwMain.GetItemString(1, "member_no")).ToString("00000");
            //                }
            //                else
            //                {
            //                    membNO = "ส" + DwMain.GetItemString(1, "member_no");
            //                }
            //            }
            //            else if (state.SsCsType == "1" && DwMain.GetItemString(1, "wftype_code") == "06")
            //            {
            //                membNO = "บ" + int.Parse(DwMain.GetItemString(1, "member_no")).ToString("00000"); ;
            //            }
            //            else if (state.SsCsType == "1" && DwMain.GetItemString(1, "wftype_code") == "13")
            //            {
            //                membNO = "ม" + int.Parse(DwMain.GetItemString(1, "member_no")).ToString("00000"); ;
            //            }
            //            else if (state.SsCsType == "6" && DwMain.GetItemString(1, "wftype_code") == "03")
            //            {
            //                membNO = "สF" + int.Parse(DwMain.GetItemString(1, "member_no")).ToString("0000"); ;
            //            }
            //            else if (state.SsCsType == "6" && DwMain.GetItemString(1, "wftype_code") == "04")
            //            {
            //                membNO = "สM" + int.Parse(DwMain.GetItemString(1, "member_no")).ToString("0000"); ;
            //            }
            //            else if (state.SsCsType == "6" && DwMain.GetItemString(1, "wftype_code") == "05")
            //            {
            //                membNO = "สS" + int.Parse(DwMain.GetItemString(1, "member_no")).ToString("0000"); ;
            //            }
            //            else if (state.SsBranchId == "0270")
            //            {
            //                membNO = int.Parse(DwMain.GetItemString(1, "member_no")).ToString("0000000"); ;
            //            }
            //            else
            //            {
            //                membNO = WebUtil.MemberNoFormat(DwMain.GetItemString(1, "member_no").Trim());
            //            }
            //            DwMain.SetItemString(1, "member_no", membNO);
            //        }
            //        catch { }

            //        ///check Data
            //        CheckData();

            //        Sdt dtable = WebUtil.QuerySdt(sqlpre);
            //        if (dtable.Next())
            //        {
            //            prename_desc = dtable.GetString("prename_desc");
            //        }
            //        string fullname = prename_desc + "" + DwMain.GetItemString(1, "deptaccount_name") + "  " + DwMain.GetItemString(1, "deptaccount_sname");
            //        DwMain.SetItemString(1, "wfaccount_name", fullname);

            //        String DWseq_no = "", Codept_CPS;
            //        int rowCount = DwCodept.RowCount;
            //        decimal foreigner_flag;
            //        for (int i = 0; i < rowCount; i++)
            //        {
            //            try
            //            {
            //                DwCodept.GetItemString(i + 1, "name");
            //                DWseq_no = DWseq_no + "'" + DwCodept.GetItemDecimal(i + 1, "seq_no") + "' ";
            //                if (i != (rowCount - 1))
            //                {
            //                    DWseq_no = DWseq_no + ",";
            //                }
            //                try
            //                {
            //                    Codept_CPS = DwCodept.GetItemString(i + 1, "codept_id");
            //                }
            //                catch
            //                {
            //                    Codept_CPS = "";
            //                }
            //                if ((state.SsCsType != "1") && (state.SsCsType != "6") && (state.SsCsType != "8") && (state.SsCsType != "2"))
            //                {
            //                    foreigner_flag = DwCodept.GetItemDecimal(i + 1, "foreigner_flag");
            //                    if (foreigner_flag == 0)
            //                    {
            //                        bool Chk_CodeptCPS = VerifyPeopleID(Codept_CPS);
            //                        if (!Chk_CodeptCPS)
            //                        {
            //                            LtServerMessage.Text = WebUtil.ErrorMessage("บัตรประชาชนผู้รับผลประโยชน์คนที่ " + (i + 1) + " ไม่ถูกต้อง");
            //                            return;
            //                        }
            //                    }
            //                }
            //            }
            //            catch
            //            {
            //                for (int j = DwCodept.RowCount; j >= (i + 1); j--)
            //                {
            //                    DwCodept.DeleteRow(j);
            //                }
            //                break;
            //            }
            //        }

            //        rowCount = DwCodept.RowCount;
            //        if (DWseq_no == "") DWseq_no = "0";
            //        DWseq_no = DWseq_no.Remove(DWseq_no.Length - 1);
            //        String SqlDel = "delete from wccodeposit where  deptaccount_no = '" + Session["deptaccount_no"].ToString() + "'";
            //        WebUtil.QuerySdt(SqlDel);

            //        String deptaccount_no = Session["deptaccount_no"].ToString();
            //        for (int k = 1; k <= DwCodept.RowCount; k++)
            //        {
            //            DwCodept.SetItemDecimal(k, "seq_no", k);
            //            DwCodept.SetItemString(k, "deptaccount_no", deptaccount_no);
            //            DwCodept.SetItemString(k, "branch_id", state.SsBranchId);
            //        }                    
            //        try
            //        {
            //            int[] rows = new int[DwCodept.RowCount];
            //            int ii = 0;
            //            for (int i = 1; i <= DwCodept.RowCount; i++)
            //            {
            //                rows[ii] = i;
            //                ii++;
            //            }
            //            DwUtil.InsertDataWindow(DwCodept, pbl, "wccodeposit", rows);
            //            Hdrowcount.Value = Convert.ToString(DwCodept.RowCount);
            //            int Def_RowCodet = 10 - DwCodept.RowCount;
            //            for (int i = 1; i <= Def_RowCodet; i++)
            //            {
            //                DwCodept.InsertRow(0);
            //                DwCodept.SetItemDecimal(i, "seq_no", i + Def_RowCodet);
            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            //            return;
            //        }
            //        try
            //        {
            //            DwMain.SetItemString(1, "deptaccount_no", Session["deptaccount_no"].ToString());
            //            String XmlCompare = DwMain.Describe("DataWindow.Data.XML");
            //            String XmlMain = Session["xml_before_audit"].ToString();
            //            string[] colid = new string[2];
            //            colid[0] = "101";
            //            colid[1] = "200";
            //            int resu = WsUtil.Walfare.SvAuditEdit(state.SsWsPass, state.SsApplication, pbl, XmlMain, XmlCompare, state.SsUsername, state.SsBranchId, state.SsCsType, "d_dp_dept_edit_master", colid);
            //            Session["xml_before_audit"] = null;

            //            if (resu == 1)
            //            {
            //                DwUtil.UpdateDateWindow(DwMain, pbl, "wcdeptmaster");
            //                DwUtil.UpdateDateWindow(DwCodept, pbl, "wccodeposit");
            //                Session["deptaccount_no"] = null;
            //                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            //                DwMain.Reset();
            //                DwMain.InsertRow(0);
            //                DwCodept.Reset();
            //                DwStment.Reset();
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้" + ex);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            //}

        }

        public void WebSheetLoadEnd()
        {
            try
            {

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

            DwMain.SaveDataCache();
            DwAtmdept.SaveDataCache();
            DwAtmloan.SaveDataCache();
            DwAtmtrans.SaveDataCache();

        }

        private void jsReTive()
        {
            string member_no = DwMain.GetItemString(1, "atmmember_member_no");
            try
            {
                DwUtil.RetrieveDataWindow(DwMain, pbl, null, member_no);
                member_no = DwMain.GetItemString(1, "atmmember_member_no");
                string coop_id = DwMain.GetItemString(1, "atmmember_coop_id");
                DwUtil.RetrieveDataWindow(DwAtmdept, pbl, null, coop_id, member_no);
                DwUtil.RetrieveDataWindow(DwAtmloan, pbl, null, coop_id, member_no);
                DwUtil.RetrieveDataWindow(DwAtmtrans, pbl, null, coop_id, member_no);
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล เลชทะเบียน " + member_no);
                DwMain.Reset();
                DwMain.InsertRow(0);
                WebSheetLoadEnd();
            }
        }

        private void CheckData()
        {
            try
            {
                string member_no = DwMain.GetItemString(1, "member_no");
                if (string.IsNullOrEmpty(member_no)) throw new Exception("กรุณากรอกเลขสมาชิกสหกรณ์");
            }
            catch
            {
                throw new Exception("กรุณากรอกเลขสมาชิกสหกรณ์");
            }
            try
            {
                string deptaccount_name = DwMain.GetItemString(1, "deptaccount_name");
                if (string.IsNullOrEmpty(deptaccount_name)) throw new Exception("กรุณากรอกชื่อผู้สมัคร");
            }
            catch
            {
                throw new Exception("กรุณากรอกชื่อผู้สมัคร");
            }
            try
            {
                string deptaccount_sname = DwMain.GetItemString(1, "deptaccount_sname");
                if (string.IsNullOrEmpty(deptaccount_sname)) throw new Exception("กรุณากรอกนามสกุลผู้สมัคร");
            }
            catch
            {
                throw new Exception("กรุณากรอกนามสกุลผู้สมัคร");
            }
            try
            {
                DateTime wfbirthday_date = DwMain.GetItemDateTime(1, "wfbirthday_date");
            }
            catch
            {
                throw new Exception("กรุณากรอกวันเกิดให้ถูกต้อง ตามรูปแบบ dd/mm/yyyy");
            }

        }
    }
}
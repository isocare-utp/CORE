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
//using CoreSavingLibrary.WcfNDeposit; 
//using CoreSavingLibrary.WcfNCommon;

using CoreSavingLibrary.WcfNDeposit; //new deposit
using CoreSavingLibrary.WcfNCommon; //new common
using System.Web.Services.Protocols;

using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_edit_dept_namethaieng : PageWebSheet, WebSheet
    {
        protected String FilterBankBranch;
        protected String FilterProvince;
        protected String FilterDistrict;
        protected String postAccountNo;
        protected String postAddRow;
        protected String postChangeTran;
        protected String postChangeDwTabCon;
        protected String postExpenseAcc;
        protected String postMemberNo;
        protected String postChangeTranDeptAcc;
        protected String postPostOffice;
        protected String MemberNoSearch;
        //private DepositClient depServ;

        private n_depositClient ndept; //new deposit
        private String memNo;
        private String accNo;
        protected String CheckCoop;
        protected String setCoopname;
        protected String postBankAcc;

        private void JsPostBankAcc()
        {
            String postExAcc = DwTabAtm.GetItemString(1, "bank_accid");
            postExAcc = ndept.of_analizeaccno(state.SsWsPass, postExAcc);            
            DwTabAtm.SetItemString(1, "bank_accid", postExAcc);
        }
        private void JsPostExpenseAcc()
        {
            String postExAcc = DwTabTran.GetItemString(1, "expense_accno");
            postExAcc = ndept.of_analizeaccno(state.SsWsPass, postExAcc);
            //postExAcc = depServ.BaseFormatAccountNo(state.SsWsPass, postExAcc);
            DwTabTran.SetItemString(1, "expense_accno", postExAcc);
        }

        private void JsPostAddRow()
        {
            int seqNo = 0;
            int rowNew = DwTabEditCo.RowCount + 1;
            DwTabEditCo.InsertRow(rowNew);
            String deptAcc = DwMain.GetItemString(1, "deptaccount_no");
            //deptAcc = depServ.BaseFormatAccountNo(state.SsWsPass, deptAcc);
            deptAcc = ndept.of_analizeaccno(state.SsWsPass, deptAcc);

            for (int i = 1; i <= DwTabEditCo.RowCount; i++)
            {
                seqNo++;
                DwTabEditCo.SetItemDecimal(i, "seq_no", seqNo);
                DwTabEditCo.SetItemString(i, "deptaccount_no", deptAcc);
                DwTabEditCo.SetItemString(i, "coop_id", state.SsCoopControl);
            }
        }

        private void JspostChangeTran()
        {
            string expenseCode;
            expenseCode = DwTabTran.GetItemString(1, "expense_code");

            String dwAccNo = DwMain.GetItemString(1, "deptaccount_no");
            //accNo = depServ.BaseFormatAccountNo(state.SsWsPass, dwAccNo);

            accNo = ndept.of_analizeaccno(state.SsWsPass, dwAccNo);

            if (expenseCode == "TRN")
            {
                DwTabTran.Modify("expense_accno.protect=0");
                DwTabTran.Modify("expense_amount.protect=0");
            }
            else
            {
                DwTabTran.Modify("expense_accno.protect=1");
                DwTabTran.Modify("expense_amount.protect=1");
                DwTabTran.SetItemString(1, "expense_accno", null);
                DwTabTran.SetItemDecimal(1, "expense_amount", 0);
            }

            DwTabTran.SetItemString(1, "deptaccount_no", accNo);
            DwTabTran.SetItemString(1, "coop_id", state.SsCoopControl);
        }

        private void JsPostAccountNo()
        {
            string membName = "";
            string membSurname = "";
            try
            {
                String dwAccNo = DwMain.GetItemString(1, "deptaccount_no");
                //accNo = depServ.BaseFormatAccountNo(state.SsWsPass, dwAccNo);
                //เอา format ออกกรณีที่ไม่ใช่ deptno 10
                //accNo = ndept.of_analizeaccno(state.SsWsPass, dwAccNo);
                //int rowRetrieve = DwMain.Retrieve(accNo, HfCoopid.Value);
                accNo = dwAccNo;
                DwMain.Reset();
                int rowRetrieve = DwMain.Retrieve(accNo, HfCoopid.Value);

                DwTabMem.Reset();
                DwTabAddress.Reset();
                DwTabCond.Reset();

                if (rowRetrieve > 0)
                {
                    DwMain.SetItemString(1, "deptaccount_no", accNo);
                    DwMain.Modify("deptaccount_no.EditMask.Mask='" + WebUtil.GetDeptCodeMask() + "'");

                    memNo = DwMain.GetItemString(1, "member_no");

                    DwTabMem.Retrieve(accNo, HfCoopid.Value);

                    try
                    {
                        membName = DwTabMem.GetItemString(1, "memb_name");
                        membSurname = DwTabMem.GetItemString(1, "memb_surname");
                        DwTabMem.SetItemString(1, "member_name", membName + ' ' + membSurname);
                    }
                    catch (Exception)
                    {
                        membName = "";
                        membSurname = "";
                    }

                    DwTabAddress.Retrieve(memNo, HfCoopid.Value);

                    DwTabEditCo.Retrieve(accNo, HfCoopid.Value);

                    DwTabCond.Retrieve(accNo, HfCoopid.Value);
                    //DwTabCond.SetItemString(1, "deptaccount_no", accNo);
                    //DwTabCond.SetItemString(1, "branch_id", HfCoopid.Value);

                    DwTabTran.Retrieve(accNo, HfCoopid.Value);
                    DwTabAtm.Retrieve(accNo, HfCoopid.Value);
                }
                else
                {
                    DwMain.InsertRow(0);
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีเลขที่บัญชี : " + accNo);
                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsPostPostOffice()
        {
            try
            {
                String postCode = DwTabCond.GetItemString(1, "tran_deptacc_no").Trim();
                String sql = "select membgroup_desc from mbucfmembgroup where membgroup_code = '" + postCode + "'";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    DwTabCond.SetItemString(1, "dept_tranacc_name", dt.Rows[0][0].ToString().Trim());
                }
                else
                {
                    DwTabCond.SetItemString(1, "dept_tranacc_name", "");
                    throw new Exception("ไม่พบรหัสปณ. : " + postCode);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.WarningMessage(ex);
            }
        }

        private void JspostChangeDwTabCon()
        {
            decimal rateStatus = DwTabCond.GetItemDecimal(1, "spcint_rate_status");
            decimal monthIntPay = DwTabCond.GetItemDecimal(1, "monthintpay_meth");
            decimal taxStatus = DwTabCond.GetItemDecimal(1, "taxspcrate_status");
            //rateStatus
            if (rateStatus == 1)
            {
                DwTabCond.Modify("spcint_rate.protect=0");
            }
            else if (rateStatus == 2)
            {
                DwTabCond.SetItemDecimal(1, "spcint_rate", 0);
            }
            else
            {
                DwTabCond.SetItemDecimal(1, "spcint_rate", 0);
            }
            //monthIntPay
            if (monthIntPay == 1)
            {
                DwTabCond.SetItemString(1, "tran_bankacc_no", null);
            }
            else if (monthIntPay == 2)
            {
                DwTabCond.SetItemString(1, "tran_bankacc_no", null);
            }
            else if (monthIntPay == 3)
            {
            }
            else if (monthIntPay == 0)
            {
                DwTabCond.SetItemString(1, "tran_bankacc_no", "");
                DwTabCond.SetItemString(1, "dept_tranacc_name", null);
                DwTabCond.SetItemString(1, "bank_code", null);
                DwTabCond.SetItemString(1, "bank_branch", null);
            }
            //taxStatus
            if (taxStatus == 1)
            {
            }
            else
            {
                DwTabCond.SetItemDecimal(1, "f_tax_rate", 0);
            }
        }

        private void JspostMemberNo()
        {

            string memberNo = DwTabMem.GetItemString(1, "member_no");
            if (memberNo != null && memberNo != "")
            {
                try
                {
                    DataTable dt = WebUtil.Query(
                        "select a.memb_name || ' ' || a.memb_surname as \"fullname\" from mbmembmaster a" + @"
                        where a.member_no = '" + memberNo + "' ");

                    if (dt.Rows.Count > 0)
                    {
                        DwTabMem.SetItemString(1, "member_name", dt.Rows[0]["fullname"].ToString());
                    }
                }

                catch (Exception ex) { ex.ToString(); }

            }
            DwTabAddress.Reset();
            DwTabAddress.Retrieve(memberNo, HfCoopid.Value);
        }

        private void JsFilterBankBranch()
        {
            String bankCode = DwTabCond.GetItemString(1, "bank_code");
            DataWindowChild dc = DwTabCond.GetChild("bank_branch");
            dc.SetTransaction(sqlca);
            dc.Retrieve();
            dc.SetFilter("bank_code = '" + bankCode + "'");
            dc.Filter();
        }

        private void JsFilterProvince()
        {
            int Row = Convert.ToInt16(HdRowEditCo.Value);
            DwTabEditCo.SetItemString(Row, "district", "กรุณาเลือกอำเภอ");
            DwTabEditCo.SetItemString(Row, "tumbol", "กรุณาเลือกตำบล");
            DwTabEditCo.SetItemString(Row, "post_code", "");
            try
            {
                if (HdProvinve.Value != null)
                {

                    String provinceCode = DwTabEditCo.GetItemString(Row, "province");
                    DataWindowChild dc2 = DwTabEditCo.GetChild("district");
                    dc2.SetTransaction(sqlca);
                    dc2.Retrieve();
                    dc2.SetFilter("province_code = '" + provinceCode + "'");
                    dc2.Filter();
                }
            }
            catch (Exception)
            {

            }
        }

        private void JsFilterDistrict()
        {
            int Row = Convert.ToInt16(HdRowEditCo.Value);
            DwTabEditCo.SetItemString(Row, "tumbol", "กรุณาเลือกตำบล");
            //DwTabEditCo.SetItemString(1, "post_code", "");
            try
            {
                if (HdDistrict.Value != null)
                {
                    String districtCode = DwTabEditCo.GetItemString(Row, "district");
                    String province = DwTabEditCo.GetItemString(Row, "province");
                    DataWindowChild dc2 = DwTabEditCo.GetChild("tumbol");
                    dc2.SetTransaction(sqlca);
                    dc2.Retrieve();
                    dc2.SetFilter("district_code = '" + districtCode + "'");
                    dc2.Filter();
                    //String province = DwTabEditCo.GetItemString(1, "district")
                    Sta ta = new Sta(state.SsConnectionString);
                    String sql = @"SELECT MBUCFDISTRICT.POSTCODE as postcode FROM MBUCFDISTRICT, MBUCFPROVINCE  WHERE ( MBUCFPROVINCE.PROVINCE_CODE = MBUCFDISTRICT.PROVINCE_CODE ) and ( MBUCFDISTRICT.PROVINCE_CODE = '" + province + "' )    and ( MBUCFDISTRICT.DISTRICT_CODE = '" + districtCode + "' )";
                    Sdt dt_sql = ta.Query(sql);
                    String postcode = dt_sql.Rows[0]["postcode"].ToString();
                    DwTabEditCo.SetItemString(Row, "post_code", postcode);
                }
            }
            catch (Exception)
            {

            }
        }

        private void JspostChangeTranDeptAcc()
        {
            //string tranAccNo = depServ.BaseFormatAccountNo(state.SsWsPass, DwTabCond.GetItemString(1, "tran_deptacc_no"));

            string tranAccNo = ndept.of_analizeaccno(state.SsWsPass, DwTabCond.GetItemString(1, "tran_deptacc_no").Trim()).Trim();
            DwTabCond.SetItemString(1, "tran_deptacc_no", tranAccNo);
            if (tranAccNo != null && tranAccNo != "")
            {
                try
                {

                    DataTable dt = WebUtil.Query(
                        "select dept_tranacc_name as \"name\" from dpdeptmaster" + @"
                        where deptaccount_no = '" + tranAccNo + "' ");
                    if (dt.Rows.Count > 0)
                    {
                        DwTabCond.SetItemString(1, "dept_tranacc_name", dt.Rows[0]["name"].ToString());
                    }
                }
                catch (Exception) { }
            }
        }

        private void checkCoop()
        {
            decimal i = 0;
            decimal crossflag = DwListCoop.GetItemDecimal(1, "cross_coopflag");
            if (crossflag == 1)
            {
                try
                {
                    i = DwListCoop.GetItemDecimal(1, "cross_coopflag");
                }
                catch
                { }
                JsClear();
                DwUtil.RetrieveDDDW(DwListCoop, "dddwcoopname", "cm_constant_config.pbl", state.SsCoopControl, state.SsCoopControl);
            }
            else
            {
                try
                {
                    JsClear();
                    HfCoopid.Value = state.SsCoopControl;
                }
                catch
                { }
            }
        }

        private void JsSetCoopname()
        {
            String Coopid = HfCoopid.Value;
            String Coopname;
            DataTable dt = WebUtil.Query("select coop_name from cmcoopmaster where coop_id ='" + Coopid + "'");
            if (dt.Rows.Count > 0)
            {
                Coopname = dt.Rows[0]["coop_name"].ToString();
                DwListCoop.SetItemDecimal(1, "cross_coopflag", 1);
                DwUtil.RetrieveDDDW(DwListCoop, "dddwcoopname", "cm_constant_config.pbl", state.SsCoopControl, state.SsCoopControl);
                DwListCoop.SetItemString(1, "dddwcoopname", Coopname);
            }

            if (HfDlg.Value == "AccountDlg")
            {
                JsPostAccountNo();
            }
            else if (HfDlg.Value == "MemberDlg")
            {
                JspostMemberNo();
            }
        }

        private void JsClear()
        {
            DwMain.Reset();
            DwTabMem.Reset();
            DwTabAddress.Reset();
            DwTabEditCo.Reset();
            DwTabCond.Reset();
            DwTabTran.Reset();

            DwMain.InsertRow(0);
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {

            postAccountNo = WebUtil.JsPostBack(this, "postAccountNo");
            postAddRow = WebUtil.JsPostBack(this, "postAddRow");
            postChangeTran = WebUtil.JsPostBack(this, "postChangeTran");
            postExpenseAcc = WebUtil.JsPostBack(this, "postExpenseAcc");
            postChangeDwTabCon = WebUtil.JsPostBack(this, "postChangeDwTabCon");
            postMemberNo = WebUtil.JsPostBack(this, "postMemberNo");
            postChangeTranDeptAcc = WebUtil.JsPostBack(this, "postChangeTranDeptAcc");
            FilterBankBranch = WebUtil.JsPostBack(this, "FilterBankBranch");
            FilterProvince = WebUtil.JsPostBack(this, "FilterProvince");
            FilterDistrict = WebUtil.JsPostBack(this, "FilterDistrict");
            postPostOffice = WebUtil.JsPostBack(this, "postPostOffice");
            CheckCoop = WebUtil.JsPostBack(this, "CheckCoop");
            setCoopname = WebUtil.JsPostBack(this, "setCoopname");
            MemberNoSearch = WebUtil.JsPostBack(this, "MemberNoSearch");
            postBankAcc = WebUtil.JsPostBack(this, "postBankAcc");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            //depServ = wcf.Deposit;
            ndept = wcf.NDeposit;
            DwMain.SetTransaction(sqlca);

            DwTabMem.SetTransaction(sqlca);
            DwTabAddress.SetTransaction(sqlca);

            DwTabEditCo.SetTransaction(sqlca);
            DwTabCond.SetTransaction(sqlca);
            DwTabTran.SetTransaction(sqlca);
            DwTabAtm.SetTransaction(sqlca);

          if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwListCoop.InsertRow(0);               
                HfCoopid.Value = state.SsCoopControl;
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwTabMem);
                this.RestoreContextDw(DwTabAddress);
                this.RestoreContextDw(DwTabEditCo);
                this.RestoreContextDw(DwTabCond);
                this.RestoreContextDw(DwTabTran);
                this.RestoreContextDw(DwListCoop);
                this.RestoreContextDw(DwTabAtm);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postAccountNo": JsPostAccountNo(); break;
                case "postAddRow": JsPostAddRow(); break;
                case "postExpenseAcc": JsPostExpenseAcc(); break;
                case "postChangeTran": JspostChangeTran(); break;
                case "postChangeDwTabCon": JspostChangeDwTabCon(); break;
                case "postMemberNo": JspostMemberNo(); break;
                case "postChangeTranDeptAcc": JspostChangeTranDeptAcc(); break;
                case "FilterBankBranch": JsFilterBankBranch(); break;
                case "FilterProvince": JsFilterProvince(); break;
                case "FilterDistrict": JsFilterDistrict(); break;
                case "postPostOffice": JsPostPostOffice(); break;
                case "CheckCoop": checkCoop(); break;
                case "setCoopname": JsSetCoopname(); break;
                case "MemberNoSearch": JsMemberNoSearch(); break;
                case "postBankAcc": JsPostBankAcc(); break;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String deptAccNo = DwMain.GetItemString(1, "deptaccount_no");
                if (deptAccNo != "" && deptAccNo != null)
                {
                    //ชื่อบัญชี
                    if (DwMain.RowCount > 0)
                    {
                        String deptaccount_name = "", deptaccount_ename="";
                        try
                        {
                            deptaccount_name = DwMain.GetItemString(1, "deptaccount_name").Trim();
                        }
                        catch { deptaccount_name = ""; }
                        try
                        {
                            deptaccount_ename = DwMain.GetItemString(1, "deptaccount_ename").Trim();
                        }
                        catch { deptaccount_ename = ""; }
                        try
                        {
                            string update_main = @"update DPDEPTMASTER set deptaccount_name = '" + deptaccount_name + @"', 
                            update_byentryid = '" + state.SsUsername + "', update_byentryip = '" + state.SsClientIp + @"',deptaccount_ename= '" + deptaccount_ename + @"' 
                            where DEPTACCOUNT_NO	='" + deptAccNo + "' and COOP_ID ='" + state.SsCoopControl + "'  ";
                            Sdt sqlupdate = WebUtil.QuerySdt(update_main);
                        }
                        catch { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกชื่อไม่สำเร็จ"); }
                    }                                       
                    ////ข้อมูลสมาชิก                    
                    if (DwTabMem.RowCount > 0)
                    {
                        String memno;
                        try
                        {
                            memno = DwTabMem.GetItemString(1, "member_no").Trim();
                        }
                        catch { memno = ""; }
                        try
                        {
                            string update_memno = "update DPDEPTMASTER set member_no = '" + memno + "', update_byentryid = '" + state.SsUsername + @"', 
                                update_byentryip = '" + state.SsClientIp + "' where DEPTACCOUNT_NO	='" + deptAccNo + "' and COOP_ID ='" + state.SsCoopControl + "'";
                            Sdt sqlupdate = WebUtil.QuerySdt(update_memno);
                        }
                        catch { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกเลขสมาชิกไม่สำเร็จ"); }
                    }   
                    //ผู้ฝากร่วม                    
                    if (DwTabEditCo.RowCount > 0) {

                        String ref_id, prename_code, nameco, surname,house_id, soi, group_id, road, province, district, tumbol, post_code, phone_no, fax_no;
                        decimal ref_type, seq_no;
                        for (int i = 1; i <= DwTabEditCo.RowCount; i++)
                        {
                             seq_no = DwTabEditCo.GetItemDecimal(i, "seq_no");
                            try
                            {
                                ref_type = DwTabEditCo.GetItemDecimal(i, "ref_type");
                            }
                            catch { ref_type = 0; }
                            try
                            {
                                ref_id = DwTabEditCo.GetItemString(i, "ref_no");
                            }
                            catch { ref_id = ""; }

                            try
                            {
                                prename_code = DwTabEditCo.GetItemString(i, "prename_code");
                            }
                            catch { prename_code = ""; }
                            try
                            {
                                nameco = DwTabEditCo.GetItemString(i, "name");
                            }
                            catch { nameco = ""; }
                            try
                            {
                                surname = DwTabEditCo.GetItemString(i, "surname");
                            }
                            catch { surname = ""; }
                            try
                            {
                                house_id = DwTabEditCo.GetItemString(i, "house_id");
                            }
                            catch { house_id = ""; }
                            try
                            {
                                soi = DwTabEditCo.GetItemString(i, "soi");
                            }
                            catch { soi = ""; }
                            try
                            {
                                group_id = DwTabEditCo.GetItemString(i, "group_id");
                            }
                            catch { group_id = ""; }
                            try
                            {
                                road = DwTabEditCo.GetItemString(i, "road");
                            }
                            catch { road = ""; }
                            try
                            {
                                province = DwTabEditCo.GetItemString(i, "province");
                            }
                            catch { province = ""; }
                            try
                            {
                                district = DwTabEditCo.GetItemString(i, "district");
                            }
                            catch { district = ""; }
                            try
                            {
                                tumbol = DwTabEditCo.GetItemString(i, "tumbol");
                            }
                            catch { tumbol = ""; }
                            try
                            {
                                post_code = DwTabEditCo.GetItemString(i, "post_code");
                            }
                            catch { post_code = ""; }
                            try
                            {
                                phone_no = DwTabEditCo.GetItemString(i, "phone_no");
                            }
                            catch { phone_no = ""; }
                            try
                            {
                                fax_no = DwTabEditCo.GetItemString(i, "fax_no");
                            }
                            catch { fax_no = ""; }
                            Sta ta = new Sta(state.SsConnectionString);
                            //chk seqno
                            String sql = @"select  seq_no from DPCODEPOSIT where  DEPTACCOUNT_NO = '" + deptAccNo + "' and  seq_no ='" + seq_no + "'";
                            Sdt dt_seqno = ta.Query(sql);
                            if (dt_seqno.Rows.Count > 0)
                            {                                
                                try
                                {
                                    string update_co = "update DPCODEPOSIT set ref_type='" + ref_type + "',ref_no='" + ref_id + "',name = '" + nameco + "',surname = '" + surname + "',prename_code='" + prename_code + "',house_id = '" + house_id + "',soi = '" + soi + "',group_id = '" + group_id + "',road = '" + road + "',province = '" + province + "',district = '" + district + "',tumbol = '" + tumbol + "',post_code = '" + post_code + "',phone_no = '" + phone_no + "',fax_no = '" + fax_no + "' where DEPTACCOUNT_NO	='" + deptAccNo + "' and seq_no='" + seq_no + "' and COOP_ID ='" + state.SsCoopControl + "'  ";
                                    Sdt sqlupdateco = WebUtil.QuerySdt(update_co);
                                }
                                catch { LtServerMessage.Text = LtServerMessage.Text + "  " + WebUtil.ErrorMessage("อัพเดทข้อมูลผู้ฝากร่วมคนที่") + seq_no + " ไม่สำเร็จ"; }
                            }
                            else 
                            {
                                try
                                {
                                    String sqlinsert_co = "INSERT INTO DPCODEPOSIT (coop_id,DEPTACCOUNT_NO, seq_no,ref_type,ref_no,name,surname,prename_code,house_id,soi ,group_id ,road ,province ,district,tumbol ,post_code ,phone_no ,fax_no) VALUES('" + state.SsCoopControl + "','" + deptAccNo + "','" + seq_no + "','" + ref_type + "','" + ref_id + "','" + nameco + "','" + surname + "','" + prename_code + "', '" + house_id + "','" + soi + "','" + group_id + "', '" + road + "', '" + province + "', '" + district + "','" + tumbol + "','" + post_code + "','" + phone_no + "','" + fax_no + "')";
                                    Sdt sqlinsertco = WebUtil.QuerySdt(sqlinsert_co);
                                }
                                catch { LtServerMessage.Text = LtServerMessage.Text + "  " + WebUtil.ErrorMessage("บันทึกข้อมูลผู้ฝากร่วมคนที่") + seq_no + " ไม่สำเร็จ"; }
                            }
                        }//end DwTabEditCo.RowCount                                                                                                 
                    }

                    //เงื่อนไข                   
                    if (DwTabCond.RowCount > 0)
                    {
                        decimal rateStatus,rateamt, monthintpay_meth, bookconfirm_status, limitprnbal_flag, limitprnbal_amt, taxStatus, f_tax_rate, prnc_no, laststmseq_no, withdraw_count;
                        String condforwithdraw, remark, postCode, dept_tranacc_name, bankCode, bank_branch, acccont_type;
                        try
                        {
                            rateStatus = DwTabCond.GetItemDecimal(1, "spcint_rate_status");
                        }
                        catch { rateStatus = 0; }
                        try
                        {
                            rateamt = DwTabCond.GetItemDecimal(1, "spcint_rate");
                        }
                        catch { rateamt = 0; }
                        try
                        {
                            condforwithdraw = DwTabCond.GetItemString(1, "condforwithdraw").Trim();
                        }
                        catch { condforwithdraw = ""; }
                        try
                        {
                            remark = DwTabCond.GetItemString(1, "remark").Trim();
                        }
                        catch { remark = ""; }
                        try
                        {
                            monthintpay_meth = DwTabCond.GetItemDecimal(1, "monthintpay_meth");
                        }
                        catch { monthintpay_meth = 0; }
                        try
                        {
                            bookconfirm_status = DwTabCond.GetItemDecimal(1, "bookconfirm_status");
                        }
                        catch { bookconfirm_status = 0; }
                        try
                        {
                            limitprnbal_flag = DwTabCond.GetItemDecimal(1, "limitprnbal_flag");
                        }
                        catch { limitprnbal_flag = 0; }
                        try
                        {
                            limitprnbal_amt = DwTabCond.GetItemDecimal(1, "limitprnbal_amt");
                        }
                        catch { limitprnbal_amt = 0; }
                        try
                        {
                            postCode = DwTabCond.GetItemString(1, "tran_deptacc_no").Trim();
                        }
                        catch { postCode = ""; }
                        try
                        {
                            dept_tranacc_name = DwTabCond.GetItemString(1, "dept_tranacc_name").Trim();
                        }
                        catch { dept_tranacc_name = ""; }
                        try
                        {
                            bankCode = DwTabCond.GetItemString(1, "bank_code");
                        }
                        catch { bankCode = ""; }
                        try
                        {
                            bank_branch = DwTabCond.GetItemString(1, "bank_branch");
                        }
                        catch { bank_branch = ""; }
                        try
                        {
                            taxStatus = DwTabCond.GetItemDecimal(1, "taxspcrate_status");
                        }
                        catch { taxStatus = 0; }
                        try
                        {
                            f_tax_rate = DwTabCond.GetItemDecimal(1, "f_tax_rate");
                        }
                        catch { f_tax_rate = 0; }
                        try
                        {
                            acccont_type = DwTabCond.GetItemString(1, "acccont_type");
                        }
                        catch { acccont_type = ""; }
                        try
                        {
                            prnc_no = DwTabCond.GetItemDecimal(1, "prnc_no");
                        }
                        catch { prnc_no = 0; }
                        try
                        {
                            laststmseq_no = DwTabCond.GetItemDecimal(1, "laststmseq_no");
                        }
                        catch { laststmseq_no = 0; }
                        try
                        {
                            withdraw_count = DwTabCond.GetItemDecimal(1, "withdraw_count");
                        }
                        catch { withdraw_count = 0; }
                        try
                        {
                            string update_deptmas = "update DPDEPTMASTER set spcint_rate_status = '" + rateStatus + "',spcint_rate='" + rateamt + "',condforwithdraw='" + condforwithdraw + @"',
                            remark='" + remark + "',monthintpay_meth='" + monthintpay_meth + "',limitprnbal_flag='" + limitprnbal_flag + @"',
                            limitprnbal_amt='" + limitprnbal_amt + "',prnc_no='" + prnc_no + "',taxspcrate_status='" + taxStatus + @"',
                            f_tax_rate='" + f_tax_rate + "',laststmseq_no='" + laststmseq_no + "',acccont_type='" + acccont_type + @"',
                            withdraw_count='" + withdraw_count + "',tran_deptacc_no='" + postCode + "',dept_tranacc_name='" + dept_tranacc_name + @"',
                            bank_code='" + bankCode + "',bank_branch='" + bank_branch + "',bookconfirm_status='" + bookconfirm_status + @"',
                            update_byentryid = '" + state.SsUsername + "', update_byentryip = '" + state.SsClientIp + @"'  
                            where DEPTACCOUNT_NO	='" + deptAccNo + "' and COOP_ID ='" + state.SsCoopControl + "'  ";
                            Sdt sqlupdate = WebUtil.QuerySdt(update_deptmas);
                        }
                        catch { LtServerMessage.Text = LtServerMessage.Text + "  "+WebUtil.ErrorMessage("บันทึกข้อมูลเงื่อนไขไม่สำเร็จ"); }
                    }
                    //การโอนเงิน                    
                    if (DwTabTran.RowCount > 0)
                    {
                        String expense_code, expense_accno;
                        decimal expense_amount;
                        try
                        {
                            expense_code = DwTabTran.GetItemString(1, "expense_code").Trim();
                        }
                        catch { expense_code = ""; }
                        try
                        {
                            expense_accno = DwTabTran.GetItemString(1, "expense_accno").Trim();
                        }
                        catch { expense_accno = ""; }
                        try
                        {
                            expense_amount = DwTabTran.GetItemDecimal(1, "expense_amount");
                        }
                        catch { expense_amount = 0; }
                        try{
                            string update_deptmas = "update DPDEPTMASTER set expense_code = '" + expense_code + "',expense_accno='" + expense_accno + @"',
                                expense_amount='" + expense_amount + "', update_byentryid = '" + state.SsUsername + "', update_byentryip = '" + state.SsClientIp + @"'  
                                where DEPTACCOUNT_NO	='" + deptAccNo + "' and COOP_ID ='" + state.SsCoopControl + "'  ";
                            Sdt sqlupdate = WebUtil.QuerySdt(update_deptmas);
                        }
                        catch{  
                            LtServerMessage.Text = LtServerMessage.Text + "  "+ WebUtil.ErrorMessage("บันทึกข้อมูลการโอนเงินไม่สำเร็จ");
                        }
                    }    
                     //การจับคู่ธนาคารเพื่อ atm                
                    if (DwTabAtm.RowCount > 0)
                    {
                        String bank_accid = "";
                        try
                        {
                            bank_accid = DwTabAtm.GetItemString(1, "bank_accid").Trim();
                        }
                        catch { bank_accid = ""; }
                        try
                        {
                            string update_atm = "update DPDEPTMASTER set bank_accid = '" + bank_accid + "', update_byentryid = '" + state.SsUsername + @"', 
                                update_byentryip = '" + state.SsClientIp + @"'  where DEPTACCOUNT_NO ='" + deptAccNo + "' and COOP_ID ='" + state.SsCoopControl + "'  ";
                            Sdt sqlupdate = WebUtil.QuerySdt(update_atm);
                        }
                        catch
                        {
                            LtServerMessage.Text = LtServerMessage.Text + "  " + WebUtil.ErrorMessage("บันทึกข้อมูลการจับคู่ธนาคารเพื่อ atm ไม่สำเร็จ");
                        }
                    }
                    
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จสิ้น");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwTabMem.SaveDataCache();
            DwTabAddress.SaveDataCache();
            DwTabEditCo.SaveDataCache();
            DwTabCond.SaveDataCache();
            DwTabTran.SaveDataCache();
            DwListCoop.SaveDataCache();
            DwTabAtm.SaveDataCache();
        }
        private void resetEditCo(int rowco)
        { 
            DwTabEditCo.SetItemString(rowco, "prename_code", "");
            DwTabEditCo.SetItemString(rowco, "name","");
            DwTabEditCo.SetItemString(rowco, "surname","");
            DwTabEditCo.SetItemString(rowco, "house_id","");
            DwTabEditCo.SetItemString(rowco, "group_id","");
            DwTabEditCo.SetItemString(rowco, "soi","");
            DwTabEditCo.SetItemString(rowco, "road","");
            DwTabEditCo.SetItemString(rowco, "province","");
            DwTabEditCo.SetItemString(rowco, "district","");          
            DwTabEditCo.SetItemString(rowco, "tumbol","");
            DwTabEditCo.SetItemString(rowco, "post_code","");
            DwTabEditCo.SetItemString(rowco, "phone_no","");
            DwTabEditCo.SetItemString(rowco, "fax_no", "");        
        }
        private void JsMemberNoSearch()
        {
            int rowco = Convert.ToInt16(HdRowMemCo.Value); ;
            Decimal ref_type = DwTabEditCo.GetItemDecimal(rowco, "ref_type");
            //String ref_id = DwTabEditCo.GetItemString(rowco, "ref_no");
            String ref_id = HdRefNo.Value;
            //reset
            resetEditCo(rowco);
            ///
            if (ref_type == 3)
            {
                ref_id = WebUtil.MemberNoFormat(ref_id);
                DwTabEditCo.SetItemString(rowco, "ref_no", ref_id);
                String sql = @"SELECT MBMEMBMASTER.MEMBER_NO ,          
                               MBMEMBMASTER.PRENAME_CODE ,         
                               MBMEMBMASTER.MEMB_NAME ,        
                               MBMEMBMASTER.MEMB_SURNAME ,     
                               MBMEMBMASTER.MATE_NAME ,  
                               MBUCFPOSITION.POSITION_DESC ,   
                               MBMEMBMASTER.CARD_PERSON ,   
                               MBMEMBMASTER.RESIGN_STATUS ,     
                               MBMEMBMASTER.CLOSE_DATE ,       
                               MBUCFPROVINCE_A.PROVINCE_DESC ,         
                               MBUCFDISTRICT_A.DISTRICT_DESC ,        
                               MBUCFDEPARTMENT.DEPARTMENT_DESC ,      
                               MBUCFTAMBOL_A.TAMBOL_DESC ,          
                               MBMEMBMASTER.TAMBOL_CODE ,         
                               MBMEMBMASTER.MEM_TELWORK ,       
                               MBMEMBMASTER.ADDR_NO ,        
                               MBMEMBMASTER.ADDR_MOO ,         
                               MBMEMBMASTER.ADDR_SOI ,       
                               MBMEMBMASTER.ADDR_VILLAGE ,   
                               MBMEMBMASTER.ADDR_ROAD ,   
                               MBMEMBMASTER.AMPHUR_CODE ,  
                               MBMEMBMASTER.PROVINCE_CODE ,     
                               MBMEMBMASTER.ADDR_POSTCODE ,       
                               MBMEMBMASTER.ADDR_PHONE ,        
                               MBMEMBMASTER.ADDR_MOBILEPHONE ,        
                               MBMEMBMASTER.ADDR_EMAIL ,         
                               MBMEMBMASTER.MARIAGE_STATUS ,    
                               MBMEMBMASTER.CURRADDR_NO ,    
                               MBMEMBMASTER.CURRADDR_MOO , 
                               MBMEMBMASTER.CURRADDR_SOI ,    
                               MBMEMBMASTER.CURRADDR_VILLAGE , 
                               MBMEMBMASTER.CURRADDR_ROAD ,       
                               MBMEMBMASTER.CURRTAMBOL_CODE ,        
                               MBMEMBMASTER.CURRAMPHUR_CODE ,        
                               MBMEMBMASTER.CURRPROVINCE_CODE ,      
                               MBMEMBMASTER.CURRADDR_POSTCODE ,    
                               MBMEMBMASTER.CURRADDR_PHONE ,        
                               MBMEMBMASTER.RETRY_STATUS ,    
			                   MBUCFTAMBOL_B.TAMBOL_DESC ,         
	 	                       MBUCFDISTRICT_B.DISTRICT_DESC ,    
     	                       MBUCFPROVINCE_B.PROVINCE_DESC       
                          FROM MBMEMBMASTER ,          
			                   MBUCFRESIGNCAUSE ,         
 			                   MBUCFDISTRICT MBUCFDISTRICT_A ,     
    		                   MBUCFPROVINCE MBUCFPROVINCE_A ,   
                               MBUCFDEPARTMENT ,           
			                   MBUCFTAMBOL MBUCFTAMBOL_A ,          
 			                   MBUCFTAMBOL MBUCFTAMBOL_B ,        
   			                   MBUCFDISTRICT MBUCFDISTRICT_B ,      
     		                   MBUCFPROVINCE MBUCFPROVINCE_B ,MBUCFPOSITION    
                        WHERE ( MBUCFRESIGNCAUSE.RESIGNCAUSE_CODE (+) = MBMEMBMASTER.RESIGNCAUSE_CODE) and  
        	                  ( MBUCFRESIGNCAUSE.COOP_ID (+) = MBMEMBMASTER.COOP_ID) and       
   		                      ( MBMEMBMASTER.AMPHUR_CODE = MBUCFDISTRICT_A.DISTRICT_CODE (+)) and   
       	                      ( MBUCFPROVINCE_A.PROVINCE_CODE (+) = MBMEMBMASTER.PROVINCE_CODE) and  
                              ( MBMEMBMASTER.PROVINCE_CODE = MBUCFDISTRICT_A.PROVINCE_CODE (+)) and 
                              ( MBMEMBMASTER.DEPARTMENT_CODE = MBUCFDEPARTMENT.DEPARTMENT_CODE (+)) and   
                              ( MBMEMBMASTER.TAMBOL_CODE = MBUCFTAMBOL_A.TAMBOL_CODE (+)) and         
                              ( MBMEMBMASTER.CURRTAMBOL_CODE = MBUCFTAMBOL_B.TAMBOL_CODE (+)) and       
   		                      ( MBMEMBMASTER.CURRAMPHUR_CODE = MBUCFDISTRICT_B.DISTRICT_CODE (+)) and   
                              ( MBMEMBMASTER.CURRPROVINCE_CODE = MBUCFPROVINCE_B.PROVINCE_CODE (+)) and      
                              ( MBMEMBMASTER.COOP_ID = mbucfdepartment.coop_id(+) ) and 
                              (MBUCFPOSITION.POSITION_CODE(+) = MBMEMBMASTER.POSITION_CODE) and                              
 	                          ( MBMEMBMASTER.MEMBER_NO = '" + ref_id + "' )    ";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    DwTabEditCo.SetItemString(rowco, "prename_code", dt.Rows[0]["prename_code"].ToString());
                    DwTabEditCo.SetItemString(rowco, "name", dt.Rows[0]["MEMB_NAME"].ToString());
                    DwTabEditCo.SetItemString(rowco, "surname", dt.Rows[0]["MEMB_SURNAME"].ToString());
                    DwTabEditCo.SetItemString(rowco, "house_id", dt.Rows[0]["ADDR_NO"].ToString());
                    DwTabEditCo.SetItemString(rowco, "group_id", dt.Rows[0]["ADDR_MOO"].ToString());
                    DwTabEditCo.SetItemString(rowco, "soi", dt.Rows[0]["ADDR_SOI"].ToString());
                    DwTabEditCo.SetItemString(rowco, "road", dt.Rows[0]["ADDR_ROAD"].ToString());
                    DwTabEditCo.SetItemString(rowco, "province", dt.Rows[0]["PROVINCE_CODE"].ToString());
                    DwTabEditCo.SetItemString(rowco, "district", dt.Rows[0]["AMPHUR_CODE"].ToString());
                    //retrive data district
                    DataWindowChild childdis = DwTabEditCo.GetChild("district");
                    childdis.SetTransaction(sqlca);
                    childdis.Retrieve();
                    childdis.Filter();
                    DwTabEditCo.SetItemString(rowco, "tumbol", dt.Rows[0]["TAMBOL_CODE"].ToString());
                    //retrive data tumbol
                    DataWindowChild child = DwTabEditCo.GetChild("tumbol");
                    child.SetTransaction(sqlca);
                    child.Retrieve();
                    child.Filter();
                    DwTabEditCo.SetItemString(rowco, "post_code", dt.Rows[0]["ADDR_POSTCODE"].ToString());
                    DwTabEditCo.SetItemString(rowco, "phone_no", dt.Rows[0]["ADDR_PHONE"].ToString());
                }
            }
            else if (ref_type == 1)
            {

                String sql = @"SELECT MBMEMBMASTER.MEMBER_NO ,          
                               MBMEMBMASTER.PRENAME_CODE ,         
                               MBMEMBMASTER.MEMB_NAME ,        
                               MBMEMBMASTER.MEMB_SURNAME ,     
                               MBMEMBMASTER.MATE_NAME ,  
                               MBUCFPOSITION.POSITION_DESC ,   
                               MBMEMBMASTER.CARD_PERSON ,   
                               MBMEMBMASTER.RESIGN_STATUS ,     
                               MBMEMBMASTER.CLOSE_DATE ,       
                               MBUCFPROVINCE_A.PROVINCE_DESC ,         
                               MBUCFDISTRICT_A.DISTRICT_DESC ,        
                               MBUCFDEPARTMENT.DEPARTMENT_DESC ,      
                               MBUCFTAMBOL_A.TAMBOL_DESC ,          
                               MBMEMBMASTER.TAMBOL_CODE ,         
                               MBMEMBMASTER.MEM_TELWORK ,       
                               MBMEMBMASTER.ADDR_NO ,        
                               MBMEMBMASTER.ADDR_MOO ,         
                               MBMEMBMASTER.ADDR_SOI ,       
                               MBMEMBMASTER.ADDR_VILLAGE ,   
                               MBMEMBMASTER.ADDR_ROAD ,   
                               MBMEMBMASTER.AMPHUR_CODE ,  
                               MBMEMBMASTER.PROVINCE_CODE ,     
                               MBMEMBMASTER.ADDR_POSTCODE ,       
                               MBMEMBMASTER.ADDR_PHONE ,        
                               MBMEMBMASTER.ADDR_MOBILEPHONE ,        
                               MBMEMBMASTER.ADDR_EMAIL ,         
                               MBMEMBMASTER.MARIAGE_STATUS ,    
                               MBMEMBMASTER.CURRADDR_NO ,    
                               MBMEMBMASTER.CURRADDR_MOO , 
                               MBMEMBMASTER.CURRADDR_SOI ,    
                               MBMEMBMASTER.CURRADDR_VILLAGE , 
                               MBMEMBMASTER.CURRADDR_ROAD ,       
                               MBMEMBMASTER.CURRTAMBOL_CODE ,        
                               MBMEMBMASTER.CURRAMPHUR_CODE ,        
                               MBMEMBMASTER.CURRPROVINCE_CODE ,      
                               MBMEMBMASTER.CURRADDR_POSTCODE ,    
                               MBMEMBMASTER.CURRADDR_PHONE ,        
                               MBMEMBMASTER.RETRY_STATUS ,    
			                   MBUCFTAMBOL_B.TAMBOL_DESC ,         
	 	                       MBUCFDISTRICT_B.DISTRICT_DESC ,    
     	                       MBUCFPROVINCE_B.PROVINCE_DESC       
                          FROM MBMEMBMASTER ,          
			                   MBUCFRESIGNCAUSE ,         
 			                   MBUCFDISTRICT MBUCFDISTRICT_A ,     
    		                   MBUCFPROVINCE MBUCFPROVINCE_A ,   
                               MBUCFDEPARTMENT ,           
			                   MBUCFTAMBOL MBUCFTAMBOL_A ,          
 			                   MBUCFTAMBOL MBUCFTAMBOL_B ,        
   			                   MBUCFDISTRICT MBUCFDISTRICT_B ,      
     		                   MBUCFPROVINCE MBUCFPROVINCE_B ,MBUCFPOSITION    
                        WHERE ( MBUCFRESIGNCAUSE.RESIGNCAUSE_CODE (+) = MBMEMBMASTER.RESIGNCAUSE_CODE) and  
        	                  ( MBUCFRESIGNCAUSE.COOP_ID (+) = MBMEMBMASTER.COOP_ID) and       
   		                      ( MBMEMBMASTER.AMPHUR_CODE = MBUCFDISTRICT_A.DISTRICT_CODE (+)) and   
       	                      ( MBUCFPROVINCE_A.PROVINCE_CODE (+) = MBMEMBMASTER.PROVINCE_CODE) and  
                              ( MBMEMBMASTER.PROVINCE_CODE = MBUCFDISTRICT_A.PROVINCE_CODE (+)) and 
                              ( MBMEMBMASTER.DEPARTMENT_CODE = MBUCFDEPARTMENT.DEPARTMENT_CODE (+)) and   
                              ( MBMEMBMASTER.TAMBOL_CODE = MBUCFTAMBOL_A.TAMBOL_CODE (+)) and         
                              ( MBMEMBMASTER.CURRTAMBOL_CODE = MBUCFTAMBOL_B.TAMBOL_CODE (+)) and       
   		                      ( MBMEMBMASTER.CURRAMPHUR_CODE = MBUCFDISTRICT_B.DISTRICT_CODE (+)) and   
                              ( MBMEMBMASTER.CURRPROVINCE_CODE = MBUCFPROVINCE_B.PROVINCE_CODE (+)) and      
                              ( MBMEMBMASTER.COOP_ID = mbucfdepartment.coop_id(+) ) and 
                              ( MBUCFPOSITION.POSITION_CODE(+) = MBMEMBMASTER.POSITION_CODE) and                              
 	                          ( MBMEMBMASTER.CARD_PERSON  = '" + ref_id + "' )    ";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    DwTabEditCo.SetItemString(rowco, "prename_code", dt.Rows[0]["prename_code"].ToString());
                    DwTabEditCo.SetItemString(rowco, "name", dt.Rows[0]["MEMB_NAME"].ToString());
                    DwTabEditCo.SetItemString(rowco, "surname", dt.Rows[0]["MEMB_SURNAME"].ToString());
                    DwTabEditCo.SetItemString(rowco, "house_id", dt.Rows[0]["ADDR_NO"].ToString());
                    DwTabEditCo.SetItemString(rowco, "group_id", dt.Rows[0]["ADDR_MOO"].ToString());
                    DwTabEditCo.SetItemString(rowco, "soi", dt.Rows[0]["ADDR_SOI"].ToString());
                    DwTabEditCo.SetItemString(rowco, "road", dt.Rows[0]["ADDR_ROAD"].ToString());
                    DwTabEditCo.SetItemString(rowco, "province", dt.Rows[0]["PROVINCE_CODE"].ToString());
                    DwTabEditCo.SetItemString(rowco, "district", dt.Rows[0]["AMPHUR_CODE"].ToString());
                    //retrive data district
                    DataWindowChild childdis = DwTabEditCo.GetChild("district");
                    childdis.SetTransaction(sqlca);
                    childdis.Retrieve();
                    childdis.Filter();
                    DwTabEditCo.SetItemString(rowco, "tumbol", dt.Rows[0]["TAMBOL_CODE"].ToString());
                    //retrive data tumbol
                    DataWindowChild child = DwTabEditCo.GetChild("tumbol");
                    child.SetTransaction(sqlca);
                    child.Retrieve();
                    child.Filter();
                    DwTabEditCo.SetItemString(rowco, "post_code", dt.Rows[0]["ADDR_POSTCODE"].ToString());
                    DwTabEditCo.SetItemString(rowco, "phone_no", dt.Rows[0]["ADDR_PHONE"].ToString());
                    //ref_id = WebUtil.ViewCardMemberFormat(ref_id);
                    //DwList.SetItemString(1, "ref_id", ref_id);
                }
            }
        }
        #endregion
    }
}

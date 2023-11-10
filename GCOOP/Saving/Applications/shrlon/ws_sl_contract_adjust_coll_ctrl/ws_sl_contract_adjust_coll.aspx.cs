using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_contract_adjust_coll_ctrl
{
    public partial class ws_sl_contract_adjust_coll : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostLoanContractNo { get; set; }
        [JsPostBack]
        public string PostInsertRowColl { get; set; }
        [JsPostBack]
        public string PostDeleteRowColl { get; set; }
        [JsPostBack]
        public string PostGetCollPermiss { get; set; }
        [JsPostBack]
        public string PostRefresh { get; set; }


        Sdt dt;
        string coll_string = "";
        str_lncontaj astr_contaj = new str_lncontaj();

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsColl.InitDsColl(this);
            dsCollOld.InitDsCollOld(this);
        }

        public void WebSheetLoadBegin()
        {
            LtServerMessagecoll.Text = "";
            if (!IsPostBack)
            {
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                dsMain.SetItem(0, dsMain.DATA.MEMBER_NOColumn, WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO));
                dsMain.RetrieveMembNo();
                dsMain.DATA[0].CONTADJUST_DATE = state.SsWorkDate;
                dsColl.ResetRow();
 
            }
            else if (eventArg == PostLoanContractNo)
            {
                JsPostLoanContractNo();
            }
            else if (eventArg == PostRefresh)
            {
            }
            else if (eventArg == PostInsertRowColl)
            {
                dsColl.InsertLastRow();
                int currow = dsColl.RowCount - 1;
                try
                {
                    dsColl.DATA[currow].SEQ_NO = dsColl.GetMaxValueDecimal("SEQ_NO") + 1;
                }
                catch
                {
                    if (dsColl.RowCount < 1)
                    {
                        dsColl.DATA[currow].SEQ_NO = 1;
                    }
                }
                dsColl.DATA[currow].CONTADJUST_TYPE = "NEW";
            }
            else if (eventArg == PostDeleteRowColl)
            {
                int row = dsColl.GetRowFocus();
                dsColl.DeleteRow(row);
            }
            else if (eventArg == PostGetCollPermiss)
            {
                GetCollPermiss();
            }

            dsMain.DdAdjustcause();
        }
        public void JsPostLoanContractNo()
        {
            try
            {
                string loancontract_no = dsMain.DATA[0].LOANCONTRACT_NO.Trim();
                DateTime contadjust_date = dsMain.DATA[0].CONTADJUST_DATE;

                astr_contaj.loancontract_no = loancontract_no;
                astr_contaj.contaj_date = contadjust_date;
                astr_contaj.xml_contdetail = dsMain.ExportXml(); ;
                astr_contaj.xml_contcoll = dsColl.ExportXml();

                astr_contaj.entry_id = state.SsUsername;

                int result = wcf.NShrlon.of_initreq_contadjust(state.SsWsPass, ref astr_contaj);
                if (result == 1)
                {
                    if (astr_contaj.xml_contdetail != null && astr_contaj.xml_contdetail != "")
                    {
                        dsMain.ResetRow();
                        dsMain.ImportData(astr_contaj.xml_contdetail);
                        dsMain.DdContlaw();
                    }
                    if (astr_contaj.xml_contloanpay != null && astr_contaj.xml_contloanpay != "")
                    {

                    }
                    if (astr_contaj.xml_contpayment != null && astr_contaj.xml_contpayment != "")
                    {

                    }
                    if (astr_contaj.xml_contcoll != null && astr_contaj.xml_contcoll != "")
                    {
                        dsColl.ResetRow();
                        dsColl.ImportData(astr_contaj.xml_contcoll);
                        dsColl.DdLnCollType();
                        for (int i = 1; i <= dsColl.RowCount; i++)
                        {
                            dsColl.DATA[i-1].CONTADJUST_TYPE = "OLD";
                        }
                        dsCollOld.ImportData(dsColl.ExportXml());
                    }
                    if (astr_contaj.xml_contint != null && astr_contaj.xml_contint != "")
                    {

                    }
                    if (astr_contaj.xml_contintspc != null && astr_contaj.xml_contintspc != "")
                    {

                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลสัญญา");
                    dsMain.ResetRow();
                    dsColl.ResetRow();
                }
            }
            catch (Exception ex) 
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        /// <summary>
        /// ตรวจสอบสถานะสมาชิก
        /// </summary>
        private void IsValidMember(string member_no)
        {
            decimal memb_age = 0,       //อายุสมาชิก
                    retry_age = 0,      //อายุเกษียณ
                    work_age = 0,       //อายุงาน
                    resign_status = 0;  //สถานะการลาออก

            string sql = @"SELECT ft_memname( coop_id , member_no ) as member_name,
                                  months_between( sysdate, mbmembmaster.member_date  ) - mod( months_between( sysdate, mbmembmaster.member_date ) , 1 ) as memb_age,   
							      months_between( mbmembmaster.retry_date, sysdate ) - mod( months_between( mbmembmaster.retry_date, sysdate ) , 1 ) as retry_age,   
							      months_between( sysdate, mbmembmaster.work_date  ) - mod( months_between( sysdate, mbmembmaster.work_date ) , 1 ) as work_age,
                                  resign_status
                            FROM mbmembmaster                               
                            WHERE ( mbmembmaster.member_no = {0} )                                  
                                  and ( mbmembmaster.coop_id = {1} )
                            ";

            sql = WebUtil.SQLFormat(sql, member_no, state.SsCoopControl);
            Sdt dt = WebUtil.QuerySdt(sql);

            if (dt.Next())
            {
                memb_age = dt.GetDecimal("memb_age");
                work_age = dt.GetDecimal("work_age");
                retry_age = dt.GetDecimal("retry_age");
                resign_status = dt.GetDecimal("resign_status");

                if (retry_age < 0)
                {
                    LtServerMessagecoll.Text += WebUtil.WarningMessage("ผู้ค้ำ " + member_no + " เป็นสมาชิกที่เกษียณแล้ว กรุณาตรวจสอบ");
                }
                if (resign_status == 1)
                {
                    LtServerMessagecoll.Text += WebUtil.WarningMessage("ผู้ค้ำ " + member_no + " เป็นสมาชิกที่ลาออกแล้ว กรุณาตรวจสอบ");
                }
            }
        }

        /// <summary>
        /// ตรวจสอบสิทธิ์ค้ำ
        /// </summary>
        private void GetCollPermiss()
        {
            string ref_collno = "";
            String description = "";        //รายละเอียดค้ำประกัน
            decimal memb_age = 0,           //อายุสมาชิก
                    retry_age = 0,          //อายุเกษียณ
                    work_age = 0,           //อายุงาน
                    period_payamt = 0;      //งวด

            string sql;

            string coop_id = state.SsCoopControl;
            string loantype_code = dsMain.DATA[0].LOANTYPE_CODE;
            string member_no = dsMain.DATA[0].MEMBER_NO;

            int collrow = Convert.ToInt32(HdCollRow.Value);
            string loancolltype_code = dsColl.DATA[collrow].LOANCOLLTYPE_CODE;
            if (loancolltype_code == "01")
            {
                ref_collno = WebUtil.MemberNoFormat(dsColl.DATA[collrow].REF_COLLNO);
                dsColl.DATA[collrow].REF_COLLNO = ref_collno;
                dsColl.DATA[collrow].CONTADJUST_TYPE = "NEW";
            }
            else if (loancolltype_code == "55")
            {
                ref_collno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsColl.DATA[collrow].REF_COLLNO = ref_collno;
                dsColl.DATA[collrow].CONTADJUST_TYPE = "NEW";
            }
            else
            {
                ref_collno = dsColl.DATA[collrow].REF_COLLNO;
                dsColl.DATA[collrow].CONTADJUST_TYPE = "NEW";
            }

            try
            {
                //เช็คสิทธิ์การค้ำประกัน
                //int result = wcf.NShrlon.of_isvalidcoll(state.SsWsPass, loantype_code, loancolltype_code, coop_id, ref_collno);
                int result = wcf.NShrlon.of_isvalidcoll(state.SsWsPass, loantype_code, loancolltype_code, coop_id, ref_collno);

                if (result == 1 && ref_collno != "")
                {
                    try
                    {
                        //คนค้ำประกัน
                        if (loancolltype_code == "01")
                        {
                            sql = @"SELECT ft_memname( coop_id , member_no ) as member_name,
                                  months_between( sysdate, mbmembmaster.member_date  ) - mod( months_between( sysdate, mbmembmaster.member_date ) , 1 ) as memb_age,   
							      months_between( mbmembmaster.retry_date, sysdate ) - mod( months_between( mbmembmaster.retry_date, sysdate ) , 1 ) as retry_age,   
							      months_between( sysdate, mbmembmaster.work_date  ) - mod( months_between( sysdate, mbmembmaster.work_date ) , 1 ) as work_age
                            FROM mbmembmaster                               
                            WHERE ( mbmembmaster.member_no = {0} )                                  
                                  and ( mbmembmaster.coop_id = {1} )
                            ";

                            sql = WebUtil.SQLFormat(sql, ref_collno, state.SsCoopControl);
                            Sdt dt = WebUtil.QuerySdt(sql);

                            if (dt.Next())
                            {
                                memb_age = dt.GetDecimal("memb_age");
                                work_age = dt.GetDecimal("work_age");
                                retry_age = dt.GetDecimal("retry_age");
                                description = dt.GetString("member_name");

                                dsColl.SetItem(collrow, dsColl.DATA.DESCRIPTIONColumn, description);
                                //period_payamt = dw_main.GetItemDecimal(1, "period_payamt");
                                if (retry_age < 0)
                                {
                                    LtServerMessagecoll.Text += WebUtil.ErrorMessage("ผู้ค้ำ " + ref_collno + " เป็นสมาชิกที่เกษียณแล้ว กรุณาตรวจสอบ");
                                }
                                else if (retry_age < period_payamt)
                                {
                                    LtServerMessagecoll.Text += WebUtil.WarningMessage("ผู้ค้ำ " + ref_collno + " ค้ำประกันได้สูงสุด " + retry_age.ToString() + " งวด ซึ่งน้อยกว่างวดขอกู้");
                                }
                            }
                        }

                        decimal permiss = 0,            //ref service of_getcollpermiss
                                percent = 0,            //ref service of_getcollpermiss
                                maxcoll = 0;            //ref service of_getcollpermiss
                        decimal collpermiss = 0,        //สิทธิค้ำสูงสุด
                                checkcollmancount = 0,  //สิทธิ์ค้ำที่ใช้ไปของสัญญาเงินกู้
                                collmax_amt = 0;        //สิทธิ์ค้ำสูงสุดของสัญญาเงินกู้
                        DateTime adtm_check = state.SsWorkDate;            //วันที่ทำรายการ
                        decimal collusecontamt = 0,     //สิทธิ์ค้ำที่ใช้ไปของใบคำขอ
                                collusereqamt = 0;      //เช็คจำนวนสัญญาที่สามารถใช้ค้ำได้
                        decimal collbalance_amt,        //สิทธิค้ำคงเหลือ
                                collactive_amt = 0,     //ค้ำประกัน
                                collactive_percent = 0, //% ค้ำ
                                flag_permiss,           //flag การคำนวณ เปอร์เซ็นต์
                                flag_collmancount;      //flag การคำนวณ เปอร์เซ็นต์
                        
                        //get ค้ำเดิม cherry
                        
                        try
                        {//สิทธิค้ำสูงสุด
                            int result_permiss = wcf.NShrlon.of_getcollpermiss(state.SsWsPass, loantype_code, loancolltype_code, state.SsCoopControl, ref_collno, adtm_check, ref permiss, ref maxcoll, ref percent);
                            collpermiss = permiss;
                            collactive_percent = percent;
                            collmax_amt = maxcoll;

                            dsColl.DATA[collrow].COLLBASE_AMT = collpermiss;
                            dsColl.DATA[collrow].COLLBASE_PERCENT = collactive_percent;
                            dsColl.DATA[collrow].COLLMAX_AMT = collmax_amt;
                        }
                        catch (Exception ex)
                        {//สิทธิ์ค้ำประกันเต็มวงเงินแล้ว
                            LtServerMessagecoll.Text += WebUtil.ErrorMessage(ex);
                        }

                        try
                        {//สิทธิ์ค้ำที่ใช้ไปของสัญญาเงินกู้
                            checkcollmancount = wcf.NShrlon.of_checkcollmancount(state.SsWsPass, state.SsCoopControl, ref_collno, member_no, loantype_code, "", "");
                            flag_collmancount = 1;
                        }
                        catch
                        {//สิทธิ์ค้ำประกันเต็มสิทธิ์ค้ำแล้ว
                            flag_collmancount = 0;
                            LtServerMessagecoll.Text += WebUtil.ErrorMessage("สมาชิก " + ref_collno + "สิทธิ์ค้ำประกันเต็มสิทธิ์ค้ำแล้ว ไม่สามารถค้ำประกันได้");
                        }

                        try
                        {//สิทธิ์ค้ำที่ใช้ไปของใบคำขอ
                            collusecontamt = wcf.NShrlon.of_getcollusecontamt(state.SsWsPass, state.SsCoopControl, ref_collno, loantype_code, loancolltype_code, "", "");
                        }
                        catch (Exception ex) { ex.ToString(); }

                        try
                        {//เช็คจำนวนสัญญาที่สามารถใช้ค้ำได้
                            collusereqamt = wcf.NShrlon.of_getcollusereqamt(state.SsWsPass, state.SsCoopControl, ref_collno, loantype_code, loancolltype_code, "");
                        }
                        catch (Exception ex) { ex.ToString(); }

                        try
                        {
                            collbalance_amt = (collpermiss - collusecontamt - collusereqamt) * percent / 100;

                            //ถ้า สิทธิ์ค้ำคงเหลือ มากกว่า สิทธิ์ค้ำสูงสุดของสัญญานี้ ให้ สิทธิ์ค้ำ = สิทธิ์ค้ำสูงสุด
                            if (collbalance_amt > maxcoll) { collactive_amt = maxcoll; }
                            else { collactive_amt = collbalance_amt; }

                            //---
                            if (collbalance_amt > 0)
                            {
                                //เคสการทางกรณีเปลี่ยน ยอดค้ำต้องได้ตามยอดคนค้ำเดิมที่แทนที่ EditBy_Cherry16022017
                                if (state.SsCoopControl == "022001") 
                                {
                                    collactive_amt = dsColl.DATA[collrow].COLLACTIVE_AMT;
                                    collactive_percent = dsColl.DATA[collrow].COLLACTIVE_PERCENT;

                                    dsColl.DATA[collrow].COLLBALANCE_AMT = collbalance_amt;
                                    dsColl.DATA[collrow].COLLACTIVE_AMT = collactive_amt;
                                    dsColl.DATA[collrow].COLLACTIVE_PERCENT = collactive_percent;
                                    dsColl.DATA[collrow].COLLUSED_AMT = collusecontamt;
                                    dsColl.DATA[collrow].COLLBASE_PERCENT = percent;
                                }
                                else
                                {
                                    dsColl.DATA[collrow].COLLBALANCE_AMT = collbalance_amt;
                                    dsColl.DATA[collrow].COLLACTIVE_AMT = collactive_amt;
                                    dsColl.DATA[collrow].COLLACTIVE_PERCENT = collactive_percent;
                                    dsColl.DATA[collrow].COLLUSED_AMT = collusecontamt;
                                    dsColl.DATA[collrow].COLLBASE_PERCENT = percent;
                                }
                            }
                            else
                            {
                                dsColl.DATA[collrow].REF_COLLNO = "";
                                dsColl.DATA[collrow].DESCRIPTION = "";
                                dsColl.DATA[collrow].COLLBALANCE_AMT = 0;
                                dsColl.DATA[collrow].COLLACTIVE_AMT = 0;
                                dsColl.DATA[collrow].COLLACTIVE_PERCENT = 0;                                
                            }
                        }
                        catch (Exception ex) { ex.ToString(); }
                    }
                    catch (Exception ex) { ex.ToString(); }

                    //ประเภทหลักประกันเ หุ้น
                    if (loancolltype_code == "02")
                    {

                    }
                    //หลักทรัพย์ค้ำประกัน
                    if (loancolltype_code == "04")
                    {

                    }
                    //ประเภทหลักประกัน ตำแหน่งค้ำ
                    if (loancolltype_code == "55")
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessagecoll.Text += WebUtil.ErrorMessage(ex);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                string loancontract_no = dsMain.DATA[0].LOANCONTRACT_NO.Trim();
                DateTime contadjust_date = dsMain.DATA[0].CONTADJUST_DATE;

                astr_contaj.loancontract_no = loancontract_no;
                astr_contaj.contaj_date = contadjust_date;
                astr_contaj.xml_contdetail = dsMain.ExportXml(); ;
                //tomy chktable ไม่มีไม่ต้อง save;
                if (chktable.Checked == true)
                {
                    astr_contaj.xml_contcoll = dsColl.ExportXml();
                }
                else { astr_contaj.xml_contcoll = ""; }

                astr_contaj.xml_oldcontcoll = dsCollOld.ExportXml() ;
                astr_contaj.xml_oldcontintspc = HdSpc.Value;
                astr_contaj.entry_id = state.SsUsername;

                //เงื่อนไข contintspc สามารถเพิ่มได้เมื่อ contint ฟิวด์ int_continttype = 3 (อัตราพิเศษเป็นช่วง)
               

                try
                {
                    int result = wcf.NShrlon.of_savereq_contadjust(state.SsWsPass, astr_contaj);
                    if (result == 1)
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                        dsMain.ResetRow();                       
                        dsColl.ResetRow();
                    }
                    else { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ ตรวจสอบอีกครั้ง"); }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            dsColl.DdLnCollType();
            SumDsColl();
        }

        public void SumDsColl()
        {
            decimal SumCOLLACTIVE_AMT = 0, SumCOLLACTIVE_PERCENT = 0;

            for (int i = 0; i < dsColl.RowCount; i++)
            {
                decimal COLLACTIVE_AMT = dsColl.DATA[i].COLLACTIVE_AMT;
                decimal COLLACTIVE_PERCENT = dsColl.DATA[i].COLLACTIVE_PERCENT;
                SumCOLLACTIVE_AMT += COLLACTIVE_AMT;
                SumCOLLACTIVE_PERCENT += COLLACTIVE_PERCENT;
            }
            dsColl.collactive_amt.Text = SumCOLLACTIVE_AMT.ToString("#,##0.00");
            dsColl.collactive_percent.Text = SumCOLLACTIVE_PERCENT.ToString("#,##0.00");
        }

        public double getMaxPeroidByCheckSalary() {

            decimal max_period_payment = 0;
            double result = 0;
            try {
                string member_no = dsMain.DATA[0].MEMBER_NO;
                string loancontract_no = dsMain.DATA[0].LOANCONTRACT_NO;
                string tmpkeepitemtype_code="", tmpcontract_no="";
                decimal principal_payment=0, interest_payment=0, item_payment = 0;
                decimal sum_keep = 0,prnc_cur_loan=0,int_cur_loan=0;
                string sql = "select coop_id,kpslip_no ,member_no,keepitemtype_code,loancontract_no,principal_payment,interest_payment,item_payment from kptempreceivedet where member_no = {0} and coop_id={1}";
                sql = WebUtil.SQLFormat(sql, member_no, state.SsCoopId);
                Sdt dt = WebUtil.QuerySdt(sql);
                while (dt.Next()) {
                    tmpcontract_no = dt.GetString("loancontract_no");
                    principal_payment = dt.GetDecimal("principal_payment");
                    interest_payment = dt.GetDecimal("interest_payment");
                    item_payment = dt.GetDecimal("item_payment");
                    if (loancontract_no != tmpcontract_no || !loancontract_no.Equals(tmpcontract_no))
                    {
                        sum_keep += item_payment;
                    }
                    else {
                        prnc_cur_loan = principal_payment;
                        int_cur_loan = interest_payment;
                    }
                }

                decimal salary_amount = 0;
                string sql_memb = "select nvl(salary_amount,0) as salary_amount from mbmembmaster where member_no={0} and coop_id={1}";
                sql_memb = WebUtil.SQLFormat(sql_memb, member_no, state.SsCoopControl);
                Sdt dt_memb = WebUtil.QuerySdt(sql_memb);
                if (dt_memb.Next())
                {
                    salary_amount = dt_memb.GetDecimal("salary_amount");
                }

                max_period_payment = salary_amount - (sum_keep + int_cur_loan + 2000);
                result = Math.Floor(Convert.ToDouble(max_period_payment) / 100.00) * 100.0;

            }catch(Exception ex){
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถคำนวณผ่อนชำระสูงสุดได้ กรุณาตรวจสอบ "+ex.Message);
            }

            return result;
        }

        public Int32 GetLnConstant()
        {
            Int32 fixpaycal = 0;
            string sql = "select fixpaycal_type from lnloanconstant where coop_id={0}";
            sql = WebUtil.SQLFormat(sql,state.SsCoopId);
            Sdt dt = WebUtil.QuerySdt(sql);
            while (dt.Next())
            {
                fixpaycal = Convert.ToInt32(dt.GetDecimal("fixpaycal_type"));
            }
            return fixpaycal;
        }

        public decimal GetIntrate(string coopid, string loancontractno)
        {
            decimal intrate = 0;
            string sql = "select ft_getlncontintrate(coop_id, loancontract_no, sysdate) as intrate from lncontmaster where coop_id = {0} and loancontract_no = {1}";
            sql = WebUtil.SQLFormat(sql,state.SsCoopId, loancontractno);
            Sdt dt = WebUtil.QuerySdt(sql);
            while (dt.Next())
            {
                intrate = dt.GetDecimal("intrate");
            }           
            return intrate;
        }
    }
}
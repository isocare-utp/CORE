using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using System.IO;
using System.Data;
using DataLibrary;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Services.Protocols;
//using CoreSavingLibrary.WcfReport;

namespace Saving.Applications.shrlon.ws_convert_txt2db_ctrl
{
    public partial class ws_convert_txt2db : PageWebSheet, WebSheet
    {
        //protected String postDatatxt;
        private n_shrlonClient shrlonService;

        [JsPostBack]
        public string postDatatxt { get; set; }
        [JsPostBack]
        public string Onclickbpost { get; set; }
        [JsPostBack]
        public string Onclickbnotpost { get; set; }
        [JsPostBack]
        public string Onclickpostagain { get; set; }
        [JsPostBack]
        public string PostMemberNo { get; set; }

        public void InitJsPostBack()
        {
            dsHeader.InitDsHeader(this);
            dsDetail.InitDsDetail(this);
            dsTailer.InitDsTailer(this);
            dsDetailFail.InitDsDetailFail(this);
            dsDetailFinish.InitDsDetailFinish(this);

            dsMain.InitDsMain(this);
            dsDetailShare.InitDsDetailShare(this);
            dsDetailLoan.InitDsDetailLoan(this);
            dsDetailEtc.InitDsDetailEtc(this);
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDatatxt")
            {
                JsPostDatatxt();
            }
            else if (eventArg == "Onclickbpost")
            {
                JsOnclickbpost();
            }
            else if (eventArg == "Onclickbnotpost")
            {
                JsOnclickbnotpost();
            }
            else if (eventArg == "Onclickpostagain")
            {
                JsOnclickpostagain();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        //อ่านข้อมูลไฟล์ txt 
        public void JsPostDatatxt()
        {
            try
            {
                dsHeader.ResetRow();
                dsTailer.ResetRow();
                dsDetail.ResetRow();
                dsDetailFail.ResetRow();
                dsDetailFinish.ResetRow();
                dsDetailLoan.ResetRow();
                dsDetailShare.ResetRow();
                dsDetailEtc.ResetRow();

                FileUpload fu = txtInput;
                string filename = txtInput.FileName.ToString().Trim();
                Stream stream = fu.PostedFile.InputStream;
                byte[] b = new byte[stream.Length];
                stream.Read(b, 0, (int)stream.Length);
                string txt = Encoding.GetEncoding("TIS-620").GetString(b); //sr.ReadToEnd();
                txt = Regex.Replace(txt, "\r", "");
                string[] lines = Regex.Split(txt, "\n");
                int txtLength;

                foreach (string line in lines)
                {
                    txtLength = line.Length;
                    if (txtLength > 3)
                    {
                        //บรรทัดแรก
                        if (line.Substring(0, 1) == "H")
                        {
                            DateTime datetransTH = DateTime.ParseExact(line.Substring(60, 8), "ddMMyyyy", new System.Globalization.CultureInfo("en-US"));
                            DateTime date_now = state.SsWorkDate;
                            string sqlf_bankname = "select bank_desc from cmucfbank where bank_code = {0}";
                            sqlf_bankname = WebUtil.SQLFormat(sqlf_bankname, line.Substring(7, 3));
                            Sdt dtBN = WebUtil.QuerySdt(sqlf_bankname);

                            if (dtBN.Next())
                            {
                                dsHeader.DATA[0].ls_bank = dtBN.GetString("bank_desc");
                            }
                            dsHeader.DATA[0].ls_effdate = String.Format("{0:dd/MM/yyyy}", date_now);
                            dsHeader.DATA[0].ls_effdate_ = line.Substring(60, 8);
                            dsHeader.DATA[0].ls_bank_code = line.Substring(7, 3);
                            dsHeader.DATA[0].ls_company = line.Substring(10, 10) + " : " + line.Substring(20, 40);
                            dsHeader.DATA[0].ls_filename = filename;
                        }
                        //บรรทัดสุดท้าย
                        else if (line.Substring(0, 1) == "T")
                        {
                            dsTailer.DATA[0].sum_row = Convert.ToDecimal(line.Substring(52, 6));
                            dsTailer.DATA[0].sum_amt = Convert.ToDecimal(line.Substring(39, 13)) / 100;

                            if (dsDetailFinish.RowCount > 0)
                            {
                                string Err_DetailFinish;
                                if (dsDetail.RowCount == 0)
                                {
                                    Err_DetailFinish = @"
                                <script type=""text/javascript"">   
                                    $(function() {   
                                        window.scrollBy(0,600);
                                        $('.Detail_F, .txtMemberFinish').show()      
                                        SetDisable(""#ctl00_ContentPlace_dsTailer_FormView1_b_post"", true) 
                                    });
                                </script>";
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Err_DetailFinish", Err_DetailFinish);
                                    LtServerMessage.Text = WebUtil.WarningMessage("ไฟล์เคยมีการผ่านรายการไปแล้วกรุณาตรวจสอบ");
                                }
                                else
                                {
                                    Err_DetailFinish = @"
                                <script type=""text/javascript"">   
                                    $(function() {   
                                        window.scrollBy(0,600);
                                        $('.Detail_F, .txtMemberFinish').show()   
                                    });
                                </script>";
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Err_DetailFinish", Err_DetailFinish);
                                    LtServerMessage.Text = WebUtil.WarningMessage("ไฟล์เคยมีการผ่านรายการไปแล้วกรุณาตรวจสอบ");
                                }
                            }
                        }
                        //รายละเอียดการทำรายการของสมาชิก
                        else if (line.Substring(0, 1) == "D")
                        {
                            DateTime ls_paytimeTH_ = DateTime.ParseExact(line.Substring(20, 8) + line.Substring(28, 6), "ddMMyyyyHHmmss", new System.Globalization.CultureInfo("en-US"));
                            string ls_paytimeTH = String.Format("{0:dd/MM/yyyy HH:mm:ss}", ls_paytimeTH_);
                            string member_no = WebUtil.MemberNoFormat(line.Substring(84, 20).Trim());

                            //string sqlf_chkbillpayment = "select * from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2} and reject_status = {3}";
                            string sqlf_chkbillpayment = "select * from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                            sqlf_chkbillpayment = WebUtil.SQLFormat(sqlf_chkbillpayment, member_no, line.Substring(1, 6), filename);
                            Sdt dtBPM = WebUtil.QuerySdt(sqlf_chkbillpayment);

                            int rowDT, rowCM;

                            if (dtBPM.Next())
                            {
                                //เคยมีการผ่านรายการมาแล้ว
                                dsDetailFinish.InsertLastRow();
                                rowCM = dsDetailFinish.RowCount - 1;
                                dsDetailFinish.DATA[rowCM].ls_memnoFinish = member_no;
                                dsDetailFinish.DATA[rowCM].ls_cusnameFinish = line.Substring(34, 50).Trim();
                                dsDetailFinish.DATA[rowCM].ls_trnscodeFinish = line.Substring(153, 3);
                                dsDetailFinish.DATA[rowCM].ls_paydateFinish = ls_paytimeTH;
                                dsDetailFinish.DATA[rowCM].ls_ref1Finish = line.Substring(84, 20).Trim();
                                dsDetailFinish.DATA[rowCM].ls_ref2Finish = line.Substring(104, 20).Trim();
                                dsDetailFinish.DATA[rowCM].ldc_trnsamtFinish = Convert.ToDecimal(line.Substring(163, 13)) / 100;
                                dsDetailFinish.DATA[rowCM].ls_chqbankFinish = line.Substring(176, 3);
                                dsDetailFinish.DATA[rowCM].ls_chqnoFinish = line.Substring(156, 7);
                                dsDetailFinish.DATA[rowCM].ls_filedocnoFinish = dsHeader.DATA[0].ls_effdate_;
                                dsDetailFinish.DATA[rowCM].ls_trnsnoFinish = line.Substring(1, 6);
                                dsDetailFinish.DATA[rowCM].ls_bankcodeFinish = line.Substring(7, 3);
                                dsDetailFinish.DATA[rowCM].ls_branchcodeFinish = line.Substring(144, 4);
                                dsDetailFinish.DATA[rowCM].ls_tellernoFinish = line.Substring(148, 4);
                                dsDetailFinish.DATA[rowCM].ls_trnstypeFinish = line.Substring(152, 1);
                                dsDetailFinish.DATA[rowCM].ls_paytimeFinish = ls_paytimeTH.Substring(11, 8);
                                dsDetailFinish.DATA[rowCM].ls_accountnoFinish = line.Substring(10, 10);
                                dsDetailFinish.DATA[rowCM].chk_memberFinish = "T";
                            }
                            else
                            {
                                string sqlf = "select member_no,accum_interest,membgroup_code from mbmembmaster where coop_id = {0} and member_no = {1}";
                                sqlf = WebUtil.SQLFormat(sqlf, state.SsCoopControl, member_no);
                                Sdt dt = WebUtil.QuerySdt(sqlf);

                                if (dt.Next())
                                {
                                    //เลขทะเบียนสมาชิกนี้มีข้อมูลในระบบจริง;
                                    dsDetail.InsertLastRow();
                                    rowDT = dsDetail.RowCount - 1;
                                    dsDetail.DATA[rowDT].ls_memno = member_no;
                                    dsDetail.DATA[rowDT].ls_cusname = line.Substring(34, 50).Trim();
                                    dsDetail.DATA[rowDT].ls_trnscode = line.Substring(153, 3);
                                    dsDetail.DATA[rowDT].ls_paydate = ls_paytimeTH;
                                    dsDetail.DATA[rowDT].ls_ref1 = line.Substring(84, 20).Trim();
                                    dsDetail.DATA[rowDT].ls_ref2 = line.Substring(104, 20).Trim();
                                    dsDetail.DATA[rowDT].ldc_trnsamt = Convert.ToDecimal(line.Substring(163, 13)) / 100;
                                    dsDetail.DATA[rowDT].ls_chqbank = line.Substring(176, 3);
                                    dsDetail.DATA[rowDT].ls_chqno = line.Substring(156, 7);
                                    dsDetail.DATA[rowDT].ls_filedocno = dsHeader.DATA[0].ls_effdate_;
                                    dsDetail.DATA[rowDT].ls_trnsno = line.Substring(1, 6);
                                    dsDetail.DATA[rowDT].ls_bankcode = line.Substring(7, 3);
                                    dsDetail.DATA[rowDT].ls_branchcode = line.Substring(144, 4);
                                    dsDetail.DATA[rowDT].ls_tellerno = line.Substring(148, 4);
                                    dsDetail.DATA[rowDT].ls_trnstype = line.Substring(152, 1);
                                    dsDetail.DATA[rowDT].ls_paytime = ls_paytimeTH.Substring(11, 8);
                                    dsDetail.DATA[rowDT].ls_accountno = line.Substring(10, 10);
                                    dsDetail.DATA[rowDT].ldc_intallacc = dt.GetDecimal("accum_interest");
                                    dsDetail.DATA[rowDT].membgroup_code = dt.GetString("membgroup_code").Substring(0, 8);
                                    dsDetail.DATA[rowDT].chk_member = "T";
                                }
                                else
                                {
                                    //เมื่อไม่พบทะเบียนสมาชิกในฐานข้อมูล;
                                    dsDetail.InsertLastRow();
                                    rowDT = dsDetail.RowCount - 1;
                                    dsDetail.DATA[rowDT].ls_memno = member_no;
                                    dsDetail.DATA[rowDT].ls_cusname = line.Substring(34, 50).Trim();
                                    dsDetail.DATA[rowDT].ls_trnscode = line.Substring(153, 3);
                                    dsDetail.DATA[rowDT].ls_paydate = ls_paytimeTH;
                                    dsDetail.DATA[rowDT].ls_ref1 = line.Substring(84, 20).Trim();
                                    dsDetail.DATA[rowDT].ls_ref2 = line.Substring(104, 20).Trim();
                                    dsDetail.DATA[rowDT].ldc_trnsamt = Convert.ToDecimal(line.Substring(163, 13)) / 100;
                                    dsDetail.DATA[rowDT].ls_chqbank = line.Substring(176, 3);
                                    dsDetail.DATA[rowDT].ls_chqno = line.Substring(156, 7);
                                    dsDetail.DATA[rowDT].ls_filedocno = dsHeader.DATA[0].ls_effdate_;
                                    dsDetail.DATA[rowDT].ls_trnsno = line.Substring(1, 6);
                                    dsDetail.DATA[rowDT].ls_bankcode = line.Substring(7, 3);
                                    dsDetail.DATA[rowDT].ls_branchcode = line.Substring(144, 4);
                                    dsDetail.DATA[rowDT].ls_tellerno = line.Substring(148, 4);
                                    dsDetail.DATA[rowDT].ls_trnstype = line.Substring(152, 1);
                                    dsDetail.DATA[rowDT].ls_paytime = ls_paytimeTH.Substring(11, 8);
                                    dsDetail.DATA[rowDT].ls_accountno = line.Substring(10, 10);
                                    dsDetail.DATA[rowDT].ldc_intallacc = 0;
                                    dsDetail.DATA[rowDT].membgroup_code = dt.GetString("membgroup_code");
                                    dsDetail.DATA[rowDT].chk_member = "F";

                                    //ข้อมูลที่ผิดพลาดให้เก็บไว้ใน dsDetailFail
                                    dsDetailFail.InsertLastRow();
                                    int row = dsDetailFail.RowCount - 1;
                                    dsDetailFail.DATA[row].ls_memnoFail = member_no;
                                    dsDetailFail.DATA[row].ls_cusnameFail = line.Substring(34, 50).Trim();
                                    dsDetailFail.DATA[row].ls_trnscodeFail = line.Substring(153, 3);
                                    dsDetailFail.DATA[row].ls_paydateFail = ls_paytimeTH.ToString();
                                    dsDetailFail.DATA[row].ls_ref1Fail = line.Substring(84, 20).Trim();
                                    dsDetailFail.DATA[row].ls_ref2Fail = line.Substring(104, 20).Trim();
                                    dsDetailFail.DATA[row].ldc_trnsamtFail = Convert.ToDecimal(line.Substring(163, 13)) / 100;
                                    dsDetailFail.DATA[row].ls_chqbankFail = line.Substring(176, 3);
                                    dsDetailFail.DATA[row].ls_chqnoFail = line.Substring(156, 7);
                                    dsDetailFail.DATA[row].ls_filedocnoFail = dsHeader.DATA[0].ls_effdate_;
                                    dsDetailFail.DATA[row].ls_trnsnoFail = line.Substring(1, 6);
                                    dsDetailFail.DATA[row].ls_bankcodeFail = line.Substring(7, 3);
                                    dsDetailFail.DATA[row].ls_branchcodeFail = line.Substring(144, 4);
                                    dsDetailFail.DATA[row].ls_tellernoFail = line.Substring(148, 4);
                                    dsDetailFail.DATA[row].ls_trnstypeFail = line.Substring(152, 1);
                                    dsDetailFail.DATA[row].ls_paytimeFail = ls_paytimeTH.ToString().Substring(11, 8);
                                    dsDetailFail.DATA[row].ls_accountnoFail = line.Substring(10, 10);
                                    dsDetailFail.DATA[row].ldc_intallaccFail = 0;
                                    dsDetailFail.DATA[row].membgroup_codeFail = dt.GetString("membgroup_code");
                                    dsDetailFail.DATA[row].chk_memberFail = "R";
                                }
                            }
                        }
                        else
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถตรวจสอบไฟล์ได้");
                        }
                    }
                }

                //นับแถวและหาผลรวม dsTailer
                decimal sum_temp = 0, sum_amt = 0;

                for (int ii = 0; ii < dsDetail.RowCount; ii++)
                {
                    sum_temp = dsDetail.DATA[ii].ldc_trnsamt;
                    sum_amt += sum_temp;
                    dsTailer.DATA[0].sum_amt = sum_amt;
                    dsTailer.DATA[0].sum_row = (ii + 1);
                }

                //เช็คว่ามีทะเบียนสมาชิกคนไหนที่ไม่มีในระบบ
                if (dsDetailFail.RowCount > 0)
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ทะเบียนสมาชิกจำนวน " + dsDetailFail.RowCount + " รายการไม่มีในระบบ");
                }
            }
            catch (Exception eX)
            {
                //เอาไว้ดู err เวลามีปัญหาตอน import
                LtServerMessage.Text = WebUtil.ErrorMessage(eX);
            }
        }

        //ทำงานเมื่อกดผ่านรายการและเลือกธนาคารที่ต้องการผ่านแล้ว
        public void JsOnclickbpost()
        {
            ExecuteDataSource ex = new ExecuteDataSource(this);
            ExecuteDataSource bx = new ExecuteDataSource(this);
            bool chk = true, chk_err = true;
            int count_order_sha = 1;

            for (int ii = 0; ii < dsDetail.RowCount && chk_err; ii++)
            {
                if (dsDetail.DATA[ii].chk_member == "T" && dsDetail.DATA[ii].reject_status == "1")
                {
                    string ls_filename = dsHeader.DATA[0].ls_filename;
                    string ls_memno = dsDetail.DATA[ii].ls_memno;
                    string ls_ref2 = dsDetail.DATA[ii].ls_ref2;
                    if (ls_ref2.Length < 3)
                    {
                        ls_ref2 = "   ";
                    }
                    DateTime date_now = state.SsWorkDate;
                    string is_entry = state.SsUsername;
                    string coop_id = state.SsCoopControl;
                    decimal ldc_trnsamt = dsDetail.DATA[ii].ldc_trnsamt;
                    //รหัสธนาคาร
                    string ls_tofromaccid = dsTailer.DATA[0].ls_tofromaccid;

                    //001	เปิดบัญชีเงินฝาก    //002	ฝาก             
                    //003	ซื้อหุ้น            
                    //004	ชำระหนี้ฉุกเฉิน    //005	ชำระหนี้สามัญ     //006	ชำระหนี้พิเศษ      
                    //007	สมัครสมาชิกสมทบ

                    #region post2dept
                    if (ls_ref2.Substring(0, 3) == "001" || ls_ref2.Substring(0, 3) == "002")
                    {
                        try
                        {
                            string ls_cusref2 = dsDetail.DATA[ii].ls_ref2;
                            DateTime ldtm_payment = DateTime.ParseExact(dsDetail.DATA[ii].ls_paydate, "dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
                            ldtm_payment = ldtm_payment.Date;
                            string ls_accid = ls_ref2.Substring(3);
                            string ls_expcode, ls_branchid, ls_branchacc, ref_slipno, system_code;
                            decimal li_chk, li_year, li_seq;
                            string ls_cusname = dsDetail.DATA[ii].ls_cusname;
                            string ls_trnscode = dsDetail.DATA[ii].ls_trnscode;
                            string ls_ref1 = dsDetail.DATA[ii].ls_ref1;
                            string ls_chqbank = dsDetail.DATA[ii].ls_chqbank;
                            string ls_chqno = dsDetail.DATA[ii].ls_chqno;
                            string ls_refdocno = dsDetail.DATA[ii].ls_filedocno;
                            string ls_trnsno = dsDetail.DATA[ii].ls_trnsno;
                            string ls_bankcode = dsDetail.DATA[ii].ls_bankcode;
                            string ls_branchcode = dsDetail.DATA[ii].ls_branchcode;
                            string ls_tellerno = dsDetail.DATA[ii].ls_tellerno;
                            string ls_trnstype = dsDetail.DATA[ii].ls_trnstype;
                            string ls_paytime = dsDetail.DATA[ii].ls_paytime;
                            string ls_accountno = dsDetail.DATA[ii].ls_accountno;

                            string sql_count = "select count(1) as li_chk from dpdeptmaster where deptaccount_no = {0} and member_No = {1}";
                            sql_count = WebUtil.SQLFormat(sql_count, ls_accid, ls_memno);
                            Sdt dt_count = WebUtil.QuerySdt(sql_count);

                            if (dt_count.Next())
                            {
                                li_chk = dt_count.GetDecimal("li_chk");

                                if (li_chk < 1)
                                {
                                    //**** billpayment list **** false
                                    //---------------------------------
                                    //insert billpayment
                                    string sqlf_chkbillpayment001 = "select * from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                                    sqlf_chkbillpayment001 = WebUtil.SQLFormat(sqlf_chkbillpayment001, ls_memno, ls_trnsno, ls_filename);
                                    Sdt dtBPM001 = WebUtil.QuerySdt(sqlf_chkbillpayment001);

                                    if (dtBPM001.Next())
                                    {
                                        string sql_billpayment = @"update billpayment set file_docno = {0}, transaction_no = {1}, bank_code = {2}, 
                                    branch_code = {3}, teller_no = {4}, member_no = {5}, account_no = {6}, transaction_type = {7}, 
                                    transaction_code = {8}, payment_date = {9}, payment_time = {10}, customer_name = {11}, 
                                    customer_ref1 = {12}, customer_ref2 = {13}, cheque_bank = {14}, cheque_no = {15}, 
                                    transaction_amt = {16}, reject_status = {17}, imp_date = {18}, location_file = {19}, 
                                    ref_docno = {20}, interest_payment = {21}, principal_payment = {22}, coop_id = {23} where member_no = {24} and transaction_no = {25} and location_file = {19}";
                                        object[] argslist_billpayment = new object[] {
                                    ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                    ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                    ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id, ls_memno, ls_trnsno
                                    };

                                        sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                        bx.SQL.Add(sql_billpayment);
                                        bx.Execute();
                                        bx.SQL.Clear();
                                    }
                                    else
                                    {
                                        string sql_billpayment = @"insert into billpayment(file_docno, transaction_no, bank_code, 
                                    branch_code, teller_no, member_no, account_no, transaction_type, transaction_code, payment_date, 
                                    payment_time, customer_name, customer_ref1, customer_ref2, cheque_bank, cheque_no, transaction_amt, 
                                    reject_status, imp_date, location_file, ref_docno, interest_payment, principal_payment, coop_id) 
                                    values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},
                                    {11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23})";
                                        object[] argslist_billpayment = new object[] {
                                    ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                    ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                    ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id
                                    };

                                        sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                        bx.SQL.Add(sql_billpayment);
                                        bx.Execute();
                                        bx.SQL.Clear();
                                    }
                                    //**** จบ billpayment ****

                                    chk = false;
                                    string Err_001002_li_chk = @"
                                    <script type=""text/javascript"">   
                                        $(function() {   
                                            $('.td_row').each(function () {
                                            var x = $(this).find('.num_row').val()
                                                if(x == " + (ii + 1) + @"){
                                                    $('#ctl00_ContentPlace_dsDetail_Repeater1_ctl" + string.Format("{0:00}", ii) + @"_chk_member').val(""F"")
                                                }
                                            });
                                            chk_errAlerttxt();
                                            chk_chgRGB('.ls_ref2_');
                                        });
                                    </script>";
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Err_001002_li_chk" + string.Format("{0:00}", ii), Err_001002_li_chk);
                                    LtServerMessage.Text = WebUtil.WarningMessage("โปรดตรวจสอบ เลขที่บัญชี : " + ls_accid + @" สมาชิกรหัส " + ls_memno);

                                    //เก็บข้อมูลรายการที่ผิดพลาด
                                    dsDetailFail.InsertLastRow();
                                    int row = dsDetailFail.RowCount - 1;
                                    dsDetailFail.DATA[row].ls_memnoFail = dsDetail.DATA[ii].ls_memno;
                                    dsDetailFail.DATA[row].ls_cusnameFail = dsDetail.DATA[ii].ls_cusname;
                                    dsDetailFail.DATA[row].ls_trnscodeFail = dsDetail.DATA[ii].ls_trnscode;
                                    dsDetailFail.DATA[row].ls_paydateFail = dsDetail.DATA[ii].ls_paydate;
                                    dsDetailFail.DATA[row].ls_ref1Fail = dsDetail.DATA[ii].ls_ref1;
                                    dsDetailFail.DATA[row].ls_ref2Fail = dsDetail.DATA[ii].ls_ref2;
                                    dsDetailFail.DATA[row].ldc_trnsamtFail = dsDetail.DATA[ii].ldc_trnsamt;
                                    dsDetailFail.DATA[row].ls_chqbankFail = dsDetail.DATA[ii].ls_chqbank;
                                    dsDetailFail.DATA[row].ls_chqnoFail = dsDetail.DATA[ii].ls_chqno;
                                    dsDetailFail.DATA[row].ls_filedocnoFail = dsDetail.DATA[ii].ls_filedocno;
                                    dsDetailFail.DATA[row].ls_trnsnoFail = dsDetail.DATA[ii].ls_trnsno;
                                    dsDetailFail.DATA[row].ls_bankcodeFail = dsDetail.DATA[ii].ls_bankcode;
                                    dsDetailFail.DATA[row].ls_branchcodeFail = dsDetail.DATA[ii].ls_branchcode;
                                    dsDetailFail.DATA[row].ls_tellernoFail = dsDetail.DATA[ii].ls_tellerno;
                                    dsDetailFail.DATA[row].ls_trnstypeFail = dsDetail.DATA[ii].ls_trnstype;
                                    dsDetailFail.DATA[row].ls_paytimeFail = dsDetail.DATA[ii].ls_paytime;
                                    dsDetailFail.DATA[row].ls_accountnoFail = dsDetail.DATA[ii].ls_accountno;
                                    dsDetailFail.DATA[row].ldc_intallaccFail = dsDetail.DATA[ii].ldc_intallacc;
                                    dsDetailFail.DATA[row].membgroup_codeFail = dsDetail.DATA[ii].membgroup_code;
                                    dsDetailFail.DATA[row].chk_memberFail = "R";
                                }
                                else
                                {
                                    li_year = 1111;
                                    ls_expcode = "TRS";
                                    ref_slipno = "BIL";
                                    system_code = "BIL";
                                    ls_branchid = "001";
                                    ls_branchacc = "008001";

                                    string sql_maxdept = @"select max(seq_no) as li_seq from dpdepttran 
                                    where tran_year = {0} 
                                    and deptaccount_no = {1} 
                                    and member_no = {2}
                                    and tran_date = {3}  
                                    and branch_operate = {4}
                                    and system_code = 'BIL'";
                                    sql_maxdept = WebUtil.SQLFormat(sql_maxdept, li_year, ls_accid, ls_memno, ldtm_payment, ls_branchid);
                                    Sdt dt_maxdept = WebUtil.QuerySdt(sql_maxdept);

                                    if (dt_maxdept.Next())
                                    {
                                        li_seq = dt_maxdept.GetDecimal("li_seq");
                                        if (ldc_trnsamt > 0)
                                        {
                                            DateTime ldtm_payment_new = ldtm_payment.Date;
                                            //insert dpdepttran
                                            li_seq ++;
                                            string sql_dpdepttran = @"insert into dpdepttran(memcoop_id, tran_year, deptaccount_no, member_no,
                                            seq_no, tran_date, deptitem_amt, tran_status, lncont_no, ref_slipno, coop_id, 
                                            branch_operate, system_code) values({0},{1},{2},{3},{4},{5},{6},{7},{8},
                                            {9},{10},{11},{12})";
                                            object[] argslist_dpdepttran = new object[] { coop_id, li_year, ls_accid, ls_memno, li_seq, 
                                            ldtm_payment_new, ldc_trnsamt, 0, ls_tofromaccid , ref_slipno, ls_branchacc, ls_branchid, system_code };

                                            sql_dpdepttran = WebUtil.SQLFormat(sql_dpdepttran, argslist_dpdepttran);
                                            bx.SQL.Add(sql_dpdepttran);
                                            bx.Execute();
                                            bx.SQL.Clear();
                                            //ex.SQL.Add(sql_dpdepttran);
                                            //**** จบ dpdepttran ****

                                            //**** billpayment list **** true
                                            //---------------------------------
                                            //insert billpayment
                                            string sqlf_chkbillpayment = "select member_no from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                                            sqlf_chkbillpayment = WebUtil.SQLFormat(sqlf_chkbillpayment, ls_memno, ls_trnsno, ls_filename);
                                            Sdt dtBPM = WebUtil.QuerySdt(sqlf_chkbillpayment);

                                            if (dtBPM.Next())
                                            {
                                                string sql_billpayment = @"update billpayment set file_docno = {0}, transaction_no = {1}, bank_code = {2}, 
                                            branch_code = {3}, teller_no = {4}, member_no = {5}, account_no = {6}, transaction_type = {7}, 
                                            transaction_code = {8}, payment_date = {9}, payment_time = {10}, customer_name = {11}, 
                                            customer_ref1 = {12}, customer_ref2 = {13}, cheque_bank = {14}, cheque_no = {15}, 
                                            transaction_amt = {16}, reject_status = {17}, imp_date = {18}, location_file = {19}, 
                                            ref_docno = {20}, interest_payment = {21}, principal_payment = {22}, coop_id = {23} where member_no = {24} and transaction_no = {25} and location_file = {19}";
                                                object[] argslist_billpayment = new object[] {
                                            ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                            ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                            ls_chqno, ldc_trnsamt, 1, date_now, ls_filename, "", 0, 0, coop_id, ls_memno, ls_trnsno
                                            };

                                                sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                bx.SQL.Add(sql_billpayment);
                                                bx.Execute();
                                                bx.SQL.Clear();
                                            }
                                            else
                                            {
                                                string sql_billpayment = @"insert into billpayment(file_docno, transaction_no, bank_code, 
                                            branch_code, teller_no, member_no, account_no, transaction_type, transaction_code, payment_date, 
                                            payment_time, customer_name, customer_ref1, customer_ref2, cheque_bank, cheque_no, transaction_amt, 
                                            reject_status, imp_date, location_file, ref_docno, interest_payment, principal_payment, coop_id) 
                                            values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},
                                            {11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23})";
                                                object[] argslist_billpayment = new object[] {
                                            ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                            ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                            ls_chqno, ldc_trnsamt, 1, date_now, ls_filename, "", 0, 0, coop_id
                                            };

                                                sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                bx.SQL.Add(sql_billpayment);
                                                bx.Execute();
                                                bx.SQL.Clear();
                                            }
                                            //**** จบ billpayment ****

                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception eX)
                        {
                            //เอาไว้ดู err เมื่อเกิดข้อผิดพลาดในส่วนของ 001 เปิดบัญชีเงินฝาก, 002 ฝาก      
                            LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                        }

                        //***** EndProcess dept *****
                    }
                    #endregion
                    #region post2shr
                    else if (ls_ref2.Substring(0, 3) == "003")
                    {
                        try
                        {
                            string ls_cusname = dsDetail.DATA[ii].ls_cusname;
                            string ls_trnscode = dsDetail.DATA[ii].ls_trnscode;
                            DateTime ldtm_payment = DateTime.ParseExact(dsDetail.DATA[ii].ls_paydate, "dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
                            DateTime ldtm_payment_new = ldtm_payment.Date;
                            string ls_ref1 = dsDetail.DATA[ii].ls_ref1;
                            string ls_chqbank = dsDetail.DATA[ii].ls_chqbank;
                            string ls_chqno = dsDetail.DATA[ii].ls_chqno;
                            string ls_refdocno = dsDetail.DATA[ii].ls_filedocno;
                            string ls_trnsno = dsDetail.DATA[ii].ls_trnsno;
                            string ls_bankcode = dsDetail.DATA[ii].ls_bankcode;
                            string ls_branchcode = dsDetail.DATA[ii].ls_branchcode;
                            string ls_tellerno = dsDetail.DATA[ii].ls_tellerno;
                            string ls_trnstype = dsDetail.DATA[ii].ls_trnstype;
                            string ls_paytime = dsDetail.DATA[ii].ls_paytime;
                            string ls_accountno = dsDetail.DATA[ii].ls_accountno;
                            decimal ldc_intallacc = dsDetail.DATA[ii].ldc_intallacc;
                            string membgroup_code = dsDetail.DATA[ii].membgroup_code;
                            decimal ldc_trnsamt_;
                            Boolean chkshr = true;

                            try
                            {

                                string sql_shamaster = "select * from shsharemaster where coop_id = {0} and member_no = {1}";
                                sql_shamaster = WebUtil.SQLFormat(sql_shamaster, state.SsCoopControl, ls_memno);
                                Sdt dt_shamaster = WebUtil.QuerySdt(sql_shamaster);

                                if (dt_shamaster.Next())
                                {
                                    string ls_sharetype = dt_shamaster.GetString("sharetype_code");
                                    decimal ldc_sharebf = dt_shamaster.GetDecimal("sharestk_amt");
                                    decimal ldc_sharevalue = 10;
                                    decimal li_seqno = dt_shamaster.GetDecimal("last_stm_no");
                                    li_seqno++;
                                    //li_seqno += count_order_sha;
                                    decimal li_period = dt_shamaster.GetDecimal("last_period");
                                    li_period++;
                                    //li_period += count_order_sha;
                                    string ls_shritem = "SPX";
                                    decimal ldc_sharestk = (ldc_sharebf * ldc_sharevalue) + ldc_trnsamt;
                                    decimal ldc_sharestk_ = (ldc_sharestk / ldc_sharevalue);
                                    string ls_itemdesc = "ซื้อหุ้น( Bill Payment )";

                                    string sql_maxshare = "select nvl(maxshare_hold,0) as maxshare_hold from shsharetype where sharetype_code = '01'";
                                    Sdt dt_maxshare = WebUtil.QuerySdt(sql_maxshare);

                                    if (dt_maxshare.Next())
                                    {
                                        decimal ldc_maxsharehold = dt_maxshare.GetDecimal("maxshare_hold");
                                        if (ldc_sharestk_ > ldc_maxsharehold)
                                        {
                                            //**** billpayment list **** false
                                            //---------------------------------
                                            //insert billpayment
                                            string sqlf_chkbillpayment003 = "select * from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                                            sqlf_chkbillpayment003 = WebUtil.SQLFormat(sqlf_chkbillpayment003, ls_memno, ls_trnsno, ls_filename);
                                            Sdt dtBPM003 = WebUtil.QuerySdt(sqlf_chkbillpayment003);

                                            if (dtBPM003.Next())
                                            {
                                                string sql_billpayment = @"update billpayment set file_docno = {0}, transaction_no = {1}, bank_code = {2}, 
                                        branch_code = {3}, teller_no = {4}, member_no = {5}, account_no = {6}, transaction_type = {7}, 
                                        transaction_code = {8}, payment_date = {9}, payment_time = {10}, customer_name = {11}, 
                                        customer_ref1 = {12}, customer_ref2 = {13}, cheque_bank = {14}, cheque_no = {15}, 
                                        transaction_amt = {16}, reject_status = {17}, imp_date = {18}, location_file = {19}, 
                                        ref_docno = {20}, interest_payment = {21}, principal_payment = {22}, coop_id = {23} where member_no = {24} and transaction_no = {25} ";
                                                object[] argslist_billpayment = new object[] {
                                        ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                        ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                        ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id, ls_memno, ls_trnsno
                                        };

                                                sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                bx.SQL.Add(sql_billpayment);
                                                bx.Execute();
                                                bx.SQL.Clear();
                                            }
                                            else
                                            {
                                                string sql_billpayment = @"insert into billpayment(file_docno, transaction_no, bank_code, 
                                        branch_code, teller_no, member_no, account_no, transaction_type, transaction_code, payment_date, 
                                        payment_time, customer_name, customer_ref1, customer_ref2, cheque_bank, cheque_no, transaction_amt, 
                                        reject_status, imp_date, location_file, ref_docno, interest_payment, principal_payment, coop_id) 
                                        values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},
                                        {11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23})";
                                                object[] argslist_billpayment = new object[] {
                                        ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                        ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                        ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id
                                        };

                                                sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                bx.SQL.Add(sql_billpayment);
                                                bx.Execute();
                                                bx.SQL.Clear();
                                            }
                                            //**** จบ billpayment ****
                                            chk = false;
                                            string Err_003_maxsharehold = @"
                                        <script type=""text/javascript"">   
                                            $(function() {   
                                                $('.td_row').each(function () {
                                                var x = $(this).find('.num_row').val()
                                                    if(x == " + (ii + 1) + @"){
                                                        $('#ctl00_ContentPlace_dsDetail_Repeater1_ctl" + string.Format("{0:00}", ii) + @"_chk_member').val(""F"")
                                                    }
                                                });
                                                chk_errAlerttxt();
                                            });
                                        </script>";
                                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Err_003_maxsharehold", Err_003_maxsharehold);
                                            LtServerMessage.Text = WebUtil.WarningMessage("ไม่สามารถทำรายการให้สมาชิกรหัส : " + ls_memno + " ได้เนื่องจากถ้าทำรายการหุ้นสะสมจะเกิน");

                                            //เก็บข้อมูลรายการที่ผิดพลาด
                                            dsDetailFail.InsertLastRow();
                                            int row = dsDetailFail.RowCount - 1;
                                            dsDetailFail.DATA[row].ls_memnoFail = dsDetail.DATA[ii].ls_memno;
                                            dsDetailFail.DATA[row].ls_cusnameFail = dsDetail.DATA[ii].ls_cusname;
                                            dsDetailFail.DATA[row].ls_trnscodeFail = dsDetail.DATA[ii].ls_trnscode;
                                            dsDetailFail.DATA[row].ls_paydateFail = dsDetail.DATA[ii].ls_paydate;
                                            dsDetailFail.DATA[row].ls_ref1Fail = dsDetail.DATA[ii].ls_ref1;
                                            dsDetailFail.DATA[row].ls_ref2Fail = dsDetail.DATA[ii].ls_ref2;
                                            dsDetailFail.DATA[row].ldc_trnsamtFail = dsDetail.DATA[ii].ldc_trnsamt;
                                            dsDetailFail.DATA[row].ls_chqbankFail = dsDetail.DATA[ii].ls_chqbank;
                                            dsDetailFail.DATA[row].ls_chqnoFail = dsDetail.DATA[ii].ls_chqno;
                                            dsDetailFail.DATA[row].ls_filedocnoFail = dsDetail.DATA[ii].ls_filedocno;
                                            dsDetailFail.DATA[row].ls_trnsnoFail = dsDetail.DATA[ii].ls_trnsno;
                                            dsDetailFail.DATA[row].ls_bankcodeFail = dsDetail.DATA[ii].ls_bankcode;
                                            dsDetailFail.DATA[row].ls_branchcodeFail = dsDetail.DATA[ii].ls_branchcode;
                                            dsDetailFail.DATA[row].ls_tellernoFail = dsDetail.DATA[ii].ls_tellerno;
                                            dsDetailFail.DATA[row].ls_trnstypeFail = dsDetail.DATA[ii].ls_trnstype;
                                            dsDetailFail.DATA[row].ls_paytimeFail = dsDetail.DATA[ii].ls_paytime;
                                            dsDetailFail.DATA[row].ls_accountnoFail = dsDetail.DATA[ii].ls_accountno;
                                            dsDetailFail.DATA[row].ldc_intallaccFail = dsDetail.DATA[ii].ldc_intallacc;
                                            dsDetailFail.DATA[row].membgroup_codeFail = dsDetail.DATA[ii].membgroup_code;
                                            dsDetailFail.DATA[row].chk_memberFail = "R";
                                        }
                                        else
                                        {
                                            string ls_money = "CBT";

                                            string ls_slipno = Get_NumberDOC("CMSLIPNO");
                                            string ls_receiptno = Get_NumberDOC("CMSLIPRECEIPT_BILL");

                                            //**** สร้าง Slip ****
                                            //---------------------------------
                                            //insert slslipayin
                                            string sql_slslipayin = @"insert into slslippayin(coop_id, payinslip_no, memcoop_id, member_no, 
                                    document_no, sliptype_code, slip_date, operate_date, sharestkbf_value, sharestk_value, 
                                    intaccum_amt, moneytype_code, accid_flag, tofrom_accid, ref_system, ref_slipno, slip_amt, 
                                    slip_status, membgroup_code, entry_id, entry_date) values({0},{1},{2},{3},{4},{5},{6},{7},{8},
                                    {9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20})";
                                            object[] argslist_slslipayin = new object[] { coop_id, ls_slipno, coop_id, ls_memno, ls_receiptno
                                    , "PX", date_now, ldtm_payment_new, (ldc_sharebf * ldc_sharevalue), (ldc_sharestk_ * ldc_sharevalue)
                                    , ldc_intallacc, ls_money, 1, ls_tofromaccid, "SHR", ls_refdocno, ldc_trnsamt, 1, membgroup_code 
                                    , is_entry, date_now };

                                            sql_slslipayin = WebUtil.SQLFormat(sql_slslipayin, argslist_slslipayin);
                                            bx.SQL.Add(sql_slslipayin);
                                            bx.Execute();
                                            bx.SQL.Clear();
                                            //ex.SQL.Add(sql_slslipayin);

                                            //---------------------------------
                                            //insert slslippayindet
                                            string sql_slslippayindet = @"insert into slslippayindet(coop_id, payinslip_no, slipitemtype_code,
                                    seq_no, operate_flag, shrlontype_code, slipitem_desc, period, item_payamt, item_balance, 
                                    calint_to, stm_itemtype, bfperiod, bfshrcont_balamt, bfshrcont_status, bfpayspec_method) values({0},
                                    {1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})";
                                            object[] argslist_slslippayindet = new object[] { 
                                    coop_id, ls_slipno, "SHR", 1, 1, "01", ls_itemdesc, li_period, ldc_trnsamt, 
                                    (ldc_sharestk_ * ldc_sharevalue) , ldtm_payment_new, ls_shritem, (li_period-1), (ldc_sharebf * ldc_sharevalue), 1, 1};

                                            sql_slslippayindet = WebUtil.SQLFormat(sql_slslippayindet, argslist_slslippayindet);
                                            bx.SQL.Add(sql_slslippayindet);
                                            bx.Execute();
                                            bx.SQL.Clear();
                                            //ex.SQL.Add(sql_slslippayindet);

                                            //---------------------------------
                                            //insert shsharestatement
                                            decimal max_seq_no = GetMax_Number("select max(seq_no) as max_value from shsharestatement where member_no = " + ls_memno + "");

                                            ldc_trnsamt_ = (ldc_trnsamt / ldc_sharevalue);
                                            string sql_shsharestatement = @"insert into shsharestatement(coop_id, member_no, sharetype_code, 
                                    seq_no,	slip_date, operate_date, account_date, ref_docno, shritemtype_code, period, share_amount, sharestk_amt, 
                                    shrarrearbf_amt, shrarrear_amt, moneytype_code, item_status, entry_id, entry_date) values({0},
                                    {1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17})";
                                            object[] argslist_shsharestatement = new object[] {
                                    coop_id, ls_memno, ls_sharetype, max_seq_no, date_now, ldtm_payment_new, date_now, ls_receiptno, ls_shritem, 
                                    li_period, ldc_trnsamt_, ldc_sharestk_, 0, 0, ls_money, 1, is_entry, ldtm_payment
                                    };

                                            sql_shsharestatement = WebUtil.SQLFormat(sql_shsharestatement, argslist_shsharestatement);
                                            bx.SQL.Add(sql_shsharestatement);
                                            bx.Execute();
                                            bx.SQL.Clear();
                                            //ex.SQL.Add(sql_shsharestatement);
                                            //**** จบ Slip ****

                                            //**** Master - Statement ****
                                            //---------------------------------
                                            //update shsharemaster
                                            string sql_shsharemaster = @"update shsharemaster set sharestk_amt = {0}, 
                                    last_stm_no = {1}, lastprocess_date = {2} where	member_no = {3}";
                                            object[] argslist_shsharemaster = new object[] {
                                    ldc_sharestk_, max_seq_no, ldtm_payment_new, ls_memno
                                    };

                                            sql_shsharemaster = WebUtil.SQLFormat(sql_shsharemaster, argslist_shsharemaster);
                                            bx.SQL.Add(sql_shsharemaster);
                                            bx.Execute();
                                            bx.SQL.Clear();
                                            //ex.SQL.Add(sql_shsharemaster);

                                            count_order_sha++;
                                            //**** จบ Master - Statement ****

                                            //**** billpayment list **** true
                                            //---------------------------------
                                            //insert billpayment
                                            string sqlf_chkbillpayment = "select member_no from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                                            sqlf_chkbillpayment = WebUtil.SQLFormat(sqlf_chkbillpayment, ls_memno, ls_trnsno, ls_filename);
                                            Sdt dtBPM = WebUtil.QuerySdt(sqlf_chkbillpayment);

                                            if (dtBPM.Next())
                                            {
                                                string sql_billpayment = @"update billpayment set file_docno = {0}, transaction_no = {1}, bank_code = {2}, 
                                        branch_code = {3}, teller_no = {4}, member_no = {5}, account_no = {6}, transaction_type = {7}, 
                                        transaction_code = {8}, payment_date = {9}, payment_time = {10}, customer_name = {11}, 
                                        customer_ref1 = {12}, customer_ref2 = {13}, cheque_bank = {14}, cheque_no = {15}, 
                                        transaction_amt = {16}, reject_status = {17}, imp_date = {18}, location_file = {19}, 
                                        ref_docno = {20}, interest_payment = {21}, principal_payment = {22}, coop_id = {23} where member_no = {24} and transaction_no = {25} ";
                                                object[] argslist_billpayment = new object[] {
                                        ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                        ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                        ls_chqno, ldc_trnsamt, 1, date_now, ls_filename, "", 0, 0, coop_id, ls_memno, ls_trnsno
                                        };

                                                sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                bx.SQL.Add(sql_billpayment);
                                                bx.Execute();
                                                bx.SQL.Clear();
                                            }
                                            else
                                            {
                                                string sql_billpayment = @"insert into billpayment(file_docno, transaction_no, bank_code, 
                                        branch_code, teller_no, member_no, account_no, transaction_type, transaction_code, payment_date, 
                                        payment_time, customer_name, customer_ref1, customer_ref2, cheque_bank, cheque_no, transaction_amt, 
                                        reject_status, imp_date, location_file, ref_docno, interest_payment, principal_payment, coop_id) 
                                        values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},
                                        {11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23})";
                                                object[] argslist_billpayment = new object[] {
                                        ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                        ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                        ls_chqno, ldc_trnsamt, 1, date_now, ls_filename, "", 0, 0, coop_id
                                        };

                                                sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                bx.SQL.Add(sql_billpayment);
                                                bx.Execute();
                                                bx.SQL.Clear();
                                            }
                                            //**** จบ billpayment ****

                                        }
                                    }
                                }
                            }
                            catch (Exception csX)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage(csX);

                                if (chkshr)
                                {
                                    //**** billpayment list **** flase
                                    //---------------------------------
                                    //insert billpayment
                                    string sqlf_chkbillpayment = "select member_no from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                                    sqlf_chkbillpayment = WebUtil.SQLFormat(sqlf_chkbillpayment, ls_memno, ls_trnsno, ls_filename);
                                    Sdt dtBPM = WebUtil.QuerySdt(sqlf_chkbillpayment);

                                    if (dtBPM.Next())
                                    {
                                        string sql_billpayment = @"update billpayment set file_docno = {0}, transaction_no = {1}, bank_code = {2}, 
                                            branch_code = {3}, teller_no = {4}, member_no = {5}, account_no = {6}, transaction_type = {7}, 
                                            transaction_code = {8}, payment_date = {9}, payment_time = {10}, customer_name = {11}, 
                                            customer_ref1 = {12}, customer_ref2 = {13}, cheque_bank = {14}, cheque_no = {15}, 
                                            transaction_amt = {16}, reject_status = {17}, imp_date = {18}, location_file = {19}, 
                                            ref_docno = {20}, interest_payment = {21}, principal_payment = {22}, coop_id = {23} where member_no = {24} and transaction_no = {25} ";
                                        object[] argslist_billpayment = new object[] {
                                            ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                            ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                            ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id, ls_memno, ls_trnsno
                                            };

                                        sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                        bx.SQL.Add(sql_billpayment);
                                        bx.Execute();
                                        bx.SQL.Clear();
                                    }
                                    else
                                    {
                                        string sql_billpayment = @"insert into billpayment(file_docno, transaction_no, bank_code, 
                                            branch_code, teller_no, member_no, account_no, transaction_type, transaction_code, payment_date, 
                                            payment_time, customer_name, customer_ref1, customer_ref2, cheque_bank, cheque_no, transaction_amt, 
                                            reject_status, imp_date, location_file, ref_docno, interest_payment, principal_payment, coop_id) 
                                            values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},
                                            {11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23})";
                                        object[] argslist_billpayment = new object[] {
                                            ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                            ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                            ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id
                                            };

                                        sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                        bx.SQL.Add(sql_billpayment);
                                        bx.Execute();
                                        bx.SQL.Clear();
                                    }
                                    //**** จบ billpayment ****
                                }
                            }
                        }
                        catch (Exception eX)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                        }

                        //***** EndProcess share *****
                    }
                    #endregion
                    #region post2loan
                    else if (ls_ref2.Substring(0, 3) == "004" || ls_ref2.Substring(0, 3) == "005" || ls_ref2.Substring(0, 3) == "006")
                    {
                        try
                        {
                            string ls_cusref2 = dsDetail.DATA[ii].ls_ref2;
                            DateTime ldtm_payment = DateTime.ParseExact(dsDetail.DATA[ii].ls_paydate, "dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
                            string ls_accid = ls_ref2.Substring(3);
                            string ls_cusname = dsDetail.DATA[ii].ls_cusname;
                            string ls_trnscode = dsDetail.DATA[ii].ls_trnscode;
                            string ls_ref1 = dsDetail.DATA[ii].ls_ref1;
                            string ls_chqbank = dsDetail.DATA[ii].ls_chqbank;
                            string ls_chqno = dsDetail.DATA[ii].ls_chqno;
                            string ls_refdocno = dsDetail.DATA[ii].ls_filedocno;
                            string ls_trnsno = dsDetail.DATA[ii].ls_trnsno;
                            string ls_bankcode = dsDetail.DATA[ii].ls_bankcode;
                            string ls_branchcode = dsDetail.DATA[ii].ls_branchcode;
                            string ls_tellerno = dsDetail.DATA[ii].ls_tellerno;
                            string ls_trnstype = dsDetail.DATA[ii].ls_trnstype;
                            string ls_paytime = dsDetail.DATA[ii].ls_paytime;
                            string ls_accountno = dsDetail.DATA[ii].ls_accountno;
                            Boolean chkloan = true;

                            //ไม่ได้ตั้งใจจะเขียนแบบนี้นะ แต่จำเป็นต้องแก้ปัญหาเฉพาะหน้าไปก่อน

                            try
                            {
                                dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
                                dsMain.DATA[0].OPERATE_DATE = ldtm_payment;
                                dsMain.DATA[0].SLIPTYPE_CODE = "PX";
                                dsMain.DATA[0].ENTRY_ID = state.SsUsername;

                                InitLnRcv(dsDetail.DATA[ii].ls_memno, ls_accid, ldc_trnsamt, ldtm_payment);

                                dsMain.DATA[0].TOFROM_ACCID = ls_tofromaccid;
                                decimal PRINCIPAL_PAYAMT = 0, INTEREST_PAYAMT = 0;
                                int DetailLoanrow = dsDetailLoan.RowCount;

                                if (DetailLoanrow > 0)
                                {
                                    for (int i = 0; i < dsDetailLoan.RowCount; i++)
                                    {
                                        if (ls_accid.Trim() == dsDetailLoan.DATA[i].LOANCONTRACT_NO.Substring(2))
                                        {
                                            PRINCIPAL_PAYAMT = dsDetailLoan.DATA[i].PRINCIPAL_PAYAMT;
                                            INTEREST_PAYAMT = dsDetailLoan.DATA[i].INTEREST_PAYAMT;

                                            if (PRINCIPAL_PAYAMT > 0) { 
                                                str_slippayin strslip = new str_slippayin();
                                                strslip.coop_id = state.SsCoopId;
                                                strslip.entry_id = state.SsUsername;
                                                strslip.xml_sliphead = dsMain.ExportXml();
                                                strslip.xml_slipshr = "";
                                                strslip.xml_sliplon = dsDetailLoan.ExportXml();
                                                strslip.xml_slipetc = "";

                                                wcf.NShrlon.of_saveslip_payin(state.SsWsPass, ref strslip);

                                                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ ");

                                                //**** billpayment list **** true
                                                //---------------------------------
                                                //insert billpayment
                                                string sqlf_chkbillpayment = "select member_no from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                                                sqlf_chkbillpayment = WebUtil.SQLFormat(sqlf_chkbillpayment, ls_memno, ls_trnsno, ls_filename);
                                                Sdt dtBPM = WebUtil.QuerySdt(sqlf_chkbillpayment);

                                                if (dtBPM.Next())
                                                {
                                                    string sql_billpayment = @"update billpayment set file_docno = {0}, transaction_no = {1}, bank_code = {2}, 
                                                    branch_code = {3}, teller_no = {4}, member_no = {5}, account_no = {6}, transaction_type = {7}, 
                                                    transaction_code = {8}, payment_date = {9}, payment_time = {10}, customer_name = {11}, 
                                                    customer_ref1 = {12}, customer_ref2 = {13}, cheque_bank = {14}, cheque_no = {15}, 
                                                    transaction_amt = {16}, reject_status = {17}, imp_date = {18}, location_file = {19}, 
                                                    ref_docno = {20}, interest_payment = {21}, principal_payment = {22}, coop_id = {23} where member_no = {24} and transaction_no = {25} and location_file = {19}";
                                                            object[] argslist_billpayment = new object[] {
                                                    ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                                    ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                                    ls_chqno, ldc_trnsamt, 1, date_now, ls_filename, "", INTEREST_PAYAMT, PRINCIPAL_PAYAMT, coop_id, ls_memno, ls_trnsno
                                                    };
                                                    sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                    bx.SQL.Add(sql_billpayment);
                                                    bx.Execute();
                                                    bx.SQL.Clear();
                                                }
                                                else
                                                {
                                                    string sql_billpayment = @"insert into billpayment(file_docno, transaction_no, bank_code, 
                                                    branch_code, teller_no, member_no, account_no, transaction_type, transaction_code, payment_date, 
                                                    payment_time, customer_name, customer_ref1, customer_ref2, cheque_bank, cheque_no, transaction_amt, 
                                                    reject_status, imp_date, location_file, ref_docno, interest_payment, principal_payment, coop_id) 
                                                    values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},
                                                    {11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23})";
                                                            object[] argslist_billpayment = new object[] {
                                                    ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                                    ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                                    ls_chqno, ldc_trnsamt, 1, date_now, ls_filename, "", INTEREST_PAYAMT, PRINCIPAL_PAYAMT, coop_id
                                                    };

                                                    sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                    bx.SQL.Add(sql_billpayment);
                                                    bx.Execute();
                                                    bx.SQL.Clear();
                                                }
                                                //**** จบ billpayment ****
                                                chkloan = false;
                                            }
                                            else {
                                                //**** billpayment list **** false
                                                //---------------------------------
                                                //insert billpayment
                                                string sqlf_chkbillpaymentMNM = "select member_no from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                                                sqlf_chkbillpaymentMNM = WebUtil.SQLFormat(sqlf_chkbillpaymentMNM, ls_memno, ls_trnsno, ls_filename);
                                                Sdt dtBPM = WebUtil.QuerySdt(sqlf_chkbillpaymentMNM);

                                                if (dtBPM.Next())
                                                {
                                                    string sql_billpayment = @"update billpayment set file_docno = {0}, transaction_no = {1}, bank_code = {2}, 
                                                    branch_code = {3}, teller_no = {4}, member_no = {5}, account_no = {6}, transaction_type = {7}, 
                                                    transaction_code = {8}, payment_date = {9}, payment_time = {10}, customer_name = {11}, 
                                                    customer_ref1 = {12}, customer_ref2 = {13}, cheque_bank = {14}, cheque_no = {15}, 
                                                    transaction_amt = {16}, reject_status = {17}, imp_date = {18}, location_file = {19}, 
                                                    ref_docno = {20}, interest_payment = {21}, principal_payment = {22}, coop_id = {23} where member_no = {24} and transaction_no = {25} and location_file = {19}";
                                                        object[] argslist_billpayment = new object[] {
                                                    ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                                    ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                                    ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id, ls_memno, ls_trnsno
                                                    };
                                                        sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                        bx.SQL.Add(sql_billpayment);
                                                        bx.Execute();
                                                        bx.SQL.Clear();
                                                }
                                                else
                                                {
                                                    string sql_billpayment = @"insert into billpayment(file_docno, transaction_no, bank_code, 
                                                    branch_code, teller_no, member_no, account_no, transaction_type, transaction_code, payment_date, 
                                                    payment_time, customer_name, customer_ref1, customer_ref2, cheque_bank, cheque_no, transaction_amt, 
                                                    reject_status, imp_date, location_file, ref_docno, interest_payment, principal_payment, coop_id) 
                                                    values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},
                                                    {11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23})";
                                                        object[] argslist_billpayment = new object[] {
                                                    ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                                    ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                                    ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id
                                                    };

                                                    sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                    bx.SQL.Add(sql_billpayment);
                                                    bx.Execute();
                                                    bx.SQL.Clear();
                                                }
                                                //**** จบ billpayment ****
                                            }
                                        }
                                        else
                                        {
                                            if (chkloan)
                                            {
                                                //**** billpayment list **** false
                                                //---------------------------------
                                                //insert billpayment
                                                string sqlf_chkbillpayment = "select member_no from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                                                sqlf_chkbillpayment = WebUtil.SQLFormat(sqlf_chkbillpayment, ls_memno, ls_trnsno, ls_filename);
                                                Sdt dtBPM = WebUtil.QuerySdt(sqlf_chkbillpayment);

                                                if (dtBPM.Next())
                                                {
                                                    string sql_billpayment = @"update billpayment set file_docno = {0}, transaction_no = {1}, bank_code = {2}, 
                                                branch_code = {3}, teller_no = {4}, member_no = {5}, account_no = {6}, transaction_type = {7}, 
                                                transaction_code = {8}, payment_date = {9}, payment_time = {10}, customer_name = {11}, 
                                                customer_ref1 = {12}, customer_ref2 = {13}, cheque_bank = {14}, cheque_no = {15}, 
                                                transaction_amt = {16}, reject_status = {17}, imp_date = {18}, location_file = {19}, 
                                                ref_docno = {20}, interest_payment = {21}, principal_payment = {22}, coop_id = {23} where member_no = {24} and transaction_no = {25} and location_file = {19}";
                                                    object[] argslist_billpayment = new object[] {
                                                ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                                ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                                ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", INTEREST_PAYAMT, PRINCIPAL_PAYAMT, coop_id, ls_memno, ls_trnsno
                                                };
                                                    sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                    bx.SQL.Add(sql_billpayment);
                                                    bx.Execute();
                                                    bx.SQL.Clear();
                                                }
                                                else
                                                {
                                                    string sql_billpayment = @"insert into billpayment(file_docno, transaction_no, bank_code, 
                                                branch_code, teller_no, member_no, account_no, transaction_type, transaction_code, payment_date, 
                                                payment_time, customer_name, customer_ref1, customer_ref2, cheque_bank, cheque_no, transaction_amt, 
                                                reject_status, imp_date, location_file, ref_docno, interest_payment, principal_payment, coop_id) 
                                                values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},
                                                {11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23})";
                                                    object[] argslist_billpayment = new object[] {
                                                ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                                ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                                ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", INTEREST_PAYAMT, PRINCIPAL_PAYAMT, coop_id
                                                };

                                                    sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                                    bx.SQL.Add(sql_billpayment);
                                                    bx.Execute();
                                                    bx.SQL.Clear();
                                                }
                                                //**** จบ billpayment ****
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //**** billpayment list **** false
                                    //---------------------------------
                                    //insert billpayment
                                    string sqlf_chkbillpayment00100 = "select * from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                                    sqlf_chkbillpayment00100 = WebUtil.SQLFormat(sqlf_chkbillpayment00100, ls_memno, ls_trnsno, ls_filename);
                                    Sdt dtBPM00100 = WebUtil.QuerySdt(sqlf_chkbillpayment00100);

                                    if (dtBPM00100.Next())
                                    {
                                        string sql_billpayment = @"update billpayment set file_docno = {0}, transaction_no = {1}, bank_code = {2}, 
                                    branch_code = {3}, teller_no = {4}, member_no = {5}, account_no = {6}, transaction_type = {7}, 
                                    transaction_code = {8}, payment_date = {9}, payment_time = {10}, customer_name = {11}, 
                                    customer_ref1 = {12}, customer_ref2 = {13}, cheque_bank = {14}, cheque_no = {15}, 
                                    transaction_amt = {16}, reject_status = {17}, imp_date = {18}, location_file = {19}, 
                                    ref_docno = {20}, interest_payment = {21}, principal_payment = {22}, coop_id = {23} where member_no = {24} and transaction_no = {25} and location_file = {19}";
                                        object[] argslist_billpayment = new object[] {
                                    ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                    ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                    ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id, ls_memno, ls_trnsno
                                    };

                                        sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                        bx.SQL.Add(sql_billpayment);
                                        bx.Execute();
                                        bx.SQL.Clear();
                                    }
                                    else
                                    {
                                        string sql_billpayment = @"insert into billpayment(file_docno, transaction_no, bank_code, 
                                    branch_code, teller_no, member_no, account_no, transaction_type, transaction_code, payment_date, 
                                    payment_time, customer_name, customer_ref1, customer_ref2, cheque_bank, cheque_no, transaction_amt, 
                                    reject_status, imp_date, location_file, ref_docno, interest_payment, principal_payment, coop_id) 
                                    values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},
                                    {11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23})";
                                        object[] argslist_billpayment = new object[] {
                                    ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                    ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                    ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id
                                    };

                                        sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                        bx.SQL.Add(sql_billpayment);
                                        bx.Execute();
                                        bx.SQL.Clear();
                                    }
                                    //**** จบ billpayment ****
                                }
                            }
                            catch (Exception XX)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage(XX);

                                //**** billpayment list **** false
                                //---------------------------------
                                //insert billpayment
                                string sqlf_chkbillpayment001 = "select * from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                                sqlf_chkbillpayment001 = WebUtil.SQLFormat(sqlf_chkbillpayment001, ls_memno, ls_trnsno, ls_filename);
                                Sdt dtBPM001 = WebUtil.QuerySdt(sqlf_chkbillpayment001);

                                if (dtBPM001.Next())
                                {
                                    string sql_billpayment = @"update billpayment set file_docno = {0}, transaction_no = {1}, bank_code = {2}, 
                                    branch_code = {3}, teller_no = {4}, member_no = {5}, account_no = {6}, transaction_type = {7}, 
                                    transaction_code = {8}, payment_date = {9}, payment_time = {10}, customer_name = {11}, 
                                    customer_ref1 = {12}, customer_ref2 = {13}, cheque_bank = {14}, cheque_no = {15}, 
                                    transaction_amt = {16}, reject_status = {17}, imp_date = {18}, location_file = {19}, 
                                    ref_docno = {20}, interest_payment = {21}, principal_payment = {22}, coop_id = {23} where member_no = {24} and transaction_no = {25} and location_file = {19}";
                                    object[] argslist_billpayment = new object[] {
                                    ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                    ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                    ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id, ls_memno, ls_trnsno
                                    };

                                    sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                    bx.SQL.Add(sql_billpayment);
                                    bx.Execute();
                                    bx.SQL.Clear();
                                }
                                else
                                {
                                    string sql_billpayment = @"insert into billpayment(file_docno, transaction_no, bank_code, 
                                    branch_code, teller_no, member_no, account_no, transaction_type, transaction_code, payment_date, 
                                    payment_time, customer_name, customer_ref1, customer_ref2, cheque_bank, cheque_no, transaction_amt, 
                                    reject_status, imp_date, location_file, ref_docno, interest_payment, principal_payment, coop_id) 
                                    values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},
                                    {11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23})";
                                    object[] argslist_billpayment = new object[] {
                                    ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                    ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                    ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id
                                    };

                                    sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                    bx.SQL.Add(sql_billpayment);
                                    bx.Execute();
                                    bx.SQL.Clear();
                                }
                                //**** จบ billpayment ****
                                chk = false;
                            }

                        }
                        catch (Exception eX)
                        {
                            //เอาไว้ดู err เมื่อเกิดข้อผิดพลาดในส่วนของ 004 ชำระหนี้ฉุกเฉิน, 005 ชำระหนี้สามัญ, 006 ชำระหนี้พิเศษ
                            LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                        }
                        //***** EndProcess loan *****
                    }
                    #endregion
                    #region post2etc
                    else if (ls_ref2.Substring(0, 3) == "007")
                    {
                        //ยังไม่มีการทำรายการประเภทนี้
                    }
                    else
                    {
                        try
                        {
                            string ls_cusref2 = dsDetail.DATA[ii].ls_ref2;
                            DateTime ldtm_payment = DateTime.ParseExact(dsDetail.DATA[ii].ls_paydate, "dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
                            string ls_accid = ls_ref2.Substring(3);
                            string ls_cusname = dsDetail.DATA[ii].ls_cusname;
                            string ls_trnscode = dsDetail.DATA[ii].ls_trnscode;
                            string ls_ref1 = dsDetail.DATA[ii].ls_ref1;
                            string ls_chqbank = dsDetail.DATA[ii].ls_chqbank;
                            string ls_chqno = dsDetail.DATA[ii].ls_chqno;
                            string ls_refdocno = dsDetail.DATA[ii].ls_filedocno;
                            string ls_trnsno = dsDetail.DATA[ii].ls_trnsno;
                            string ls_bankcode = dsDetail.DATA[ii].ls_bankcode;
                            string ls_branchcode = dsDetail.DATA[ii].ls_branchcode;
                            string ls_tellerno = dsDetail.DATA[ii].ls_tellerno;
                            string ls_trnstype = dsDetail.DATA[ii].ls_trnstype;
                            string ls_paytime = dsDetail.DATA[ii].ls_paytime;
                            string ls_accountno = dsDetail.DATA[ii].ls_accountno;

                            //**** billpayment list **** flase
                            //---------------------------------
                            //insert billpayment
                            string sqlf_chkbillpaymentERR = "select member_no from billpayment where member_no = {0} and transaction_no = {1} and location_file = {2}";
                            sqlf_chkbillpaymentERR = WebUtil.SQLFormat(sqlf_chkbillpaymentERR, ls_memno, ls_trnsno, ls_filename);
                            Sdt dtBPM = WebUtil.QuerySdt(sqlf_chkbillpaymentERR);

                            if (dtBPM.Next())
                            {
                                string sql_billpayment = @"update billpayment set file_docno = {0}, transaction_no = {1}, bank_code = {2}, 
                                branch_code = {3}, teller_no = {4}, member_no = {5}, account_no = {6}, transaction_type = {7}, 
                                transaction_code = {8}, payment_date = {9}, payment_time = {10}, customer_name = {11}, 
                                customer_ref1 = {12}, customer_ref2 = {13}, cheque_bank = {14}, cheque_no = {15}, 
                                transaction_amt = {16}, reject_status = {17}, imp_date = {18}, location_file = {19}, 
                                ref_docno = {20}, interest_payment = {21}, principal_payment = {22}, coop_id = {23} where member_no = {24} and transaction_no = {25} and location_file = {19}";
                                object[] argslist_billpayment = new object[] {
                                ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id, ls_memno, ls_trnsno
                                };

                                sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                bx.SQL.Add(sql_billpayment);
                                bx.Execute();
                                bx.SQL.Clear();
                            }
                            else
                            {
                                string sql_billpayment = @"insert into billpayment(file_docno, transaction_no, bank_code, 
                                branch_code, teller_no, member_no, account_no, transaction_type, transaction_code, payment_date, 
                                payment_time, customer_name, customer_ref1, customer_ref2, cheque_bank, cheque_no, transaction_amt, 
                                reject_status, imp_date, location_file, ref_docno, interest_payment, principal_payment, coop_id) 
                                values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},
                                {11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23})";
                                object[] argslist_billpayment = new object[] {
                                ls_refdocno, ls_trnsno, ls_bankcode, ls_branchcode, ls_tellerno, ls_memno, ls_accountno, ls_trnstype,
                                ls_trnscode, ldtm_payment, ls_paytime, ls_cusname, ls_ref1, ls_ref2, ls_chqbank,
                                ls_chqno, ldc_trnsamt, 8, date_now, ls_filename, "", 0, 0, coop_id
                                };

                                sql_billpayment = WebUtil.SQLFormat(sql_billpayment, argslist_billpayment);
                                bx.SQL.Add(sql_billpayment);
                                bx.Execute();
                                bx.SQL.Clear();
                            }
                            //**** จบ billpayment ****
                        }
                        catch (Exception eX)
                        {
                            //เอาไว้ดู err 
                            LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                        }
                    }
                    #endregion
                }
            }

            //เช็คว่าตรวจสอบผ่านทุก Statement รึป่าว
            if (chk)
            {
                try
                {
                    ex.Execute();
                    string Err_Finish = @"
                    <script type=""text/javascript"">   
                        $(function() {   
                            disableBT(true);
                        });
                    </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Err_Finish", Err_Finish);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ทำรายการเรียบร้อย");
                }
                catch (Exception eX)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                }
            }
            else
            {
                try
                {
                    ex.Execute();
                    string Err_Finish = @"
                    <script type=""text/javascript"">   
                        $(function() {   
                            disableBT(true);
                        });
                    </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Err_Finish", Err_Finish);
                }
                catch (Exception eX)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                }
            }
        }

        public void JsOnclickbnotpost()
        {
            dsTailer.ResetRow();
            dsDetail.ResetRow();

            string Err_Not_Finish = @"
                <script type=""text/javascript"">   
                    $(function(){
                        $('.Detail_H , .b_post_H').hide()
                        $('.DetailFail_H, .b_postagin').show()
                        disableBT(true);
                    });
                </script>";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Err_Not_Finish", Err_Not_Finish);

            decimal sum_temp = 0, sum_amt = 0;

            for (int ii = 0; ii < dsDetailFail.RowCount; ii++)
            {
                sum_temp = dsDetailFail.DATA[ii].ldc_trnsamtFail;
                sum_amt += sum_temp;
                dsTailer.DATA[0].sum_amt = sum_amt;
                dsTailer.DATA[0].sum_row = (ii + 1);
            }
        }

        public void JsOnclickpostagain()
        {
            int rowDT;
            for (int ii = 0; ii < dsDetailFail.RowCount; ii++)
            {
                dsDetail.InsertLastRow();
                rowDT = dsDetail.RowCount - 1;
                dsDetail.DATA[rowDT].ls_memno = dsDetailFail.DATA[ii].ls_memnoFail;
                dsDetail.DATA[rowDT].ls_cusname = dsDetailFail.DATA[ii].ls_cusnameFail;
                dsDetail.DATA[rowDT].ls_trnscode = dsDetailFail.DATA[ii].ls_trnscodeFail;
                dsDetail.DATA[rowDT].ls_paydate = dsDetailFail.DATA[ii].ls_paydateFail;
                dsDetail.DATA[rowDT].ls_ref1 = dsDetailFail.DATA[ii].ls_ref1Fail;
                dsDetail.DATA[rowDT].ls_ref2 = dsDetailFail.DATA[ii].ls_ref2Fail;
                dsDetail.DATA[rowDT].ldc_trnsamt = dsDetailFail.DATA[ii].ldc_trnsamtFail;
                dsDetail.DATA[rowDT].ls_chqbank = dsDetailFail.DATA[ii].ls_chqbankFail;
                dsDetail.DATA[rowDT].ls_chqno = dsDetailFail.DATA[ii].ls_chqnoFail;
                dsDetail.DATA[rowDT].ls_filedocno = dsDetailFail.DATA[ii].ls_filedocnoFail;
                dsDetail.DATA[rowDT].ls_trnsno = dsDetailFail.DATA[ii].ls_trnsnoFail;
                dsDetail.DATA[rowDT].ls_bankcode = dsDetailFail.DATA[ii].ls_bankcodeFail;
                dsDetail.DATA[rowDT].ls_branchcode = dsDetailFail.DATA[ii].ls_branchcodeFail;
                dsDetail.DATA[rowDT].ls_tellerno = dsDetailFail.DATA[ii].ls_tellernoFail;
                dsDetail.DATA[rowDT].ls_trnstype = dsDetailFail.DATA[ii].ls_trnstypeFail;
                dsDetail.DATA[rowDT].ls_paytime = dsDetailFail.DATA[ii].ls_paytimeFail;
                dsDetail.DATA[rowDT].ls_accountno = dsDetailFail.DATA[ii].ls_accountnoFail;
                dsDetail.DATA[rowDT].ldc_intallacc = dsDetailFail.DATA[ii].ldc_intallaccFail;
                dsDetail.DATA[rowDT].membgroup_code = dsDetailFail.DATA[ii].membgroup_codeFail;
                dsDetail.DATA[rowDT].chk_member = "T";
            }
            JsOnclickbpost();
        }

        //Funtion นี้เอาไว้ get เลขเอกสารต่างๆ รายละเอียดดูใน table cmdocumentcontrol
        public string Get_NumberDOC(string typecode)
        {
            string coop_id = state.SsCoopControl;
            Sta ta = new Sta(state.SsConnectionString);
            string postNumber = "";
            try
            {
                ta.AddInParameter("AVC_COOPID", coop_id, System.Data.OracleClient.OracleType.VarChar);
                ta.AddInParameter("AVC_DOCCODE", typecode, System.Data.OracleClient.OracleType.VarChar);
                ta.AddReturnParameter("return_value", System.Data.OracleClient.OracleType.VarChar);
                ta.ExePlSql("N_PK_DOCCONTROL.OF_GETNEWDOCNO");
                postNumber = ta.OutParameter("return_value").ToString();
                ta.Close();
            }
            catch
            {
                ta.Close();
            }
            return postNumber.ToString();
        }

        // decimal max_seq_no = Get_NumberDOC(select max(seq_no) as max_value from xx where xx = " + xx + ")
        public decimal GetMax_Number(string Select_Condition)
        {
            decimal max_value = 0;
            string sqlf = Select_Condition;
            Sdt dt = WebUtil.QuerySdt(sqlf);

            if (dt.Next())
            {
                max_value = dt.GetDecimal("max_value");
            }
            return max_value += 1;
        }

        private void InitLnRcv(string member_no, string ls_accid, decimal ldc_trnsamt, DateTime ldtm_payment)
        {
            try
            {
                string mem_no = WebUtil.MemberNoFormat(member_no);

                str_slippayin slip = new str_slippayin();
                slip.member_no = mem_no;
                slip.slip_date = dsMain.DATA[0].SLIP_DATE;
                slip.operate_date = dsMain.DATA[0].OPERATE_DATE;
                slip.sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
                slip.memcoop_id = state.SsCoopControl;

                wcf.NShrlon.of_initslippayin(state.SsWsPass, ref slip);
                dsMain.ResetRow();
                dsMain.ImportData(slip.xml_sliphead);

                dsMain.DATA[0].ENTRY_ID = state.SsUsername;

                String mType = dsMain.DATA[0].MONEYTYPE_CODE;
                if (mType == "")
                {
                    dsMain.DATA[0].MONEYTYPE_CODE = "CBT";
                }
                dsMain.DATA[0].SLIPTYPE_CODE = dsMain.DATA[0].SLIPTYPE_CODE.Trim();
                string sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;



                dsMain.DdSliptype();
                dsMain.DdFromAccId(sliptype_code, moneytype_code);
                dsMain.DdMoneyType();
                dsDetailEtc.DdLoanType();

                try
                {
                    dsDetailLoan.ResetRow();
                    wcf.NShrlon.of_initslippayin(state.SsWsPass, ref slip);
                    dsDetailLoan.ImportData(slip.xml_sliplon);

                    for (int i = 0; i < dsDetailLoan.RowCount; i++)
                    {
                        //if (ls_accid.Trim() == dsDetailLoan.DATA[i].LOANCONTRACT_NO || ls_accid.Trim() == dsDetailLoan.DATA[i].LOANCONTRACT_NO.Substring(2))
                        if (ls_accid.Trim() == dsDetailLoan.DATA[i].LOANCONTRACT_NO.Substring(2))
                        {
                            decimal INTEREST_PAYAMT = dsDetailLoan.DATA[i].INTEREST_PAYAMT;
                            decimal CP_INTERESTPAY = dsDetailLoan.DATA[i].CP_INTERESTPAY;
                            decimal ITEM_PAYAMT = dsDetailLoan.DATA[i].ITEM_PAYAMT;
                            decimal PRINCIPAL_PAYAMT = dsDetailLoan.DATA[i].PRINCIPAL_PAYAMT;

                            dsDetailLoan.DATA[i].INTEREST_PAYAMT = CP_INTERESTPAY;
                            dsDetailLoan.DATA[i].ITEM_PAYAMT = ldc_trnsamt;
                            dsDetailLoan.DATA[i].PRINCIPAL_PAYAMT = (ldc_trnsamt - CP_INTERESTPAY);
                            dsDetailLoan.DATA[i].OPERATE_FLAG = 1;
                            dsDetailLoan.DATA[i].SLIPITEM_DESC = "ชำระหนี้( Bill Payment )";
                            //dsDetailLoan.DATA[i].SLIPITEMTYPE_CODE = "BIL";
                        }
                    }
                }
                catch { dsDetailLoan.ResetRow(); }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

    }
}

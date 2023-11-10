using System;
using CoreSavingLibrary;
using System.Web.Services.Protocols;
//using CoreSavingLibrary.WcfFinance;
using CoreSavingLibrary.WcfNFinance;
using DataLibrary;
using Sybase.DataWindow;
using System.Data;
using System.Threading;


namespace Saving.Applications.app_finance
{
    public partial class w_sheet_finslip_tranfer_bank : PageWebSheet, WebSheet
    {
        private DwThDate tDwMain;
        protected String postInitMember;
        protected String postInitSlip;
        protected String postSlipTranfer;
        protected String postPrint;
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        public String outputProcess = "";
        
        #region WebSheet Members
        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain);
            tDwMain.Add("entry_date", "entry_tdate");
            tDwMain.Add("operate_date", "operate_tdate");

            postInitMember = WebUtil.JsPostBack(this, "postInitMember");
            postInitSlip = WebUtil.JsPostBack(this, "postInitSlip");
            postSlipTranfer = WebUtil.JsPostBack(this, "postSlipTranfer");
            postPrint = WebUtil.JsPostBack(this, "postPrint");

        }
        public void WebSheetLoadBegin()
        {
      
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwUtil.RetrieveDDDW(DwMain,"branch_code","finslip_spc.pbl","006");
                DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                DwMain.SetItemDateTime(1, "entry_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
                DwMain.SetItemString(1, "coop_id", state.SsCoopId);
                DwMain.SetItemString(1, "entry_id", state.SsUsername);
                DwMain.SetItemString(1, "machine_id", state.SsClientIp);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }

        }
        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postInitMember":
                    InitMember();
                    break;

                case "postInitSlip":
                    InitSlip();
                    break;

                case "postSlipTranfer":
                    DwUtil.RetrieveDataWindow(DwMain, "finslip_spc.pbl", null, HdSlipTranNo.Value.ToString());
                    tDwMain.Eng2ThaiAllRow();
                    break;

                case "postPrint":
                    PopupReportshr();
                    break;
               
            }
        }
        public void SaveWebSheet()
        {
            bool flag = true;
            String sliptran = DwMain.GetItemString(1, "sliptrandoc_no");
            String cmdocno = "FNRECEIPTBANK";
            //บันทึกรายการใหม่
            if (sliptran == "Auto") // FNRECEIPTBANK
            {
                //หาเลขเอกสาร
                string Docno = "";
                int last_docno = 0;
                string sqlcount = @"SELECT last_documentno FROM cmdocumentcontrol where document_code = '" + cmdocno + "'  and coop_id = '" +
                             state.SsCoopControl + "' ";
                Sdt count = WebUtil.QuerySdt(sqlcount);
                while (count.Next())
                {
                    bool IsEquals = false;
                    last_docno = count.GetInt32("last_documentno");
                    last_docno = last_docno + 1;
                    string entry_tdate = DwMain.GetItemString(1, "entry_tdate");
                    String year = entry_tdate.Substring(6, 2);
                    DateTime entry_date = DwMain.GetItemDateTime(1, "entry_date");
                    if (!IsEquals)
                    {
                        Docno = year + entry_date.Month.ToString("00") + entry_date.Day.ToString("00") + last_docno.ToString("0000");     
                    }
                    else
                    {
                        WebUtil.ErrorMessage("ไม่พบเลขเอกสาร FNRECEIPTBANK ");
                    }
                }

                try
                {
                    String ref_slipno = "", member_no = "", account_no = "", tranfer_desc = "", member_name = "";
                    String account_name = "", machine_id = "", tranfer_type = "", branch_code = "", slip_type = "";
                    Decimal tranfer_amt = 0, tranfre_status = 0;
                    DateTime operate_date = DwMain.GetItemDateTime(1, "operate_date");
                    Decimal ref_slipamt = DwMain.GetItemDecimal(1, "refslip_amt");
                    String tranfer_bank = DwMain.GetItemString(1, "tranferbank_code");
                    String entry_id = DwMain.GetItemString(1, "entry_id");
                    DateTime entry_date = DwMain.GetItemDateTime(1, "entry_date");

                    if (HdTranType.Value.ToString() != "")
                    {
                        tranfer_type = HdTranType.Value.ToString();
                    }
                    else { tranfer_type = DwMain.GetItemString(1, "tranfer_type"); }
                    if (HdBankBranch.Value.ToString() != "")
                    {
                        branch_code = HdBankBranch.Value.ToString();
                    }
                    else { branch_code = DwMain.GetItemString(1, "branch_code"); }
                    if (HdTranferAmt.Value.ToString() != "")
                    {
                        tranfer_amt = decimal.Parse(HdTranferAmt.Value.ToString());
                    }
                    else { tranfer_amt = DwMain.GetItemDecimal(1, "tranfer_amt"); }
                    if (HdStatus.Value.ToString() != "")
                    {
                        tranfre_status = decimal.Parse(HdStatus.Value.ToString());
                    }
                    else { tranfre_status = DwMain.GetItemDecimal(1, "tranfre_status"); }
                    if (HdSlipType.Value.ToString() != "")
                    {
                        slip_type = HdSlipType.Value.ToString();
                    }
                    else { slip_type = DwMain.GetItemString(1, "slip_type"); }

                    try { ref_slipno = DwMain.GetItemString(1, "refslip_no"); }
                    catch { ref_slipno = ""; }
                    try { member_no = DwMain.GetItemString(1, "member_no"); }
                    catch { member_no = ""; }
                    try { account_no = DwMain.GetItemString(1, "account_no"); }
                    catch { account_no = HdAccountNo.Value.ToString(); }
                    try { tranfer_desc = DwMain.GetItemString(1, "tranfer_desc"); }
                    catch { tranfer_desc = HdTranferDesc.Value.ToString(); }
                    try { member_name = DwMain.GetItemString(1, "member_name"); }
                    catch { member_name = ""; }
                    try { account_name = DwMain.GetItemString(1, "account_name"); }
                    catch { account_name = HdAccountName.Value.ToString(); }
                    try { machine_id = DwMain.GetItemString(1, "machine_id"); }
                    catch { machine_id = ""; } //to_date('" + state.SsWorkDate.ToString("MM/dd/yyyy", WebUtil.EN) + "', 'MM/dd/yyyy') ) "

                    string sqlinsert = @"INSERT INTO FINSLIPTRANSFERBANK VALUES('" + state.SsCoopId + "','" + Docno + "',to_date('" + operate_date.ToString("MM/dd/yyyy", WebUtil.EN) + "','MM/dd/yyyy'),'" +
                                    ref_slipno + "','" + ref_slipamt + "','" + tranfer_type + "','" + tranfer_bank + "','" + member_no + "','" +
                                    tranfer_bank + "','" + branch_code + "','" + account_no + "','" + tranfer_amt + "','" + entry_id + "',to_date('" +
                                    entry_date.ToString("MM/dd/yyyy", WebUtil.EN) + "','MM/dd/yyyy'),'" + tranfer_desc + "','" + member_name + "','" + account_name + "','" + tranfre_status + "','" +
                                    machine_id + "','" + slip_type + "')";
                    Sdt insert = WebUtil.QuerySdt(sqlinsert);
                }
                catch
                {
                    flag = false;
                }
                if (flag)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                    DwUtil.RetrieveDataWindow(DwMain, "finslip_spc.pbl", null, Docno);
                    tDwMain.Eng2ThaiAllRow();
                    
                    //DwMain.Reset();
                    //DwMain.InsertRow(0);
                    //DwUtil.RetrieveDDDW(DwMain, "branch_code", "finslip_spc.pbl", "006");
                    //DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                    //DwMain.SetItemDateTime(1, "entry_date", state.SsWorkDate);
                    //tDwMain.Eng2ThaiAllRow();
                    //DwMain.SetItemString(1, "coop_id", state.SsCoopId);
                    //DwMain.SetItemString(1, "entry_id", state.SsUsername);
                    //DwMain.SetItemString(1, "machine_id", state.SsClientIp);

                    try
                    {
                        string sqlupdate = @"UPDATE cmdocumentcontrol SET last_documentno = '" + last_docno + "' WHERE document_code = '" +
                            cmdocno + "'  and coop_id = '" + state.SsCoopControl + "'";
                        Sdt update = WebUtil.QuerySdt(sqlupdate);

                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถอัพเดทเลขเอกสารได้");
                    }
                    //สั่งพิมพ์เลย เมื่อคีย์ครั้งแรก
                    PopupReportshr();
                }
                else
                {
                    //DwMain.Reset();
                    //DwMain.InsertRow(0);
                    //DwUtil.RetrieveDDDW(DwMain, "branch_code", "finslip_spc.pbl", "006");
                    //DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                    //DwMain.SetItemDateTime(1, "entry_date", state.SsWorkDate);
                    //tDwMain.Eng2ThaiAllRow();
                    //DwMain.SetItemString(1, "coop_id", state.SsCoopId);
                    //DwMain.SetItemString(1, "entry_id", state.SsUsername);
                    //DwMain.SetItemString(1, "machine_id", state.SsClientIp);

                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้");
                }
            }
            else //ดึงรายการเดิมมาบันทึกใหม่
            {
                try
                {
                    DwUtil.UpdateDataWindow(DwMain, "finslip_spc.pbl", "FINSLIPTRANSFERBANK");
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้");
                }
            }
        }
        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
           
        }
        #endregion

        private void InitMember()
        {
            DwMain.SetItemString(1, "member_no", HdMemberNo.Value.ToString());
            string sqlcount = @"SELECT mp.prename_desc || mb.memb_name || ' ' || mb.memb_surname as memname FROM mbmembmaster mb, mbucfprename mp where mb.prename_code = mp.prename_code and 
                            mb.coop_id = '" + state.SsCoopControl + "' and mb.member_no = '" + HdMemberNo.Value.ToString() + "' ";
            Sdt count = WebUtil.QuerySdt(sqlcount);
            while (count.Next())
            {
                bool IsEquals = false;
                string memname = count.GetString("memname").Trim();
                DwMain.SetItemString(1, "member_name", memname);
                DwMain.SetItemString(1, "account_name", memname);
                if (!IsEquals)
                {
                    WebUtil.ErrorMessage("ไม่พบทะเบียนสมาชิกเลขที่ " + HdMemberNo.Value.ToString());
                }
            }
        }

        private void InitSlip()
        {
            DwMain.SetItemString(1, "refslip_no", HdSlipNo.Value.ToString());
            string sqlcount = @"SELECT f.item_amtnet as itempay, mb.member_no as member_no , mp.prename_desc || mb.memb_name || ' ' || mb.memb_surname as memname , f.payment_desc as payment_desc
                            FROM mbmembmaster mb, mbucfprename mp, finslip f where mb.prename_code = mp.prename_code and 
                            f.member_no = mb.member_no and f.slip_no = '" + HdSlipNo.Value.ToString() + "' and mb.coop_id = '" +
                             state.SsCoopControl + "' ";
            Sdt count = WebUtil.QuerySdt(sqlcount);
            while (count.Next())
            {
                bool IsEquals = false;
                string memname = count.GetString("memname").Trim();
                string member_no = count.GetString("member_no").Trim();
                string payment_desc = count.GetString("payment_desc").Trim();
                Decimal itempay = count.GetDecimal("itempay");
                DwMain.SetItemString(1, "member_name", memname);
                DwMain.SetItemString(1, "account_name", memname);
                DwMain.SetItemString(1, "tranfer_desc", payment_desc);

                DwMain.SetItemString(1, "member_no", member_no);
                DwMain.SetItemDecimal(1, "refslip_amt", itempay);
                DwMain.SetItemDecimal(1, "tranfer_amt", itempay);
                if (!IsEquals)
                {
                    WebUtil.ErrorMessage("ไม่พบข้อมูลรายการ " + HdSlipNo.Value.ToString());
                }
            }
        }

        //  พิมพ์ใบเสร็จหน้าลงรายวัน
        public void PopupReportshr()
        {
            Print();
            // Thread.Sleep(5000);
            Thread.Sleep(700);

            String pop = "Gcoop.OpenPopup('" + Session["pdf"].ToString() + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);

        }

        //  พิมพ์ใบเสร็จหน้าลงรายวัน
        private void Print()
        {

            app = state.SsApplication;
            try
            {
                gid = "FINANCE_DAILY";
            }
            catch { }
            try
            {
                rid = "FNDAILY006";
            }
            catch { }


            String Doc_no = DwMain.GetItemString(1, "sliptrandoc_no");
            
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.

            ReportHelper lnv_helper = new ReportHelper();

            //lnv_helper.AddArgument(state.SsCoopId, ArgumentType.String);
            lnv_helper.AddArgument(Doc_no, ArgumentType.String);

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();
            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                String criteriaXML = lnv_helper.PopArgumentsXML();
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, "PDF");

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            Session["pdf"] = pdf;

            try
            {
                string sqlupdate = @"UPDATE finsliptransferbank SET tranfre_status = 1 WHERE sliptrandoc_no = '" +
                    Doc_no + "'  and coop_id = '" + state.SsCoopId + "'";
                Sdt update = WebUtil.QuerySdt(sqlupdate);
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถอัพเดทสถานะการพิมพ์ของเอกสารได้");
            }


            DwMain.Reset();
            DwMain.InsertRow(0);
            // PopupReport();
        }

        
    }
}

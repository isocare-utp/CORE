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
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNDeposit;
using System.IO;
using DataLibrary;
using System.Globalization;
using System.Text;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_create_file_tobank : PageWebDialog, WebDialog
    {

        private String Space = "                                                                                                    ";
        private String COOP_TYPE = "007097";
        private String COOP_ID = "007097";
        private DateTime WorkDate = DateTime.Now;
        CultureInfo en = new CultureInfo("en-US");

        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {

            if (!IsPostBack)
            {
                //cash_type = Request["cash_type"];
                //div_year = Request["div_year"];
                //bank_code = Request["bank_code"];
                //from_system = Request["from_system"];
                //directcredit_slipno = Request["directcredit_slipno"];
            }
            else
            {

            }

        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {
            try
            {
                ProcessAccountMapping();
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void ProcessAccountMapping()
        {
            int Reccord = 0;
            Decimal CUST_LIMIT_AMT = 0;
            Decimal OD_PRINCIPAL = 0;
            try
            {
                StringWriter DataStringWriter = new StringWriter();
                String filename = "COOP097.txt";
                GetHeader(ref DataStringWriter);
                GetDetail(ref DataStringWriter, ref Reccord, ref CUST_LIMIT_AMT, ref OD_PRINCIPAL);
                GetTailer(ref DataStringWriter, Reccord, CUST_LIMIT_AMT, OD_PRINCIPAL);
                createDownloadFile(filename, "text/plain", DataStringWriter, Encoding.Default);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void createDownloadFile(string filename, string p, StringWriter oStringWriter, Encoding encoding)
        {
            try
            {
                Response.ContentType = "text/plain";

                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Clear();

                using (StreamWriter writer = new StreamWriter(Response.OutputStream, encoding))
                {
                    writer.Write(oStringWriter.ToString());
                }
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetHeader(ref StringWriter DataStringWriter)
        {
            try
            {
                String Data = String.Empty;
                String REC_TYPE = "0";
                String COOP_ID = COOP_TYPE.Substring(COOP_TYPE.Length - 3, 3);
                String SETTLEMENT_DATE = DateTime.Now.ToString("dd/MM/yyyy", en);
                String FILE_DESC = "COOP";//35 หลัก
                FILE_DESC = (FILE_DESC + Space).Substring(0, 35);
                String RESERVE = (Space + Space + Space).Substring(0, 245);
                Data = REC_TYPE + COOP_ID + SETTLEMENT_DATE + FILE_DESC + COOP_TYPE + RESERVE;
                DataStringWriter.WriteLine(Data);
                if (Data.Length != 300) throw new Exception("ข้อมูล Header ไม่เท่ากับ 300 ตัวอักษร (" + Data.Length + ") กรุณาตรวจสอบ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetDetail(ref StringWriter DataStringWriter, ref int Reccord, ref Decimal CUST_LIMIT_AMT, ref Decimal OD_PRINCIPAL)
        {
            try
            {
                //จะทำการ Process ตามลำดับคือ 1, 2, 3, 4, 5 และ 0
                String SqlProcess = String.Empty;
                Sdt dtProcess;
                String MEMBER_NO = String.Empty;
                //Process [1]Information
                SqlProcess = "SELECT MEMBER_NO, INFO_FLAG FROM ATMBANKPROCESS WHERE INFO_FLAG in (1,-9) AND COOP_ID = '" + COOP_ID + "'";
                dtProcess = WebUtil.QuerySdt(SqlProcess);
                while (dtProcess.Next())
                {
                    MEMBER_NO = dtProcess.GetString("MEMBER_NO");
                    Decimal INFO_FLAG = dtProcess.GetDecimal("INFO_FLAG");
                    ProcessINFO(ref DataStringWriter, MEMBER_NO, INFO_FLAG, ref Reccord);
                }
                //############################################################################################
                //Process [2]Loan Receive
                SqlProcess = "SELECT MEMBER_NO, LNRECV_FLAG FROM ATMBANKPROCESS WHERE LNRECV_FLAG in (1,-9) AND COOP_ID = '" + COOP_ID + "'";
                dtProcess = WebUtil.QuerySdt(SqlProcess);
                while (dtProcess.Next())
                {
                    MEMBER_NO = dtProcess.GetString("MEMBER_NO");
                    Decimal LNRECV_FLAG = dtProcess.GetDecimal("LNRECV_FLAG");
                    ProcessLoanRecv(ref DataStringWriter, MEMBER_NO, LNRECV_FLAG, ref Reccord, ref CUST_LIMIT_AMT, ref OD_PRINCIPAL);
                }
                //############################################################################################
                //Process [3]Deposit Withdraw
                SqlProcess = "SELECT MEMBER_NO, DPWITH_FLAG FROM ATMBANKPROCESS WHERE DPWITH_FLAG in (1,-9) AND COOP_ID = '" + COOP_ID + "'";
                dtProcess = WebUtil.QuerySdt(SqlProcess);
                while (dtProcess.Next())
                {
                    MEMBER_NO = dtProcess.GetString("MEMBER_NO");
                    Decimal DPWITH_FLAG = dtProcess.GetDecimal("DPWITH_FLAG");
                    ProcessDeptWith(ref DataStringWriter, MEMBER_NO, DPWITH_FLAG, ref Reccord, ref CUST_LIMIT_AMT, ref OD_PRINCIPAL);
                }
                //############################################################################################
                //Process [4]Loan Payment
                SqlProcess = "SELECT MEMBER_NO, LNPAY_FLAG FROM ATMBANKPROCESS WHERE LNPAY_FLAG in (1,-9) AND COOP_ID = '" + COOP_ID + "'";
                dtProcess = WebUtil.QuerySdt(SqlProcess);
                while (dtProcess.Next())
                {
                    MEMBER_NO = dtProcess.GetString("MEMBER_NO");
                    Decimal LNPAY_FLAG = dtProcess.GetDecimal("LNPAY_FLAG");
                    ProcessLoanPay(ref DataStringWriter, MEMBER_NO, LNPAY_FLAG, ref Reccord);
                }
                //############################################################################################
                //Process [4]Deposit
                SqlProcess = "SELECT MEMBER_NO, DPDEPT_FLAG FROM ATMBANKPROCESS WHERE DPDEPT_FLAG in (1,-9) AND COOP_ID = '" + COOP_ID + "'";
                dtProcess = WebUtil.QuerySdt(SqlProcess);
                while (dtProcess.Next())
                {
                    MEMBER_NO = dtProcess.GetString("MEMBER_NO");
                    Decimal DPDEPT_FLAG = dtProcess.GetDecimal("DPDEPT_FLAG");
                    ProcessDeposit(ref DataStringWriter, MEMBER_NO, DPDEPT_FLAG, ref Reccord);
                }
            }
            catch
            {

            }
        }
        private void GetTailer(ref StringWriter DataStringWriter, int Reccord, Decimal CUST_LIMIT_AMT, Decimal OD_PRINCIPAL)
        {
            try
            {
                String Space = "                                                                                                    ";
                String REC_TYPE = "9";
                String TOTAL_END = "END";
                String TOTAL_RECORDS = Reccord.ToString("000000000");
                String S_CUST_LIMIT_AMT = CUST_LIMIT_AMT.ToString("00000000000.00").Replace(".", "");
                String S_OD_PRINCIPAL = OD_PRINCIPAL.ToString("00000000000.00").Replace(".", "");
                String RESERVE = (Space + Space + Space).Substring(0, 261);
                DataStringWriter.WriteLine(REC_TYPE + TOTAL_END + TOTAL_RECORDS + S_CUST_LIMIT_AMT + S_OD_PRINCIPAL + RESERVE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessINFO(ref StringWriter DataStringWriter, String Member_No, Decimal INFO_FLAG, ref int Reccord)
        {
            try
            {
                Decimal UPDATE_FLAG = 0;
                String REC_TYPE = "1";
                String COOP_CUST = "0000000000" + Member_No.Trim();
                COOP_CUST = COOP_CUST.Substring(COOP_CUST.Length - 10, 10);
                String RESERVE = "   ";
                String DATA_TYPE = "1";
                String FUNCTION = String.Empty;
                String ACCOUNT_NO = String.Empty;
                if (INFO_FLAG == 1)
                {
                    String SqlCheck = "SELECT INFO_FLAG, SAVING_ACC FROM ATMMEMBER WHERE SAVING_ACC IS NOT NULL AND MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                    Sdt dt = WebUtil.QuerySdt(SqlCheck);
                    if (dt.Next())
                    {
                        Decimal MAININFO_FLAG = dt.GetDecimal("INFO_FLAG");
                        if (MAININFO_FLAG == 0) FUNCTION = "A";
                        else FUNCTION = "C";
                        UPDATE_FLAG = 1;
                        ACCOUNT_NO = dt.GetString("SAVING_ACC");
                    }
                    else return; //ไม่ทำเพราะหาเลข Bank ไม่ได้
                }
                else if (INFO_FLAG == -9)
                {
                    FUNCTION = "D";
                    UPDATE_FLAG = 0;
                }
                else
                {
                    return; //ข้ามคนนั้นไปเลย
                }
                String THAI_NAME = String.Empty;
                String ENGS_NAME = String.Empty;
                String SEX = String.Empty;
                String BIRTH_DATE = String.Empty;
                String CARD_NUM = String.Empty;
                String SqlMember = "SELECT MEMB_NAME, MEMB_SURNAME, NVL(MEMB_ENAME, 'UNKNOW') as MEMB_ENAME, NVL(MEMB_ESURNAME,'UNKNOW') as MEMB_ESURNAME, SEX, BIRTH_DATE, CARD_PERSON FROM MBMEMBMASTER WHERE MEMBER_NO = '" + Member_No + "'";
                    //"SELECT MEMB_NAME, MEMB_SURNAME, MEMB_ENAME, MEMB_ESURNAME, SEX, BIRTH_DATE, CARD_PERSON FROM MBMEMBMASTER WHERE MEMB_ENAME IS NOT NULL AND MEMB_ESURNAME IS NOT NULL AND MEMBER_NO = '" + Member_No + "'";
                Sdt dtMember = WebUtil.QuerySdt(SqlMember);
                if (dtMember.Next())
                {
                    String MEMB_NAME = dtMember.GetString("MEMB_NAME").Trim();
                    String MEMB_SURNAME = dtMember.GetString("MEMB_SURNAME").Trim();
                    String MEMB_ENAME = dtMember.GetString("MEMB_ENAME").Trim().ToUpper();
                    String MEMB_ESURNAME = dtMember.GetString("MEMB_ESURNAME").Trim().ToUpper();

                    THAI_NAME = (MEMB_NAME + " " + MEMB_SURNAME + Space).Substring(0, 50);
                    ENGS_NAME = (MEMB_ENAME + " " + MEMB_ESURNAME.Substring(0, 1) + "." + Space).Substring(0, 50);

                    SEX = dtMember.GetString("SEX").Trim();
                    if (SEX == "F") SEX = "2";
                    else SEX = "1";

                    DateTime MAINBIRTH_DATE = dtMember.GetDate("BIRTH_DATE");
                    BIRTH_DATE = MAINBIRTH_DATE.ToString("yyyyMMdd");

                    CARD_NUM = dtMember.GetString("CARD_PERSON").Trim();
                    CARD_NUM = (CARD_NUM + Space).Substring(0, 13);
                }
                else return;

                String CARD_TYPE = "01";
                String RESERVE2 = Space.Substring(0, 26);
                String CONTRACT_ADDR = Space.Substring(0, 80);
                ACCOUNT_NO = (ACCOUNT_NO + Space).Substring(0, 10);
                String RESERVE3 = Space.Substring(0, 38);
                String SqlUpdate = "UPDATE ATMMEMBER SET INFO_FLAG = " + UPDATE_FLAG + " WHERE MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                WebUtil.Query(SqlUpdate);
                SqlUpdate = "UPDATE ATMBANKPROCESS SET INFO_FLAG = 0 WHERE MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                WebUtil.Query(SqlUpdate);
                Reccord++;
                DataStringWriter.WriteLine(REC_TYPE + COOP_TYPE + COOP_CUST + RESERVE + DATA_TYPE + FUNCTION + THAI_NAME + ENGS_NAME + SEX + BIRTH_DATE + CARD_TYPE + CARD_NUM + RESERVE2 + CONTRACT_ADDR + ACCOUNT_NO + RESERVE3);
                LogProcess(COOP_ID, Member_No, 1, FUNCTION, WorkDate);
            }
            catch
            {
                return;
            }
        }
        private void ProcessLoanRecv(ref StringWriter DataStringWriter, String Member_No, Decimal LNRECV_FLAG, ref int Reccord, ref Decimal CUST_LIMIT_AMT, ref Decimal OD_PRINCIPAL)
        {
            try
            {
                Decimal UPDATE_FLAG = 0;
                String REC_TYPE = "1";
                String COOP_CUST = "0000000000" + Member_No.Trim();
                COOP_CUST = COOP_CUST.Substring(COOP_CUST.Length - 10, 10);
                String RESERVE = "   ";
                String DATA_TYPE = "2";
                String FUNCTION = String.Empty;
                String CUST_STATUS = "1";
                String CUST_EXPDATE = "20991231";
                String AUTH_INFORMATION = "00000000000" + "99999999999" + "99999" + "99999999999" + "99999" + "99999999999" + "99999" + "99999999999" + "99999";
                Decimal CUSTLIMIT_AMT = 10000000.00m;
                AUTH_INFORMATION += CUSTLIMIT_AMT.ToString("000000000.00").Replace(".", "");
                Decimal OD_PRINCIPAL_AMT = 10000000.00m;
                String DEBT_INFORMATION = OD_PRINCIPAL_AMT.ToString("000000000.00").Replace(".", "");
                String RESERVE2 = Space.Substring(0, 67);
                String BANK_ACCOUNT = String.Empty;
                String RESERVE3 = Space.Substring(0, 95);
                if (LNRECV_FLAG == 1)
                {
                    String SqlCheck = "SELECT INFO_FLAG, LNRECV_FLAG, SAVING_ACC FROM ATMMEMBER WHERE SAVING_ACC IS NOT NULL AND MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                    Sdt dt = WebUtil.QuerySdt(SqlCheck);
                    if (dt.Next())
                    {
                        Decimal MAININFO_FLAG = dt.GetDecimal("INFO_FLAG");
                        if (MAININFO_FLAG != 1) return; //ต้องมี Info ก่อนถึงจะทำรายการนี้ได้
                        Decimal MAINLNRECV_FLAG = dt.GetDecimal("LNRECV_FLAG");
                        if (MAINLNRECV_FLAG == 0) FUNCTION = "A";
                        else FUNCTION = "C";
                        UPDATE_FLAG = 1;
                        BANK_ACCOUNT = dt.GetString("SAVING_ACC");
                    }
                    else return; //ไม่ทำเพราะหาเลข Bank ไม่ได้
                }
                else if (LNRECV_FLAG == -9)
                {
                    FUNCTION = "D";
                    UPDATE_FLAG = 0;
                }
                else
                {
                    return; //ข้ามคนนั้นไปเลย
                }

                BANK_ACCOUNT = (BANK_ACCOUNT + Space).Substring(0, 10);
                String SqlUpdate = "UPDATE ATMMEMBER SET LNRECV_FLAG = " + UPDATE_FLAG + " WHERE MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                WebUtil.Query(SqlUpdate);
                SqlUpdate = "UPDATE ATMBANKPROCESS SET LNRECV_FLAG = 0 WHERE MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                WebUtil.Query(SqlUpdate);
                Reccord++;
                DataStringWriter.WriteLine(REC_TYPE + COOP_TYPE + COOP_CUST + RESERVE + DATA_TYPE + FUNCTION + CUST_STATUS + CUST_EXPDATE + AUTH_INFORMATION + DEBT_INFORMATION + RESERVE2 + BANK_ACCOUNT + RESERVE3);
                CUST_LIMIT_AMT += CUSTLIMIT_AMT;
                OD_PRINCIPAL += OD_PRINCIPAL_AMT;
                LogProcess(COOP_ID, Member_No, 2, FUNCTION, WorkDate);
            }
            catch
            {
                return;
            }
        }
        private void ProcessDeptWith(ref StringWriter DataStringWriter, String Member_No, Decimal DPWITH_FLAG, ref int Reccord, ref Decimal CUST_LIMIT_AMT, ref Decimal OD_PRINCIPAL)
        {
            try
            {
                Decimal UPDATE_FLAG = 0;
                String REC_TYPE = "1";
                String COOP_CUST = "0000000000" + Member_No.Trim();
                COOP_CUST = COOP_CUST.Substring(COOP_CUST.Length - 10, 10);
                String RESERVE = "   ";
                String DATA_TYPE = "3";
                String FUNCTION = String.Empty;
                String CUST_STATUS = "1";
                String CUST_EXPDATE = "20991231";
                String AUTH_INFORMATION = "00000000000" + "99999999999" + "99999" + "99999999999" + "99999" + "99999999999" + "99999" + "99999999999" + "99999";
                Decimal CUSTLIMIT_AMT = 10000000.00m;
                AUTH_INFORMATION += CUSTLIMIT_AMT.ToString("000000000.00").Replace(".", "");
                Decimal OD_PRINCIPAL_AMT = 10000000.00m;
                String DEBT_INFORMATION = OD_PRINCIPAL_AMT.ToString("000000000.00").Replace(".", "");
                String RESERVE2 = Space.Substring(0, 67);
                String BANK_ACCOUNT = String.Empty;
                String RESERVE3 = Space.Substring(0, 95);
                if (DPWITH_FLAG == 1)
                {
                    String SqlCheck = "SELECT INFO_FLAG, DPWITH_FLAG, SAVING_ACC FROM ATMMEMBER WHERE SAVING_ACC IS NOT NULL AND MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                    Sdt dt = WebUtil.QuerySdt(SqlCheck);
                    if (dt.Next())
                    {
                        Decimal MAININFO_FLAG = dt.GetDecimal("INFO_FLAG");
                        if (MAININFO_FLAG != 1) return; //ต้องมี Info ก่อนถึงจะทำรายการนี้ได้
                        Decimal MAINDPWITH_FLAG = dt.GetDecimal("DPWITH_FLAG");
                        if (MAINDPWITH_FLAG == 0) FUNCTION = "A";
                        else FUNCTION = "C";
                        UPDATE_FLAG = 1;
                        BANK_ACCOUNT = dt.GetString("SAVING_ACC");
                    }
                    else return; //ไม่ทำเพราะหาเลข Bank ไม่ได้
                }
                else if (DPWITH_FLAG == -9)
                {
                    FUNCTION = "D";
                    UPDATE_FLAG = 0;
                }
                else
                {
                    return; //ข้ามคนนั้นไปเลย
                }

                BANK_ACCOUNT = (BANK_ACCOUNT + Space).Substring(0, 10);
                String SqlUpdate = "UPDATE ATMMEMBER SET DPWITH_FLAG = " + UPDATE_FLAG + " WHERE MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                WebUtil.Query(SqlUpdate);
                SqlUpdate = "UPDATE ATMBANKPROCESS SET DPWITH_FLAG = 0 WHERE MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                WebUtil.Query(SqlUpdate);
                Reccord++;
                DataStringWriter.WriteLine(REC_TYPE + COOP_TYPE + COOP_CUST + RESERVE + DATA_TYPE + FUNCTION + CUST_STATUS + CUST_EXPDATE + AUTH_INFORMATION + DEBT_INFORMATION + RESERVE2 + BANK_ACCOUNT + RESERVE3);
                CUST_LIMIT_AMT += CUSTLIMIT_AMT;
                OD_PRINCIPAL += OD_PRINCIPAL_AMT;
                LogProcess(COOP_ID, Member_No, 3, FUNCTION, WorkDate);
            }
            catch
            {
                return;
            }
        }
        private void ProcessLoanPay(ref StringWriter DataStringWriter, String Member_No, Decimal LNPAY_FLAG, ref int Reccord)
        {
            try
            {
                Decimal UPDATE_FLAG = 0;
                String REC_TYPE = "1";
                String COOP_CUST = "0000000000" + Member_No.Trim();
                COOP_CUST = COOP_CUST.Substring(COOP_CUST.Length - 10, 10);
                String RESERVE = "   ";
                String DATA_TYPE = "4";
                String FUNCTION = String.Empty;
                String THAI_NAME = String.Empty;
                String ENGS_NAME = String.Empty;
                String RESERVE2 = (Space + Space).Substring(0, 130);
                String LOANCONTRACT_NO = String.Empty;
                String RESERVE3 = Space.Substring(0, 38);

                String SqlMember = "SELECT MEMB_NAME, MEMB_SURNAME, MEMB_ENAME, MEMB_ESURNAME FROM MBMEMBMASTER WHERE MEMB_ENAME IS NOT NULL AND MEMB_ESURNAME IS NOT NULL AND MEMBER_NO = '" + Member_No + "'";
                Sdt dtMember = WebUtil.QuerySdt(SqlMember);
                if (dtMember.Next())
                {
                    String MEMB_NAME = dtMember.GetString("MEMB_NAME").Trim();
                    String MEMB_SURNAME = dtMember.GetString("MEMB_SURNAME").Trim();
                    String MEMB_ENAME = dtMember.GetString("MEMB_ENAME").Trim();
                    String MEMB_ESURNAME = dtMember.GetString("MEMB_ESURNAME").Trim();

                    THAI_NAME = (MEMB_NAME + " " + MEMB_SURNAME + Space).Substring(0, 50);
                    ENGS_NAME = (MEMB_ENAME + " " + MEMB_ESURNAME.Substring(0, 1) + "." + Space).Substring(0, 50);
                }
                else return;

                if (LNPAY_FLAG == 1)
                {
                    String SqlCheck = "SELECT INFO_FLAG, LNPAY_FLAG FROM ATMMEMBER WHERE SAVING_ACC IS NOT NULL AND MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                    Sdt dt = WebUtil.QuerySdt(SqlCheck);
                    if (dt.Next())
                    {
                        Decimal MAININFO_FLAG = dt.GetDecimal("INFO_FLAG");
                        if (MAININFO_FLAG != 1) return; //ต้องมี Info ก่อนถึงจะทำรายการนี้ได้
                        Decimal MAINLNPAY_FLAG = dt.GetDecimal("LNPAY_FLAG");
                        if (MAINLNPAY_FLAG == 0) FUNCTION = "A";
                        else FUNCTION = "C";
                        UPDATE_FLAG = 1;
                    }
                    else return; //ไม่ทำเพราะหาเลข Bank ไม่ได้
                }
                else if (LNPAY_FLAG == -9)
                {
                    FUNCTION = "D";
                    UPDATE_FLAG = 0;
                }
                else
                {
                    return; //ข้ามคนนั้นไปเลย
                }

                String SqlLoancontract = "SELECT LOANCONTRACT_NO FROM ATMLOAN WHERE ACCOUNT_STATUS = 1 AND MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                Sdt dtLoanCont = WebUtil.QuerySdt(SqlLoancontract);
                if (dtLoanCont.Next())
                {
                    LOANCONTRACT_NO = dtLoanCont.GetString("LOANCONTRACT_NO").Trim();
                    LOANCONTRACT_NO = "0000000000" + LOANCONTRACT_NO.Substring(2);
                    LOANCONTRACT_NO = LOANCONTRACT_NO.Substring(LOANCONTRACT_NO.Length - 10);
                }
                else return; //ไม่มีสัญญาเงินกู้

                String SqlUpdate = "UPDATE ATMMEMBER SET LNPAY_FLAG = " + UPDATE_FLAG + " WHERE MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                WebUtil.Query(SqlUpdate);
                SqlUpdate = "UPDATE ATMBANKPROCESS SET LNPAY_FLAG = 0 WHERE MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                WebUtil.Query(SqlUpdate);
                Reccord++;
                DataStringWriter.WriteLine(REC_TYPE + COOP_TYPE + COOP_CUST + RESERVE + DATA_TYPE + FUNCTION + THAI_NAME + ENGS_NAME + RESERVE2 + LOANCONTRACT_NO + RESERVE3);
                LogProcess(COOP_ID, Member_No, 4, FUNCTION, WorkDate);
            }
            catch
            {
                return;
            }
        }
        private void ProcessDeposit(ref StringWriter DataStringWriter, String Member_No, Decimal DPDEPT_FLAG, ref int Reccord)
        {
            try
            {
                Decimal UPDATE_FLAG = 0;
                String REC_TYPE = "1";
                String COOP_CUST = "0000000000" + Member_No.Trim();
                COOP_CUST = COOP_CUST.Substring(COOP_CUST.Length - 10, 10);
                String RESERVE = "   ";
                String DATA_TYPE = "5";
                String FUNCTION = String.Empty;
                String THAI_NAME = String.Empty;
                String ENGS_NAME = String.Empty;
                String RESERVE2 = (Space + Space).Substring(0, 130);
                String DEPTACCOUNT_NO = String.Empty;
                String RESERVE3 = Space.Substring(0, 38);

                String SqlMember = "SELECT MEMB_NAME, MEMB_SURNAME, MEMB_ENAME, MEMB_ESURNAME FROM MBMEMBMASTER WHERE MEMB_ENAME IS NOT NULL AND MEMB_ESURNAME IS NOT NULL AND MEMBER_NO = '" + Member_No + "'";
                Sdt dtMember = WebUtil.QuerySdt(SqlMember);
                if (dtMember.Next())
                {
                    String MEMB_NAME = dtMember.GetString("MEMB_NAME").Trim();
                    String MEMB_SURNAME = dtMember.GetString("MEMB_SURNAME").Trim();
                    String MEMB_ENAME = dtMember.GetString("MEMB_ENAME").Trim();
                    String MEMB_ESURNAME = dtMember.GetString("MEMB_ESURNAME").Trim();

                    THAI_NAME = (MEMB_NAME + " " + MEMB_SURNAME + Space).Substring(0, 50);
                    ENGS_NAME = (MEMB_ENAME + " " + MEMB_ESURNAME.Substring(0, 1) + "." + Space).Substring(0, 50);
                }
                else return;

                if (DPDEPT_FLAG == 1)
                {
                    String SqlCheck = "SELECT INFO_FLAG, DPDEPT_FLAG FROM ATMMEMBER WHERE SAVING_ACC IS NOT NULL AND MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                    Sdt dt = WebUtil.QuerySdt(SqlCheck);
                    if (dt.Next())
                    {
                        Decimal MAININFO_FLAG = dt.GetDecimal("INFO_FLAG");
                        if (MAININFO_FLAG != 1) return; //ต้องมี Info ก่อนถึงจะทำรายการนี้ได้
                        Decimal MAINDPDEPT_FLAG = dt.GetDecimal("DPDEPT_FLAG");
                        if (MAINDPDEPT_FLAG == 0) FUNCTION = "A";
                        else FUNCTION = "C";
                        UPDATE_FLAG = 1;
                    }
                    else return; //ไม่ทำเพราะหาเลข Bank ไม่ได้
                }
                else if (DPDEPT_FLAG == -9)
                {
                    FUNCTION = "D";
                    UPDATE_FLAG = 0;
                }
                else
                {
                    return; //ข้ามคนนั้นไปเลย
                }

                String SqlLoancontract = "SELECT DEPTACCOUNT_NO FROM ATMDEPT WHERE ACCOUNT_STATUS = 1 AND MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                Sdt dtLoanCont = WebUtil.QuerySdt(SqlLoancontract);
                if (dtLoanCont.Next())
                {
                    DEPTACCOUNT_NO = dtLoanCont.GetString("DEPTACCOUNT_NO").Trim();
                    DEPTACCOUNT_NO = "0000000000" + DEPTACCOUNT_NO;
                    DEPTACCOUNT_NO = DEPTACCOUNT_NO.Substring(DEPTACCOUNT_NO.Length - 10);
                }
                else return; //ไม่มีสัญญาเงินกู้

                String SqlUpdate = "UPDATE ATMMEMBER SET DPDEPT_FLAG = " + UPDATE_FLAG + " WHERE MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                WebUtil.Query(SqlUpdate);
                SqlUpdate = "UPDATE ATMBANKPROCESS SET DPDEPT_FLAG = 0 WHERE MEMBER_NO = '" + Member_No + "' AND COOP_ID = '" + COOP_ID + "'";
                WebUtil.Query(SqlUpdate);
                Reccord++;
                DataStringWriter.WriteLine(REC_TYPE + COOP_TYPE + COOP_CUST + RESERVE + DATA_TYPE + FUNCTION + THAI_NAME + ENGS_NAME + RESERVE2 + DEPTACCOUNT_NO + RESERVE3);
                LogProcess(COOP_ID, Member_No, 5, FUNCTION, WorkDate);
            }
            catch
            {
                return;
            }
        }

        private void LogProcess(String COOP_ID, String MEMBER_NO, int DATA_TYPE, String FUNCTION_TYPE, DateTime PROCESS_DATE)
        {
            try
            {
                //DATA_TYPE [0:DELETE_ALL, 1:INFORMATION, 2:LOAN_RECEIVE, 3:DEPOSIT_WITHDRAW, 4:LOAN_PAYMENT, 5:DEPOSIT_DEPOSIT]
                String SqlUpdate = @"INSERT INTO ATMBANKPROCESSDET  
                                             ( COOP_ID,   
                                               MEMBER_NO,   
                                               DATA_TYPE,   
                                               FUNCTION_TYPE,   
                                               PROCESS_DATE )  
                                      VALUES ( '" + COOP_ID + @"',   
                                               '" + MEMBER_NO + @"',    
                                               '" + DATA_TYPE + @"',   
                                               '" + FUNCTION_TYPE + @"',   
                                               to_date('" + PROCESS_DATE.ToString("ddMMyyyy HHmmss", en) + @"','ddmmyyyy hh24miss')) ";
                WebUtil.Query(SqlUpdate);
            }
            catch
            {

            }
        }
    }
}


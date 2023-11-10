using System;
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
using DataLibrary;
using Saving.WcfCommon;

namespace Saving.Applications.lawsys
{
    public partial class w_sheet_lw_lawmaster : PageWebSheet, WebSheet
    {
        private string pbl = "lw_lawmaster.pbl";
        protected string postMemberNo;
        protected string postLoanContractNo;
        protected string postLoanContractDialog;
        protected string postOpenWorking;
        protected string postWorkingEdit;

        private DwThDate tDwCancel;
        private DwThDate tDwCollDetail;
        private DwThDate tDwLawMaster;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postMemberNo = WebUtil.JsPostBack(this, "postMemberNo");
            postLoanContractNo = WebUtil.JsPostBack(this, "postLoanContractNo");
            postLoanContractDialog = WebUtil.JsPostBack(this, "postLoanContractDialog");
            postOpenWorking = WebUtil.JsPostBack(this, "postOpenWorking");
            postWorkingEdit = WebUtil.JsPostBack(this, "postWorkingEdit");

            tDwCancel = new DwThDate(DwCancel, this);
            tDwCancel.Add("received_date", "received_tdate");
            tDwCancel.Add("transhare_date", "transhare_tdate");
            tDwCancel.Add("trancoll_date", "trancoll_tdate");

            tDwCollDetail = new DwThDate(DwCollDetail, this);
            tDwCollDetail.Add("trancoll_date", "trancoll_tdate");
            tDwCollDetail.Add("lastpayment_date", "lastpayment_tdate");

            tDwLawMaster = new DwThDate(DwLawMaster, this);
            tDwLawMaster.Add("lawregister_date", "lawregister_tdate");
            tDwLawMaster.Add("indict_date", "indict_tdate");
            tDwLawMaster.Add("judge_date", "judge_tdate");
            tDwLawMaster.Add("receivedoc_date", "receivedoc_tdate");
            tDwLawMaster.Add("lawexe_date", "lawexe_tdate");
        }

        public void WebSheetLoadBegin()
        {
            HdIsOpenLoanDialog.Value = "false";
            HdIsOpenWorking.Value = "false";
            if (!IsPostBack)
            {
                HdIsPostBack.Value = "false";
                DwMain.InsertRow(0);
                DwLoan.InsertRow(0);
                DwLawMaster.InsertRow(0);
            }
            else
            {
                HdIsPostBack.Value = "true";
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwLoan);

                this.RestoreContextDw(DwLawMaster);
                this.RestoreContextDw(DwWorking);

                //ทั้งแท็บบอกเลิกสัญญา
                this.RestoreContextDw(DwCancel);
                this.RestoreContextDw(DwCollDetail);
                this.RestoreContextDw(DwLoanOther);
            }
            divTableMessage.Visible = false;
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postMemberNo")
            {
                JsPostMemberNo();
            }
            else if (eventArg == "postLoanContractNo")
            {
                JsPostLoanContractNo();
            }
            else if (eventArg == "postLoanContractDialog")
            {
                JsPostLoanContractDialog();
            }
            else if (eventArg == "postOpenWorking")
            {
                JsPostOpenWorking();
            }
            else if (eventArg == "postWorkingEdit")
            {
                object[] loanNoArray = new object[] { DwUtil.GetString(DwLoan, 1, "loancontract_no", "") };
                DwUtil.RetrieveDataWindow(DwWorking, pbl, null, loanNoArray);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกการดำเนินงานสำเร็จ");
                //postWorkingEdit
            }
        }

        public void SaveWebSheet()
        {
            string loancontract_no = DwUtil.GetString(DwLoan, 1, "loancontract_no", "");
            if (loancontract_no != "")
            {
                Sta ta = new Sta(state.SsConnectionString);
                bool isError = false;
                ta.Transection();
                try
                {
                    UpdateDwDetail(ta);
                    ta.Commit();
                }
                catch (Exception ex)
                {
                    isError = true;
                    ta.RollBack();
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                ta.Close();
                if (!isError)
                {
                    JsPostLoanContractNo();
                }
            }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                string memberNo = WebUtil.MemberNoFormat(DwUtil.GetString(DwMain, 1, "member_no", ""));
                if (!string.IsNullOrEmpty(memberNo))
                {
                    DwUtil.RetrieveDDDW(DwLoan, "loancontract_no", pbl, new object[] { memberNo });
                }
            }
            catch { }
            DwMain.SaveDataCache();
            DwLoan.SaveDataCache();
            DwLawMaster.SaveDataCache();
            DwWorking.SaveDataCache();
            //ทั้งแท็บยกเลิกสัญญา
            DwCancel.SaveDataCache();
            DwCollDetail.SaveDataCache();
            DwLoanOther.SaveDataCache();


        }

        #endregion

        private void JsPostMemberNo()
        {
            string memberNo = WebUtil.MemberNoFormat(DwMain.GetItemString(1, "member_no"));
            object[] memberNoArray = new object[] { memberNo };
            DwLoan.Reset();
            DwLoan.InsertRow(0);
            DwCancel.Reset();
            DwCollDetail.Reset();
            DwLoanOther.Reset();
        }

        private void JsPostLoanContractNo()
        {
            object[] loanNoArray = new object[] { DwUtil.GetString(DwLoan, 1, "loancontract_no", "") };
            DwUtil.RetrieveDataWindow(DwLoan, pbl, null, loanNoArray);
            bool isCancel = DwUtil.GetInt(DwLoan, 1, "cancel_status", 0) == 1;
            if (!isCancel)
            {
                divTableMessage.Visible = true;
                LbLoanNumberMessage.Text = loanNoArray[0].ToString();
            }
            else
            {
                try
                {
                    CommonClient comSrv = wcf.Common;
                    String dwobjectName = DwCancel.DataWindowObject;
                    String strDS = comSrv.GetXmlDataStore(state.SsWsPass, state.SsApplication, pbl, dwobjectName, loanNoArray);
                    DwCancel.Reset();
                    DwCancel.ImportString(strDS, Sybase.DataWindow.FileSaveAsType.Xml);
                    tDwCancel.Eng2ThaiAllRow();
                    DwUtil.RetrieveDataWindow(DwCollDetail, pbl, null, loanNoArray);
                    string memberNo = WebUtil.MemberNoFormat(DwMain.GetItemString(1, "member_no"));
                    DwUtil.RetrieveDataWindow(DwLoanOther, pbl, null, new object[] { memberNo, loanNoArray[0].ToString() });


                    dwobjectName = DwLawMaster.DataWindowObject;
                    strDS = comSrv.GetXmlDataStore(state.SsWsPass, state.SsApplication, pbl, dwobjectName, loanNoArray);
                    DwLawMaster.Reset();
                    DwLawMaster.ImportString(strDS, Sybase.DataWindow.FileSaveAsType.Xml);
                    tDwLawMaster.Eng2ThaiAllRow();

                    try
                    {
                        DwUtil.RetrieveDataWindow(DwWorking, pbl, null, loanNoArray);
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }

        private void JsPostLoanContractDialog()
        {
            Session["sslncontno"] = HdOpenLoanDialog.Value;
            HdIsOpenLoanDialog.Value = "true";
        }

        private void JsPostOpenWorking()
        {
            Session["sslncontno"] = DwLoan.GetItemString(1, "loancontract_no");
            HdIsOpenWorking.Value = "true";
        }

        private String OraDT(DateTime values)
        {
            return "to_date('" + values.ToString("yyyy-MM-dd", WebUtil.EN) + "', 'yyyy-mm-dd')";
        }

        private String OraStr(string values)
        {
            return string.IsNullOrEmpty(values) ? "null" : "'" + values + "'";
        }

        private int InsertDwDetail(Sta ta)
        {
            //            DateTime receieveDate = DwUtil.GetDateTime(DwCancel, 1, "received_date");
            //            string loancontract_no = DwUtil.GetString(DwCancel, 1, "loancontract_no", "");
            //            string memberNo = DwUtil.GetString(DwMain, 1, "member_no", "");
            //            string sql = @"INSERT INTO LWCANCELMASTER
            //                          (RECEIVED_DATE, LOANCONTRACT_NO, MEMBER_NO)
            //                           VALUES
            //                          (" + OraDT(receieveDate) + ", '" + loancontract_no + "', '" + memberNo + "')";
            //            int result01 = ta.Exe(sql);
            //            if (result01 <= 0) throw new Exception("ลงทะเบียนข้อมูลยกเลิกสัญญา[01] ไม่สำเร็จ");
            //            string sqlContColl = @"
            //            SELECT 
            //                LNCONTCOLL.LOANCONTRACT_NO,
            //                LNCONTCOLL.REF_COLLNO,
            //                LNCONTCOLL.DESCRIPTION,
            //                LNCONTMASTER.PRINCIPAL_BALANCE,
            //                LNCONTCOLL.COLL_PERCENT,   
            //                LNCONTCOLL.LOANCOLLTYPE_CODE,   
            //                MBUCFMEMBGROUP.MEMBGROUP_DESC  
            //            FROM 
            //                LNCONTCOLL,   
            //                LNCONTMASTER,   
            //                MBMEMBMASTER,   
            //                MBUCFMEMBGROUP  
            //            WHERE ( lncontmaster.member_no = mbmembmaster.member_no (+)) and  
            //                 ( mbucfmembgroup.membgroup_code (+) = mbmembmaster.membgroup_code) and  
            //                 ( LNCONTMASTER.LOANCONTRACT_NO = LNCONTCOLL.LOANCONTRACT_NO ) and  
            //                 ( lncontcoll.loancontract_no = '" + loancontract_no + @"' AND
            //                 ( lncontcoll.coll_status = 1 ) )";
            //            Sdt dt = ta.Query(sqlContColl);
            //            int ii = 0;
            //            while (dt.Next())
            //            {
            //                string sqlInsertContColl = "insert into lwcontcoll(loancontract_no, seq_no, ref_collno)values('" + loancontract_no + "', " + (ii + 1) + ", '" + dt.GetString("ref_collno").Trim() + "')";
            //                ta.Exe(sqlInsertContColl);
            //                ii++;
            //                if (ii > 99)
            //                {
            //                    throw new Exception("Error infinity loop! register cancel loan");
            //                    break;
            //                }
            //            }
            int ii = -1;
            return ii;
        }

        //        private int UpdateDwDetail(Sta ta)
        //        {
        //            string loancontract_no = DwUtil.GetString(DwDetail, 1, "loancontract_no", "");
        //            string received_date = OraDT(DwUtil.GetDateTime(DwDetail, 1, "received_date"));
        //            string enforcement_date = DwUtil.GetDateTime(DwDetail, 1, "enforcement_date") == new DateTime(1370, 1, 1) ? "null" : OraDT(DwUtil.GetDateTime(DwDetail, 1, "enforcement_date"));
        //            string indict_date = DwUtil.GetDateTime(DwDetail, 1, "indict_date") == new DateTime(1370, 1, 1) ? "null" : OraDT(DwUtil.GetDateTime(DwDetail, 1, "indict_date"));
        //            string judge_date = DwUtil.GetDateTime(DwDetail, 1, "judge_date") == new DateTime(1370, 1, 1) ? "null" : OraDT(DwUtil.GetDateTime(DwDetail, 1, "judge_date"));
        //            string judge_desc = OraStr(DwUtil.GetString(DwDetail, 1, "judge_desc", ""));
        //            string case_blackno = OraStr(DwUtil.GetString(DwDetail, 1, "case_blackno", ""));
        //            string case_redno = OraStr(DwUtil.GetString(DwDetail, 1, "case_redno", ""));
        //            string court_name = OraStr(DwUtil.GetString(DwDetail, 1, "court_name", ""));
        //            string status = OraStr(DwUtil.GetString(DwDetail, 1, "status", ""));
        //            string result_require = OraStr(DwUtil.GetString(DwDetail, 1, "result_require", ""));
        //            string remark = OraStr(DwUtil.GetString(DwDetail, 1, "remark", ""));
        //            string sql = @"
        //                            update lwcancelmaster
        //                            set
        //                                received_date = " + received_date + @",
        //                                enforcement_date = " + enforcement_date + @",
        //                                indict_date = " + indict_date + @",
        //                                judge_date = " + judge_date + @",
        //                                case_blackno = " + case_blackno + @",
        //                                case_redno = " + case_redno + @",
        //                                court_name = " + court_name + @",
        //                                status = " + status + @",
        //                                judge_desc = " + judge_desc + @",
        //                                result_require = " + result_require + @",
        //                                remark = " + remark + @"
        //                            where loancontract_no = '" + loancontract_no + "' and received_no = '" + received_no + "'";
        //            return ta.Exe(sql);
        //        }

        private int UpdateDwDetail(Sta ta)
        {
            string loancontract_no = DwUtil.GetString(DwCancel, 1, "loancontract_no", "");

            string lawregister_date = OraDT(DwUtil.GetDateTime(DwLawMaster, 1, "lawregister_date"));
            string indict_date = OraDT(DwUtil.GetDateTime(DwLawMaster, 1, "indict_date"));
            string judge_date = OraDT(DwUtil.GetDateTime(DwLawMaster, 1, "judge_date"));
            string receivedoc_date = OraDT(DwUtil.GetDateTime(DwLawMaster, 1, "receivedoc_date"));
            string lawexe_date = OraDT(DwUtil.GetDateTime(DwLawMaster, 1, "lawexe_date"));
            decimal status = DwUtil.GetDec(DwLawMaster, 1, "status", 0);
            decimal indict_amt = DwUtil.GetDec(DwLawMaster, 1, "indict_amt", 0);
            decimal judgedoc_status = DwUtil.GetDec(DwLawMaster, 1, "judgedoc_status", 0);
            decimal document_amt = DwUtil.GetDec(DwLawMaster, 1, "document_amt", 0);
            decimal asset_face = DwUtil.GetDec(DwLawMaster, 1, "asset_face", 0);
            string court_name = OraStr(DwUtil.GetString(DwLawMaster, 1, "court_name", ""));
            string case_blackno = OraStr(DwUtil.GetString(DwLawMaster, 1, "case_blackno", ""));
            string case_redno = OraStr(DwUtil.GetString(DwLawMaster, 1, "case_redno", ""));
            string remark = OraStr(DwUtil.GetString(DwLawMaster, 1, "remark", ""));
            string judge_desc = OraStr(DwUtil.GetString(DwLawMaster, 1, "judge_desc", ""));
            string asset_desc = OraStr(DwUtil.GetString(DwLawMaster, 1, "asset_desc", ""));
            string lawexe_desc = OraStr(DwUtil.GetString(DwLawMaster, 1, "lawexe_desc", ""));
            string sql = @"
            update lwcancelmaster
            set
                lawregister_date = " + lawregister_date + @",
                indict_date = " + indict_date + @",
                judge_date = " + judge_date + @",
                receivedoc_date = " + receivedoc_date + @",
                lawexe_date = " + lawexe_date + @",
                status = " + status + @",
                indict_amt = " + indict_amt + @",
                judgedoc_status = " + judgedoc_status + @",
                document_amt = " + document_amt + @",
                asset_face = " + asset_face + @",
                court_name = " + court_name + @",
                case_blackno = " + case_blackno + @",
                case_redno = " + case_redno + @",
                remark = " + remark + @",
                judge_desc = " + judge_desc + @",
                asset_desc = " + asset_desc + @",
                lawexe_desc = " + lawexe_desc + @"
            where loancontract_no = '" + loancontract_no + "'";
            return ta.Exe(sql);
        }


    }
}

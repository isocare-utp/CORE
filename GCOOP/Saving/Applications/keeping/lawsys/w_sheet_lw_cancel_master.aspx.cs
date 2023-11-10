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
    public partial class w_sheet_lw_cancel_master : PageWebSheet, WebSheet
    {
        private string pbl = "lw_cancel_master.pbl";
        protected string postMemberNo;
        protected string postLoanContractNo;
        protected string postLoanContractDialog;

        private DwThDate tDwCancel;
        private DwThDate tDwCollDetail;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postMemberNo = WebUtil.JsPostBack(this, "postMemberNo");
            postLoanContractNo = WebUtil.JsPostBack(this, "postLoanContractNo");
            postLoanContractDialog = WebUtil.JsPostBack(this, "postLoanContractDialog");

            tDwCancel = new DwThDate(DwCancel, this);
            tDwCancel.Add("received_date", "received_tdate");
            tDwCancel.Add("transhare_date", "transhare_tdate");
            tDwCancel.Add("trancoll_date", "trancoll_tdate");

            tDwCollDetail = new DwThDate(DwCollDetail, this);
            tDwCollDetail.Add("trancoll_date", "trancoll_tdate");
            tDwCollDetail.Add("lastpayment_date", "lastpayment_tdate");
        }

        public void WebSheetLoadBegin()
        {
            HdIsOpenLoanDialog.Value = "false";
            if (!IsPostBack)
            {
                HdIsPostBack.Value = "false";
                DwMain.InsertRow(0);
                DwLoan.InsertRow(0);
            }
            else
            {
                HdIsPostBack.Value = "true";
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwLoan);
                this.RestoreContextDw(DwLoanAll);
                this.RestoreContextDw(DwShare);
                this.RestoreContextDw(DwCollAll);
                this.RestoreContextDw(DwCollWho);
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
                    bool isCancel = DwUtil.GetInt(DwLoan, 1, "cancel_status", 0) == 1;
                    if (!isCancel)
                    {
                        int ii = InsertDwDetail(ta);
                        if (ii >= 0)
                        {
                            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกการลงทะเบียนยกเลิกสัญญาสำเร็จ");
                        }
                    }
                    else
                    {
                        int ii = UpdateDwDetail(ta);
                        if (ii >= 0)
                        {
                            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                        }
                    }
                    ta.Commit();
                }
                catch (Exception ex)
                {
                    isError = true;
                    ta.RollBack();
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                ta.Close();
                //DwUtil.RetrieveDataWindow(DwLoan, pbl, null, new object[] { loancontract_no });
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
            DwLoanAll.SaveDataCache();
            DwShare.SaveDataCache();
            DwCollAll.SaveDataCache();
            DwCollWho.SaveDataCache();
            DwCancel.SaveDataCache();
            DwCollDetail.SaveDataCache();
            DwLoanOther.SaveDataCache();
        }

        #endregion

        private void JsPostMemberNo()
        {
            string memberNo = WebUtil.MemberNoFormat(DwMain.GetItemString(1, "member_no"));
            object[] memberNoArray = new object[] { memberNo };
            DwUtil.RetrieveDataWindow(DwMain, pbl, null, memberNoArray);
            DwUtil.RetrieveDataWindow(DwLoanAll, pbl, null, memberNoArray);
            DwUtil.RetrieveDataWindow(DwShare, pbl, null, memberNoArray);
            DwUtil.RetrieveDataWindow(DwCollAll, pbl, null, memberNoArray);
            DwUtil.RetrieveDataWindow(DwCollWho, pbl, null, memberNoArray);
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

                DwCancel.Reset();
                DwCancel.InsertRow(0);
                DwCancel.SetItemString(1, "loancontract_no", loanNoArray[0].ToString());
                DwCancel.SetItemDateTime(1, "received_date", state.SsWorkDate);
                tDwCancel.Eng2ThaiAllRow();
                DwCollDetail.Reset();

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
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            for (int i = 1; i <= DwCollDetail.RowCount; i++)
            {
                string tLoancontractNo = DwUtil.GetString(DwCollDetail, i, "loancontract_no");
                //string sqlAdd = "select * from lncontmaster where 
            }
        }

        private void JsPostLoanContractDialog()
        {
            Session["sslncontno"] = HdOpenLoanDialog.Value;
            HdIsOpenLoanDialog.Value = "true";
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
            DateTime receieveDate = DwUtil.GetDateTime(DwCancel, 1, "received_date");
            string loancontract_no = DwUtil.GetString(DwCancel, 1, "loancontract_no", "");
            string memberNo = DwUtil.GetString(DwMain, 1, "member_no", "");
            string sql = @"INSERT INTO LWCANCELMASTER
                          (RECEIVED_DATE, LOANCONTRACT_NO, MEMBER_NO)
                           VALUES
                          (" + OraDT(receieveDate) + ", '" + loancontract_no + "', '" + memberNo + "')";
            int result01 = ta.Exe(sql);
            if (result01 <= 0) throw new Exception("ลงทะเบียนข้อมูลยกเลิกสัญญา[01] ไม่สำเร็จ");
            string sqlContColl = @"
            SELECT 
                LNCONTCOLL.LOANCONTRACT_NO,
                LNCONTCOLL.REF_COLLNO,
                LNCONTCOLL.DESCRIPTION,
                LNCONTMASTER.PRINCIPAL_BALANCE,
                LNCONTCOLL.COLL_PERCENT,   
                LNCONTCOLL.LOANCOLLTYPE_CODE,   
                MBUCFMEMBGROUP.MEMBGROUP_DESC  
            FROM 
                LNCONTCOLL,   
                LNCONTMASTER,   
                MBMEMBMASTER,   
                MBUCFMEMBGROUP  
            WHERE ( lncontmaster.member_no = mbmembmaster.member_no (+)) and  
                 ( mbucfmembgroup.membgroup_code (+) = mbmembmaster.membgroup_code) and  
                 ( LNCONTMASTER.LOANCONTRACT_NO = LNCONTCOLL.LOANCONTRACT_NO ) and  
                 ( lncontcoll.loancontract_no = '" + loancontract_no + @"' AND
                 ( lncontcoll.coll_status = 1 ) )";
            Sdt dt = ta.Query(sqlContColl);
            int ii = 0;
            while (dt.Next())
            {
                string sqlInsertContColl = "insert into lwcontcoll(loancontract_no, seq_no, ref_collno)values('" + loancontract_no + "', " + (ii + 1) + ", '" + dt.GetString("ref_collno").Trim() + "')";
                ta.Exe(sqlInsertContColl);
                ii++;
                if (ii > 99)
                {
                    throw new Exception("Error infinity loop! register cancel loan");
                    break;
                }
            }
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
            string received_date = OraDT(DwUtil.GetDateTime(DwCancel, 1, "received_date"));
            string transhare_date = OraDT(DwUtil.GetDateTime(DwCancel, 1, "transhare_date"));
            string book_no = OraStr(DwUtil.GetString(DwCancel, 1, "book_no", ""));
            string retrie_reason = OraStr(DwUtil.GetString(DwCancel, 1, "retrie_reason", ""));
            decimal trancoll_period = DwUtil.GetDec(DwCancel, 1, "trancoll_period", 0);
            string trancoll_date = OraDT(DwUtil.GetDateTime(DwCancel, 1, "trancoll_date"));
            decimal retrie_result = DwUtil.GetDec(DwCancel, 1, "retrie_result", 0);
            string result_require = OraStr(DwUtil.GetString(DwCancel, 1, "result_require", ""));
            string sql = @"
                        update lwcancelmaster
                        set
                            received_date = " + received_date + @",
                            transhare_date = " + transhare_date + @",
                            book_no = " + book_no + @",
                            retrie_reason = " + retrie_reason + @",
                            trancoll_period = " + trancoll_period + @",
                            trancoll_date = " + trancoll_date + @",
                            retrie_result = " + retrie_result + @",
                            result_require = " + result_require + @"
                        where loancontract_no = '" + loancontract_no + "' ";
            return ta.Exe(sql);
        }
    }
}

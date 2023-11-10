using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNShrlon;
using System.IO;
using System.Data;
using DataLibrary;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Services.Protocols;
//using CoreSavingLibrary.WcfReport;
using CoreSavingLibrary;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Script.Serialization;

namespace Saving.Applications.mbshr.ws_mbshr_update_shr_ctrl
{
    public partial class ws_mbshr_update_shr : PageWebSheet, WebSheet
    {
        private n_shrlonClient shrlonService;
        [JsPostBack]
        public string JsUpdateShr { get; set; }

        public void InitJsPostBack()
        {

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
            if (eventArg == "JsUpdateShr")
            {
                UpdateShr();
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }

        //อัฟเดทข้อมูลหุ้นฐาน
        public void UpdateShr()
        {
            ExecuteDataSource ex = new ExecuteDataSource(this);
            string sql_chkmemb = "";
            string member_no, membtype_code, code_person;
            decimal salary_amount, minshare_amt, periodbase_amt, periodshare_amt, sum_salary_amount, incomeetc_amt;
            if (state.SsCoopControl == "022001")
            {
                sql_chkmemb = @"select * from mbmembmaster where member_type = 1 and resign_status = 0 and mbmembgroup_code between 'A' and 'F90000'";
            }
            else
            {
                sql_chkmemb = @"select * from mbmembmaster where member_type = 1 and resign_status = 0";
            }
    
            Sdt dtBN = WebUtil.QuerySdt(sql_chkmemb);

            while (dtBN.Next())
            {
                member_no = dtBN.GetString("member_no");
                membtype_code = dtBN.GetString("membtype_code");
                code_person = dtBN.GetString("code_person");
                salary_amount = dtBN.GetDecimal("salary_amount");
                incomeetc_amt = dtBN.GetDecimal("incomeetc_amt");
                sum_salary_amount = salary_amount + incomeetc_amt;

                string sql_chkslr = @"select start_salary, end_salary, minshare_amt from shsharetypemthrate where {0} between start_salary and end_salary";
                sql_chkslr = WebUtil.SQLFormat(sql_chkslr, sum_salary_amount);
                Sdt dtSLR = WebUtil.QuerySdt(sql_chkslr);

                if (dtSLR.Next())
                {
                    minshare_amt = dtSLR.GetDecimal("minshare_amt");

                    string sql_chkslrbase = @"select periodbase_amt, periodshare_amt from shsharemaster where member_no = {0}";
                    sql_chkslrbase = WebUtil.SQLFormat(sql_chkslrbase, member_no);
                    Sdt dtSLRBB = WebUtil.QuerySdt(sql_chkslrbase);

                    if (dtSLRBB.Next())
                    {
                        periodbase_amt = dtSLRBB.GetDecimal("periodbase_amt");
                        periodshare_amt = dtSLRBB.GetDecimal("periodshare_amt");

                        if (minshare_amt > periodshare_amt)
                        {
                            //update periodshare_amt
                            string sql_updateperiodshare = @"update shsharemaster set periodshare_amt = {0} where member_no = {1}";
                            object[] argslist_updateperiodshare = new object[] { minshare_amt, member_no };

                            sql_updateperiodshare = WebUtil.SQLFormat(sql_updateperiodshare, argslist_updateperiodshare);
                            ex.SQL.Add(sql_updateperiodshare);
                            ex.Execute();
                            ex.SQL.Clear();

                            if (minshare_amt > periodbase_amt)
                            {
                                //update periodbase_amt
                                string sql_updateperiodbase = @"update shsharemaster set periodbase_amt = {0} where member_no = {1}";
                                object[] argslist_updateperiodbase = new object[] { minshare_amt, member_no };

                                sql_updateperiodbase = WebUtil.SQLFormat(sql_updateperiodbase, argslist_updateperiodbase);
                                ex.SQL.Add(sql_updateperiodbase);
                                ex.Execute();
                                ex.SQL.Clear();
                            }
                        }
                        else 
                        {
                            if (minshare_amt > periodbase_amt)
                            {
                                //update periodbase_amt
                                string sql_updateperiodbase = @"update shsharemaster set periodbase_amt = {0} where member_no = {1}";
                                object[] argslist_updateperiodbase = new object[] { minshare_amt, member_no };

                                sql_updateperiodbase = WebUtil.SQLFormat(sql_updateperiodbase, argslist_updateperiodbase);
                                ex.SQL.Add(sql_updateperiodbase);
                                ex.Execute();
                                ex.SQL.Clear();
                            }
                        }
                    }

                    LtServerMessage.Text = WebUtil.CompleteMessage("ทำรายการเรียบร้อย");
                }
            }
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
    }
}
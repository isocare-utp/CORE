using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user
{
    public partial class u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user : PageWebReport, WebReport
    {
        protected String app;
        protected String gid;
        protected String rid;
        [JsPostBack]
        public String PostMemberNo;
        [JsPostBack]
        public String PostName { get; set; }

        string bahtTH_sum_sharestk_amt = "";
        //string  = "";

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            PostMemberNo = WebUtil.JsPostBack(this, "PostMemberNo");
            //PostName = WebUtil.JsPostBack(this, "PostName");
        }

        public void WebSheetLoadBegin()
        {
            //--- Page Arguments
            try
            {
                app = Request["app"].ToString();
            }
            catch { }
            if (app == null || app == "")
            {
                app = state.SsApplication;
            }
            try
            {
                gid = Request["gid"].ToString();
            }
            catch { }
            try
            {
                rid = Request["rid"].ToString();
            }
            catch { }

            //Report Name.
            try
            {
                Sta ta = new Sta(state.SsConnectionString);
                String sql = "";
                sql = @"SELECT REPORT_NAME  
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
                Sdt dt = ta.Query(sql);
                ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }

            if (!IsPostBack)
            {

                dsMain.DATA[0].us = Convert.ToDecimal(33.19);
                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            //if (eventArg == PostMemberNo)
            //{
            //    string membno = dsMain.DATA[0].memno;
            //    membno = WebUtil.MemberNoFormat(membno);
            //    dsMain.DATA[0].memno = membno;  
            //}
                if (eventArg == "PostMemberNo")
                {
                    string membno = WebUtil.MemberNoFormat(dsMain.DATA[0].memno);
                    dsMain.DATA[0].memno = membno;
                    of_GetMemName();
                    //dsMain.RetrieveMain(membno);
                }
                if (eventArg == "PostName")
                {
                    of_GetMemName();
                }
        }

        public void RunReport()
        {
            string ls_memno2="%",ls_memno3="%";
            string ls_memno1 = dsMain.DATA[0].memno;
            string ls_deptaccountno = dsMain.DATA[0].deptaccount_no; decimal prncbal = 0; decimal sharestk_amt = 0;
            if (ls_deptaccountno != "")
            {
                ls_memno3 = dsMain.DATA[0].memno;
                ls_deptaccountno = ls_deptaccountno.Substring(1, ls_deptaccountno.Length-2);
            }
            string ls_share = dsMain.DATA[0].c_share;
            if(ls_share=="1"){ ls_memno2=dsMain.DATA[0].memno;}
            
            string ls_guarantee = dsMain.DATA[0].guarantee;
            string ls_language = dsMain.DATA[0].language;
            string ls_manager = dsMain.DATA[0].as_manager;
            string ls_seconder = dsMain.DATA[0].seconder;
            string ls_username = dsMain.DATA[0].user_name;
            string ls_country = dsMain.DATA[0].country;
            //decimal ls_us = dsMain.DATA[0].us;

            string ls_deptaccountname = dsMain.DATA[0].deptaccount_name;

            try
            {
                string sql_dropview = "drop view report_certificate_new";
                sql_dropview = WebUtil.SQLFormat(sql_dropview);
                Sdt dtIns_dropview = WebUtil.QuerySdt(sql_dropview);
            }
            catch
            {
            }
            

             //////////////// select เอาค่าหุ้น

            string sql_share = @"select (sharestk_amt * 10) as sharestk_amt from shsharemaster where sharemaster_status = 1 and coop_id = {0} and member_no = {1}";
            sql_share = WebUtil.SQLFormat(sql_share, state.SsCoopControl, dsMain.DATA[0].memno);
            Sdt dt_share = WebUtil.QuerySdt(sql_share);
            if (dt_share.Next())
            {
                sharestk_amt = dt_share.GetDecimal("sharestk_amt");

                //// ทำค่า sum รวมเป็นภาษาไทย
                string bahtTxt, n;
                double amount;
                try { amount = Convert.ToDouble(sharestk_amt); }
                catch { amount = 0; }
                bahtTxt = amount.ToString("####.00");
                string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
                string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
                string[] temp = bahtTxt.Split('.');
                string intVal = temp[0];
                string decVal = temp[1];
                if (Convert.ToDouble(bahtTxt) == 0)
                    bahtTH_sum_sharestk_amt = "ศูนย์บาทถ้วน";
                else
                {
                    for (int j = 0; j < intVal.Length; j++)
                    {
                        n = intVal.Substring(j, 1);
                        if (n != "0")
                        {
                            if ((j == (intVal.Length - 1)) && (n == "1"))
                                bahtTH_sum_sharestk_amt += "เอ็ด";
                            else if ((j == (intVal.Length - 2)) && (n == "2"))
                                bahtTH_sum_sharestk_amt += "ยี่";
                            else if ((j == (intVal.Length - 2)) && (n == "1"))
                                bahtTH_sum_sharestk_amt += "";
                            else
                                bahtTH_sum_sharestk_amt += num[Convert.ToInt32(n)];
                            bahtTH_sum_sharestk_amt += rank[(intVal.Length - j) - 1];
                        }
                    }
                    bahtTH_sum_sharestk_amt += "บาท";
                    if (decVal == "00")
                        bahtTH_sum_sharestk_amt += "ถ้วน";
                    else
                    {
                        for (int j = 0; j < decVal.Length; j++)
                        {
                            n = decVal.Substring(j, 1);
                            if (n != "0")
                            {
                                if ((j == decVal.Length - 1) && (n == "1"))
                                    bahtTH_sum_sharestk_amt += "เอ็ด";
                                else if ((j == (decVal.Length - 2)) && (n == "2"))
                                    bahtTH_sum_sharestk_amt += "ยี่";
                                else if ((j == (decVal.Length - 2)) && (n == "1"))
                                    bahtTH_sum_sharestk_amt += "";
                                else
                                    bahtTH_sum_sharestk_amt += num[Convert.ToInt32(n)];
                                bahtTH_sum_sharestk_amt += rank[(decVal.Length - j) - 1];
                            }
                        }
                        bahtTH_sum_sharestk_amt += "สตางค์";
                    }
                }

            }




            string sql = @"create view report_certificate_new as select distinct mb.member_no,
            'MAS' as system,
            '' as  prncbal1,
            '' as  sharestk_amt1,
            '' as  prncbal_us,
            '' as  sharestk_amt_us,
            '' as  sharestk_shr,
            '' as  deptaccount_no,
            'Name of Account:' as eng_desc,
            '' as type_desc,
            '' as type_shere,
            '' as type_tbh,
            '' as type_us,
            
            (pn.PRENAME_desc+mb.MEMB_NAME+' '+mb.MEMB_SURNAME) as fullname_thai,
            '' as deptaccount_name,
            0 as prncbal,
            0 as sharestk_amt,
            '' as bth_thai,
            1 as seq_no
            from  mbmembmaster mb
            inner join MBUCFPRENAME pn on  pn.PRENAME_CODE = mb.PRENAME_CODE
            where
             mb.member_no ={1}
            and mb.coop_id = {0}


            union

            select distinct mb.member_no,
            'DEP' as system ,
           LTRIM(RTRIM(CONVERT(varchar,FORMAT(dp.prncbal,'#,##0.00')))) as prncbal1 ,
            '' as sharestk_amt1,
		LTRIM(RTRIM(CONVERT(varchar,FORMAT(dp.prncbal / {5},'#,##0.00')))) as prncbal_us ,
            '' as sharestk_amt_us,
            '' as sharestk_shr,
            substring(dp.deptaccount_no,0,3)+'-'+substring(dp.deptaccount_no,4,2)+'-'+substring(dp.deptaccount_no,6,5) as deptaccount_no,
            'Type of Accounts : The deposit of saving account(s) number' as eng_desc,
            '' as type_desc,
            'credit' as type_shere,
            'balance THB' as type_tbh,
            'Approx US $' as type_us,
            '' as fullname_thai,
            dp.deptaccount_name,
            dp.prncbal ,
            0 as sharestk_amt,
            '' as bth_thai,
            2 as seq_no
            from  mbmembmaster mb
            inner join MBUCFPRENAME pn on  pn.PRENAME_CODE = mb.PRENAME_CODE
            inner join dpdeptmaster dp on mb.member_no = dp.member_no
           where
             mb.member_no ={2}
            and dp.deptaccount_no in({4})
            and mb.coop_id = {0}
            and dp.deptclose_status= 0

            union

          select distinct mb.member_no,
            'SHR' as system ,
            '' as  prncbal1,    
		LTRIM(RTRIM(CONVERT(varchar,FORMAT(s.sharestk_amt,'#,##0.00')))) as sharestk_amt1,
            '' as  prncbal_us,
		LTRIM(RTRIM(CONVERT(varchar,FORMAT(s.sharestk_amt*10,'#,##0.00')))) as sharestk_amt_us,
		LTRIM(RTRIM(CONVERT(varchar,FORMAT(s.sharestk_amt*10,'#,##0.00')))) as sharestk_shr,
            '' as  deptaccount_no,
            'The member of Tak Saving and Credit Co-operative for Officials in Ministry of Education Limited member' as eng_desc,
            'holding' as type_desc,
            'share' as type_shere,
            'equal THB' as type_tbh,
            'Approx US $' as type_us,
            '' as  fullname_thai,
            '' as deptaccount_name,
            0 as prncbal,
            s.sharestk_amt,
            '" + bahtTH_sum_sharestk_amt + @"' as bth_thai,
            3 as seq_no
            from  mbmembmaster mb
            inner join shsharemaster s on  s.member_no = mb.member_no
            where
             mb.member_no = {3}            and mb.coop_id = {0}
            and s.sharemaster_status = 1 
            ";
                //sql = sql.Replace("$P{as_coopid}", format2);
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_memno1, ls_memno3, ls_memno2, ls_deptaccountno, dsMain.DATA[0].us);
                Sdt dtIns = WebUtil.QuerySdt(sql);

                string sql1 = @"delete FROM  report_certificate "   ;
                sql1 = WebUtil.SQLFormat(sql1);
                Sdt dt_dept2 = WebUtil.QuerySdt(sql1);

                string sql2 = @"INSERT INTO report_certificate (member_no ,
                            system ,
                            prncbal1 ,
                            sharestk_amt1 ,
                            prncbal_us ,
                            sharestk_amt_us ,
                            sharestk_shr ,
                            deptaccount_no ,
                            eng_desc ,
                            type_desc ,
                            type_shere ,
                            type_tbh ,
                            type_us ,
                            fullname_thai  ,
                            deptaccount_name ,
                            prncbal ,
                            sharestk_amt ,
                            bth_thai ,
                            seq_no )
                            SELECT member_no ,
                            system ,
                            prncbal1 ,
                            sharestk_amt1 ,
                            prncbal_us ,
                            sharestk_amt_us ,
                            sharestk_shr ,
                            deptaccount_no ,
                            eng_desc ,
                            type_desc ,
                            type_shere ,
                            type_tbh ,
                            type_us ,
                            fullname_thai  ,
                            deptaccount_name ,
                            prncbal ,
                            sharestk_amt ,
                            bth_thai ,
                            seq_no
                            FROM report_certificate_new";
                sql2 = WebUtil.SQLFormat(sql2);
                Sdt dt_dept1 = WebUtil.QuerySdt(sql2);

                ///// select เอาค่าเงินฝาก
            string deptaccount_no = "";
                string sql_dept = @"select 
                                        deptaccount_no,
                                        prncbal from report_certificate_new where system = 'DEP'";
                sql_dept = WebUtil.SQLFormat(sql_dept);
                Sdt dt_dept = WebUtil.QuerySdt(sql_dept);
                if (dt_dept.Next())
                {

                    for (int r = 0; r < dt_dept.GetRowCount(); r++)
                    {

                        deptaccount_no = dt_dept.Rows[r]["deptaccount_no"].ToString();
                        prncbal = Convert.ToDecimal(dt_dept.Rows[r]["prncbal"].ToString());

                        //bahtTH_sum_prncbal


                         //// ทำค่า sum รวมเป็นภาษาไทย
                string bahtTxt, n; string bahtTH_sum_prncbal = "";
                double amount;
                try { amount = Convert.ToDouble(prncbal); }
                catch { amount = 0; }
                bahtTxt = amount.ToString("####.00");
                string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
                string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
                string[] temp = bahtTxt.Split('.');
                string intVal = temp[0];
                string decVal = temp[1];
                if (Convert.ToDouble(bahtTxt) == 0)
                    bahtTH_sum_prncbal = "ศูนย์บาทถ้วน";
                else
                {
                    for (int j = 0; j < intVal.Length; j++)
                    {
                        n = intVal.Substring(j, 1);
                        if (n != "0")
                        {
                            if ((j == (intVal.Length - 1)) && (n == "1"))
                                bahtTH_sum_prncbal += "เอ็ด";
                            else if ((j == (intVal.Length - 2)) && (n == "2"))
                                bahtTH_sum_prncbal += "ยี่";
                            else if ((j == (intVal.Length - 2)) && (n == "1"))
                                bahtTH_sum_prncbal += "";
                            else
                                bahtTH_sum_prncbal += num[Convert.ToInt32(n)];
                            bahtTH_sum_prncbal += rank[(intVal.Length - j) - 1];
                        }
                    }
                    bahtTH_sum_prncbal += "บาท";
                    if (decVal == "00")
                        bahtTH_sum_prncbal += "ถ้วน";
                    else
                    {
                        for (int j = 0; j < decVal.Length; j++)
                        {
                            n = decVal.Substring(j, 1);
                            if (n != "0")
                            {
                                if ((j == decVal.Length - 1) && (n == "1"))
                                    bahtTH_sum_prncbal += "เอ็ด";
                                else if ((j == (decVal.Length - 2)) && (n == "2"))
                                    bahtTH_sum_prncbal += "ยี่";
                                else if ((j == (decVal.Length - 2)) && (n == "1"))
                                    bahtTH_sum_prncbal += "";
                                else
                                    bahtTH_sum_prncbal += num[Convert.ToInt32(n)];
                                bahtTH_sum_prncbal += rank[(decVal.Length - j) - 1];
                            }
                        }
                        bahtTH_sum_prncbal += "สตางค์";
                    }
                }

                        string ls_sql = @"update report_certificate set bth_thai = {0} where deptaccount_no = {1}";
                        ls_sql = WebUtil.SQLFormat(ls_sql, bahtTH_sum_prncbal, deptaccount_no);
                        Sdt dt;
                        dt = WebUtil.QuerySdt(ls_sql);

            }

                    }


                


            try
            {
                string report_name = "";
                if (ls_language == "thai") { report_name = "rpt_certificate_thai"; }
                else if (ls_language == "eng") { report_name = "rpt_certificate"; }
                string report_label = report_name;
                iReportArgument arg = new iReportArgument();
                arg.Add("as_manager", iReportArgumentType.String, ls_manager);
                arg.Add("as_name", iReportArgumentType.String, ls_seconder);
                arg.Add("as_type", iReportArgumentType.Integer, ls_guarantee);

                iReportBuider report = new iReportBuider(this, arg);
                //iReportBuider report = new iReportBuider(this, "");
                //report.AddCriteria("r_ln_print_loan_req_doc_rbt", "สัญญาเงินกู้", ReportType.pdf, args);
                report.AddCriteria(report_name, report_label, ReportType.pdf, arg);
                report.AutoOpenPDF = true;
                report.Retrieve();

                
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {

        }

        public void of_GetMemName()
        {
            if (dsMain.DATA[0].language == "thai")
            {
                string sql = @"select  (pn.PRENAME_desc+mb.MEMB_NAME +' '+ mb.MEMB_SURNAME) as  mem_name  from  mbmembmaster mb           
            inner join MBUCFPRENAME pn on pn.PRENAME_CODE = mb.PRENAME_CODE
            WHERE mb.coop_id={0} and  mb.member_no={1} ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].memno);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsMain.DATA[0].seconder = dt.GetString("mem_name");
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "init_memname", "init_memname(1)", true);
            }
            else
            {
                dsMain.DATA[0].seconder = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "init_memname", "init_memname(2)", true);
            }
        }
    }
}
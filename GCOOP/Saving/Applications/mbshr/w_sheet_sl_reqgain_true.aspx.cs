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
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using System.Web.Services.Protocols;
using DataLibrary;
using Sybase.DataWindow;


namespace Saving.Applications.mbshr
{
    public partial class w_sheet_sl_reqgain_true : PageWebSheet, WebSheet
    {
        //private DwThDate tDwMain;
        private String pbl = "sl_reqgain_true.pbl";
        protected String postMemberno;
        protected String postSalaryId;
        protected String DetailAddRow;
        protected String DetailDelRow;
        protected DateTime birhtdate;
        protected DateTime retrydate;
        protected String docno;
        protected String ndocno;
        private DwThDate tdwMain;
        private n_commonClient cmdService;

        #region WebSheet Members
        public void InitJsPostBack()
        {
            postMemberno = WebUtil.JsPostBack(this, "postMemberno");
            postSalaryId = WebUtil.JsPostBack(this, "postSalaryId");
            DetailAddRow = WebUtil.JsPostBack(this, "DetailAddRow");
            DetailDelRow = WebUtil.JsPostBack(this, "DetailDelRow");
            tdwMain = new DwThDate(dw_gain, this);
            tdwMain.Add("write_date", "write_tdate");
            tdwMain.Add("retry_date", "retry_tdate");
        }

        public void WebSheetLoadBegin()
        {
            cmdService = wcf.NCommon;
            this.ConnectSQLCA();
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_gain);
                this.RestoreContextDw(dw_gaindetail);
            }
            else
            {
                dw_gain.Reset();
                dw_gaindetail.Reset();
                dw_gain.InsertRow(0);
                dw_gain.SetItemDateTime(1, "write_date", state.SsWorkDate);
                dw_gain.SetItemDateTime(1, "retry_date", state.SsWorkDate);
            }
            DwUtil.RetrieveDDDW(dw_gaindetail, "prename_code", pbl, null);
            DwUtil.RetrieveDDDW(dw_gaindetail, "gain_relation", pbl, null);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postMemberno")
            {
                JsPostMemberno();
            }
            else if (eventArg == "postSalaryId")
            {
                JsPostSalaryId();
            }
            else if (eventArg == "DetailAddRow")
            {
                JsDetailAddRow();
            }
            else if (eventArg == "DetailDelRow")
            {
                JsDetailDelRow();
            }
        }

        public void SaveWebSheet()
        {
            string coopid = state.SsCoopControl;
            //======================== Last Docno ================================//
            DataTable dt_doc = WebUtil.Query(@"SELECT last_documentno
            FROM   CMDOCUMENTCONTROL
            WHERE  DOCUMENT_CODE ='MBREGGAINDOCNO' AND COOP_ID='" + coopid + "'");

            if (dt_doc.Rows.Count > 0)
            {
                docno = dt_doc.Rows[0]["last_documentno"].ToString();
            }

            //======================== Check Gain Docno ================================//
            String membno = dw_gain.GetItemString(1, "member_no");
            DataTable dt_mem = WebUtil.Query(@"SELECT MEMBER_NO 
            FROM   MBGAINMASTER
            WHERE  COOP_ID='" + coopid + "'AND MEMBER_NO ='" + membno + "'");
            int rowmemb = dt_mem.Rows.Count;

            //======================== Count Row Detail ================================//
            int row_detail = dw_gaindetail.RowCount;

            //======================== Check Row ================================//
            if ((rowmemb == 0) && (row_detail > 0))
            {
                try
                {
                    SetGainMaster(row_detail, membno);

                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย ...");
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if ((rowmemb > 0) && (row_detail > 0))
            {

                ndocno = (Convert.ToInt32(docno) + 1).ToString("0000000000");

                SetNewData(ndocno, coopid, membno);
                SetOldData(ndocno, coopid, membno);
                try
                {
                    String sqlDelete = "DELETE FROM MBGAINMASTER WHERE COOP_ID ='" + coopid + "' AND MEMBER_NO ='" + membno + "'";
                    Sta taDelete = new Sta(state.SsConnectionString);
                    int result = taDelete.Exe(sqlDelete);

                    if (result > 0)
                    {
                        SetGainMaster(row_detail, membno);
                    }
                    //   else { LtServerMessage.Text = WebUtil.ErrorMessage("DELETE ERROR"); }
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                //=================================UPDATE CMDOCUMENTCONTROL====================================//
                try
                {
                    String doc = (Convert.ToInt32(docno) + 1).ToString();
                    String sqlUpdate = ("UPDATE CMDOCUMENTCONTROL SET LAST_DOCUMENTNO = '" + doc + "' WHERE DOCUMENT_CODE = 'MBREGGAINDOCNO'");
                    Sta taUpdate = new Sta(state.SsConnectionString);
                    int re = taUpdate.Exe(sqlUpdate);
                    JsClear();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย ...");
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }

        }

        public void WebSheetLoadEnd()
        {
            dw_gain.SaveDataCache();
            dw_gaindetail.SaveDataCache();
            tdwMain.Eng2ThaiAllRow();
        }
        #endregion

        public void JsDetailAddRow()
        {
            dw_gaindetail.InsertRow(0);
        }

        public void JsDetailDelRow()
        {
            int row = Convert.ToInt32(HRow.Value);
            if (row != 0)
            {
                dw_gaindetail.DeleteRow(row);
            }
        }

        public void JsPostMemberno()
        {
            String memno = WebUtil.MemberNoFormat(dw_gain.GetItemString(1, "member_no"));
            HfMemberNo.Value = memno;
            String coopid = state.SsCoopControl;
            DataTable dt = WebUtil.Query(@" SELECT  MBUCFMEMBGROUP.MEMBGROUP_CODE as group_code,
                                                    MBUCFMEMBGROUP.MEMBGROUP_DESC as group_desc,
                                                    MBMEMBMASTER.MEMB_NAME as membname,
                                                    MBMEMBMASTER.MEMB_SURNAME as surname,
                                                    MBMEMBMASTER.BIRTH_DATE as birthdate,
                                                    MBMEMBMASTER.RETRY_DATE as retrydate,
                                                    MBUCFPRENAME.PRENAME_DESC as prename,
		                                            MBMEMBMASTER.SALARY_ID as salaryid
                                            FROM    MBMEMBMASTER,   
                                                    MBUCFMEMBGROUP,   
                                                    MBUCFPRENAME  
                                            WHERE   MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE  and  
                                                    MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP.COOP_ID  and  
                                                    MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE  and  
                                                    MBMEMBMASTER.COOP_ID = '" + coopid + "' and MBMEMBMASTER.MEMBER_NO ='" + memno + "'");
            if (dt.Rows.Count > 0)
            {
                dw_gain.Reset();
                dw_gain.InsertRow(0);
                dw_gain.SetItemDateTime(1, "write_date", state.SsWorkDate);
                try
                {
                    retrydate = Convert.ToDateTime(dt.Rows[0]["retrydate"].ToString());
                }
                catch { retrydate = state.SsWorkDate; }
                try
                {
                    birhtdate = Convert.ToDateTime(dt.Rows[0]["birthdate"].ToString());
                }
                catch { birhtdate = state.SsWorkDate; }
                String name = dt.Rows[0]["prename"].ToString() + dt.Rows[0]["membname"].ToString() + ' ' + dt.Rows[0]["surname"].ToString();
                dw_gain.SetItemString(1, "member_no", memno);
                dw_gain.SetItemString(1, "mbname", name);
                dw_gain.SetItemString(1, "age", Convert.ToString(Convert.ToInt32(DateTime.Now.Year - birhtdate.Year)));
                dw_gain.SetItemString(1, "membgroup_code", dt.Rows[0]["group_code"].ToString().Trim());
                dw_gain.SetItemString(1, "membgroup_desc", dt.Rows[0]["group_desc"].ToString().Trim());
                dw_gain.SetItemString(1, "salary_id", dt.Rows[0]["salaryid"].ToString().Trim());
                dw_gain.SetItemDateTime(1, "retry_date", retrydate);
                dw_gain.SetItemString(1, "write_at", "");
                GetDetail();
            }
        }

        private void JsPostSalaryId()
        {
            String memberNo = "";
            String salary_id = dw_gain.GetItemString(1, "salary_id").Trim();
            //ดึงเลขสมาชิกจากเลขพนักงาน
            string sqlMemb = @"select member_no from mbmembmaster where salary_id like '" + salary_id + @"%' and member_status = 1";
            Sdt dtMemb = WebUtil.QuerySdt(sqlMemb);
            if (dtMemb.Next())
            {
                memberNo = dtMemb.GetString("member_no");
                dw_gain.SetItemString(1, "member_no", memberNo);
                JsPostMemberno();
            }
            else
            {
                this.JsClear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขสมาชิก " + memberNo);
            }
        }

        public void JsClear()
        {
            dw_gain.Reset();
            dw_gaindetail.Reset();
            dw_gain.InsertRow(0);
            dw_gain.SetItemDateTime(1, "write_date", state.SsWorkDate);
            dw_gain.SetItemDateTime(1, "retry_date", state.SsWorkDate);
        }

        private void GetDetail()
        {
            String coopid = state.SsCoopControl;
            Int32 ll_row = 0;
            DataTable dt1 = WebUtil.Query(@"SELECT WRITE_AT, REMARK, GAIN_NAME, GAIN_SURNAME, GAIN_ADDR, GAIN_RELATION, (case when  GAIN_PERCENT is null then 0 else GAIN_PERCENT end)  AS GAIN_PERCENT
            FROM   MBGAINMASTER
            WHERE  COOP_ID ='" + coopid + "' AND MEMBER_NO ='" + HfMemberNo.Value + "'");
            if (dt1.Rows.Count > 0)
            {
                dw_gain.SetItemString(1, "write_at", dt1.Rows[0]["write_at"].ToString());
                dw_gain.SetItemString(1, "remark", dt1.Rows[0]["remark"].ToString());

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ll_row = dw_gaindetail.InsertRow(0);

                    dw_gaindetail.SetItemDecimal(ll_row, "seq_no", ll_row);
                    dw_gaindetail.SetItemString(ll_row, "gain_name", dt1.Rows[i]["gain_name"].ToString());
                    dw_gaindetail.SetItemString(ll_row, "gain_surname", dt1.Rows[i]["gain_surname"].ToString());
                    dw_gaindetail.SetItemString(ll_row, "gain_addr", dt1.Rows[i]["gain_addr"].ToString());
                    dw_gaindetail.SetItemString(ll_row, "gain_relation", dt1.Rows[i]["gain_relation"].ToString());
                    dw_gaindetail.SetItemDecimal(ll_row, "gain_percent", Convert.ToDecimal(dt1.Rows[i]["gain_percent"]));
                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.WarningMessage("เลขสมาชิกนี้ยังไม่มีผู้รับผลประโยชน์..");
            }

        }

        public int row { get; set; }

        private void SetGainMaster(int row_detail, String membno)
        {

            //======================== Get Main================================//
            DateTime writeDate = dw_gain.GetItemDateTime(1, "write_date");
            String writeAt = dw_gain.GetItemString(1, "write_at");
            String age = dw_gain.GetItemString(1, "age");
            String gremark = "";
            decimal gain_percent = 0;

            //try { gremark = dw_gain.GetItemString(1, "remark"); }
            //catch { gremark = ""; }

            String write_date_dd = "";
            String write_date_mm = "";
            String write_date_yy = "";
            try
            {
                write_date_dd = writeDate.Day.ToString();
                write_date_mm = writeDate.Month.ToString();
                write_date_yy = writeDate.Year.ToString();
            }
            catch { }

            String gname = "", gsurname = "", gaddr = "", grelation = "" ,prename="";
            //======================== INSERT MBGAINMASTER================================//
            for (int i = 1; i <= row_detail; i++)
            {
                try { grelation = dw_gaindetail.GetItemString(i, "prename_code"); }
                catch { prename = ""; }

                try { gname = dw_gaindetail.GetItemString(i, "gain_name"); }
                catch { gname = ""; }

                try { gsurname = dw_gaindetail.GetItemString(i, "gain_surname"); }
                catch { gsurname = ""; }

                try { gaddr = dw_gaindetail.GetItemString(i, "gain_addr"); }
                catch { gaddr = ""; }

                try { grelation = dw_gaindetail.GetItemString(i, "gain_relation"); }
                catch { grelation = ""; }

                try { gain_percent = dw_gaindetail.GetItemDecimal(i, "gain_percent"); }
                catch { gain_percent = 0; }

                try { gremark = dw_gaindetail.GetItemString(i, "remark"); }
                catch { gremark = ""; }

                try
                {
                    String sqlInsert = @"INSERT INTO MBGAINMASTER(COOP_ID,MEMBER_NO,SEQ_NO,GAIN_NAME,GAIN_SURNAME,GAIN_ADDR,GAIN_RELATION,REMARK,WRITE_DATE, WRITE_AT,AGE,GAIN_PERCENT,PRENAME_CODE) VALUES 
                    ('" + state.SsCoopControl + "','" + membno + "','" + i + "','" + gname + "','" + gsurname + "','" + gaddr + "','" + grelation + "','" + gremark + "',convert(DATETIME,'" + write_date_yy + "/" + write_date_mm + "/" + write_date_dd + "'),'" + writeAt + "','" + age + "','" + gain_percent + "','" + prename + "')";
                    Sta taInsert = new Sta(state.SsConnectionString);
                    int result = taInsert.Exe(sqlInsert);
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }

        }

        private void SetNewData(String ndocno, String coopid, String membno)
        {

            DateTime ngWriteDate = dw_gain.GetItemDateTime(1, "write_date");
            String ngWriteAt = dw_gain.GetItemString(1, "write_at");
            String ngAge = dw_gain.GetItemString(1, "age");
            String ngName, ngSurname, ngAddr, ngRelation, ngRemark;
            String ngWrite_date_dd = "";
            String ngWrite_date_mm = "";
            String ngWrite_date_yy = "";
            decimal gain_percent = 0;
            try
            {
                ngWrite_date_dd = ngWriteDate.Day.ToString();
                ngWrite_date_mm = ngWriteDate.Month.ToString();
                ngWrite_date_yy = ngWriteDate.Year.ToString();
            }
            catch { }
            //=================================INSERT REQGAIN NEW====================================//
            try
            {
                //=================================HARD MEMCOOP_IP====================================//
                String sqlInsertreq = @"INSERT INTO MBREQGAIN(COOP_ID,GAIN_DOCNO,MEMCOOP_ID,MEMBER_NO,WRITE_DATE, WRITE_AT,AGE,ENTRY_ID,ENTRY_DATE) VALUES 
                        ('" + state.SsCoopControl + "','" + ndocno + "','" + state.SsCoopControl + "','" + membno + "',CONVERT(DATETIME,'" + ngWrite_date_yy + "/" + ngWrite_date_mm + "/" + ngWrite_date_dd + "'),'" + ngWriteAt + "','" + ngAge + "','" + state.SsUsername + "',CONVERT(DATETIME,'" + ngWrite_date_yy + "/" + ngWrite_date_mm + "/" + ngWrite_date_dd + "'))";
                Sta taInsertreq = new Sta(state.SsConnectionString);
                int result2 = taInsertreq.Exe(sqlInsertreq);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

            int rowDetail = dw_gaindetail.RowCount;
            if (rowDetail > 0)
            {
                for (int j = 1; j <= rowDetail; j++)
                {
                    try
                    {
                        ngName = dw_gaindetail.GetItemString(j, "gain_name");
                    }
                    catch { ngName = ""; }

                    try
                    {
                        ngSurname = dw_gaindetail.GetItemString(j, "gain_surname");
                    }
                    catch { ngSurname = ""; }
                    try
                    {
                        ngAddr = dw_gaindetail.GetItemString(j, "gain_addr");
                    }
                    catch { ngAddr = ""; }
                    try
                    {
                        ngRelation = dw_gaindetail.GetItemString(j, "gain_relation");
                    }
                    catch { ngRelation = ""; }
                    try
                    {
                        ngRemark = dw_gaindetail.GetItemString(j, "remark");
                    }
                    catch { ngRemark = ""; }
                    try
                    {
                        gain_percent = dw_gaindetail.GetItemDecimal(j, "gain_percent");
                    }
                    catch { gain_percent = 0; }



                    //=================================INSERT REQGAINDETAIL NEW====================================//
                    try
                    {
                        //=================================HARD TYPE====================================//
                        String sqlInsertreqdetail = @"INSERT INTO MBREQGAINDETAIL(COOP_ID,GAIN_DOCNO,GAIN_CHGTYPE,SEQ_NO,GAIN_NAME,GAIN_SURNAME,GAIN_ADDR,GAIN_RELATION,REMARK,gain_percent) VALUES 
                            ('" + state.SsCoopControl + "','" + ndocno + "','" + "NEW" + "','" + j + "','" + ngName + "','" + ngSurname + "','" + ngAddr + "','" + ngRelation + "','" + ngRemark + "'," + gain_percent + ")";
                        Sta taInsertreqdetail = new Sta(state.SsConnectionString);
                        int result3 = taInsertreqdetail.Exe(sqlInsertreqdetail);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
            }


        }

        private void SetOldData(String ndocno, String coopid, String membno)
        {
            DataTable dt_main = WebUtil.Query(@"SELECT *
                FROM MBGAINMASTER   
                WHERE COOP_ID = '" + coopid + "' and MEMBER_NO ='" + membno + "'");

            if (dt_main.Rows.Count > 0)
            {

                /*DateTime ogWriteDate = Convert.ToDateTime(dt_main.Rows[0]["write_date"].ToString());
                String ogWriteAt = dt_main.Rows[0]["write_at"].ToString();
                String ogAge = dt_main.Rows[0]["age"].ToString();

                String ogWrite_date_dd = "";
                String ogWrite_date_mm = "";
                String ogWrite_date_yy = "";
                try
                {
                    ogWrite_date_dd = ogWriteDate.Day.ToString();
                    ogWrite_date_mm = ogWriteDate.Month.ToString();
                    ogWrite_date_yy = ogWriteDate.Year.ToString();
                }
                catch { }*/
                //                //=================================INSERT REQGAIN OLD====================================//
                //                try
                //                {
                //                    //=================================HARD MEMCOOP_IP====================================//
                //                    String sqlInsertreq = @"INSERT INTO MBREQGAIN(COOP_ID,GAIN_DOCNO,MEMCOOP_ID,MEMBER_NO,WRITE_DATE, WRITE_AT,AGE,ENTRY_ID,ENTRY_DATE) VALUES 
                //                        ('" + coopid + "','" + ndocno + "','" + coopid + "','" + membno + "',to_date('" + ogWrite_date_dd + "/" + ogWrite_date_mm + "/" + ogWrite_date_yy + "','dd/mm/yyyy'),'" + ogWriteAt + "','" + ogAge + "','" + state.SsUsername + "',to_date('" + ogWrite_date_dd + "/" + ogWrite_date_mm + "/" + ogWrite_date_yy + "','dd/mm/yyyy'))";
                //                    Sta taInsertreq = new Sta(state.SsConnectionString);
                //                    int result2 = taInsertreq.Exe(sqlInsertreq);
                //                }
                //                catch(Exception ex){LtServerMessage.Text=WebUtil.ErrorMessage(ex);}

                for (int j = 1; j <= dt_main.Rows.Count; j++)
                {
                    String ogName = dt_main.Rows[j - 1]["gain_name"].ToString();
                    String ogSurname = dt_main.Rows[j - 1]["gain_surname"].ToString();
                    String ogAddr = dt_main.Rows[j - 1]["gain_addr"].ToString();
                    String ogRelation = dt_main.Rows[j - 1]["gain_relation"].ToString();
                    String ogRemark = dt_main.Rows[j - 1]["remark"].ToString();
                    //String ogPercent = dt_main.Rows[j - 1]["gain_percent"].ToString();
                    //=================================INSERT REQGAINDETAIL OLD====================================//
                    try
                    {
                        //=================================HARD TYPE====================================//
                        String sqlInsertreqdetail = @"INSERT INTO MBREQGAINDETAIL(COOP_ID,GAIN_DOCNO,GAIN_CHGTYPE,SEQ_NO,GAIN_NAME,GAIN_SURNAME,GAIN_ADDR,GAIN_RELATION,REMARK) VALUES 
                            ('" + coopid + "','" + ndocno + "','" + "OLD" + "','" + j + "','" + ogName + "','" + ogSurname + "','" + ogAddr + "','" + ogRelation + "','" + ogRemark + "')";
                        Sta taInsertreqdetail = new Sta(state.SsConnectionString);
                        int result3 = taInsertreqdetail.Exe(sqlInsertreqdetail);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }

            }

        }
    }
}

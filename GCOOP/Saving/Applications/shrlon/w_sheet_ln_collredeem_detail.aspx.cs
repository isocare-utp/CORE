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
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_ln_collredeem_detail : PageWebSheet, WebSheet
    {
        private DwThDate tdwhead;

        protected String jsPostMember;
        protected String jsNewClear;
        protected String jsPostcollmast;

        public void InitJsPostBack()
        {
            jsNewClear = WebUtil.JsPostBack(this, "jsNewClear");
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            jsPostcollmast = WebUtil.JsPostBack(this, "jsPostcollmast");

            //ตั้งค่าให้วันที่
            tdwhead = new DwThDate(Dw_main, this);
            tdwhead.Add("redeem_date", "redeem_tdate");
            
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_main.SetTransaction(sqlca);
           
            if (IsPostBack)
            {
                this.RestoreContextDw(Dw_main);
            }
            else
            {
                JsNewClear();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            } 
            else if (eventArg == "jsPostcollmast")
            {
                JsNewCollmast();
            }
            else if (eventArg == "jsNewClear")
            {
                JsNewClear();
            }
           
        }

        public void SaveWebSheet()
        {
            String Date_reqreceive = "", Date_entry = "", Num_Row = "";
            String redeem_cause, mb_no, coll_code, redeem_tdate = "", entry_tdate = "";
            Decimal Last_Doc = 0;
            try
            {
                try
                {
                    mb_no = Dw_main.GetItemString(1, "member_no");
                }
                catch { mb_no = null; }
                try
                {
                    coll_code = Dw_main.GetItemString(1, "collmast_no").Trim();
                }
                catch { coll_code = null; }
                try
                {
                    redeem_cause = Dw_main.GetItemString(1, "redeem_cause");
                }
                catch { redeem_cause = null; }
                try
                {
                    redeem_tdate = Dw_main.GetItemString(1, "redeem_tdate");
                }
                catch { redeem_tdate = null; }
                try
                {
                    entry_tdate = Dw_main.GetItemString(1, "compute_2");
                }
                catch { entry_tdate = null; }
                

                try
                {
                    //ตัดวันที่
                    Date_reqreceive = Dw_main.GetItemString(1, "redeem_tdate");
                    Date_reqreceive = Date_reqreceive.Replace("/", "");

                    String str_date_lnreqreceive = Date_reqreceive.Substring(0, 2);
                    String str_month_lnreqreceive = Date_reqreceive.Substring(2, 2);
                    String str_year_lnreqreceive = Convert.ToString(Convert.ToInt32(Date_reqreceive.Substring(4, 4)) - 543);
                    Date_reqreceive = str_date_lnreqreceive + str_month_lnreqreceive + str_year_lnreqreceive;

                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                try
                {
                    Date_entry = Dw_main.GetItemString(1, "compute_2");
                    Date_entry = Date_entry.Replace("/", "");

                    String str_date_entry = Date_entry.Substring(0, 2);
                    String str_month_entry = Date_entry.Substring(2, 2);
                    String str_year_entry = Convert.ToString(Convert.ToInt32(Date_entry.Substring(4, 4)) - 543);
                    Date_entry = str_date_entry + str_month_entry + str_year_entry;

                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                //ดึงข้อมูลมานับเลข
                try
                {
                    Sta ta_se = new Sta(sqlca.ConnectionString);
                    String sql = @" select * from LNREQCOLLMASTREDEEM";
                    DataTable dt_con = WebUtil.Query(sql);

                    if (dt_con.Rows.Count >= 0)
                    {
                        Last_Doc = dt_con.Rows.Count + 1;
                        Num_Row = "LN" + Last_Doc.ToString("00000000");   
                    }

                }
                catch (Exception ex) { LtServerMessage.Text = ex.ToString(); }

                //เพิ่มลงเบส

                    Sta ta = new Sta(sqlca.ConnectionString);
                    try
                    {
                        String sql = @"  INSERT INTO LNREQCOLLMASTREDEEM  
                               ( REDEEM_DOCNO, REDEEM_DATE, MEMBER_NO, COLLMAST_NO, REDEEM_CAUSE, REDEEM_STATUS, ENTRY_ID, ENTRY_DATE)  
                                VALUES ( '" + Num_Row + "',to_date('" + Date_reqreceive + "','ddmmyyyy'),'" + mb_no + "','" + coll_code + "','" + redeem_cause + "',1,'" + state.SsUsername + "',to_date('" + Date_entry + "','ddmmyyyy'))";
                        ta.Exe(sql);
                        ta.Close();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                    try
                    {
                    Sta ta_udate = new Sta(sqlca.ConnectionString);
                    String sqlStr;
                 
                        sqlStr = @"  UPDATE lncollmaster
                                     SET  redeem_flag = 1, redeem_date =to_date('" + Date_reqreceive + "','ddmmyyyy') WHERE collmast_no = '" + coll_code + "'";
                        Sdt dt_update = ta.Query(sqlStr);
                        ta_udate.Close();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย");
                JsNewClear();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                JsNewClear();
            }
        }

        public void WebSheetLoadEnd()
        {
            Dw_main.SaveDataCache();
        }

        private int JsPostMember()
        {
            String pr_desc = "", mem_name = "", mem_surname = "";
            String ls_memno = Hfmember_no.Value.ToString();

            if (ls_memno != null)
            {
                try
                {
                    Sta ta_se = new Sta(sqlca.ConnectionString);
                    String sql = @" select mbucfprename.prename_desc,
                                             mbmembmaster.memb_name,
                                             mbmembmaster.memb_surname
                                      from mbmembmaster, mbucfprename, mbucfmembgroup
                                      where ( mbmembmaster.prename_code = mbucfprename.prename_code ) and
                                             ( mbmembmaster.member_no = '" + ls_memno + "')";

                    Sdt dt = ta_se.Query(sql);
                    if (dt.Next())
                    {

                        pr_desc = dt.GetString("prename_desc");
                        mem_name = dt.GetString("memb_name");
                        mem_surname = dt.GetString("memb_surname");
                        Dw_main.SetItemString(1, "prename_desc", pr_desc);
                        Dw_main.SetItemString(1, "memb_name", mem_name);
                        Dw_main.SetItemString(1, "memb_surname", mem_surname);

                        Dw_main.SetItemString(1, "collmast_no", "");
                        Dw_main.SetItemString(1, "collmast_refno", "");
                        Dw_main.SetItemDate(1, "mortgage_date", state.SsWorkDate);
                        Dw_main.SetItemString(1, "collmast_desc", "");
                        Dw_main.SetItemDecimal(1, "mortgage_price", 0);

                        DwUtil.RetrieveDDDW(Dw_main, "collmast_no", "sl_collredeem_detail.pbl", ls_memno);                      
                    }

                }
                catch (Exception ex) { LtServerMessage.Text = ex.ToString(); }
            }
            else
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่สมาชิก กรุณาตรวจสอบ");
                JsNewClear();
            }
            return 1;
        }

        private int JsNewCollmast()
        {
            String coll_refno = "", coll_desc= "";
            Decimal mort_price = 0;
            DateTime mort_date = new DateTime();
            String ls_collno = Hfcoll_no.Value.ToString();
            
            if (ls_collno != null)
            {
                try
                {
                    Sta ta_se = new Sta(sqlca.ConnectionString);
                    String sql = @" select collmast_refno, collmast_desc, mortgage_price, mortgage_date
                                    from lncollmaster
                                    where (collmast_no = '" + ls_collno + "')";

                 Sdt dt = ta_se.Query(sql);
                    if (dt.Next())
                    {
                        coll_refno = dt.GetString("collmast_refno");
                        coll_desc = dt.GetString("collmast_desc");
                        mort_price = dt.GetDecimal("mortgage_price");
                        mort_date = dt.GetDate("mortgage_date");
                        Dw_main.SetItemString(1, "collmast_refno", coll_refno);
                        Dw_main.SetItemString(1, "collmast_desc", coll_desc);
                        Dw_main.SetItemDecimal(1, "mortgage_price", mort_price);
                        Dw_main.SetItemDate(1, "mortgage_date", mort_date);
                       
                    }

                }
                catch (Exception ex) { LtServerMessage.Text = ex.ToString(); }
            }
            else
            {

                LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขทะเบียนอ้างอิง กรุณาตรวจสอบ");
                JsNewClear();
            }
            return 1;
        }

        private void JsNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);

            Dw_main.SetItemDate(1, "redeem_date", state.SsWorkDate);
            tdwhead.Eng2ThaiAllRow();
        }
    }
}
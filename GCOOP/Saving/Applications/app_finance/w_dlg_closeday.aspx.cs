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
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;
using System.Globalization;
using DataLibrary;

namespace Saving.Applications.app_finance
{
    public partial class w_dlg_closeday : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private DwThDate tDwMain;

        #region WebSheet Members

        void WebSheet.InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain);
            tDwMain.Add("operate_date", "operate_tdate");
            tDwMain.Add("entry_date", "entry_tdate");
            tDwMain.Add("close_date", "close_tdate");
        }

        void WebSheet.WebSheetLoadBegin()
        {
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                Int32 gph;
                String closeday_xml = "", chqwait_xml = "";

                try
                {
                    try
                    {
                        gph = fin.of_init_close_day(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsApplication, ref closeday_xml, ref chqwait_xml);
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage(ex);

                    }

                    DwMain.Reset();
                    DwMain.ImportString(closeday_xml, FileSaveAsType.Xml);

                    if (chqwait_xml != "")
                    {
                        //DwChqlist.ImportString(chqwait_xml, FileSaveAsType.Xml);
                        DwUtil.ImportData(chqwait_xml, DwChqlist, tDwMain, FileSaveAsType.Xml);
                    }

                    DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                    tDwMain.Eng2ThaiAllRow();

                    DwMain.SetItemDateTime(1, "close_date", state.SsWorkDate);
                    tDwMain.Eng2ThaiAllRow();

                    //DataWindowChild dc = DwMain.GetChild("coopbranch_id");
                    //DwMain.SetItemString(1, "coopbranch_id", state.SsCoopId);
                    //dc.ImportString(fin.GetChildBranch(state.SsWsPass), FileSaveAsType.Xml);
                    Decimal cash_foward = DwMain.GetItemDecimal(1, "cash_foward");
                    if (cash_foward < 0)
                    {
                        throw new Exception("ยอดเงินสดคงเหลือติดลบ กรุณาตรวจสอบ");
                    }
                }
                catch (Exception ex)
                {
                    CultureInfo ThaiCulture = new CultureInfo("th-TH");
                    //LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                    LtServerMessage.Text = WebUtil.WarningMessage("ระบบได้ทำการปิดสิ้นวันที่" + "   " + state.SsWorkDate.ToString("dd/MM/yyyy", ThaiCulture) + "   " + "เรียบร้อยแล้ว");
                }
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwChqlist);
            }

            DwMain.Modify("t_entry_time.text = '" + DateTime.Now.ToString("hh:mm:ss") + "'");
            DwMain.Modify("t_coopname.text='" + state.SsCoopName + "'");

            DwMain.SetItemString(1, "entry_date", Convert.ToString(state.SsWorkDate));
            tDwMain.Eng2ThaiAllRow();
        }

        void WebSheet.CheckJsPostBack(string eventArg)
        {

        }

        void WebSheet.SaveWebSheet()
        {
            try
            {
                String XmlMain = DwMain.Describe("DataWindow.Data.XML");
                String XmlChqlist = DwChqlist.Describe("DataWindow.Data.XML");
                Decimal cash_foward = DwMain.GetItemDecimal(1, "cash_foward");
                if (cash_foward >= 0)
                {
                    int re = fin.of_close_day(state.SsWsPass, state.SsApplication, XmlMain, XmlChqlist);
                    if (re == 1)
                    {
                        if (state.SsCoopControl == "040001")
                        {
                            try
                            {
                                string sql = "";

                                //ตั้งยอดยกมา  กรณีปิดวันซ้ำให้มีการลบก่อรทุกครั้ง  เพิ่มตาราง finbank_statement  finbankmaster // master อาจต่อยอดต่อไป
                                sql = @"delete from finbank_statement where entry_date = {0}";
                                sql = WebUtil.SQLFormat(sql, state.SsWorkDate);
                                WebUtil.ExeSQL(sql);

                                sql = @"insert into finbank_statement
                            (coop_id                    , account_no              , bank_code          , bankbranch_code           , seq_no 
                            , detail_desc           , entry_id            , entry_date           , operate_date            , item_status
                            ,balance_begin              , account_id   )
                            select fb.coop_id,fb.account_no,fb.bank_code,fb.bankbranch_code,'1',
                            fb.detail_desc,{0},{1},{1},'1', balance,account_id
                            from finbank_statement fb where entry_date = (select max(entry_date) from finbank_statement)";
                                sql = WebUtil.SQLFormat(sql, state.SsUsername, state.SsWorkDate);
                                WebUtil.ExeSQL(sql);

                                sql = @"
                            SELECT 
                            tofrom_accid as account_id,
                            sum( case when fs.pay_recv_status=1 then fs.itempay_amt else 0.00 end ) as recv  ,
                            sum( case when fs.pay_recv_status=0 then fs.itempay_amt else 0.00 end ) as pay
                            from finslip fs
                            where fs.entry_date = {0}
                            and fs.cash_type in('CHQ','CBT')
                            and payment_status=1
                            group by tofrom_accid

                            union
                                
                            SELECT
                            fb.account_id, 
                            sum(fd.itempayamt_net) as recv ,
                            0.00	as   pay
                            from finslip fs
                            inner join finslipdet fd on fd.slip_no = fs.slip_no
                            inner join finbankmaster fb on  ltrim(rtrim(fd.account_id))=fb.account_id					    
                            where fs.entry_date = {0}
                            and fs.cash_type ='CSH' and fs.from_system='FIN'
                            and payment_status=1 and  fs.pay_recv_status=0
                            GROUP BY fb.account_id
							
                            union
                            SELECT		
                            fb.account_id,	
                            0	as   recv,
                            sum(fd.itempayamt_net) as pay 	
                            from finslip fs	
                            inner join finslipdet fd on fd.slip_no = fs.slip_no	
                            inner join finbankmaster fb on  ltrim(rtrim(fd.account_id))=fb.account_id	
                            where fs.entry_date = {0}
                            and fs.cash_type <>'CSH' and fs.from_system='FIN'	
                            and fs.payment_status=1 and  fs.pay_recv_status=1	
                            GROUP BY fb.account_id	

							union
							
							SELECT 
                            fb.account_id, 
                            sum(fd.itempayamt_net) as recv ,
                            0.00	as   pay
                            from finslip fs
                            inner join finslipdet fd on fd.slip_no = fs.slip_no
                            inner join finbankmaster fb on  ltrim(rtrim(fd.account_id))=fb.account_id					    
                            where fs.entry_date = {0}
                            and fs.cash_type <>'CSH' and fs.from_system='FIN'
                            and payment_status=1 and  fs.pay_recv_status=0 
                            GROUP BY fb.account_id
							
                            union
                               
                            SELECT
                            fb.account_id, 
                            0.00 as recv  ,
                            sum(fd.itempayamt_net) as pay
                            from finslip fs
                            inner join finslipdet fd on fd.slip_no = fs.slip_no
                            inner join finbankmaster fb on  ltrim(rtrim(fd.account_id))=fb.account_id					    
                            where fs.entry_date = {0}
                            and fs.cash_type ='CSH' and fs.from_system='FIN'
                            and payment_status=1 and  fs.pay_recv_status=1
                            GROUP BY fb.account_id";
                                sql = WebUtil.SQLFormat(sql, state.SsWorkDate);
                                Sdt dt = WebUtil.QuerySdt(sql);
                                int ln_row = dt.Rows.Count;
                                string ls_accountid = "";
                                decimal ld_rec = 0, ld_pay = 0;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    dt.Next();
                                    ls_accountid = dt.GetString("account_id").Trim();
                                    ld_rec = dt.GetDecimal("recv");
                                    ld_pay = dt.GetDecimal("pay");

                                    string sql_ud_statement = @"update finbank_statement set recv = isnull(recv,0)+" + ld_rec + ",pay = isnull(pay,0)+" + ld_pay + " where entry_date={0}  and account_id='" + ls_accountid + "' ";
                                    sql_ud_statement = WebUtil.SQLFormat(sql_ud_statement, state.SsWorkDate);
                                    Sdt dt_ud_statement = WebUtil.QuerySdt(sql_ud_statement);
                                }
                                string sql_ud_statement2 = @"update finbank_statement set balance = isnull(balance_begin,0) + isnull(recv,0) - isnull(pay,0) where entry_date={0}";
                                sql_ud_statement2 = WebUtil.SQLFormat(sql_ud_statement2, state.SsWorkDate);
                                Sdt dt_ud_statement2 = WebUtil.QuerySdt(sql_ud_statement2);                                
                            }
                            catch
                            {
                                LtServerMessage.Text = WebUtil.WarningMessage("ปิดงานประจำวันเรียบร้อย แต่อัพเดทยอดคุมธนาคารไม่สำเร็จ!!!"); return;
                                //LtServerMessage.Text = WebUtil.WarningMessage("ระบบการเงิน ปิดสรุปวันเเล้ว เเต่ไม่สามารถ ปิดระบบที่ AppStatus กรุณาปิดที่ระบบ Admin");
                            }
                        }
                    }
                    LtServerMessage.Text = WebUtil.CompleteMessage("ปิดงานประจำวันเรียบร้อย");
                }
                else
                {
                    throw new Exception("ไม่สามารถบันทึกได้เนื่องจากยอดเงินสดคงเหลือติดลบ กรุณาตรวจสอบ");
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        void WebSheet.WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwChqlist.SaveDataCache();
        }

        #endregion
    }
}

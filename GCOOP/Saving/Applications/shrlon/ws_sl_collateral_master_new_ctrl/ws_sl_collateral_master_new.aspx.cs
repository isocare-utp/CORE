using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using System.Data;
using System.IO;
namespace Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl
{
    public partial class ws_sl_collateral_master_new : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostInsertRowMemco { get; set; }
        [JsPostBack]
        public String PostMemcoNo { get; set; }
        [JsPostBack]
        public String PostInsertRowCollprop { get; set; }
        [JsPostBack]
        public String PostInsertRowReview { get; set; }
        [JsPostBack]
        public String PostDeleteRowMemco { get; set; }
        [JsPostBack]
        public String PostDeleteRowCollprop { get; set; }
        [JsPostBack]
        public String PostDeleteRowReview { get; set; }
        [JsPostBack]
        public String PostSearchRetrieve { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
            dsMemco.InitDsMemco(this);
            dsCollprop.InitDsCollprop(this);
            dsReview.InitDsReview(this);
            dsCollDetail.InitDsCollDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsDetail.DdCollmasttype();
                dsDetail.DdCollrelation();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(member_no);
                DefaultMemco(member_no);
            }
            else if (eventArg == PostInsertRowMemco)
            {
                dsMemco.InsertLastRow();
            }
            else if (eventArg == PostInsertRowCollprop)
            {
                dsCollprop.InsertLastRow();
            }
            else if (eventArg == PostInsertRowReview)
            {
                dsReview.InsertLastRow();
                int row = dsReview.RowCount;
                for (int i = 0; i < row; i++)
                {
                    dsReview.DATA[i].REVIEW_NO = i + 1;
                }
            }
            else if (eventArg == PostDeleteRowMemco)
            {
                int row = dsMemco.GetRowFocus();
                dsMemco.DeleteRow(row);
            }
            else if (eventArg == PostDeleteRowCollprop)
            {
                int row = dsCollprop.GetRowFocus();
                dsCollprop.DeleteRow(row);
            }
            else if (eventArg == PostDeleteRowReview)
            {
                int row = dsReview.GetRowFocus();
                dsReview.DeleteRow(row);
            }
            else if (eventArg == PostMemcoNo)
            {
                String member_no = WebUtil.MemberNoFormat(Hdmemco_no.Value);
                int row = dsMemco.GetRowFocus();
                String sql = @" SELECT MEMBER_NO,   
                           ( MBMEMBMASTER.MEMB_NAME||'  '||  MBMEMBMASTER.MEMB_SURNAME)as MEMB_NAME
                        FROM MBMEMBMASTER       WHERE   MBMEMBMASTER.MEMBER_NO={0} ";

                sql = WebUtil.SQLFormat(sql, member_no);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    // dsMemco.SetItemString(row, "memco_no", dt.Rows[0]["MEMBER_NO"].ToString());
                    dsMemco.DATA[row].MEMCO_NO = dt.GetString("MEMBER_NO");
                    dsMemco.DATA[row].mem_name = dt.GetString("MEMB_NAME");
                    // dw_detail.SetItemString(row, "mem_name", dt.Rows[0]["MEMB_NAME"].ToString());
                }
            }
            else if (eventArg == PostSearchRetrieve)
            {
                PostSearchRetrieve2();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                str_lncollmast strlncoll = new str_lncollmast();
                strlncoll.member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                strlncoll.xml_collmastdet = dsDetail.ExportXml();
                // strlncoll.xml_collmemco = dw_detail.RowCount > 0 ? dw_detail.Describe("DataWindow.Data.XML") : "";
                if (dsMemco.RowCount == 0)
                {
                    dsMemco.InsertLastRow(1);
                    dsMemco.DATA[0].MEMCO_NO = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                    dsMemco.DATA[0].COLLMASTMAIN_FLAG = 1;
                    dsMemco.DATA[0].COOP_ID = state.SsCoopControl;
                    dsMemco.DATA[0].MEMCOOP_ID = state.SsCoopId;
                }


                strlncoll.xml_collmemco = dsMemco.ExportXml();
                strlncoll.xml_review = dsReview.ExportXml();
                strlncoll.xml_prop = dsCollprop.ExportXml();

                strlncoll.entry_id = state.SsUsername;
                strlncoll.coop_id = state.SsCoopControl;

                int result = wcf.NShrlon.of_savelncollmast(state.SsWsPass, ref strlncoll);
                if (result == 1)
                {
                    SaveCollDetail(strlncoll.xml_collmastdet);
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อยแล้ว");
                    dsMain.ResetRow();
                    dsMemco.ResetRow();
                    dsCollprop.ResetRow();
                    dsReview.ResetRow();
                    dsDetail.ResetRow();
                    //JsPostMember();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {

        }
        public void SaveCollDetail(string xml_collmastdet)
        {
            try
            {
                string collmast_no="";
                DataSet ds0 = new DataSet();
                StringReader strd = new StringReader(xml_collmastdet);
                ds0.ReadXml(strd);
                DataTable dt0 = ds0.Tables[0];
                dt0.TableName = "collmastdet";
                collmast_no = dt0.Rows[0]["collmast_no"].ToString();
                dsCollDetail.DATA[0].COLLMAST_NO = collmast_no;
                dsCollDetail.DATA[0].COOP_ID = state.SsCoopId;
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                exed1.AddFormView(dsCollDetail, ExecuteType.Insert);
                int ii = exed1.Execute();
            }
            catch {
                throw new Exception("ไม่สามารถบันทึกรายละเอียดหลักทรัพย์(lncolldetail) ได้");
            }
        }
        public void PostSearchRetrieve2()
        {
            try
            {
                string member_no = WebUtil.MemberNoFormat(Hdmember_no.Value);
                dsMain.RetrieveMain(member_no);
                str_lncollmast strlncoll = new str_lncollmast();
                strlncoll.collmast_no = Hdcollmast_no.Value;
                strlncoll.coop_id = state.SsCoopControl;

                strlncoll.xml_collmastdet = dsDetail.ExportXml();
                strlncoll.xml_collmemco = dsMemco.ExportXml();
                strlncoll.xml_review = dsReview.ExportXml();
                strlncoll.xml_prop = dsCollprop.ExportXml();
                //strlncoll.xml_colluse = dsColluse.ExportXml();
                int result = wcf.NShrlon.of_initlncollmastdet(state.SsWsPass, ref  strlncoll);
                try
                {
                    try
                    {
                        dsDetail.ResetRow();
                        dsDetail.ImportData(strlncoll.xml_collmastdet);

                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }


                    if (dsDetail.RowCount > 1)
                    {
                        // dsDetail.DeleteRow(dsDetail.RowCount);
                    }

                    try
                    {
                        dsMemco.ResetRow();
                        dsMemco.ImportData(strlncoll.xml_collmemco);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                    try
                    {
                        dsReview.ResetRow();
                        dsReview.ImportData(strlncoll.xml_review);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                    try
                    {
                        dsCollprop.ResetRow();
                        dsCollprop.ImportData(strlncoll.xml_prop);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }



                    dsDetail.DdCollmasttype();
                    dsDetail.DdCollrelation();
                }
                catch
                {

                }


                //ไถ่ถอน

                decimal flag = dsDetail.DATA[0].REDEEM_FLAG;//.GetItemDecimal(1, "redeem_flag");
                // Hdfredeemflag.Value = Convert.ToString(flag);


                //dsList.SelectRow(0, false);
                //dw_list.SelectRow(li_row, true);
                //dw_list.SetRow(li_row);
                //}
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void DefaultMemco(string member_no) {
            try
            {
                dsMemco.InsertLastRow();
                String sql = @" SELECT MEMBER_NO,   
                           ( MBMEMBMASTER.MEMB_NAME||'  '||  MBMEMBMASTER.MEMB_SURNAME)as MEMB_NAME
                        FROM MBMEMBMASTER       WHERE   MBMEMBMASTER.MEMBER_NO={0} ";

                sql = WebUtil.SQLFormat(sql, member_no);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    // dsMemco.SetItemString(row, "memco_no", dt.Rows[0]["MEMBER_NO"].ToString());
                    dsMemco.DATA[0].MEMCO_NO = dt.GetString("MEMBER_NO");
                    dsMemco.DATA[0].mem_name = dt.GetString("MEMB_NAME");
                    dsMemco.DATA[0].COLLMASTMAIN_FLAG = 1;
                    // dw_detail.SetItemString(row, "mem_name", dt.Rows[0]["MEMB_NAME"].ToString());
                }
            }
            catch { }
        }
    }
}
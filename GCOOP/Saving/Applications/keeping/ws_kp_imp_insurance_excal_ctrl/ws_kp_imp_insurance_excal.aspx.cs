using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
//////////////////////////////////////////
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OracleClient;
using System.Globalization;

/////////////////////////////////////////
using System.IO;
using System.Text;
using OfficeOpenXml;
/////////////////////////////////////////


namespace Saving.Applications.keeping.ws_kp_imp_insurance_excal_ctrl
{
    public partial class ws_kp_imp_insurance_excal : PageWebSheet, WebSheet
    {

        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                as_year.Text = (state.SsWorkDate.Year + 543).ToString();
                as_month.SelectedValue = "00";
                DropDownListKeepType();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }
        public void DropDownListKeepType()
        {
            string sql = "select keepitemtype_code,keepothitemtype_desc from KPUCFKEEPOTHITEMTYPE where coop_id = '" + state.SsCoopControl + "'  ";
            Sdt dt = WebUtil.QuerySdt(sql);
            this.as_type.DataSource = dt;
            this.as_type.DataTextField = "keepothitemtype_desc";
            this.as_type.DataValueField = "keepitemtype_code";
            this.as_type.DataBind();
        }
        public void SaveWebSheet()
        {
            try
            {
                if (as_month.SelectedValue == "00")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกเดือนที่ทำรายการ"); return;
                }
                if (as_type.SelectedValue == "00")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาประเภทรายการ"); return;
                }

                if (txtInput.HasFile)
                {
                    if (System.IO.Path.GetExtension(txtInput.FileName) == ".xlsx" )
                    {

                        string sql = "";
                        string error = "";
                        string ls_coopid = state.SsCoopId;
                        string ls_keeptype = as_type.SelectedValue;
                        ExecuteDataSource exe = new ExecuteDataSource(this);
                        string into = Server.MapPath("~/WSRPDF/") + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + txtInput.FileName;
                        txtInput.PostedFile.SaveAs(into);
                        FileInfo excel = new FileInfo(into);
                        using (var package = new ExcelPackage(excel))
                        {
                            var workbook = package.Workbook;
                            var worksheet = workbook.Worksheets.First();
                            decimal ld_amount = 0;
                            string ls_memno = "";
                            string ls_period = as_year.Text.Trim() + as_month.SelectedValue;

                            sql = "DELETE FROM KPRCVKEEPOTHER  where COOP_ID ='" + ls_coopid + "' and  KEEPITEMTYPE_CODE = '" + ls_keeptype + "' ";
                            WebUtil.ExeSQL(sql);

                            for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                            {
                                try
                                {
                                    string ls_keetypedesc = "", ls_keepothercode = "";
                                    int ln_seq = 0;
                                   
                                    ls_memno = worksheet.Cells[i, 1].Text.Trim();
                                    ls_memno = WebUtil.MemberNoFormat(ls_memno);
                                    ld_amount = Convert.ToDecimal(worksheet.Cells[i, 2].Text.Trim());

                                    sql = "select isnull(max(seq_no),0)+1  as seq_no from KPRCVKEEPOTHER where coop_id = '" + ls_coopid + "' and  MEMBER_NO = '" + ls_memno + "' ";
                                    Sdt dt = WebUtil.QuerySdt(sql);
                                    if (dt.Next())
                                    { ln_seq = dt.GetInt32("seq_no"); }

                                    sql = "select keepothitemtype_code,keepothitemtype_desc,keepitemtype_code from KPUCFKEEPOTHITEMTYPE where coop_id = '" + state.SsCoopControl + "' and  keepitemtype_code = '" + ls_keeptype + "' ";
                                    dt = WebUtil.QuerySdt(sql);
                                    if (dt.Next())
                                    { 
                                        ls_keetypedesc = dt.GetString("keepothitemtype_desc");
                                        ls_keepothercode = dt.GetString("keepothitemtype_code");
                                    }
                                    //if (as_type.SelectedValue == "CSL")
                                    //{
                                    //    ls_desc = "เงินสงเคราะห์ สปอ.";
                                    //     li_seq = 1;
                                    //     ls_keepothercode = "05";
                                    //}
                                    //else if (as_type.SelectedValue == "CMS")
                                    //{
                                    //    ls_desc = "เงินสงเคราะห์ สส.อส.";
                                    //    li_seq = 2;
                                    //    ls_keepothercode = "06";
                                    //}
                                    //else if (as_type.SelectedValue == "CSS")
                                    //{
                                    //    ls_desc = "เงินสงเคราะห์ สส.ชสน.";
                                    //    li_seq = 3;
                                    //    ls_keepothercode = "07";
                                    //}
                                   
                                    sql = "INSERT INTO KPRCVKEEPOTHER ( COOP_ID ,MEMBER_NO ,	SEQ_NO,	MEMCOOP_ID,	KEEPITEMTYPE_CODE,	KEEPOTHITEMTYPE_CODE,	KEEPOTHER_TYPE,";
                                    sql += "STARTKEEP_PERIOD,	LASTKEEP_PERIOD,	DESCRIPTION,	ITEM_PAYMENT,	ENTRY_ID) values ";
                                    sql += "('" + ls_coopid + "','" + ls_memno + "','" + ln_seq + "','" + ls_coopid + "','" + ls_keeptype + "','" + ls_keepothercode + "','1',";
                                    sql += "'" + ls_period + "','" + ls_period + "','" + ls_keetypedesc + "'," + ld_amount + ",'" + state.SsUsername + "' )";
                                    WebUtil.Query(sql);
                                }
                                catch
                                {
                                    error += ls_memno + " ";
                                }
                            }
                        }
                        if (error.Trim() == "")
                        {
                            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จสิ้น");
                        }
                        else
                        {
                            sql = "DELETE FROM KPRCVKEEPOTHER  where COOP_ID ='" + state.SsCoopId + "' and  KEEPITEMTYPE_CODE = '" + as_type.SelectedValue + "'";
                            WebUtil.ExeSQL(sql);
                            LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกข้อมูลไม่สำเร็จ เนื่องจากมีข้อมูลเลขสมาชิกซ้ำ ได้แก่ " + error);
                        }
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ต้องเป็น ไฟล์ .xlsx เท่านั้น");
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกไฟล์ข้อมูล");
                }
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกข้อมูลไม่สำเร็จ ไม่สำเร็จ");
            }
        }

        public void WebSheetLoadEnd()
        {


        }
    }
}
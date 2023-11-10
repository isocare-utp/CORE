using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_export_mrtgmast_ctrl
{
    public partial class ws_sl_mg_export_mrtgmast : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostRetrieve { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdAssettype();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostRetrieve)
            {
                string member_no, assettype_code;
                int mortgage_type = 0;
                string ls_sql, sqlext = "";

                member_no = dsMain.DATA[0].member_no.Trim();
                assettype_code = dsMain.DATA[0].assettype_code.Trim();
                mortgage_type = dsMain.DATA[0].mortgage_type;

                if (member_no != "")
                {
                    sqlext += " and mb.member_no = '" + WebUtil.MemberNoFormat(member_no) + "'";
                }
                if (assettype_code != "")
                {
                    sqlext += " and mgm.assettype_code = '" + assettype_code + "'";
                }
                if (mortgage_type != -1)
                {
                    sqlext += " and mgm.mortgage_type = '" + mortgage_type + "'";
                }

                ls_sql = @"select mgm.member_no,
                    mup.prename_desc || mb.memb_name || ' ' || mb.memb_surname as full_name,
                    mgm.mrtgmast_no,
                    mgm.assettype_code || ' - ' || lua.assettype_desc as assettype_desc,
                    case mgm.mortgage_type when 0 then 'แปลงเดียว' when 1 then 'เฉพาะส่วน' when 2 then 'หลายแปลง' else '' end as cp_mrtgtype
                    from lnmrtgmaster mgm, 
                    mbmembmaster mb,
                    mbucfprename mup,
                    lnucfassettype lua
                    where mgm.coop_id = mb.coop_id
                    and mgm.member_no = mb.member_no
                    and mb.prename_code = mup.prename_code
                    and mgm.assettype_code = lua.assettype_code
                    and mgm.coop_id = '" + state.SsCoopControl + "'" + sqlext + @"
                    order by mgm.mrtgmast_no";

                Sdt dt = WebUtil.QuerySdt(ls_sql);

                dsList.ImportData(dt);
            }
        }

        protected void PostExportExcel(object sender, EventArgs e)
        {
            string mrtgmast_no = "";

            for (int i = 0; i < dsList.RowCount; i++)
            {
                if (dsList.DATA[i].operate_flag == 1)
                {
                    if (mrtgmast_no != "")
                    {
                        mrtgmast_no += ",'" + dsList.DATA[i].mrtgmast_no + "'";
                    }
                    else
                    {
                        mrtgmast_no = "'" + dsList.DATA[i].mrtgmast_no + "'";
                    }
                }
            }

            string queryStr = @"select land_ravang, land_landno, land_survey, land_docno, land_bookno, land_pageno, pos_moo, 
                photoair_province, photoair_number, photoair_page, pos_tambol, pos_amphur, pos_province, size_rai, size_ngan, size_wa,
                mrtg_name1, replace( to_char( mrtg_personid1 , '9,9999,99999,99,9' ) , ',' , '-' ) , mrtg_name2, mrtg_personid2, mrtg_age1, mrtg_nationality1, mrtg_citizenship1, mrtg_parentname1, mrtg_matename1,
                mrtg_village1, mrtg_address1, mrtg_soi1, mrtg_road1, mrtg_moo1, mrtg_tambol1, mrtg_amphur1, mrtg_province1,
                mortgage_grtname, autzd_name, autzd_age, autzd_nationality, autzd_citizenship, autzd_parentname, 
                autzd_village, autzd_address, autzd_moo, autzd_tambol, autzd_amphur, autzd_province,
                mortgagesum_amt, ft_readtbaht(mortgagesum_amt), case mortgage_type when 0 then 1 when 2 then mortgage_landnum else null end,
                case assettype_code when '03' then 'ก.' end, land_office, ftcnvtdate(mortgage_date, 3)
                from lnmrtgmaster 
                where coop_id = '" + state.SsCoopControl + @"'
                and mrtgmast_no in (" + mrtgmast_no + ")";

            Sdt dt = WebUtil.QuerySdt(queryStr);

            ExportTableData(dt);
        }

        public void ExportTableData(DataTable dtdata)
        {
            string attach = "attachment;filename=mrtgmaster.xls";
            Response.Charset = "windows-874";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding(874);
            Response.ClearContent();
            Response.AddHeader("content-disposition", attach);
            Response.ContentType = "application/ms-excel";
            if (dtdata != null)
            {
                //foreach (DataColumn dc in dtdata.Columns)
                //{
                //    Response.Write(dc.ColumnName + "\t");
                //    //sep = ";";
                //}
                Response.Write("ระวาง" + "\t");
                Response.Write("เลขที่ดิน" + "\t");
                Response.Write("หน้าสำรวจ" + "\t");
                Response.Write("เลขที่โฉนดหรือนส3ก" + "\t");
                Response.Write("ทะเบียนเล่ม" + "\t");
                Response.Write("หน้า" + "\t");
                Response.Write("หมู่ที่" + "\t");
                Response.Write("ระวางรูปถ่ายทางอากาศ" + "\t");
                Response.Write("หมายเลข" + "\t");
                Response.Write("แผ่นที่" + "\t");
                Response.Write("ตำบล" + "\t");
                Response.Write("อำเภอ" + "\t");
                Response.Write("จังหวัด" + "\t");
                Response.Write("ไร่" + "\t");
                Response.Write("งาน" + "\t");
                Response.Write("ตารางวา" + "\t");
                Response.Write("ผู้จำนอง1" + "\t");
                Response.Write("เลขประชาชน1" + "\t");
                Response.Write("ผู้จำนอง2" + "\t");
                Response.Write("เลขประชาชน2" + "\t");
                Response.Write("อายุ" + "\t");
                Response.Write("เชื้อชาติ" + "\t");
                Response.Write("สัญชาติ" + "\t");
                Response.Write("บิดามารดาชื่อ" + "\t");
                Response.Write("คู่สมรสชื่อ" + "\t");
                Response.Write("อยู่ที่บ้านหมู่บ้าน" + "\t");
                Response.Write("เลขที่บ้านผู้จำนอง" + "\t");
                Response.Write("ตรอกซอย" + "\t");
                Response.Write("ถนน" + "\t");
                Response.Write("หมู่" + "\t");
                Response.Write("ตำบลหรือแขวง" + "\t");
                Response.Write("อำเภอหรือเขต" + "\t");
                Response.Write("จว" + "\t");
                Response.Write("ทุกประเภทของ" + "\t");
                Response.Write("โดย" + "\t");
                Response.Write("อายุผจก" + "\t");
                Response.Write("เชื้อชาติผจก" + "\t");
                Response.Write("สัญชาติผจก" + "\t");
                Response.Write("บุตร" + "\t");
                Response.Write("อยู่ที่บ้าน" + "\t");
                Response.Write("เลขที่" + "\t");
                Response.Write("ม" + "\t");
                Response.Write("ต" + "\t");
                Response.Write("อ" + "\t");
                Response.Write("จ" + "\t");
                Response.Write("ในวงเงิน" + "\t");
                Response.Write("ตัวอักษร" + "\t");
                Response.Write("ที่ดินจำนวน" + "\t");
                Response.Write("นส3" + "\t");
                Response.Write("สำนักงานที่ดิน" + "\t");
                Response.Write("วัน" + "\t");

                Response.Write("เชื้อชาติผู้จำนอง" + "\t");
                Response.Write("เจ้าพนักงานที่ดิน" + "\t");
                Response.Write("เงินจำนองเดิม" + "\t");
                Response.Write("เงินจำนองรวม" + "\t");
                Response.Write("จำนวนรวมตัวอักษร" + "\t");
                Response.Write("เงินจำนองครั้งที่" + "\t");
                Response.Write("อัตราดอกเบี้ย" + "\t");

                Response.Write("เงินจำนองครั้งที่หนึ่ง" + "\t");
                Response.Write("เงินจำนองครั้งที่สอง" + "\t");
                Response.Write("เงินจำนองครั้งที่สาม" + "\t");
                Response.Write("เงินจำนองครั้งที่สี่" + "\t");

                Response.Write("บันทึกทำขึ้น" + "\t");

                //Response.Write("วันที่" + "\t");
                //Response.Write("เดือน" + "\t");
                //Response.Write("พศ" + "\t");

                Response.Write(System.Environment.NewLine);
                foreach (DataRow dr in dtdata.Rows)
                {
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {
                        Response.Write(dr[i].ToString() + "\t");
                    }
                    Response.Write(System.Environment.NewLine);
                }
                //if (ibl_upgrade == true)
                //{
                //    of_setUpgAmt();
                //    Response.Write("สาม" + "\t");
                //}
                Response.Write("\n");

                Response.End();
                WebSheetLoadEnd();
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
    }
}
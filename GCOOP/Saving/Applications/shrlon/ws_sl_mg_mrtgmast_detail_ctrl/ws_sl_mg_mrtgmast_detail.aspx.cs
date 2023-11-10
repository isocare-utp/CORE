using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Xsl;
using DataLibrary;
using System.Text;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl
{
    public partial class ws_sl_mg_mrtgmast_detail : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostMrtgNo { get; set; }
        //[JsPostBack]
        //public string PostPrint { get; set; }
        //[JsPostBack]
        //public string PostPrintUpg { get; set; }

        DataTable dt = new DataTable();
        Boolean ibl_upgrade = false;

        public void InitJsPostBack()
        {
            dsAutz.InitDsAutz(this);
            dsDetail.InitDsDetail(this);
            dsDetailBding.InitDsDetailBding(this);
            dsDetailCondo.InitDsDetailCondo(this);
            dsDetailDeed.InitDsDetailDeed(this);
            dsDetailNs3.InitDsDetailNs3(this);
            dsMain.InitDsMain(this);
            dsMrtger.InitDsMrtger(this);
            dsRefcollno.InitDsRefcollno(this);
            dsList.InitDsList(this);
            dsUpgrade.InitDsUpgrade(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsDetail.DdAssettype();
                dsDetailDeed.Visible = true;
                dsDetailBding.Visible = false;
                dsDetailNs3.Visible = false;
                dsDetailCondo.Visible = false;
                this.SetOnLoadedScript(" parent.Setfocus();");
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                dsDetail.ResetRow();
                dsDetailBding.ResetRow();
                dsDetailCondo.ResetRow();
                dsDetailDeed.ResetRow();
                dsDetailNs3.ResetRow();
                dsAutz.ResetRow();
                dsMrtger.ResetRow();
                dsRefcollno.ResetRow();
                dsList.ResetRow();

                string as_memno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO); ;
                dsMain.DATA[0].MEMBER_NO = as_memno;
                dsMain.MemberNoRetrieve();
                dsList.Retrieve(as_memno);
            }
            else if (eventArg == PostMrtgNo)
            {
                int row = dsList.GetRowFocus();
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    dsList.FindTextBox(i, "mrtgmast_no").BackColor = Color.White;
                    dsList.FindTextBox(i, "assettype_desc").BackColor = Color.White;
                    dsList.FindTextBox(i, "cp_mrtgtype").BackColor = Color.White;
                    dsList.FindTextBox(i, "mrtgmast_status").BackColor = Color.White;
                }
                dsList.FindTextBox(row, "mrtgmast_no").BackColor = Color.SkyBlue;
                dsList.FindTextBox(row, "assettype_desc").BackColor = Color.SkyBlue;
                dsList.FindTextBox(row, "cp_mrtgtype").BackColor = Color.SkyBlue;
                dsList.FindTextBox(row, "mrtgmast_status").BackColor = Color.SkyBlue;

                string ls_mrtgno = dsList.DATA[row].mrtgmast_no; ;
                dsDetail.Retrieve(ls_mrtgno);
                dsDetailBding.Retrieve(ls_mrtgno);
                dsDetailCondo.Retrieve(ls_mrtgno);
                dsDetailDeed.Retrieve(ls_mrtgno);
                dsDetailNs3.Retrieve(ls_mrtgno);
                dsAutz.Retrieve(ls_mrtgno);
                dsMrtger.Retrieve(ls_mrtgno);
                dsRefcollno.Retrieve(ls_mrtgno);
                dsDetail.DdAssettype();
                dsUpgrade.Retrieve(ls_mrtgno);
            }
            else if (eventArg == "PostPrint_")
            {
                string queryStr = @"select mortgage_date ,mortgage_grtname, mortgagesum_amt, 'ฟังก์ชั่นตัวเลขภาษาไทย' as mortgagesum_amt ,land_office,size_rai,size_ngan,size_wa,
		pos_moo,pos_tambol,pos_amphur,pos_province,
		land_docno,land_ravang,land_survey,land_landno,land_bookno,land_pageno,
		ns3_docno,ns3_bookno,ns3_pageno,ns3_landno,photoair_province,photoair_number,photoair_page,
		condo_regisno,condo_name,condo_towerno,condo_floor,condo_roomno,condo_roomsize,
		mrtg_name1,replace( mrtg_personid1 , ',' , '-' ),mrtg_age1,mrtg_nationality1,mrtg_citizenship1,mrtg_parentname1,mrtg_matename1,
		mrtg_village1,mrtg_address1,mrtg_moo1,mrtg_soi1,mrtg_road1,mrtg_tambol1,mrtg_amphur1,mrtg_province1
		mrtg_name2,replace( mrtg_personid2 , ',' , '-'  ),mrtg_age2,mrtg_nationality2,mrtg_citizenship2,mrtg_parentname2,mrtg_matename2,
		mrtg_village2,mrtg_address2,mrtg_moo2,mrtg_soi2,mrtg_road2,mrtg_tambol2,mrtg_amphur2,mrtg_province2
		autrz_name1,autrz_pos1,autrz_name2,autrz_pos2,
		autzd_name,autzd_age,autzd_nationality,autzd_citizenship,autzd_parentname,autzd_village,autzd_address,autzd_moo,autzd_soi,autzd_road,
		autzd_tambol, autzd_amphur, autzd_province,autrz_date,
				case mortgage_type when 0 then 1 when 2 then mortgage_landnum else null end,
				case assettype_code when '03' then 'ก.' end, land_office, 'ฟังก์ชั่นเเปลงวันที่เป็นภาษาไทย' as mortgage_date
                from lnmrtgmaster 
                where coop_id = {0}
                and mrtgmast_no = {1} ";

                queryStr = WebUtil.SQLFormat(queryStr, state.SsCoopControl, dsDetail.DATA[0].MRTGMAST_NO);
                Sdt dt = WebUtil.QuerySdt(queryStr);

                ExportTableData(dt);
            }
            else if (eventArg == "PostPrintUpg_")
            {
                ibl_upgrade = true;
                string queryStr = @"select lmg.land_ravang, lmg.land_landno, lmg.land_survey, lmg.land_docno, lmg.land_bookno, lmg.land_pageno, lmg.pos_moo, 
                lmg.photoair_province, lmg.photoair_number, lmg.photoair_page, lmg.pos_tambol, lmg.pos_amphur, lmg.pos_province, lmg.size_rai, lmg.size_ngan, lmg.size_wa,
                lmg.mrtg_name1, replace( lmg.mrtg_personid1, ',' , '-'  ) , lmg.mrtg_name2, lmg.mrtg_personid2, lmg.mrtg_age1, lmg.mrtg_nationality1, lmg.mrtg_citizenship1, lmg.mrtg_parentname1, lmg.mrtg_matename1,
                lmg.mrtg_village1, lmg.mrtg_address1, lmg.mrtg_soi1, lmg.mrtg_road1, lmg.mrtg_moo1, lmg.mrtg_tambol1, lmg.mrtg_amphur1, lmg.mrtg_province1,
                lmg.mortgage_grtname, lmg.autzd_name, lmg.autzd_age, lmg.autzd_nationality, lmg.autzd_citizenship, lmg.autzd_parentname, 
                lmg.autzd_village, lmg.autzd_address, lmg.autzd_moo, lmg.autzd_tambol, lmg.autzd_amphur, lmg.autzd_province,
                lmg.mortgagesum_amt, '' as mortgagesum_amt , case lmg.mortgage_type when 0 then 1 when 2 then lmg.mortgage_landnum else null end,
                case lmg.assettype_code when '03' then 'ก.' end, lmg.land_office, ''  as mortgage_date , lmg.autzd_nationality, lmg.land_office,
                 lmg.mortgagefirst_amt, lmg.mortgagesum_amt, '' as mortgagesum_amt , '' as upgrade_no , lug.upgrade_intrate
                from lnmrtgmaster lmg ,
			( select lug.coop_id , lug.mrtgmast_no , max( lug.upgrade_no ) as upgrade_no from lnmrtgmastupgrade lug group by lug.coop_id , lug.mrtgmast_no  ) lmug ,
                lnmrtgmastupgrade lug
                where lmg.coop_id = lug.coop_id
                and lmg.mrtgmast_no = lug.mrtgmast_no
                and lug.coop_id = lmug.coop_id
                and lug.mrtgmast_no = lmug.mrtgmast_no
                and lug.upgrade_no = lmug.upgrade_no
                and lmg.coop_id = {0}
                and lmg.mrtgmast_no = {1}";


                queryStr = WebUtil.SQLFormat(queryStr, state.SsCoopControl, dsDetail.DATA[0].MRTGMAST_NO);
                Sdt dt = WebUtil.QuerySdt(queryStr);

                ExportTableData(dt);
            }
            this.SetOnLoadedScript(" parent.Setfocus();");
        }

        protected void PostPrintUpg(object sender, EventArgs e)
        {
            ibl_upgrade = true;
            string queryStr = @"select lmg.land_ravang, lmg.land_landno, lmg.land_survey, lmg.land_docno, lmg.land_bookno, lmg.land_pageno, lmg.pos_moo, 
                lmg.photoair_province, lmg.photoair_number, lmg.photoair_page, lmg.pos_tambol, lmg.pos_amphur, lmg.pos_province, lmg.size_rai, lmg.size_ngan, lmg.size_wa,
                lmg.mrtg_name1, replace( lmg.mrtg_personid1, ',' , '-' ) , lmg.mrtg_name2, lmg.mrtg_personid2, lmg.mrtg_age1, lmg.mrtg_nationality1, lmg.mrtg_citizenship1, lmg.mrtg_parentname1, lmg.mrtg_matename1,
                lmg.mrtg_village1, lmg.mrtg_address1, lmg.mrtg_soi1, lmg.mrtg_road1, lmg.mrtg_moo1, lmg.mrtg_tambol1, lmg.mrtg_amphur1, lmg.mrtg_province1,
                lmg.mortgage_grtname, lmg.autzd_name, lmg.autzd_age, lmg.autzd_nationality, lmg.autzd_citizenship, lmg.autzd_parentname, 
                lmg.autzd_village, lmg.autzd_address, lmg.autzd_moo, lmg.autzd_tambol, lmg.autzd_amphur, lmg.autzd_province,
                lmg.mortgagesum_amt, '' as mortgagesum_amt , case lmg.mortgage_type when 0 then 1 when 2 then lmg.mortgage_landnum else null end,
                case lmg.assettype_code when '03' then 'ก.' end, lmg.land_office, '' as mortgage_date , lmg.autzd_nationality, lmg.land_office,
                 lmg.mortgagefirst_amt, lmg.mortgagesum_amt, '' as mortgagesum_amt , '' as upgrade_no , lug.upgrade_intrate
                from lnmrtgmaster lmg ,
			( select lug.coop_id , lug.mrtgmast_no , max( lug.upgrade_no ) as upgrade_no from lnmrtgmastupgrade lug group by lug.coop_id , lug.mrtgmast_no  ) lmug ,
                lnmrtgmastupgrade lug
                where lmg.coop_id = lug.coop_id
                and lmg.mrtgmast_no = lug.mrtgmast_no
                and lug.coop_id = lmug.coop_id
                and lug.mrtgmast_no = lmug.mrtgmast_no
                and lug.upgrade_no = lmug.upgrade_no
                and lmg.coop_id = {0}
                and lmg.mrtgmast_no = {1}";


            queryStr = WebUtil.SQLFormat(queryStr, state.SsCoopControl, dsDetail.DATA[0].MRTGMAST_NO);
            Sdt dt = WebUtil.QuerySdt(queryStr);

            ExportTableData(dt);
        }

        protected void PostPrint(object sender, EventArgs e)
        {
            string queryStr = @"select CONVERT(char, mortgage_date ,103) as mortgage_date,
		   Convert(char , redeem_date,103) as redeem_date ,
mortgage_grtname, 
(lnreqloan.loanrequest_amt ) as loanrequest_amt, 
'' as loanrequest_amt ,land_office,size_rai,size_ngan,size_wa,
		pos_moo,pos_tambol,pos_amphur,pos_province,
		land_docno,land_ravang,land_survey,land_landno,land_bookno,land_pageno,
		ns3_docno,ns3_bookno,ns3_pageno,ns3_landno,photoair_province,photoair_number,photoair_page,
		condo_regisno,condo_name,condo_towerno,condo_floor,condo_roomno,condo_roomsize,
		mrtg_name1,mrtg_personid1,mrtg_age1,mrtg_nationality1,mrtg_citizenship1,mrtg_parentname1,mrtg_matename1,
		mrtg_village1,mrtg_address1,mrtg_moo1,mrtg_soi1,mrtg_road1,mrtg_tambol1,mrtg_amphur1,mrtg_province1,
		mrtg_name2,mrtg_personid2,mrtg_age2,mrtg_nationality2,mrtg_citizenship2,mrtg_parentname2,mrtg_matename2,
		mrtg_village2,mrtg_address2,mrtg_moo2,mrtg_soi2,mrtg_road2,mrtg_tambol2,mrtg_amphur2,mrtg_province2,
		autrz_name1,autrz_pos1,autrz_name2,autrz_pos2,
		autzd_name,autzd_age,autzd_nationality,autzd_citizenship,autzd_parentname,autzd_village,autzd_address,autzd_moo,autzd_soi,autzd_road,
		autzd_tambol, autzd_amphur, autzd_province,autrz_date,lnucfloanobjective.loanobjective_desc,
		SUBSTRING (CONVERT(VARCHAR(8), mortgage_date, 112), 7, 2) as day,
       SUBSTRING (CONVERT(VARCHAR(8), mortgage_date, 112), 5, 2) as month,
		SUBSTRING (CONVERT(VARCHAR(8), mortgage_date, 112), 5, 2) as year,
				case mortgage_type when 0 then 1 when 2 then mortgage_landnum else null end,
				case assettype_code when '03' then 'ก.' end
                from lnmrtgmaster,lnreqloan,lnucfloanobjective
                where 
                lnmrtgmaster.coop_id = lnreqloan.coop_id and 
                lnmrtgmaster.member_no = lnreqloan.member_no and 
                lnreqloan.loanobjective_code = lnucfloanobjective.loanobjective_code and
                lnmrtgmaster.coop_id = {0} and
                lnmrtgmaster.mrtgmast_no = {1}";


            queryStr = WebUtil.SQLFormat(queryStr, state.SsCoopControl, dsDetail.DATA[0].MRTGMAST_NO);
            Sdt dt = WebUtil.QuerySdt(queryStr);

            ExportTableData(dt);
        }

        public void ExportTableData(DataTable dtdata)
        {
            Encoding encoding = Encoding.UTF8;
            string attach = "attachment;filename=mrtgmaster.csv";
            //Response.ContentEncoding = Encoding.Unicode;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Response.Charset = "windows-874";
            //Response.ContentEncoding = System.Text.Encoding.GetEncoding(874);
            Response.AddHeader("content-disposition", attach);
            Response.ClearContent();
            Response.ContentType = "text/csv";

 
            if (dtdata != null)
            {
                //foreach (DataColumn dc in dtdata.Columns)
                //{
                //    Response.Write(dc.ColumnName + "\t");
                //    //sep = ";";
                //}
                Response.Write("วันที่จำนอง" + "\t");
                Response.Write("วันที่ไถ่ถอนจำนอง" + "\t");
                Response.Write("เป็นประกันเงินกู้ของ" + "\t");
                Response.Write("ในวงเงิน" + "\t");
                Response.Write("จำนวนเงินตัวอักษร" + "\t");
		Response.Write("สำนักงานที่ดิน" + "\t");
		Response.Write("ไร่" + "\t");
                Response.Write("งาน" + "\t");
                Response.Write("ตารางวา" + "\t");
		Response.Write("หมู่ที่" + "\t");
		Response.Write("ตำบล" + "\t");
                Response.Write("อำเภอ" + "\t");
                Response.Write("จังหวัด" + "\t");
                Response.Write("เลขที่โฉนด" + "\t");
		Response.Write("ระวาง" + "\t");
                Response.Write("หน้าสำรวจ" + "\t");
                Response.Write("เลขที่ดิน" + "\t");
                Response.Write("เล่มที่โฉนด" + "\t");
                Response.Write("หน้าโฉนด" + "\t");
               	Response.Write("เลขที่นส3ก" + "\t");
		Response.Write("เล่มนส3" + "\t");
		Response.Write("หน้านส3" + "\t");
		Response.Write("เลขที่ดินนส3" + "\t");
                Response.Write("ระวางรูปถ่ายทางอากาศ" + "\t");
                Response.Write("หมายเลข" + "\t");
                Response.Write("แผ่นที่" + "\t");
                Response.Write("ทะเบียนอาคารชุด" + "\t");
		Response.Write("ชื่ออาคารชุด" + "\t");
		Response.Write("อาคารเลขที่" + "\t");
                Response.Write("ชั้นที่" + "\t");
		Response.Write("ห้องชุดเลขที่" + "\t");
		Response.Write("เนื้อที่ห้องชุด" + "\t");
                Response.Write("ผู้จำนอง1" + "\t");
                Response.Write("เลขประชาชน1" + "\t");
		Response.Write("อายุผู้จำนอง1" + "\t");
		Response.Write("สัญชาติผู้จำนอง1" + "\t");
		Response.Write("เชื้อชาติผู้จำนอง1" + "\t");
		Response.Write("บิดามารดาผู้จำนอง1" + "\t");
		Response.Write("คู่สมรสผู้จำนอง1" + "\t");
		Response.Write("หมู่บ้านผู้จำนอง1" + "\t");
		Response.Write("เลขที่บ้านผู้จำนอง1" + "\t");
        Response.Write("หมู่ที่ผู้จำนอง1" + "\t");
                Response.Write("ตรอกซอยผู้จำนอง1" + "\t");
                Response.Write("ถนนผู้จำนอง1" + "\t");
                Response.Write("ตำบลหรือแขวงผู้จำนอง1" + "\t");
                Response.Write("อำเภอหรือเขตผู้จำนอง1" + "\t");
                Response.Write("จังหวัดผู้จำนอง1" + "\t");
		Response.Write("ผู้จำนอง2" + "\t");
                Response.Write("เลขประชาชน2" + "\t");
		Response.Write("อายุผู้จำนอง2" + "\t");
		Response.Write("สัญชาติผู้จำนอง2" + "\t");
		Response.Write("เชื้อชาติผู้จำนอง2" + "\t");
		Response.Write("บิดามารดาผู้จำนอง2" + "\t");
		Response.Write("คู่สมรสผู้จำนอง2" + "\t");
		Response.Write("หมู่บ้านผู้จำนอง2" + "\t");
		Response.Write("เลขที่บ้านผู้จำนอง2" + "\t");
        Response.Write("หมู่ที่ผู้จำนอง2" + "\t");
                Response.Write("ตรอกซอยผู้จำนอง2" + "\t");
                Response.Write("ถนนผู้จำนอง2" + "\t");     
                Response.Write("ตำบลหรือแขวงผู้จำนอง2" + "\t");
                Response.Write("อำเภอหรือเขตผู้จำนอง2" + "\t");
                Response.Write("จังหวัดผู้จำนอง2" + "\t");
                Response.Write("ผู้มอบอำนาจ1" + "\t");
                Response.Write("ตำแหน่งผู้มอบอำนาจ1" + "\t");
                Response.Write("ผู้มอบอำนาจ2" + "\t");
                Response.Write("ตำแหน่งผู้มอบอำนาจ2" + "\t");
                Response.Write("ผู้รับมอบอำนาจ" + "\t");
                Response.Write("อายุผู้รับมอบอำนาจ" + "\t");
                Response.Write("สัญชาติผู้รับมอบอำนาจ" + "\t");
                Response.Write("เชื้อชาติผู้รับมอบอำนาจ"+"\t");
                Response.Write("บิดามารดาผู้รับมอบอำนาจ" + "\t");
		Response.Write("หมู่บ้านผู้รับมอบอำนาจ" + "\t");
		Response.Write("เลขที่บ้านผู้รับมอบอำนาจ" + "\t");
        Response.Write("หมู่ที่ผู้รับมอบอำนาจ" + "\t");
                Response.Write("ตรอกซอยผู้รับมอบอำนาจ" + "\t");
                Response.Write("ถนนผู้รับมอบอำนาจ" + "\t");          
                Response.Write("ตำบลหรือแขวงผู้รับมอบอำนาจ" + "\t");
                Response.Write("อำเภอหรือเขตผู้รับมอบอำนาจ" + "\t");
                Response.Write("จังหวัดผู้รับมอบอำนาจ" + "\t");
                Response.Write("วันที่รับมอบอำนาจ" + "\t");
                Response.Write("วัตถุประสงค์" + "\t");
                Response.Write("วัน" + "\t");
                Response.Write("เดือน" + "\t");
                Response.Write("ปี" + "\t");
                

                Response.Write(System.Environment.NewLine);
                foreach (DataRow dr in dtdata.Rows)
                {
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {
                        Response.Write(dr[i].ToString() + "\t");
                    }                    
                }
                if (ibl_upgrade == true)
                {
                    of_setUpgAmt();
                    Response.Write("สาม" + "\t");
                }
                Response.Write("\n");

                Response.End();
                WebSheetLoadEnd();
            }
        }

        public void of_setUpgAmt()
        {
            string upg_queryStr = @"select upgrade_addamt
                from lnmrtgmastupgrade
                where coop_id = {0}
                and mrtgmast_no = {1}";

            upg_queryStr = WebUtil.SQLFormat(upg_queryStr, state.SsCoopControl, dsDetail.DATA[0].MRTGMAST_NO);
            Sdt dt = WebUtil.QuerySdt(upg_queryStr);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (i <= j)
                    {
                        if(dt.Next())
                        Response.Write(dt.GetDecimal("upgrade_addamt") + "\t");
                    }
                    else
                    {
                        Response.Write("" + "\t");
                    }
                }
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            string assettype_code = dsDetail.DATA[0].ASSETTYPE_CODE;
            if (assettype_code == "01")
            {
                dsDetailDeed.Visible = true;
                dsDetailBding.Visible = false;
                dsDetailNs3.Visible = false;
                dsDetailCondo.Visible = false;
            }
            else if (assettype_code == "02" || assettype_code == "03" || assettype_code == "04")
            {
                dsDetailNs3.Visible = true;
                dsDetailBding.Visible = false;
                dsDetailDeed.Visible = false;
                dsDetailCondo.Visible = false;
            }
            else if (assettype_code == "05")
            {
                dsDetailBding.Visible = false;
                dsDetailDeed.Visible = false;
                dsDetailNs3.Visible = false;
                dsDetailCondo.Visible = true;
            }
            else if (assettype_code == "06")
            {
                dsDetailNs3.Visible = false;
                //dsDetailBding.ResetRow();
                dsDetailBding.Visible = true;
                dsDetailDeed.Visible = false;
                dsDetailCondo.Visible = false;
            }
        }
    }
}
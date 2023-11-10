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

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl
{
    public partial class ws_sl_mg_mrtgmast_edit : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostAssettype { get; set; }
        [JsPostBack]
        public String PostMrtgmastNo { get; set; }
        [JsPostBack]
        public String PostMateMemno { get; set; }
        [JsPostBack]
        public String PostInsertRowRefcollno { get; set; }
        [JsPostBack]
        public String PostDelRowRefcollno { get; set; }
        [JsPostBack]
        public string PostTemplateNo { get; set; }
        [JsPostBack]
        public string PostCollmastNo { get; set; }
        [JsPostBack]
        public string PostMrtgNo { get; set; }
        string ls_collmastno = "";
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsAutz.InitDsAutz(this);
            dsDetail.InitDsDetail(this);
            dsDetailBding.InitDsDetailBding(this);
            dsDetailCondo.InitDsDetailCondo(this);
            dsDetailDeed.InitDsDetailDeed(this);
            dsDetailNs3.InitDsDetailNs3(this);
            dsMrtger.InitDsMrtger(this);
            dsList.InitDsList(this);
            dsRefcollno.InitDsRefcollno(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsDetail.DdAssettype();
                dsDetail.DATA[0].MORTGAGE_DATE = state.SsWorkDate;
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
                dsDetailCondo.ResetRow();
                dsDetailDeed.ResetRow();
                dsDetailNs3.ResetRow();
                dsAutz.ResetRow();
                dsMrtger.ResetRow();
                dsRefcollno.ResetRow();
                dsList.ResetRow();

                string as_memno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.DATA[0].MEMBER_NO = as_memno;
                dsMain.MemberNoRetrieve();
                dsList.Retrieve(as_memno);
                //dsDetail.DATA[0].MEMBER_NO = as_memno;
                //dsDetail.DdAssettype();
                //dsDetail.DATA[0].ASSETTYPE_CODE = "01";
            }
            else if (eventArg == PostMrtgNo)
            {
                int row = dsList.GetRowFocus();
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    dsList.FindTextBox(i, "mrtgmast_no").BackColor = Color.White;
                    dsList.FindTextBox(i, "assettype_desc").BackColor = Color.White;
                    dsList.FindTextBox(i, "cp_mrtgtype").BackColor = Color.White;
                }
                dsList.FindTextBox(row, "mrtgmast_no").BackColor = Color.SkyBlue;
                dsList.FindTextBox(row, "assettype_desc").BackColor = Color.SkyBlue;
                dsList.FindTextBox(row, "cp_mrtgtype").BackColor = Color.SkyBlue;

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
            }
            else if (eventArg == PostAssettype)
            {
            }
            else if (eventArg == "PostMrtgmastNo")
            {
                string ls_mrtgno = dsDetail.DATA[0].MRTGMAST_NO;
                dsDetail.Retrieve(ls_mrtgno);
                dsDetailBding.Retrieve(ls_mrtgno);
                dsDetailCondo.Retrieve(ls_mrtgno);
                dsDetailDeed.Retrieve(ls_mrtgno);
                dsDetailNs3.Retrieve(ls_mrtgno);
                dsAutz.Retrieve(ls_mrtgno);
                dsMrtger.Retrieve(ls_mrtgno);
                try
                {
                    string sql = @"select collmast_no from lnmrtgreflncollmast where coop_id = {0} and mrtgmast_no = {1}";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_mrtgno);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {

                    }
                }
                catch 
                { 

                }
                dsRefcollno.Retrieve(ls_mrtgno);
                dsDetail.DdAssettype();
            }
            else if (eventArg == "PostMateMemno")
            {
                string ls_matememno = WebUtil.MemberNoFormat(dsDetail.DATA[0].COLLMATE_MEMNO);
                dsDetail.DATA[0].COLLMATE_MEMNO = ls_matememno;
                string ls_sql = @"select ft_memname({0},{1}) as mate_name from mbmembmaster where coop_id = {0} and member_no = {1}";

                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_matememno);
                Sdt dt = WebUtil.QuerySdt(ls_sql);
                if (dt.Next())
                {
                    dsDetail.DATA[0].cp_matename = dt.GetString("mate_name");
                }
            }
            else if (eventArg == "PostInsertRowRefcollno")
            {
                dsRefcollno.InsertLastRow();

                int row = dsRefcollno.RowCount - 1;
                dsRefcollno.DATA[row].COOP_ID = state.SsCoopControl;
                dsRefcollno.DATA[row].MRTGMAST_NO = dsDetail.DATA[0].MRTGMAST_NO;
            }
            else if (eventArg == "PostDelRowRefcollno")
            {
                int r = dsRefcollno.GetRowFocus();
                dsRefcollno.DeleteRow(r);
            }
            else if (eventArg == "PostTemplateNo")
            {
                string ls_autrz_name1 = dsAutz.DATA[0].AUTRZ_NAME1;
                string ls_autrz_pos1 = dsAutz.DATA[0].AUTRZ_POS1;
                string ls_autrz_name2 = dsAutz.DATA[0].AUTRZ_NAME2;
                string ls_autrz_pos2 = dsAutz.DATA[0].AUTRZ_POS2;
                dsAutz.RetrieveAutzd(Convert.ToDecimal(HdTemplateNo.Value));
                dsAutz.DATA[0].AUTRZ_NAME1 = ls_autrz_name1;
                dsAutz.DATA[0].AUTRZ_POS1 = ls_autrz_pos1;
                dsAutz.DATA[0].AUTRZ_NAME2 = ls_autrz_name2;
                dsAutz.DATA[0].AUTRZ_POS2 = ls_autrz_pos2;
                dsAutz.DATA[0].MRTGMAST_NO = dsDetail.DATA[0].MRTGMAST_NO;
            }
            else if (eventArg == "PostCollmastNo")
            {
                int r = dsRefcollno.GetRowFocus();
                dsRefcollno.DATA[r].COLLMAST_NO = HdCollmastNo.Value;

                for (int i = 0; i < dsRefcollno.RowCount; i++)
                {
                    //    ls_collmastno += "," + dsRefcollno.DATA[i].COLLMAST_NO;
                    //}
                    //ls_collmastno = ls_collmastno.Substring(1); 
                    if (ls_collmastno == "")
                    {
                        ls_collmastno = "'" + dsRefcollno.DATA[i].COLLMAST_NO.Trim() + "'";
                    }
                    else
                    {
                        ls_collmastno = ls_collmastno + ",'" + dsRefcollno.DATA[i].COLLMAST_NO.Trim() + "'";
                    }
                }
                string ls_sql = @" select   LISTAGG(collmast_no,',') WITHIN GROUP (ORDER BY collmast_no) collmast_no ,   
                    LISTAGG(collmast_refno,',') WITHIN GROUP (ORDER BY collmast_refno) collmast_refno ,
                    collmasttype_code ,
                    sum(landestimate_amt) as landestimate_amt ,   
                    sum(houseestimate_amt) as houseestimate_amt ,   
                    sum(estimate_price) as estimate_price ,
                    sum(mortgage_price) as mortgage_price ,   
                    LISTAGG(mortgage_date,',') WITHIN GROUP (ORDER BY mortgage_date) mortgage_date ,     
                    LISTAGG(collmast_date,',') WITHIN GROUP (ORDER BY collmast_date) collmast_date ,                       
                    sum(collmast_price) as collmast_price ,   
                    LISTAGG(land_ravang,',') WITHIN GROUP (ORDER BY land_ravang) land_ravang ,
                    LISTAGG(land_docno,',') WITHIN GROUP (ORDER BY land_docno) land_docno , 
                    LISTAGG(land_landno,',') WITHIN GROUP (ORDER BY land_landno) land_landno ,
                    LISTAGG(land_survey,',') WITHIN GROUP (ORDER BY land_survey) land_survey , 
                    LISTAGG(land_bookno,',') WITHIN GROUP (ORDER BY land_bookno) land_bookno , 
                    LISTAGG(land_pageno,',') WITHIN GROUP (ORDER BY land_pageno) land_pageno ,
                    LISTAGG(pos_tambol,',') WITHIN GROUP (ORDER BY pos_tambol) pos_tambol ,
                    LISTAGG(pos_amphur,',') WITHIN GROUP (ORDER BY pos_amphur) pos_amphur ,
                    LISTAGG(pos_province,',') WITHIN GROUP (ORDER BY pos_province) pos_province ,
                    LISTAGG(to_char(size_rai,'9,999.99'),',') WITHIN GROUP (ORDER BY size_rai) size_rai ,   
                    LISTAGG(to_char(size_ngan,'9,999.99'),',') WITHIN GROUP (ORDER BY size_ngan) size_ngan ,  
                    LISTAGG(to_char(size_wa,'9,999.99'),',') WITHIN GROUP (ORDER BY size_wa) size_wa ,   
                    LISTAGG(photoair_province,',') WITHIN GROUP (ORDER BY photoair_province) photoair_province ,   
                    LISTAGG(photoair_number,',') WITHIN GROUP (ORDER BY photoair_number) photoair_number ,   
                    LISTAGG(photoair_page,',') WITHIN GROUP (ORDER BY photoair_page) photoair_page ,   
                    LISTAGG(priceper_unit,',') WITHIN GROUP (ORDER BY priceper_unit) priceper_unit ,   
                    (select sum(lnreqloan.loanrequest_amt) from lnreqloan,lnreqloancoll,lncollmaster
where collmast_no in (" + ls_collmastno + @") 
and lnreqloan.coop_id = lnreqloancoll.coop_id
and lnreqloan.loanrequest_docno = lnreqloancoll.loanrequest_docno
and lnreqloancoll.ref_collno = lncollmaster.collmast_no) as dol_prince ,   
                    LISTAGG(to_char(est_percent),',') WITHIN GROUP (ORDER BY est_percent) est_percent ,  
                    (select sum(lnreqloan.loanrequest_amt) from lnreqloan,lnreqloancoll,lncollmaster
where collmast_no in (" + ls_collmastno + @")  
and lnreqloan.coop_id = lnreqloancoll.coop_id
and lnreqloan.loanrequest_docno = lnreqloancoll.loanrequest_docno
and lnreqloancoll.ref_collno = lncollmaster.collmast_no) as est_price ,   
                    LISTAGG(condo_registno,',') WITHIN GROUP (ORDER BY condo_registno) condo_registno ,
                    LISTAGG(condo_name,',') WITHIN GROUP (ORDER BY condo_name) condo_name ,   
                    LISTAGG(condo_towerno,',') WITHIN GROUP (ORDER BY condo_towerno) condo_towerno ,
                    LISTAGG(condo_floor,',') WITHIN GROUP (ORDER BY condo_floor) condo_floor ,  
                    LISTAGG(condo_roomno,',') WITHIN GROUP (ORDER BY condo_roomno) condo_roomno , 
                    LISTAGG(condo_roomsize,',') WITHIN GROUP (ORDER BY condo_roomsize) condo_roomsize ,
                    LISTAGG(condo_pricesquare,',') WITHIN GROUP (ORDER BY condo_pricesquare) condo_pricesquare ,   
                    collmasttype_grp ,
                    LISTAGG(bd_village,',') WITHIN GROUP (ORDER BY bd_village) bd_village,
                    LISTAGG(bd_addrno,',') WITHIN GROUP (ORDER BY bd_addrno) bd_addrno,
                    LISTAGG(bd_addrmoo,',') WITHIN GROUP (ORDER BY bd_addrmoo) bd_addrmoo,
                    LISTAGG(bd_soi,',') WITHIN GROUP (ORDER BY bd_soi) bd_soi,
                    LISTAGG(bd_road,',') WITHIN GROUP (ORDER BY bd_road) bd_road,
                    LISTAGG(bd_tambol,',') WITHIN GROUP (ORDER BY bd_tambol) bd_tambol,
                    LISTAGG(bd_amphur,',') WITHIN GROUP (ORDER BY bd_amphur) bd_amphur,
                    LISTAGG(bd_province,',') WITHIN GROUP (ORDER BY bd_province) bd_province,
                    LISTAGG(bd_type,',') WITHIN GROUP (ORDER BY bd_type) bd_type,
                    LISTAGG(bd_depreciation,',') WITHIN GROUP (ORDER BY bd_depreciation) bd_depreciation,
                    LISTAGG(bd_onlandno,',') WITHIN GROUP (ORDER BY bd_onlandno) bd_onlandno,
                    LISTAGG(bd_age,',') WITHIN GROUP (ORDER BY bd_age) bd_age,
                    LISTAGG(condo_depreciation,',') WITHIN GROUP (ORDER BY condo_depreciation) condo_depreciation ,   
                    LISTAGG(condo_age,',') WITHIN GROUP (ORDER BY condo_age) condo_age  
                    from  lncollmaster
                    where coop_id = {0}
                    and collmast_no in (" + ls_collmastno + @") 
                    group by collmasttype_code,collmasttype_grp";

                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl);
                Sdt dt = WebUtil.QuerySdt(ls_sql);
                if (dt.Next())
                {
                    dsRefcollno.DATA[r].COLLMAST_REFNO = dt.GetString("collmast_refno");
                    if (dt.GetString("collmasttype_grp") == "01")
                    {
                        dsRefcollno.DATA[r].COLLMAST_DESC = "ตั้งอยู่ที่ตำบล " + dt.GetString("pos_tambol") + " อำเภอ " + dt.GetString("pos_amphur") + " จังหวัด " + dt.GetString("pos_province") + " จำนวน " + dt.GetString("size_rai") + " ไร่ " + dt.GetString("size_ngan") + " งาน " + dt.GetString("size_wa") + " วา";
                    }
                    else if (dt.GetString("collmasttype_grp") == "02")
                    {
                        dsRefcollno.DATA[r].COLLMAST_DESC = "หมู่บ้าน " + dt.GetString("bd_village") + " บ้านเลขที่ " + dt.GetString("bd_addrno") + " หมู่ที่ " + dt.GetString("bd_addrmoo") + " ซอย " + dt.GetString("bd_soi") + " ถนน " + dt.GetString("bd_road") + " ตำบล " + dt.GetString("bd_tambol") + " อำเภอ " + dt.GetString("bd_amphur") + " จังหวัด " + dt.GetString("bd_province");
                    }
                    else if (dt.GetString("collmasttype_grp") == "03")
                    {
                        dsRefcollno.DATA[r].COLLMAST_DESC = "ชื่อคอนโด " + dt.GetString("condo_name") + " ตึกที่ " + dt.GetString("condo_towerno") + " ชั้นที่ " + dt.GetString("condo_floor") + " ห้องที่ " + dt.GetString("condo_roomno") + " ขนาดห้อง " + dt.GetString("condo_roomsize");
                    }
                    else
                    {
                        dsRefcollno.DATA[r].COLLMAST_DESC = "ไม่มีข้อมูล";
                    }
                    decimal ss = dt.GetDecimal("est_price");
                    for (int d = 0; d < r; d++)
                    {
                        ss -= dsRefcollno.DATA[d].ESTIMATE_PRICE;
                    }
                    dsRefcollno.DATA[r].ESTIMATE_PRICE = Convert.ToDecimal(ss);//dt.GetDecimal("est_price");

                    string ls_typegroup = dt.GetString("collmasttype_grp");
                    if (ls_typegroup == "01")
                    {
                        string ls_collmasttype = dt.GetString("collmasttype_code");
                        dsDetail.DATA[0].ASSETTYPE_CODE = ls_collmasttype;
                        if (ls_collmasttype == "01")
                        {
                            dsDetailDeed.DATA[0].LAND_RAVANG = dt.GetString("land_ravang");
                            dsDetailDeed.DATA[0].LAND_DOCNO = dt.GetString("land_docno");
                            dsDetailDeed.DATA[0].LAND_LANDNO = dt.GetString("land_landno");
                            dsDetailDeed.DATA[0].LAND_BOOKNO = dt.GetString("land_bookno");
                            dsDetailDeed.DATA[0].LAND_SURVEY = dt.GetString("land_survey");
                            dsDetailDeed.DATA[0].LAND_PAGENO = dt.GetString("land_pageno");
                            dsDetailDeed.DATA[0].POS_TAMBOL = dt.GetString("pos_tambol");
                            dsDetailDeed.DATA[0].POS_AMPHUR = dt.GetString("pos_amphur");
                            dsDetailDeed.DATA[0].POS_PROVINCE = dt.GetString("pos_province");
                            dsDetailDeed.DATA[0].SIZE_RAI = dt.GetString("size_rai");
                            dsDetailDeed.DATA[0].SIZE_NGAN = dt.GetString("size_ngan");
                            dsDetailDeed.DATA[0].SIZE_WA = dt.GetString("size_wa");
                        }
                        else
                        {
                            //dsDetailNs3.DATA[0].POS_MOO = dt.GetString("pos_moo");
                            dsDetailNs3.DATA[0].NS3_DOCNO = dt.GetString("land_docno");
                            dsDetailNs3.DATA[0].POS_TAMBOL = dt.GetString("pos_tambol");
                            dsDetailNs3.DATA[0].NS3_BOOKNO = dt.GetString("land_bookno");
                            dsDetailNs3.DATA[0].POS_AMPHUR = dt.GetString("pos_amphur");
                            dsDetailNs3.DATA[0].NS3_PAGENO = dt.GetString("land_pageno");
                            dsDetailNs3.DATA[0].POS_PROVINCE = dt.GetString("pos_province");
                            dsDetailNs3.DATA[0].PHOTOAIR_PROVINCE = dt.GetString("photoair_province");
                            dsDetailNs3.DATA[0].PHOTOAIR_NUMBER = dt.GetString("photoair_number");
                            dsDetailNs3.DATA[0].PHOTOAIR_PAGE = dt.GetString("photoair_page");
                            dsDetailNs3.DATA[0].SIZE_RAI = dt.GetString("size_rai");
                            dsDetailNs3.DATA[0].SIZE_NGAN = dt.GetString("size_ngan");
                            dsDetailNs3.DATA[0].SIZE_WA = dt.GetString("size_wa");
                        }
                    }
                    else if (ls_typegroup == "03")
                    {
                        dsDetail.DATA[0].ASSETTYPE_CODE = "05";
                        //dsDetailCondo.DATA[0].LAND_DOCNO = dt.GetString("land_docno");
                        dsDetailCondo.DATA[0].CONDO_NAME = dt.GetString("condo_name");
                        dsDetailCondo.DATA[0].CONDO_TOWERNO = dt.GetString("condo_towerno");
                        dsDetailCondo.DATA[0].CONDO_ROOMNO = dt.GetString("condo_roomno");
                        dsDetailCondo.DATA[0].CONDO_REGISNO = dt.GetString("condo_registno");
                        dsDetailCondo.DATA[0].CONDO_FLOOR = dt.GetString("condo_floor");
                        dsDetailCondo.DATA[0].POS_TAMBOL = dt.GetString("pos_tambol");
                        dsDetailCondo.DATA[0].POS_AMPHUR = dt.GetString("pos_amphur");
                        dsDetailCondo.DATA[0].POS_PROVINCE = dt.GetString("pos_province");
                        dsDetailCondo.DATA[0].CONDO_ROOMSIZE = dt.GetString("condo_roomsize");
                    }
                    else
                    {
                        dsDetailBding.DATA[0].BD_AGE = Convert.ToDecimal(dt.GetString("bd_age"));
                        dsDetailBding.DATA[0].BD_ADDRNO = dt.GetString("bd_addrno");
                        dsDetailBding.DATA[0].BD_ADDRMOO = dt.GetString("bd_addrmoo");
                        dsDetailBding.DATA[0].BD_AMPHUR = dt.GetString("bd_amphur");
                        dsDetailBding.DATA[0].BD_PROVINCE = dt.GetString("bd_province");
                        dsDetailBding.DATA[0].BD_ROAD = dt.GetString("bd_road");
                        dsDetailBding.DATA[0].BD_SOI = dt.GetString("bd_soi");
                        dsDetailBding.DATA[0].BD_TAMBOL = dt.GetString("bd_tambol");
                        dsDetailBding.DATA[0].BD_TYPE = dt.GetString("bd_type");
                        dsDetailBding.DATA[0].BD_VILLAGE = dt.GetString("bd_village");
                        dsDetailBding.DATA[0].BD_DEPRECIATION = dt.GetDecimal("bd_depreciation");
                        dsDetailBding.DATA[0].BD_ONLANDNO = dt.GetString("bd_onlandno");
                        dsDetailBding.DATA[0].COLLMASTTYPE_CODE = dt.GetString("collmasttype_code");
                        dsDetail.DATA[0].ASSETTYPE_CODE = "06";
                        dsDetailBding.DdBdtype();
                        dsDetailBding.DdCollmasttype();

                    }
                    dsDetail.DATA[0].MORTGAGE_DATE = state.SsWorkDate;
                    dsDetail.DATA[0].MORTGAGEFIRST_AMT = dt.GetDecimal("dol_prince");
                    dsDetail.DATA[0].MORTGAGESUM_AMT = dt.GetDecimal("est_price");
                    dsDetail.DATA[0].INTEREST_RATE = dt.GetDecimal("est_percent");
                    dsDetail.DATA[0].INTEREST = dt.GetString("est_percent");
                }
                dsDetail.DdAssettype();
            }
            this.SetOnLoadedScript(" parent.Setfocus();");
        }

        public void SaveWebSheet()
        {
            try
            {
                dsDetail.DATA[0].COOP_ID = state.SsCoopControl;
                dsMrtger.DATA[0].MEMBER_NO = dsMain.DATA[0].MEMBER_NO;

                ExecuteDataSource exed = new ExecuteDataSource(this);
                ExecuteDataSource exed_update = new ExecuteDataSource(this);
                string assettype_code = dsDetail.DATA[0].ASSETTYPE_CODE;
                string ls_mrtgno = dsDetail.DATA[0].MRTGMAST_NO;

                if (ls_mrtgno != "Auto")
                {
                    exed.AddFormView(dsDetail, ExecuteType.Update);

                    if (assettype_code == "01")
                    {
                        exed.AddFormView(dsDetailDeed, ExecuteType.Update);
                    }
                    else if (assettype_code == "02" || assettype_code == "03" || assettype_code == "04")
                    {
                        exed.AddFormView(dsDetailNs3, ExecuteType.Update);
                    }
                    else if (assettype_code == "05")
                    {
                        exed.AddFormView(dsDetailCondo, ExecuteType.Update);
                    }
                    else if (assettype_code == "06")
                    {
                        exed.AddFormView(dsDetailBding, ExecuteType.Update);
                    }
                    exed.AddFormView(dsAutz, ExecuteType.Update);
                    exed.AddFormView(dsMrtger, ExecuteType.Update);
                    exed.AddRepeater(dsRefcollno);
                    exed.Execute();
                    exed.SQL.Clear();
                    LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขเรียบร้อย");
                }
                else
                {
                    ls_mrtgno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "MRTGMASTER");

                    dsDetail.DATA[0].MRTGMAST_NO = ls_mrtgno;
                    dsAutz.DATA[0].COOP_ID = state.SsCoopControl;
                    dsMrtger.DATA[0].COOP_ID = state.SsCoopControl;
                    dsAutz.DATA[0].MRTGMAST_NO = ls_mrtgno;
                    dsMrtger.DATA[0].MEMBER_NO = dsMain.DATA[0].MEMBER_NO;
                    for (int i = 0; i < dsRefcollno.RowCount; i++)
                    {
                        dsRefcollno.DATA[i].MRTGMAST_NO = ls_mrtgno;
                    }
                    dsMrtger.DATA[0].MRTGMAST_NO = ls_mrtgno;
                    exed.AddFormView(dsDetail, ExecuteType.Insert);
                    if (assettype_code == "01")
                    {
                        dsDetailDeed.DATA[0].COOP_ID = state.SsCoopControl;
                        dsDetailDeed.DATA[0].MRTGMAST_NO = ls_mrtgno;
                        exed.AddFormView(dsDetailDeed, ExecuteType.Update);
                    }
                    else if (assettype_code == "02" || assettype_code == "03" || assettype_code == "04")
                    {
                        dsDetailNs3.DATA[0].COOP_ID = state.SsCoopControl;
                        dsDetailNs3.DATA[0].MRTGMAST_NO = ls_mrtgno;
                        exed.AddFormView(dsDetailNs3, ExecuteType.Update);
                    }
                    else if (assettype_code == "05")
                    {
                        dsDetailCondo.DATA[0].COOP_ID = state.SsCoopControl;
                        dsDetailCondo.DATA[0].MRTGMAST_NO = ls_mrtgno;
                        exed.AddFormView(dsDetailCondo, ExecuteType.Update);
                    }
                    else if (assettype_code == "06")
                    {
                        dsDetailBding.DATA[0].COOP_ID = state.SsCoopControl;
                        dsDetailBding.DATA[0].MRTGMAST_NO = ls_mrtgno;
                        exed.AddFormView(dsDetailBding, ExecuteType.Update);
                    }
                    exed.AddFormView(dsAutz, ExecuteType.Update);
                    exed.AddFormView(dsMrtger, ExecuteType.Update);
                    exed.AddRepeater(dsRefcollno);
                    exed.Execute();
                    exed.SQL.Clear();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                }

                //dsMain.ResetRow();
                dsDetail.ResetRow();
                dsDetailBding.ResetRow();
                dsDetailCondo.ResetRow();
                dsDetailDeed.ResetRow();
                dsDetailNs3.ResetRow();
                dsAutz.ResetRow();
                dsMrtger.ResetRow();
                dsRefcollno.ResetRow();
                dsList.Retrieve(dsMain.DATA[0].MEMBER_NO);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            this.SetOnLoadedScript(" parent.Setfocus();");
        }

        public void WebSheetLoadEnd()
        {
            string assettype_code = dsDetail.DATA[0].ASSETTYPE_CODE;
            if (assettype_code == "01")
            {
                dsDetailDeed.Visible = true;
                dsDetailBding.ResetRow();
                dsDetailNs3.ResetRow();
                dsDetailCondo.ResetRow();
                dsDetailBding.Visible = false;
                dsDetailNs3.Visible = false;
                dsDetailCondo.Visible = false;
            }
            else if (assettype_code == "02" || assettype_code == "03" || assettype_code == "04")
            {
                dsDetailNs3.Visible = true;
                dsDetailBding.ResetRow();
                dsDetailDeed.ResetRow();
                dsDetailCondo.ResetRow();
                dsDetailBding.Visible = false;
                dsDetailDeed.Visible = false;
                dsDetailCondo.Visible = false;
            }
            else if (assettype_code == "05")
            {
                dsDetailDeed.ResetRow();
                dsDetailNs3.ResetRow();
                dsDetailBding.ResetRow();
                dsDetailBding.Visible = false;
                dsDetailDeed.Visible = false;
                dsDetailNs3.Visible = false;
                dsDetailCondo.Visible = true;
            }
            else if (assettype_code == "06")
            {
                dsDetailNs3.Visible = false;
                //dsDetailBding.ResetRow();
                dsDetailDeed.ResetRow();
                dsDetailCondo.ResetRow();
                dsDetailBding.Visible = true;
                dsDetailDeed.Visible = false;
                dsDetailCondo.Visible = false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_collateral_detail_ctrl
{
    public partial class ws_sl_collateral_detail : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostPanel { get; set; }
        [JsPostBack]
        public String PostCollmastNo { get; set; }
        [JsPostBack]
        public String PostInsertRowBdingdet { get; set; }
        [JsPostBack]
        public String PostDeleteRowBdingdet { get; set; }
        [JsPostBack]
        public String PostInsertRowCollprop { get; set; }
        [JsPostBack]
        public String PostDeleteRowCollprop { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsLanduse.InitDsLanduse(this);
            dsLand.InitDsLand(this);
            dsCondo.InitDsCondo(this);
            dsBdingdet.InitDsBdingdet(this);
            dsBding.InitDsBding(this);
            dsCollprop.InitDsCollprop(this);
            dsRate.InitDsRate(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

                Panel1.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = true;
                Panel5.Visible = true;
                dsMain.DATA[0].COLLMASTTYPE_GRP = "01";
                dsRate.DATA[0].txt1 = "ราคากรมที่ดิน";
            }

            dsMain.DdCollmasttypegrp();
            dsBding.DdBdtype();
            dsLand.DdCollmasttype();
            dsBding.DdCollmasttype();
        }

        public void Collmastgrp_Changed()
        {
            string type_grp = dsMain.DATA[0].COLLMASTTYPE_GRP;

            switch (type_grp)
            {
                case "01":
                    Panel1.Visible = true;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel4.Visible = true;
                    Panel5.Visible = true;
                    dsRate.DATA[0].txt1 = "ราคากรมที่ดิน";
                    break;
                case "02":
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    Panel3.Visible = false;
                    Panel4.Visible = false;
                    Panel5.Visible = false;
                    dsRate.DATA[0].txt1 = "ราคารวม";
                    break;
                case "03":
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = true;
                    Panel4.Visible = true;
                    Panel5.Visible = true;
                    dsRate.DATA[0].txt1 = "ราคารวม";
                    break;
                default:
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel4.Visible = false;
                    Panel5.Visible = false;
                    break;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostPanel)
            {
                Collmastgrp_Changed();
            }
            else if (eventArg == PostMemberNo)
            {
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(member_no);
                dsList.RetrieveList(member_no);

                dsLand.ResetRow();
                dsLanduse.ResetRow();
                dsRate.ResetRow();
                dsCondo.ResetRow();
                dsCollprop.ResetRow();
                dsBdingdet.ResetRow();
                dsBding.ResetRow();
                //dsMain.DdCollmasttypegrp();
                dsBding.DdBdtype();
                dsLand.DdCollmasttype();
                dsBding.DdCollmasttype();
                Collmastgrp_Changed();
            }
            else if (eventArg == PostCollmastNo)
            {

                string collmast_no = dsMain.DATA[0].COLLMAST_NO;
                dsMain.Retrieve(collmast_no);
                dsLanduse.Retrieve(collmast_no);
                dsLand.Retrieve(collmast_no);
                dsCondo.Retrieve(collmast_no);
                dsBdingdet.Retrieve(collmast_no);
                dsBding.Retrieve(collmast_no);
                dsCollprop.Retrieve(collmast_no);
                dsRate.Retrieve(collmast_no);
                Collmastgrp_Changed();

                dsMain.FindDropDownList(0, "collmasttype_grp").Enabled = false;
            }
            else if (eventArg == PostInsertRowBdingdet)
            {
                dsBdingdet.InsertLastRow();
                int r = dsBdingdet.RowCount - 1;
            }
            else if (eventArg == PostDeleteRowBdingdet)
            {
                int r = dsBdingdet.GetRowFocus();
                dsBdingdet.DeleteRow(r);
            }
            else if (eventArg == PostInsertRowCollprop)
            {
                dsCollprop.InsertLastRow();
            }
            else if (eventArg == PostDeleteRowCollprop)
            {
                int r = dsCollprop.GetRowFocus();
                dsCollprop.DeleteRow(r);
            }
        }

        public void SaveWebSheet()
        {
            /*try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                string collmast_no = dsMain.DATA[0].COLLMAST_NO;
                if (collmast_no == "")
                {
                    string last_documentno = wcf.Common.GetNewDocNo(state.SsWsPass, "COLLMASTER");
                    dsMain.DATA[0].COLLMAST_NO = last_documentno;
                    exe.AddFormView(dsMain, ExecuteType.Insert);
                    exe.Execute();
                }
                else
                {
                    exe.AddFormView(dsMain, ExecuteType.Update);
                    exe.Execute();
                }
                exe.SQL.Clear();
                dsLanduse.DATA[0].COLLMAST_NO = dsMain.DATA[0].COLLMAST_NO;
                dsLanduse.DATA[0].COOP_ID = state.SsCoopControl;
                dsLand.DATA[0].COLLMAST_NO = dsMain.DATA[0].COLLMAST_NO;
                dsLand.DATA[0].COOP_ID = state.SsCoopControl;
                dsCondo.DATA[0].COOP_ID = state.SsCoopControl;
                dsCondo.DATA[0].COLLMAST_NO = dsMain.DATA[0].COLLMAST_NO;
                dsBding.DATA[0].COOP_ID = state.SsCoopControl;
                dsBding.DATA[0].COLLMAST_NO = dsMain.DATA[0].COLLMAST_NO;
                dsRate.DATA[0].COOP_ID = state.SsCoopControl;
                dsRate.DATA[0].COLLMAST_NO = dsMain.DATA[0].COLLMAST_NO;


                string type_grp = dsMain.DATA[0].COLLMASTTYPE_GRP;

                switch (type_grp)
                {
                    case "01":
                        exe.AddFormView(dsLand, ExecuteType.Update);
                        exe.AddFormView(dsLanduse, ExecuteType.Update);
                        break;
                    case "02":
                        exe.AddFormView(dsBding, ExecuteType.Update);

                        String sqlbddet = @"delete lncollmastbuilding where coop_id = {0} and collmast_no = {1} ";
                        sqlbddet = WebUtil.SQLFormat(sqlbddet, state.SsCoopControl, dsMain.DATA[0].COLLMAST_NO);
                        WebUtil.QuerySdt(sqlbddet);

                        for (int i = 0; i < dsBdingdet.RowCount; i++)
                        {
                            ExecuteDataSource exedinsert = new ExecuteDataSource(this);
                            string sqlInsert = @"insert into lncollmastbuilding (coop_id,
                                                collmast_no,
                                                floor_no ,
                                                floor_square,
                                                floor_pricesquare,
                                                floor_sumprice)values(
                                                {0},{1},{2},{3},{4},{5})";
                            object[] argslistInsert = new object[] { state.SsCoopControl,
                        dsMain.DATA[0].COLLMAST_NO,
                        dsBdingdet.DATA[i].FLOOR_NO,
                        dsBdingdet.DATA[i].FLOOR_SQUARE,
                        dsBdingdet.DATA[i].FLOOR_PRICESQUARE,
                        dsBdingdet.DATA[i].FLOOR_SUMPRICE};
                            sqlInsert = WebUtil.SQLFormat(sqlInsert, argslistInsert);
                            exedinsert.SQL.Add(sqlInsert);
                            exedinsert.Execute();
                        }
                        break;
                    case "03":
                        exe.AddFormView(dsCondo, ExecuteType.Update);
                        exe.AddFormView(dsLanduse, ExecuteType.Update);
                        break;
                }

                exe.AddFormView(dsRate, ExecuteType.Update);



                String sql = @"delete lncollmastprop where coop_id = {0} and collmast_no = {1} ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].COLLMAST_NO);
                WebUtil.QuerySdt(sql);

                for (int i = 0; i < dsCollprop.RowCount; i++)
                {
                    dsCollprop.DATA[i].COLLMAST_NO = dsMain.DATA[0].COLLMAST_NO;
                    dsCollprop.DATA[i].COOP_ID = state.SsCoopControl;
                    dsCollprop.DATA[i].SEQ_NO = i + 1;
                }
                exe.AddRepeater(dsCollprop);
                exe.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");

                dsMain.DATA[0].COLLMAST_NO = "";
                dsMain.DATA[0].COLLMASTTYPE_GRP = "01";
                dsLand.ResetRow();
                dsLanduse.ResetRow();
                dsRate.ResetRow();
                dsCondo.ResetRow();
                dsCollprop.ResetRow();
                dsBdingdet.ResetRow();
                dsBding.ResetRow();
                Collmastgrp_Changed();
                dsMain.FindDropDownList(0, "collmasttype_grp").Enabled = true;
                dsMain.DdCollmasttypegrp();
                dsBding.DdBdtype();
                dsLand.DdCollmasttype();
                dsBding.DdCollmasttype();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ" + ex);
            }*/
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}
<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_cfloantype.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.ws_sl_cfloantype" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsGeneral.ascx" TagName="DsGeneral" TagPrefix="uc2" %>
<%@ Register Src="DsIntdetail.ascx" TagName="DsIntdetail" TagPrefix="uc3" %>
<%@ Register Src="DsRightdet.ascx" TagName="DsRightdet" TagPrefix="uc4" %>
<%@ Register Src="DsClearbuyshr.ascx" TagName="DsClearbuyshr" TagPrefix="uc5" %>
<%@ Register Src="DsCleardet.ascx" TagName="DsCleardet" TagPrefix="uc6" %>
<%@ Register Src="DsClearlist.ascx" TagName="DsClearlist" TagPrefix="uc7" %>
<%@ Register Src="DsCollcanuse.ascx" TagName="DsCollcanuse" TagPrefix="uc8" %>
<%@ Register Src="DsColldet.ascx" TagName="DsColldet" TagPrefix="uc9" %>
<%@ Register Src="DsCollreqgrt.ascx" TagName="DsCollreqgrt" TagPrefix="uc10" %>
<%@ Register Src="DsDpcancoll.ascx" TagName="DsDpcancoll" TagPrefix="uc11" %>
<%@ Register Src="DsDropln.ascx" TagName="DsDropln" TagPrefix="uc12" %>
<%@ Register Src="DsMbsubgrp.ascx" TagName="DsMbsubgrp" TagPrefix="uc13" %>
<%@ Register Src="DsPaymentdet.ascx" TagName="DsPaymentdet" TagPrefix="uc14" %>
<%@ Register Src="DsPaymentlist.ascx" TagName="DsPaymentlist" TagPrefix="uc15" %>
<%@ Register Src="DsPermdown.ascx" TagName="DsPermdown" TagPrefix="uc16" %>
<%@ Register Src="DsRightcollmast.ascx" TagName="DsRightcollmast" TagPrefix="uc17" %>
<%@ Register Src="DsRightcustom.ascx" TagName="DsRightcustom" TagPrefix="uc18" %>
<%@ Register Src="DsIntspc.ascx" TagName="DsIntspc" TagPrefix="uc19" %>
<%@ Register Src="DsSalbal.ascx" TagName="DsSalbal" TagPrefix="uc20" %>
<%@ Register Src="DsCmSalbal.ascx" TagName="DsCmSalbal" TagPrefix="uc21" %>
<%@ Register Src="DsLoantypeSalbal.ascx" TagName="DsLoantypeSalbal" TagPrefix="uc22" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsMain = new DataSourceTool;
        var dsGeneral = new DataSourceTool;
        var dsRightdet = new DataSourceTool;
        var dsIntdetail = new DataSourceTool;
        var dsColldet = new DataSourceTool;
        var dsCollreqgrt = new DataSourceTool;
        var dsDpcancoll = new DataSourceTool;
        var dsCollcanuse = new DataSourceTool;
        var dsCleardet = new DataSourceTool;
        var dsClearlist = new DataSourceTool;
        var dsClearbuyshr = new DataSourceTool;
        var dsDropln = new DataSourceTool;
        var dsMbsubgrp = new DataSourceTool;
        var dsPaymentdet = new DataSourceTool;
        var dsPaymentlist = new DataSourceTool;
        var dsPermdown = new DataSourceTool;
        var dsRightcollmast = new DataSourceTool;
        var dsRightcustom = new DataSourceTool;
        var dsIntspc = new DataSourceTool;
        var dsSalbal = new DataSourceTool;
        var dsCmSalbal = new DataSourceTool;
        var dsLoantypeSalbal = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        //DsMain
        function OnDsMainClicked(s, r, c) {
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "loantype_code") {
                PostLoanTypeCode();
            }
        }

        //DsGeneral
        function OnDsGeneralClicked(s, r, c) {
            if (c == "b_searchdoc") {
                Gcoop.OpenIfameExtend('600', '600', 'w_dlg_sl_cfloantype_contnodlg.aspx', '');
            }
        }

        function OnDsGeneralItemChanged(s, r, c, v) {
            if (c == "counttimecont_type") {
                if (v == "0") {
                    dsGeneral.GetElement(0, "contract_time").readOnly = true;
                    dsGeneral.GetElement(0, "contract_time").style.background = "#CCCCCC";
                    dsGeneral.GetElement(0, "contalert_time").readOnly = true;
                    dsGeneral.GetElement(0, "contalert_time").style.background = "#CCCCCC";
                } else {
                    dsGeneral.GetElement(0, "contract_time").readOnly = false;
                    dsGeneral.GetElement(0, "contract_time").style.background = "#FFFFFF";
                    dsGeneral.GetElement(0, "contalert_time").readOnly = false;
                    dsGeneral.GetElement(0, "contalert_time").style.background = "#FFFFFF";
                }
            } 
            /*else if (c == "salarybal_status") {

                if (v == "1") {
                    dsGeneral.GetElement(0, "salarybal_code").disabled = false;
                    dsGeneral.SetItem(0, "salarybal_code", "");
                } else if (v == "2") {
                    dsGeneral.GetElement(0, "salarybal_code").disabled = false;
                    dsGeneral.SetItem(0, "salarybal_code", "");
                    dsGeneral.GetElement(0, "salarybal_code").disabled = true;
                } else {
                    dsGeneral.GetElement(0, "salarybal_code").disabled = true;
                    dsGeneral.SetItem(0, "salarybal_code", "");
                }
            } */
        }

        //DsRightdet
        function OnDsRightdetClicked(s, r, c) {

        }

        function OnDsRightdetItemChanged(s, r, c, v) {
            if (c == "loanright_type") {
                if (v == "1") {
                    dsRightdet.GetElement(0, "customtime_type").disabled = true;
                    dsRightdet.SetItem(0, "customtime_type", "");
                } else {
                    dsRightdet.GetElement(0, "customtime_type").disabled = false;
                }
            }
        }

        //DsRightcollmast        
        function OnDsRightcollmastClicked(s, r, c) {
            if (c == "b_del") {
                dsRightcollmast.SetRowFocus(r);
                PostDelRowRightcollmast();
            }

        }

        function OnDsRightcollmastItemChanged(s, r, c, v) {
            if (c == "loancolltype_code") {
                if (v != "04") {
                    dsRightcollmast.GetElement(r, "collmasttype_code").disabled = true;
                    dsRightcollmast.SetItem(r, "collmasttype_code", "00");
                } else {
                    dsRightcollmast.GetElement(r, "collmasttype_code").disabled = false;
                    dsRightcollmast.SetItem(r, "collmasttype_code", "");
                }
            } else if (c == "right_format") {

                if (v == "1") {
                    dsRightcollmast.GetElement(r, "right_ratio").style.display = "none";
                    dsRightcollmast.GetElement(r, "right_perc").style.display = "block";
                    dsRightcollmast.SetItem(r, "right_ratio", "");
                } else {
                    dsRightcollmast.GetElement(r, "right_perc").style.display = "none";
                    dsRightcollmast.GetElement(r, "right_ratio").style.display = "block";
                    dsRightcollmast.SetItem(r, "right_perc", "");
                }
            }
        }

        //dsRightcustom
        function OnDsRightcustomClicked(s, r, c) {
            if (c == "b_del") {
                dsRightcustom.SetRowFocus(r);
                PostDelRowRightcustom();
            }

        }

        function OnDsRightcustomItemChanged(s, r, c, v) {

        }

        //dsIntdetail
        function OnDsIntdetailClicked(s, r, c) {
        }

        function OnDsIntdetailItemChanged(s, r, c, v) {
            if (c == "contint_type") {
                switch (v) {
                    case "0":
                        dsIntdetail.GetElement(r, "inttabfix_code").disabled = true;
                        dsIntdetail.SetItem(r, "inttabfix_code", "");
                        dsIntdetail.GetElement(r, "inttabrate_code").disabled = true;
                        dsIntdetail.SetItem(r, "inttabrate_code", "");
                        dsIntdetail.GetElement(0, "intrate_increase").readOnly = true;
                        dsIntdetail.GetElement(0, "intrate_increase").style.background = "#CCCCCC";
                        dsIntdetail.SetItem(0, "intrate_increase", "0");
                        break;
                    case "1":
                        dsIntdetail.GetElement(r, "inttabfix_code").disabled = false;
                        dsIntdetail.SetItem(r, "inttabfix_code", "");
                        dsIntdetail.GetElement(r, "inttabrate_code").disabled = true;
                        dsIntdetail.SetItem(r, "inttabrate_code", "");
                        dsIntdetail.GetElement(0, "intrate_increase").readOnly = true;
                        dsIntdetail.GetElement(0, "intrate_increase").style.background = "#CCCCCC";
                        dsIntdetail.SetItem(0, "intrate_increase", "0");
                        break;
                    case "2":
                        dsIntdetail.GetElement(r, "inttabfix_code").disabled = true;
                        dsIntdetail.SetItem(r, "inttabfix_code", "");
                        dsIntdetail.GetElement(r, "inttabrate_code").disabled = false;
                        dsIntdetail.SetItem(r, "inttabrate_code", "");
                        dsIntdetail.GetElement(0, "intrate_increase").readOnly = false;
                        dsIntdetail.GetElement(0, "intrate_increase").style.background = "#FFFFFF";
                        dsIntdetail.SetItem(0, "intrate_increase", "0");
                        break;
                    case "3":
                        dsIntdetail.GetElement(r, "inttabfix_code").disabled = true;
                        dsIntdetail.SetItem(r, "inttabfix_code", "");
                        dsIntdetail.GetElement(r, "inttabrate_code").disabled = true;
                        dsIntdetail.SetItem(r, "inttabrate_code", "");
                        dsIntdetail.GetElement(0, "intrate_increase").readOnly = true;
                        dsIntdetail.GetElement(0, "intrate_increase").style.background = "#CCCCCC";
                        dsIntdetail.SetItem(0, "intrate_increase", "0");
                        break;
                    case "4":

                        dsIntdetail.GetElement(r, "inttabfix_code").disabled = true;
                        dsIntdetail.SetItem(r, "inttabfix_code", "");
                        dsIntdetail.GetElement(r, "inttabrate_code").disabled = false;
                        dsIntdetail.SetItem(r, "inttabrate_code", "");
                        dsIntdetail.GetElement(0, "intrate_increase").readOnly = false;
                        dsIntdetail.GetElement(0, "intrate_increase").style.background = "#FFFFFF";
                        dsIntdetail.SetItem(0, "intrate_increase", "0");
                        break;
                    case "5":
                        dsIntdetail.GetElement(r, "inttabfix_code").disabled = true;
                        dsIntdetail.SetItem(r, "inttabfix_code", "");
                        dsIntdetail.GetElement(r, "inttabrate_code").disabled = true;
                        dsIntdetail.SetItem(r, "inttabrate_code", "");
                        dsIntdetail.GetElement(0, "intrate_increase").readOnly = false;
                        dsIntdetail.GetElement(0, "intrate_increase").style.background = "#FFFFFF";
                        dsIntdetail.SetItem(0, "intrate_increase", "0");
                        break;
                }
            } else if (c == "interest_method") {
                if (v == "1") {
                    dsIntdetail.GetElement(0, "calintrcv_nottype").disabled = true;
                    dsIntdetail.SetItem(0, "calintrcv_nottype", 0);
                    dsIntdetail.GetElement(0, "calintrcv_notdate").readOnly = true;
                    dsIntdetail.GetElement(0, "calintrcv_notdate").style.background = "#CCCCCC";
                    dsIntdetail.SetItem(0, "calintrcv_notdate", 0);
                    dsIntdetail.GetElement(0, "calintpay_nottype").disabled = true;
                    dsIntdetail.SetItem(0, "calintpay_nottype", 0);
                    dsIntdetail.GetElement(0, "calintpay_notdate").readOnly = true;
                    dsIntdetail.GetElement(0, "calintpay_notdate").style.background = "#CCCCCC";
                    dsIntdetail.SetItem(0, "calintpay_notdate", 0);
                    dsIntdetail.GetElement(0, "calintrcv_halftype").disabled = true;
                    dsIntdetail.SetItem(0, "calintrcv_halftype", 0);
                    dsIntdetail.GetElement(0, "calintrcv_halfdate").readOnly = true;
                    dsIntdetail.GetElement(0, "calintrcv_halfdate").style.background = "#CCCCCC";
                    dsIntdetail.SetItem(0, "calintrcv_halfdate", 0);
                    dsIntdetail.GetElement(0, "calintpay_halftype").disabled = true;
                    dsIntdetail.SetItem(0, "calintpay_halftype", 0);
                    dsIntdetail.GetElement(0, "calintpay_halfdate").readOnly = true;
                    dsIntdetail.GetElement(0, "calintpay_halfdate").style.background = "#CCCCCC";
                    dsIntdetail.SetItem(0, "calintpay_halfdate", 0);
                } else {
                    dsIntdetail.GetElement(0, "calintrcv_nottype").disabled = false;
                    dsIntdetail.GetElement(0, "calintrcv_notdate").readOnly = false;
                    dsIntdetail.GetElement(0, "calintrcv_notdate").style.background = "#FFFFFF";
                    dsIntdetail.GetElement(0, "calintpay_nottype").disabled = false;
                    dsIntdetail.GetElement(0, "calintpay_notdate").readOnly = false;
                    dsIntdetail.GetElement(0, "calintpay_notdate").style.background = "#FFFFFF";
                    dsIntdetail.GetElement(0, "calintrcv_halftype").disabled = false;
                    dsIntdetail.GetElement(0, "calintrcv_halfdate").readOnly = false;
                    dsIntdetail.GetElement(0, "calintrcv_halfdate").style.background = "#FFFFFF";
                    dsIntdetail.GetElement(0, "calintpay_halftype").disabled = false;
                    dsIntdetail.GetElement(0, "calintpay_halfdate").readOnly = false;
                    dsIntdetail.GetElement(0, "calintpay_halfdate").style.background = "#FFFFFF";
                }
            }
        }

        //dsIntspc
        function OnDsIntspcClicked(s, r, c) {
            if (c == "b_del") {
                dsIntspc.SetRowFocus(r);
                PostDelRowIntspc();
            }

        }

        function OnDsIntspcItemChanged(s, r, c, v) {
            if (c == "inttime_amt_1") {
                if (v == 0) {
                    dsIntspc.SetItem(r, "inttime_amt", 0);
                    dsIntspc.SetItem(r, "inttime_amt_1", "จนจบสัญญา");
                } else {
                    dsIntspc.SetItem(r, "inttime_amt", v);
                }
            } else if (c == "intrate_type") {
                if (v == "0") {
                    dsIntspc.GetElement(r, "intratetab_code").disabled = true;
                    dsIntspc.SetItem(r, "intratetab_code", "");
                    dsIntspc.GetElement(r, "intratefix_rate").readOnly = true;
                    dsIntspc.GetElement(r, "intratefix_rate").style.background = "#CCCCCC";
                    dsIntspc.SetItem(r, "intratefix_rate", "0");
                    dsIntspc.GetElement(r, "intrate_increase").readOnly = true;
                    dsIntspc.GetElement(r, "intrate_increase").style.background = "#CCCCCC";
                    dsIntspc.SetItem(r, "intrate_increase", "0");
                }
                else if (v == "1") {
                    dsIntspc.GetElement(r, "intratetab_code").disabled = true;
                    dsIntspc.SetItem(r, "intratetab_code", "");
                    dsIntspc.GetElement(r, "intratefix_rate").readOnly = false;
                    dsIntspc.GetElement(r, "intratefix_rate").style.background = "#FFFFFF";
                    dsIntspc.SetItem(r, "intratefix_rate", "0");
                    dsIntspc.GetElement(r, "intrate_increase").readOnly = true;
                    dsIntspc.GetElement(r, "intrate_increase").style.background = "#CCCCCC";
                    dsIntspc.SetItem(r, "intrate_increase", "0");
                }
                else if (v == "2") {
                    dsIntspc.GetElement(r, "intratetab_code").disabled = false;
                    dsIntspc.SetItem(r, "intratetab_code", "");
                    dsIntspc.GetElement(r, "intratefix_rate").readOnly = true;
                    dsIntspc.GetElement(r, "intratefix_rate").style.background = "#CCCCCC";
                    dsIntspc.SetItem(r, "intratefix_rate", "0");
                    dsIntspc.GetElement(r, "intrate_increase").readOnly = false;
                    dsIntspc.GetElement(r, "intrate_increase").style.background = "#FFFFFF";
                    dsIntspc.SetItem(r, "intrate_increase", "0");
                }
            }
        }

        //dsColldet
        function OnDsColldetClicked(s, r, c) {

        }

        function OnDsColldetItemChanged(s, r, c, v) {
            if (c == "grtneed_flag") {
                if (v == "1") {
                    dsColldet.GetElement(r, "usemangrt_status").disabled = false;
                    dsColldet.SetItem(0, "cntmangrtval_flag", 1);
                } else {
                    dsColldet.GetElement(r, "usemangrt_status").disabled = true;
                    dsColldet.SetItem(0, "usemangrt_status", 0);
                    dsColldet.GetElement(r, "mangrtpermgrp_code").disabled = true;
                    dsColldet.SetItem(0, "mangrtpermgrp_code", "");
                    dsColldet.GetElement(r, "mangrtpermgrpco_code").disabled = true;
                    dsColldet.SetItem(0, "mangrtpermgrpco_code", "");
                    dsColldet.GetElement(r, "cntmangrtnum_flag").disabled = true;
                    dsColldet.SetItem(0, "cntmangrtnum_flag", 0);
                    dsColldet.GetElement(r, "cntmangrtval_flag").disabled = true;
                    dsColldet.SetItem(0, "cntmangrtval_flag", 0);
                    dsColldet.GetElement(r, "lockshare_flag").disabled = true;
                }
            } else if (c == "usemangrt_status") {
                if (v == "1") {
                    dsColldet.GetElement(r, "mangrtpermgrp_code").disabled = false;
                    dsColldet.GetElement(r, "mangrtpermgrpco_code").disabled = false;
                    dsColldet.GetElement(r, "cntmangrtnum_flag").disabled = false;
                    dsColldet.GetElement(r, "cntmangrtval_flag").disabled = false;
                    dsColldet.GetElement(r, "lockshare_flag").disabled = false;
                } else {
                    dsColldet.GetElement(r, "mangrtpermgrp_code").disabled = true;
                    dsColldet.SetItem(0, "mangrtpermgrp_code", "");
                    dsColldet.GetElement(r, "mangrtpermgrpco_code").disabled = true;
                    dsColldet.SetItem(0, "mangrtpermgrpco_code", "");
                    dsColldet.GetElement(r, "cntmangrtnum_flag").disabled = true;
                    dsColldet.SetItem(0, "cntmangrtnum_flag", 0);
                    dsColldet.GetElement(r, "cntmangrtval_flag").disabled = true;
                    dsColldet.SetItem(0, "cntmangrtval_flag", 1);
                    dsColldet.GetElement(r, "lockshare_flag").disabled = true;
                }

            }
        }

        //dsCollreqgrt
        function OnDsCollreqgrtClicked(s, r, c) {
            if (c == "b_del") {
                dsCollreqgrt.SetRowFocus(r);
                PostDelRowCollreqgrt();
            }
        }

        function OnDsCollreqgrtItemChanged(s, r, c, v) {
            if (c == "useman_type") {
                if (v == "0") {
                    dsCollreqgrt.GetElement(r, "useman_amt").readOnly = true;
                    dsCollreqgrt.GetElement(r, "useman_amt").style.background = "#CCCCCC";
                    dsCollreqgrt.SetItem(r, "useman_amt", 0);
                    dsCollreqgrt.GetElement(r, "usememmain_amt").readOnly = true;
                    dsCollreqgrt.GetElement(r, "usememmain_amt").style.background = "#CCCCCC";
                    dsCollreqgrt.GetElement(r, "usemem_operation").disabled = true;
                    dsCollreqgrt.GetElement(r, "usememco_amt").readOnly = true;
                    dsCollreqgrt.GetElement(r, "usememco_amt").style.background = "#CCCCCC";
                } else if (v == "1") {
                    dsCollreqgrt.GetElement(r, "useman_amt").readOnly = false;
                    dsCollreqgrt.GetElement(r, "useman_amt").style.background = "#FFFFFF";
                    dsCollreqgrt.GetElement(r, "usememmain_amt").readOnly = true;
                    dsCollreqgrt.GetElement(r, "usememmain_amt").style.background = "#CCCCCC";
                    dsCollreqgrt.GetElement(r, "usemem_operation").disabled = true;
                    dsCollreqgrt.GetElement(r, "usememco_amt").readOnly = true;
                    dsCollreqgrt.GetElement(r, "usememco_amt").style.background = "#CCCCCC";
                } else if (v == "2") {
                    dsCollreqgrt.GetElement(r, "useman_amt").readOnly = true;
                    dsCollreqgrt.GetElement(r, "useman_amt").style.background = "#CCCCCC";
                    dsCollreqgrt.GetElement(r, "usememmain_amt").readOnly = false;
                    dsCollreqgrt.GetElement(r, "usememmain_amt").style.background = "#FFFFFF";
                    dsCollreqgrt.GetElement(r, "usemem_operation").disabled = false;
                    dsCollreqgrt.GetElement(r, "usememco_amt").readOnly = false;
                    dsCollreqgrt.GetElement(r, "usememco_amt").style.background = "#FFFFFF";
                }

            }
        }

        //dsCollcanuse
        function OnDsCollcanuseClicked(s, r, c) {
            if (c == "b_del") {
                dsCollcanuse.SetRowFocus(r);
                PostDelRowCollcanuse();
            }

        }

        function OnDsCollcanuseItemChanged(s, r, c, v) {
            if (c == "loancolltype_code") {
                if (v != "04") {
                    dsCollcanuse.GetElement(r, "collmasttype_code").disabled = true;
                    dsCollcanuse.SetItem(r, "collmasttype_code", "00");
                } else {
                    dsCollcanuse.GetElement(r, "collmasttype_code").disabled = false;
                    dsCollcanuse.SetItem(r, "collmasttype_code", "");
                }
            }
        }

        //DsDpcancoll
        function OnDsDpcancollClicked(s, r, c) {

        }

        function OnDsDpcancollItemChanged(s, r, c, v) {

        }



        //dsCleardet
        function OnDsCleardetClicked(s, r, c) {

        }

        function OnDsCleardetItemChanged(s, r, c, v) {

        }

        //dsClearlist
        function OnDsClearlistClicked(s, r, c) {
            if (c == "b_del") {
                dsClearlist.SetRowFocus(r);
                PostDelRowClearlist();
            }

        }

        function OnDsClearlistItemChanged(s, r, c, v) {
            if (c == "loantype_clear") {
                dsClearlist.SetItem(r, "loantype_clear1", v);

            } else if (c == "loantype_clear1") {
                dsClearlist.SetItem(r, "loantype_clear", v);

            } else if (c == "finecond_type") {
                if (v == "0") {
                    dsClearlist.GetElement(r, "find_percent").readOnly = true;
                    dsClearlist.GetElement(r, "find_percent").style.background = "#CCCCCC";
                    dsClearlist.SetItem(r, "find_percent", 0);
                    dsClearlist.GetElement(r, "fine_amt").readOnly = true;
                    dsClearlist.GetElement(r, "fine_amt").style.background = "#CCCCCC";
                    dsClearlist.SetItem(r, "fine_amt", 0);
                    dsClearlist.GetElement(r, "fine_maxamt").readOnly = true;
                    dsClearlist.GetElement(r, "fine_maxamt").style.background = "#CCCCCC";
                    dsClearlist.SetItem(r, "fine_maxamt", 0);
                } else {
                    dsClearlist.GetElement(r, "find_percent").readOnly = false;
                    dsClearlist.GetElement(r, "find_percent").style.background = "#FFFFFF";
                    dsClearlist.GetElement(r, "fine_amt").readOnly = false;
                    dsClearlist.GetElement(r, "fine_amt").style.background = "#FFFFFF";
                    dsClearlist.GetElement(r, "fine_maxamt").readOnly = false;
                    dsClearlist.GetElement(r, "fine_maxamt").style.background = "#FFFFFF";
                }
            }
        }

        //dsClearbuyshr
        function OnDsClearbuyshrClicked(s, r, c) {
            if (c == "b_del") {
                dsClearbuyshr.SetRowFocus(r);
                PostDelRowClearbuyshr();
            }

        }

        function OnDsClearbuyshrItemChanged(s, r, c, v) {

        }

        //dsPaymentdet
        function OnDsPaymentdetClicked(s, r, c) {

        }

        function OnDsPaymentdetItemChanged(s, r, c, v) {
            if (c == "loanpayment_type") {
                if (v == "0") {
                    dsPaymentdet.GetElement(r, "lastpayment_type").disabled = true;
                    dsPaymentdet.SetItem(r, "lastpayment_type", "");
                    dsPaymentdet.GetElement(r, "retryloansend_type").disabled = true;
                    dsPaymentdet.SetItem(r, "retryloansend_type", "");
                    dsPaymentdet.GetElement(r, "dropprncpay_flag").disabled = true;
                    dsPaymentdet.SetItem(r, "dropprncpay_flag", 0);
                    dsPaymentdet.GetElement(r, "loanpayment_count").disabled = true;
                    dsPaymentdet.SetItem(r, "loanpayment_count", 0); //retryloansend_time
                    dsPaymentdet.GetElement(r, "retryloansend_time").readOnly = true;
                    dsPaymentdet.GetElement(r, "retryloansend_time").style.background = "#CCCCCC";
                    dsPaymentdet.SetItem(r, "retryloansend_time", "");
                } else {
                    dsPaymentdet.GetElement(r, "lastpayment_type").disabled = false;
                    dsPaymentdet.GetElement(r, "retryloansend_type").disabled = false;
                    dsPaymentdet.GetElement(r, "dropprncpay_flag").disabled = false;
                    dsPaymentdet.GetElement(r, "loanpayment_count").disabled = false;
                    //                    dsPaymentdet.GetElement(r, "retryloansend_time").readOnly = false;
                    //                    dsPaymentdet.GetElement(r, "retryloansend_time").style.background = "#FFFFFF";
                }
            } else if (c == "retryloansend_type") {
                if (v == "0") {
                    dsPaymentdet.GetElement(r, "retryloansend_time").readOnly = true;
                    dsPaymentdet.GetElement(r, "retryloansend_time").style.background = "#CCCCCC";
                    dsPaymentdet.SetItem(r, "retryloansend_time", "");
                } else {
                    dsPaymentdet.GetElement(r, "retryloansend_time").readOnly = false;
                    dsPaymentdet.GetElement(r, "retryloansend_time").style.background = "#FFFFFF";
                }
            }
        }

        //dsPaymentlist
        function OnDsPaymentlistClicked(s, r, c) {
            if (c == "b_del") {
                dsPaymentlist.SetRowFocus(r);
                PostDelRowPaymentlist();
            }

        }

        function OnDsPaymentlistItemChanged(s, r, c, v) {

        }

        //dsDropln
        function OnDsDroplnClicked(s, r, c) {
            if (c == "b_del") {
                dsDropln.SetRowFocus(r);
                PostDelRowDropln();
            }

        }

        function OnDsDroplnItemChanged(s, r, c, v) {
            if (c == "loantype_pause_1") {
                dsDropln.SetItem(r, "loantype_pause", v);

            } else if (c == "loantype_pause") {
                dsDropln.SetItem(r, "loantype_pause_1", v);

            }
        }

        //dsPermdown
        function OnDsPermdownClicked(s, r, c) {
            if (c == "b_del") {
                dsPermdown.SetRowFocus(r);
                PostDelRowPermdown();
            }

        }

        function OnDsPermdownItemChanged(s, r, c, v) {
            if (c == "loantype_down_1") {
                dsPermdown.SetItem(r, "loantype_down", v);

            } else if (c == "loantype_down") {
                dsPermdown.SetItem(r, "loantype_down_1", v);

            }
        }

        //DsMbsubgrp
        function OnDsMbsubgrpClicked(s, r, c) {

        }

        function OnDsMbsubgrpItemChanged(s, r, c, v) {

        }

        function GetContNo(contno) {
            var str_temp = window.location.toString();
            var str_arr = str_temp.split("?", 2);
            dsGeneral.SetItem(1, "document_code", contno);
        }

        //DsSalbal
        function OnDsSalbalClicked(s, r, c) {
            
        }

        function OnDsSalbalItemChanged(s, r, c, v) {
            if (c == "cksalarybal_status") {
                PostCkSalarybal();
            }
        }

        //DsCmSalbal
        function OnDsCmSalbalClicked(s, r, c) {

        }

        function OnDsCmSalbalItemChanged(s, r, c, v) {
            if (c == "salarybal_code") {
                dsCmSalbal.SetRowFocus(r);
                PostCmSalbalCode();
            }

        }

        //DsLoantypeSalbal
        function OnDsLoantypeSalbalClicked(s, r, c) {
            if (c == "b_dellnbal") {
                dsLoantypeSalbal.SetRowFocus(r);
                PostDelRowLnSalbal();
            }
        }

        function OnDsLoantypeSalbalItemChanged(s, r, c, v) {

        }

        function SheetLoadComplete() {

            if (dsGeneral.GetItem(0, "counttimecont_type") == "0") {
                dsGeneral.GetElement(0, "contract_time").readOnly = true;
                dsGeneral.GetElement(0, "contract_time").style.background = "#CCCCCC";
                dsGeneral.GetElement(0, "contalert_time").readOnly = true;
                dsGeneral.GetElement(0, "contalert_time").style.background = "#CCCCCC";
            }

            /*if (dsGeneral.GetItem(0, "salarybal_status") == "0") {
                dsGeneral.GetElement(0, "salarybal_code").disabled = true;
            }*/

            if (dsRightdet.GetItem(0, "loanright_type") == "1") {
                dsRightdet.GetElement(0, "customtime_type").disabled = true;
            }

            for (var i = 0; i < dsRightcollmast.GetRowCount(); i++) {
                var v = dsRightcollmast.GetItem(i, "collmasttype_code");
                if (v == "00") {
                    dsRightcollmast.GetElement(i, "collmasttype_code").disabled = true;
                }

                var format = dsRightcollmast.GetItem(i, "right_format");

                if (format == "1") {
                    dsRightcollmast.GetElement(i, "right_ratio").style.display = "none";
                    dsRightcollmast.GetElement(i, "right_perc").style.display = "block";
                } else {
                    dsRightcollmast.GetElement(i, "right_perc").style.display = "none";
                    dsRightcollmast.GetElement(i, "right_ratio").style.display = "block";
                }
            }

            switch (dsIntdetail.GetItem(0, "contint_type")) {
                case 0:
                    dsIntdetail.GetElement(0, "inttabfix_code").disabled = true;
                    dsIntdetail.GetElement(0, "inttabrate_code").disabled = true;
                    dsIntdetail.GetElement(0, "intrate_increase").readOnly = true;
                    dsIntdetail.GetElement(0, "intrate_increase").style.background = "#CCCCCC";
                    break;
                case 1:
                    dsIntdetail.GetElement(0, "inttabfix_code").disabled = false;
                    dsIntdetail.GetElement(0, "inttabrate_code").disabled = true;
                    dsIntdetail.GetElement(0, "intrate_increase").readOnly = true;
                    dsIntdetail.GetElement(0, "intrate_increase").style.background = "#CCCCCC";
                    break;
                case 2:
                    dsIntdetail.GetElement(0, "inttabfix_code").disabled = true;
                    dsIntdetail.GetElement(0, "inttabrate_code").disabled = false;
                    dsIntdetail.GetElement(0, "intrate_increase").readOnly = false;
                    dsIntdetail.GetElement(0, "intrate_increase").style.background = "#FFFFFF";
                    break;
                case 3:
                    dsIntdetail.GetElement(0, "inttabfix_code").disabled = true;
                    dsIntdetail.GetElement(0, "inttabrate_code").disabled = true;
                    dsIntdetail.GetElement(0, "intrate_increase").readOnly = true;
                    dsIntdetail.GetElement(0, "intrate_increase").style.background = "#CCCCCC";
                    break;
                case 4:

                    dsIntdetail.GetElement(0, "inttabfix_code").disabled = true;
                    dsIntdetail.GetElement(0, "inttabrate_code").disabled = false;
                    dsIntdetail.GetElement(0, "intrate_increase").readOnly = false;
                    dsIntdetail.GetElement(0, "intrate_increase").style.background = "#FFFFFF";
                    break;
                case 5:
                    dsIntdetail.GetElement(0, "inttabfix_code").disabled = true;
                    dsIntdetail.GetElement(0, "inttabrate_code").disabled = true;
                    dsIntdetail.GetElement(0, "intrate_increase").readOnly = false;
                    dsIntdetail.GetElement(0, "intrate_increase").style.background = "#FFFFFF";
                    break;
            }

            if (dsIntdetail.GetItem(0, "interest_method") == "1") {
                dsIntdetail.GetElement(0, "calintrcv_nottype").disabled = true;
                dsIntdetail.GetElement(0, "calintrcv_notdate").readOnly = true;
                dsIntdetail.GetElement(0, "calintrcv_notdate").style.background = "#CCCCCC";
                dsIntdetail.GetElement(0, "calintpay_nottype").disabled = true;
                dsIntdetail.GetElement(0, "calintpay_notdate").readOnly = true;
                dsIntdetail.GetElement(0, "calintpay_notdate").style.background = "#CCCCCC";
                dsIntdetail.GetElement(0, "calintrcv_halftype").disabled = true;
                dsIntdetail.GetElement(0, "calintrcv_halfdate").readOnly = true;
                dsIntdetail.GetElement(0, "calintrcv_halfdate").style.background = "#CCCCCC";
                dsIntdetail.GetElement(0, "calintpay_halftype").disabled = true;
                dsIntdetail.GetElement(0, "calintpay_halfdate").readOnly = true;
                dsIntdetail.GetElement(0, "calintpay_halfdate").style.background = "#CCCCCC";
            }

            for (var i = 0; i < dsIntspc.GetRowCount(); i++) {
                if (dsIntspc.GetItem(i, "inttime_amt_1") == 0) {
                    dsIntspc.SetItem(i, "inttime_amt_1", "จนจบสัญญา");
                }

                if (dsIntspc.GetItem(i, "intrate_type") == "0") {
                    dsIntspc.GetElement(i, "intratetab_code").disabled = true;
                    dsIntspc.GetElement(i, "intratefix_rate").readOnly = true;
                    dsIntspc.GetElement(i, "intratefix_rate").style.background = "#CCCCCC";
                    dsIntspc.GetElement(i, "intrate_increase").readOnly = true;
                    dsIntspc.GetElement(i, "intrate_increase").style.background = "#CCCCCC";
                }
                else if (dsIntspc.GetItem(i, "intrate_type") == "1") {
                    dsIntspc.GetElement(i, "intratetab_code").disabled = true;
                    dsIntspc.GetElement(i, "intratefix_rate").readOnly = false;
                    dsIntspc.GetElement(i, "intratefix_rate").style.background = "#FFFFFF";
                    dsIntspc.GetElement(i, "intrate_increase").readOnly = true;
                    dsIntspc.GetElement(i, "intrate_increase").style.background = "#CCCCCC";
                }
                else if (dsIntspc.GetItem(i, "intrate_type") == "2") {
                    dsIntspc.GetElement(i, "intratetab_code").disabled = false;
                    dsIntspc.GetElement(i, "intratefix_rate").readOnly = true;
                    dsIntspc.GetElement(i, "intratefix_rate").style.background = "#CCCCCC";
                    dsIntspc.GetElement(i, "intrate_increase").readOnly = false;
                    dsIntspc.GetElement(i, "intrate_increase").style.background = "#FFFFFF";
                }

            }

            if (dsColldet.GetItem(0, "usemangrt_status") == "0") {
                dsColldet.GetElement(0, "mangrtpermgrp_code").disabled = true;
                dsColldet.GetElement(0, "mangrtpermgrpco_code").disabled = true;
                dsColldet.GetElement(0, "cntmangrtnum_flag").disabled = true;
                dsColldet.GetElement(0, "cntmangrtval_flag").disabled = true;
                dsColldet.GetElement(0, "lockshare_flag").disabled = true;
            }

            for (var i = 0; i < dsCollreqgrt.GetRowCount(); i++) {
                var useman_type = dsCollreqgrt.GetItem(i, "useman_type");
                if (useman_type == "0") {
                    dsCollreqgrt.GetElement(i, "useman_amt").readOnly = true;
                    dsCollreqgrt.GetElement(i, "useman_amt").style.background = "#CCCCCC";
                    dsCollreqgrt.GetElement(i, "usememmain_amt").readOnly = true;
                    dsCollreqgrt.GetElement(i, "usememmain_amt").style.background = "#CCCCCC";
                    dsCollreqgrt.GetElement(i, "usemem_operation").disabled = true;
                    dsCollreqgrt.GetElement(i, "usememco_amt").readOnly = true;
                    dsCollreqgrt.GetElement(i, "usememco_amt").style.background = "#CCCCCC";
                } else if (useman_type == "1") {
                    dsCollreqgrt.GetElement(i, "useman_amt").readOnly = false;
                    dsCollreqgrt.GetElement(i, "useman_amt").style.background = "#FFFFFF";
                    dsCollreqgrt.GetElement(i, "usememmain_amt").readOnly = true;
                    dsCollreqgrt.GetElement(i, "usememmain_amt").style.background = "#CCCCCC";
                    dsCollreqgrt.GetElement(i, "usemem_operation").disabled = true;
                    dsCollreqgrt.GetElement(i, "usememco_amt").readOnly = true;
                    dsCollreqgrt.GetElement(i, "usememco_amt").style.background = "#CCCCCC";
                } else if (useman_type == "2") {
                    dsCollreqgrt.GetElement(i, "useman_amt").readOnly = true;
                    dsCollreqgrt.GetElement(i, "useman_amt").style.background = "#CCCCCC";
                    dsCollreqgrt.GetElement(i, "usememmain_amt").readOnly = false;
                    dsCollreqgrt.GetElement(i, "usememmain_amt").style.background = "#FFFFFF";
                    dsCollreqgrt.GetElement(i, "usemem_operation").disabled = false;
                    dsCollreqgrt.GetElement(i, "usememco_amt").readOnly = false;
                    dsCollreqgrt.GetElement(i, "usememco_amt").style.background = "#FFFFFF";
                }
            }

            for (var i = 0; i < dsClearlist.GetRowCount(); i++) {
                var v = dsClearlist.GetItem(i, "finecond_type");
                if (v == "0") {
                    dsClearlist.GetElement(i, "find_percent").readOnly = true;
                    dsClearlist.GetElement(i, "find_percent").style.background = "#CCCCCC";
                    dsClearlist.GetElement(i, "fine_amt").readOnly = true;
                    dsClearlist.GetElement(i, "fine_amt").style.background = "#CCCCCC";
                    dsClearlist.GetElement(i, "fine_maxamt").readOnly = true;
                    dsClearlist.GetElement(i, "fine_maxamt").style.background = "#CCCCCC";
                }
            }

            for (var i = 0; i < dsCollcanuse.GetRowCount(); i++) {
                var v = dsCollcanuse.GetItem(i, "collmasttype_code");
                if (v == "00") {
                    dsCollcanuse.GetElement(i, "collmasttype_code").disabled = true;
                }
            }

            //"loanpayment_type") {
            if (dsPaymentdet.GetItem(0, "loanpayment_type") == "0") {
                dsPaymentdet.GetElement(0, "lastpayment_type").disabled = true;
                dsPaymentdet.SetItem(0, "lastpayment_type", "");
                dsPaymentdet.GetElement(0, "retryloansend_type").disabled = true;
                dsPaymentdet.SetItem(0, "retryloansend_type", "");
                dsPaymentdet.GetElement(0, "dropprncpay_flag").disabled = true;
                dsPaymentdet.SetItem(0, "dropprncpay_flag", 0);
                dsPaymentdet.GetElement(0, "loanpayment_count").disabled = true;
                dsPaymentdet.SetItem(0, "loanpayment_count", 0); //retryloansend_time
                dsPaymentdet.GetElement(0, "retryloansend_time").readOnly = true;
                dsPaymentdet.GetElement(0, "retryloansend_time").style.background = "#CCCCCC";
                dsPaymentdet.SetItem(0, "retryloansend_time", "");
            }

            // "retryloansend_type") {
            if (dsPaymentdet.GetItem(0, "retryloansend_type") == "0") {
                dsPaymentdet.GetElement(0, "retryloansend_time").readOnly = true;
                dsPaymentdet.GetElement(0, "retryloansend_time").style.background = "#CCCCCC";
            }
        }

        function AddNewGroup() {
            Gcoop.OpenIFrame2("520", "250", "ws_dlg_sl_addloantype.aspx", "");
        }

        function GetValueFromDlg(loantype_code) {
            dsMain.SetItem(0, "loantype_code", loantype_code);
            PostLoanTypeCode();
        }
            
    </script>
    <style type="text/css">
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        #tabs
        {
            width: 760px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //            $("#tabs").tabs();

            var tabIndex = Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
            $("#tabs").tabs({
                active: tabIndex,
                activate: function (event, ui) {
                    $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                }
            });
        });</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <span class="NewRowLink" onclick="AddNewGroup()" style="font-size: small;">เพิ่มประเภทหนี้ใหม่</span>
    <center>
        <uc1:DsMain ID="dsMain" runat="server" />
    </center>
    <br />
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">ทั่วไป</a></li>
            <li><a href="#tabs-2">วงเงินกู้</a></li>
            <li><a href="#tabs-3">ดอกเบี้ย</a></li>
            <li><a href="#tabs-4">หลักประกัน</a></li>
            <li><a href="#tabs-5">หักชำระ</a></li>
            <li><a href="#tabs-6">งวดชำระ</a></li>
            <li><a href="#tabs-7">จำกัดการกู้</a></li>
            <li><a href="#tabs-8">ประเภทสมาชิก</a></li>
            <li><a href="#tabs-9">ตรวจเงินเดือนคงเหลือ</a></li>
        </ul>
        <div id="tabs-1">
            <uc2:DsGeneral ID="dsGeneral" runat="server" />
        </div>
        <div id="tabs-2">
            <uc4:DsRightdet ID="dsRightdet" runat="server" />
            <table width="100%">
                <tr>
                    <td>
                        <strong style="font-size: small"><u>สิทธิจากหลักประกัน</u></strong>
                    </td>
                    <td align="right">
                        <span class="NewRowLink" onclick="PostInsertRowRightcollmast()" style="font-size: small;">
                            เพิ่มแถว </span>
                    </td>
                </tr>
            </table>
            <uc17:DsRightcollmast ID="dsRightcollmast" runat="server" />
            <table width="100%">
                <tr>
                    <td>
                        <strong style="font-size: small"><u>สิทธิแบบกำหนดเอง</u></strong>
                    </td>
                    <td align="right">
                        <span class="NewRowLink" onclick="PostInsertRowRightcustom()" style="font-size: small;">
                            เพิ่มแถว </span>
                    </td>
                </tr>
            </table>
            <uc18:DsRightcustom ID="dsRightcustom" runat="server" />
        </div>
        <div id="tabs-3">
            <uc3:DsIntdetail ID="dsIntdetail" runat="server" />
            <table width="100%">
                <tr>
                    <td>
                        <strong style="font-size: small"><u>ข้อกำหนดอัตราดอกเบี้ยเป็นช่วง</u></strong>
                    </td>
                    <td align="right">
                        <span class="NewRowLink" onclick="PostInsertRowIntspc()" style="font-size: small;">เพิ่มแถว
                        </span>
                    </td>
                </tr>
            </table>
            <uc19:DsIntspc ID="dsIntspc" runat="server" />
        </div>
        <div id="tabs-4">
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <uc9:DsColldet ID="dsColldet" runat="server" />
                        <table width="95%">
                            <tr>
                                <td>
                                    <strong style="font-size: small"><u>กำหนดหุ้นหรือบุคคลที่ใช้ค้ำประกัน</u></strong>
                                </td>
                                <td align="right">
                                    <span class="NewRowLink" onclick="PostInsertRowCollreqgrt()" style="font-size: small;">
                                        เพิ่มแถว </span>
                                </td>
                            </tr>
                        </table>
                        <uc10:DsCollreqgrt ID="dsCollreqgrt" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <table width="90%">
                            <tr>
                                <td>
                                    <strong style="font-size: small"><u>กำหนดมูลค่าหลักประกันที่ใช้ค้ำได้</u></strong>
                                </td>
                                <td align="right">
                                    <span class="NewRowLink" onclick="PostInsertRowCollcanuse()" style="font-size: small;">
                                        เพิ่มแถว </span>
                                </td>
                            </tr>
                        </table>
                        <uc8:DsCollcanuse ID="dsCollcanuse" runat="server" />
                    </td>
                    <td valign="top">
                        <strong style="font-size: small"><u>ประเภทเงินฝากที่ใช้ค้ำได้</u></strong>
                        <uc11:DsDpcancoll ID="dsDpcancoll" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="tabs-5">
            <uc6:DsCleardet ID="dsCleardet" runat="server" />
            <table width="100%">
                <tr>
                    <td>
                        <strong style="font-size: small"><u>เงินกู้ที่ต้องหักกลบ</u></strong>
                    </td>
                    <td align="right">
                        <span class="NewRowLink" onclick="PostInsertRowClearlist()" style="font-size: small;">
                            เพิ่มแถว </span>
                    </td>
                </tr>
            </table>
            <uc7:DsClearlist ID="dsClearlist" runat="server" />
            <table width="100%">
                <tr>
                    <td>
                        <strong style="font-size: small"><u>จำนวนหุ้นเทียบกับยอดเงินกู้</u></strong>
                    </td>
                    <td align="right">
                        <span class="NewRowLink" onclick="PostInsertRowClearbuyshr()" style="font-size: small;">
                            เพิ่มแถว </span>
                    </td>
                </tr>
            </table>
            <uc5:DsClearbuyshr ID="dsClearbuyshr" runat="server" />
        </div>
        <div id="tabs-6">
            <uc14:DsPaymentdet ID="dsPaymentdet" runat="server" />
            <table width="100%">
                <tr>
                    <td>
                        <strong style="font-size: small"><u>กำหนดงวดการส่งชำระ</u></strong>
                    </td>
                    <td align="right">
                        <span class="NewRowLink" onclick="PostInsertRowPaymentlist()" style="font-size: small;">
                            เพิ่มแถว </span>
                    </td>
                </tr>
            </table>
            <uc15:DsPaymentlist ID="dsPaymentlist" runat="server" />
        </div>
        <div id="tabs-7">
            <table width="100%">
                <tr>
                    <td>
                        <strong style="font-size: small"><u>รายการเงินกู้ที่ห้ามกู้</u></strong>
                    </td>
                    <td align="right">
                        <span class="NewRowLink" onclick="PostInsertRowDropln()" style="font-size: small;">เพิ่มแถว
                        </span>
                    </td>
                </tr>
            </table>
            <uc12:DsDropln ID="dsDropln" runat="server" />
            <table width="100%">
                <tr>
                    <td>
                        <strong style="font-size: small"><u>รายการเงินกู้ที่ลดสิทธิ์ลง</u></strong>
                    </td>
                    <td align="right">
                        <span class="NewRowLink" onclick="PostInsertRowPermdown()" style="font-size: small;">
                            เพิ่มแถว </span>
                    </td>
                </tr>
            </table>
            <uc16:DsPermdown ID="dsPermdown" runat="server" />
        </div>
        <div id="tabs-8">
            <strong style="font-size: small"><u>ประเภทสมาชิกที่กู้ได้</u></strong>
            <uc13:DsMbsubgrp ID="dsMbsubgrp" runat="server" />
        </div>
        <div id="tabs-9">
            <strong style="font-size: small"><u>การตรวจสอบเงินเดือนคงเหลือ</u></strong>
            <uc20:DsSalbal ID="dsSalbal" runat="server" />
            <uc21:DsCmSalbal ID="dsCmSalbal" runat="server" Visible="false" />
            <table id="LntypeSalbal" width="100%" runat="server">
                <tr>
                    <td>
                        <strong style="font-size: small"><u>ข้อกำหนดการตรวจสอบเงินเดือนคงเหลือเป็นช่วง</u></strong>
                    </td>
                    <td align="right">
                        <span class="NewRowLink" onclick="PostInsertRowLnSalbal()" style="font-size: small;">เพิ่มแถว
                        </span>
                    </td>
                </tr>
            </table>
            <uc22:DsLoantypeSalbal ID="dsLoantypeSalbal" runat="server" />
        </div>
    </div>
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
</asp:Content>

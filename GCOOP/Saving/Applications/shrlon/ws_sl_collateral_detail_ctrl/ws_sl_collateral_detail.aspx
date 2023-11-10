<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_collateral_detail.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_detail_ctrl.ws_sl_collateral_detail" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsLand.ascx" TagName="DsLand" TagPrefix="uc2" %>
<%@ Register Src="DsBding.ascx" TagName="DsBding" TagPrefix="uc3" %>
<%@ Register Src="DsCondo.ascx" TagName="DsCondo" TagPrefix="uc4" %>
<%@ Register Src="DsLanduse.ascx" TagName="DsLanduse" TagPrefix="uc5" %>
<%@ Register Src="DsBdingdet.ascx" TagName="DsBdingdet" TagPrefix="uc6" %>
<%@ Register Src="DsCollprop.ascx" TagName="DsCollprop" TagPrefix="uc7" %>
<%@ Register Src="DsRate.ascx" TagName="DsRate" TagPrefix="uc8" %>
<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsLanduse = new DataSourceTool;
        var dsLand = new DataSourceTool;
        var dsCondo = new DataSourceTool;
        var dsBdingdet = new DataSourceTool;
        var dsBding = new DataSourceTool;
        var dsCollprop = new DataSourceTool;
        var dsRate = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function MenubarOpen() {
            Gcoop.OpenIFrame3("730", "550", "ws_dlg_sl_editcollateral_master.aspx", "");
        }
        function GetItem(member_no, collmast_no) {
            Gcoop.RemoveIFrame();
            dsMain.SetItem(0, "member_no", member_no);
            dsMain.SetItem(0, "collmast_no", collmast_no);
            PostCollmastNo();
        }
        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_memsearch") {
                Gcoop.OpenDlg("630", "720", "w_dlg_member_search.aspx", "")
            }

        }
        //แป้ง เพิ่มฟังค์ชัน OnDsListClicked
        function OnDsListClicked(s, r, c) {
            if (c == "collmast_no" || c == "cp_colltype" || c == "est_price") {
                var collmastno = dsList.GetItem(r, "collmast_no");
                var collmasttype_grp = dsList.GetItem(r, "collmasttype_grp");
                dsMain.SetItem(0, "collmast_no", collmastno);
                dsMain.SetItem(0, "collmasttype_grp", collmasttype_grp);
                PostCollmastNo();
            }
        }

        function GetMemDetFromDlg(memberno, prename_desc, memb_name, memb_surname, card_person) {

            dsMain.SetItem(0, "member_no", memberno);
            // objdw_main.AcceptText();
            // Gcoop.GetEl("Hdfmember_no").value = memberno;
            PostMemberNo();

        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            } else if (c == "collmasttype_grp") {
                PostPanel();
            }
        }

        function OnDsLanduseClicked(s, r, c, v) { }

        function OnDsLanduseItemChanged(s, r, c, v) { }

        function OnDsLandClicked(s, r, c, v) { }

        function OnDsLandItemChanged(s, r, c, v) {
            if (c == "size_rai" || c == "size_ngan" || c == "size_wa" || c == "priceper_unit") {
                var rai = dsLand.GetItem(0, "size_rai");
                var ngan = dsLand.GetItem(0, "size_ngan");
                var wa = dsLand.GetItem(0, "size_wa");

                var area_amt = 0;
                area_amt += (rai * 400);
                area_amt += (ngan * 100);
                area_amt += (wa * 1);

                var dol_prince = area_amt * dsLand.GetItem(0, "priceper_unit");
                dsRate.SetItem(0, "dol_prince", dol_prince);
                OnDsRateItemChanged('', 0, "dol_prince", dol_prince);
            }
        }

        function OnDsCondoClicked(s, r, c, v) { }

        function OnDsCondoItemChanged(s, r, c, v) {
            if (c == "condo_roomsize" || c == "condo_pricesquare" || c == "condo_age" || c == "condo_depreciation") {
                //condo_pricesquare
                var condo_roomsize = dsCondo.GetItem(0, "condo_roomsize");
                var condo_pricesquare = dsCondo.GetItem(0, "condo_pricesquare");
                var prince_sum = condo_roomsize * condo_pricesquare;
                var condo_age = dsCondo.GetItem(0, "condo_age");
                var condo_depreciation = dsCondo.GetItem(0, "condo_depreciation");
                var depreciation = condo_age * condo_depreciation;

                var dol_prince = prince_sum - ((prince_sum * depreciation) / 100);
                dsRate.SetItem(0, "dol_prince", dol_prince);
                OnDsRateItemChanged('', 0, "dol_prince", dol_prince);
            }

        }

        function OnDsBdingdetClicked(s, r, c, v) {
            if (c == "b_del") {
                dsBdingdet.SetRowFocus(r);
                PostDeleteRowBdingdet();
            }
        }

        function OnDsBdingdetItemChanged(s, r, c, v) {
            if (c == "floor_pricesquare" || c == "floor_square") {
                var floor_pricesquare = dsBdingdet.GetItem(r, "floor_pricesquare");
                var floor_square = dsBdingdet.GetItem(r, "floor_square");
                var floor_sumprice = floor_pricesquare * floor_square;
                dsBdingdet.SetItem(r, "floor_sumprice", floor_sumprice);

                var bd_sumprice = 0;
                for (var i = 0; i < dsBdingdet.GetRowCount(); i++) {
                    bd_sumprice += dsBdingdet.GetItem(i, "floor_sumprice");
                }

                var bd_age = dsBding.GetItem(0, "bd_age");
                var bd_depreciation = dsBding.GetItem(0, "bd_depreciation");
                var depreciation = bd_age * bd_depreciation;

                var dol_prince = bd_sumprice - ((bd_sumprice * depreciation) / 100);

                dsBding.SetItem(0, "bd_sumprice", bd_sumprice);
                dsRate.SetItem(0, "dol_prince", dol_prince);
                OnDsRateItemChanged('', 0, "dol_prince", dol_prince);
            }
        }

        function OnDsBdingClicked(s, r, c, v) { }

        function OnDsBdingItemChanged(s, r, c, v) {
            if (c == "bd_depreciation" || c == "bd_age") {
                var bd_sumprice = 0;
                for (var i = 0; i < dsBdingdet.GetRowCount(); i++) {
                    bd_sumprice += dsBdingdet.GetItem(i, "floor_sumprice");
                }
                var bd_age = dsBding.GetItem(0, "bd_age");
                var bd_depreciation = dsBding.GetItem(0, "bd_depreciation");
                var depreciation = bd_age * bd_depreciation;

                var dol_prince = bd_sumprice - ((bd_sumprice * depreciation) / 100);

                dsBding.SetItem(0, "bd_sumprice", bd_sumprice);
                dsRate.SetItem(0, "dol_prince", dol_prince);
                OnDsRateItemChanged('', 0, "dol_prince", dol_prince);
            }

        }

        function OnDsCollpropClicked(s, r, c, v) {
            if (c == "b_del") {
                dsCollprop.SetRowFocus(r);
                PostDeleteRowCollprop();
            }
        }

        function OnDsCollpropItemChanged(s, r, c, v) { }

        function OnDsRateClicked(s, r, c, v) { }

        function OnDsRateItemChanged(s, r, c, v) {
            if (c == "est_percent" || c == "dol_prince") {
                var percent = dsRate.GetItem(0, "est_percent");
                var dol_prince = dsRate.GetItem(0, "dol_prince");
                var est_price = (percent * dol_prince) / 100;
                dsRate.SetItem(0, "est_price", est_price);
            }
        }

        $(function () {
            var tabIndex = Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
            $("#tabs").tabs({
                active: tabIndex,
                activate: function (event, ui) {
                    $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                }
            });
        });
    </script>
    <style type="text/css">
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        #tabs
        {
            width: 750px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc9:DsList ID="dsList" runat="server" />
    <br />
    <span style="font-size: small"><strong><u>รายละเอียด</u></strong></span>
    <asp:Panel ID="Panel1" runat="server">
        <uc2:DsLand ID="dsLand" runat="server" />
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
        <uc3:DsBding ID="dsBding" runat="server" />
        
        <uc6:DsBdingdet ID="dsBdingdet" runat="server" />
    </asp:Panel>
    <asp:Panel ID="Panel3" runat="server">
        <uc4:DsCondo ID="dsCondo" runat="server" />
    </asp:Panel>
    <asp:Panel ID="Panel4" runat="server">
        <br />
        <span style="font-size: small"><strong><u>สภาพที่ดิน</u></strong></span>
        <uc5:DsLanduse ID="dsLanduse" runat="server" />
    </asp:Panel>
    <br />
    <hr />
    <table width="100%">
        <tr>
            <td valign="top" align="left">
                <asp:Panel ID="Panel5" runat="server">
                    <br />
                    
                    <uc7:DsCollprop ID="dsCollprop" runat="server" />
                </asp:Panel>
            </td>
            <td valign="top" align="right">
                <uc8:DsRate ID="dsRate" runat="server" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
</asp:Content>

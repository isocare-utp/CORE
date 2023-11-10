<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_mg_mrtgmast_detail.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl.ws_sl_mg_mrtgmast_detail" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<%@ Register Src="DsDetailDeed.ascx" TagName="DsDetailDeed" TagPrefix="uc3" %>
<%@ Register Src="DsDetailNs3.ascx" TagName="DsDetailNs3" TagPrefix="uc4" %>
<%@ Register Src="DsDetailCondo.ascx" TagName="DsDetailCondo" TagPrefix="uc5" %>
<%@ Register Src="DsMrtger.ascx" TagName="DsMrtger" TagPrefix="uc6" %>
<%@ Register Src="DsAutz.ascx" TagName="DsAutz" TagPrefix="uc7" %>
<%@ Register Src="DsRefcollno.ascx" TagName="DsRefcollno" TagPrefix="uc8" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc9" %>
<%@ Register Src="DsUpgrade.ascx" TagName="DsUpgrade" TagPrefix="uc10" %>
<%@ Register Src="DsDetailBding.ascx" TagName="DsDetailBding" TagPrefix="uc11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_search") {
                Gcoop.OpenDlg("630", "720", "w_dlg_member_search.aspx", "")
            }
        }

        function GetMemDetFromDlg(memberno, prename_desc, memb_name, memb_surname, card_person) {

            dsMain.SetItem(0, "member_no", memberno);
            PostMemberNo();
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        }
        function Setfocus() {
            dsMain.Focus(0, "member_no");
        }

        function OnDsListClicked(s, r, c, v) {
            if (c == "mrtgmast_no" || c == "assettype_desc" || c == "cp_mrtgtype") {
                PostMrtgNo();
            }
        }

        function OnDsDetailItemChanged(s, r, c, v) {

        }

        function SheetLoadComplete() {
            $('.ui-tabs-panel').find('input,select,button').attr('readOnly', true);
        }

        $(function () {
            $("#tabs").tabs();
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
            width: 740px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table width="720px;">
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="พิมพ์จำนอง" OnClick="PostPrint" Style="margin-left: 510px;
                    width: 100px" />
                <asp:Button ID="Button2" runat="server" Text="พิมพ์การขึ้นเงิน" OnClick="PostPrintUpg"
                    Style="margin-left: 2px; width: 100px" />
                <%--<input type="button" value="พิมพ์จำนอง" id="btnPrint" onclick="PostPrint()" style="margin-left: 550px;" />--%>
                <%--<input type="button" value="พิมพ์การขึ้นเงิน" id="btnPrintUpg" onclick="PostPrintUpg()"
                    style="margin-left: 2px;" />--%>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:DsMain ID="dsMain" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top" width="30%">
                <uc9:DsList ID="dsList" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="70%">
                <div id="tabs">
                    <ul>
                        <li><a href="#tabs-1">รายละเอียดจำนอง</a></li>
                        <li><a href="#tabs-2">ผู้จำนอง </a></li>
                        <li><a href="#tabs-3">ผู้รับมอบอำนาจ</a></li>
                        <li><a href="#tabs-4">อ้างอิงหลักทรัพย์</a></li>
                        <li><a href="#tabs-5">การขึ้นเงิน</a></li>
                    </ul>
                    <div id="tabs-1">
                        <div align="center">
                            <uc2:DsDetail ID="dsDetail" runat="server" />
                            <uc3:DsDetailDeed ID="dsDetailDeed" runat="server" />
                            <uc4:DsDetailNs3 ID="dsDetailNs3" runat="server" />
                            <uc5:DsDetailCondo ID="dsDetailCondo" runat="server" />
                            <uc11:DsDetailBding ID="dsDetailBding" runat="server" />
                            <br />
                        </div>
                    </div>
                    <div id="tabs-2">
                        <div align="center">
                            <uc6:DsMrtger ID="dsMrtger" runat="server" />
                        </div>
                    </div>
                    <div id="tabs-3">
                        <div align="center">
                            <uc7:DsAutz ID="dsAutz" runat="server" />
                        </div>
                    </div>
                    <div id="tabs-4">
                        <div align="center">
                            <uc8:DsRefcollno ID="dsRefcollno" runat="server" />
                            <br />
                        </div>
                    </div>
                    <div id="tabs-5">
                        <div align="center">
                            <uc10:DsUpgrade ID="dsUpgrade" runat="server" />
                            <br />
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

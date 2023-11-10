<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_conteck_lite.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_conteck_lite" %>

<%@ Register Src="w_sheet_dp_conteck_lite_ctrl/DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="w_sheet_dp_conteck_lite_ctrl/DsDetail.ascx" TagName="DsDetail"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsDetail = new DataSourceTool();

        function OnDsMainItemChange(s, r, c, v) {
            if (c == "deptaccount_no") {
                postAccountNo();
            }
        }

        function OnDsMainClick(s, r, c) {
        }

        function OnDsDetailItemChange(s, r, c, v) {
        }

        function OnDsDetailClick(s, r, c) {
        }

        function Validate() {
            return true;
        }

        function PrintNewBook() {
            var deptAccountNo = dsMain.GetItem(0, "deptaccount_no");
            var deptPassBookNo = dsMain.GetItem(0, "deptpassbook_no");
            Gcoop.OpenIFrame(450, 110, "w_iframe_dp_printfirstpage.aspx", "?deptAccountNo=" + deptAccountNo + "&deptPassBookNo=" + deptPassBookNo);
        }

        function PrintBook() {
            var deptAccountNo = dsMain.GetItem(0, "deptaccount_no");
            if (deptAccountNo != "") {
                Gcoop.OpenIFrame(900, 240, "w_dlg_dp_printbook.aspx", "?deptAccountNo=" + deptAccountNo);
            }
        }

        function PrintBookReload() {
            postAccountNo();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <input id="ShifF5" type="button" value="พิมพ์ปกสมุด" onclick="PrintNewBook()" style="height: 40px;
        width: 100px;" />
    <input id="ShifF7" type="button" value="พิมพ์ BOOK" onclick="PrintBook()" style="height: 40px;
        width: 100px;" />
    <input id="ShifF11" type="button" value="ออกสมุดใหม่" onclick="DialBookNew()" style="height: 40px;
        width: 220px; visibility: hidden;" />
    <br />
    <uc2:DsDetail ID="dsDetail" runat="server" />
</asp:Content>

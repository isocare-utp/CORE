<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_regicancel_pborcert.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_regicancel_pborcert" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=newRecord%>
    <%=ItemChangedJS%>
    <%=GetBeginBkNo%>
    <%=FilterBookType%>
    <style type="text/css">
        .style3
        {
            width: 198px;
        }
        .style4
        {
            width: 47px;
        }
    </style>
    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                newRecord();
            }
        }

        function ItemChanged(s, r, c, v) {
            if (c == "as_coopid") {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                Gcoop.GetEl("HfCoopid").value = v + "";
            }
            else if (c == "as_pbstart" || c == "as_pbend" || c == "an_amt") {
                objDwMain.SetItem(1, c, v);
                objDwMain.AcceptText();
                Gcoop.GetEl("Hcol").value = c;
                ItemChangedJS();
            } else if (c == "as_bookgrp") {
                objDwMain.SetItem(1, c, v);
                objDwMain.AcceptText();
                GetBeginBkNo();

            } else if (c == "as_booktype") {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                FilterBookType();
            }

            return 0;
        }

        function CnvNumber(num) {
            if (IsNum(num)) {
                return parseFloat(num);
            }
            return 0;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                &nbsp; &nbsp; &nbsp;
                <asp:Label ID="Label1" runat="server" Text="ข้อมูลการทำรายการ" Font-Bold="True" Font-Names="tahoma"
                    Font-Size="12px"></asp:Label>
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dp_bookregis"
                    LibraryList="~/DataWindow/ap_deposit/dp_regicancel_pborcert.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientEventItemChanged="ItemChanged">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hcol" runat="server" />
    <asp:HiddenField ID="HfCoopid" runat="server" />
</asp:Content>

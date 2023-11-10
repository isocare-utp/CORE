﻿<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_edit_atm.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_edit_atm" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=jsRetive%>
    <%=jsRetiveDwDetail%>
    <script type="text/javascript">

        function Validate() {
            return confirm("ต้องการบันทึก หรือไม่?");
        }

         function DwMainItemChanged(s, r, c, v) {
             switch (c) {
                 case "atmmember_member_no":
                     s.SetItem(r, c, v);
                     s.AcceptText();
                     jsRetive();
                     break;

                 case "deptaccount_no":
                     s.SetItem(r, c, v);
                     s.AcceptText();
                     jsRetiveDwDetail();
                     break;
             }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_member_detail"
        LibraryList="~/DataWindow/ap_deposit/dp_atm_edit.pbl" AutoSaveDataCacheAfterRetrieve="True"
        AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
        ClientEventItemChanged="DwMainItemChanged" ClientEventButtonClicked="DwMainButtonClicked">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_edit_atmdept"
        LibraryList="~/DataWindow/ap_deposit/dp_atm_edit.pbl" AutoSaveDataCacheAfterRetrieve="True"
        AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
        ClientEventItemChanged="DwDetailItemChanged" ClientEventButtonClicked="DwDetailButtonClicked">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdRowPass" runat="server" Value="" />
    <asp:HiddenField ID="HdOperate_Date" runat="server" Value="" />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_member_fund.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_member_fund" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=changeGetMember%>
    <script type="text/javascript">

        function ItemDwMainChanged(sender, rowNumber, columnName, newValue) {
            Gcoop.GetEl("hdMemb_no").value = newValue;
            changeGetMember();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table width="100%">
        <tr>
            <td colspan="4">
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_mutdetail_master"
                    LibraryList="~/DataWindow/Shrlon/sl_member_fund.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="OnDwMainClicked" ClientEventItemChanged="ItemDwMainChanged"
                    TabIndex="600"  >
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <dw:WebDataWindowControl ID="dw_statelist" runat="server" DataWindowObject="d_mut_statement"
                    LibraryList="~/DataWindow/Shrlon/sl_member_fund.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="OnDwStateListClicked" ClientEventItemChanged="ItemDwStateListChanged"
                    TabIndex="600"  >
                </dw:WebDataWindowControl>
            </td>
            </tr>
            <tr>
            <td>
                <br />
                <dw:WebDataWindowControl ID="dw_gaindetail" runat="server" DataWindowObject="d_mut_gaindetail"
                    LibraryList="~/DataWindow/Shrlon/sl_member_fund.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="OnDwStateListClicked" ClientEventItemChanged="ItemDwStateListChanged"
                    TabIndex="300"  >
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdMemb_no" runat="server" />
</asp:Content>



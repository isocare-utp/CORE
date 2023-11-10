<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_addnewtype.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_addnewtype" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postDepttypeCode%>
    <%=postdeptstatus%>>
    <script type="text/javascript">
        function OnDwMainItemChanged(sender, rowNumber, columnName, newValue){
            if(columnName == "depttype_group"){
                objDwMain.SetItem(rowNumber,columnName,newValue);
                objDwMain.AcceptText();
                postDepttypeCode();

            }
            if (columnName == "depttype_status") {
                Gcoop.CheckDw(sender, rowNumber, columnName, "depttype_status", 1, 0);
                sender.SetItem(rowNumber, columnName, newValue);
                sender.AcceptText();
                postdeptstatus();
            }
        }
        function Validate()
        {
            return confirm("ต้องการเพิ่มข้อมูลประเภทเงินฝาก ใช่หรือไม่?");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dpdepttype_add"
        LibraryList="~/DataWindow/ap_deposit/dp_addnewtype.pbl" ClientEventItemChanged="OnDwMainItemChanged"
        ClientFormatting="True">
    </dw:WebDataWindowControl>
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="400px" 
        Width="600px">
        <dw:WebDataWindowControl ID="DwList" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_depttype_listadd"
            LibraryList="~/DataWindow/ap_deposit/dp_addnewtype.pbl" ClientFormatting="True" Height="400px">
        </dw:WebDataWindowControl>
    </asp:Panel>
</asp:Content>

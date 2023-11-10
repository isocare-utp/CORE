<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_enquiry_pborcert.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_enquiry_pborcert" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=FilterBookType%>
    <%=postBookGrp%>
    <%=postBookNoDetail%>
    <%=SearchBook%>
    <%=CheckCoop %>

    <script type="text/javascript">
        
        function MenubarNew()
        {
            window.location = Gcoop.GetUrl() + "Applications/ap_deposit/w_sheet_dp_enquiry_pborcert.aspx";
        }
        
        function OnDwMasterItemChange(sender, rowNumber, columnName, newValue)
        {
            if(columnName == "book_type"){
                objdw_master.SetItem(rowNumber, columnName, newValue);
                objdw_master.AcceptText();
                FilterBookType();
            }
            else if(columnName == "book_grp"){
                var bookGrp = objdw_master.GetItem(rowNumber, columnName);
                objdw_master.SetItem(rowNumber, columnName, newValue);
                objdw_master.AcceptText();
                postBookGrp();
            }
        }
        
        function OndwlistClicked(sender, rowNumber, objectName){
            var bookNo = objdw_dplist.GetItem(rowNumber,"book_no");
            Gcoop.GetEl("HdBookNo").value = bookNo;
            postBookNoDetail();           
        }
        
        function OnDwGoToButtonClicked(sender, rowNumber, buttonName){
            if(buttonName == "b_search"){
                SearchBook();
            }
        }

        function OnDwListCoopClick(s, r, c) {
            if (c == "cross_coopflag") {
                Gcoop.CheckDw(s, r, c, "cross_coopflag", 1, 0);
                CheckCoop();
            }
        }
        function OnDwListCoopItemChanged(s, r, c, v) {
            if (c == "dddwcoopname") {
                s.SetItem(r, c, v);
                s.AcceptText();
                var coopid = s.GetItem(r, "dddwcoopname");
                Gcoop.GetEl("HfCoopid").value = coopid + "";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="DwListCoop" runat="server" DataWindowObject="d_dp_dept_cooplist"
            LibraryList="~/DataWindow/ap_deposit/cm_constant_config.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            ClientEventItemChanged="OnDwListCoopItemChanged" ClientFormatting="True" TabIndex="1"
            ClientEventClicked="OnDwListCoopClick">
        </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="dw_master" runat="server" DataWindowObject="d_dp_bookenq_mas"
        LibraryList="~/DataWindow/ap_deposit/dp_enquiry_pborcert.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventItemChanged="OnDwMasterItemChange">
    </dw:WebDataWindowControl>
    <table>
        <tr>
            <td style="width: 230px;" valign="top">
                <asp:Panel ID="Panel1" runat="server" Width="240px" Height="405px" ScrollBars="Auto">
                    <dw:WebDataWindowControl ID="dw_dplist" runat="server" DataWindowObject="d_dp_booklist"
                        LibraryList="~/DataWindow/ap_deposit/dp_enquiry_pborcert.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventClicked="OndwlistClicked" Height="405px">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td valign="top">
                <dw:WebDataWindowControl ID="dw_dpdetail" runat="server" DataWindowObject="d_dp_booklist_detail"
                    LibraryList="~/DataWindow/ap_deposit/dp_enquiry_pborcert.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <dw:WebDataWindowControl ID="dw_goto" runat="server" DataWindowObject="d_dp_search"
        LibraryList="~/DataWindow/ap_deposit/dp_enquiry_pborcert.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventButtonClicked="OnDwGoToButtonClicked">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdBookNo" runat="server" />
    <asp:HiddenField ID="HfCoopid" runat="server" />
</asp:Content>

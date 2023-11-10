<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_divsrv_opr_slip_ccl.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_opr_slip_ccl" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postNewClear%>
    <%=postInit %>
    <%=postInitMemberNo %>

    <script type="text/JavaScript">
        //Function Main
        //=================================================================
        function GetMemberNoFromDialog(member_no) {
            Gcoop.GetEl("Hdmember_no").value = member_no;
            postInitMemberNo();
        }

        function OnDwMainButtonClick(s, r, b) {
            if (b == "b_search_memno") {
                Gcoop.OpenIFrame("800", "500", "w_dlg_divsrv_search_mem.aspx", "");
            }
        }

        function OnDwMainItemChange(s, r, c, v) {
            if (c == "member_no") {
                objDw_main.SetItem(1, "member_no", v)
                objDw_main.AcceptText();
                postInit();
            }
        }
        //Function List
        //=================================================================
        function OnDwListClick(s, r, c) {
            if (c == "operate_flag") {
                Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
            }
        }
        //Function Default
        //=================================================================
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function MenubarOpen() {
            
        }
        function MenubarNew() {
            postNewClear();
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width:100%;">
            <tr>
                <td colspan="2">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                        ClientFormatting="True" ClientScriptable="True" 
                        DataWindowObject="d_divsrv_opr_ccl_main" 
                        LibraryList="~/DataWindow/divavg/divsrv_opr_slip_ccl.pbl" 
                        ClientEventItemChanged="OnDwMainItemChange" 
                        ClientEventButtonClicked="OnDwMainButtonClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="2">
                    </td>
            </tr>
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="Dw_list" runat="server" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
                        ClientScriptable="True" DataWindowObject="d_divsrv_opr_ccl_list" 
                        LibraryList="~/DataWindow/divavg/divsrv_opr_slip_ccl.pbl" 
                        ClientEventClicked="OnDwListClick">
                    </dw:WebDataWindowControl>
                </td>
                <td>
                    <dw:WebDataWindowControl ID="Dw_detail" runat="server" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
                        ClientScriptable="True" DataWindowObject="d_divsrv_opr_ccl_detail" 
                        LibraryList="~/DataWindow/divavg/divsrv_opr_slip_ccl.pbl">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
        </table>
        <asp:HiddenField ID="Hdmember_no" runat="server" />
          <asp:HiddenField ID="Hddiv_year" runat="server" />
          
        
        <br />
    </p>
</asp:Content>

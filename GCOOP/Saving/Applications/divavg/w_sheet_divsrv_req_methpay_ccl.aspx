<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_divsrv_req_methpay_ccl.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_req_methpay_ccl" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postNewClear%>
    <%=postInit%>
    <%=postInitMemberNo%>
    <script type="text/JavaScript">
        //Function Main
        //=================================================================
        function OnDwListClick(s, r, c) {
            if (c == "operate_flag") {
                Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
            }
        }

        function GetMemberNoFromDialog(member_no) {
            Gcoop.GetEl("Hdmember_no").value = member_no;
            postInitMemberNo();
        }

        function OnDwMainButtonClick(s, r, b) 
        {
            if (b == "b_search_memno") 
            {
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
        //Function Detail
        //=================================================================

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        DataWindowObject="d_divsrv_req_methpay_ccl_main" LibraryList="~/DataWindow/divavg/divsrv_req_methpay_ccl.pbl"
                        ClientEventItemChanged="OnDwMainItemChange" ClientEventButtonClicked="OnDwMainButtonClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel2" runat="server" Width="290px">
                        <dw:WebDataWindowControl ID="Dw_list" runat="server" 
    AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" 
    ClientFormatting="True" ClientScriptable="True"
                        DataWindowObject="d_divsrv_req_methpay_ccl_list" LibraryList="~/DataWindow/divavg/divsrv_req_methpay_ccl.pbl" ClientEventClicked="OnDwListClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Auto" 
                        Width="450px">
                        <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" DataWindowObject="d_divsrv_req_methpay_ccl_detail" LibraryList="~/DataWindow/divavg/divsrv_req_methpay_ccl.pbl"
                            ClientEventItemChanged="OnDwMainItemChange" ClientEventButtonClicked="OnDwMainButtonClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:HiddenField ID="Hdmember_no" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
</asp:Content>

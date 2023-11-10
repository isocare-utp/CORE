<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_divsrv_req_chg_refrain.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_req_chg_refrain" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postInitMemberNo%>
    <%=postNewClear%>
    <%=postInit%>
    <%=postRefresh%>
    
    <script type="text/JavaScript">
        //Function Main
        //=================================================================
        function OnDwDetailClick(s, r, c) {
            if (c == "operate_flag") {
                Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
                postRefresh();
            }
            else if (c == "new_flag") {
                Gcoop.CheckDw(s, r, c, "new_flag", 1, 0);
                postRefresh();
            }
           
        }
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
                        DataWindowObject="d_divsrv_req_chg_main" LibraryList="~/DataWindow/divavg/divsrv_req_chg_refrain.pbl"
                        ClientEventItemChanged="OnDwMainItemChange" 
                        ClientEventButtonClicked="OnDwMainButtonClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="top" colspan="2">
                    <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Auto" 
                        Width="700px">
                        <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" 
    AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" 
    DataWindowObject="d_divsrv_req_chg_det_refrain" LibraryList="~/DataWindow/divavg/divsrv_req_chg_refrain.pbl" ClientEventClicked="OnDwDetailClick">

                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<asp:HiddenField ID="Hdmember_no" runat="server" />
                    <asp:HiddenField ID="Hddiv_year" runat="server" />
                    
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
</asp:Content>



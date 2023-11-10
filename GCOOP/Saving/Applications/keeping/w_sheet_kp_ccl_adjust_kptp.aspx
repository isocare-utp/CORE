<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_kp_ccl_adjust_kptp.aspx.cs"
    Inherits="Saving.Applications.keeping.w_sheet_kp_ccl_adjust_kptp" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postInit%>
    <%=postInitMember%>
    <script type="text/javascript">
        function GetValueFromDlg(memberno) {
            objDw_main.SetItem(1, "member_no", memberno);
            objDw_main.AcceptText();
            Gcoop.GetEl("Hd_memberno").value = memberno;
            postInitMember();
        }


        function OnButtonClick(s, r, b) {
            if (b == "b_search_memno") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_sl_member_search.aspx', '');
            }
        }
        function Validate() {
            objDw_main.AcceptText();
            objDw_list.AcceptText();
            objDw_detmain.AcceptText();
            objDw_detdetail.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }


        function OnDwMainItemChange(s, r, c, v) {
            if (c == "member_no") {
                objDw_main.SetItem(1, "member_no", v);
                objDw_main.AcceptText();
                postInit();
            }
        }

        function SheetLoadComplete() {

            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
        }

        
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <asp:Panel ID="Panel1" runat="server" Height="150px">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_kp_adjust_ccl_kptp_main"
                        LibraryList="~/DataWindow/keeping/kp_ccl_adjust_kptp.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="1" Style="top: 0px; left: 0px" ClientEventItemChanged="OnDwMainItemChange"
                        ClientEventButtonClicked="OnButtonClick">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Panel ID="Panel4" runat="server" Height="100px" Width="200px">
                    <dw:WebDataWindowControl ID="Dw_list" runat="server" DataWindowObject="d_kp_adjust_ccl_kptp_list"
                        LibraryList="~/DataWindow/keeping/kp_ccl_adjust_kptp.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="1" Style="top: 0px; left: 0px">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td colspan="2" valign="top">
                <asp:Panel ID="Panel2" runat="server" Height="200px" Width="550px" ScrollBars="Auto">
                    <dw:WebDataWindowControl ID="Dw_detmain" runat="server" DataWindowObject="d_kp_adjust_ccl_kptp_det_main"
                        LibraryList="~/DataWindow/keeping/kp_ccl_adjust_kptp.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="1" Style="top: 0px; left: 0px">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <asp:CheckBox ID="CheckAll" runat="server" AutoPostBack="True" Font-Size="Small"
                    ForeColor="#0000CC" Text="เลือกทั้งหมด" OnCheckedChanged="CheckAll_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <asp:Panel ID="Panel3" runat="server" Height="200px" ScrollBars="Auto" Width="550px">
                    <dw:WebDataWindowControl ID="Dw_detdetail" runat="server" DataWindowObject="d_kp_adjust_ccl_kptp_det_detail"
                        LibraryList="~/DataWindow/keeping/kp_ccl_adjust_kptp.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="1" Style="top: 0px; left: 0px">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:HiddenField ID="HdIsPostBack" runat="server" />
                <asp:HiddenField ID="Hd_memberno" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

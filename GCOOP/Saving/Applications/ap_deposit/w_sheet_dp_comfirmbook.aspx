<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_comfirmbook.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_comfirmbook" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=PostChkMember%>
    <%=PostData%>
    <script type="text/javascript">

        function OnDwMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                PostChkMember();
            } else if (c == "member_no") {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                PostChkMember();
            }
        }

        function OnDwMainClicked(s, r, c) {
            if (c == "b_memsearch") {
                Gcoop.OpenGlobalIFrame(580, 600, "w_dlg_mb_member_search_lite.aspx", "");
            } else if (c == "b_acc") {
                Gcoop.OpenIFrame(900, 600, "w_dlg_dp_account_search.aspx", "");
            }
        }

        function GetValueSearchMemberNo(member_no) {
            objDwMain.SetItem(1, "member_no", Gcoop.Trim(member_no));
            objDwMain.AcceptText();
        }

        function NewAccountNo(coopid, accNo) {
            objDwMain.SetItem(1, "acc_list", Gcoop.Trim(accNo));
            objDwMain.AcceptText();
            Gcoop.RemoveIFrame();
        }

        function OnClickConfirmbook() {
            PostData();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" TabIndex="1">
                    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_head_cfmbook"
                        LibraryList="~/DataWindow/ap_deposit/deposit.pbl" ClientEventItemChanged="OnDwMainItemChanged"
                        ClientEventClicked="OnDwMainClicked" ClientEventItemError="OnError" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <div align="left">
                    <input type="button" value="ออกรายงาน" style="width: 60px" onclick="OnClickConfirmbook()" />
                </div>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Panel ID="Panel2" runat="server" TabIndex="1">
                    <dw:WebDataWindowControl ID="DwReport" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_cfmbook_report_th"
                        LibraryList="~/DataWindow/ap_deposit/deposit.pbl" ClientEventItemChanged="OnDwMainItemChanged"
                        ClientEventClicked="OnDwMainClicked" ClientEventItemError="OnError" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    
</asp:Content>

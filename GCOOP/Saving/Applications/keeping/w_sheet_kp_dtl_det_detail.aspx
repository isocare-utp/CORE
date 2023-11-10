<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
CodeBehind="w_sheet_kp_dtl_det_detail.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_dtl_det_detail" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postNewClear%>
    <%=postInit%>
    <%=postInitMember%>

    <script type="text/javascript">
        function OnDwMainItemChange(s, r, c, v) {
            if (c == "member_no") {
                objDw_main.SetItem(1, "member_no", v);
                objDw_main.AcceptText();
                postInit();
            }
        }

        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            postNewClear();
        }


        function GetValueFromDlg(memberno) {
            objDw_main.SetItem(1, "member_no", memberno);
            objDw_main.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memberno;
            postInitMember();
        }

        function OnDwMainButtonClick(s, r, b) {
            if (b == "b_search_memno") {
                Gcoop.OpenIFrame('580', '590', 'w_dlg_sl_member_search.aspx', '');
            }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_kp_det_main"
        LibraryList="~/DataWindow/keeping/kp_dtl_det_detail.pbl" ClientScriptable="True"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" TabIndex="1" ClientEventItemChanged="OnDwMainItemChange"
        ClientEventButtonClicked="OnDwMainButtonClick">
    </dw:WebDataWindowControl>
    <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Auto" Width="750px">
        <dw:WebDataWindowControl ID="Dw_detail" runat="server" DataWindowObject="d_kp_det_detail"
            LibraryList="~/DataWindow/keeping/kp_dtl_det_detail.pbl" ClientScriptable="True"
            AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
            ClientFormatting="True" TabIndex="500">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <br />
    <br />
</asp:Content>

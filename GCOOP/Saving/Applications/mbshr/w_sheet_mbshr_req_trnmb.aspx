<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mbshr_req_trnmb.aspx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mbshr_req_trnmb" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postNewClear%>
    <%=postInitMember%>
    <%=postSetMemberNo%>
    <script type="text/javascript">
        function Validate() {
            objDw_main.AcceptText();
            confirm("ยืนยันการบันทึกข้อมูล");
            return true;
        }

        function MenubarNew() {
            postNewClear()
        }

        function OnDwmain_ItemChange(s, r, c, v) {
            if (c == "memold_no") {
                objDw_main.SetItem(r, "memold_no", v);
                objDw_main.AcceptText();
                postInitMember();
            }
        }
        function MenubarOpen() {
           // Gcoop.OpenDlg('580', '590', 'w_dlg_mbshr_trnmb_search.aspx', '');
            Gcoop.OpenDlg2('630', '590', 'w_dlg_mbshr_trnmb_search.aspx', '');
        }

        function GetValueFromDlg(memberno) {

            objDw_main.SetItem(1, "memold_no", memberno);
            objDw_main.AcceptText();
            Gcoop.GetEl("Hdmember_no").value = memberno;
            postInitMember();
        }
        function OnDwmain_ButtonClick(s, r, b) {
            if (b == "b_memnew") {
                postSetMemberNo();
            } else if (b == "b_sch_memb") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_sl_member_search.aspx', '');
            }
        }

        function SheetLoadComplete() {
            //$("input[name='memnew_no_0']").prop('disabled', true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_mbsrv_req_trnmb"
            LibraryList="~/DataWindow/mbshr/mb_req_trnmb.pbl" ClientScriptable="True" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
            TabIndex="1" ClientEventItemChanged="OnDwmain_ItemChange" ClientEventClicked="OnDwmain_Click"
            ClientEventButtonClicked="OnDwmain_ButtonClick">
        </dw:WebDataWindowControl>
    </p>
    <asp:HiddenField ID="Hdmember_no" runat="server" />
</asp:Content>

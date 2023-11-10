<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_atm_cancle.aspx.cs" Inherits="Saving.Applications.atm.w_sheet_atm_cancle" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostMemberNo%>
    <%=jsRetrieve%>
    <%=jsPostBack%>
    <script type="text/JavaScript">
        function Validate() {
            var atmcard_id = objDwMain.GetItem(1, "atmcard_id");
            var a = atmcard_id.substring(0, 4) + " " + atmcard_id.substring(4, 8) + " " + atmcard_id.substring(8, 12) + " " + atmcard_id.substring(12, 16);
            return confirm("ยืนยันการยกเลิกบัตร เลขทะเบียน: " + objDwMain.GetItem(1, "member_no") + " หมายเลขบัตร: " + a);
        }

        function OnDwMainButtonClicked(s, r, c) {
            switch (c) {
                case "b_1":
                    Gcoop.OpenDlg(530, 500, "w_dlg_dp_member_search.aspx", "?coopid=001001");
                    break;
            }
        }
        function GetMemberNoFromDlg(Coopid, memberNo) {
            objDwMain.SetItem(1, "member_no", memberNo);
            objDwMain.AcceptText();
            jsPostMemberNo();
        }

        function OnDwMainItemChanged(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            switch (c) {
                case "member_no":
                    jsPostMemberNo();
                    break;
                case "doucment_etc":
                    if (objDwMain.GetItem(1, "doucment_etc") == 0) objDwMain.SetItem(1, "document", "");
                    jsRetrieve();
                    break;
            }
        }
        function OnDwDetailItemChanged(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            switch (c) {
                case "chk":
                    //jsPostBack();
                    break;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_atm_cancle"
        LibraryList="~/DataWindow/atm/dp_atm_cancle.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventItemChanged="OnDwMainItemChanged" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <asp:Literal ID="Message" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_atm_cancle_detail"
        LibraryList="~/DataWindow/atm/dp_atm_cancle.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventItemChanged="OnDwDetailItemChanged" ClientEventButtonClicked="OnDwDetailButtonClicked">
    </dw:WebDataWindowControl>
</asp:Content>

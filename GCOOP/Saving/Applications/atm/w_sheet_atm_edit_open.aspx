<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_atm_edit_open.aspx.cs" Inherits="Saving.Applications.atm.w_sheet_atm_edit_open" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostMemberNo%>
    <%=jsRetrieve%>
    <%=jsPostBack%>
    <%=jsAddDeptNo%>
    <script type="text/JavaScript">
        function Validate() {
            return confirm("ยืนยันข้อมูล เลขทะเบียน: " + objDwMain.GetItem(1, "member_no") + " สาขา: " + objDwMain.GetItem(1, "coop_id"));
        }

        function OnDwMainButtonClicked(s, r, c) {
            switch (c) {
                case "b_1":
                    Gcoop.OpenDlg(530, 500, "w_dlg_dp_member_search.aspx", "?coopid=001001");
                    break;
                case "default_1":
                    Gcoop.GetEl("HdCoop_id").value = objDwMain.GetItem(1, "coop_id");
                    alert("Default สาขา : " + Gcoop.GetEl("HdCoop_id").value);
                    break;
                case "default_2":
                    Gcoop.GetEl("HdDoccument").value = objDwMain.GetItem(1, "document_card") + "" + objDwMain.GetItem(1, "document_card_office") + objDwMain.GetItem(1, "doucment_etc");
                    alert("Default เอกสารประกอบ : " + Gcoop.GetEl("HdDoccument").value);
                    break;
                case "default_3":
                    Gcoop.GetEl("HdReceive_coop_id").value = objDwMain.GetItem(1, "receive_coop_id");
                    alert("Default รับที่ : " + Gcoop.GetEl("HdReceive_coop_id").value);
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

        function OnDwDetailButtonClicked(s, r, c) {
            switch (c) {
                case "b_1": //เพิ่มบัญชีเข้าระบบ
                    Gcoop.GetEl("HdDeptaccount_no").value = s.GetItem(r, "deptaccount_no")
                    jsAddDeptNo();
                    break;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_atm_edit_open"
        LibraryList="~/DataWindow/atm/dp_atm_edit_open.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventItemChanged="OnDwMainItemChanged" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <asp:Literal ID="Message" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_atm_edit_open_detail"
        LibraryList="~/DataWindow/atm/dp_atm_edit_open.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventItemChanged="OnDwDetailItemChanged" ClientEventButtonClicked="OnDwDetailButtonClicked">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdCoop_id" runat="server" Value="001001" />
    <asp:HiddenField ID="HdDoccument" runat="server" Value="100" />
    <asp:HiddenField ID="HdReceive_coop_id" runat="server" Value="001001" />
    <asp:HiddenField ID="HdDeptaccount_no" runat="server" />
</asp:Content>

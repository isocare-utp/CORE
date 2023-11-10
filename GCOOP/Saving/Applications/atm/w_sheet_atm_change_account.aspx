<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_atm_change_account.aspx.cs" Inherits="Saving.Applications.atm.w_sheet_atm_change_account" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostMemberNo%>
    <%=jsRetrieve%>
    <%=jsPostBack%>
    <%=jsAddDeptNo%>
    <%=jsCloseDeptNo%>
    <%=jsOpenDeptNo%>
<script type="text/JavaScript">
    function Validate() {
        return confirm("ยืนยันข้อมูล เลขทะเบียน: " + objDwMain.GetItem(1, "member_no") + " สาขา: " + objDwMain.GetItem(1, "coop_id"));
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

    function OnDwDetailButtonClicked(s, r, c) {
        switch (c) {
            case "b_1": //เพิ่มบัญชีเข้าระบบ
                Gcoop.GetEl("HdDeptaccount_no").value = s.GetItem(r, "deptaccount_no")
                jsAddDeptNo();
                break;
            case "b_2": //close
                Gcoop.GetEl("HdDeptaccount_no").value = s.GetItem(r, "deptaccount_no")
                jsCloseDeptNo();
                break;
            case "b_3": //open
                Gcoop.GetEl("HdDeptaccount_no").value = s.GetItem(r, "deptaccount_no")
                jsOpenDeptNo();
                break;
        }
    }
    </script>
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_atm_change_account"
        LibraryList="~/DataWindow/atm/dp_atm_change_account.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventItemChanged="OnDwMainItemChanged" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_atm_change_account_detail"
        LibraryList="~/DataWindow/atm/dp_atm_change_account.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventItemChanged="OnDwMainItemChanged" ClientEventButtonClicked="OnDwDetailButtonClicked">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdDeptaccount_no" runat="server" />
</asp:Content>

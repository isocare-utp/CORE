<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_atm_setcoop.aspx.cs" Inherits="Saving.Applications.atm.w_sheet_atm_setcoop" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsRetrieve%>
    <%=jsSaveCoop%>
    <%=jsSaveBranch%>
    <%=jsDelBranch%>
    <%=jsDelCoop%>
    <%=jsAddCoop%>
    <%=jsAddBranch%>
    <script type="text/JavaScript">
        function OnDwBranchButtonClicked(s, r, c) {
            switch (c) {
                case "b_del":
                    isconfirm = confirm("ยืนยันการลบข้อมูล");
                    if (!isconfirm) {
                        return false;
                    }
                    Gcoop.GetEl("HdRowDelBranch").value = r;
                    jsDelBranch();
                    break;
            }
        }

        function OnDwCoopButtonClicked(s, r, c) {
            switch (c) {
                case "b_del":
                    isconfirm = confirm("ยืนยันการลบข้อมูล");
                    if (!isconfirm) {
                        return false;
                    }
                    Gcoop.GetEl("HdRowDelCoop").value = r;
                    jsDelCoop();
                    break;
            }
        }

        function OnDwMasterCoopButtonClicked(s, r, c) {
            switch (c) {
                case "b_1":
                    jsSaveCoop();
                    break;
            }
        }

        function OnDwMasterBranchButtonClicked(s, r, c) {
            switch (c) {
                case "b_save_branch":
                    jsSaveBranch();
                    break;
            }
        }

        function OnDwMasterCoopItemChanged(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            switch (c) {
                case "atmcoop_atmdept_maxamt":
                    //jsRetrieve();
                    break;
            }
        }

        function OnDwMasterBranchItemChanged(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            switch (c) {
                case "atmcoop_coop_id":
                    //jsRetrieve();
                    break;
            }
        }

        function AddCoop() {
            //Gcoop.OpenDlg(800, 200, "w_dlg_atm_addatmcoop.aspx", "?flag=1");
            jsAddCoop();
        }
//        function GetAddATMcoop(atmcoop_coop_id, atmcoop_coop_desc, atmcoop_coophold, atmcoop_withdraw_flag, atmcoop_atmdept_maxamt, atmdept_maxcnt) {
//            objDwMasterCoop.SetItem(1, "atmcoop_coop_id", atmcoop_coop_id);
//            objDwMasterCoop.SetItem(1, "atmcoop_coop_desc", atmcoop_coop_desc);
//            objDwMasterCoop.SetItem(1, "atmcoop_coophold", atmcoop_coophold);
//            objDwMasterCoop.SetItem(1, "atmcoop_withdraw_flag", atmcoop_withdraw_flag);
//            objDwMasterCoop.SetItem(1, "atmcoop_atmdept_maxamt", atmcoop_atmdept_maxamt);
//            objDwMasterCoop.SetItem(1, "atmdept_maxcnt", atmdept_maxcnt);
//            objDwMasterCoop.AcceptText();
//            jsSaveCoop();
//        }

        function AddBranch() {
            //Gcoop.OpenDlg(800, 200, "w_dlg_atm_addatmcoop.aspx", "?flag=2");
            jsAddBranch();
        }

//        function GetAddATMcoopBranch(atmcoop_coop_id, atmcoopbranch_branch_id, atmcoopbranch_branch_desc, atmcoopbranch_branchhold, atmcoopbranch_withdraw_flag) {
//            objDwMasterBranch.SetItem(1, "atmcoop_coop_id", atmcoop_coop_id);
//            objDwMasterBranch.SetItem(1, "atmcoopbranch_branch_id", atmcoopbranch_branch_id);
//            objDwMasterBranch.SetItem(1, "atmcoopbranch_branch_desc", atmcoopbranch_branch_desc);
//            objDwMasterBranch.SetItem(1, "atmcoopbranch_branchhold", atmcoopbranch_branchhold);
//            objDwMasterBranch.SetItem(1, "atmcoopbranch_withdraw_flag", atmcoopbranch_withdraw_flag);
//            objDwMasterBranch.AcceptText();
//            jsSaveBranch();
//        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <input id="ShifF5" type="button" value="เพิ่มสหกรณ์" onclick="AddCoop()" style="height: 40px;
        width: 100px;" />
    <input id="ShifF7" type="button" value="เพิ่มสาขา" onclick="AddBranch()" style="height: 40px;
        width: 100px;" />
    <dw:WebDataWindowControl ID="DwCoop" runat="server" DataWindowObject="d_atm_setcoop_coop"
        LibraryList="~/DataWindow/atm/dp_atm_setcoop.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventButtonClicked="OnDwCoopButtonClicked">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwBranch" runat="server" DataWindowObject="d_atm_setcoop_branch"
        LibraryList="~/DataWindow/atm/dp_atm_setcoop.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventButtonClicked="OnDwBranchButtonClicked">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwMasterCoop" runat="server" DataWindowObject="d_atm_setcoop_master_coop"
        LibraryList="~/DataWindow/atm/dp_atm_setcoop.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventButtonClicked="OnDwMasterCoopButtonClicked"
        ClientEventItemChanged="OnDwMasterCoopItemChanged">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwMasterBranch" runat="server" DataWindowObject="d_atm_setcoop_master_branch"
        LibraryList="~/DataWindow/atm/dp_atm_setcoop.pbl" ClientScriptable="True" ClientEvents="true"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        AutoRestoreContext="False" ClientEventButtonClicked="OnDwMasterBranchButtonClicked"
        ClientEventItemChanged="OnDwMasterBranchItemChanged">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdCoop_id" runat="server" Value="001001" />
    <asp:HiddenField ID="HdDoccument" runat="server" Value="100" />
    <asp:HiddenField ID="HdReceive_coop_id" runat="server" Value="001001" />
    <asp:HiddenField ID="HdRowDelBranch" runat="server" />
    <asp:HiddenField ID="HdRowDelCoop" runat="server" />
    <asp:HiddenField ID="AddCoopFlag" runat="server" />
    <asp:HiddenField ID="AddBranchFlag" runat="server" />
</asp:Content>

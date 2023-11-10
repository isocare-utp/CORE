<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="ws_dlg_ass_edit.aspx.cs" Inherits="Saving.Applications.assist.dlg.ws_dlg_ass_edit_ctrl.ws_dlg_ass_edit" %>


<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc1" %>


<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        var dsMain = new DataSourceTool();

        function OnDsMainClicked(s, r, c) {
        
            if (c == "b_search") {
                PostRetrive();
            }

        }


        function OnDsMainItemChanged(s, r, c) {
            if (c == "member_no") {
                PostRetriveMemberNo();

            }

        }


        function OnDsListClicked(s, r, c) {
            var assistdocno = dsList.GetItem(r, "assist_docno")
            try {
                window.opener.GetValueFromDlg(assistdocno);
                window.close();
            } catch (err) {
                parent.GetValueFromDlg(assistdocno);
                parent.RemoveIFrame();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc2:DsMain ID="dsMain" runat="server" />

    <uc1:DsList ID="dsList" runat="server" />

    <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px"></asp:Label>
    <br />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="ws_dlg_sl_editcollateral_master.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl.ws_dlg_sl_editcollateral_master_ctrl.ws_dlg_sl_editcollateral_master" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool;
        var dsMain = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        }
        function OnDsListClicked(s, r, c, v) {
            if (c == "collmast_refno" || c == "cp_redeem") {

                dsList.SetRowFocus(r);
                var member_no = dsMain.GetItem(0, "member_no");
                var collmast_no = dsList.GetItem(r, "collmast_no");
                try {
                    window.opener.GetItem(member_no, collmast_no);
                    window.close();
                } catch (err) {
                    parent.GetItem(member_no, collmast_no);
                    window.close();
                }

            }
        }
        function SheetLoadComplete() {

        }                
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <div align="center">
        <uc1:DsMain ID="dsMain" runat="server" />
        <uc2:DsList ID="dsList" runat="server" />
    </div>
</asp:Content>

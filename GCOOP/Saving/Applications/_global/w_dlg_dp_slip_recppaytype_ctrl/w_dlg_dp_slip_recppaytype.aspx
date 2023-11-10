<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_dp_slip_recppaytype.aspx.cs" Inherits="Saving.Applications.deposit.w_dlg_dp_slip_recppaytype_ctrl.w_dlg_dp_slip_recppaytype" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var DsMain = new DataSourceTool();

        function OnDsMaintemChanged(s, r, c, v) {
            if (c == "deptwith_flag") {
                PostList();
            }
        }

        function OnDsListClicked(s, r, c) {
            if (r >= 0) {
                var recppaytype_code = dsList.GetItem(r, "recppaytype_code");
                var recppaytype_desc = dsList.GetItem(r, "recppaytype_desc");
                var moneytype = dsList.GetItem(r, "moneytype_support");
                parent.GetValueRecpPayTypeCode(recppaytype_code, recppaytype_desc, moneytype);
                parent.RemoveIFrame(); 
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <div align="left" style="margin-left: 15px; margin-top: 10px;">
        <span class="TitleSpan">กรุณาเลือกทำรายการ</span>
        <uc1:DsMain ID="dsMain" runat="server" />
        <br />
        <uc2:DsList ID="dsList" runat="server" />
    </div>
</asp:Content>

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_const_cmucftofromaccid.aspx.cs" Inherits="Saving.Applications.shrlon_const.w_sheet_sl_const_cmucftofromaccid_ctrl.w_sheet_sl_const_cmucftofromaccid" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsList = new DataSourceTool();

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function SheetLoadComplete() {
        }
        function OnDsDeListItemChanged(s, r, c, v) {
            if (c == "account_id") {
                PostSetAccName();
            }
        }

        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);
                PostDelRow();
            }
        }

        function OnClickNewRow() {
            PostNewRow();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มแถว</span>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>

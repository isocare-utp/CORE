<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="dlg_sl_bankbranch.aspx.cs" Inherits="Saving.Applications.shrlon.ws_convert_txt2db_ctrl.dlg_sl_bankbranch_ctrl.dlg_sl_bankbranch" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .CCSBank
        {
            padding-left: 2ex;
        }
    </style>
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        function OnDsMainClicked(s, r, c) {
            if (confirm("ผ่านรายการไปยัง "+dsMain.GetItem(r, "bank_desc"))) {
                if (c == "bank_desc" || c == "bank_code") {
                    var bank_code = dsMain.GetItem(r, "bank_code");
                    parent.RecBankCode(bank_code);
                    parent.RemoveIFrame();
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <div class="CCSBank">
        <br />
        <div>
            <center>
                <span style="font-weight: bold;">เลือกธนาคารที่ต้องการผ่านข้อมูล</span>
            </center>
        </div>
        <br />
        <uc1:DsMain ID="dsMain" runat="server" />
    </div>
</asp:Content>

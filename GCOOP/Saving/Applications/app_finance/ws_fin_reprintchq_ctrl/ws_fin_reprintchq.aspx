<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fin_reprintchq.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_reprintchq_ctrl.ws_fin_reprintchq" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool();
    var dsList = new DataSourceTool();
    function Validate() {
        var save_flag = 0;
        for (var i = 1; i <= dsList.GetRowCount(); i++) {
            if (dsList.GetItem(i - 1, "ai_flag") == "1") {
                save_flag = 1;
            }
        }
        if (save_flag == 1) {
            return confirm("ยืนยันการบันทึกข้อมูล");
        } else { alert("กรุณาเลือกรายการที่จะพิมพ์เช็ค"); }
    }
    function OnDsMainClicked(s, r, c) {
        if (c == "b_search") {
            PostSearch();
        }
    }
    function OnDsMainItemChanged(s, r, c) {
        if (c == "chq_no") {
            PostInitchqno();
        }
        else if (c == "bank_code") {
            dsMain.SetItem(0, "branch_code", "");
            if (c == null) {
                dsMain.GetElement(0, "branch_code").disabled = true;
            } else {
                dsMain.GetElement(0, "branch_code").disabled = false;
            }
            PostGetBank();
        }
    }
    function SheetLoadComplete() {
        if (dsMain.GetItem(0, "bank_code") == null || dsMain.GetItem(0, "bank_code") == "") {
            dsMain.SetItem(0, "branch_code", "");
            dsMain.GetElement(0, "branch_code").disabled = true;
        } else {
            dsMain.GetElement(0, "branch_code").disabled = false;
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
   <uc2:DsList ID="dsList" runat="server" />
</asp:Content>

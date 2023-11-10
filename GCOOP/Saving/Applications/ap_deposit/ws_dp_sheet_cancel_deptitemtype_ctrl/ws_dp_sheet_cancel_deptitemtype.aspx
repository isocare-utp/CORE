<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_dp_sheet_cancel_deptitemtype.aspx.cs" Inherits="Saving.Applications.ap_deposit.ws_dp_sheet_cancel_deptitemtype_ctrl.ws_dp_sheet_cancel_deptitemtype" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;       

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
       
        function OnDsMainClicked(s, r, c) {
            if (c == "b_save") {
                var code = dsMain.GetItem(0, "system_code");
                if (code == '' || code == null) {
                    alert("กรุณาเลือกรหัสรายการ");
                    return;
                } else {
                    PostSave();
                }                
            } 
        }

        function OnDsListClicked(s, r, c) {
        }

        function SheetLoadComplete() {
        
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>   
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>



<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_as_assucfdisaste.aspx.cs" Inherits="Saving.Applications.assist.ws_as_assucfdisaste_ctrl.ws_as_assucfdisaste" %>

<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsDetail = new DataSourceTool();

        function Validate() {
            for (var i = 0; i < dsDetail.GetRowCount(); i++) {
                var disaster_code = dsDetail.GetItem(i, "disaster_code");
                var disaster_desc = dsDetail.GetItem(i, "disaster_desc");
                if (disaster_code == "" || disaster_code == null) {
                    alert("กรุณากรอกรหัสประเภทภัยพิบัติ ให้ถูกต้อง");
                    return false;
                } else if (disaster_desc == "" || disaster_desc == null) {
                    alert("กรุณากรอกชื่อประเภทภัยพิบัติ ให้ถูกต้อง");
                    return false;
                }
            }
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsDetailClicked(s, r, c) {
            if (c == "b_del") {
                dsDetail.SetRowFocus(r);
                if (confirm("ยืนยันการลบ") == true) {
                    PostDeleteRow();
                }
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="right" style="margin-right: 1px;">
        <input id="ShifF1" type="button" value="เพิ่มแถว" onclick="PostInsertRow()" style="height: 20px; width : 50px;" />
        </div>
        <uc1:DsDetail ID="dsDetail" runat="server" />
</asp:Content>

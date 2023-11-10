<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_as_assucfedulevel.aspx.cs" Inherits="Saving.Applications.assist.ws_as_assucfedulevel_ctrl.ws_as_assucfedulevel" %>

<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsDetail = new DataSourceTool();

        function Validate() {
            for (var i = 0; i < dsDetail.GetRowCount(); i++) {
                var edulevel_code = dsDetail.GetItem(i, "edulevel_code");
                var edulevel_desc = dsDetail.GetItem(i, "edulevel_desc");
                if (edulevel_code == "" || edulevel_code == null) {
                    alert("กรุณากรอกรหัสระดับชั้นการศึกษา ให้ถูกต้อง");
                    return false;
                } else if (edulevel_desc == "" || edulevel_desc == null) {
                    alert("กรุณากรอกระดับชั้นการศึกษา ให้ถูกต้อง");
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


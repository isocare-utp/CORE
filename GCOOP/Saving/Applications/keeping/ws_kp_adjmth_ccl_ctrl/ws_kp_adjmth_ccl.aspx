<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_kp_adjmth_ccl.aspx.cs" Inherits="Saving.Applications.keeping.ws_kp_adjmth_ccl_ctrl.ws_kp_adjmth_ccl" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();
        var dsDetail = new DataSourceTool();

        function Validate() {
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                var operate_flag = dsList.GetItem(i, "operate_flag");
                if (operate_flag == 1) {
                    return confirm("ยืนยันการบันทึกข้อมูล?");
                }
            }
            alert("กรุณาเลือกรายการใบเสร็จที่ต้องการยกเลิก");
            return;            
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                postMemberNo();
            }
        }

        function OnDsMainClicked(s, r, c) {

        }

        function OnDsListItemChanged(s, r, c, v) {
            if (c == "operate_flag") {
                postSlipNo();
            }
        }

        function OnDsListClicked(s, r, c) {

        }

        function SheetLoadComplete() {
            document.getElementById("ctl00_ContentPlace_dsMain_FormView1_member_no").focus();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsList ID="dsList" runat="server" />
    <br />
    <uc3:DsDetail ID="dsDetail" runat="server" />
</asp:Content>

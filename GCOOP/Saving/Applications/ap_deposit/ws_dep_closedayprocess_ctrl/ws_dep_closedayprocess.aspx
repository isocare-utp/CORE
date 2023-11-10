<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_dep_closedayprocess.aspx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_closedayprocess_ctrl.ws_dep_closedayprocess" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool();

    function OnDsMainClicked(s, r, c) {
        if (c == "b_closedate") {
            var check_save = dsMain.GetItem(0, "check_save");
            if (check_save == "" || check_save == null) {
                var isConfirm = confirm("ต้องการปิดงานสิ้นวันใช่หรือไม่");
                if (isConfirm) {
                    PostCloseDay();
                }
            } else {
                alert(check_save);
            }
        }
        return 0;
    }

    function Validate() {
        alert("หน้าจอประมวลผลปิดสิ้นวัน ไม่มีคำสั่งเซฟ");
        return false;
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <span id="F_dsList" style="display: none;">
        <uc2:DsList ID="dsList" runat="server" />
    </span>
    <asp:HiddenField ID="HdCloseday" runat="server" />
    <%=outputProcess%>
</asp:Content>

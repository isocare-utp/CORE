<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_divsrv_oper_cls.aspx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_oper_cls_ctrl.ws_divsrv_oper_cls" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการปิดยอดปันผลค้างจ่ายปีที่แล้ว")
        }

        function SheetLoadComplete() {
//            if (Gcoop.GetEl("Hd_process").value == "true") {
//                Gcoop.OpenProgressBar("ประมวลปิดยอดปันผลค้างจ่ายปีที่แล้ว", true, true, "");
//            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
    <asp:HiddenField ID="Hd_process" runat="server" />
    <%=outputProcess%>
</asp:Content>

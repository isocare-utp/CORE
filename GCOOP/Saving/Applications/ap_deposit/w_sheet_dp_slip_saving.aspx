<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_slip_saving.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_slip_saving"
    Culture="th-TH" %>

<%@ Register Src="w_sheet_dp_slip_saving_ctrl/SlipMaster.ascx" TagName="SlipMaster"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postDeptAccountNo%>
    <%=postDeptAccountNoHd%>
    <%=postReset%>
    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกหน้าจอ");
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame(900, 600, "w_dlg_dp_account_search.aspx", "");
        }

        function MenubarNew() {
            window.location = "";
        }

        function NewAccountNo(coopid, dept_no) {
            Gcoop.GetEl("HdDumCoopId").value = coopid;
            Gcoop.GetEl("HdDumAccountNo").value = dept_no;
            postDeptAccountNoHd();
        }

        function SheetLoadComplete() {
            var hdPrintBook = Gcoop.GetEl("HdPrintBook").value;
            var hdPrintSlip = Gcoop.GetEl("HdPrintSlip").value;

            if (hdPrintBook == "true") {
                Gcoop.OpenIFrame(900, 400, "w_dlg_dp_printbook.aspx", "?deptAccountNo=" + Gcoop.GetEl("HdDumAccountNo").value);
            }
            if (Gcoop.GetEl("HdFinish").value == "true") {
                document.getElementById("ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_deptaccount_no").focus();
            }
        }

        this.onfocus = function () {
            if (Gcoop.GetEl("HdFinish").value == "true") {
                document.getElementById("ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_deptaccount_no").focus();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:SlipMaster ID="SlipMaster1" runat="server" />
    <table border="0" width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="LbDetail" runat="server" Text="&nbsp;"></asp:Label>
            </td>
            <td align="right">
                <span id="totalAmount">&nbsp; &nbsp; &nbsp;</span>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdPrintSlip" runat="server" />
    <asp:HiddenField ID="HdPrintBook" runat="server" />
    <asp:HiddenField ID="HdDumAccountNo" Value="" runat="server" />
    <asp:HiddenField ID="HdDumCoopId" Value="" runat="server" />
    <asp:HiddenField ID="HdFinish" Value="" runat="server" />
</asp:Content>

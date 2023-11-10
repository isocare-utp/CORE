<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_cfinttable.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfinttable_ctrl.ws_sl_cfinttable" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc3" %>
<%@ Register Src="DsNew.ascx" TagName="DsNew" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;
        var dsList = new DataSourceTool;
        var dsNew = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "loanintrate_code" || c == "loanintrate_desc") {
                dsMain.SetRowFocus(r)
                PostLoanintrateCode();


            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
        }

        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);
                PostDeleteRow();
            }
        }

        function OnDsListItemChanged(s, r, c, v) {
        }

        function OnDsDetailClicked(s, r, c) {
        }

        function OnDsDetailItemChanged(s, r, c, v) {
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table width="100%">
        <tr>
            <td>
                <span class="NewRowLink" onclick="PostInsertNew()" style="font-size: small;">เพิ่มประเภท
                </span>
            </td>
            <td>
                <span class="NewRowLink" onclick="PrintReport()" style="font-size: small;">พิมพ์รายงาน
                </span>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <uc4:DsNew ID="dsNew" runat="server" />
                <uc1:DsMain ID="dsMain" runat="server" />
            </td>
            <td valign="top">
                <uc2:DsDetail ID="dsDetail" runat="server" />
                <span class="NewRowLink" onclick="PostInsertRow()" style="font-size: small;">เพิ่มแถว
                </span>
                <uc3:DsList ID="dsList" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

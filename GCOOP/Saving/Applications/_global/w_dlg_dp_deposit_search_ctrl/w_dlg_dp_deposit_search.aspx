<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_dp_deposit_search.aspx.cs" Inherits="Saving.Applications._global.w_dlg_dp_deposit_search_ctrl.w_dlg_dp_deposit_search" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var DsMain = new DataSourceTool();

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function SheetLoadComplete() {
        }

        function OnDsListClicked(s, r, c) {
            if (r >= 0) {
                var member_no = dsList.GetItem(r, "member_no");
                var member_name = dsList.GetItem(r, "member_name");
                var deptaccount_no = dsList.GetItem(r, "deptaccount_no");

                parent.GetValueMember(member_no, member_name, deptaccount_no);
                parent.RemoveIFrame();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <div align="left" style="margin-left: 18px; margin-top: 10px;">
        <table>
            <tr>
                <td>
                    <uc1:DsMain ID="dsMain" runat="server" />
                </td>
                <td>
                    <asp:Button ID="BtSearch" runat="server" Text="ค้น" Width="60px" Height="60px" OnClick="BtSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div align="left" style="margin-left: 18px;">
        <span class="TitleSpan">ข้อมูลสมาชิก</span>
        <uc2:DsList ID="dsList" runat="server" />
        <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma"
            Font-Size="14px"></asp:Label>
    </div>
</asp:Content>

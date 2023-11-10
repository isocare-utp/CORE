<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_am_apvlevel.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_am_apvlevel_ctrl.w_sheet_am_apvlevel" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsDetail = new DataSourceTool();
        var dsList = new DataSourceTool();
        var pl = new CallPlsql();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsListClicked(s, r, c) {
            if (c == "apvlevel_id" || c == "description") {
             
                dsList.SetRowFocus(r);
                PostApv();
            }
//            if (c == "description") {
//             //   pl.CallAjax();
//            
//            }
        
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td style="vertical-align:top; width:270px;" >
                <uc1:DsList ID="dsList" runat="server" />
            </td>
            <td style="vertical-align:top; width:450px;">
                <uc2:DsDetail ID="dsDetail" runat="server" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>

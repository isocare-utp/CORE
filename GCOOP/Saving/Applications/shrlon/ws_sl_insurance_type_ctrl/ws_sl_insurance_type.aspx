<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_insurance_type.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_insurance_type_ctrl.ws_sl_insurance_type" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsList = new DataSourceTool;
        var dsDetail = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsDetailItemChanged(s, r, c, v) {
            
        }
        function OnDsListClicked(s, r, c) {
            if (c == "instype_code" || c == "instype_desc" ) {
                PostInsType();
            }
           
        }
        function OnClickNew() {
            Gcoop.OpenIFrame3("630", "450", "w_dlg_sl_add_instype.aspx", "");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
     <span class="NewRowLink" onclick="OnClickNew()">เพิ่มประกัน</span>
    <table>
        <tr>
            <td valign="top">
                <uc2:DsList ID="dsList" runat="server" />
            </td>
            <td valign="top">
                <uc3:DsDetail ID="dsDetail" runat="server" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>

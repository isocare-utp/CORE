<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_setPrinter.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_setPrinter_ctrl.w_sheet_setPrinter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <%@ register src="DsList.ascx" tagname="DsList" tagprefix="uc1" %>
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r); //ให้รู้ว่ากดแถวไหนในลิส
                PostDeleteRow(); //PostDeleteRow(); ไว้ลบแถวที่เลือก
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
    <uc1:DsList ID="dsList" runat="server" />
    <asp:HiddenField ID="seq_no_old" runat="server" />

</asp:Content>
 
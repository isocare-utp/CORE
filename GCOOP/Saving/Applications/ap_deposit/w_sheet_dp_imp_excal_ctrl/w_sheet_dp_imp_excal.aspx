<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_imp_excal.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_imp_excal_ctrl.w_sheet_dp_imp_excal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validate() {
            return confirm("ต้องการ Import ข้อมูลหรือไม่ ?");
        }
        function fnConfirmDelete() {
            return confirm("ต้องการลบข้อมูลที่ Import ทั้งหมดหรือไหม ?");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table class="DataSourceFormView">
        <tr>
            <td style="width:150px;">
            <span>วันที่ : </span>
            </td>
            <td>
            <asp:TextBox ID="entry_date" runat="server" Width="100px" style="text-align:center;" ReadOnly="true"></asp:TextBox>
            </td>
       </tr>
        <tr>
            <td >
            <span>ประเภทรายการ : </span>
            </td>
            <td>
            <asp:DropDownList ID="type_code" runat="server" Width="200px">
             </asp:DropDownList>
            </td>
       </tr>
        <tr>
            <td >
            <span>File Path : </span>
            </td>
            <td>
            <asp:FileUpload ID="txtInput" runat="server" />
            </td>
       </tr>
       <tr>
            <td >
            <span>*** หมายเหตุ : </span>
            </td>
            <td>
            ไฟล์ที่จะ Import จะต้องเป็นไฟล์ .xlsx เท่านั้น
            </td>
       </tr>
       <tr>
            <td >
            <span>ลบข้อมูลที่ Import : </span>
            </td>
            <td>
            <asp:Button ID="b_delete" runat="server" Text="ลบข้อมูลที่ Import" Style="width: 195px;" OnClick="B_Delete_Click"  onClientClick="return fnConfirmDelete();" />
            </td>
       </tr>
       <tr>
             <td colspan="2" align="right" >
            <a href="../../../filecommon/Sample_Deposit_Excal.xlsx">ไฟล์ตัวอย่าง</a>
            </td>
       </tr>
    </table>
</asp:Content>

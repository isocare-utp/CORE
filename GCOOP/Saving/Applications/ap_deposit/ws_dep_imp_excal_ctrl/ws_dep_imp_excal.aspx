<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_dep_imp_excal.aspx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_imp_excal_ctrl.ws_dep_imp_excal" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
       var dsMain = new DataSourceTool;

       function OnDsMainClicked(s, r, c) {
           if (c == "b_process") {
               var type_code = dsMain.GetItem(0, "type_code");
               if (type_code == null) {
                   alert("กรุณาเลือกประเภทรายการ!"); return;
               }
               if (confirm("ยืนยันการ Import ข้อมูล")) {
                   JsPostProcess();
               }
           } else if (c == "b_delete") {
               var type_code = dsMain.GetItem(0, "type_code");
               if (type_code == null) {
                   alert("กรุณาเลือกประเภทรายการ!"); return;
               }
               var isConfirm = confirm("ยืนยันการลบข้อมูลที่ Import");
               if (isConfirm) {
                   JsPostDelete();
               }
           }
       }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <table class="DataSourceFormView">
         <tr>
            <td style="width:180px;">
                <span>File Path : </span>
            </td>
            <td colspan="2">
                <asp:FileUpload ID="txtInput" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <span>*** หมายเหตุ : </span>
            </td>
            <td style="width:180px;">
                <div style="font-size:16px;background-color:Yellow"><a href="../../../filecommon/Sample_Deposit_Excal.xlsx">ดาวน์โหลดไฟล์ตัวอย่าง</a></div>
            </td>
            <td>
                **** ไฟล์ที่จะ Import จะต้องเป็นไฟล์ .xlsx เท่านั้น
            </td>
        </tr>
    </table>
</asp:Content>
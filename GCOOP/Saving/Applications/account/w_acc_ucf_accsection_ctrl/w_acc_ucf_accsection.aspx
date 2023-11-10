<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_acc_ucf_accsection.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_accsection_ctrl.w_acc_ucf_accsection" %>

<%@ Register Src="wd_list.ascx" TagName="wd_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var wd_list = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnWdListClicked(s, r, c, v) {
            if (c == "b_delete") {
                wd_list.SetRowFocus(r);
                PostDeleteRow();
            }
        }
        function OnWdListItemChanged(s, r, c, v) {
            if (c == "section_id") {
                var ls_row = wd_list.GetRowCount() - 1;
                var ls_section = wd_list.GetItem(r, "section_id");
                //alert(ls_prename);
                for (var i = 0; i < ls_row; i++) {
                    var ls_section_2 = wd_list.GetItem(i, "section_id");
                    // alert(ls_prename_code);
                    if (section_id == ls_section_2) {
                        alert("ชื่อย่อระบบซ้ำ");
                        wd_list.SetItem(r, "section_id", "");
                        break;
                    }
                }
            }
        }
        function SheetLoadComplete() {


        }
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="right" style="margin-right: 1px; width: 720px;">
        <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
        <uc1:wd_list ID="wd_list" runat="server" />
    </div>
</asp:Content>

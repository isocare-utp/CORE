<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mb_ucfprovince.aspx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_ucfprovince_ctrl.ws_mb_ucfprovince" %>

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
            if (c == "province_code") {
                var ls_row = wd_list.GetRowCount() - 1;
                var ls_province = wd_list.GetItem(r, "province_code");
                //alert(ls_prename);
                for (var i = 0; i < ls_row; i++) {
                    var ls_province_code = wd_list.GetItem(i, "province_code");
                    // alert(ls_prename_code);
                    if (ls_province == ls_province_code) {
                        alert("รหัสจังหวัดซ้ำ");
                        wd_list.SetItem(r, "province_code", "");
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mb_ucftambol.aspx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_ucftambol_ctrl.ws_mb_ucftambol" %>

<%@ Register Src="wd_main.ascx" TagName="wd_main" TagPrefix="uc1" %>
<%@ Register Src="wd_list.ascx" TagName="wd_list" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var wd_list = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function OnMainItemChanged(s, r, c, v) {
            if (c == "province_code") {
                PostProvince();
            } else if (c == "district_code") {
                PostDistrict();
            }
        }
        function OnListClicked(s, r, c, v) {
            if (c == "b_delete") {

                PostDeleteRow();
            }
        }
        function OnListItemChanged(s, r, c, v) {

            if (c == "tambol_code") {
                var ls_row = wd_list.GetRowCount() - 1;
                var ls_tambol = wd_list.GetItem(r, "tambol_code");
                for (var i = 0; i < ls_row; i++) {
                    var ls_tambol_code = wd_list.GetItem(i, "tambol_code");
                    // alert(ls_prename_code);
                    if (ls_tambol == ls_tambol_code) {
                        alert("รหัสตำบลซ้ำ");
                        wd_list.SetItem(r, "tambol_code", "");
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
        <uc1:wd_main ID="wd_main" runat="server" />
        <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
        <uc2:wd_list ID="wd_list" runat="server" />
    </div>
</asp:Content>

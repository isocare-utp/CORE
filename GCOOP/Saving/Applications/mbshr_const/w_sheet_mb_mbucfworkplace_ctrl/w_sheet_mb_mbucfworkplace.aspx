<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mb_mbucfworkplace.aspx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfworkplace_ctrl.w_sheet_mb_mbucfworkplace" %>

<%@ Register src="DsList.ascx" tagname="dsList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r); //ให้รู้ว่ากดแถวไหนในลิส
                PostdeleteRow(); //PostDeleteRow(); ไว้ลบแถวที่เลือก
            }
        }
        function OnListItemChanged(s, r, c, v) {
            if (c == "department_code") {
                var ls_row = dsList.GetRowCount() - 1;
                var ls_appltype = dsList.GetItem(r, "department_code");
                for (var i = 0; i < ls_row; i++) {
                    var ls_appltype_code = dsList.GetItem(i, "department_code");
                    if (ls_appltype == ls_appltype_code) {
                        alert("รหัสประเภทซ้ำ");
                        dsList.SetItem(r, "department_code", "");
                        break;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"> </asp:Literal>
    <div style="text-align: right; height: 16px; width: 746px;">
        <span class="NewRowLink" onclick="PostInsertRow()" style="padding-left: 30px;">
     
        เพิ่มแถว</span>
      
        <uc1:DsList ID="dsList" runat="server" />
      
    
    </div>
   
</asp:Content>

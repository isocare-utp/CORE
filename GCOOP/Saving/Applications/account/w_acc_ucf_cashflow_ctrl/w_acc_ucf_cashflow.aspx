<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_acc_ucf_cashflow.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_cashflow_ctrl.w_acc_ucf_cashflow" %>

<%@ Register Src="wd_list.ascx" TagName="wd_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var wd_list = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnWdListClicked(s, r, c) {
            if (c == "b_delete") {
                wd_list.SetRowFocus(r);
                PostDeleteRow();
            }
            else if (c == "getaccidbutton") {
               
                wd_list.SetRowFocus(r);
                var accid_list = wd_list.GetItem(r, "accid_list");
                if (accid_list == null) {
                    accid_list = "";
                }
                Gcoop.OpenIFrame(950, 550, "w_dlg_select_account_new.aspx", "?acc_list=" + accid_list);
            }

        }
        function GetAccount(acc_id) {
            Gcoop.GetEl("Hdacclist").value = acc_id;
            PostGetAccid();
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
     <asp:HiddenField ID="Hdrow" runat="server" />
    <asp:HiddenField ID="Hdacclist" runat="server" />
</asp:Content>

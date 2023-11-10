<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_share_edit.aspx.cs" Inherits="Saving.Applications.mbshr.ws_sl_share_edit_ctrl.ws_sl_share_edit" %>

<%@ Register Src="wd_main.ascx" TagName="wd_main" TagPrefix="uc1" %>
<%@ Register Src="wd_list.ascx" TagName="wd_list" TagPrefix="uc2" %>
<%@ Register Src="wd_detail.ascx" TagName="wd_detail" TagPrefix="uc3" %>
<%@ Register Src="wd_statement.ascx" TagName="wd_statement" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var wd_main = new DataSourceTool;
        var wd_list = new DataSourceTool;
        var wd_detail = new DataSourceTool;
        var wd_statement = new DataSourceTool;


        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function OnMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        } function OnMainClicked(s, r, c, v) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2(650, 600, 'w_dlg_sl_member_search_lite.aspx', '')
            }
        }
        function GetValueFromDlg(memberno) {
            wd_main.SetItem(0, "member_no", memberno.trim());
            PostMemberNo();
            // Gcoop.GetEl("HdcheckPdf").value = "False";
        }
        function OnListClicked(s, r, c, v) {
            if (c == "sharetype_code" || c == "sharetype_desc" || c == "cp_shrstk") {
                wd_list.SetRowFocus(r);
                PostList();
            }
        }
        function OnStatementClicked(s, r, c, v) {
            if (c == "b_delete") {
                wd_statement.SetRowFocus(r);
                PostDeleteRow();
            }
        }
        function SheetLoadComplete() {


        }
  
    </script>
    <style type="text/css">
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        #tabs
        {
            width: 720px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //            $("#tabs").tabs();

            var tabIndex = Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
            $("#tabs").tabs({
                active: tabIndex,
                activate: function (event, ui) {
                    $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                }
            });
        });</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 700px;">
        <tr>
            <td colspan="2">
                <uc1:wd_main ID="wd_main" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div id="tabs">
                    <ul>
                        <li><a href="#tabs-1">รายละเอียด</a></li>
                        <li><a href="#tabs-2">Statement</a></li>
                    </ul>
                    <div id="tabs-1">
                        <uc3:wd_detail ID="wd_detail" runat="server" />
                    </div>
                    <div id="tabs-2">
                        <div align="right">
                            <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
                        </div>
                        <uc4:wd_statement ID="wd_statement" runat="server" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
</asp:Content>

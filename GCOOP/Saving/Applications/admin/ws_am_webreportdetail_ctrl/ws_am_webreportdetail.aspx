<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_am_webreportdetail.aspx.cs" Inherits="Saving.Applications.admin.ws_am_webreportdetail_ctrl.ws_am_webreportdetail" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    var dsList = new DataSourceTool();
    var dsMain = new DataSourceTool();

    function OnDsMainItemChanged(s, r, c, v) {
        if (c == "application") {
            JsPostApp();
        }else if(c=="group_id"){
            JsPostGroup();
        }
    }

    function OnDsListItemChanged(s, r, c, v) {
    }

    function OnDsListClicked(s, r, c) {
        if (c == "b_del") {
            if (confirm("ยืนยันการลบข้อมูล")) {
                dsList.SetRowFocus(r);
                JsPostDelete();
            }
        }
    }

    function MenubarOpen() {
    }

    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล");
    }

    function SheetLoadComplete() {
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    
    <span class="NewRowLink" onclick="JsPostInsert()">เพิ่มแถว</span> &nbsp; &nbsp;
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>

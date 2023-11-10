<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_dep_edit_prncfixed_masdue.aspx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_edit_prncfixed_masdue_ctrl.ws_dep_edit_prncfixed_masdue" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsPrncfixed.ascx" TagName="DsPrncfixed" TagPrefix="uc2" %>

<%@ Register Src="DsMasdue.ascx" TagName="DsMasdue" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsPrncfixed = new DataSourceTool();
        var dsMasdue = new DataSourceTool();

        function MenubarOpen() {
            Gcoop.OpenIFrame2(735, 700, 'wd_dep_search_deptaccount.aspx', '')
        }
        function GetDeptNoFromDlg(deptno) {           
            dsMain.SetItem(0, "deptaccount_no", deptno);
            JsPostDeptaccountno();
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "deptaccount_no") {
                JsPostDeptaccountno();
            }
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_searchdeptno") {
                Gcoop.OpenIFrame2(735, 700, 'wd_dep_search_deptaccount.aspx', '')
            }
        }
        function SheetLoadComplete() {
        }

        function Validate() {
            var alertstr = "";
            if (dsMain.GetItem(0, "deptaccount_no") == null) {
                alertstr += "กรุณากรอกเลขบัญชีที่จะทำรายการ!"; 
            }           
            if (alertstr == "") {
                return confirm("ยืนยันการบันทึกข้อมูล");
            } else {
                alert(alertstr);
                return false;
            }
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
            width:100%;
        }
    </style>
    <script type="text/javascript">
        Number.prototype.round = function (p) {
            p = p || 10;
            return parseFloat(this.toFixed(p));
        };


        $(function () {

            //  console.log($("#tabs").tabs()); //ชื่อฟิวส์
            encodeURIComponent('\u0e3a')
            var tabIndex = Gcoop.GetEl("hdTabIndex").value;
            $("#tabs").tabs({
                active: tabIndex,
                activate: function (event) {
                    //console.log(event)
                    $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                }
            });
        });    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">ต้นเงินฝาก</a></li>
            <li><a href="#tabs-2">วันครบกำหนด</a></li>
        </ul>
        <div id="tabs-1">
            <uc2:DsPrncfixed ID="dsPrncfixed" runat="server" />
        </div>
        <div id="tabs-2">
            <uc3:DsMasdue ID="dsMasdue" runat="server" />
        </div>
    </div>
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
</asp:Content>

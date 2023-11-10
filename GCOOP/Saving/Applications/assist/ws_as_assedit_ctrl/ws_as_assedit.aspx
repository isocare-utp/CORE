<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_as_assedit.aspx.cs" Inherits="Saving.Applications.assist.ws_as_assedit_ctrl.ws_as_assedit" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsCont.ascx" TagName="DsCont" TagPrefix="uc2" %>
<%@ Register Src="DsContSTM.ascx" TagName="DsContSTM" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMBNo();
            }
            else if (c == "asscontract_no") {
                PostAssContNo();
            }
        }

        function OnDsMainClicked(s, r, c) {
            //alert(c);
            if (c == "b_search") {
                Gcoop.OpenIFrame2(650, 600, 'wd_as_member_search.aspx', '')
            }
        }

        function OnDsContItemChanged(s, r, c, v) {
            if (c == "moneytype_code") {
                PostSetAttrib();
            }
            else if (c == "expense_bank") {
                PostSetAttrib();
            }
            else if (c == "send_system") {
                PostSetAttrib();
            }
        }

        function OnDsContClicked(s, r, c) {
        }

        function OnDsContSTMItemChanged(s, r, c, v) {
        }

        function OnDsContSTMClicked(s, r, c) {
            if (c == "b_del") {
                dsPaytype.SetRowFocus(r);
                if (confirm("ลบข้อมูลแถวที่ " + (r + 1) + " ?") == true) {
                    PostDelSTM();
                }
            }
        }

        function OnClickNewRowSTM() {
            PostNewSTM();
        }

        function GetMembNoFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno.trim());
            PostMBNo();
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
            width: 760px;
        }
    </style>
    <script type="text/javascript">
        Number.prototype.round = function (p) {
            p = p || 10;
            return parseFloat(this.toFixed(p));
        };

        $(function () {
            //alert($("#tabs").tabs()); //ชื่อฟิวส์

            var tabIndex = Gcoop.GetEl("hdTabIndex").value; // Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
            $("#tabs").tabs({
                active: tabIndex,
                activate: function (event, ui) {
                    $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <div id="tabs">
        <ul style="font-size: 12px;">
            <li><a href="#tabs-1">รายละเอียด</a></li>
            <li><a href="#tabs-2">Statement</a></li>
        </ul>
        <div id="tabs-1">
            <uc2:DsCont ID="dsCont" runat="server" />
        </div>
        <div id="tabs-2">
            <span class="NewRowLink" onclick="OnClickNewRowSTM()">เพิ่มแถว</span>
            <uc3:DsContSTM ID="dsContSTM" runat="server" />
        </div>
    </div>
    <br />
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
</asp:Content>

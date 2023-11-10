<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_as_ucfassisttype.aspx.cs" Inherits="Saving.Applications.assist.ws_as_ucfassisttype_ctrl.ws_as_ucfassisttype" %>

<%@ Register Src="DsCriteria.ascx" TagName="DsCriteria" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<%@ Register Src="DsPaytype.ascx" TagName="DsPaytype" TagPrefix="uc3" %>
<%@ Register Src="DsMbtype.ascx" TagName="DsMbtype" TagPrefix="uc4" %>
<%@ Register Src="DsAssCut.ascx" TagName="DsAssCut" TagPrefix="uc5" %>
<%@ Register Src="DsAssLoan.ascx" TagName="DsAssLoan" TagPrefix="uc6" %>
<%@ Register Src="DsAssPause.ascx" TagName="DsAssPause" TagPrefix="uc7" %>
<%@ Register Src="DsAssins.ascx" TagName="DsAssins" TagPrefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //ประกาศตัวแปร ควรอยู่บริเวณบนสุดใน tag <script>
        var dsCriteria = new DataSourceTool();
        var dsDetail = new DataSourceTool();
        var dsPaytype = new DataSourceTool();
        var dsMbtype = new DataSourceTool();
        var dsAssCut = new DataSourceTool();
        var dsAssLoan = new DataSourceTool();
        var dsAssPause = new DataSourceTool();
        var dsAssins = new DataSourceTool();

        function OnDsCriteriaItemChanged(s, r, c, v) {
            //alert(v);
            //alert(dsAssisttype.GetItem(0, "assisttype_code"));
            //alert(dsCriteria.GetItem(1, "assisttype_code"));
            PostSendAssCode();
        }
        function OnClickNewRow() {
            Gcoop.OpenIFrame2("520", "270", "wd_as_addassisttype.aspx", "");
        }

        function OnClickDeleteRow() {
            var assisttype_code = dsAssisttype.GetItem(0, "assisttype_code");
            if (assisttype_code == "00") {
            alert("กรุณาเลือกประเภทสวัสดิการ")
            }
            else {
                if (confirm("ยืนยันการลบประเภทสวัสดิการ!!!") == true) {
                    PostDelAssist();
                }
            }
        }

        function OnClickNewRowPaytype() {
            PostNewRowPaytype();
        }

        function OnDsPaytypeItemChanged(s, r, c, v) {
        }

        function OnDsPaytypeClicked(s, r, c) {
            if (c == "b_del") {
                dsPaytype.SetRowFocus(r);
                if (confirm("ลบข้อมูลแถวที่ " + (r + 1) + " ?") == true) {
                    PostDelPaytype();
                }
            }
        }
        function OnDsCriteriaClicked(s, r, c) {
        }

        function OnDsDetailClicked(s, r, c) {
            if (c == "b_cal") {
                //dsCriteria.GetItem(1, "assisttype_code");
                var assisttype_code = dsAssisttype.GetItem(0, "assisttype_code");
                //var assisttype_code = "80";
                Gcoop.OpenIFrame2("1219", "600", "wd_ass_set_calculator.aspx", "?assisttype_code=" + assisttype_code);
            } //console.log(assisttype_code)
        }

        function OnDsMbtypeItemChanged(s, r, c, v) {
        }

        function OnDsMbtypeClicked(s, r, c) {
        }

        function OnDsAssCutItemChanged(s, r, c, v) {
        }

        function OnDsAssCutClicked(s, r, c) {
        }
        function OnDsAssinsItemChanged(s, r, c, v) {
        }
        function OnDsAssinsClicked(s, r, c) {
        }
        function GetValueFromDlg(assisttype_code) {
            dsAssisttype.SetItem(0, "assisttype_code", assisttype_code);
            PostNewAssCode();
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
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
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มประเภทสวัสดิการ</span> <span
        class="NewRowLink" style="float: right;" onclick="OnClickDeleteRow()">ลบประเภทสวัสดิการ</span>
    <uc1:DsCriteria ID="dsCriteria" runat="server" />
    <br />
    <div id="tabs">
        <ul style="font-size: 12px;">
            <li><a href="#tabs-1">ข้อกำหนดสวัสดิการ</a></li>
            <li><a href="#tabs-2">เงื่อนไขการจ่ายสวัสดิการ</a></li>
            <li><a href="#tabs-3">สวัสดิการที่นำมาหักออก</a></li>
            <li><a href="#tabs-4">ประเภทเงินกู้ที่นำมาคำนวณ</a></li>
            <li><a href="#tabs-5">จำกัดสวัสดิการ</a></li>
            <li><a href="#tabs-6">ตรวจสอบประกัน</a></li>
        </ul>
        <div id="tabs-1">
            <uc2:DsDetail ID="dsDetail" runat="server" />
        </div>
     <%--   <div id="tabs-2">
            <uc4:DsMbtype ID="dsMbtype" runat="server" Visible="False" />
      </div> --%> 
        <div id="tabs-2">
            <span class="NewRowLink" onclick="OnClickNewRowPaytype()">เพิ่มแถว</span>
            <uc3:DsPaytype ID="dsPaytype" runat="server" />
        </div>
        <div id="tabs-3">
            <uc5:DsAssCut ID="dsAssCut" runat="server" />
        </div>
        <div id="tabs-4">
            <uc6:DsAssLoan ID="dsAssLoan" runat="server" />
        </div>
        <div id="tabs-5">
            <uc7:DsAssPause ID="dsAssPause" runat="server" />
        </div>
        <div id="tabs-6">
            <uc8:DsAssins ID="dsAssins" runat="server" />
        </div>
    </div>
    <br />
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
    <asp:HiddenField ID="hdSaveChk_GPA" runat="server" />
    <asp:HiddenField ID="hdInertRow" runat="server" />
    <asp:HiddenField ID="hdCalDay" runat="server" />
    <asp:HiddenField ID="hdActMemno" runat="server" />
</asp:Content>

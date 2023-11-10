<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_as_assdetail.aspx.cs" Inherits="Saving.Applications.assist.ws_as_assdetail_ctrl.ws_as_assdetail" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsCont.ascx" TagName="DsCont" TagPrefix="uc3" %>
<%@ Register Src="DsContSTM.ascx" TagName="DsContSTM" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //ประกาศตัวแปร ควรอยู่บริเวณบนสุดใน tag <script>
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();
        var dsCont = new DataSourceTool();
        var dsContSTM = new DataSourceTool();

        function OnDsMainClicked(s, r, c) {
            //alert(c);
            if (c == "b_search") {
                Gcoop.OpenIFrame2(650, 600, 'wd_as_member_search.aspx', '')
            }
        }
        function GetMembNoFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno.trim());
            PostMBNo();
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMBNo();
            }
        }

        function OnDsListClicked(s, r, c) {
            Gcoop.GetEl("hdRow").value = r;

            if (c == "asscontract_no" || c == "approve_date" || c == "assisttype_code") {
                PostContNo();
            }
        }

     
        function OnDsContClicked(s, r, c, v) {
            if (c == "b_eimg") {
                var assistreq_docno = dsCont.GetItem(0, "assistreq_docno");
                Gcoop.OpenIFrameUploadImg("", "001", "assist_docno", assistreq_docno);
            }
        }


        function Validate() {
        }

        function SheetLoadComplete() {
            var r = Gcoop.GetEl("hdRow").value;
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                if (i == r) {
                    dsList.GetElement(i, "assisttype_code").style.background = "#FFFF99";
                    dsList.GetElement(i, "asscontract_no").style.background = "#FFFF99";
                    dsList.GetElement(i, "approve_date").style.background = "#FFFF99";
                }
                else {
                    dsList.GetElement(i, "assisttype_code").style.background = "#FFFFFF";
                    dsList.GetElement(i, "asscontract_no").style.background = "#FFFFFF";
                    dsList.GetElement(i, "approve_date").style.background = "#FFFFFF";
                }
            }

            var ls_contsts = dsCont.GetItem(0, "asscont_status");
            if (ls_contsts == -9) {
                dsCont.GetElement(0, "statusdesc").style.background = "#FF0000";
                dsCont.GetElement(0, "statusdesc").style.color = "#FFFFFF";
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
            width: 540px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var tabIndex = Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
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
    <strong style="font-size: 12px;">รายการสวัสดิการ</strong>
    <table>
        <tr>
            <td valign="top" style="width: 220px">
                <uc2:DsList ID="dsList" runat="server" />
            </td>
            <td valign="top" style="width: 540px">
                <div id="tabs">
                    <ul style="font-size: 12px;">
                        <li><a href="#tabs-1">รายละเอียด</a></li>
                        <li><a href="#tabs-2">ความเคลื่อนไหว</a></li>
                    </ul>
                    <div id="tabs-1">
                        <uc3:DsCont ID="dsCont" runat="server" />
                    </div>
                    <div id="tabs-2">
                        <uc4:DsContSTM ID="dsContSTM" runat="server" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
    <asp:HiddenField ID="hdRow" Value="0" runat="server" />
    <asp:HiddenField ID="HdTokenIMG" runat="server" />
</asp:Content>


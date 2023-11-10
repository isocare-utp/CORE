<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_sharetype_detail.aspx.cs" Inherits="Saving.Applications.mbshr.ws_sl_sharetype_detail_ctrl.ws_sl_sharetype_detail" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<%@ Register Src="DsMthrate.ascx" TagName="DsMthrate" TagPrefix="uc3" %>
<%@ Register Src="DsMembtype.ascx" TagName="DsMembtype" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;
        var dsMthrate = new DataSourceTool;
        var dsMembtype = new DataSourceTool;
        
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {          
        }
        function AddNewGroup() {
            Gcoop.OpenIFrame("520", "250", "ws_dlg_sl_shareucftype.aspx", "");
        }
        function OnDsMainItemChanged(s, r, c) {

            var ls_sharetype_code = dsMain.GetItem(0, "sharetype_code");           
            dsMain.SetItem(0, "sharetype_code", ls_sharetype_code);
            RetrieveDetail(); 
        }
        function GetValueFromDlg(sharetype_code) {
            //dsMain.SetItem(0, "sharetype_code", sharetype_code);
            alert("บันทึกสำเร็จ");
            RetrieveShare();
        }
        function OnDsDetailClicked(s, r, c) {
        }

        function OnDsDetailItemChanged(s, r, c, v) {
            if (c == "chgcount_type") {
                if (v == "1") {
                    dsDetail.GetElement(0, "chgcountall_amt").readOnly = false;
                    dsDetail.GetElement(0, "chgcountall_amt").style.background = "#FFFFFF";

                    dsDetail.GetElement(0, "chgcountadd_amt").readOnly = true;
                    dsDetail.GetElement(0, "chgcountadd_amt").style.background = "#CCCCCC";
                    dsDetail.GetElement(0, "chgcountlow_amt").readOnly = true;
                    dsDetail.GetElement(0, "chgcountlow_amt").style.background = "#CCCCCC";
                    dsDetail.GetElement(0, "chgcountstop_amt").readOnly = true;
                    dsDetail.GetElement(0, "chgcountstop_amt").style.background = "#CCCCCC";
                    dsDetail.GetElement(0, "chgcountcont_amt").readOnly = true;
                    dsDetail.GetElement(0, "chgcountcont_amt").style.background = "#CCCCCC";
                } else {
                    dsDetail.GetElement(0, "chgcountall_amt").readOnly = true;
                    dsDetail.GetElement(0, "chgcountall_amt").style.background = "#CCCCCC";

                    dsDetail.GetElement(0, "chgcountadd_amt").readOnly = false;
                    dsDetail.GetElement(0, "chgcountadd_amt").style.background = "#FFFFFF";
                    dsDetail.GetElement(0, "chgcountlow_amt").readOnly = false;
                    dsDetail.GetElement(0, "chgcountlow_amt").style.background = "#FFFFFF";
                    dsDetail.GetElement(0, "chgcountstop_amt").readOnly = false;
                    dsDetail.GetElement(0, "chgcountstop_amt").style.background = "#FFFFFF";
                    dsDetail.GetElement(0, "chgcountcont_amt").readOnly = false;
                    dsDetail.GetElement(0, "chgcountcont_amt").style.background = "#FFFFFF";
                }
            }
        }

        function OnDsMthrateClicked(s, r, c) {
            if (c == "b_del") {
                dsMthrate.SetRowFocus(r);
                PostDeleteRow();
            }
        }

        function OnDsMthrateItemChanged(s, r, c, v) {
        }

        function OnDsMembtypeClicked(s, r, c) {
        }

        function OnDsMembtypeItemChanged(s, r, c, v) {
            if (c == "membtype") {
                PostMembtype();
            }
        }
        function SheetLoadComplete() {

            if (dsDetail.GetItem(0, "chgcount_type") == "1") {
                dsDetail.GetElement(0, "chgcountall_amt").readOnly = false;
                dsDetail.GetElement(0, "chgcountall_amt").style.background = "#FFFFFF";

                dsDetail.GetElement(0, "chgcountadd_amt").readOnly = true;
                dsDetail.GetElement(0, "chgcountadd_amt").style.background = "#CCCCCC";
                dsDetail.GetElement(0, "chgcountlow_amt").readOnly = true;
                dsDetail.GetElement(0, "chgcountlow_amt").style.background = "#CCCCCC";
                dsDetail.GetElement(0, "chgcountstop_amt").readOnly = true;
                dsDetail.GetElement(0, "chgcountstop_amt").style.background = "#CCCCCC";
                dsDetail.GetElement(0, "chgcountcont_amt").readOnly = true;
                dsDetail.GetElement(0, "chgcountcont_amt").style.background = "#CCCCCC";
            } else {
                dsDetail.GetElement(0, "chgcountall_amt").readOnly = true;
                dsDetail.GetElement(0, "chgcountall_amt").style.background = "#CCCCCC";

                dsDetail.GetElement(0, "chgcountadd_amt").readOnly = false;
                dsDetail.GetElement(0, "chgcountadd_amt").style.background = "#FFFFFF";
                dsDetail.GetElement(0, "chgcountlow_amt").readOnly = false;
                dsDetail.GetElement(0, "chgcountlow_amt").style.background = "#FFFFFF";
                dsDetail.GetElement(0, "chgcountstop_amt").readOnly = false;
                dsDetail.GetElement(0, "chgcountstop_amt").style.background = "#FFFFFF";
                dsDetail.GetElement(0, "chgcountcont_amt").readOnly = false;
                dsDetail.GetElement(0, "chgcountcont_amt").style.background = "#FFFFFF";
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
            width: 760px;
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
    <span class="NewRowLink" onclick="AddNewGroup()" style="font-size: small;">เพิ่มประเภทหุ้นใหม่</span>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">รายละเอียด</a></li>
            <li><a href="#tabs-2">การส่งหุ้นขั้นต่ำรายเดือน</a></li>
        </ul>
        <div id="tabs-1">
            <uc2:DsDetail ID="dsDetail" runat="server" />
        </div>
        <div id="tabs-2">
            <uc4:DsMembtype ID="dsMembtype" runat="server" />
            <br />
            <span class="NewRowLink" onclick="PostInsertRow()" style="font-size: small">เพิ่มแถว
            </span>
            <uc3:DsMthrate ID="dsMthrate" runat="server" />
        </div>
    </div>
    <asp:HiddenField ID="hdTabIndex" runat="server" />   
</asp:Content>

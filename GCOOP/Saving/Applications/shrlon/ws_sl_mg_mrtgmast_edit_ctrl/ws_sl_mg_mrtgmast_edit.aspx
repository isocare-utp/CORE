<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_mg_mrtgmast_edit.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl.ws_sl_mg_mrtgmast_edit" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<%@ Register Src="DsDetailDeed.ascx" TagName="DsDetailDeed" TagPrefix="uc3" %>
<%@ Register Src="DsDetailNs3.ascx" TagName="DsDetailNs3" TagPrefix="uc4" %>
<%@ Register Src="DsDetailCondo.ascx" TagName="DsDetailCondo" TagPrefix="uc5" %>
<%@ Register Src="DsMrtger.ascx" TagName="DsMrtger" TagPrefix="uc6" %>
<%@ Register Src="DsAutz.ascx" TagName="DsAutz" TagPrefix="uc7" %>
<%@ Register Src="DsRefcollno.ascx" TagName="DsRefcollno" TagPrefix="uc8" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc9" %>
<%@ Register Src="DsDetailBding.ascx" TagName="DsDetailBding" TagPrefix="uc10" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_search") {
                Gcoop.OpenDlg("630", "720", "w_dlg_member_search.aspx", "")
            }
        }

        function GetMemDetFromDlg(memberno, prename_desc, memb_name, memb_surname, card_person) {
            dsMain.SetItem(0, "member_no", memberno);
            PostMemberNo();

        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        }

        function Setfocus() {
            dsMain.Focus(0, "member_no");
        }

        function OnDsListClicked(s, r, c, v) {
            if (c == "mrtgmast_no" || c == "assettype_desc" || c == "cp_mrtgtype") {
                PostMrtgNo();
            }
        }

        function OnDsDetailItemChanged(s, r, c, v) {
            if (c == "assettype_code") {
                PostAssettype();
            } else if (c == "mortgage_type") {

                var mortgage_type = dsDetail.GetItem(0, "mortgage_type");

                if (mortgage_type == "0") {
                    dsDetail.GetElement(0, "mortgage_landnum").readOnly = true;
                    dsDetail.GetElement(0, "mortgage_partamt").readOnly = true;
                    dsDetail.GetElement(0, "mortgage_partall").readOnly = true;
                    dsDetail.GetElement(0, "mortgage_partname").readOnly = true;
                    dsDetail.GetElement(0, "mortgage_landnum").style.background = "#CCCCCC";
                    dsDetail.GetElement(0, "mortgage_partamt").style.background = "#CCCCCC";
                    dsDetail.GetElement(0, "mortgage_partall").style.background = "#CCCCCC";
                    dsDetail.GetElement(0, "mortgage_partname").style.background = "#CCCCCC";

                } else if (mortgage_type == "1") {
                    dsDetail.GetElement(0, "mortgage_landnum").readOnly = true;
                    dsDetail.GetElement(0, "mortgage_partamt").readOnly = false;
                    dsDetail.GetElement(0, "mortgage_partall").readOnly = false;
                    dsDetail.GetElement(0, "mortgage_partname").readOnly = false;
                    dsDetail.GetElement(0, "mortgage_landnum").style.background = "#CCCCCC";
                    dsDetail.GetElement(0, "mortgage_partamt").style.background = "#FFFFFF";
                    dsDetail.GetElement(0, "mortgage_partall").style.background = "#FFFFFF";
                    dsDetail.GetElement(0, "mortgage_partname").style.background = "#FFFFFF";
                } else {
                    dsDetail.GetElement(0, "mortgage_landnum").readOnly = false;
                    dsDetail.GetElement(0, "mortgage_partamt").readOnly = true;
                    dsDetail.GetElement(0, "mortgage_partall").readOnly = true;
                    dsDetail.GetElement(0, "mortgage_partname").readOnly = true;
                    dsDetail.GetElement(0, "mortgage_landnum").style.background = "#FFFFFF";
                    dsDetail.GetElement(0, "mortgage_partamt").style.background = "#CCCCCC";
                    dsDetail.GetElement(0, "mortgage_partall").style.background = "#CCCCCC";
                    dsDetail.GetElement(0, "mortgage_partname").style.background = "#CCCCCC";
                }
            } else if (c == "collmate_flag") {
                if (v == 0) {
                    dsDetail.GetElement(0, "collmate_memno").readOnly = true;
                    dsDetail.GetElement(0, "collmate_memno").style.background = "#CCCCCC";
                    dsDetail.SetItem(0, "collmate_memno", "");
                    dsDetail.SetItem(0, "cp_matename", "");
                } else {
                    dsDetail.GetElement(0, "collmate_memno").readOnly = false;
                    dsDetail.GetElement(0, "collmate_memno").style.background = "#FFFFFF";
                }
            } else if (c == "collmate_memno") {
                PostMateMemno();
            }
        }

        function OnDsDetailClicked(s, r, c, v) {
            if (c == "b_mrtgsearch") {
                var member_no = dsMain.GetItem(0, "member_no");
                Gcoop.OpenIFrame2("520", "520", "w_dlg_mg_mrtgmast.aspx", "?member_no=" + member_no)
            }
            else if (c == "b_matesearch") {
                Gcoop.OpenIFrame2("630", "580", "w_dlg_sl_member_search_tks.aspx", "")
            }
        }

        function GetValueFromDlg(member_no) {
            dsDetail.SetItem(0, "collmate_memno", member_no);
            PostMateMemno();
        }

        function GetMrtgmastNoFromDlg(mrtgmast_no) {
            dsDetail.SetItem(0, "mrtgmast_no", mrtgmast_no);
            PostMrtgmastNo();
        }

        function OnDsAutzClicked(s, r, c, v) {
            if (c == "b_autzd") {
                Gcoop.OpenIFrame2("650", "500", "w_dlg_mg_autzd_search.aspx", "");
            }
        }

        function GetTemplateNoFromDlg(template_no) {
            Gcoop.GetEl("HdTemplateNo").value = template_no;
            PostTemplateNo();
        }

        function OnDsRefcollnoClicked(s, r, c, v) {
            if (c == "b_del") {
                PostDelRowRefcollno();
            }
            else if (c == "b_collmast") {
                var member_no = dsMain.GetItem(0, "member_no");
                Gcoop.OpenIFrame3("650", "500", "w_dlg_ln_collmast.aspx", "?member_no=" + member_no);
            }
        }

        function GetCollmastNoFromDlg(collmast_no) {
            Gcoop.GetEl("HdCollmastNo").value = collmast_no;
            PostCollmastNo();
        }

        function OnDsDetailBdingItemChanged(s, r, c, v) {
        }

        function OnDsDetailBdingClicked(s, r, c, v) {
        }

        function SheetLoadComplete() {
            var mortgage_type = dsDetail.GetItem(0, "mortgage_type");

            if (mortgage_type == "0") {
                dsDetail.GetElement(0, "mortgage_landnum").readOnly = true;
                dsDetail.GetElement(0, "mortgage_partamt").readOnly = true;
                dsDetail.GetElement(0, "mortgage_partall").readOnly = true;
                dsDetail.GetElement(0, "mortgage_partname").readOnly = true;
                dsDetail.GetElement(0, "mortgage_landnum").style.background = "#CCCCCC";
                dsDetail.GetElement(0, "mortgage_partamt").style.background = "#CCCCCC";
                dsDetail.GetElement(0, "mortgage_partall").style.background = "#CCCCCC";
                dsDetail.GetElement(0, "mortgage_partname").style.background = "#CCCCCC";
            }

            var collmate_flag = dsDetail.GetItem(0, "collmate_flag");
            if (collmate_flag == 0) {
                dsDetail.GetElement(0, "collmate_memno").readOnly = true;
                dsDetail.GetElement(0, "collmate_memno").style.background = "#CCCCCC";
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
            width: 770px;
            background-color: #FFFFCC;
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
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
    <asp:HiddenField ID="HdTemplateNo" runat="server" />
    <asp:HiddenField ID="HdCollmastNo" runat="server" />
    <br />
    <table width="770px;">
        <tr>
            <td>
                <uc1:DsMain ID="dsMain" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top" width="30%">
                <uc9:DsList ID="dsList" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="tabs">
                    <ul>
                        <li><a href="#tabs-1">รายละเอียดจำนอง</a></li>
                        <li><a href="#tabs-2">ผู้จำนอง </a></li>
                        <li><a href="#tabs-3">ผู้รับมอบอำนาจ</a></li>
                        <li><a href="#tabs-4">อ้างอิงหลักทรัพย์</a></li>
                    </ul>
                    <div id="tabs-1">
                        <div align="center">
                            <uc2:DsDetail ID="dsDetail" runat="server" />
                            <uc3:DsDetailDeed ID="dsDetailDeed" runat="server" />
                            <uc4:DsDetailNs3 ID="dsDetailNs3" runat="server" />
                            <uc5:DsDetailCondo ID="dsDetailCondo" runat="server" />
                            <uc10:DsDetailBding ID="dsDetailBding" runat="server" />
                            <br />
                        </div>
                    </div>
                    <div id="tabs-2">
                        <div align="center">
                            <uc6:DsMrtger ID="dsMrtger" runat="server" />
                        </div>
                    </div>
                    <div id="tabs-3">
                        <div align="center">
                            <uc7:DsAutz ID="dsAutz" runat="server" />
                        </div>
                    </div>
                    <div id="tabs-4">
                        <span class="NewRowLink" onclick="PostInsertRowRefcollno()" style="font-size: small;">
                            เพิ่มแถว </span>
                        <div align="center">
                            <uc8:DsRefcollno ID="dsRefcollno" runat="server" />
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

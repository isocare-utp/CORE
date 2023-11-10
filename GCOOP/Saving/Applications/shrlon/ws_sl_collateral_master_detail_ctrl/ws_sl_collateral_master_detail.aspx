<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_collateral_master_detail.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_detail_ctrl.ws_sl_collateral_master_detail" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsListSum.ascx" TagName="DsListSum" TagPrefix="uc3" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc4" %>
<%@ Register Src="DsColluse.ascx" TagName="DsColluse" TagPrefix="uc5" %>
<%@ Register Src="DsColluseSum.ascx" TagName="DsColluseSum" TagPrefix="uc6" %>
<%@ Register Src="DsMemco.ascx" TagName="DsMemco" TagPrefix="uc7" %>
<%@ Register Src="DsCollprop.ascx" TagName="DsCollprop" TagPrefix="uc8" %>
<%@ Register Src="DsReview.ascx" TagName="DsReview" TagPrefix="uc9" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;
        var dsList = new DataSourceTool;
        var dsMemco = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_memsearch") {
                Gcoop.OpenDlg("630", "720", "w_dlg_member_search.aspx", "")
            }

        }
        function GetMemDetFromDlg(memberno, prename_desc, memb_name, memb_surname, card_person) {

            dsMain.SetItem(0, "member_no", memberno);
            // objdw_main.AcceptText();
            // Gcoop.GetEl("Hdfmember_no").value = memberno;
            PostMemberNo();

        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        }
        function OnDsListClicked(s, r, c, v) {
            if (c == "collmast_refno" || c == "cp_redeem") {
                dsList.SetRowFocus(r);
                PostCollmast();
            }

        }
        function OnDsDetailClicked(s, r, c, v) {
            if (c == "b_landsideno") {
                Gcoop.OpenDlg("630", "720", "w_dlg_sl_collmaster_search.aspx", "")
            }
        }
        function GetValueFromDlgCollmast(collmast_refno) {

            dsDetail.SetItem(0, "landside_no", collmast_refno);
        }
        function OnDsMemcoClicked(s, r, c, v) {
            if (c == "b_search") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_search_mb.aspx', '');
            }
        }
        function OnDsMemcoItemChanged(s, r, c, v) {
            if (c == "memco_no") {
                
                Gcoop.GetEl("Hdmemco_no").value = dsMemco.GetItem(r, "memco_no");
                PostGetMember();
            }
        }
        function GetMemSeachMb(memberno) {
            //dsMemco.SetRowFocus(r);
            //dsMemco.SetItem(r, "memco_no", memberno);

            Gcoop.GetEl("Hdmemco_no").value = memberno;

            PostGetMember();

        }


        $(function () {
            $("#tabs").tabs();
        });
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
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
    <uc3:DsListSum ID="dsListSum" runat="server" />
    <br />
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">รายละเอียดหลักทรัพย์ </a></li>
            <li><a href="#tabs-2">ผู้มีกรรมสิทธิ์กู้ร่วม </a></li>
            <li><a href="#tabs-3">ทบทวนราคา</a></li>
        </ul>
        <div id="tabs-1">
            <div>
                <div align="center">
                    <uc4:DsDetail ID="dsDetail" runat="server" />
                    <br />
                    <uc5:DsColluse ID="dsColluse" runat="server" />
                    <uc6:DsColluseSum ID="dsColluseSum" runat="server" />
                </div>
            </div>
        </div>
        <div id="tabs-2">
            <div>
                <div align="center">
                    <uc7:DsMemco ID="dsMemco" runat="server" />
                    <uc8:DsCollprop ID="dsCollprop" runat="server" />
                </div>
            </div>
        </div>
        <div id="tabs-3">
            <div>
                <div align="center">
                    <uc9:DsReview ID="dsReview" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="Hdmemco_no" runat="server" />
    <asp:HiddenField ID="Hdfredeemflag" runat="server" />
</asp:Content>

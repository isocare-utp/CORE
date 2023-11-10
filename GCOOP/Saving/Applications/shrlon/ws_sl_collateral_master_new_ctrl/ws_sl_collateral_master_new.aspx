<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_collateral_master_new.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl.ws_sl_collateral_master_new" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<%@ Register Src="DsMemco.ascx" TagName="DsMemco" TagPrefix="uc3" %>
<%@ Register Src="DsCollprop.ascx" TagName="DsCollprop" TagPrefix="uc4" %>
<%@ Register Src="DsReview.ascx" TagName="DsReview" TagPrefix="uc5" %>
<%@ Register Src="DsCollDetail.ascx" TagName="DsCollDetail" TagPrefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function MenubarOpen() {
            Gcoop.OpenIFrame3Extend("730", "550", "ws_dlg_sl_editcollateral_master.aspx", "");
        }
        function GetItem(member_no, collmast_no) {
            Gcoop.RemoveIFrame();
            Gcoop.GetEl("Hdmember_no").value = member_no;
            Gcoop.GetEl("Hdcollmast_no").value = collmast_no;
            dsMain.SetItem(0, "member_no", member_no);
            dsDetail.SetItem(0, "collmast_no", collmast_no);
            PostSearchRetrieve();
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

        function OnDsDetailItemChanged(s, r, c, v) {
            if (c == "redeem_flag") {
                if (v == 0) {
                    dsDetail.GetElement(0, "redeem_date").readOnly = true;
                    dsDetail.GetElement(0, "redeem_date").style.background = "#CCCCCC";
                } else {
                    dsDetail.GetElement(0, "redeem_date").readOnly = false;
                    dsDetail.GetElement(0, "redeem_date").style.background = "#FFFFFF";
                }
            } if (c == "blindland_flag") {
                if (v == 0) {
                    dsDetail.GetElement(0, "landside_no").readOnly = true;
                    dsDetail.GetElement(0, "landside_no").style.background = "#CCCCCC";
                } else {
                    dsDetail.GetElement(0, "landside_no").readOnly = false;
                    dsDetail.GetElement(0, "landside_no").style.background = "#FFFFFF";
                }
            }
            if (c == "total_area1" || c == "price_area1" || c == "landest_percent" || c == "houseestimate2_amt" || c == "houseest_percent" || c == "otherest_amt" || c == "othernet_percent") {

                CalEstimate();
            }
        }
        function OnDsMemcoItemChanged(s, r, c, v) {
            if (c == "memco_no") {
                var memberno = dsMemco.GetItem(r, "memco_no");
                Gcoop.GetEl("Hdmemco_no").value = memberno;
                PostMemcoNo();
            }
        }
        function GetValueFromDlgCollmast(collmast_refno) {

            dsDetail.SetItem(0, "landside_no", collmast_refno);
        }
        function OnDsDetailClicked(s, r, c, v) {
            if (c == "b_landsideno") {
                Gcoop.OpenDlg("630", "720", "w_dlg_sl_collmaster_search.aspx", "");
            }
        }
        function OnDsMemcoClicked(s, r, c) {
            if (c == "b_delete") {
                dsMemco.SetRowFocus(r);
                PostDeleteRowMemco();
            } else if (c == "b_search") {
                Gcoop.OpenDlg("580", "590", "w_dlg_search_mb.aspx", "");
            }
        }
        function GetMemSeachMb(memberno) {
            //dsMemco.SetRowFocus(r);
            //dsMemco.SetItem(r, "memco_no", memberno);

            Gcoop.GetEl("Hdmemco_no").value = memberno;

            PostMemcoNo();

        }
        function OnDsCollpropClicked(s, r, c) {
            if (c == "b_delprop") {
                dsCollprop.SetRowFocus(r);
                PostDeleteRowCollprop();
            }
        }
        function OnDsReviewClicked(s, r, c) {
            if (c == "b_delreview") {
                dsReview.SetRowFocus(r);
                PostDeleteRowReview();
            }
        }

        function SheetLoadComplete() {
            if (dsDetail.GetItem(0, "redeem_flag") == "0") {
                dsDetail.GetElement(0, "redeem_date").readOnly = true;
                dsDetail.GetElement(0, "redeem_date").style.background = "#CCCCCC";
            } else {
                dsDetail.GetElement(0, "redeem_date").readOnly = false;
                dsDetail.GetElement(0, "redeem_date").style.background = "#FFFFFF";
            }

            if (dsDetail.GetItem(0, "blindland_flag") == "0") {
                dsDetail.GetElement(0, "landside_no").readOnly = true;
                dsDetail.GetElement(0, "landside_no").style.background = "#CCCCCC";
            } else {
                dsDetail.GetElement(0, "landside_no").readOnly = false;
                dsDetail.GetElement(0, "landside_no").style.background = "#FFFFFF";
            }
        }

        function CalEstimate() {
            var total_area1 = dsDetail.GetItem(0, "total_area1");
            var price_area1 = dsDetail.GetItem(0, "price_area1");
            var landest_percent = dsDetail.GetItem(0, "landest_percent");
            var landestimate1_amt = 0.00;
            var landestimate_amt = 0.00;
            landestimate1_amt = total_area1 * price_area1;
            landestimate_amt = landestimate1_amt * (landest_percent / 100);
            dsDetail.SetItem(0, "landestimate1_amt", landestimate1_amt);
            dsDetail.SetItem(0, "landestimate_amt", landestimate_amt);
            dsDetail.SetItem(0, "total_area2", total_area1);
            dsDetail.SetItem(0, "price_area2", price_area1);
            dsDetail.SetItem(0, "landestimate2_amt", landestimate1_amt);

            var houseest_percent = dsDetail.GetItem(0, "houseest_percent");
            var houseestimate2_amt = dsDetail.GetItem(0, "houseestimate2_amt");
            var houseestimate_amt = 0.00;
            houseestimate_amt = houseestimate2_amt * (houseest_percent / 100);
            dsDetail.SetItem(0, "houseestimate_amt", houseestimate_amt);

            var otherest_amt = dsDetail.GetItem(0, "otherest_amt");
            var othernet_percent = dsDetail.GetItem(0, "othernet_percent");
            var othernet_amt = 0.00;
            othernet_amt = otherest_amt * (othernet_percent / 100);
            dsDetail.SetItem(0, "othernet_amt", othernet_amt);

            var estimate_price = landestimate_amt + houseestimate_amt + othernet_amt;
            dsDetail.SetItem(0, "estimate_price", estimate_price);
            dsDetail.SetItem(0, "mortgage_price", estimate_price);


        }
        function OnDsCollDetailItemChanged(s, r, c, v) {
        
         }
         function OnDsCollDetailClicked(s, r, c) { 
        
        }
        $(function () {
            $("#tabs").tabs();
            $("#tabs2").tabs();
            $("#tabs3").tabs();
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
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsDetail ID="dsDetail" runat="server" />
    <br />
    <div align="right" style="width: 706px">
        <span class="NewRowLink" onclick="PostInsertRowMemco()">เพิ่มผู้ยื่นกู้ </span>
    </div>
    <uc3:DsMemco ID="dsMemco" runat="server" />
    <div align="right" style="width: 706px">
        <span class="NewRowLink" onclick="PostInsertRowCollprop()">เพิ่มผู้มีกรรมสิทธิ์
        </span>
    </div>
    <uc4:DsCollprop ID="dsCollprop" runat="server" />
    <div align="right" style="width: 706px">
        <span class="NewRowLink" onclick="PostInsertRowReview()">เพิ่มแถว </span>
    </div>
    <uc5:DsReview ID="dsReview" runat="server" />
    <uc6:DsCollDetail ID="dsCollDetail" runat="server" />
    <asp:HiddenField ID="Hdmemco_no" runat="server" />
    <asp:HiddenField ID="Hdmember_no" runat="server" />
    <asp:HiddenField ID="Hdcollmast_no" runat="server" />
</asp:Content>

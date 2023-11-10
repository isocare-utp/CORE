<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_mbshr_share_gift.aspx.cs" 
Inherits="Saving.Applications.mbshr.ws_mbshr_share_gift_ctrl.ws_mbshr_share_gift" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function OnDsMainClicked(s, r, c) {
            if (c == "bprocess") {
                var v = confirm("ยืนยันการประมวลรางวัลหุ้น");
                if (v == true) {
                    PostProcessShrGift();
                }
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "share_amount") {
                PostShrAmt();
            }
            else if (c == "member_type") {
                PostRetrieveMain();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">    
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_recpay_wrt.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_recpay_wrt_ctrl.ws_sl_recpay_wrt" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        var dsMain = new DataSourceTool();
        function Validate() {

            return confirm("ยืนยันการบันทึกข้อมูล");

        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostRetrieve();
            } else if (c == "cash_type") {
                PostMoneyCode();
            } else if (c == "tofrom_accid") {
            
            }
        }

        function OnDsMainClicked(s, r, c) {
            
            
             }

        function OnDsDsListItemChanged(s, r, c, v) {
            if (c == "operate_flag") {
                var op_flag = dsList.GetItem(r, "operate_flag");
                var itempay_amt = dsMain.GetItem(0, "itempay_amt");
                var itempay = 0;
                if (op_flag == 1) {
                    itempay = dsList.GetItem(r, "itempay_amt");
                    itempay_amt += itempay
                    dsMain.SetItem(0, "itempay_amt", itempay_amt);
                } else if (op_flag == 0) {
                    itempay = dsList.GetItem(r, "itempay_amt");
                    itempay_amt -= itempay
                    dsMain.SetItem(0, "itempay_amt", itempay_amt);
                
                }
            }
        
        }

        function OnDsListClicked(s, r, c) {

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">

    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>

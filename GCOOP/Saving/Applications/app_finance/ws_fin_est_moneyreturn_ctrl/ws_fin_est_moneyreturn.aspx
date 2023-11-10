<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fin_est_moneyreturn.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_est_moneyreturn_ctrl.ws_fin_est_moneyreturn" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();
        function Validate() {

            return confirm("ยืนยันการบันทึกข้อมูล");

        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "choosemem_flag") {
                PostBlank();
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_ret") {
                PostRetrieveList();
            }

        }

        function OnDsDsListItemChanged(s, r, c, v) {
            if (c == "operate_flag") {
                var cnt = 0;
                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    if (dsList.GetItem(i, "operate_flag") == 1) {
                        cnt++
                    }
                }
                $("#ctl00_ContentPlace_Label1").text("จำนวนที่เลือก " + cnt.toString() + " รายการ");
            }
        }

        function OnDsListClicked(s, r, c) { }


        function SheetLoadComplete() {
            
            $("#chk_all").click(function () {
                var cnt = 0;
                if ($("#chk_all").prop('checked')) {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "operate_flag", 1);
                        cnt++
                    }
                } else {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "operate_flag", 0);
                    }
                }
                $("#ctl00_ContentPlace_Label1").text("จำนวนที่เลือก " + cnt.toString() + " รายการ");
            });

            

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>

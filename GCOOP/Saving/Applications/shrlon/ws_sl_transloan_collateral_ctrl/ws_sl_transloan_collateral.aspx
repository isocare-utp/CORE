<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_transloan_collateral.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_transloan_collateral_ctrl.ws_sl_transloan_collateral" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMembno();
            } else if (c == "loancontract_no") {
                PostLoancont();
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_searchmemb") {
                Gcoop.OpenIFrame2("630", "720", "w_dlg_sl_member_search_tks.aspx", "");
            } else if (c == "b_searchcont") {
                var member_no = dsMain.GetItem(0, "member_no");
                Gcoop.OpenIFrame('630', '600', 'w_dlg_sl_loancontract_search_memno.aspx', "?memno=" + member_no);
            }
        }
        function OnDsListItemChanged(s, r, c, v) {
            if (c == "operate_flag") {
                var row = 0;
                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    if (dsList.GetItem(i, "operate_flag") == "1") {
                        row += 1;
                    }
                }

                var bfprnbal_amt = dsMain.GetItem(0, "bfprnbal_amt");
                var bfintarrear_amt = dsMain.GetItem(0, "bfintarrear_amt");
                var bfperiod_payment = dsMain.GetItem(0, "bfperiod_payment");

                var arr1 = new Array();
                var arr2 = new Array();
                var arr3 = new Array();

                for (var i = 0; i < row; i++) {
                    var bfintarrear_amt1 = bfintarrear_amt / ((row - (i + 1)) + 1);
                    arr1[i] = bfintarrear_amt1.toFixed(2);
                    bfintarrear_amt = bfintarrear_amt - bfintarrear_amt1.toFixed(2);

                    var bfprnbal_amt1 = bfprnbal_amt / ((row - (i + 1)) + 1);
                    arr2[i] = bfprnbal_amt1.toFixed(2);
                    bfprnbal_amt = bfprnbal_amt - bfprnbal_amt1.toFixed(2);

                    var bfperiod_payment1 = bfperiod_payment / ((row - (i + 1)) + 1);
                    arr3[i] = bfperiod_payment1.toFixed(2);
                    bfperiod_payment = bfperiod_payment - bfperiod_payment1.toFixed(2);
                }



                for (var j = 0; j < dsList.GetRowCount(); j++) {
                    if (dsList.GetItem(j, "operate_flag") == "1") {
                        for (var i = 0; i < arr1.length; i++) {
                            dsList.SetItem(j, "trnintarrear_amt", arr1[i]);
                            dsList.SetItem(j, "periodpayintarr_amt", arr1[i]);
                            arr1.shift();
                            break;
                        }
                        for (var i = 0; i < arr2.length; i++) {
                            dsList.SetItem(j, "trnprnbal_amt", arr2[i]);
                            arr2.shift();
                            break;
                        }
                        for (var i = 0; i < arr3.length; i++) {
                            dsList.SetItem(j, "periodpayprn_amt", arr3[i]);
                            arr3.shift();
                            break;
                        }
                    } else {
                        dsList.SetItem(j, "trnprnbal_amt", 0);
                        dsList.SetItem(j, "trnintarrear_amt", 0);
                        dsList.SetItem(j, "periodpayprn_amt", 0);
                        dsList.SetItem(j, "periodpayintarr_amt", 0);
                    }
                }



            }
        }


        function OnDsListClicked(s, r, c) { }

        function GetValueFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno);
            PostMembno();
        }

        function GetContFromDlg(loancontractno) {
            dsMain.SetItem(0, "loancontract_no", loancontractno);
            PostLoancont();
        }


        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>

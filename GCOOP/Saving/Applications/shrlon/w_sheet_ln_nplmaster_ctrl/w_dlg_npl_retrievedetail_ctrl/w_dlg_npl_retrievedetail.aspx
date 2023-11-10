<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_npl_retrievedetail.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.w_dlg_npl_retrievedetail_ctrl.w_dlg_npl_retrievedetail" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var checkAllStatus = true;

        function OnDsMainItemChanged(s, r, c, v) {
        }

        function OnDsMainClicked(s, r, c) {
        }

        function OnClickChecking() {
            var ii = -1;
            if (checkAllStatus) {
                checkAllStatus = false;
                var dsNPL = opener.dsNPL;
                var received_date = dsNPL.GetItem(0, "received_date");
                if (received_date == null) {
                    dsMain.SetItem(0, "receive_date_flag", 1);
                }
                else if (received_date == "1500-01-01 00:00:00") {
                    dsMain.SetItem(0, "receive_date_flag", 1);
                } else {
                    dsMain.SetItem(0, "receive_date_flag", 0);
                }
                var ii = 1;
            } else {
                checkAllStatus = true;
                var ii = 0;
                dsMain.SetItem(0, "receive_date_flag", ii);
            }
            //dsMain.SetItem(0, "last_payment_flag", ii);
            dsMain.SetItem(0, "indict_prnamt_flag", ii);
            dsMain.SetItem(0, "debtor_class_flag", ii);
            dsMain.SetItem(0, "status_flag", ii);
            dsMain.SetItem(0, "period_payment_flag", ii);
            dsMain.SetItem(0, "prn_last_year_flag", ii);
            dsMain.SetItem(0, "int_last_year_flag", ii);
            dsMain.SetItem(0, "int_balance_flag", ii);
        }

        function OnClickOK() {
            var dsNPL = null; //new DataSourceTool();
            if (confirm("ยืนยันการนำข้อมูลไปใช้งานแทนข้อมูลเดิม")) {
                dsNPL = opener.dsNPL;
            } else {
                return;
            }
            if (dsMain.GetItem(0, "receive_date_flag") == 1) {
                dsNPL.SetItem(0, "received_date", dsMain.GetItem(0, "receive_date"));
            }
            //            if (dsMain.GetItem(0, "last_payment_flag") == 1) {
            //                dsNPL.SetItem(0, "last_payment", dsMain.GetItem(0, "last_payment"));
            //            }
            if (dsMain.GetItem(0, "indict_prnamt_flag") == 1) {
                dsNPL.SetItem(0, "indict_prnamt", dsMain.GetItem(0, "indict_prnamt"));
            }
            if (dsMain.GetItem(0, "debtor_class_flag") == 1) {
                dsNPL.SetItem(0, "debtor_class", dsMain.GetItem(0, "debtor_class"));
            }
            if (dsMain.GetItem(0, "status_flag") == 1) {
                dsNPL.SetItem(0, "status", dsMain.GetItem(0, "status"));
            }
            if (dsMain.GetItem(0, "period_payment_flag") == 1) {
                dsNPL.SetItem(0, "period_payment", dsMain.GetItem(0, "period_payment"));
            }
            if (dsMain.GetItem(0, "prn_last_year_flag") == 1) {
                dsNPL.SetItem(0, "prince_last_year", dsMain.GetItem(0, "prn_last_year"));
            }
            if (dsMain.GetItem(0, "int_last_year_flag") == 1) {
                dsNPL.SetItem(0, "int_last_year", dsMain.GetItem(0, "int_last_year"));
            }
            if (dsMain.GetItem(0, "int_balance_flag") == 1) {
                dsNPL.SetItem(0, "int_balance", dsMain.GetItem(0, "int_balance"));
            }
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <div align="center">
        <div style="width: 304px;" align="left">
            <uc1:DsMain ID="dsMain" runat="server" />
            <br />
            <input type="button" value="Check/Uncheck all" onclick="OnClickChecking()" />
            &nbsp;
            <input type="button" value="ตกลง" onclick="OnClickOK()" />
        </div>
    </div>
</asp:Content>

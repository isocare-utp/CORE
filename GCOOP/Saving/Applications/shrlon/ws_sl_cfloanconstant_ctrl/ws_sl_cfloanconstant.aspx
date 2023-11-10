<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_cfloanconstant.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfloanconstant_ctrl.ws_sl_cfloanconstant" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "grtright_contflag") {
                if (v == "1") {
                    dsMain.GetElement(0, "grtright_contract").readOnly = false;
                    dsMain.GetElement(0, "grtright_contract").style.background = "#FFFFFF";
                } else {
                    dsMain.GetElement(0, "grtright_contract").readOnly = true;
                    dsMain.GetElement(0, "grtright_contract").style.background = "#CCCCCC";
                    dsMain.SetItem(0, "grtright_contract", 0);
                }
            } else if (c == "grtright_memflag") {
                if (v == 1) {
                    dsMain.GetElement(0, "grtright_member").readOnly = false;
                    dsMain.GetElement(0, "grtright_member").style.background = "#FFFFFF";
                } else {
                    dsMain.GetElement(0, "grtright_member").readOnly = true;
                    dsMain.GetElement(0, "grtright_member").style.background = "#CCCCCC";
                    dsMain.SetItem(0, "grtright_member", 0);
                }
            } else if (c == "grtmemco_contflag") {
                if (v == 1) {
                    dsMain.GetElement(0, "grtmemco_contract").readOnly = false;
                    dsMain.GetElement(0, "grtmemco_contract").style.background = "#FFFFFF";
                } else {
                    dsMain.GetElement(0, "grtmemco_contract").readOnly = true;
                    dsMain.GetElement(0, "grtmemco_contract").style.background = "#CCCCCC";
                    dsMain.SetItem(0, "grtmemco_contract", 0);
                }
            } else if (c == "grtmemco_memflag") {
                if (v == 1) {
                    dsMain.GetElement(0, "grtmemco_member").readOnly = false;
                    dsMain.GetElement(0, "grtmemco_member").style.background = "#FFFFFF";
                } else {
                    dsMain.GetElement(0, "grtmemco_member").readOnly = true;
                    dsMain.GetElement(0, "grtmemco_member").style.background = "#CCCCCC";
                    dsMain.SetItem(0, "grtmemco_member", 0);
                }
            } else if (c == "fixpayintoverfst_type") {
                if (v == 2) {
                    dsMain.GetElement(0, "fixpayintoverfstprn_type").disabled = false;
                } else {
                    dsMain.GetElement(0, "fixpayintoverfstprn_type").disabled = true;
                    dsMain.SetItem(0, "fixpayintoverfstprn_type", 0);
                }
            } else if (c == "fixpayintovernxt_type") {
                if (v == 2) {
                    dsMain.GetElement(0, "fixpayintovernxtprn_type").disabled = false;
                } else {
                    dsMain.GetElement(0, "fixpayintovernxtprn_type").disabled = true;
                    dsMain.SetItem(0, "fixpayintovernxtprn_type", 0);
                }
            } else if (c == "dayinyear_1") {
                if (v == 0) {
                    dsMain.SetItem(0, "dayinyear_1", "< ตามปี >");
                    dsMain.SetItem(0, "dayinyear", 0);
                } 
            } else if (c == "intdateinc_firstrcv_1") {
                if (v == 0) {
                    dsMain.SetItem(0, "intdateinc_firstrcv_1", "<ไม่เพิ่ม/ลด>");
                    dsMain.SetItem(0, "intdateinc_firstrcv", 0);
                }
            } else if (c == "intdateinc_lastpay_1") {
                if (v == 0) {
                    dsMain.SetItem(0, "intdateinc_lastpay_1", "<ไม่เพิ่ม/ลด>");
                    dsMain.SetItem(0, "intdateinc_lastpay", 0);
                }
            }
        }

        function SheetLoadComplete() {
            if (dsMain.GetItem(0, "grtright_contflag") == "0") {
                dsMain.GetElement(0, "grtright_contract").readOnly = true;
                dsMain.GetElement(0, "grtright_contract").style.background = "#CCCCCC";
            }

            if (dsMain.GetItem(0, "grtright_memflag") == "0") {
                dsMain.GetElement(0, "grtright_memflag").readOnly = true;
                dsMain.GetElement(0, "grtright_memflag").style.background = "#CCCCCC";
            }

            if (dsMain.GetItem(0, "grtmemco_contflag") == "0") {
                dsMain.GetElement(0, "grtmemco_contract").readOnly = true;
                dsMain.GetElement(0, "grtmemco_contract").style.background = "#CCCCCC";
            }

            if (dsMain.GetItem(0, "grtmemco_memflag") == "0") {
                dsMain.GetElement(0, "grtmemco_member").readOnly = true;
                dsMain.GetElement(0, "grtmemco_member").style.background = "#CCCCCC";
            }

            if (dsMain.GetItem(0, "fixpayintoverfst_type") != 2) {
                dsMain.GetElement(0, "fixpayintoverfstprn_type").disabled = false;
            }

            
            if (dsMain.GetItem(0, "fixpayintovernxt_type") != 2) {
                dsMain.GetElement(0, "fixpayintovernxtprn_type").disabled = false;

            }

            if (dsMain.GetItem(0, "dayinyear_1") == 0) {
                dsMain.SetItem(0, "dayinyear_1", "< ตามปี >");

            }

            if (dsMain.GetItem(0, "intdateinc_firstrcv_1") == 0) {
                dsMain.SetItem(0, "intdateinc_firstrcv_1", "<ไม่เพิ่ม/ลด>");
            }

            if (dsMain.GetItem(0, "intdateinc_lastpay_1") == 0) {
                dsMain.SetItem(0, "intdateinc_lastpay_1", "<ไม่เพิ่ม/ลด>");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>

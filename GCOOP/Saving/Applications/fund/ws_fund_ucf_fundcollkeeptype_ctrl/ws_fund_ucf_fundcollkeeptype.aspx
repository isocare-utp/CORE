<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fund_ucf_fundcollkeeptype.aspx.cs" Inherits="Saving.Applications.fund.ws_fund_ucf_fundcollkeeptype_ctrl.ws_fund_ucf_fundcollkeeptype" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">  
    <script type="text/javascript">

        var dsList = new DataSourceTool();

        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);
                if (confirm("ลบข้อมูลแถวที่ " + (r + 1) + " ?") == true) {
                    PostDelRow();
                }
            }
        }
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
            //            for (var i = 0; i < dsList.GetRowCount() - 1; i++) {
            //                var chk_code = dsList.GetItem(i, "ASSISTTYPE_CODE");
            //                var chk_desc = dsList.GetItem(i, "ASSISTTYPE_GROUPDESC");
            //                if (chk_code == "") {
            //                    alert("กรุณากรอกรหัสประเภทสวัสดิการ");
            //                    dsList.SetItem(i, "ASSISTTYPE_CODE", "");
            //                    break;
            //                } else if (chk_desc = "") {
            //                    alert("กรุณากรอกคำอธิบายสวัสดิการ");
            //                    dsList.SetItem(i, "ASSISTTYPE_GROUPDESC", "");
            //                    break;
            //                }
            //            }                                        
        }

        function SheetLoadComplete() {
        }

        function OnClickNewRow() {
            PostNewRow();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มแถว</span>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_divsrv_constant_roundmoney.aspx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_constant_roundmoney_ctrl.ws_divsrv_constant_roundmoney" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsListClicked(s, r, c, v) {
            if (c == "b_delete") {
                dsList.SetRowFocus(r);
                PostDeleteRow();
            }
        }
        function OnDsListItemChanged(s, r, c, v) {
            if (c == "use_flag") {
                var li_use_flag = dsList.GetItem(r, "use_flag");
                if (li_use_flag == 0) {
                    dsList.GetElement(r, "running_number").style.background = "#DDDDDD";
                    dsList.GetElement(r, "function_code").style.background = "#DDDDDD";
                    dsList.GetElement(r, "satang_type").style.background = "#DDDDDD";
                    dsList.GetElement(r, "truncate_pos_amt").style.background = "#DDDDDD";
                    dsList.GetElement(r, "round_type").style.background = "#DDDDDD";
                    dsList.GetElement(r, "round_pos_amt").style.background = "#DDDDDD";
                    dsList.GetElement(r, "use_flag").style.background = "#DDDDDD";

                    dsList.GetElement(r, "function_code").readOnly = true;
                    dsList.GetElement(r, "satang_type").disabled = true;
                    dsList.GetElement(r, "truncate_pos_amt").readOnly = true;
                    dsList.GetElement(r, "round_type").disabled = true;
                    dsList.GetElement(r, "round_pos_amt").readOnly = true;

                } else {
                    dsList.GetElement(r, "running_number").style.background = "#FFFFFF";
                    dsList.GetElement(r, "function_code").style.background = "#FFFFFF";
                    dsList.GetElement(r, "satang_type").style.background = "#FFFFFF";
                    dsList.GetElement(r, "truncate_pos_amt").style.background = "#FFFFFF";
                    dsList.GetElement(r, "round_type").style.background = "#FFFFFF";
                    dsList.GetElement(r, "round_pos_amt").style.background = "#FFFFFF";
                    dsList.GetElement(r, "use_flag").style.background = "#FFFFFF";

                    dsList.GetElement(r, "function_code").readOnly = false;
                    dsList.GetElement(r, "satang_type").disabled = false;
                    dsList.GetElement(r, "truncate_pos_amt").readOnly = false;
                    dsList.GetElement(r, "round_type").disabled = false;
                    dsList.GetElement(r, "round_pos_amt").readOnly = false;
                }
            }
        }
        function SheetLoadComplete() {
            var li_row = dsList.GetRowCount();
            for (var i = 0; i < li_row; i++) {
                var li_use_flag = dsList.GetItem(i, "use_flag");

                if (li_use_flag == 0) {
                    dsList.GetElement(i, "running_number").style.background = "#DDDDDD";
                    dsList.GetElement(i, "function_code").style.background = "#DDDDDD";
                    dsList.GetElement(i, "satang_type").style.background = "#DDDDDD";
                    dsList.GetElement(i, "truncate_pos_amt").style.background = "#DDDDDD";
                    dsList.GetElement(i, "round_type").style.background = "#DDDDDD";
                    dsList.GetElement(i, "round_pos_amt").style.background = "#DDDDDD";
                    dsList.GetElement(i, "use_flag").style.background = "#DDDDDD";

                    dsList.GetElement(i, "function_code").readOnly = true;
                    dsList.GetElement(i, "satang_type").disabled = true;
                    dsList.GetElement(i, "truncate_pos_amt").readOnly = true;
                    dsList.GetElement(i, "round_type").disabled = true;
                    dsList.GetElement(i, "round_pos_amt").readOnly = true;

                } else {
                    dsList.GetElement(i, "running_number").style.background = "#FFFFFF";
                    dsList.GetElement(i, "function_code").style.background = "#FFFFFF";
                    dsList.GetElement(i, "satang_type").style.background = "#FFFFFF";
                    dsList.GetElement(i, "truncate_pos_amt").style.background = "#FFFFFF";
                    dsList.GetElement(i, "round_type").style.background = "#FFFFFF";
                    dsList.GetElement(i, "round_pos_amt").style.background = "#FFFFFF";
                    dsList.GetElement(i, "use_flag").style.background = "#FFFFFF";

                    dsList.GetElement(i, "function_code").readOnly = false;
                    dsList.GetElement(i, "satang_type").disabled = false;
                    dsList.GetElement(i, "truncate_pos_amt").readOnly = false;
                    dsList.GetElement(i, "round_type").disabled = false;
                    dsList.GetElement(i, "round_pos_amt").readOnly = false;
                }
            }

        }
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="right" style="margin-right: 1px; width: 720px;">
        <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
        <uc1:DsList ID="dsList" runat="server" />
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fin_chgstatuschq_pay.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_chgstatuschq_pay_ctrl.ws_fin_chgstatuschq_pay" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool();
    var dsList = new DataSourceTool();

    function Validate() {
        var save_flag = 0;
        for (var i = 1; i <= dsMain.GetRowCount(); i++) {
            if (dsMain.GetItem(i - 1, "status") == "1") {
                save_flag = 1;
            }
        }
        if (save_flag == 1) {
            return confirm("ยืนยันการบันทึกข้อมูล");
        } else { alert("กรุณาเลือกรายการที่จะทำการเปลี่ยนแปลง"); }
    }
    function OnDsMainClicked(s, r, c) {
        if (c == "cheque_no" || c == "date_onchq" || c == "bank_desc" || c == "to_whom") {
            Gcoop.GetEl("HfRow").value = r;
            PostDetail();
        }
    }
    function OnDsMainItemChanged(s, r, c, v) {

    }
            
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
   <uc1:DsMain ID="dsMain" runat="server" />
   </br>
   <uc2:DsList ID="dsList" runat="server" />
   <asp:HiddenField ID="HfRow" runat="server" />
      <asp:HiddenField ID="HfChk" runat="server" />
</asp:Content>
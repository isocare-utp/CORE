<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_fnucfconstant.ascx.cs"
    Inherits="Saving.Applications.app_finance.u_fnucfconstant" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<script type="text/javascript">

    function MenubarSave() {
        if (confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")) {


            objdw_main.AcceptText();
            objdw_main.Update();
        }
    }

</script>
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
<dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_fnucfconstant"
    LibraryList="~/DataWindow/App_finance/cm_constant_config.pbl" ClientScriptable="True"
    OnBeginUpdate="dw_main_BeginUpdate" OnEndUpdate="dw_main_EndUpdate" BorderStyle="None"
    ClientEventButtonClicked="OnButtonClick">
</dw:WebDataWindowControl>

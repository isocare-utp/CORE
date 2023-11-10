<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="websetprinter.aspx.cs" Inherits="Saving.websetprinter" %><form id="form1" runat="server">

<script>
    function postOutput(msg) {
        //alert(msg);
        window.parent.document.getElementById('PrinterTxt').innerHTML = msg;
    }
</script>
<asp:Label ID="LbServerMessage" runat="server" Text=""></asp:Label>
</form>
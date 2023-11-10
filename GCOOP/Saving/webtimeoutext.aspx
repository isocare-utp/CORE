<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webtimeoutext.aspx.cs" Inherits="Saving.webtimeoutext" %><form id="form1" runat="server">

<script>
    function postOutput(msg) {
        //alert(msg);
        window.parent.document.getElementById('SesstionTimeoutTxt').innerHTML = msg;
    }
</script>
<asp:Label ID="LbServerMessage" runat="server" Text=""></asp:Label>
</form>
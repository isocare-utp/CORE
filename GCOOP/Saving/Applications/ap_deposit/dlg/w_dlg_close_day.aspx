<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_close_day.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_close_day" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ปิดงานสิ้นวัน</title>
    <%=postLooping%>

    <script type="text/javascript">
    function DialogLoadComplete(){
        var maxLoop = Gcoop.GetEl("HdMaxLoop").value;
        var currLoop = Gcoop.GetEl("HdCurrentLoop").value;
        var isLoop = Gcoop.GetEl("HdIsLoop").value == "true";
        
        maxLoop = Gcoop.ParseInt(maxLoop);
        currLoop = Gcoop.ParseInt(currLoop);
        
        if(maxLoop > currLoop && isLoop){
            postLooping();
        }
        else
        {
            Gcoop.GetEl("b_close").style.visibility = "visible";
        }
    }
    
    function Click(){
        parent.RemoveIFrame();
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        <input id="b_close" type="button" value="ปิด" style="visibility: hidden" onclick = "Click()"/>
    </div>
    <asp:HiddenField ID="HdIsLoop" Value="false" runat="server" />
    <asp:HiddenField ID="HdMaxLoop" runat="server" />
    <asp:HiddenField ID="HdCurrentLoop" runat="server" />
    <asp:HiddenField ID="HdCloseDate" runat="server" />
    </form>
</body>
</html>

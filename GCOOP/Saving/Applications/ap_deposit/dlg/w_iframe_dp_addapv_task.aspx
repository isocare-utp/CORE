<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_iframe_dp_addapv_task.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_iframe_dp_addapv_task" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รออนุมัติ</title>
    <%=postCheckApv%>

    <script type="text/javascript">
    function DialogLoadComplete(){ 
        if(Gcoop.GetEl("HdCheckApv").value == "true"){
            var valueCheckApv = Gcoop.GetEl("HdValueCheckApv").value;
            var nameApv = Gcoop.GetEl("HdNameApv").value;
            //window.opener.GetValueCheckApv(valueCheckApv,nameApv);
            //window.close();
            parent.GetValueCheckApv(valueCheckApv, nameApv);
            return;
        }
        if(Gcoop.GetEl("HdDlgClose").value == "true"){
            //window.close();
            parent.RemoveIFrame();
            return;
        }
        setTimeout("postCheckApv()", 3000);
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:Label ID="Label1" runat="server" Text="กำลังรอการอนุมัติ..."></asp:Label></center>
        <br />
        <br />
        <br />
        <table width="200px">
            <tr>
                <td align="center">
                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" OnClick="BtnCancle_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HdCheckApv" runat="server" />
    <asp:HiddenField ID="HdValueCheckApv" runat="server" />
    <asp:HiddenField ID="HdNameApv" runat="server" />
    <asp:HiddenField ID="HdDlgClose" runat="server" />
    </form>
</body>
</html>

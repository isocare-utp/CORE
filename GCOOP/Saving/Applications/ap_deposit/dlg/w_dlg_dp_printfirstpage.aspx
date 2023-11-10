<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_printfirstpage.aspx.cs" Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_printfirstpage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>พิมพ์ปกสมุด</title>
    <%=printFirstPage%>
    <script type ="text/javascript">
    
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>พิมพ์ปกสมุด</center>
    <br />
    <div align="center">
        <asp:Button ID="btnCommit" runat="server" Text="ตกลง" onclick="btnCommit_Click" 
            />
        &nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" />
    </div>
    </div>
    <asp:HiddenField ID="HdDeptAccountNo" runat="server" />
    <asp:HiddenField ID="HdPassBookNo" runat="server" />
    </form>
</body>
</html>

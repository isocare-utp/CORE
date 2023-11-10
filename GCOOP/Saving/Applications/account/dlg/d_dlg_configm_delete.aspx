<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="d_dlg_configm_delete.aspx.cs"
    Inherits="Saving.Applications.account.dlg.d_dlg_configm_delete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configm Delete</title>
    <script type="text/javascript">
        function ClickDelete() {
            var row = <%=delete_row%> ;
            parent.ConfirmDelete(row);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        คุณต้องการลบข้อมูลลำดับที่ <asp:Label ID="LbRow" runat="server"></asp:Label>
        <br />
        <font color="red">ข้อมูลจะถูกลบออกจากระบบทันที ยืนยันจะลบข้อมูลหรือไม่</font>
        <br />
        <br />
        <br />
        <input type="button" value="ลบทันที" onclick="ClickDelete()" />
        &nbsp;
        <input type="button" value="ไม่ลบ" onclick="parent.RemoveIFrame()" />
    </div>
    </form>
</body>
</html>

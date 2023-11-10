<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_contajust_coll.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_contajust_coll" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายละเอียดหลักประกัน</title>

    <script type="text/javascript">
        //รับค่ามาเพื่อ retrieve มาจาก sheet หลัก
        function GetCollDet(collno, colltype) {
            Gcoop.GetEl("HiddenFieldCollNo").value = collno;
            Gcoop.GetEl("HiddenFieldCollType").value = colltype;
        }
    </script>

</head>
<body>
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_sl_contadj_colldet"
            LibraryList="~/DataWindow/shrlon/sl_contract_adjust.pbl">
        </dw:WebDataWindowControl>
    </div>
    <br />
    <dw:WebDataWindowControl ID="dw_item" runat="server" DataWindowObject="d_sl_contadj_collitem"
        LibraryList="~/DataWindow/shrlon/sl_contract_adjust.pbl">
    </dw:WebDataWindowControl>
    </form>
    <asp:HiddenField ID="HiddenFieldCollNo" runat="server" />
    <asp:HiddenField ID="HiddenFieldCollType" runat="server" />
</body>
</html>

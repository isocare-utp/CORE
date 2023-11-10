<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_intratespc.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_loanrequest_intratespc" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ดอกเบี้ย</title>
    <%=saveWebDialog%>
    <%=refreshDW  %>
     <script type="text/javascript">
        function OnInsert(dwname)
        {
            if( dwname == "10")
            {  
                var row = objdw_intspc.RowCount() +1;
                objdw_intspc.InsertRow(objdw_intspc.RowCount() +1 );
            }
        }
        function DialogLoadComplete(){
             var chkStatus = Gcoop.GetEl("HfChkStatus").value;
             if (chkStatus == "1" ) 
             {
                window.opener.CloseDLG();
                window.close();
             }else if ( chkStatus == "2")
             {
                if(confirm('ไม่มีรายการดอกเบี้ย ต้องการปิดหน้าจอ กด OK ไม่ต้องการปิดกด CANCEL')){
                    window.opener.CloseDLG();
                    window.close();
                }else{
                    Gcoop.GetEl("HfChkStatus").value = "";
                }
             }
        }
        function OnSaveClick(){
                objdw_intspc.AcceptText();
                saveWebDialog();
        }
        function OnDwIntSpcButtonClicked(sender, rowNumber, buttonName){
            if (buttonName =="b_delete")
                {
                    objdw_intspc.DeleteRow (rowNumber);
                }
        }
        function ItemChangeDwIntSpc(sender, rowNumber, columnName, newValue)
        { 
                objdw_intspc.SetItem(rowNumber, columnName, newValue );
                objdw_intspc.AcceptText();
                refreshDW();
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table >
        <tr>
            <td>
            <span class ="linkSpan" style="cursor:pointer;" onclick="OnInsert('10');" >เพิ่มแถว</span>
            <dw:WebDataWindowControl ID="dw_intspc" runat="server" AutoRestoreContext="False" 
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                ClientScriptable="True" ClientEventButtonClicked="OnDwIntSpcButtonClicked"
                DataWindowObject ="d_sl_loanrequest_intratespc"
                LibraryList ="~/DataWindow/shrlon/sl_loan_requestment.pbl" 
                ClientFormatting="True" ClientEventItemChanged="ItemChangeDwIntSpc">
             </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <input id="btnSave" type="button" value="ปิด" OnClick="OnSaveClick(this)" />
            </td>
        </tr>
    </table>
    <asp:Literal ID="xml_otherclr" runat="server"></asp:Literal>
    <asp:HiddenField ID="HfChkStatus" runat="server" />
    <asp:HiddenField ID="HdXmlOtherClr" runat="server" />
    <asp:HiddenField ID="HdFirst" runat="server" />
    </form>
</body>
</html>

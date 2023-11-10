<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_cashflow_formula.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_cashflow_formula" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postMain%>
    <%=postDwDetail%>
    <%=insertRowDetail%>
    <%=deleteRowDetail%>
    <%=postInsertAfterRow%>

    <script type="text/javascript">
    
        function SheetLoadComplete(){
            var row = Gcoop.ParseInt(Gcoop.GetEl("HdDetailRow").value);
            if ( row > 0 ){
                Gcoop.SetFocus("seq_no_" + (row -1));
                objDwDetail.SelectRow(row);
            }
        }
        
        function Validate(){
            return confirm("คุณต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }
        
        function OnDwMainItemChanged(sender, rowNumber, colunmName, newValue){
            if(colunmName == "sheet_type"){
                objDwMain.SetItem(rowNumber,colunmName,newValue);
                objDwMain.AcceptText();
                postMain();   
            }
        }
        
        function OnDwMainButtonClicked(sender, rowNumber, buttonName){
            if(buttonName == "cb_ok"){
                Gcoop.GetEl("HdCheckClick").value = "true";     
                postDwDetail(); 
            }
        }
        
        function OnClickInsertRow(){
            if(Gcoop.GetEl("HdCheckClick").value == "true"){
                insertRowDetail();
            }
        }
        
        function OnClickDeleteRow(){
            if(objDwDetail.RowCount() > 0){
                var currentRow = Gcoop.GetEl("HdDetailRow").value;
                var confirmText = "ยืนยันการลบแถวที่ " + currentRow;
                if (confirm(confirmText)) {
                    deleteRowDetail();
                }
            }
            else{
                alert("ยังไม่มีการเพิ่มแถวสำหรับรายการ");
            }
        }

        function Dw_detailInsertAfterRow() {
            postInsertAfterRow();
        }

        
        function OnDwDetailClicked(sender, rowNumber, objectName) {
            for (var i = 1; i <= sender.RowCount(); i++) {
                sender.SelectRow(i, false);
            }
            sender.SelectRow(rowNumber, true);
            sender.SetRow(rowNumber);
            Gcoop.GetEl("HdDetailRow").value = rowNumber + "";
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" ClientScriptable="True" DataWindowObject="d_acc_cashflow_main"
        LibraryList="~/DataWindow/account/acc_cashflow.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnDwMainButtonClicked"
        ClientEventItemChanged="OnDwMainItemChanged" ClientFormatting="False">
    </dw:WebDataWindowControl>
    <table style="width: 100%;">
        <tr>
            <td class="style5">
                <asp:Label ID="Label7" runat="server" Text="รายการ" Font-Bold="True" Font-Names="Tahoma"
                    Font-Size="14px" ForeColor="#0099CC" Font-Overline="False" Font-Underline="True" />
                &nbsp; <span onclick="OnClickInsertRow()" style="cursor: pointer;">
                    <asp:Label ID="LbInsert2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="#006600" /></span> &nbsp;&nbsp;
                  &nbsp; <span onclick="Dw_detailInsertAfterRow()" style="cursor: pointer;">
                    <asp:Label ID="Label1" runat="server" Text="แทรกแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="#006600" /></span> &nbsp;&nbsp;
                <span onclick="OnClickDeleteRow()" style="cursor: pointer;">
                    <asp:Label ID="LbDel2" runat="server" Text="ลบแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="Red" /></span> &nbsp;&nbsp;<asp:Button
                            ID="Btn_Generate" runat="server" Text="จัดเลขลำดับใหม่" Width="100px" OnClick="Btn_Generate_Click" />
            </td>
        </tr>
        <tr>
            <td class="style5">
                <dw:WebDataWindowControl ID="DwDetail" runat="server" ClientScriptable="True" DataWindowObject="d_acc_cashflow_list"
                    LibraryList="~/DataWindow/account/acc_cashflow.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                    ClientEventClicked="OnDwDetailClicked" >
             <%--       <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                    </PageNavigationBarSettings>--%>
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdDetailRow" runat="server" Value="0" />
    <asp:HiddenField ID="HdCheckClick" runat="server" Value="false" />
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_cfinttable.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_cfinttable" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=IntRateType%>
    <%=postEstimateDate %>
    <%=NewRateCodeNo %>
    <%=getDelete %>

    <script type="text/javascript">

        function Validate()
        {
             return confirm("บันทึกรายการ");
        }
        
        function OnDwListRowChange(sender, rowNumber, objectName)
        {
            objdw_list.SelectRow(Gcoop.GetEl("HfRowNumber").value, false);
            Gcoop.GetEl("HiddenFieldCode").value = objdw_list.GetItem(rowNumber, "loanintrate_code");
            Gcoop.GetEl("HfRowNumber").value = rowNumber;
            objdw_list.SelectRow(rowNumber, true);
            IntRateType();
        }
        
        function OnDwDetailItemChanged(sender, rowNumber, columnName, newValue)
        {
            //DwThDate("effective_date", "effective_tdate", objdw_detail, sender, rowNumber, columnName, newValue);
            if( columnName == "effective_tdate" ){
                objdw_detail.SetItem(1, "effective_tdate", newValue);
                objdw_detail.AcceptText();
                objdw_detail.SetItem(1, "effective_date", Gcoop.ToEngDate(newValue));
                objdw_detail.AcceptText();
            }
            else if (columnName == "interest_rate"){
                objdw_detail.SetItem(rowNumber,columnName,newValue);
                var interrestRate = objdw_detail.GetItem(rowNumber,columnName);
                interrestRate = interrestRate/100;
                
                setTimeout("objdw_detail.SetItem("+rowNumber+", '"+columnName+"', '"+interrestRate+"')", 500);
                //objdw_detail.AcceptText();
            }
        }

        function MenubarNew(){
            Gcoop.OpenDlg('350','150','w_dlg_sl_cfloan_inttable_newtype.aspx','');
        }
        
        function NewRateCode(loanintrate_code,loanintrate_desc){
            Gcoop.GetEl("HiddenFieldCode").value = loanintrate_code;
            NewRateCodeNo();
        }
        function OnDelete(){
            var idde = Gcoop.GetEl("HiddenFieldCode").value;
            if(confirm("คุณต้องการลบรายการ "+ idde +" ใช่หรือไม่?")){
                    getDelete();
                }
        }
        function OnButtonClick(sender, row, name){
            if(name == "b_delete"){
                if(confirm("คุณต้องการลบรายการแถว "+ row +" ใช่หรือไม่?")){
                    objdw_detail.DeleteRow(row);
                }
            }
            return 0;
        }
        function OnKeyUpEnd(e){
            if(e.keyCode == "115"){
                //TryOpenDlgDeptWith();
            } else if (e.keyCode == "123"){
        }
        function SheetLoadComplete(){
            var rr =  Gcoop.GetEl("HfRowNumber").value; 
            objdw_list.SelectRow(rr, true);
        }
    }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table width="100%" border="0">
        <tr>
            <td valign="top">
                <dw:WebDataWindowControl ID="dw_list" runat="server" DataWindowObject="d_sl_cfloan_inttable_list"
                    LibraryList="~/DataWindow/shrlon/sl_cfinttable.pbl" ClientScriptable="True" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="OnDwListRowChange" TabIndex="1">
                </dw:WebDataWindowControl>
                <br />
                <span class="linkSpan" onclick="OnDelete()" style="font-size: small; color: #808080;
                    float: left">ลบ</span>
            </td>
            <td valign="top">
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_cfloan_inttable_mast"
                    LibraryList="~/DataWindow/shrlon/sl_cfinttable.pbl" ClientScriptable="True" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" TabIndex="1000">
                </dw:WebDataWindowControl>
                <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_sl_cfloan_inttable_detail"
                    LibraryList="~/DataWindow/shrlon/sl_cfinttable.pbl" 
                    ClientScriptable="True" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnButtonClick"
                    ClientEventItemChanged="OnDwDetailItemChanged" TabIndex="1500">
                </dw:WebDataWindowControl>
                <asp:Button ID="btnInsertRow" runat="server" Text="เพิ่มแถว" OnClick="btnInsertRow_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HfRowNumber" runat="server" />
    <asp:HiddenField ID="HiddenFieldCode" runat="server" />
</asp:Content>

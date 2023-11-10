<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_cm_constant_config.aspx.cs" Inherits="Saving.Applications.account.w_sheet_cm_constant_config" Title="Untitled Page" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<%=postDeleteRow%>
<%=postAddRow%>
<%=postCheckType%>
<script type ="text/javascript" >
 // ฟังก์ชันการลบข้อมูล
     function OnDwdataButtonClick(sender, row, bName) 
     {
         if (bName == "b_del")
         {
            var isConfirm = confirm("ต้องการลบข้อมูลแถวนี้ใช่หรือไม่ ?" );
            if (isConfirm) 
            {
                Gcoop.GetEl("Hd_row").value = row + "";
                postDeleteRow(); 
            }
         }
         return 0;
     }
     // Add Row
     function DwDataAddRow()
     {
        postAddRow();
     }
     
     function Validate() 
     {
       return confirm("ยืนยันการบันทึกข้อมูล");
   }
   function CheckType() {
       postCheckType();
   }
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
                <br />
                <span class="linkSpan" onclick="DwDataAddRow()" 
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว
                </span> 
                <span class="linkSpan" onclick="CheckType()" 
                    style="font-family: Tahoma; font-size: small; float: right; color: #0000CC;">ตรวจสอบรายการที่ยังไม่ได้ Map คู่บัญชี
                </span>
        <asp:Panel 
        ID="Panel1" runat="server" Height="495px" 
        ScrollBars="Auto" Width="746px" BorderStyle="Ridge">
                    <dw:WebDataWindowControl ID="Dw_data" runat="server" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                        ClientFormatting="True" ClientScriptable="True" 
                        DataWindowObject="d_cmucfvcmapaccid" 
                        LibraryList="~/DataWindow/account/cm_constant_config.pbl" 
                        ClientEventButtonClicked="OnDwdataButtonClick">
                    </dw:WebDataWindowControl>
                </asp:Panel>
    <br />
    <table style="width:100%;">
        <tr>
            <td>
                <asp:HiddenField ID="Hd_row" runat="server" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_kp_import_agent_group.aspx.cs" Inherits="Saving.Applications.keeping.dlg.w_dlg_kp_import_agent_group" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>นำเข้าข้อมูลสังกัดลูกหนี้ตัวแทน</title>
    <%=importTxt%>
    <%=showTxt%>
    <script type="text/javascript">
    function CloseClick()
    {
        window.close();
    }
    
    function ImportClick()
    {
//    alert ("import");
        importTxt();
    }
   function ShowClick()
   {
        showTxt();
   }
       function DialogLoadComplete()
       {
//             var returnValue =  Gcoop.GetEl("HdReturn").value;
//            if ( returnValue == "10"){
//                Gcoop.GetEl("HdReturn").value="";
//                if ( objdw_data.Update() ==1 ){
//                    commit;
//                }else{
//                    rollback;
//                }
//            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table>
            <tr>
                <td>
                 <asp:Label ID="Label1" runat="server" Text="Label">เลือกไฟล์.txt</asp:Label>
                <asp:FileUpload ID="FileUpload" runat="server" />
                 <input id="btnShow" type="button" value="แสดงข้อมูล" onclick="ShowClick()" />
                  <dw:WebDataWindowControl ID="dw_main" runat="server" AutoRestoreContext="False" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                    ClientScriptable="True" 
                    DataWindowObject ="d_kp_agent_group_list" 
                    LibraryList ="~/DataWindow/keeping/kp_agent_group.pbl" 
                    ClientFormatting="True"
                    RowsPerPage="15" 
                    style="top: 0px; left: 0px" >
                    <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
                    <BarStyle HorizontalAlign="Center" />
                    <NumericNavigator FirstLastVisible="True" />
                    </PageNavigationBarSettings>
                    </dw:WebDataWindowControl>
                    <input id="btnImport" type="button" value="บันทึก" onclick="ImportClick()" />
                    <input id="btnClose" type="button" value="ปิด" onclick="CloseClick()"  />
                    </td>
            </tr>
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="dw_data" runat="server" AutoRestoreContext="False" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                    ClientScriptable="True" 
                    DataWindowObject ="d_kp_agent_group_list" 
                    LibraryList ="~/DataWindow/keeping/kp_agent_group.pbl" 
                    ClientFormatting="True" Visible="False">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
        </table>
         <asp:HiddenField ID="HdReturn" runat="server" />
    </form>
</body>
</html>

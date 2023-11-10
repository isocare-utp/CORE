<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dbms.aspx.cs" Inherits="Saving.dbms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SQL DDL & QUERY </title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="font-size: small; position: relative;" >
        <asp:Label ID="Label1" runat="server" Text="Connection String Target"></asp:Label>
        <asp:TextBox ID="TbConnectionString" runat="server" Width="600px" Text=""></asp:TextBox>
         <asp:DropDownList ID="DropDownListAutoRefresh" runat="server">
            <asp:ListItem Value="-">-</asp:ListItem>
            <asp:ListItem Value="10">ดึงทุกๆ 10 วินาที</asp:ListItem>
            <asp:ListItem Value="30">ดึงทุกๆ 30 วินาที</asp:ListItem>
            <asp:ListItem Value="60">ดึงทุกๆ 1 นาที</asp:ListItem>
            <asp:ListItem Value="300">ดึงทุกๆ 5 นาที</asp:ListItem>
            <asp:ListItem Value="600">ดึงทุกๆ 10 นาที</asp:ListItem>
        </asp:DropDownList>
        <br/>
        <asp:Label ID="Label2" runat="server" Text="Connection String Source" Visible=false ></asp:Label>
        <asp:TextBox ID="TbConnectionStringSrc" runat="server" Width="650px" Text="" Visible=false></asp:TextBox>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="select * from v_current_login">รายการผู้ใช้งานระบบ</asp:ListItem>
            <asp:ListItem Value="select * from v_finslip_waittopost">รายการคิวรอดึงการเงิน</asp:ListItem>
            <asp:ListItem Value="select * from v_finallocate_waittopost">รายการคิวรอดึงจัดสรร</asp:ListItem>
            <asp:ListItem Value="select * from v_finslip_today">รายการบันทึกการเงินวันนี้</asp:ListItem>
            <asp:ListItem Value="select * from v_finallocate_today">รายการจัดสรรวันนี้</asp:ListItem>
            <asp:ListItem Value="select * from v_finslip_multi_topost">รายการตัดจ่ายมากกว่า1เช็ค</asp:ListItem>
            <asp:ListItem Value="select distinct table_name from  all_tab_columns where lower(owner)='ifsct' and table_name not like '%$%' order by table_name asc  ">ดึงรายการ TABLE</asp:ListItem>
            <asp:ListItem Value="select 'ALTER SYSTEM KILL SESSION '''||b.sid||','||b.serial#||''' IMMEDIATE ' as sql from v$locked_object a,v$session b,dba_objects c where b.sid = a.session_id and a.object_id = c.object_id ">คำสั่งKillตารางที่Lock</asp:ListItem>
            <asp:ListItem Value="select c.owner||' * '||c.object_name||' * '||c.object_type||' * '||b.sid||' * '||b.serial#||' * '||b.status||' * '||b.osuser||' * '||b.machine||' * '||a.LOCKED_MODE from v$locked_object a ,v$session b,dba_objects c where b.sid = a.session_id and a.object_id = c.object_id ">ตรวจดูรายการTableLock</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="Button9" runat="server" onclick="Button9_Click" Text="ดึง" />
        
        <asp:Button ID="Button1" runat="server" Text="Query" onclick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Execute" onclick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="ExePL" onclick="Button3_Click" />
        <asp:Button ID="Button6" runat="server" Text="SaveDataDict"  onclick="Button6_Click" />
        <asp:Button ID="Button4" runat="server" Text="ExeCommand"  
            onclick="Button4_Click" Visible="False" />
        <asp:Button ID="Button5" runat="server" Text="StartIReportBuilder"  onclick="Button45_Click" Visible=false />
        <asp:Button ID="Button7" runat="server" Text="BuildDataDic"  onclick="Button7_Click"  />
        <asp:Button ID="Button8" runat="server" Text="CreateCopyTableDataSQL"  onclick="Button8_Click"  />
        <asp:Button ID="Button11" runat="server" Text="BuildTriggerSync"  
            onclick="Button11_Click"  />
        <br />
        <asp:TextBox ID="TbSQL" runat="server" Height="150px" TextMode="MultiLine"   Width="760px"></asp:TextBox>
        <br /><div style="display:none;" >
        <br />
        Create SVN Zip from Path : 
        <asp:TextBox ID="svnRootPath" runat="server" Width="258px">o:\Dropbox\svn_repository\FSCT_WEB</asp:TextBox>
        SVN Version
        <asp:TextBox ID="svnVersion" runat="server" Width="54px"></asp:TextBox>
&nbsp;<asp:Button ID="Button10" runat="server" onclick="Button10_Click" 
            Text="Create SVN File Sync" Width="150px" />
        <br />
        SVN Path 
        <asp:TextBox ID="svnPath" runat="server" Width="275px">z:\svn\Dropbox\svn_repository\FSCT_WEB</asp:TextBox>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="UploadSVNBtn" runat="server" onclick="UploadSVNBtn_Click" 
            Text="UploadSVNFiles" />
        <br /></div>
        <br />
        <asp:Label ID="LbServerMessage" runat="server" Text=""></asp:Label>
        <asp:Label ID="LbShowKey" runat="server" Text="" Visible=false></asp:Label>
        <asp:Label ID="LbOutput" runat="server" Text=""></asp:Label>
    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    </div>
    </form>    
    <script language="javascript">
        var timeID;
        var active = false;
        function actionEvent() {
            if (document.getElementById("DropDownListAutoRefresh").value != "-") {
                document.getElementById("Button1").click();
            }
        }
        function reloadPage() {
            if (document.getElementById("DropDownListAutoRefresh").value != "-") {
                time = 1000 * document.getElementById("DropDownListAutoRefresh").value;
                //alert(time);
                if (active) actionEvent(); else active = true;
                timeID = setTimeout("reloadPage()", time);
            } else {
                clearTimeout(timeID);
            }
        }
        reloadPage();
    </script>
</body>
</html>

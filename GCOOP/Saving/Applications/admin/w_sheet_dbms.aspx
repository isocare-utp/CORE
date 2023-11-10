<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
     CodeBehind="w_sheet_dbms.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_dbms" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <script type="text/javascript">
        function Validate() {
            return confirm("คุณต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }

        function OnDwButtomClicked(s, r, c, v) {
            switch (c) {
                case "b_add":
                    break;
            }

        }
        function itemChange(s, r, c, v) {
            if (c == "user_name") {
            } 
        }
        function OnDwClicked(s, r, c, v) {

            switch (c) {
                case "user_name":

                    break;
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <div align="center" style="font-size: small; position: relative;" >
        <asp:Label ID="Label1" runat="server" Text="Connection String Target" Visible=false></asp:Label>
        <asp:TextBox ID="TbConnectionString" runat="server" Width="730px" Text=""></asp:TextBox><br/>
        <asp:Label ID="Label2" runat="server" Text="Connection String Source" Visible=false ></asp:Label>
        <asp:TextBox ID="TbConnectionStringSrc" runat="server" Width="650px" Text="" Visible=false></asp:TextBox>
        <br />
         <table><tr><td>
        <asp:DropDownList ID="DropDownListAutoRefresh" runat="server" >
            <asp:ListItem Value="-">-</asp:ListItem>
            <asp:ListItem Value="10">ดึงทุกๆ 10 วินาที</asp:ListItem>
            <asp:ListItem Value="30">ดึงทุกๆ 30 วินาที</asp:ListItem>
            <asp:ListItem Value="60">ดึงทุกๆ 1 นาที</asp:ListItem>
            <asp:ListItem Value="300">ดึงทุกๆ 5 นาที</asp:ListItem>
            <asp:ListItem Value="600">ดึงทุกๆ 10 นาที</asp:ListItem>
        </asp:DropDownList></td>
        <td><asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="select * from v_current_login">รายการผู้ใช้งานระบบ</asp:ListItem>
			<asp:ListItem Value="select b.username ,a.LAST_LOAD_TIME,a.LAST_ACTIVE_TIME,a.CPU_TIME, a.disk_reads ,a.executions ,a.disk_reads, a.executions , a.sql_text from v$sqlarea a, dba_users b where a.parsing_user_id = b.user_id and a.sql_text not like '%v$sqlarea%' and a.sql_text not like '%/*%' and a.sql_text not like '%sys.%' and a.sql_text not like '%sql_text%' and b.username like '%SCO%'and rownum <=200  order by a.LAST_ACTIVE_TIME desc">รายการประวัติ SQL</asp:ListItem>
            <asp:ListItem Value="SELECT  s.username,s.status,s.program,SECONDS_IN_WAIT,concat(concat(' ALTER SYSTEM KILL SESSION ''',concat(concat( s.sid,',' ),s.serial#) ),''' IMMEDIATE;')  sql  FROM   gv$session s JOIN gv$process p ON p.addr = s.paddr AND p.inst_id = s.inst_id WHERE  s.status='INACTIVE' and  ( lower(NVL(s.program,'')) in ( '','w3wp.exe') or s.program is null ) order by s.logon_time desc">ดึงรายการ SESSION Web Inactive</asp:ListItem>
            <asp:ListItem Value="SELECT  s.username,s.status,s.program,SECONDS_IN_WAIT,concat(concat(' ALTER SYSTEM KILL SESSION ''',concat(concat( s.sid,',' ),s.serial#) ),''' IMMEDIATE;')  sql  FROM   gv$session s JOIN gv$process p ON p.addr = s.paddr AND p.inst_id = s.inst_id WHERE  s.status='INACTIVE' order by s.logon_time desc">ดึงรายการ SESSION Inactive ทั้งหมด</asp:ListItem>
            <asp:ListItem Value="select distinct table_name from  all_tab_columns where lower(owner)='ifsct' and table_name not like '%$%' order by table_name asc  ">ดึงรายการ TABLE</asp:ListItem>
            <asp:ListItem Value="select 'ALTER SYSTEM KILL SESSION '''||b.sid||','||b.serial#||''' IMMEDIATE; ' as sql from v$locked_object a,v$session b,dba_objects c where b.sid = a.session_id and a.object_id = c.object_id ">คำสั่งKillตารางที่Lock</asp:ListItem>
            <asp:ListItem Value="select c.owner||' * '||c.object_name||' * '||c.object_type||' * '||b.sid||' * '||b.serial#||' * '||b.status||' * '||b.osuser||' * '||b.machine||' * '||a.LOCKED_MODE from v$locked_object a ,v$session b,dba_objects c where b.sid = a.session_id and a.object_id = c.object_id ">ตรวจดูรายการTableLock</asp:ListItem>
        </asp:DropDownList></td>
        <td><asp:Button ID="Button9" runat="server" onclick="Button9_Click" Text="ดึง" />  </td>      
        <td><asp:Button ID="Button1" runat="server" Text="Query" onclick="Button1_Click" /></td>
        <td><asp:Button ID="Button2" runat="server" Text="Execute" onclick="Button2_Click" /></td>
        <td><asp:Button ID="Button3" runat="server" Text="ExePL" onclick="Button3_Click" /></td>
        <td><asp:Button ID="Button6" runat="server" Text="SaveDataDict"  onclick="Button6_Click" Visible="False" /></td><td>
        <asp:Button ID="Button4" runat="server" Text="ExeCommand"  onclick="Button4_Click" Visible="False" />
        <asp:Button ID="Button5" runat="server" Text="StartIReportBuilder"  onclick="Button45_Click" Visible="false" />
        <asp:Button ID="Button7" runat="server" Text="BuildDataDic"  onclick="Button7_Click" Visible="false" />
        <asp:Button ID="Button8" runat="server" Text="CreateCopyTableDataSQL"  onclick="Button8_Click" Visible="false" />
        <asp:Button ID="Button11" runat="server" Text="BuildTriggerSync"  onclick="Button11_Click" Visible="false" /></td>
        </tr></table>
        <br />
        <asp:TextBox ID="TbSQL" runat="server" Height="100px" TextMode="MultiLine"   Width="740px"></asp:TextBox>
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
    <div style="overflow:scroll;width:750px;height:250px;"><div style="width:950px" align="left">
    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    </div></div>
    ตัวอย่าง PL/SQL สำหรับ Kill Session ต้องนำไป Run ที่ sqlplus
 <asp:TextBox ID="TbKillSQL" runat="server" Height="100px" TextMode="MultiLine"   Width="740px">
begin
 for i in (SELECT concat(concat(' ALTER SYSTEM KILL SESSION ''',concat(concat( s.sid,',' ),s.serial#) ),''' IMMEDIATE')  sqld  FROM   gv$session s JOIN gv$process p ON p.addr = s.paddr AND p.inst_id = s.inst_id WHERE  s.status='INACTIVE' and SECONDS_IN_WAIT > 60*5 and ( lower(NVL(s.program,'')) in ( '','w3wp.exe') or s.program is null ) order by s.logon_time desc) LOOP
  execute immediate i.SQLd||' ';
 end loop;
end;
/
</asp:TextBox>
    </div>
    <script language="javascript" type="text/javascript">
        var timeID;
        var active = false;
        function actionEvent() {
            if ($("select[id*='DropDownListAutoRefresh']").val() != "-") {
                $("[id*='Button1']").click();
            }
        }
        function reloadPage() {
            var DropDownListAutoRefresh = $("select[id*='DropDownListAutoRefresh']").val();
            //alert(DropDownListAutoRefresh);
            if (DropDownListAutoRefresh != "-") {
                time = 1000 * DropDownListAutoRefresh;
                //alert(time);
                if (active) actionEvent(); else active = true;
                timeID = setTimeout("reloadPage()", time);
            } else {
                clearTimeout(timeID);
            }
        }
        reloadPage();
    </script>
</asp:Content>

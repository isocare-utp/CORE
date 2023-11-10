<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dbbackup.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_dbbackup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
	<!--
     <iframe scrolling="no" src="oracle_em.html" style="border: 0px none;width: 600px; height:250px;" >
     </iframe>
      -->
    รายการแฟ้มข้อมูลฐานข้อมูล <asp:Button ID="SetupBtn" runat="server"  Visible="true"  
        CausesValidation="True" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')"
        onclick="RunSetupDBProfile_Click" Text="ตั้งค่าเริ่มต้น" Width="89px" />
     <br />
    <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>
    <asp:Literal ID="Literal0" runat="server"></asp:Literal>
    <br />
    <script language="javascript" type="text/javascript">
        function performA(db, a) {
            $("[id*='DBProfile']").val(db);
            $("[id*='actionDBProfileA']").val(a);
            $("[id*='actionDBProfileD']").val("");
            $("[id*='actionDBProfileR']").val("");
            $("[id*='actionDBProfileJ']").val("");
            $("[id*='actionDBProfileRestore']").val("");
            $("[id*='ActionDbProfileBtn']").click();
        }
        function performD(db, d,a) {
            $("[id*='DBProfile']").val(db);
            $("[id*='actionDBProfileA']").val(a);
            $("[id*='actionDBProfileD']").val(d);
            $("[id*='actionDBProfileR']").val("");
            $("[id*='actionDBProfileJ']").val("");
            $("[id*='actionDBProfileRestore']").val("");
            $("[id*='ActionDbProfileBtn']").click();
        }
        function performR(db, r) {
            $("[id*='DBProfile']").val(db);
            $("[id*='actionDBProfileR']").val(r);
            $("[id*='actionDBProfileA']").val("");
            $("[id*='actionDBProfileD']").val("");
            $("[id*='actionDBProfileJ']").val("");
            $("[id*='actionDBProfileRestore']").val("");
            $("[id*='ActionDbProfileBtn']").click();
        }
        function performSR(db, r) {
            $("[id*='DBProfile']").val(db);
            $("[id*='actionDBProfileR']").val(r);
            $("[id*='actionDBProfileA']").val("");
            $("[id*='actionDBProfileD']").val("");
            $("[id*='actionDBProfileJ']").val("");
            $("[id*='actionDBProfileRestore']").val("");
            $("[id*='DBProfileRetore']").val(db);
            $("[id*='DBProfileRetoreConnStr']").val(r);
            $("[id*='ActionDbProfileBtn']").click();
        }
        function performJ(db, a, j) {
            $("[id*='DBProfile']").val(db);
            $("[id*='actionDBProfileJ']").val(j);
            $("[id*='actionDBProfileA']").val(a);
            $("[id*='actionDBProfileD']").val("");
            $("[id*='actionDBProfileR']").val("");
            $("[id*='actionDBProfileRestore']").val("");
            $("[id*='ActionDbProfileBtn']").click();
        }
        function performRestore(db, r) {
            $("[id*='DBProfile']").val(db);
            $("[id*='actionDBProfileR']").val("");
            $("[id*='actionDBProfileA']").val("");
            $("[id*='actionDBProfileD']").val("");
            $("[id*='actionDBProfileJ']").val("");
            $("[id*='actionDBProfileRestore']").val(r);
            $("[id*='ActionDbProfileBtn']").click();
        }
    </script>
    <div style="display:none;">
    <asp:TextBox ID="DBProfile" runat="server"  Visible="true"  ></asp:TextBox>
    <asp:TextBox ID="actionDBProfileA" runat="server"  Visible="true"  ></asp:TextBox>
    <asp:TextBox ID="actionDBProfileD" runat="server"  Visible="true"  ></asp:TextBox>
    <asp:TextBox ID="actionDBProfileR" runat="server"  Visible="true"  ></asp:TextBox>
    <asp:TextBox ID="actionDBProfileJ" runat="server"  Visible="true"  ></asp:TextBox> 
    <asp:TextBox ID="actionDBProfileRestore" runat="server"  Visible="true"  ></asp:TextBox> 
    <asp:TextBox ID="DBProfileRetore" runat="server" Width="50px" ></asp:TextBox>
    <asp:Button ID="ActionDbProfileBtn" runat="server"  Visible="true"  
        CausesValidation="True" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')"
        onclick="RunActionDBProfile_Click" Text="เริ่มดำเนินการ" Width="89px" />
    </div>
    <!-- สำรองข้อมูลทุกๆวัน ที่เวลา -->
    <!-- (hh:mm,hh:mm) -->
    <asp:Button ID="CreateTaskButton" runat="server" Visible="false"  
        CausesValidation="True" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')"
        onclick="CreateTaskButton_Click" Text="บันทึก" Width="43px" />&nbsp;&nbsp;
    <asp:Button ID="RunProcessButton" runat="server"  Visible="false"  
        CausesValidation="True" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')"
        onclick="RunProcessButton_Click" Text="เริ่มดำเนินการ" Width="89px" />
    <asp:Literal ID="TaskTimerMsg" runat="server" ></asp:Literal>
    <table border=0 cellpadding=2 cellspacing=2 style="display:<%=(TaskTimerTxt_.Text!=""?"":"none")%>;" class="DataSourceRepeater">   
    <tr><td colspan="3">สร้าง ตารางสำรองข้อมูล : <asp:TextBox ID="TaskTimerTxt" runat="server" Width="254px" Visible=false></asp:TextBox> <asp:Literal ID="TaskTimerTxt_" runat="server"></asp:Literal> </td></tr>
    <tr><th>ค่าระบบ</th><th>ข้อมูล</th><th>ตัวอย่าง</th></tr>
    <tr><th>ORA_ADM_USR</th><td><asp:TextBox ID="ORA_ADM_USR" runat="server" Width="150px"></asp:TextBox></td><td>(sys)
    </td></tr><tr><th>ORA_ADM_PWD</th><td><asp:TextBox ID="ORA_ADM_PWD" runat="server" Width="150px"></asp:TextBox></td><td>(admin)
    </td></tr><tr><th>ORA_ADM_EXP_USR</th><td><asp:TextBox ID="ORA_ADM_EXP_USR" runat="server" Width="150px"></asp:TextBox></td><td>(system)
    </td></tr><tr><th>ORA_ADM_EXP_PWD</th><td><asp:TextBox ID="ORA_ADM_EXP_PWD" runat="server" Width="150px"></asp:TextBox></td><td>(admin)
    </td></tr><tr><th>ORA_TARGET_DB_HOST</th><td><asp:TextBox ID="ORA_TARGET_DB_HOST" runat="server" Width="100px"></asp:TextBox></td><td>(192.168.99.11)
    </td></tr><tr><th>ORA_TARGET_DB_PORT</th><td><asp:TextBox ID="ORA_TARGET_DB_PORT" MaxLength="6"  runat="server" Width="100px"></asp:TextBox></td><td>(1521)
    </td></tr><tr><th>ORA_TARGET_DB_SID</th><td><asp:TextBox ID="ORA_TARGET_DB_SID" runat="server" Width="80px"></asp:TextBox></td><td>(icoop)
    </td></tr><tr><th>ORA_TARGET_USR</th><td><asp:TextBox ID="ORA_TARGET_USR" runat="server" Width="150px"></asp:TextBox></td><td>(ISCOICOOPTRN)
    </td></tr><tr><th>ORA_TARGET_PWD</th><td><asp:TextBox ID="ORA_TARGET_PWD" runat="server" Width="150px"></asp:TextBox></td><td>(iscoicooptrn)
    </td></tr><tr><th>ORA_VERSION</th><td><asp:TextBox ID="ORA_VERSION" runat="server" Width="140px"></asp:TextBox></td><td>(10.2 หรือ 11.2 หรือ 12.1)
    </td></tr><tr><th>DATAPUMP_DIR</th><td><asp:TextBox ID="DATAPUMP_DIR" MaxLength="1" runat="server" Width="20px"></asp:TextBox></td><td>(C)
    </td></tr><tr><th>DATAPUMP</th><td><asp:TextBox ID="DATAPUMP" runat="server" Width="100px"></asp:TextBox></td><td>(DATAPUMP)
    </td></tr><tr><th>ORA_OS_USR</th><td><asp:TextBox ID="ORA_OS_USR" runat="server" Width="150px"></asp:TextBox></td><td>(Administrator)
    </td></tr><tr><th>ORA_OS_PWD</th><td><asp:TextBox ID="ORA_OS_PWD" runat="server" Width="150px"></asp:TextBox></td><td>(Admin123)
    </td></tr><tr><th>NLS_LANG</th><td><asp:TextBox ID="NLS_LANG" runat="server" Width="250px"></asp:TextBox></td><td>(AMERICAN_AMERICA.TH8TISASCII)
    </td></tr><tr><th>BACKUP_PATH</th><td><asp:TextBox ID="BACKUP_PATH" runat="server" Width="250px"></asp:TextBox></td><td>(C:\DATAPUMP)
    </td></tr><tr><th>SCRIPT_PATH</th><td><asp:TextBox ID="SCRIPT_PATH" runat="server" Width="200px"></asp:TextBox></td><td>(C:\DATAPUMP)
    </td></tr><tr><th>สำรองข้อมูลวัน จันทร์ ที่เวลา</th><td><asp:TextBox ID="TaskTimerTxt_0" runat="server" Width="254px"></asp:TextBox></td><td>(hh:mm,hh:mm)</td></tr><tr><th>
    สำรองข้อมูลวัน อังคาร ที่เวลา</th><td><asp:TextBox ID="TaskTimerTxt_1" runat="server" Width="254px"></asp:TextBox></td><td>(hh:mm,hh:mm)</td></tr><tr><th>
    สำรองข้อมูลวัน พุธ ที่เวลา</th><td><asp:TextBox ID="TaskTimerTxt_2" runat="server" Width="254px"></asp:TextBox></td><td>(hh:mm,hh:mm)</td></tr><tr><th>
    สำรองข้อมูลวัน พฤหัสฯ ที่เวลา</th><td><asp:TextBox ID="TaskTimerTxt_3" runat="server" Width="254px"></asp:TextBox></td><td>(hh:mm,hh:mm)</td></tr><tr><th>
    สำรองข้อมูลวัน ศุกร์ ที่เวลา</th><td><asp:TextBox ID="TaskTimerTxt_4" runat="server" Width="254px"></asp:TextBox></td><td>(hh:mm,hh:mm)</td></tr><tr><th>
    สำรองข้อมูลวัน เสาร์ ที่เวลา</th><td><asp:TextBox ID="TaskTimerTxt_5" runat="server" Width="254px"></asp:TextBox></td><td>(hh:mm,hh:mm)</td></tr><tr><th>
    สำรองข้อมูลวัน อาทิตย์ ที่เวลา</th><td><asp:TextBox ID="TaskTimerTxt_6" runat="server" Width="254px"></asp:TextBox></td><td>(hh:mm,hh:mm)</td></tr>
    <tr><td colspan="3" align="center">

    <asp:Button ID="NewDBProfileBTN" runat="server"  Visible="true"  
        CausesValidation="True" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')"
        onclick="RunActionNewDBProfile_Click" Text="บันทึกเป็นProfileใหม่" Width="120px" />
        &nbsp; &nbsp; &nbsp;
    <asp:Button ID="SaveDBProfileBTN" runat="server"  Visible="true"  
        CausesValidation="True" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')"
        onclick="RunActionSaveDBProfile_Click" Text="บันทึกปรับปรุงProfile" Width="120px" />
         &nbsp; &nbsp; &nbsp;
    <asp:Button ID="DelDBProfileBTN" runat="server"  Visible="true"  
        CausesValidation="True" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')"
        onclick="RunActionDeleteDBProfile_Click" Text="ลบProfile" Width="89px" />
        
        </td></tr>
    </table>
    <br />
    รายการแฟ้มข้อมูลสำรองระบบ : <asp:TextBox ID="DBProfileRetoreConnStr" runat="server" Width="300px" Visible=false ></asp:TextBox>
     <br />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_ad_monitor_select.aspx.cs"
    Inherits="Saving.Applications.admin.dlg.w_dlg_ad_monitor_select" Culture="th-TH" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=postSave%>
    <%=postDelete%>
    <style type="text/css">
        span
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        input[type=text]
        {
            font-family: Tahoma;
            font-size: 12px;
            color: #1243AA;
            width: 100px;
            height: 16px;
            border: 1px solid #000000;
            background-color: #EECCDD;
        }
        select
        {
            background-color: #EECCDD;
            color: #1243AA;
            color: #1243AA;
        }
        .tbSqlSyntax
        {
            font-family: Tahoma;
            font-size: 13px;
            width: 700px;
            height: 100px;
            border: 1px solid #000000;
            background-color: #EECCDD;
            color: #1243AA;
        }
        .fildName
        {
            font-family: Tahoma;
            font-size: 12px;
            border: 1px solid #000000;
            width: 700px;
        }
        .gridview01
        {
        }
        .gridview01 th, td
        {
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">
        function DialogLoadComplete() {
            Gcoop.AddDisEnter("tbSqlSyntax");
        }

        function OnClickButtonClean() {
            Gcoop.GetEl("tbSelectName").value = "";
            Gcoop.GetEl("tbDescription").value = "";
            Gcoop.GetEl("tbSaveBy").value = "";
            Gcoop.GetEl("tbSqlSyntax").value = "";
        }

        function OnClickButton() {
            var isSave = false;
            try {
                var selectName = Gcoop.GetEl("tbSelectName").value;
                var description = Gcoop.GetEl("tbDescription").value;
                var saveBy = Gcoop.GetEl("tbSaveBy").value;
                var sqlSyntax = Gcoop.GetEl("tbSqlSyntax").value;
                var dbType = "";

                if (Gcoop.Trim(selectName) == "") {
                    throw "กรุณากรอกค่า ชื่อ(uniqe/primary)";
                }
                if (Gcoop.Trim(description) == "") {
                    throw "กรุณากรอกค่า รายละเอียด";
                }

                try {
                    var elDbType = Gcoop.GetEl("ddDbType");
                    dbType = elDbType.value;
                } catch (err1) {
                    dbType = "";
                }

                if (Gcoop.Trim(dbType) == "") {
                    throw "กรุณาเลือก Database";
                }

                if (Gcoop.Trim(dbType) == "Oracle") {
                    throw "ระบบยังไม่รองรับ Database: Oracle :(";
                }

                isSave = true;

            } catch (err) {
                alert(err);
            }
            if (isSave) {
                if (confirm("ยืนยันการบันทึกข้อมูล '" + selectName + "'")) {
                    postSave();
                }
            }
        }

        function gridUsing(selectName) {
            parent.UsingSelectName(selectName);
        }

        function gridEdit(selectName) {
            try {
                var description = Gcoop.GetEl("hd_description_" + selectName).value;
                var saveBy = Gcoop.GetEl("hd_modify_by_" + selectName).value;
                var sqlSyntax = Gcoop.GetEl("hd_sql_syntax_" + selectName).value;

                Gcoop.GetEl("tbSelectName").value = selectName;
                Gcoop.GetEl("tbDescription").value = description;
                Gcoop.GetEl("tbSaveBy").value = saveBy;
                Gcoop.GetEl("tbSqlSyntax").value = sqlSyntax;
            } catch (err) {
                alert(err);
            }
        }

        function gridDelete(selectName) {
            if (confirm("ยืนยันการลบข้อมูล " + selectName)) {
                OnClickButtonClean();
                Gcoop.GetEl("HdDeleteName").value = selectName + "";
                postDelete();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
            Width="700px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
            CellPadding="3" CssClass="gridview01">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        เลือก
                    </HeaderTemplate>
                    <ItemTemplate>
                        <u style="cursor: pointer;" onclick="gridUsing('<%#Eval("select_name")%>')">ใช้งาน</u>
                        &nbsp; <u style="cursor: pointer;" onclick="gridEdit('<%#Eval("select_name")%>')">แก้ไข</u>
                        <input type="hidden" id="hd_description_<%#Eval("select_name")%>" value="<%#Eval("description")%>" />
                        <input type="hidden" id="hd_modify_by_<%#Eval("select_name")%>" value="<%#Eval("modify_by")%>" />
                        <input type="hidden" id="hd_sql_syntax_<%#Eval("select_name")%>" value="<%#Eval("sql_syntax")%>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="select_name" HeaderText="ชื่อ(Uniqe)" />
                <asp:BoundField DataField="description" HeaderText="รายละเอียด" />
                <asp:BoundField DataField="create_by" HeaderText="สร้างโดย" />
                <asp:BoundField DataField="modify_by" HeaderText="แก้ไขโดย" />
                <asp:BoundField DataField="modify_time" HeaderText="แก้ไขเวลา" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        ลบ
                    </HeaderTemplate>
                    <ItemTemplate>
                        <u style="cursor: pointer; color: Red;" onclick="gridDelete('<%#Eval("select_name")%>')">
                            ลบ</u>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <br />
        <asp:Label ID="Label1" runat="server" Text="ชื่อ(uniqe/primary): "></asp:Label>
        <asp:TextBox ID="tbSelectName" runat="server" Width="110px"></asp:TextBox>
        &nbsp;
        <asp:Label ID="Label2" runat="server" Text="รายละเอียด: "></asp:Label>
        <asp:TextBox ID="tbDescription" runat="server" Width="120px"></asp:TextBox>
        &nbsp;
        <asp:Label ID="Label5" runat="server" Text="Database: "></asp:Label>
        <asp:DropDownList ID="ddDbType" runat="server">
            <asp:ListItem Text="MySQL" Value="MySQL"></asp:ListItem>
            <asp:ListItem Text="Oracle" Value="Oracle"></asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="Label3" runat="server" Text="ผู้บันทึก: "></asp:Label>
        <asp:TextBox ID="tbSaveBy" runat="server" Width="60px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="รายชื่อ column: "></asp:Label>
        <div class="fildName">
            session_id, hit_date, server_ip, client_ip, application, username, coop_id, coop_control,
            current_page, current_pagedesc, url_absolute, methode, jspostback, webservice, webservice_ram,
            webservicereport, webservicereport_ram, server_message, message_type, load_time</div>
        <br />
        <asp:Label ID="Label6" runat="server" Text="คำสั่ง SELECT * FROM HITLOG WHERE ....., ORDER BY ....., LIMIT ... : "></asp:Label>
        <asp:TextBox ID="tbSqlSyntax" runat="server" TextMode="MultiLine" CssClass="tbSqlSyntax"></asp:TextBox>
        <br />
        <br />
        <div align="center">
            <input type="button" value="บันทึกข้อมูล" style="width: 100px" onclick="OnClickButton()" />
            &nbsp;
            <input type="button" value="ล้างข้อมูล" style="width: 100px" onclick="OnClickButtonClean()" />
        </div>
        <asp:HiddenField ID="HdDeleteName" Value="" runat="server" />
    </div>
    </form>
</body>
</html>
